// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="RareEventProbabilityEstimationContext"/> instance.
    /// </summary>
    class TestableRareEventProbabilityEstimationContext
        : TestableCrossEntropyContext<RareEventProbabilityEstimationContext>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableRareEventProbabilityEstimationContext"/>
        /// class.</summary>
        /// <param name="context">The context to test.</param>
        /// <param name="stateDimension">The expected state dimension.</param>
        /// <param name="eliteSampleDefinition">The expected elite sample definition.</param>
        /// <param name="traceExecution">The expected value about tracing context execution.</param>
        /// <param name="thresholdLevel">The expected elite threshold level.</param>
        /// <param name="rareEventPerformanceBoundedness">The expected rare event performance boundedness.</param>
        /// <param name="initialParameter">The expected nominal parameter.</param>
        /// <param name="rareEventProbability">The expected rare event probability.</param>
        public TestableRareEventProbabilityEstimationContext(
            RareEventProbabilityEstimationContext context,
            int stateDimension,
            EliteSampleDefinition eliteSampleDefinition,
            bool traceExecution,
            double thresholdLevel,
            RareEventPerformanceBoundedness rareEventPerformanceBoundedness,
            DoubleMatrix initialParameter,
            double rareEventProbability
            ) : base(
                context,
                stateDimension,
                eliteSampleDefinition,
                traceExecution)
        {
            this.ThresholdLevel = thresholdLevel;
            this.RareEventPerformanceBoundedness = rareEventPerformanceBoundedness;
            this.NominalParameter = initialParameter;
            this.RareEventProbability = rareEventProbability;
        }

        /// <summary>Gets the expected threshold level.</summary>
        /// <value>The expected threshold level.</value>
        public double ThresholdLevel { get; }

        /// <summary>Gets the expected rare event performance boundedness.</summary>
        /// <value>The expected rare event performance boundedness.</value>
        public RareEventPerformanceBoundedness RareEventPerformanceBoundedness { get; }

        /// <summary>Gets the expected nominal parameter.</summary>
        /// <value>The expected nominal parameter.</value>
        public DoubleMatrix NominalParameter { get; }

        /// <summary>Gets the expected rare event probability.</summary>
        /// <value>The expected rare event probability.</value>
        public double RareEventProbability { get; }
    }
}
