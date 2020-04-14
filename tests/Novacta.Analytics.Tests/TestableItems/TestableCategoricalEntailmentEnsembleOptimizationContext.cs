// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="CategoricalEntailmentEnsembleOptimizationContext"/> instance.
    /// </summary>
    class TestableCategoricalEntailmentEnsembleOptimizationContext
        : TestableSystemPerformanceOptimizationContext<
            CategoricalEntailmentEnsembleOptimizationContext>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestablePartitionOptimizationContext"/>
        /// class.</summary>
        /// <param name="context">The context to test.</param>
        /// <param name="stateDimension">The expected state dimension.</param>
        /// <param name="eliteSampleDefinition">The expected elite sample definition.</param>
        /// <param name="traceExecution">The expected value about tracing context execution.</param>
        /// <param name="optimizationGoal">The expected optimization goal.</param>
        /// <param name="initialParameter">The expected initial parameter.</param>
        /// <param name="minimumNumberOfIterations">
        /// The expected minimum number of iterations.</param>
        /// <param name="maximumNumberOfIterations">
        /// The expected maximum number of iterations.</param>
        /// <param name="optimalState">The expected optimal state.</param>
        /// <param name="optimalPerformance">The expected optimal performance.</param>
        /// <param name="objectiveFunction">The expected objective function.</param>
        /// <param name="featureVariables">
        /// The expected feature variables.</param>
        /// <param name="responseVariable">
        /// The expected response variable.</param>
        /// <param name="numberOfResponseCategories">
        /// The expected number of response categories</param>
        /// <param name="numberOfCategoricalEntailments">
        /// The expected number of categorical entailments.
        /// </param>
        /// <param name="allowEntailmentPartialTruthValues">
        /// The expected allowance for entailment partial truth values.
        /// </param>
        /// <param name="probabilitySmoothingCoefficient">
        /// The expected probability smoothing coefficient.</param>
        public TestableCategoricalEntailmentEnsembleOptimizationContext(
            CategoricalEntailmentEnsembleOptimizationContext context,
            int stateDimension,
            EliteSampleDefinition eliteSampleDefinition,
            bool traceExecution,
            OptimizationGoal optimizationGoal,
            DoubleMatrix initialParameter,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations,
            DoubleMatrix optimalState,
            double optimalPerformance,
            Func<DoubleMatrix, double> objectiveFunction,
            List<CategoricalVariable> featureVariables,
            CategoricalVariable responseVariable,
            int numberOfResponseCategories,
            int numberOfCategoricalEntailments,
            bool allowEntailmentPartialTruthValues,
            double probabilitySmoothingCoefficient
            ) : base(
                context,
                stateDimension,
                eliteSampleDefinition,
                traceExecution,
                optimizationGoal,
                initialParameter,
                minimumNumberOfIterations,
                maximumNumberOfIterations,
                optimalState,
                optimalPerformance)
        {
            this.ObjectiveFunction = objectiveFunction;
            this.FeatureVariables = featureVariables;
            this.ResponseVariable = responseVariable;

            var featureCategoryCounts = new List<int>(featureVariables.Count);
            for (int i = 0; i < featureVariables.Count; i++)
            {
                featureCategoryCounts.Add(featureVariables[i].NumberOfCategories);
            }
            this.FeatureCategoryCounts = featureCategoryCounts;
            
            this.NumberOfResponseCategories = numberOfResponseCategories;
            this.NumberOfCategoricalEntailments = numberOfCategoricalEntailments;
            this.AllowEntailmentPartialTruthValues = allowEntailmentPartialTruthValues;
            this.ProbabilitySmoothingCoefficient = probabilitySmoothingCoefficient;
        }

        /// <summary>Gets the expected feature variables.</summary>
        /// <value>The expected feature variables.</value>
        public List<CategoricalVariable> FeatureVariables { get; }

        /// <summary>Gets the expected response variable.</summary>
        /// <value>The expected response variable.</value>
        public CategoricalVariable ResponseVariable { get; }

        /// <summary>Gets the expected objective function.</summary>
        /// <value>The expected objective function.</value>
        public Func<DoubleMatrix, double> ObjectiveFunction { get; }

        /// <summary>Gets the expected feature category counts.</summary>
        /// <value>The expected feature category counts.</value>
        public List<int> FeatureCategoryCounts { get; }

        /// <summary>Gets the expected number of response categories.</summary>
        /// <value>The expected number of response categories.</value>
        public int NumberOfResponseCategories { get; }

        /// <summary>Gets the expected number of categorical entailments.</summary>
        /// <value>The expected number of categorical entailments.</value>
        public int NumberOfCategoricalEntailments { get; }

        /// <summary>Gets the expected allowance for entailment partial truth values.</summary>
        /// <value>The expected allowance for entailment partial truth values.</value>
        public bool AllowEntailmentPartialTruthValues { get; }

        /// <summary>Gets the expected probability smoothing coefficient.</summary>
        /// <value>The expected probability smoothing coefficient.</value>
        public double ProbabilitySmoothingCoefficient { get; }
    }
}
