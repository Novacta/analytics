// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="SystemPerformanceOptimizationContext"/> instance.
    /// </summary>
    class TestableSystemPerformanceOptimizationContext<TCrossEntropyContext>
        : TestableCrossEntropyContext<TCrossEntropyContext>
        where TCrossEntropyContext : SystemPerformanceOptimizationContext
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableSystemPerformanceOptimizationContext"/>
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
        public TestableSystemPerformanceOptimizationContext(
            TCrossEntropyContext context,
            int stateDimension,
            EliteSampleDefinition eliteSampleDefinition,
            bool traceExecution,
            OptimizationGoal optimizationGoal,
            DoubleMatrix initialParameter,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations,
            DoubleMatrix optimalState,
            double optimalPerformance
            ) : base(
                context,
                stateDimension,
                eliteSampleDefinition,
                traceExecution)
        {
            this.OptimizationGoal = optimizationGoal;
            this.InitialParameter = initialParameter;
            this.MinimumNumberOfIterations = minimumNumberOfIterations;
            this.MaximumNumberOfIterations = maximumNumberOfIterations;
            this.OptimalState = optimalState;
            this.OptimalPerformance = optimalPerformance;
        }

        /// <summary>Gets the expected optimization goal.</summary>
        /// <value>The expected optimization goal.</value>
        public OptimizationGoal OptimizationGoal { get; }

        /// <summary>Gets the expected initial parameter.</summary>
        /// <value>The expected initial parameter.</value>
        public DoubleMatrix InitialParameter { get; }

        /// <summary>Gets the expected minimum number of iterations.</summary>
        /// <value>The expected minimum number of iterations.</value>
        public int MinimumNumberOfIterations { get; }

        /// <summary>Gets the expected maximum number of iterations.</summary>
        /// <value>The expected maximum number of iterations.</value>
        public int MaximumNumberOfIterations { get; }

        /// <summary>Gets the expected optimal state.</summary>
        /// <value>The expected optimal state.</value>
        public DoubleMatrix OptimalState { get;  }

        /// <summary>Gets the expected optimal performance.</summary>
        /// <value>The expected optimal performance.</value>
        public double OptimalPerformance { get; }
    }
}
