// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="ContinuousOptimizationContext"/> instance.
    /// </summary>
    class TestableContinuousOptimizationContext 
        : TestableSystemPerformanceOptimizationContext<ContinuousOptimizationContext>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableContinuousOptimizationContext"/>
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
        /// <param name="initialArgument">The expected initial argument.</param>
        /// <param name="meanSmoothingCoefficient">The expected mean smoothing coefficient.</param>
        /// <param name="standardDeviationSmoothingCoefficient">The expected standard deviation smoothing coefficient.</param>
        /// <param name="standardDeviationSmoothingExponent">The expected standard deviation smoothing exponent.</param>
        /// <param name="initialStandardDeviation">The expected initial standard deviation.</param>
        /// <param name="terminationTolerance">The expected termination tolerance.</param>
        public TestableContinuousOptimizationContext(
            ContinuousOptimizationContext context,
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
            DoubleMatrix initialArgument,
            double meanSmoothingCoefficient,
            double standardDeviationSmoothingCoefficient,
            int standardDeviationSmoothingExponent,
            double initialStandardDeviation,
            double terminationTolerance
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
            this.InitialArgument = initialArgument;
            this.MeanSmoothingCoefficient = meanSmoothingCoefficient;
            this.StandardDeviationSmoothingCoefficient = standardDeviationSmoothingCoefficient;
            this.StandardDeviationSmoothingExponent = standardDeviationSmoothingExponent;
            this.InitialStandardDeviation = initialStandardDeviation;
            this.TerminationTolerance = terminationTolerance;
        }

        /// <summary>Gets the expected objective function.</summary>
        /// <value>The expected objective function.</value>
        public Func<DoubleMatrix,double> ObjectiveFunction { get; }

        /// <summary>Gets the expected initial argument.</summary>
        /// <value>The expected initial argument.</value>
        public DoubleMatrix InitialArgument { get; }

        /// <summary>Gets the expected mean smoothing coefficient.</summary>
        /// <value>The expected mean smoothing coefficient.</value>
        public double MeanSmoothingCoefficient { get; }

        /// <summary>Gets the expected standard deviation smoothing coefficient.</summary>
        /// <value>The expected standard deviation smoothing coefficient.</value>
        public double StandardDeviationSmoothingCoefficient { get; }

        /// <summary>Gets the expected standard deviation smoothing exponent.</summary>
        /// <value>The expected standard deviation smoothing exponent.</value>
        public int StandardDeviationSmoothingExponent { get; }

        /// <summary>Gets the expected initial standard deviation.</summary>
        /// <value>The expected initial standard deviation.</value>
        public double InitialStandardDeviation { get; }

        /// <summary>Gets the expected termination tolerance.</summary>
        /// <value>The expected termination tolerance.</value>
        public double TerminationTolerance { get; }
    }
}
