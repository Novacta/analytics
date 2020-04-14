// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about <see cref="IndexCollection"/> 
    /// instances in unit tests using true/false propositions.
    /// </summary>
    static class IndexCollectionAssert
    {
        /// <summary>
        /// Checks that the specified <see cref="IndexCollection"/> 
        /// instances are equal.
        /// </summary>
        /// <param name="expected">The expected collection.</param>
        /// <param name="actual">The actual collection.</param>
        public static void AreEqual(
            IndexCollection expected,
            IndexCollection actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One IndexCollection instance is null, the other is not.");

            int expectedLength = expected.Count;
            int actualLength = actual.Count;

            if (expectedLength != actualLength)
                throw new AssertFailedException(
                    "IndexCollection instances have not the same length.");

            // IndexCollection state

            int expectedMaxIndex = expected.Max;
            int actualMaxIndex = actual.Max; 
            Assert.AreEqual(
                expectedMaxIndex, 
                actualMaxIndex, 
                "Wrong value for MaxIndex.");

            for (int i = 0; i < actual.Count; i++) {
                Assert.AreEqual(
                    expected[i], 
                    actual[i],
                   "Wrong index at position {0}.", i);
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="IndexCollection" />
        /// instance has the expected state.
        /// </summary>
        /// <param name="expectedIndexes">The indexes expected in the collection.</param>
        /// <param name="expectedMaxIndex">The expected value of the maximum index in the collection.</param>
        /// <param name="actual">The actual index collection.</param>
        public static void IsStateAsExpected(
            int[] expectedIndexes,
            int expectedMaxIndex,
            IndexCollection actual)
        {
            if (null == expectedIndexes && null == actual)
                return;

            if (((null == expectedIndexes) && (null != actual))
                ||
                ((null != expectedIndexes) && (null == actual)))
                throw new AssertFailedException(
                    "The actual IndexCollection instance is null unexpectedly, " +
                    "or the opposite is true.");

            int expectedLength = expectedIndexes.Length;
            int actualLength = actual.Count;

            if (expectedLength != actualLength)
                throw new AssertFailedException(
                    "IndexCollection instance has not the expected length.");

            // IndexCollection state

            int actualMaxIndex = actual.Max;
            Assert.AreEqual(
                expectedMaxIndex,
                actualMaxIndex,
                "IndexCollection instance has not the expected MaxIndex.");

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(
                    expectedIndexes[i],
                    actual[i],
                   "Wrong index at position {0}.", i);
            }
        }
    }
}
