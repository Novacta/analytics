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
    class TestableSystemPerformanceOptimizationContext01 :
        TestableSystemPerformanceOptimizationContext
            <SystemPerformanceOptimizationContext>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableSystemPerformanceOptimizationContext01" /> class.
        /// </summary>
        TestableSystemPerformanceOptimizationContext01() : base(
            context: new SystemPerformanceOptimizationContext01(),
            stateDimension: 1,
            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Maximization,
            initialParameter: DoubleMatrix.Dense(2, 1,
                [-6.0, 100.0]),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 10000,
            optimalState: DoubleMatrix.Dense(1, 1, 2.0),
            optimalPerformance: 1.0 + .8 * Math.Exp(-16.0)
            )
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableSystemPerformanceOptimizationContext01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableSystemPerformanceOptimizationContext01"/> class.</returns>
        public static TestableSystemPerformanceOptimizationContext01 Get()
        {
            return new TestableSystemPerformanceOptimizationContext01();
        }
    }
}
