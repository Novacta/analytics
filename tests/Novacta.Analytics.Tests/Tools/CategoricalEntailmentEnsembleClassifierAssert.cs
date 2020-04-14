// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about categorical entailment
    /// ensemble classifiers in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class CategoricalEntailmentEnsembleClassifierAssert
    {
        /// <summary>
        /// Determines whether the specified target has the
        /// expected state.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="expectedFeatureVariables">
        /// The expected feature variables.</param>
        /// <param name="expectedResponseVariable">
        /// The expected response variable.</param>
        /// <param name="expectedCategoricalEntailments">
        /// The expected categorical entailments.</param>
        /// <exception cref="AssertFailedException">
        /// Target categorical entailment ensemble classifier 
        /// has an unexpected state.
        /// </exception>
        public static void IsStateAsExpected(
            CategoricalEntailmentEnsembleClassifier target,
            IReadOnlyList<CategoricalVariable> expectedFeatureVariables,
            CategoricalVariable expectedResponseVariable,
            IReadOnlyList<CategoricalEntailment> expectedCategoricalEntailments)
        {
            if (target.FeatureVariables.Count != expectedFeatureVariables.Count)
                throw new AssertFailedException(
                    "The list of target feature variables has an unexpected count.");

            for (int i = 0; i < expectedFeatureVariables.Count; i++)
            {
                CategoricalVariableAssert.AreEqual(
                    expectedFeatureVariables[i],
                    target.FeatureVariables[i]);
            }

            CategoricalVariableAssert.AreEqual(
                expectedResponseVariable,
                target.ResponseVariable);

            ListAssert<CategoricalEntailment>.ContainSameItems(
                expected: new List<CategoricalEntailment>(expectedCategoricalEntailments),
                actual: new List<CategoricalEntailment>(target.Entailments),
                areEqual: CategoricalEntailmentAssert.AreEqual);
        }

        /// <summary>
        /// Verifies that specified categorical entailment ensemble
        /// classifiers are equal.
        /// </summary>
        /// <param name="expected">The expected categorical entailment ensemble classifier.</param>
        /// <param name="actual">The actual categorical entailment ensemble classifier.</param>
        /// <exception cref="AssertFailedException">
        /// One categorical entailment is <b>null</b>, the other is not.<br/>
        /// -or- <br/>
        /// Categorical entailment ensemble classifiers have different feature variables.<br/>
        /// -or- <br/>
        /// Categorical entailment ensemble classifiers have different response variable.<br/>
        /// -or- <br/>
        /// Categorical entailment ensemble classifiers have different categorical entailments.
        /// </exception>
        public static void AreEqual(
            CategoricalEntailmentEnsembleClassifier expected,
            CategoricalEntailmentEnsembleClassifier actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One categorical entailment ensemble classifier is null, the other is not.");

            if (expected.FeatureVariables.Count != actual.FeatureVariables.Count)
                throw new AssertFailedException(
                    "The categorical entailment ensemble classifiers have different feature variables.");

            for (int i = 0; i < expected.FeatureVariables.Count; i++)
            {
                CategoricalVariableAssert.AreEqual(
                    expected.FeatureVariables[i],
                    actual.FeatureVariables[i]);
            }

            CategoricalVariableAssert.AreEqual(
                expected.ResponseVariable,
                actual.ResponseVariable);

            ListAssert<CategoricalEntailment>.ContainSameItems(
                expected: new List<CategoricalEntailment>(expected.Entailments),
                actual: new List<CategoricalEntailment>(actual.Entailments),
                areEqual: CategoricalEntailmentAssert.AreEqual);
        }
    }
}