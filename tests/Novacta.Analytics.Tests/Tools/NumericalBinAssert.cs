// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about numerical bins in 
    /// unit tests using true/false propositions.
    /// </summary>
    class NumericalBinAssert
    {
        /// <summary>
        /// Determines whether the specified target has the expected state.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="expectedFirstPosition">The expected first position.</param>
        /// <param name="expectedLastPosition">The expected last position.</param>
        /// <param name="expectedFirstValue">The expected first value.</param>
        /// <param name="expectedLastValue">The expected last value.</param>
        /// <param name="expectedTargetFrequencyDistribution">
        /// The expected target frequency distribution.</param>
        /// <exception cref="AssertFailedException">
        /// Actual target value is not in the expected frequency distribution.
        /// </exception>
        public static void IsStateAsExpected(
            NumericalBin target,
            int expectedFirstPosition,
            int expectedLastPosition,
            double expectedFirstValue,
            double expectedLastValue,
            Dictionary<double, int> expectedTargetFrequencyDistribution)
        {
            var actualFirstPosition = 
                (int)Reflector.GetBaseField(target, "firstPosition");
            var actualLastPosition = 
                (int)Reflector.GetBaseField(target, "lastPosition");
            var actualFirstValue = 
                (double)Reflector.GetBaseField(target, "firstValue");
            var actualLastValue = 
                (double)Reflector.GetBaseField(target, "lastValue");
            var actualTargetFrequencyDistribution =
                (Dictionary<double, int>)Reflector.GetBaseField(
                    target, "targetFrequencyDistribution");

            Assert.AreEqual(expectedFirstPosition, actualFirstPosition);
            Assert.AreEqual(expectedLastPosition, actualLastPosition);

            Assert.AreEqual(expectedFirstValue, actualFirstValue);
            Assert.AreEqual(expectedLastValue, actualLastValue);

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
