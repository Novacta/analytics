// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about complex numbers in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class ComplexAssert
    {
        /// <summary>
        /// Checks that the specified <see cref="Complex"/> 
        /// instances are equal.
        /// </summary>
        /// <param name="expected">The expected complex.</param>
        /// <param name="actual">The actual complex.</param>
        /// <param name="delta">The required accuracy.</param>
        public static void AreEqual(
            Complex expected,
            Complex actual,
            double delta)
        {
            if (Complex.IsNaN(expected))
            {
                Assert.IsTrue(Complex.IsNaN(actual));
            }
            else
            {
                Assert.AreEqual(expected.Real, actual.Real, delta,
                   String.Format("Unexpected real value."));

                Assert.AreEqual(expected.Imaginary, actual.Imaginary, delta,
                   String.Format("Unexpected imaginary value."));
            }
        }
    }
}
