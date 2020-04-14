// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about categories in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class CategoryAssert
    {
        /// <summary>
        /// Determines whether the specified target has the expected state.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="expectedCode">The expected code.</param>
        /// <param name="expectedLabel">The expected label.</param>
        public static void IsStateAsExpected(
            Category target,
            double expectedCode,
            string expectedLabel)
        {
            Assert.AreEqual(expectedCode, target.Code);

            Assert.AreEqual(expectedLabel, target.Label);
        }

        /// <summary>
        /// Verifies that specified categories are equal.
        /// </summary>
        /// <param name="expected">The expected category.</param>
        /// <param name="actual">The actual category.</param>
        /// <exception cref="AssertFailedException">
        /// One category is <b>null</b>, the other is not.<br/>
        /// -or- <br/>
        /// Categories have different codes. <br/>
        /// -or- <br/>
        /// Categories have different labels. <br/>
        /// -or- <br/>
        /// Categories have different descriptions.
        /// </exception>
        public static void AreEqual(Category expected, Category actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual)) 
                || 
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One category is null, the other is not.");

            if (expected.Code != actual.Code)
                throw new AssertFailedException(
                    "Categories have different codes.");

            if (expected.Label != actual.Label)
                throw new AssertFailedException(
                    "Categories have different labels.");
        }
    }
}