// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class CategoricalEntailmentTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            // From entailment representation as a double matrix
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 1.0 },
                        new SortedSet<double>() };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                DoubleMatrix entailmentRepresentation = DoubleMatrix.Dense(1, 5 + 4 + 3 + 1,
                    new double[5 + 4 + 3 + 1]
                    { 1, 1, 0, 0, 0,  0, 0, 0, 0,  0, 1, 0,  .9 });

                var target = new CategoricalEntailment(
                    entailmentRepresentation: entailmentRepresentation,
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                CategoricalEntailmentAssert.IsStateAsExpected(
                    target: target,
                    expectedFeatureVariables: featureVariables,
                    expectedResponseVariable: responseVariable,
                    expectedFeaturePremises: featurePremises,
                    expectedResponseConclusion: responseConclusion,
                    expectedTruthValue: truthValue);
            }

            // From entailment state
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 1.0 },
                        new SortedSet<double>() };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                CategoricalEntailmentAssert.IsStateAsExpected(
                    target: target,
                    expectedFeatureVariables: featureVariables,
                    expectedResponseVariable: responseVariable,
                    expectedFeaturePremises: featurePremises,
                    expectedResponseConclusion: responseConclusion,
                    expectedTruthValue: truthValue);
            }
        }

        [TestMethod()]
        public void TruthValueTest()
        {
            // value is negative
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 1.0 },
                        new SortedSet<double>() };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.TruthValue = -.00001;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "value");
            }

            // value is greater than 1
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 1.0 },
                        new SortedSet<double>() };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.TruthValue = 1.00001;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "value");
            }

            // valid input
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 1.0 },
                        new SortedSet<double>() };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                var expected = .7;
                target.TruthValue = expected;

                Assert.AreEqual(
                    expected: expected,
                    actual: target.TruthValue,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            CategoricalVariable f0 = new("F-0")
            {
                { 0, "0" },
                { 1, "1" },
                { 2, "2" },
                { 3, "3" },
                { 4, "4" }
            };
            f0.SetAsReadOnly();

            CategoricalVariable f1 = new("F-1")
            {
                { 0, "0" },
                { 1, "1" },
                { 2, "2" },
                { 3, "3" }
            };
            f1.SetAsReadOnly();

            var featureVariables =
                new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

            CategoricalVariable responseVariable = new("R")
            {
                { 0, "0" },
                { 1, "1" },
                { 2, "2" }
            };
            responseVariable.SetAsReadOnly();

            var featurePremises
                = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 1.0 },
                        new SortedSet<double>() };

            double responseConclusion = responseVariable.Categories[1].Code;
            double truthValue = .9;

            var target = new CategoricalEntailment(
                featureVariables: featureVariables,
                responseVariable: responseVariable,
                featurePremises: featurePremises,
                responseConclusion: responseConclusion,
                truthValue: truthValue);

            string expected =
                " IF " + Environment.NewLine +
                " " + featureVariables[0].Name + " IN { 0 1 } " + Environment.NewLine +
                " AND " + Environment.NewLine +
                " " + featureVariables[1].Name + " IN { 0 1 2 3 } " + Environment.NewLine +
                " THEN " + Environment.NewLine +
                " " + responseVariable.Name + " IS " + responseConclusion + Environment.NewLine +
                " WITH TRUTH VALUE " + truthValue + Environment.NewLine;
            Assert.AreEqual(expected: expected,
                actual: target.ToString());
        }

        [TestMethod()]
        public void ValidatePremisesTest()
        {
            // dataSet is null
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 1.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.ValidatePremises(
                            dataSet: null,
                            featureVariableIndexes: featureVariableIndexes);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "dataSet");
            }

            // featureVariableIndexes is null
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 1.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.ValidatePremises(
                            dataSet: dataSet,
                            featureVariableIndexes: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "featureVariableIndexes");
            }

            // featureVariableIndexes contains invalid column indexes for dataSet
            {
                string STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS
                    = String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", "dataSet");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 1.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.ValidatePremises(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(1, 3));
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "featureVariableIndexes");
            }

            // featureVariableIndexes has count not equal to the number of features
            {
                string STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT
                    = ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 1.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.ValidatePremises(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 2));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT,
                    expectedParameterName: "featureVariableIndexes");
            }

            // Valid input: from data set - with some empty or not proper premise
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 1.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                var actual =
                    target.ValidatePremises(dataSet, featureVariableIndexes);

                var expected = IndexCollection.FromArray(
                    new int[4] { 1, 3, 9, 11 });

                IndexCollectionAssert.AreEqual(expected, actual);
            }

            // Valid input: from data set - with some empty premise
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                var actual =
                    target.ValidatePremises(dataSet, featureVariableIndexes);

                var expected = IndexCollection.FromArray(
                    new int[8] { 0, 1, 2, 3, 8, 9, 10, 11 });

                IndexCollectionAssert.AreEqual(expected, actual);
            }

            // Valid input: from data set - with some not proper premise
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 0.0, 1.0, 2.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                var actual =
                    target.ValidatePremises(dataSet, featureVariableIndexes);

                var expected = IndexCollection.FromArray(
                    new int[8] { 0, 1, 2, 3, 8, 9, 10, 11 });

                IndexCollectionAssert.AreEqual(expected, actual);
            }

            // Valid input: from double matrix - with some empty or not proper premise
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 1.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                bool[] actual = new bool[dataSet.NumberOfRows];
                for (int i = 0; i < actual.Length; i++)
                {
                    actual[i] =
                        target.ValidatePremises(dataSet.Data[i, featureVariableIndexes]);
                }

                bool[] expected = new bool[dataSet.NumberOfRows];
                var expectedTrueIndexes = IndexCollection.FromArray(
                    new int[4] { 1, 3, 9, 11 });
                for (int i = 0; i < expectedTrueIndexes.Count; i++)
                {
                    expected[expectedTrueIndexes[i]] = true;
                }

                var allIndexes = IndexCollection.Range(0, dataSet.NumberOfRows - 1);
                var expectedFalseIndexes = 
                    IndexCollection.FromArray(
                        allIndexes.Except(expectedTrueIndexes).ToArray());

                for (int i = 0; i < expectedTrueIndexes.Count; i++)
                {
                    Assert.AreEqual(
                        expected: expected[expectedTrueIndexes[i]],
                        actual: actual[expectedTrueIndexes[i]]);
                }

                for (int i = 0; i < expectedFalseIndexes.Count; i++)
                {
                    Assert.AreEqual(
                        expected: expected[expectedFalseIndexes[i]],
                        actual: actual[expectedFalseIndexes[i]]);
                }
            }

            // Valid input: from double matrix - with some empty premise
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                bool[] actual = new bool[dataSet.NumberOfRows];
                for (int i = 0; i < actual.Length; i++)
                {
                    actual[i] =
                        target.ValidatePremises(dataSet.Data[i, featureVariableIndexes]);
                }

                bool[] expected = new bool[dataSet.NumberOfRows];
                var expectedTrueIndexes = IndexCollection.FromArray(
                    new int[8] { 0, 1, 2, 3, 8, 9, 10, 11 });
                for (int i = 0; i < expectedTrueIndexes.Count; i++)
                {
                    expected[expectedTrueIndexes[i]] = true;
                }

                var allIndexes = IndexCollection.Range(0, dataSet.NumberOfRows - 1);
                var expectedFalseIndexes =
                    IndexCollection.FromArray(
                        allIndexes.Except(expectedTrueIndexes).ToArray());

                for (int i = 0; i < expectedTrueIndexes.Count; i++)
                {
                    Assert.AreEqual(
                        expected: expected[expectedTrueIndexes[i]],
                        actual: actual[expectedTrueIndexes[i]]);
                }

                for (int i = 0; i < expectedFalseIndexes.Count; i++)
                {
                    Assert.AreEqual(
                        expected: expected[expectedFalseIndexes[i]],
                        actual: actual[expectedFalseIndexes[i]]);
                }
            }

            // Valid input: from double matrix - with some not proper premise
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                f1.SetAsReadOnly();

                var featureVariables =
                    new List<CategoricalVariable>(2) {
                        f0,
                        f1 };

                CategoricalVariable responseVariable = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                responseVariable.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, responseVariable };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                var featurePremises
                    = new List<SortedSet<double>>(2) {
                        new SortedSet<double>() { 0.0, 2.0 },
                        new SortedSet<double>() { 0.0, 1.0, 2.0, 3.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var target = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                bool[] actual = new bool[dataSet.NumberOfRows];
                for (int i = 0; i < actual.Length; i++)
                {
                    actual[i] =
                        target.ValidatePremises(dataSet.Data[i, featureVariableIndexes]);
                }

                bool[] expected = new bool[dataSet.NumberOfRows];
                var expectedTrueIndexes = IndexCollection.FromArray(
                    new int[8] { 0, 1, 2, 3, 8, 9, 10, 11 });
                for (int i = 0; i < expectedTrueIndexes.Count; i++)
                {
                    expected[expectedTrueIndexes[i]] = true;
                }

                var allIndexes = IndexCollection.Range(0, dataSet.NumberOfRows - 1);
                var expectedFalseIndexes =
                    IndexCollection.FromArray(
                        allIndexes.Except(expectedTrueIndexes).ToArray());

                for (int i = 0; i < expectedTrueIndexes.Count; i++)
                {
                    Assert.AreEqual(
                        expected: expected[expectedTrueIndexes[i]],
                        actual: actual[expectedTrueIndexes[i]]);
                }

                for (int i = 0; i < expectedFalseIndexes.Count; i++)
                {
                    Assert.AreEqual(
                        expected: expected[expectedFalseIndexes[i]],
                        actual: actual[expectedFalseIndexes[i]]);
                }
            }
        }
    }
}