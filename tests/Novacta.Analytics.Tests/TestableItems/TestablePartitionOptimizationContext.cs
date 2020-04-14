// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="PartitionOptimizationContext"/> instance.
    /// </summary>
    class TestablePartitionOptimizationContext
        : TestableSystemPerformanceOptimizationContext<PartitionOptimizationContext>
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
        /// <param name="partitionDimension">The expected partition dimension.</param>
        /// <param name="probabilitySmoothingCoefficient">The expected probability smoothing coefficient.</param>
        public TestablePartitionOptimizationContext(
            PartitionOptimizationContext context,
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
            int partitionDimension,
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
            this.PartitionDimension = partitionDimension;
            this.ProbabilitySmoothingCoefficient = probabilitySmoothingCoefficient;
        }

        /// <summary>Gets the expected objective function.</summary>
        /// <value>The expected objective function.</value>
        public Func<DoubleMatrix, double> ObjectiveFunction { get; }

        /// <summary>Gets the expected partition dimension.</summary>
        /// <value>The expected partition dimension.</value>
        public int PartitionDimension { get; }

        /// <summary>Gets the expected probability smoothing coefficient.</summary>
        /// <value>The expected probability smoothing coefficient.</value>
        public double ProbabilitySmoothingCoefficient { get; }
    }
}
