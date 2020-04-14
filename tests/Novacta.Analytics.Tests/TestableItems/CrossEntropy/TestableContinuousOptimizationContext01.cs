// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{
    /// <summary>
    /// Provides methods to test
    /// a Cross-Entropy context for maximizing 
    /// the function f(x) = exp(-(x-2)^2) + .8 exp(-(x+2)^2).
    /// </summary>
    class TestableContinuousOptimizationContext01 :
        TestableContinuousOptimizationContext
    {
        // Define the performance function of the 
        // system under study (in this context, 
        // f(x) = exp(-(x-2)^2) + .8 exp(-(x+2)^2)).
        static double Performance(DoubleMatrix x)
        {
            double performance = 0.0;
            var x_0 = x[0];
            performance += Math.Exp(-Math.Pow(x_0 - 2.0, 2.0));
            performance += .8 * Math.Exp(-Math.Pow(x_0 + 2.0, 2.0));
            return performance;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableContinuousOptimizationContext01" /> class.
        /// </summary>
        TestableContinuousOptimizationContext01() : base(
            context: new ContinuousOptimizationContext(
                objectiveFunction: Performance,
                initialArgument: DoubleMatrix.Dense(1, 1, -6.0),
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                terminationTolerance: 1e-3,
                optimizationGoal: OptimizationGoal.Maximization,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000),
            objectiveFunction: Performance,
            stateDimension: 1,
            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Maximization,
            initialParameter: DoubleMatrix.Dense(2, 1,
                new double[] { -6.0, 100.0 }),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 1000,
            optimalState: DoubleMatrix.Dense(1, 1, 2.0),
            optimalPerformance: 1.0 + .8 * Math.Exp(-16.0),
            initialArgument: DoubleMatrix.Dense(1, 1, -6.0),
            meanSmoothingCoefficient: .8,
            standardDeviationSmoothingCoefficient: .7,
            standardDeviationSmoothingExponent: 6,
            initialStandardDeviation: 100.0,
            terminationTolerance: 1e-3)
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableContinuousOptimizationContext01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableContinuousOptimizationContext01"/> class.</returns>
        public static TestableContinuousOptimizationContext01 Get()
        {
            return new TestableContinuousOptimizationContext01();
        }
    }
}
