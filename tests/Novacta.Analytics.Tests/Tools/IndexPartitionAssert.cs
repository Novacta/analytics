// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about <see cref="IndexPartition"/> 
    /// instances in unit tests using true/false propositions.
    /// </summary>
    static class IndexPartitionAssert
    {
        /// <summary>
        /// Checks that the specified <see cref="IndexPartition{T}"/> 
        /// instances are equal.
        /// </summary>
        /// <param name="expected">The expected partition.</param>
        /// <param name="actual">The actual partition.</param>
        public static void AreEqual<T>(
            IndexPartition<T> expected,
            IndexPartition<T> actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One IndexPartition instance is null, the other is not.");

            int expectedCount = expected.Count;
            int actualCount = actual.Count;

            if (expectedCount != actualCount)
                throw new AssertFailedException(
                    "IndexPartition instances have not the same number of parts.");

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(
                    expected.Identifiers[i],
                    actual.Identifiers[i],
                    "Wrong part identifier at position: {0}", i);

                var expectedPart = expected[expected.Identifiers[i]];
                var actualPart = actual[actual.Identifiers[i]];
                IndexCollectionAssert.AreEqual(expectedPart, actualPart);
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="IndexPartition{T}"/> 
        /// instances have equal identifiers, irrespective of the corresponding
        /// parts.
        /// </summary>
        /// <param name="expected">The partition containing the expected identifiers.</param>
        /// <param name="actual">The partition containing the actual identifiers.</param>
        public static void HaveEqualIdentifiers<T>(
            IndexPartition<T> expected,
            IndexPartition<T> actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One IndexPartition instance is null, the other is not.");

            int expectedCount = expected.Count;
            int actualCount = actual.Count;

            if (expectedCount != actualCount)
                throw new AssertFailedException(
                    "IndexPartition instances have not the same number of parts.");

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(
                    expected.Identifiers[i],
                    actual.Identifiers[i],
                    "Wrong part identifier at position: {0}", i);
            }
        }


        /// <summary>
        /// Checks that the specified <see cref="IndexPartition{T}"/> 
        /// instances have equal parts, irrespective of the corresponding
        /// identifiers.
        /// </summary>
        /// <param name="expected">The partition containing the expected parts.</param>
        /// <param name="actual">The partition containing the actual parts.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "By design: needed for testing purposes.")]
        public static void HaveEqualParts<T>(
            IndexPartition<T> expected,
            IndexPartition<T> actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One IndexPartition instance is null, the other is not.");

            int expectedCount = expected.Count;
            int actualCount = actual.Count;

            if (expectedCount != actualCount)
                throw new AssertFailedException(
                    "IndexPartition instances have not the same number of parts.");

            List<T> availableIds = new(actual.Identifiers);
            for (int i = 0; i < expected.Count; i++)
            {
                bool expectedPartIsMissing = true;
                var expectedPart = expected[expected.Identifiers[i]];
                for (int j = 0; j < availableIds.Count; j++)
                {
                    var actualPart = actual[availableIds[j]];
                    try
                    {
                        IndexCollectionAssert.AreEqual(
                            expectedPart, actualPart);
                        expectedPartIsMissing = false;
                        availableIds.RemoveAt(j);
                        break;
                    }
                    catch (AssertFailedException)
                    {
                    }
                }
                if (expectedPartIsMissing)
                {
                    throw new AssertFailedException(
                        msg: string.Format(
                            "Missing expected part {0}.",
                            expectedPart));
                }
            }
        }
    }
}
