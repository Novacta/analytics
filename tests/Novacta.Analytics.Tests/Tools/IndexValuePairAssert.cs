// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about <see cref="IndexValuePair"/> 
    /// instances in unit tests using true/false propositions.
    /// </summary>
    static class IndexValuePairAssert
    {
        /// <summary>
        /// Asserts that the specified 
        /// <see cref="IndexValuePair"/> instances are equal.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        /// <param name="delta">The required accuracy.</param>
        /// <param name="message">The message to include in the exception
        /// if the instances are not equal.</param>
        /// <exception cref="AssertFailedException">
        /// A value is unexpected.</br>
        /// -or-</br>
        /// An index is unexpected</br>
        /// </exception>
        public static void AreEqual(
            IndexValuePair expected,
            IndexValuePair actual,
            double delta,
            string message = null)
        {
            // IndexValuePair state

            Assert.AreEqual(
                expected.Index,
                actual.Index,
               "Wrong index in IndexValuePair. " + message);

            Assert.AreEqual(
               expected.Value,
               actual.Value,
               delta,
              "Wrong value in IndexValuePair. " + message);
        }

        /// <summary>
        /// Asserts that the specified arrays of
        /// <see cref="IndexValuePair"/> instances are equal.
        /// </summary>
        /// <param name="expected">The expected array.</param>
        /// <param name="actual">The actual array.</param>
        /// <param name="delta">The required accuracy.</param>
        /// <exception cref="AssertFailedException">
        /// One array is null, the other is not.</br>
        /// -or-</br>
        /// The arrays have not the same length.</br>
        /// -or-</br>
        /// A value is unexpected at a given position.</br>
        /// -or-</br>
        /// An index is unexpected at a given position.</br>
        /// </exception>
        public static void AreEqual(
            IndexValuePair[] expected,
            IndexValuePair[] actual,
            double delta)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One IndexValuePair[] instance is null, the other is not.");

            int expectedLength = expected.Length;
            int actualLength = actual.Length;

            if (expectedLength != actualLength)
                throw new AssertFailedException(
                    "IndexValuePair[] instances have not the same length.");

            // IndexValuePair states

            for (int i = 0; i < actual.Length; i++)
            {
                IndexValuePairAssert.AreEqual(
                    expected[i],
                    actual[i],
                    delta,
                    string.Format("at position {0}.", i));
            }
        }
    }
}
