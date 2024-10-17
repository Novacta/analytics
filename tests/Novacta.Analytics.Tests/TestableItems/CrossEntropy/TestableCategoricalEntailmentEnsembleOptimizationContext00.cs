// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{
    /// <summary>
    /// Provides methods to test
    /// a Cross-Entropy context for simultaneously selecting 
    /// the best 2 categorical entailments 
    /// (in the sense of classification accuracy maximization),
    /// aimed to explain the specified 
    /// items in a feature set.
    /// </summary>
    class TestableCategoricalEntailmentEnsembleOptimizationContext00 :
        TestableCategoricalEntailmentEnsembleOptimizationContext
    {
        private static readonly CategoricalDataSet features;
        private static readonly CategoricalDataSet response;
        private static readonly SortedList<double, int> responseCodeIndexPairs;
        private static readonly ConcurrentDictionary<int, RandomNumberGenerator>
            randomNumberGeneratorPool;
        private const int numberOfCategoricalEntailments = 2;
        private static readonly int entailmentRepresentationLength;
        private static readonly int numberOfResponseCategories;
        private static readonly int overallNumberOfCategories;

        static TestableCategoricalEntailmentEnsembleOptimizationContext00()
        {
            CategoricalVariable f0 = new("F-0")
            {
                { 0, "0" },
                { 1, "1" },
                { 2, "2" },
                { 3, "3" },
                { 4, "4" }
            };
            f0.SetAsReadOnly();

            List<int> featureCategoryCounts =
                [
                    f0.NumberOfCategories ];

            CategoricalVariable r = new("R")
            {
                { 0, "0" },
                { 1, "1" }
            };
            r.SetAsReadOnly();

            List<CategoricalVariable> variables =
                [f0, r];

            DoubleMatrix data = DoubleMatrix.Dense(
                new double[20, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 } });

            CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                variables,
                data);

            var featureIndexes = IndexCollection.Range(0, 0);
            int responseIndex = 1;
            features = dataSet[":", featureIndexes];
            response = dataSet[":", responseIndex];

            int overallNumberOfFeatureCategories = featureCategoryCounts.Sum();
            numberOfResponseCategories = response.Variables[0].NumberOfCategories;

            overallNumberOfCategories = overallNumberOfFeatureCategories +
                numberOfResponseCategories;
            entailmentRepresentationLength = overallNumberOfCategories + 1;

            responseCodeIndexPairs = [];
            int i = 0;
            foreach (var code in response.Variables[0].CategoryCodes)
            {
                responseCodeIndexPairs[code] = i;
                i++;
            }

            randomNumberGeneratorPool =
                new ConcurrentDictionary<int, RandomNumberGenerator>();
        }

        // Define the performance function of the 
        // system under study (in this context, 
        // the accuracy index of the ensemble of
        // categorical entailments represented by 
        // parameter x.
        public static double Performance(DoubleMatrix x)
        {
            #region Create the ensemble of categorical entailments

            var entailments = new List<CategoricalEntailment>(numberOfCategoricalEntailments);

            var featureVariables = features.Variables;
            var responseVariable = response.Variables[0];

            for (int e = 0; e < numberOfCategoricalEntailments; e++)
            {
                int entailmentRepresentationIndex = e * entailmentRepresentationLength;
                entailments.Add(new CategoricalEntailment(
                    x[0, IndexCollection.Range(
                        entailmentRepresentationIndex,
                        entailmentRepresentationIndex + overallNumberOfCategories)],
                    featureVariables,
                    responseVariable));
            }

            #endregion

            #region Exploit the ensemble to classify observed items

            ReadOnlyDoubleMatrix itemData = features.Data;
            int numberOfItems = itemData.NumberOfRows;
            DoubleMatrix itemClassifications = DoubleMatrix.Dense(numberOfItems, 1);

            DoubleMatrix item, votes;

            for (int r = 0; r < itemData.NumberOfRows; r++)
            {
                votes = DoubleMatrix.Dense(1, numberOfResponseCategories);
                item = itemData[r, ":"];
                for (int e = 0; e < entailments.Count; e++)
                {
                    if (entailments[e].ValidatePremises(item))
                    {
                        votes[responseCodeIndexPairs[entailments[e].ResponseConclusion]]
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
                        response.Variables[0].Categories[maximumVoteIndexes[0]].Code;
                }
                else
                {
                    // Pick a position corresponding to a maximum vote at random
                    int randomMaximumVotePosition = Convert.ToInt32(
                        Math.Floor(numberOfMaximumVoteIndexes *
                            randomNumberGeneratorPool.GetOrAdd(
                                Environment.CurrentManagedThreadId,
                                (threadId) =>
                                {
                                    var localRandomNumberGenerator =
                                        RandomNumberGenerator.CreateNextMT2203(7777777);
                                    return localRandomNumberGenerator;
                                }).DefaultUniform()));

                    itemClassifications[r] =
                        response.Variables[0].Categories[
                            maximumVoteIndexes[randomMaximumVotePosition]].Code;
                }
            }

            var predictedResponses = new CategoricalDataSet(
                new List<CategoricalVariable>(response.Variables),
                itemClassifications);

            #endregion

            #region Evaluate classification accuracy

            var actualResponses = response;

            var responseCodes = actualResponses.Variables[0].CategoryCodes;

            double numberOfExactPredictions = 0.0;
            foreach (var code in responseCodes)
            {
                IndexCollection codePredictedIndexes = predictedResponses.Data.Find(code);
                if (codePredictedIndexes is not null)
                {
                    DoubleMatrix correspondingActualResponses =
                        actualResponses.Data.Vec(codePredictedIndexes);
                    numberOfExactPredictions +=
                        correspondingActualResponses.Find(code)?.Count ?? 0;
                }
            }

            // Compute the overall confusion
            double totalConfusion = actualResponses.Data.Count;

            var accuracy = numberOfExactPredictions / totalConfusion;

            double performance = accuracy;

            return performance;

            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableCategoricalEntailmentEnsembleOptimizationContext00" /> class.
        /// </summary>
        TestableCategoricalEntailmentEnsembleOptimizationContext00() : base(
            context: new CategoricalEntailmentEnsembleOptimizationContext(
                objectiveFunction: Performance,
                featureCategoryCounts: [5],
                numberOfResponseCategories: 2,
                numberOfCategoricalEntailments: 2,
                allowEntailmentPartialTruthValues: false,
                probabilitySmoothingCoefficient: .9,
                optimizationGoal: OptimizationGoal.Maximization,
                minimumNumberOfIterations: 5,
                maximumNumberOfIterations: 1000),
            objectiveFunction: Performance,
            stateDimension: 2 * (5 + 2 + 1),
            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Maximization,
            initialParameter: DoubleMatrix.Dense(1, 14, [
                    .5, .5, .5, .5, .5, 1.0 / 2.0, 1.0 / 2.0,
                    .5, .5, .5, .5, .5, 1.0 / 2.0, 1.0 / 2.0 ]),
            minimumNumberOfIterations: 5,
            maximumNumberOfIterations: 1000,
            optimalState: DoubleMatrix.Dense(1, 16,
                [ 
                    1, 1, 1, 0, 0,    1, 0,    1.0,
                    0, 0, 0, 1, 1,    0, 1,    1.0 ]),
            optimalPerformance: Performance(DoubleMatrix.Dense(1, 16,
                [
                    1, 1, 1, 0, 0,    1, 0,    1.0,
                    0, 0, 0, 1, 1,    0, 1,    1.0 ])),
            featureVariables: new List<CategoricalVariable>(features.Variables),
            responseVariable: response.Variables[0],
            numberOfResponseCategories: 2,
            numberOfCategoricalEntailments: 2,
            allowEntailmentPartialTruthValues: false,
            probabilitySmoothingCoefficient: .9)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableCategoricalEntailmentEnsembleOptimizationContext00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableCategoricalEntailmentEnsembleOptimizationContext00"/> class.</returns>
        public static TestableCategoricalEntailmentEnsembleOptimizationContext00 Get()
        {
            return new TestableCategoricalEntailmentEnsembleOptimizationContext00();
        }
    }
}
