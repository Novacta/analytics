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
    /// the function f(x) = 10 * ( (x[0])^2 + ... + (x[n-1])^2 ),
    /// where n is the count of x.
    /// </summary>
    class TestableContinuousOptimizationContext03 :
        TestableContinuousOptimizationContext
    {
        // Define the performance function of the 
        // system under study (in this context, 
        // f(x) = 10 * ( (x[0])^2 + ... + (x[n-1])^2 ).
        static double Performance(DoubleMatrix x)
        {
            double performance = 0.0;
            for (int i = 0; i < x.Count; i++)
            {
                performance += 10.0 * Math.Pow(x[i], 2.0);
            }
            return performance;
        }

        public static DoubleMatrix GetInitialParameter(int stateDimension)
        {
            var initialParameter = DoubleMatrix.Dense(2, stateDimension);
            for (int j = 0; j < stateDimension; j++)
            {
                initialParameter[0, j] = 5.0;
                initialParameter[1, j] = 100.0;
            }
            return initialParameter;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableContinuousOptimizationContext03" /> class.
        /// </summary>
        TestableContinuousOptimizationContext03(int stateDimension) : base(
            context: new ContinuousOptimizationContext(
                objectiveFunction: Performance,
                initialArgument: DoubleMatrix.Dense(1, stateDimension, 5.0),
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
            initialParameter: GetInitialParameter(stateDimension),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 1000,
            optimalState: DoubleMatrix.Dense(1, stateDimension, 0.0),
            optimalPerformance: 0.0,
            initialArgument: DoubleMatrix.Dense(1, stateDimension, 5.0),
            meanSmoothingCoefficient: .8,
            standardDeviationSmoothingCoefficient: .7,
            standardDeviationSmoothingExponent: 6,
            initialStandardDeviation: 100.0,
            terminationTolerance: 1e-3)
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableContinuousOptimizationContext03"/> class.
        /// </summary>
        /// <param name="stateDimension">The number of arguments.</param>
        /// <returns>An instance of the 
        /// <see cref="TestableContinuousOptimizationContext03"/> class.</returns>
        public static TestableContinuousOptimizationContext03 Get(
            int stateDimension)
        {
            return new TestableContinuousOptimizationContext03(stateDimension);
        }
    }
}
