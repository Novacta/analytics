// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about arrays in 
    /// unit tests using true/false propositions.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
    static class ArrayAssert<T>
    {
        /// <summary>
        /// Tests whether the specified arrays are equal and throws
        /// an exception if they are not equal.
        /// </summary>
        /// <param name="expected">The expected array.</param>
        /// <param name="actual">The actual array.</param>
        /// <exception cref="AssertFailedException">
        /// One array is null, the other is not.</br>
        /// -or-</br>
        /// The arrays have not the same length.</br>
        /// -or-</br>
        /// A value is unexpected at a given position.
        /// </exception>
        public static void AreEqual(T[] expected, T[] actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual)) || ((null != expected) && (null == actual)))
                throw new AssertFailedException("One array is null, the other is not.");

            int expectedLength = expected.Length;
            int actualLength = actual.Length;

            if (expectedLength != actualLength)
                throw new AssertFailedException("Arrays have not the same length.");

            for (int i = 0; i < expectedLength; i++)
                Assert.AreEqual(expected[i], actual[i], "Unexpected value at position {0}.", i);
        }
    }
}
