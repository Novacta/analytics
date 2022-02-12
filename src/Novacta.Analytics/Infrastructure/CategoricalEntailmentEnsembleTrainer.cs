// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Represents the information required to train
    /// a <see cref="CategoricalEntailmentEnsembleClassifier"/> 
    /// instance by sequentially or simultaneously selecting
    /// optimal categorical entailments.
    /// </summary>
    class CategoricalEntailmentEnsembleTrainer
    {
        #region Status

        /// <summary>
        /// If set to <c>true</c> signals that the ensemble is trained 
        /// sequentially, i.e. it starts as an empty collection, and new
        /// categorical entailments are added through a step-by-step 
        /// procedure to the trained ensemble,
        /// by selecting, at each step, the entailment that better 
        /// improves the system's performance of the current ensemble.
        /// Otherwise, the categorical entailments are trained simultaneously.
        /// </summary>
        private readonly bool trainSequentially;

        /// <summary>
        /// The collection of entailments already selected.
        /// At instantiation, it can be empty, or an initial
        /// set of entailments can be specified.
        /// </summary>
        /// <remarks>
        /// <para>
        /// While training, if <see cref="trainSequentially"/> is <c>true</c>, 
        /// the collection is enlarged by adding 
        /// one new (optimized) categorical entailment at each step.
        /// </para>
        /// <para>
        /// At each <see cref="Performance(DoubleMatrix)"/> evaluation,
        /// a new ensemble is built that contains all the items 
        /// in <see cref="entailments"/> increased by the addition of the
        /// entailments represented by the sampled state of the system.
        /// </para>
        /// </remarks>
        internal List<CategoricalEntailment> entailments;

        /// <summary>
        /// A list whose length equals the number of features on which 
        /// are based the premises of the categorical entailments to be searched. 
        /// At a given position, the list stores the number of categories in
        /// the corresponding feature variable.
        /// </summary>
        internal List<int> featureCategoryCounts;

        /// <summary>
        /// If set to <c>true</c> signals that the truth value of a 
        /// categorical entailment must be equal to the homogeneity 
        /// of the probability distribution from which its conclusion has been
        /// drawn. Otherwise, the truth value is unity.
        /// </summary>
        internal bool allowEntailmentPartialTruthValues;

        /// <summary>
        /// Gets the list of feature variables about which 
        /// are defined the <see cref="CategoricalEntailment.FeaturePremises"/>
        /// of the categorical entailments trained by
        /// this instance.
        /// </summary>
        /// <value>
        /// The list of feature variables about which 
        /// are defined the <see cref="CategoricalEntailment.FeaturePremises"/>
        /// of the categorical entailments trained by
        /// this instance.
        /// </value>
        public IReadOnlyList<CategoricalVariable> FeatureVariables { get; }

        /// <summary>
        /// Gets the response variable from which 
        /// are extracted the <see cref="CategoricalEntailment.ResponseConclusion"/>
        /// of the categorical entailments trained by this instance.
        /// </summary>
        /// <value>
        /// The response variable from which 
        /// are extracted the <see cref="CategoricalEntailment.ResponseConclusion"/>
        /// of the categorical entailments trained by this instance.
        /// </value>
        public CategoricalVariable ResponseVariable { get; }

        /// <summary>
        /// Gets the array of items from the feature space
        /// exploited for training.
        /// </summary>
        private readonly DoubleMatrix[] featuresData;

        /// <summary>
        /// The <see cref="CategoricalDataSet.Data"/> in the 
        /// set containing information about
        /// the observed response exploited for training.
        /// </summary>
        private readonly ReadOnlyDoubleMatrix responseData;

        /// <summary>
        /// A collection of thread related random number generators
        /// exploited for tie resolutions in the performance function
        /// exploited for training.
        /// </summary>
        private readonly ConcurrentDictionary<int, RandomNumberGenerator>
            randomNumberGeneratorPool;

        /// <summary>
        /// A list of pairs where each response code is attached to an index.
        /// </summary>
        private readonly SortedList<double, int> responseCodeIndexPairs;

        private readonly int numberOfTrainedCategoricalEntailments;
        private readonly int entailmentRepresentationLength;
        private readonly int numberOfResponseCategories;
        private readonly int overallNumberOfCategories;

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="CategoricalEntailmentEnsembleTrainer"/> class 
        /// aimed to train the specified number of categorical 
        /// entailments by exploiting the specified data sets.
        /// </summary>
        /// <param name="initialCategoricalEntailments">
        /// The collection of initial categorical entailments.
        /// It can be empty: see <see cref="entailments"/>.
        /// </param>
        /// <param name="numberOfTrainedCategoricalEntailments">
        /// The number of categorical entailments
        /// to be trained.
        /// </param>
        /// <param name="features">
        /// The data set containing the training features.
        /// </param>
        /// <param name="response">
        /// The data set containing the training response.
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
        public CategoricalEntailmentEnsembleTrainer(
            IReadOnlyList<CategoricalEntailment> initialCategoricalEntailments,
            int numberOfTrainedCategoricalEntailments,
            CategoricalDataSet features,
            CategoricalDataSet response,
            bool allowEntailmentPartialTruthValues,
            bool trainSequentially)
        {
            this.trainSequentially = trainSequentially;

            this.entailments =
                    new List<CategoricalEntailment>(initialCategoricalEntailments);

            this.numberOfTrainedCategoricalEntailments = numberOfTrainedCategoricalEntailments;

            var responseVariable = response.Variables[0];
            var featureVariables = features.Variables;

            List<int> featureCategoryCounts = new(featureVariables.Count);
            int overallNumberOfFeatureCategories = 0;
            for (int j = 0; j < featureVariables.Count; j++)
            {
                int currentFeatureNumberOfCategories =
                    featureVariables[j].NumberOfCategories;
                featureCategoryCounts.Add(currentFeatureNumberOfCategories);
                overallNumberOfFeatureCategories += currentFeatureNumberOfCategories;
            }
            this.featureCategoryCounts = featureCategoryCounts;
            this.numberOfResponseCategories = responseVariable.NumberOfCategories;

            this.overallNumberOfCategories = overallNumberOfFeatureCategories +
                this.numberOfResponseCategories;
            this.entailmentRepresentationLength = this.overallNumberOfCategories + 1;

            this.responseCodeIndexPairs = new SortedList<double, int>();
            int c = 0;
            foreach (var code in responseVariable.CategoryCodes)
            {
                this.responseCodeIndexPairs[code] = c;
                c++;
            }

            this.randomNumberGeneratorPool =
                new ConcurrentDictionary<int, RandomNumberGenerator>();

            this.featuresData = new DoubleMatrix[features.NumberOfRows];
            for (int i = 0; i < this.featuresData.Length; i++)
            {
                this.featuresData[i] = features.Data[i, ":"];
            }

            this.allowEntailmentPartialTruthValues = allowEntailmentPartialTruthValues;
            this.responseData = response.Data;
            this.FeatureVariables = features.Variables;
            this.ResponseVariable = response.Variables[0];
        }

        #endregion

        public double Performance(DoubleMatrix state)
        {
            #region Create the ensemble of categorical entailments

            List<CategoricalEntailment> entailments = 
                new(this.entailments);

            int numberOfSelectedCategoricalEntailments =
                this.trainSequentially 
                ?
                1
                :
                this.numberOfTrainedCategoricalEntailments;

            int numberOfResponseCategories = this.numberOfResponseCategories;

            for (int e = 0; e < numberOfSelectedCategoricalEntailments; e++)
            {
                int entailmentRepresentationIndex = e * this.entailmentRepresentationLength;
                entailments.Add(new CategoricalEntailment(
                    state[0, IndexCollection.Range(
                        entailmentRepresentationIndex,
                        entailmentRepresentationIndex + this.overallNumberOfCategories)],
                    this.FeatureVariables,
                    this.ResponseVariable));
            }

            #endregion

            #region Exploit the ensemble to classify observed items

            int numberOfItems = this.featuresData.Length;
            DoubleMatrix itemClassifications = DoubleMatrix.Dense(numberOfItems, 1);

            DoubleMatrix item, votes;

            for (int r = 0; r < this.featuresData.Length; r++)
            {
                votes = DoubleMatrix.Dense(1, numberOfResponseCategories);
                item = this.featuresData[r];
                for (int e = 0; e < entailments.Count; e++)
                {
                    if (entailments[e].ValidatePremises(item))
                    {
                        votes[this.responseCodeIndexPairs[entailments[e].ResponseConclusion]]
                            += entailments[e].TruthValue;
                    }
                }

                double maximumVote = Stat.Max(votes).value;

                var maximumVoteIndexes =
                    votes.Find(maximumVote);

                int numberOfMaximumVoteIndexes = maximumVoteIndexes.Count;
                if (numberOfMaximumVoteIndexes == 1)
                {
                    itemClassifications[r] =
                        this.ResponseVariable.Categories[maximumVoteIndexes[0]].Code;
                }
                else
                {
                    // Pick a position corresponding to a maximum vote at random
                    int randomMaximumVotePosition = Convert.ToInt32(
                        Math.Floor(numberOfMaximumVoteIndexes *
                            this.randomNumberGeneratorPool.GetOrAdd(
                                Environment.CurrentManagedThreadId,
                                (threadId) =>
                                {
                                    var localRandomNumberGenerator =
                                        RandomNumberGenerator.CreateNextMT2203(7777777);
                                    return localRandomNumberGenerator;
                                }).DefaultUniform()));

                    itemClassifications[r] =
                        this.ResponseVariable.Categories[
                            maximumVoteIndexes[randomMaximumVotePosition]].Code;
                }
            }

            var predictedResponses = new CategoricalDataSet(
                new List<CategoricalVariable>(1) { this.ResponseVariable },
                itemClassifications);

            #endregion

            #region Evaluate classification accuracy

            var actualResponses = this.responseData;

            var responseCodes = this.ResponseVariable.CategoryCodes;

            double numberOfExactPredictions = 0.0;
            foreach (var code in responseCodes)
            {
                IndexCollection codePredictedIndexes = predictedResponses.Data.Find(code);
                if (codePredictedIndexes is not null)
                {
                    DoubleMatrix correspondingActualResponses =
                        actualResponses.Vec(codePredictedIndexes);
                    numberOfExactPredictions +=
                        correspondingActualResponses.Find(code)?.Count ?? 0;
                }
            }

            // Compute the overall confusion
            double totalConfusion = actualResponses.Count;

            var accuracy = numberOfExactPredictions / totalConfusion;

            return accuracy;

            #endregion
        }
    }
}
