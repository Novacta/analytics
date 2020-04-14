// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about categorical variables in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class CategoricalVariableAssert
    {
        /// <summary>
        /// Determines whether the specified target has the
        /// expected state.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="expectedName">The expected name.</param>
        /// <param name="expectedCategories">The expected categories.</param>
        /// <param name="expectedReadOnlyFlag">
        /// If set to <c>true</c>, the target is expected to be
        /// read-only; otherwise, <c>false</c>.</param>
        /// <exception cref="AssertFailedException">
        /// Target categorical variable has an unexpected state.
        /// </exception>
        public static void IsStateAsExpected(
            CategoricalVariable target,
            string expectedName,
            List<Category> expectedCategories,
            bool expectedReadOnlyFlag)
        {
            var actualName = (string)Reflector.GetField(target, "name");
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedName, target.Name);

            if (target.Categories.Count != expectedCategories.Count)
                throw new AssertFailedException(
                    "Target categorical variable has an unexpected number " +
                    "of categories.");

            Assert.AreEqual(expectedCategories.Count, target.NumberOfCategories);

            for (int i = 0; i < expectedCategories.Count; i++) {
                CategoryAssert.AreEqual(expectedCategories[i], target.Categories[i]);
            }

            int j = 0;
            foreach (var code in target.CategoryCodes) {
                Assert.AreEqual(target.Categories[j++].Code, code);
            }

            j = 0;
            foreach (var label in target.CategoryLabels) {
                Assert.AreEqual(target.Categories[j++].Label, label);
            }

            Assert.AreEqual(expectedReadOnlyFlag, target.IsReadOnly);
        }

        /// <summary>
        /// Verifies that specified categorical variables are equal.
        /// </summary>
        /// <param name="expected">The expected categorical variable.</param>
        /// <param name="actual">The actual categorical variable.</param>
        /// <exception cref="AssertFailedException">
        /// One categorical variable is <b>null</b>, the other is not.<br/>
        /// -or- <br/>
        /// Categorical variables have different names.<br/>
        /// -or- <br/>
        /// Categorical variables have different descriptions.<br/>
        /// -or- <br/>
        /// One categorical variable is read only, the other is not.<br/>
        /// -or- <br/>
        /// Categorical variables have different numbers of categories.
        /// </exception>
        public static void AreEqual(
            CategoricalVariable expected, 
            CategoricalVariable actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual)) 
                || 
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One categorical variable is null, the other is not.");

            if (expected.Name != actual.Name)
                throw new AssertFailedException(
                    "Categorical variables have different names.");

            if (expected.IsReadOnly != actual.IsReadOnly)
                throw new AssertFailedException(
                    "One categorical variable is read only, the other is not.");

            if (expected.Categories.Count != actual.Categories.Count)
                throw new AssertFailedException(
                    "Categorical variables have different numbers of categories.");

            for (int i = 0; i < expected.Categories.Count; i++) {
                CategoryAssert.AreEqual(expected.Categories[i], actual.Categories[i]);
            }
        }
    }
}