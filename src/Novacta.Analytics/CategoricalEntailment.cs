// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a collection of premises about a set of feature 
    /// categorical variables that implies a specific response category. 
    /// The conclusion is entailed by the premises with an eventually
    /// partial truth value, ranging between completely false to
    /// completely true.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="CategoricalEntailment"/> instance represents a
    /// relationship between a set of <see cref="FeatureVariables"/> 
    /// and a corresponding <see cref="ResponseVariable"/>.
    /// </para>
    /// <para>
    /// The relationship has the form of an implication 
    /// from a collection of <see cref="FeaturePremises"/>, statements
    /// about the available features,
    /// to a corresponding <see cref="ResponseConclusion"/>,
    /// a category <see cref="Category.Code"/> 
    /// in the <see cref="ResponseVariable"/>.
    /// </para>
    /// <para>
    /// The conclusion is entailed
    /// by the premises with a degree of confirmation represented
    /// by the <see cref="TruthValue"/> of the instance.
    /// </para>
    /// <para>
    /// Let us assume that items from a given 
    /// feature space <latex>\FS</latex> must be classified 
    /// into a set <latex>\RD</latex> of labels. 
    /// If <latex>L</latex> feature variables
    /// are taken into account, and if 
    /// variable <latex>l\in\{0,\dots,L-1\}</latex> has 
    /// finite domain <latex>\FD_l</latex>, then <latex>\FS</latex> can
    /// be represented as the 
    /// Cartesian product <latex>\FD_0\times\dots\times\FD_{L-1}</latex>.
    /// A categorical entailment is a 
    /// triple <latex>\round{\EP,\EC,\ET}</latex>, 
    /// where <latex>\EP</latex> is a subset of <latex>\FS</latex>
    /// representing the <see cref="FeaturePremises"/>, 
    /// <latex>\EC\in\RD</latex> is the <see cref="ResponseConclusion"/>,
    /// and <latex>\ET</latex> is the <see cref="TruthValue"/>. 
    /// </para>
    /// <para>
    /// Given a <see cref="CategoricalDataSet"/> instance in which 
    /// observations of the
    /// <see cref="FeatureVariables"/> are included, you can check 
    /// for what of its rows the premises are satisfied by
    /// calling method <see cref="ValidatePremises(CategoricalDataSet, 
    /// IndexCollection)"/>. 
    /// </para>
    /// <para id='NonemptyVariables'>
    /// Empty feature or response domains are not allowed. 
    /// </para>
    /// <para id='EmptyPremises'>
    /// A premise can be empty, 
    /// but in such case it is represented 
    /// as matching the corresponding feature domain. 
    /// Equivalently, a premise which is not a nonempty proper
    /// subset of the domain is always 
    /// valid for every item in the feature space.
    /// </para>
    /// <para>
    /// You can't directly instantiate a <see cref="CategoricalEntailment"/>
    /// object. However, categorical entailments can be exploited for classification 
    /// purposes via 
    /// <see cref="CategoricalEntailmentEnsembleClassifier"/> instances.
    /// Entailments can be included in such a classifier
    /// by calling its method <see cref="CategoricalEntailmentEnsembleClassifier.Add(
    /// IList{SortedSet{double}}, double, double)"/>.
    /// They can be populated automatically by executing methods
    /// <see cref="CategoricalEntailmentEnsembleClassifier.AddTrained(
    /// CategoricalDataSet, IndexCollection, int, int, bool, bool)"/>
    /// or <see cref="CategoricalEntailmentEnsembleClassifier.Train(
    /// CategoricalDataSet, IndexCollection, int, int, bool, bool)"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="CategoricalEntailmentEnsembleClassifier"/>
    /// <seealso cref="Advanced.CategoricalEntailmentEnsembleOptimizationContext"/>
    public class CategoricalEntailment 
    {
        #region State

        private readonly List<SortedSet<double>> featurePremises;
        private double truthValue;

        /// <summary>
        /// Gets a boolean array, storing at a given position <c>true</c>
        /// if the corresponding premise is a nonempty proper subset of the
        /// feature domain; otherwise <c>false</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An empty premise, or a premise matching
        /// the feature domain, is always satisfied by a given item.
        /// </para>
        /// </remarks>
        private readonly bool[] isNonemptyProperPremise;

        /// <summary>
        /// Gets the list of feature variables about which 
        /// this instance defines its <see cref="FeaturePremises"/>.
        /// </summary>
        /// <value>
        /// The list of feature variables about which 
        /// this instance defines its <see cref="FeaturePremises"/>.
        /// </value>
        public IReadOnlyList<CategoricalVariable> FeatureVariables { get; }

        /// <summary>
        /// Gets the response variable from which 
        /// this instance defines its <see cref="ResponseConclusion"/>.
        /// </summary>
        /// <value>
        /// The response variable from which 
        /// this instance defines its <see cref="ResponseConclusion"/>.
        /// </value>
        public CategoricalVariable ResponseVariable { get; }

        /// <summary>
        /// Gets the list of premises this instance defines about its 
        /// <see cref="FeatureVariables"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <see cref="IReadOnlyCollection{T}.Count"/> of 
        /// <see cref="FeaturePremises"/> is the same 
        /// of <see cref="FeatureVariables"/>.
        /// At a given position, the list stores a <see cref="SortedSet{T}"/>
        /// representing a subset of 
        /// the <see cref="CategoricalVariable.CategoryCodes"/> in the 
        /// corresponding variable.
        /// </para>
        /// </remarks>
        /// <value>
        /// The list of premises this instance defines about its
        /// <see cref="FeatureVariables"/>.
        /// </value>
        public IReadOnlyList<SortedSet<double>> FeaturePremises { get; }

        /// <summary>
        /// Gets the category <see cref="Category.Code"/> 
        /// in <see cref="ResponseVariable"/> derived 
        /// by this instance when its <see cref="FeaturePremises"/>
        /// are satisfied.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Property <see cref="ResponseConclusion"/> returns
        /// a <see cref="double"/> included in the 
        /// <see cref="CategoricalVariable.CategoryCodes"/> of 
        /// <see cref="ResponseVariable"/>.
        /// </para>
        /// </remarks>
        /// <value>
        /// The category code in <see cref="ResponseVariable"/> derived 
        /// by this instance when its <see cref="FeaturePremises"/>
        /// are satisfied.
        /// </value>
        public double ResponseConclusion { get; }

        /// <summary>
        /// Gets or sets the eventually partial truth value assigned to 
        /// this instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The truth value of a <see cref="CategoricalEntailment"/>
        /// instance may be any real number in the closed interval 
        /// <latex>[0,1]</latex>, ranging from a completely false to 
        /// a completely true value, respectively.
        /// </para>
        /// </remarks>
        /// <value>
        /// A value representing the truth value of this instance.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater than unity.
        /// </exception>
        public double TruthValue
        {
            get => this.truthValue;
            set
            {
                if ((value < 0.0) || (1.0 < value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            0.0,
                            1.0));
                }
                this.truthValue = value;
            }
        }

        #endregion

        #region Constructors and factory methods 

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="CategoricalEntailment"/> class as
        /// defined by the corresponding sub-state of a
        /// <see cref="Advanced.CategoricalEntailmentEnsembleOptimizationContext"/>, 
        /// and trained by the given <see cref="CategoricalEntailmentEnsembleTrainer"/>
        /// instance.
        /// </summary>
        /// <param name="entailmentRepresentation">
        /// A matrix representing a valid state of a
        /// <see cref="Advanced.CategoricalEntailmentEnsembleOptimizationContext"/> instance.
        /// </param>
        /// <param name="featureVariables">
        /// The list of feature categorical variables about which 
        /// this instance defines its <see cref="FeaturePremises"/>.
        /// </param>
        /// <param name="responseVariable">
        /// The categorical variable from which 
        /// this instance defines its <see cref="ResponseConclusion"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// Let <latex>L</latex> be
        /// the number of feature variables. For <latex>j=0,\dots,L-1</latex>, 
        /// let <latex>n_j</latex> be the
        /// number of categories for feature <latex>\FD_j</latex>. 
        /// Let <latex>n_{\RD}</latex> be the number of  
        /// categories of response <latex>\RD</latex>.
        /// </para>
        /// <para>
        /// An <paramref name="entailmentRepresentation"/> instance
        /// is a partitioned 
        /// row vector, say 
        /// <latex>\mx{x_{r,0} &amp; \cdots &amp; x_{r,M-1} &amp;
        /// y_r &amp; w_r}</latex>, where, 
        /// for <latex>j=0,\dots,M-1</latex>, 
        /// <latex>x_{r,j} = \mx{x_{r,j,0} &amp; \cdots &amp; 
        /// x_{r,j,m_j-1}}</latex>,
        /// with <latex>x_{r,j,l}</latex> being unity if 
        /// the <latex>l</latex>-th category code of <latex>X_j</latex> is 
        /// included in the entailment, zero otherwise.
        /// Block <latex>y_r</latex> is a binary
        /// vector <latex>\mx{y_{r,0} &amp; \cdots &amp; y_{r,K-1}}</latex> in 
        /// which there 
        /// is only one entry equal 
        /// to unity corresponding to the selected 
        /// <see cref="ResponseConclusion"/>, and
        /// <latex>w_r</latex> represent the truth value of the entailment.
        /// </para>
        /// </remarks>
        /// <seealso cref="Advanced.CategoricalEntailmentEnsembleOptimizationContext"/>
        internal CategoricalEntailment(
            DoubleMatrix entailmentRepresentation,
            IReadOnlyList<CategoricalVariable> featureVariables,
            CategoricalVariable responseVariable)
        {
            SortedSet<double> premise;
            IReadOnlyList<Category> currentFeatureCategories;
            int numberOfFeatureVariables = featureVariables.Count;

            this.isNonemptyProperPremise = new bool[numberOfFeatureVariables];
            this.featurePremises = new List<SortedSet<double>>(numberOfFeatureVariables);

            int entailmentRepresentationIndex = 0;

            for (int f = 0; f < numberOfFeatureVariables; f++)
            {
                this.featurePremises.Add(new SortedSet<double>());
                premise = this.featurePremises[f];
                currentFeatureCategories = featureVariables[f].Categories;
                int numberOfCurrentFeatureCategories = currentFeatureCategories.Count;

                for (int c = 0; c < numberOfCurrentFeatureCategories; c++)
                {
                    if (entailmentRepresentation[entailmentRepresentationIndex + c] == 1.0)
                    {
                        premise.Add(currentFeatureCategories[c].Code);
                    }
                }

                this.isNonemptyProperPremise[f] =
                    premise.IsProperSubsetOf(featureVariables[f].CategoryCodes)
                    &&
                    premise.Count > 0
                    ? true : false;

                entailmentRepresentationIndex += numberOfCurrentFeatureCategories;
            }

            IReadOnlyList<Category> responseCategories = responseVariable.Categories;
            for (int c = 0; c < responseCategories.Count; c++)
            {
                if (entailmentRepresentation[entailmentRepresentationIndex + c] == 1.0)
                {
                    this.ResponseConclusion = responseCategories[c].Code;
                    entailmentRepresentationIndex += responseCategories.Count;
                    break;
                }
            }

            this.truthValue = entailmentRepresentation[entailmentRepresentationIndex];
            this.FeaturePremises = this.featurePremises;
            this.ResponseVariable = responseVariable;
            this.FeatureVariables = featureVariables;
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="CategoricalEntailment"/> class having
        /// the specified premises on feature variables, 
        /// derived response category, and truth value.
        /// </summary>
        /// <param name="featurePremises">
        /// The list of premises the entailment defines about its
        /// <see cref="FeatureVariables"/>.
        /// </param>
        /// <param name="responseConclusion">
        /// The category of <see cref="ResponseVariable"/> derived 
        /// by the entailment when its <see cref="FeaturePremises"/>
        /// are satisfied.
        /// </param>
        /// <param name="truthValue">
        /// The eventually partial truth value assigned to
        /// the entailment.
        /// </param>
        /// <param name="featureVariables">
        /// The list of feature categorical variables about which 
        /// the entailment defines its <see cref="FeaturePremises"/>.
        /// </param>
        /// <param name="responseVariable">
        /// The categorical variable from which 
        /// the entailment defines its <see cref="ResponseConclusion"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// Variables passed as parameters to this constructor will be 
        /// set as read-only.
        /// </para>
        /// </remarks>
        internal CategoricalEntailment(
            IReadOnlyList<CategoricalVariable> featureVariables,
            CategoricalVariable responseVariable,
            IList<SortedSet<double>> featurePremises,
            double responseConclusion,
            double truthValue)
        {
            int numberOfPremises = featureVariables.Count;

            this.isNonemptyProperPremise = new bool[numberOfPremises];
            this.featurePremises = new List<SortedSet<double>>(numberOfPremises);

            for (int j = 0; j < numberOfPremises; j++)
            {
                this.featurePremises.Add(new SortedSet<double>(featurePremises[j]));
                featureVariables[j].SetAsReadOnly();

                this.isNonemptyProperPremise[j] =
                    this.featurePremises[j].IsProperSubsetOf(featureVariables[j].CategoryCodes)
                    && this.featurePremises[j].Count > 0
                    ? true : false;
            }

            responseVariable.SetAsReadOnly();

            this.FeaturePremises = new List<SortedSet<double>>(featurePremises);
            this.ResponseVariable = responseVariable;
            this.FeatureVariables = new List<CategoricalVariable>(featureVariables);
            this.ResponseConclusion = responseConclusion;
            this.truthValue = truthValue;
        }

        #endregion

        #region Validate premises

        /// <summary>
        /// Validates that the premises of this instance are satisfied 
        /// by the specified item.
        /// </summary>
        /// <param name="item">
        /// The item at which premises must be validated.
        /// </param>
        /// <returns>
        /// <c>true</c> if the item satisfies the premises, 
        /// <c>false</c> otherwise.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The item is intended as a row extracted from 
        /// the <see cref="CategoricalDataSet.Data"/> property of a feature
        /// data set used to train a <see cref="CategoricalEntailmentEnsembleClassifier"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="CategoricalEntailmentEnsembleClassifier"/>
        internal bool ValidatePremises(DoubleMatrix item)
        {
            SortedSet<double> premise;
            for (int j = 0; j < item.Count; j++)
            {
                premise = this.featurePremises[j];
                if (!premise.Contains(item[j])
                    &&
                    this.isNonemptyProperPremise[j])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates that the premises of this instance are satisfied 
        /// by the categorical items in the specified data set.
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose rows contain the specified items.
        /// </param>
        /// <param name="featureVariableIndexes">
        /// The zero-based indexes of the data set columns containing 
        /// information about the features involved in the premises.</param>
        /// <returns>
        /// The collection of data set row indexes containing the items 
        /// satisfying the premises of this instance.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Let <latex>L</latex> be the <see cref="ICollection{T}.Count"/> of
        /// the <see cref="FeaturePremises"/> of this instance. It is expected
        /// that <paramref name="featureVariableIndexes"/> has the same count, 
        /// and that the <latex>l</latex>-th position 
        /// of <paramref name="featureVariableIndexes"/>, say <latex>k_l</latex>,
        /// is the index of the 
        /// column that, in <paramref name="dataSet"/>, contains observations
        /// about the same feature variable on which is built the <latex>l</latex>-th
        /// premise of this instance.
        /// </para>
        /// <para>
        /// Given a data set row index <latex>i</latex>, let the 
        /// <latex>l</latex>-th premise be represented by set 
        /// <latex>\EP_l \subset \FD_l</latex>, where 
        /// <latex>\FD_l</latex> is the domain of the corresponding feature,
        /// and let <latex>x_{i,l}</latex>
        /// be the category code of the <latex>l</latex>-th feature variable observed 
        /// at the <latex>i</latex>-th item.
        /// Then index <latex>i</latex> is included in the returned 
        /// collection if and only if the following condition holds true:
        /// <latex>x_{i,l} \in \EP_l</latex>, for <latex>l=0,\dots,L-1</latex>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dataSet"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariableIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// At least an index in <paramref name="featureVariableIndexes"/> is 
        /// not a valid column index for <paramref name="dataSet"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="IndexCollection.Count"/> of 
        /// <paramref name="featureVariableIndexes"/> is not equal to the 
        /// number of features defined by this instance.
        /// </exception>
        public IndexCollection ValidatePremises(
            CategoricalDataSet dataSet,
            IndexCollection featureVariableIndexes)
        {
            #region Input validation

            if (dataSet is null)
            {
                throw new ArgumentNullException(nameof(dataSet));
            }

            if (featureVariableIndexes is null)
            {
                throw new ArgumentNullException(nameof(featureVariableIndexes));
            }

            if (featureVariableIndexes.Max >= dataSet.NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(featureVariableIndexes),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", nameof(dataSet)));
            }

            if (this.featurePremises.Count != featureVariableIndexes.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT"),
                    nameof(featureVariableIndexes));
            }

            #endregion

            List<int> validatedItemIndexes = new();

            for (int i = 0; i < dataSet.NumberOfRows; i++)
            {
                bool isItemSatisfyingPremises = true;

                for (int j = 0; j < featureVariableIndexes.Count; j++)
                {
                    SortedSet<double> premise = this.featurePremises[j];

                    if (!premise.Contains(dataSet.Data[i, featureVariableIndexes[j]])
                        &&
                        this.isNonemptyProperPremise[j])
                    {
                        isItemSatisfyingPremises = false;
                        break;
                    }
                }

                if (isItemSatisfyingPremises)
                {
                    validatedItemIndexes.Add(i);
                }
            }

            return IndexCollection.FromArray(
                validatedItemIndexes.ToArray(), false);
        }

        #endregion

        #region Object

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine(" IF ");
            int numberOfFeatures = this.featurePremises.Count;
            for (int i = 0; i < numberOfFeatures; i++)
            {
                stringBuilder.AppendFormat(
                    CultureInfo.InvariantCulture, " {0} IN ", this.FeatureVariables[i].Name);
                stringBuilder.Append("{ ");

                IEnumerable<double> codes = this.isNonemptyProperPremise[i] ?
                    this.featurePremises[i]
                    :
                    this.FeatureVariables[i].CategoryCodes;

                foreach (var code in codes)
                {
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture, "{0} ",
                        code.ToString(CultureInfo.InvariantCulture));
                }
                stringBuilder.AppendLine("} ");
                if (i < numberOfFeatures - 1)
                {
                    stringBuilder.AppendLine(" AND ");
                }
            }
            stringBuilder.AppendLine(" THEN ");

            stringBuilder.AppendLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    " {0} IS {1}",
                    this.ResponseVariable.Name,
                    this.ResponseConclusion));

            stringBuilder.AppendLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    " WITH TRUTH VALUE {0}",
                    this.TruthValue));

            return stringBuilder.ToString();
        }

        #endregion
    }
}