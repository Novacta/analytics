// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Novacta.Analytics.Advanced;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a classifier that exploits an ensemble of categorical
    /// entailments to assign labels to items from a given feature
    /// space.
    /// </summary>
    /// <remarks>
    /// <para><b>Categorical entailments for maximal accuracy</b></para> 
    /// <para>
    /// Let us assume that items from a given categorical space 
    /// <latex>\FS</latex> must be 
    /// classified into a set <latex>\RD</latex> of labels. 
    /// If <latex>L</latex> input attributes 
    /// are taken into account, and if attribute 
    /// <latex>l\in\{0,\dots,L-1\}</latex> has nonempty
    /// finite domain <latex>\FD_l</latex>, then 
    /// <latex>\FS</latex> can be represented as the 
    /// Cartesian product <latex>\FD_0\times\dots\times\FD_{L-1}</latex>.
    /// </para>
    /// <para>
    /// A <see cref="CategoricalEntailment"/> instance, 
    /// say <latex>\EI</latex>, is a 
    /// triple <latex>\round{\EP,\EC,\ET}</latex>, 
    /// where <latex>\EP</latex> is a subset of <latex>\FS</latex>, 
    /// <latex>\EC\in\RD</latex>, and <latex>\ET</latex> is a truth value.
    /// Given an 
    /// instance <latex>\FSI\in\FS</latex>, the entailment will classify 
    /// <latex>\FSI</latex> as <latex>\EC</latex> if and 
    /// only if <latex>\FSI\in\EP</latex>. 
    /// </para>
    /// <para><b>Instantiation and training</b></para> 
    /// <para>
    /// Method <see cref="Train(
    /// CategoricalDataSet, IndexCollection, int, int, bool, bool)"/> returns
    /// a <see cref="CategoricalEntailmentEnsembleClassifier"/> instance 
    /// whose ensemble of categorical entailments has been trained by 
    /// maximizing the accuracy of label assignments to items in a 
    /// specified data set.
    /// </para>
    /// <para>
    /// Classifiers can also be instantiated by calling the constructor
    /// <see cref="CategoricalEntailmentEnsembleClassifier(
    /// IList{CategoricalVariable}, CategoricalVariable)"/>, and then adding 
    /// categorical entailments by calling method
    /// <see cref="Add(IList{SortedSet{double}}, double, double)"/>, or 
    /// <see cref="AddTrained(CategoricalDataSet, IndexCollection, int, int, bool, bool)"/>.
    /// </para>
    /// <inheritdoc cref="CategoricalEntailment" path="para[@id='NonemptyVariables']"/>
    /// <inheritdoc cref="CategoricalEntailment" path="para[@id='EmptyPremises']"/>
    /// <para><b>Classification</b></para> 
    /// <para>
    /// Property <see cref="Entailments"/>
    /// gets the list of the categorical entailments in the ensemble of a given
    /// classifier.
    /// </para>
    /// <para>
    /// Given a classifier based on, say, <latex>J</latex> categorical 
    /// entailments,
    /// entailment <latex>\EI_j=\round{\EP_j,\EC_j,\ET_j}</latex>, 
    /// with <latex>j=0,\dots,J-1</latex>,
    /// is said to vote to classify <latex>\FSI</latex> as <latex>\EC</latex> 
    /// whenever <latex>\FSI\in\EP_j</latex>.  
    /// Unlabeled items can thus be 
    /// classified via the collection of 
    /// <see cref="Entailments"/> by 
    /// performing majority voting procedures, where 
    /// the classification <latex>\varsigma</latex> of a 
    /// given item <latex>\FSI</latex>, is defined as 
    /// satisfying
    /// <latex mode='display'>
    /// \varsigma\round{\FSI}={\arg\max}_{\RC\in\RD}\sum_{j\in\set{0,\dots,J-1}\,:\,
    /// \FSI\in\EP_j\,\wedge\,\RC=\EC_j} \ET_j \,,
    /// </latex>
    /// and ties are eventually resolved by selecting one of the 
    /// maximizing arguments at random.
    /// </para>
    /// <para>
    /// Method <see cref="Classify(
    /// CategoricalDataSet, IndexCollection)"/> returns such classification
    /// for a given collection of items in the feature space.
    /// </para>
    /// <para><b>Advanced scenarios</b></para> 
    /// <para>
    /// Internally, the problem of training an ensemble of 
    /// categorical entailments is solved 
    /// via a default 
    /// Cross-Entropy context of type
    /// <see cref="CategoricalEntailmentEnsembleOptimizationContext"/>.
    /// For advanced scenarios, in which additional control on the 
    /// parameters of the underlying algorithm is needed, or a 
    /// different performance function should be optimized, a specialized 
    /// context can be instantiated and hence exploited executing
    /// method <see cref="SystemPerformanceOptimizer.Optimize(
    /// SystemPerformanceOptimizationContext, double, int)">Optimize</see>
    /// on a <see cref="SystemPerformanceOptimizer"/> 
    /// object.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, a classifier is trained on
    /// a categorical data set. Items are thus classified and
    /// the corresponding accuracy computed.
    /// </para>
    /// <code title="Exploiting a classifier based on an ensemble of categorical entailments" 
    /// source="..\Novacta.Analytics.CodeExamples\CategoricalEntailmentEnsembleClassifierExample0.cs.txt" 
    /// language="cs" />
    /// </example>
    /// <seealso cref="CategoricalEntailment"/>
    /// <seealso cref="CategoricalEntailmentEnsembleOptimizationContext"/>
    public class CategoricalEntailmentEnsembleClassifier
    {
        #region Status

        /// <summary>
        /// Gets the categorical variables defining the 
        /// feature space whose items can be classified by
        /// this instance.
        /// </summary>
        /// <value>
        /// The categorical variables defining the 
        /// feature space whose items can be classified by
        /// this instance.
        /// </value>
        public IReadOnlyList<CategoricalVariable> FeatureVariables { get; }

        /// <summary>
        /// Gets the response variable whose categories 
        /// this instance exploits for classification.
        /// </summary>
        /// <value>
        /// The response variable whose categories 
        /// this instance exploits for classification.
        /// </value>
        public CategoricalVariable ResponseVariable { get; }

        /// <summary>
        /// A list of pairs where each response code is attached to an index.
        /// </summary>
        private readonly SortedList<double, int> responseCodeIndexPairs;

        /// <summary>
        /// Backing field for property <see cref="Entailments"/>.
        /// </summary>
        private readonly List<CategoricalEntailment> entailments;

        /// <summary>
        /// Gets the list of categorical entailments contributing
        /// to classification.
        /// </summary>
        /// <value>
        /// The list of categorical entailments contributing
        /// to classification.
        /// </value>
        public IReadOnlyList<CategoricalEntailment> Entailments { get { return this.entailments; } }

        /// <summary>
        /// A random number generator aimed to resolve at random 
        /// ties among votes in <see cref="Classify(CategoricalDataSet, IndexCollection)"/>.
        /// </summary>
        private readonly RandomNumberGenerator randomNumberGenerator =
            RandomNumberGenerator.CreateMT19937(777777);

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="CategoricalEntailmentEnsembleClassifier"/> class 
        /// whose categorical entailments
        /// will classify items from the space defined by the specified 
        /// feature variables by assigning categories of 
        /// the given response variable.
        /// </summary>
        /// <param name="featureVariables">
        /// The collection of categorical variables that define the 
        /// space whose items can be classified from this instance.
        /// </param>
        /// <param name="responseVariable">
        /// The response variable whose categories must be assigned to 
        /// classify items in a feature space through this instance.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="CategoricalEntailment" path="para[@id='NonemptyVariables']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="featureVariables"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="responseVariable"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="featureVariables"/> is empty since its 
        /// <see cref="ICollection{T}.Count"/> is zero.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariables"/> contains at least a
        /// variable that has no categories.<br/>
        /// -or-<br/>
        /// <paramref name="responseVariable"/> has no categories.
        /// </exception>
        public CategoricalEntailmentEnsembleClassifier(
            IList<CategoricalVariable> featureVariables,
            CategoricalVariable responseVariable)
        {
            if (featureVariables is null)
            {
                throw new ArgumentNullException(nameof(featureVariables));
            }

            if (featureVariables.Count == 0)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY"),
                    nameof(featureVariables));
            }

            for (int j = 0; j < featureVariables.Count; j++)
            {
                if (featureVariables[j].NumberOfCategories == 0)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CEE_VARIABLE_IS_EMPTY"),
                            j + "-th feature"),
                        nameof(featureVariables));
                }
            }

            if (responseVariable is null)
            {
                throw new ArgumentNullException(nameof(responseVariable));
            }

            if (responseVariable.NumberOfCategories == 0)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CEE_VARIABLE_IS_EMPTY"),
                        "response"),
                    nameof(responseVariable));
            }

            this.FeatureVariables = new List<CategoricalVariable>(featureVariables);

            foreach (var featureVariable in this.FeatureVariables)
            {
                featureVariable.SetAsReadOnly();
            }

            this.responseCodeIndexPairs = new SortedList<double, int>();
            int i = 0;
            foreach (var code in responseVariable.CategoryCodes)
            {
                this.responseCodeIndexPairs[code] = i;
                i++;
            }
            this.ResponseVariable = responseVariable;
            this.ResponseVariable.SetAsReadOnly();

            this.entailments = new List<CategoricalEntailment>();
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CategoricalEntailmentEnsembleClassifier" /> class 
        /// by training an ensemble of categorical entailments on the 
        /// specified features and response categorical 
        /// variables in a given data set.
        /// </summary>
        /// <param name="dataSet">The categorical data set containing 
        /// information about
        /// the available feature and response variables.
        /// </param>
        /// <param name="featureVariableIndexes">
        /// The zero-based indexes of the columns in <paramref name="dataSet"/>
        /// containing observations about 
        /// the feature variables on which premises must be defined.
        /// </param>
        /// <param name="responseVariableIndex">
        /// The zero-based index of the column in <paramref name="dataSet"/> 
        /// containing observations about the response variable.
        /// </param>
        /// <param name="numberOfTrainedCategoricalEntailments">
        /// The number of categorical entailments to be trained.
        /// </param>
        /// <param name="allowEntailmentPartialTruthValues">
        /// If set to <c>true</c> signals that the truth value of a 
        /// categorical entailment must be equal to the homogeneity 
        /// of the probability distribution from which its conclusion has been
        /// drawn. Otherwise, the truth value is unity.
        /// </param>
        /// <param name="trainSequentially">
        /// If set to <c>true</c> signals that the ensemble is trained 
        /// sequentially, i.e. it starts as an empty collection, and new
        /// categorical entailments are added through a step-by-step 
        /// procedure to the trained ensemble,
        /// by selecting, at each step, the entailment that better 
        /// improves the system's performance of the current ensemble.
        /// Otherwise, the categorical entailments are trained simultaneously.
        /// </param>
        /// <returns>
        /// The instance of the 
        /// <see cref="CategoricalEntailmentEnsembleClassifier" /> class 
        /// based on the trained ensemble of categorical entailments.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dataSet"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariableIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfTrainedCategoricalEntailments"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariableIndexes"/> contains values which 
        /// are not valid column indexes for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="dataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="responseVariableIndex"/> is
        /// not a valid column index for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="dataSet"/>.
        /// </exception>
        public static CategoricalEntailmentEnsembleClassifier Train(
            CategoricalDataSet dataSet,
            IndexCollection featureVariableIndexes,
            int responseVariableIndex,
            int numberOfTrainedCategoricalEntailments,
            bool allowEntailmentPartialTruthValues,
            bool trainSequentially)
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

            if (featureVariableIndexes.maxIndex >= dataSet.Data.NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(featureVariableIndexes),
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                            "column", 
                            nameof(dataSet)));
            }

            if (responseVariableIndex >= dataSet.Data.NumberOfColumns
                ||
                responseVariableIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(responseVariableIndex),
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                            "column", 
                            nameof(dataSet)));
            }

            if (numberOfTrainedCategoricalEntailments < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfTrainedCategoricalEntailments),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            var features = dataSet[":", featureVariableIndexes];
            var response = dataSet[":", responseVariableIndex];

            if (trainSequentially)
            {
                var trainer = new CategoricalEntailmentEnsembleTrainer(
                    new List<CategoricalEntailment>(),
                    numberOfTrainedCategoricalEntailments,
                    features,
                    response,
                    allowEntailmentPartialTruthValues,
                    trainSequentially);

                for (int i = 0; i < numberOfTrainedCategoricalEntailments; i++)
                {
                    var partialClassifier = Train(
                        trainer,
                        numberOfTrainedCategoricalEntailments: 1);

                    trainer.entailments.Add(partialClassifier.Entailments[0]);
                }

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(features.Variables),
                    responseVariable: response.Variables[0]);

                for (int i = 0; i < trainer.entailments.Count; i++)
                {
                    classifier.Add(trainer.entailments[i]);
                }

                return classifier;
            }
            else
            {
                var trainer = new CategoricalEntailmentEnsembleTrainer(
                    new List<CategoricalEntailment>(),
                    numberOfTrainedCategoricalEntailments,
                    features,
                    response,
                    allowEntailmentPartialTruthValues,
                    trainSequentially: false);

                var classifier = Train(
                    trainer,
                    numberOfTrainedCategoricalEntailments);

                return classifier;
            }
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CategoricalEntailmentEnsembleClassifier" /> class 
        /// by exploiting the specified <paramref name="trainer"/>
        /// to select the given 
        /// <paramref name="numberOfTrainedCategoricalEntailments"/>.
        /// </summary>
        /// <param name="trainer">
        /// An object whose state contains the information needed to 
        /// train the classifier.
        /// </param>
        /// <param name="numberOfTrainedCategoricalEntailments">
        /// The number of categorical entailments to be trained.
        /// </param>
        /// <returns>
        /// A classifier whose ensemble contains the trained
        /// categorical entailments.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="trainer"/> can be equipped with a 
        /// nonempty initial ensemble of categorical entailments. 
        /// This method always trains a classifier by adding 
        /// to such ensemble further entailments, whose number 
        /// is specified by parameter
        /// <paramref name="numberOfTrainedCategoricalEntailments"/>.
        /// However, the method returns the partial classifier whose 
        /// ensemble contains the additional trained entailments only 
        /// (no initial entailments).
        /// </para>
        /// </remarks>
        private static CategoricalEntailmentEnsembleClassifier Train(
            CategoricalEntailmentEnsembleTrainer trainer,
            int numberOfTrainedCategoricalEntailments)
        {
            var optimizer = new SystemPerformanceOptimizer();

            int numberOfResponseCategories = 
                trainer.ResponseVariable.NumberOfCategories;

            var context =
                new CategoricalEntailmentEnsembleOptimizationContext(
                    objectiveFunction: trainer.Performance,
                    trainer.featureCategoryCounts,
                    numberOfResponseCategories,
                    numberOfTrainedCategoricalEntailments,
                    trainer.allowEntailmentPartialTruthValues,
                    probabilitySmoothingCoefficient: .9,
                    optimizationGoal: OptimizationGoal.Maximization,
                    minimumNumberOfIterations: 10,
                    maximumNumberOfIterations: 1000);

            int numberOfParameters = numberOfTrainedCategoricalEntailments * (
                trainer.featureCategoryCounts.Sum() + numberOfResponseCategories);
           
            int sampleSize = 100 * numberOfParameters;
            
            double rarity = .01;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            var partialClassifier = context.GetCategoricalEntailmentEnsembleClassifier(
                results.OptimalState,
                new List<CategoricalVariable>(trainer.FeatureVariables),
                trainer.ResponseVariable);

            return partialClassifier;
        }

        #endregion

        #region Classification

        /// <summary>
        /// Adds to this instance a
        /// <see cref="CategoricalEntailment"/> object having
        /// the specified premises on feature variables, 
        /// derived response category, and truth value.
        /// </summary>
        /// <param name="featurePremises">
        /// The list of premises the entailment defines about its
        /// <see cref="FeatureVariables"/>.
        /// </param>
        /// <param name="responseConclusion">
        /// The category of <see cref="ResponseVariable"/> derived 
        /// by the entailment when 
        /// its <see cref="CategoricalEntailment.FeaturePremises"/>
        /// are satisfied.
        /// </param>
        /// <param name="truthValue">
        /// The eventually partial truth value assigned to
        /// the entailment.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="CategoricalEntailment" path="para[@id='EmptyPremises']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="featurePremises"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="truthValue"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="truthValue"/> is greater than unity.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="featurePremises"/> has not the same 
        /// <see cref="ICollection{T}.Count"/> of the
        /// <see cref="FeatureVariables"/> of this instance.<br/>
        /// -or-<br/>
        /// Some set in <paramref name="featurePremises"/> contains
        /// a value which is not a category code in the
        /// corresponding variable of <see cref="FeatureVariables"/>.
        /// </exception>
        public void Add(
            IList<SortedSet<double>> featurePremises,
            double responseConclusion,
            double truthValue)
        {
            #region Input validation

            if (featurePremises is null)
            {
                throw new ArgumentNullException(nameof(featurePremises));
            }

            if (this.FeatureVariables.Count != featurePremises.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT"),
                    nameof(featurePremises));
            }

            for (int j = 0; j < featurePremises.Count; j++)
            {
                if (!featurePremises[j].IsSubsetOf(
                    this.FeatureVariables[j].CategoryCodes))
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CEE_PREMISE_IS_NOT_FEATURE_SUBSET"),
                            j),
                        nameof(featurePremises));
                }
            }

            if (!this.ResponseVariable.TryGet(
                responseConclusion, out Category _))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_UNRECOGNIZED_RESPONSE_CODE"),
                    nameof(responseConclusion));
            }

            if ((truthValue < 0.0) || (1.0 < truthValue))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(truthValue),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"), 
                        0.0, 
                        1.0));
            }

            #endregion

            this.entailments.Add(
                new CategoricalEntailment(
                    this.FeatureVariables,
                    this.ResponseVariable,
                    featurePremises,
                    responseConclusion,
                    truthValue));
        }

        /// <summary>
        /// Adds a number of new categorical entailments 
        /// by training them together with 
        /// the entailments currently included in this instance.
        /// Training happens on the 
        /// specified features and response categorical 
        /// variables in a given data set.
        /// </summary>
        /// <param name="dataSet">The categorical data set containing 
        /// information about
        /// the available feature and response variables.
        /// </param>
        /// <param name="featureVariableIndexes">
        /// The zero-based indexes of the columns in <paramref name="dataSet"/>
        /// containing observations about 
        /// the feature variables on which premises must be defined.
        /// </param>
        /// <param name="responseVariableIndex">
        /// The zero-based index of the column in <paramref name="dataSet"/> 
        /// containing observations about the response variable.
        /// </param>
        /// <param name="numberOfTrainedCategoricalEntailments">
        /// The number of categorical entailments to be trained.
        /// </param>
        /// <param name="allowEntailmentPartialTruthValues">
        /// If set to <c>true</c> signals that the truth value of a 
        /// categorical entailment must be equal to the homogeneity 
        /// of the probability distribution from which its conclusion has been
        /// drawn. Otherwise, the truth value is unity.
        /// </param>
        /// <param name="trainSequentially">
        /// If set to <c>true</c> signals that the ensemble is trained 
        /// sequentially, i.e. it starts as an empty collection, and new
        /// categorical entailments are added through a step-by-step 
        /// procedure to the trained ensemble,
        /// by selecting, at each step, the entailment that better 
        /// improves the system's performance of the current ensemble.
        /// Otherwise, the categorical entailments are trained simultaneously.
        /// </param>
        /// <returns>
        /// The instance of the 
        /// <see cref="CategoricalEntailmentEnsembleClassifier" /> class 
        /// based on the trained ensemble of categorical entailments.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The entailments to be trained are added to the 
        /// <see cref="Entailments"/>
        /// to optimally enlarge such collection by the specified 
        /// <paramref name="numberOfTrainedCategoricalEntailments"/>.
        /// </para>
        /// <para>
        /// it is expected that <paramref name="featureVariableIndexes"/> has the same count
        /// of the <see cref="FeatureVariables"/>
        /// of this instance, 
        /// and that the <latex>l</latex>-th position 
        /// of <paramref name="featureVariableIndexes"/> is the index of the 
        /// column that, in <paramref name="dataSet"/>, contains observations
        /// about the <latex>l</latex>-th feature variable of the classifier.
        /// Furthermore, <paramref name="responseVariableIndex"/> must be the index
        /// of the column where, in <paramref name="dataSet"/>, are stored 
        /// observations about 
        /// the <see cref="ResponseVariable"/>
        /// of this instance.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dataSet"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariableIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfTrainedCategoricalEntailments"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariableIndexes"/> contains values which 
        /// are not valid column indexes for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="dataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="responseVariableIndex"/> is
        /// not a valid column index for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="dataSet"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="featureVariableIndexes"/> has not the same 
        /// <see cref="IndexCollection.Count"/> of the
        /// <see cref="FeatureVariables"/> of this instance.
        /// </exception>
        public void AddTrained(
            CategoricalDataSet dataSet,
            IndexCollection featureVariableIndexes,
            int responseVariableIndex,
            int numberOfTrainedCategoricalEntailments,
            bool allowEntailmentPartialTruthValues,
            bool trainSequentially)
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

            if (featureVariableIndexes.Max >= dataSet.Data.NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(featureVariableIndexes),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", nameof(dataSet)));
            }

            if (this.FeatureVariables.Count != featureVariableIndexes.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT"),
                    nameof(featureVariableIndexes));
            }

            if (responseVariableIndex >= dataSet.Data.NumberOfColumns
                ||
                responseVariableIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(responseVariableIndex),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", nameof(dataSet)));
            }

            if (numberOfTrainedCategoricalEntailments < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfTrainedCategoricalEntailments),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            var features = dataSet[":", featureVariableIndexes];
            var response = dataSet[":", responseVariableIndex];

            if (trainSequentially)
            {
                var trainer = new CategoricalEntailmentEnsembleTrainer(
                    new List<CategoricalEntailment>(this.entailments),
                    numberOfTrainedCategoricalEntailments,
                    features,
                    response,
                    allowEntailmentPartialTruthValues,
                    trainSequentially);

                for (int i = 0; i < numberOfTrainedCategoricalEntailments; i++)
                {
                    var partialClassifier = Train(
                        trainer,
                        numberOfTrainedCategoricalEntailments: 1);

                    var trainedEntailment =
                        partialClassifier.Entailments[0];

                    trainer.entailments.Add(trainedEntailment);

                    this.entailments.Add(trainedEntailment);
                }
            }
            else
            {
                var trainer = new CategoricalEntailmentEnsembleTrainer(
                    new List<CategoricalEntailment>(this.entailments),
                    numberOfTrainedCategoricalEntailments,
                    features,
                    response,
                    allowEntailmentPartialTruthValues,
                    trainSequentially: false);

                var partialClassifier = Train(
                    trainer,
                    numberOfTrainedCategoricalEntailments);

                for (int i = 0; i < numberOfTrainedCategoricalEntailments; i++)
                {
                    this.entailments.Add(partialClassifier.entailments[i]);
                }
            }
        }

        /// <summary>
        /// Adds a categorical entailment while 
        /// training this instance.
        /// </summary>
        /// <param name="entailment"></param>
        internal void Add(
            CategoricalEntailment entailment)
        {
            this.entailments.Add(entailment);
        }

        /// <summary>
        /// Classifies the categorical items in the specified data set.
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose rows contain the specified items.
        /// </param>
        /// <param name="featureVariableIndexes">
        /// The zero-based indexes of the data set columns that contain the  
        /// data about the features involved in the premises of the 
        /// entailments defined by this instance.
        /// </param>
        /// <remarks>
        /// <para>
        /// Let <latex>L</latex> be the <see cref="IReadOnlyCollection{T}.Count"/> of
        /// the <see cref="CategoricalEntailment.FeaturePremises"/> 
        /// defined by the <see cref="Entailments"/> exploited by this instance. 
        /// It is expected
        /// that <paramref name="featureVariableIndexes"/> has the same count, 
        /// and that the <latex>l</latex>-th position 
        /// of <paramref name="featureVariableIndexes"/>, say <latex>k_l</latex>,
        /// is the index of the 
        /// column that, in <paramref name="dataSet"/>, contains observations
        /// about the same feature variable on which is built the <latex>l</latex>-th
        /// premise of the <see cref="Entailments"/> of this instance.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The collection of data set row indexes containing the items 
        /// that satisfy the premises of the entailments defined by this instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dataSet"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariableIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="featureVariableIndexes"/> contains values which 
        /// are not valid column indexes for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="dataSet"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="featureVariableIndexes"/> has not the same 
        /// <see cref="IndexCollection.Count"/> of the
        /// <see cref="FeatureVariables"/> of this instance.
        /// </exception>
        public CategoricalDataSet Classify(
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

            if (dataSet.NumberOfColumns <= featureVariableIndexes.Max)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(featureVariableIndexes),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", nameof(dataSet)));
            }

            if (this.FeatureVariables.Count != featureVariableIndexes.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT"),
                    nameof(featureVariableIndexes));
            }

            #endregion

            var itemData = dataSet.Data[":", featureVariableIndexes];
            int numberOfItems = itemData.NumberOfRows;
            DoubleMatrix itemResponses = DoubleMatrix.Dense(numberOfItems, 1);
            itemResponses.SetColumnName(0, this.ResponseVariable.Name);

            DoubleMatrix item, votes;
            int numberOfResponseCategories = this.responseCodeIndexPairs.Count;
            for (int r = 0; r < itemData.NumberOfRows; r++)
            {
                votes = DoubleMatrix.Dense(1, numberOfResponseCategories);
                item = itemData[r, ":"];
                for (int e = 0; e < this.entailments.Count; e++)
                {
                    if (this.entailments[e].ValidatePremises(item))
                    {
                        votes[this.responseCodeIndexPairs[this.entailments[e].ResponseConclusion]]
                            += this.entailments[e].TruthValue;
                    }
                }

                double maximumVote = Stat.Max(votes).value;

                var maximumVoteIndexes =
                    votes.Find(maximumVote);

                int numberOfMaximumVoteIndexes = maximumVoteIndexes.Count;
                if (numberOfMaximumVoteIndexes == 1)
                {
                    itemResponses[r] =
                        this.ResponseVariable.Categories[maximumVoteIndexes[0]].Code;
                }
                else
                {
                    // Pick a position corresponding to a maximum vote at random
                    int randomMaximumVotePosition = Convert.ToInt32(
                        Math.Floor(numberOfMaximumVoteIndexes *
                            this.randomNumberGenerator.DefaultUniform()));

                    itemResponses[r] =
                        this.ResponseVariable.Categories[
                            maximumVoteIndexes[randomMaximumVotePosition]].Code;
                }
            }

            return new CategoricalDataSet(
                new List<CategoricalVariable>() { this.ResponseVariable },
                itemResponses);
        }

        /// <summary>
        /// Returns the accuracy of a predicted classification with respect
        /// to an actual one.
        /// </summary>
        /// <param name="predictedDataSet">
        /// The data set containing the predicted classification.
        /// </param>
        /// <param name="predictedResponseVariableIndex">
        /// The zero-based index of the column 
        /// in <paramref name="predictedDataSet"/> containing the 
        /// predictions about the response variable applied
        /// for classifying.
        /// </param>
        /// <param name="actualDataSet">
        /// The data set containing the actual classification.
        /// </param>
        /// <param name="actualResponseVariableIndex">
        /// The zero-based index of the column 
        /// in <paramref name="actualDataSet"/> containing the 
        /// observations about the response variable applied
        /// for classifying.
        /// </param>
        /// <returns>
        /// The accuracy of a predicted classification with respect to an
        /// actual one.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="predictedDataSet"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="actualDataSet"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="predictedResponseVariableIndex"/> is
        /// not a valid column index for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="predictedDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="actualResponseVariableIndex"/> is
        /// not a valid column index for the 
        /// <see cref="CategoricalDataSet.Data"/> of 
        /// <paramref name="actualDataSet"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="actualDataSet"/> has not the same
        /// <see cref="CategoricalDataSet.NumberOfRows"/> of
        /// parameter <paramref name="predictedDataSet"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Confusion_matrix"/>
        public static double EvaluateAccuracy(
            CategoricalDataSet predictedDataSet,
            int predictedResponseVariableIndex,
            CategoricalDataSet actualDataSet,
            int actualResponseVariableIndex)
        {
            #region Input validation

            if (predictedDataSet is null)
            {
                throw new ArgumentNullException(nameof(predictedDataSet));
            }

            if (predictedResponseVariableIndex >= predictedDataSet.Data.NumberOfColumns
                ||
                predictedResponseVariableIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(predictedResponseVariableIndex),
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                            "column", 
                            nameof(predictedDataSet)));
            }

            if (actualDataSet is null)
            {
                throw new ArgumentNullException(nameof(actualDataSet));
            }

            if (actualResponseVariableIndex >= actualDataSet.Data.NumberOfColumns
                ||
                actualResponseVariableIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(actualResponseVariableIndex),
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                            "column", 
                            nameof(actualDataSet)));
            }

            if (predictedDataSet.Data.NumberOfRows != actualDataSet.Data.NumberOfRows)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        nameof(predictedDataSet)),
                    nameof(actualDataSet));
            }

            #endregion

            var actualResponses = actualDataSet.Data[":", actualResponseVariableIndex];
            var predictedResponses = predictedDataSet.Data[":", predictedResponseVariableIndex];

            var responseCodes =
                actualDataSet.Variables[actualResponseVariableIndex].CategoryCodes;

            double numberOfExactPredictions = 0.0;
            foreach (var code in responseCodes)
            {
                IndexCollection codePredictedIndexes =
                    predictedResponses.Find(code);

                if (!(codePredictedIndexes is null))
                {
                    var correspondingActualResponses =
                        actualResponses.Vec(codePredictedIndexes);
                    numberOfExactPredictions +=
                        correspondingActualResponses.Find(code)?.Count ?? 0;
                }
            }

            var accuracy = numberOfExactPredictions
                / (double)(actualDataSet.NumberOfRows);

            return accuracy;
        }

        #endregion
    }
}
