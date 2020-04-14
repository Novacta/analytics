// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{
    /// <summary>
    /// Provides methods to test
    /// a Cross-Entropy context for estimating 
    /// the probability of the rare event described
    /// as { x < -4 }, under a nominal 
    /// Gaussian Standard distribution.
    /// </summary>
    class TestableRareEventProbabilityEstimationContext01 :
        TestableRareEventProbabilityEstimationContext
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableRareEventProbabilityEstimationContext01" /> class.
        /// </summary>
        TestableRareEventProbabilityEstimationContext01() : base(
            context: new RareEventProbabilityEstimationContext01(),
            stateDimension: 1,
            eliteSampleDefinition: EliteSampleDefinition.LowerThanLevel,
            traceExecution: false,
            thresholdLevel: -4.0,
            rareEventPerformanceBoundedness: RareEventPerformanceBoundedness.Upper,
            initialParameter: DoubleMatrix.Dense(2, 1,
                new double[] { 0, 1 }),
            rareEventProbability: 3.16712418331199E-05 // delta: 1e-6
            )
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableRareEventProbabilityEstimationContext01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableRareEventProbabilityEstimationContext01"/> class.</returns>
        public static TestableRareEventProbabilityEstimationContext01 Get()
        {
            return new TestableRareEventProbabilityEstimationContext01();
        }
    }
}
