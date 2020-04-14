// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about categorical data sets in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class CategoricalDataSetAssert
    {
        /// <summary>
        /// Verifies that specified categorical data sets are equal.
        /// </summary>
        /// <param name="expected">The expected categorical data set.</param>
        /// <param name="actual">The actual categorical data set.</param>
        public static void AreEqual(
            CategoricalDataSet expected, 
            CategoricalDataSet actual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual)) 
                || 
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One categorical data set is null, the other is not.");

            if (expected.Name != actual.Name)
                throw new AssertFailedException(
                    "Categorical data sets have different names.");

            if (expected.Variables.Count != actual.Variables.Count)
                throw new AssertFailedException(
                    "Categorical data sets have different numbers of variables.");

            for (int i = 0; i < expected.Variables.Count; i++) {
                CategoricalVariableAssert.AreEqual(
                    expected.Variables[i], actual.Variables[i]);
            }

            var expectedVariables = (List<CategoricalVariable>)
                Reflector.GetField(expected, "variables");

            var actualVariables = (List<CategoricalVariable>)
                Reflector.GetField(actual, "variables");

            if (expectedVariables.Count != actualVariables.Count)
                throw new AssertFailedException(
                    "Categorical data sets have different numbers of variables.");

            for (int i = 0; i < expectedVariables.Count; i++)
            {
                CategoricalVariableAssert.AreEqual(
                    expectedVariables[i], actualVariables[i]);
            }

            DoubleMatrixAssert.AreEqual(
                (DoubleMatrix)Reflector.GetField(expected, "data"),
                (DoubleMatrix)Reflector.GetField(actual, "data"), 
                1e-4);

            DoubleMatrixAssert.AreEqual(expected.Data, actual.Data, 1e-4);
        }
    }
}