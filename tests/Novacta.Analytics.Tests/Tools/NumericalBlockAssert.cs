// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about numerical blocks in 
    /// unit tests using true/false propositions.
    /// </summary>
    class NumericalBlockAssert
    {
        /// <summary>
        /// Determines whether the specified target has the expected state.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="expectedFirstPosition">
        /// The expected first position.</param>
        /// <param name="expectedLastPosition">
        /// The expected last position.</param>
        /// <param name="expectedFirstValue">
        /// The expected first value.</param>
        /// <param name="expectedLastValue">
        /// The expected last value.</param>
        /// <param name="expectedTargetFrequencyDistribution">
        /// The expected target frequency distribution.</param>
        /// <exception cref="AssertFailedException">
        /// Actual target value is not in the expected frequency distribution.
        /// </exception>
        public static void IsStateAsExpected(
            NumericalBlock target,
            int expectedFirstPosition,
            int expectedLastPosition,
            double expectedFirstValue,
            double expectedLastValue,
            Dictionary<double, int> expectedTargetFrequencyDistribution)
        {
            int actualFirstPosition;
            int actualLastPosition;
            double actualFirstValue;
            double actualLastValue;
            Dictionary<double, int> actualTargetFrequencyDistribution;
            if (typeof(NumericalBin) == target.GetType()) {
                actualFirstPosition = 
                    (int)Reflector.GetBaseField(target, "firstPosition");
                actualLastPosition = 
                    (int)Reflector.GetBaseField(target, "lastPosition");
                actualFirstValue = 
                    (double)Reflector.GetBaseField(target, "firstValue");
                actualLastValue = 
                    (double)Reflector.GetBaseField(target, "lastValue");
                actualTargetFrequencyDistribution =
                    (Dictionary<double, int>)Reflector.GetBaseField(
                        target, "targetFrequencyDistribution");
            }
            else { // Here if type is NumericalBlock
                actualFirstPosition = 
                    (int)Reflector.GetField(target, "firstPosition");
                actualLastPosition = 
                    (int)Reflector.GetField(target, "lastPosition");
                actualFirstValue = 
                    (double)Reflector.GetField(target, "firstValue");
                actualLastValue = 
                    (double)Reflector.GetField(target, "lastValue");
                actualTargetFrequencyDistribution =
                    (Dictionary<double, int>)Reflector.GetField(
                        target, "targetFrequencyDistribution");
            }

            Assert.AreEqual(expectedFirstPosition, actualFirstPosition);
            Assert.AreEqual(expectedLastPosition, actualLastPosition);

            Assert.AreEqual(expectedFirstValue, actualFirstValue);
            Assert.AreEqual(expectedLastValue, actualLastValue);

            Assert.AreEqual(expectedFirstPosition, target.FirstPosition);
            Assert.AreEqual(expectedLastPosition, target.LastPosition);

            Assert.AreEqual(expectedFirstValue, target.FirstValue);
            Assert.AreEqual(expectedLastValue, target.LastValue);

            foreach (var pair in actualTargetFrequencyDistribution)
            {
                if (!expectedTargetFrequencyDistribution.TryGetValue(
                    pair.Key, out int expectedFrequency))
                {
                    throw new AssertFailedException(
                        "Actual target value is not in the expected " +
                        "frequency distribution.");
                }
                else
                {
                    Assert.AreEqual(expectedFrequency, pair.Value);
                }
            }
        }
    }
}
