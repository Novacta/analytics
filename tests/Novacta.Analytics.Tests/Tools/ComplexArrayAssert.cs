﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about arrays of complex numbers in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class ComplexArrayAssert
    {
        /// <summary>
        /// Asserts that the specified objects are equal.
        /// </summary>
        /// <param name="expected">The expected array.</param>
        /// <param name="actual">The actual array.</param>
        /// <param name="delta">The required accuracy.</param>
        /// <exception cref="AssertFailedException">
        /// One array is null, the other is not.</br>
        /// -or-</br>
        /// The arrays have not the same length.</br>
        /// -or-</br>
        /// A value is unexpected at a given position.
        /// </exception>
        public static void AreEqual(
            Complex[] expected, Complex[] actual, double delta)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual)) || ((null != expected) && (null == actual)))
                throw new AssertFailedException("One array is null, the other is not.");

            int expectedLength = expected.Length;
            int actualLength = actual.Length;

            if (expectedLength != actualLength)
                throw new AssertFailedException("The arrays have not the same length.");

            for (int l = 0; l < expectedLength; l++)
            {
                ComplexAssert.AreEqual(expected[l], actual[l], delta);
            }
        }
    }
}
