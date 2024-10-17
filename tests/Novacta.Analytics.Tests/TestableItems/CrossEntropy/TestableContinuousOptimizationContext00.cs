// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{
    /// <summary>
    /// Provides methods to test
    /// a Cross-Entropy context for minimizing 
    /// the function f(x) = exp(-(x-2)^2) + .8 exp(-(x+2)^2)
    /// in the domain [-3, 3].
    /// </summary>
    class TestableContinuousOptimizationContext00 :
        TestableContinuousOptimizationContext
    {
        // Define the performance function of the 
        // system under study (in this context, 
        // f(x) = exp(-(x-2)^2) + .8 exp(-(x+2)^2)),
        // penalized for minimization if x is not in [-3, 3].
        static double Performance(DoubleMatrix x)
        {
            double performance = 0.0;
            var x_0 = x[0];
            performance += Math.Exp(-Math.Pow(x_0 - 2.0, 2.0));
            performance += .8 * Math.Exp(-Math.Pow(x_0 + 2.0, 2.0));
            if (x_0 < -3.0 || 3.0 < x_0)
            {
                performance += 1.0e6;
            }
            return performance;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableContinuousOptimizationContext00" /> class.
        /// </summary>
        TestableContinuousOptimizationContext00() : base(
            context: new ContinuousOptimizationContext(
                objectiveFunction: Performance,
                initialArgument: DoubleMatrix.Dense(1, 1, -6.0),
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                terminationTolerance: 1e-3,
                optimizationGoal: OptimizationGoal.Minimization,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000),
            objectiveFunction: Performance,
            stateDimension: 1,
            eliteSampleDefinition: EliteSampleDefinition.LowerThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Minimization,
            initialParameter: DoubleMatrix.Dense(2, 1,
                [-6.0, 100.0]),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 1000,
            optimalState: DoubleMatrix.Dense(1, 1, -.031878),
            optimalPerformance: .0327349,
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
        /// <see cref="TestableContinuousOptimizationContext00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableContinuousOptimizationContext00"/> class.</returns>
        public static TestableContinuousOptimizationContext00 Get()
        {
            return new TestableContinuousOptimizationContext00();
        }
    }
}
