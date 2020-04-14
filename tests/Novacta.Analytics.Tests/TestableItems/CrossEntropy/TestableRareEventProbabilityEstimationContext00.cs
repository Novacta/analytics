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
    /// in Section 2.2.1 of Rubinstein and Kroese,
    /// The Cross-Entropy Method, 2004.
    /// </summary>
    class TestableRareEventProbabilityEstimationContext00 :
        TestableRareEventProbabilityEstimationContext
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableRareEventProbabilityEstimationContext00" /> class.
        /// </summary>
        TestableRareEventProbabilityEstimationContext00() : base(
            context: new RareEventProbabilityEstimationContext00(),
            stateDimension: 5,
            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
            traceExecution: false,
            thresholdLevel: 2.0,
            rareEventPerformanceBoundedness: RareEventPerformanceBoundedness.Lower,
            initialParameter: DoubleMatrix.Dense(1, 5,
                new double[] { 0.25, 0.4, 0.1, 0.3, 0.2 }),
            rareEventProbability: 1.33930743730864E-05
            )
        {            
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableRareEventProbabilityEstimationContext00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableRareEventProbabilityEstimationContext00"/> class.</returns>
        public static TestableRareEventProbabilityEstimationContext00 Get()
        {
            return new TestableRareEventProbabilityEstimationContext00();
        }
    }
}
