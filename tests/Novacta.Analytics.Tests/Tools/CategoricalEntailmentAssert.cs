// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about categorical entailments in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class CategoricalEntailmentAssert
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
        /// <param name="expectedFeaturePremises">
        /// The expected feature premises.</param>
        /// <param name="expectedResponseConclusion">
        /// The expected response conclusion.</param>
        /// <param name="expectedTruthValue">
        /// The expected truth value.</param>
        /// <exception cref="AssertFailedException">
        /// Target categorical entailment has an unexpected state.
        /// </exception>
        public static void IsStateAsExpected(
            CategoricalEntailment target,
            IReadOnlyList<CategoricalVariable> expectedFeatureVariables,
            CategoricalVariable expectedResponseVariable,
            IReadOnlyList<SortedSet<double>> expectedFeaturePremises,
            double expectedResponseConclusion,
            double expectedTruthValue)
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

            var targetFeaturePremises = 
                (List<SortedSet<double>>) Reflector.GetField(target, "featurePremises");

            for (int i = 0; i < expectedFeaturePremises.Count; i++)
            {
                if (!expectedFeaturePremises[i].SetEquals(
                        target.FeaturePremises[i]))
                {
                    throw new AssertFailedException(
                        "The target feature premises are not as expected.");
                }

                if (!expectedFeaturePremises[i].SetEquals(
                        targetFeaturePremises[i]))
                {
                    throw new AssertFailedException(
                        "The target feature premises are not as expected.");
                }
            }

            bool[] expectedIsProperPremise = new bool[expectedFeaturePremises.Count];
            for (int i = 0; i < expectedFeaturePremises.Count; i++)
            {
                expectedIsProperPremise[i] = !(
                    expectedFeaturePremises[i].Count == 0
                    ||
                    expectedFeaturePremises[i].Count == expectedFeatureVariables[i].NumberOfCategories);
            }

            bool[] actualIsProperPremise =
                (bool[])Reflector.GetField(target, "isNonemptyProperPremise");

            ArrayAssert<bool>.AreEqual(
                expected: expectedIsProperPremise,
                actual: actualIsProperPremise);

            Assert.AreEqual(
                expected: expectedResponseConclusion,
                actual: target.ResponseConclusion,
                delta: DoubleMatrixTest.Accuracy);

            Assert.AreEqual(
                expected: expectedTruthValue,
                actual: target.TruthValue,
                delta: DoubleMatrixTest.Accuracy);
        }

        /// <summary>
        /// Verifies that specified categorical entailments are equal.
        /// </summary>
        /// <param name="expected">The expected categorical entailment.</param>
        /// <param name="actual">The actual categorical entailment.</param>
        /// <exception cref="AssertFailedException">
        /// One categorical entailment is <b>null</b>, the other is not.<br/>
        /// -or- <br/>
        /// Categorical entailments have different feature variables.<br/>
        /// -or- <br/>
        /// Categorical entailments have different response variable.<br/>
        /// -or- <br/>
        /// Categorical entailments have different feature premises.<br/>
        /// -or- <br/>
        /// Categorical entailments have different response conclusions.<br/>
        /// -or- <br/>
        /// Categorical entailments have different truth values.
        /// </exception>
        public static void AreEqual(
            CategoricalEntailment expected,
            CategoricalEntailment actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One categorical entailment is null, the other is not.");

            if (expected.FeatureVariables.Count != actual.FeatureVariables.Count)
                throw new AssertFailedException(
                    "The categorical entailments have different feature variables.");

            for (int i = 0; i < expected.FeatureVariables.Count; i++)
            {
                CategoricalVariableAssert.AreEqual(
                    expected.FeatureVariables[i],
                    actual.FeatureVariables[i]);
            }

            CategoricalVariableAssert.AreEqual(
                expected.ResponseVariable,
                actual.ResponseVariable);

            for (int i = 0; i < expected.FeaturePremises.Count; i++)
            {
                // An empty premise is equivalent to a premise
                // matching the feature's domain
                bool isNonemptyProperExpectedPremise =
                expected.FeaturePremises[i].IsProperSubsetOf(expected.FeatureVariables[i].CategoryCodes)
                    && expected.FeaturePremises[i].Count > 0;

                bool isNonemptyProperActualPremise =
                actual.FeaturePremises[i].IsProperSubsetOf(actual.FeatureVariables[i].CategoryCodes)
                    && actual.FeaturePremises[i].Count > 0;

                if (!isNonemptyProperExpectedPremise)
                {
                    if (isNonemptyProperActualPremise)
                    {
                        throw new AssertFailedException(
                            "The categorical entailments have different feature premises.");
                    }
                }
                else
                {
                    if (!isNonemptyProperActualPremise)
                    {
                        throw new AssertFailedException(
                            "The categorical entailments have different feature premises.");
                    }
                    else
                    {
                        if (!expected.FeaturePremises[i].SetEquals(
                                actual.FeaturePremises[i]))
                        {
                            throw new AssertFailedException(
                                "The categorical entailments have different feature premises.");
                        }
                    }
                }
            }

            Assert.AreEqual(
                expected: expected.ResponseConclusion,
                actual: actual.ResponseConclusion,
                delta: DoubleMatrixTest.Accuracy);

            Assert.AreEqual(
                expected: expected.TruthValue,
                actual: actual.TruthValue,
                delta: DoubleMatrixTest.Accuracy);
        }
    }
}