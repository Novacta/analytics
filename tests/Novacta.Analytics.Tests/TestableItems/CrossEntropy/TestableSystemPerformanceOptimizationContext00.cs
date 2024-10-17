// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{
    /// <summary>
    /// Provides methods to test
    /// a Cross-Entropy context for minimizing 
    /// the bi-dimensional Rosenbrock function.
    /// </summary>
    class TestableSystemPerformanceOptimizationContext00 :
        TestableSystemPerformanceOptimizationContext
            <SystemPerformanceOptimizationContext>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableSystemPerformanceOptimizationContext00" /> class.
        /// </summary>
        TestableSystemPerformanceOptimizationContext00() : base(
            context: new SystemPerformanceOptimizationContext00(),
            stateDimension: 2,
            eliteSampleDefinition: EliteSampleDefinition.LowerThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Minimization,
            initialParameter: DoubleMatrix.Dense(2, 2,
                [-1.0, 10000.0, -1.0, 10000.0]),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 10000,
            optimalState: DoubleMatrix.Dense(1, 2, 1.0),
            optimalPerformance: 0.0
            )
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableSystemPerformanceOptimizationContext00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableSystemPerformanceOptimizationContext00"/> class.</returns>
        public static TestableSystemPerformanceOptimizationContext00 Get()
        {
            return new TestableSystemPerformanceOptimizationContext00();
        }
    }
}
