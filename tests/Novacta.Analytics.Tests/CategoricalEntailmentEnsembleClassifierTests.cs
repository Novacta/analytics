// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class CategoricalEntailmentEnsembleClassifierTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // featureVariables is null
            {
                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleClassifier(
                            featureVariables: null,
                            responseVariable: responseVariable);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "featureVariables");
            }

            // featureVariables is empty
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_EMPTY =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY");

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleClassifier(
                            featureVariables: new List<CategoricalVariable>(),
                            responseVariable: responseVariable);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_EMPTY,
                    expectedParameterName: "featureVariables");
            }

            // at least a variable in featureVariables is empty
            {
                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { 0, "0" }
                };

                var f1 = new CategoricalVariable(name: "F-1");
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                string STR_EXCEPT_CEE_VARIABLE_IS_EMPTY =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CEE_VARIABLE_IS_EMPTY"),
                        1 + "-th feature");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleClassifier(
                            featureVariables: featureVariables,
                            responseVariable: responseVariable);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_VARIABLE_IS_EMPTY,
                    expectedParameterName: "featureVariables");
            }

            // responseVariable is null
            {
                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { 0, "0" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 0, "0" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleClassifier(
                            featureVariables: featureVariables,
                            responseVariable: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "responseVariable");
            }

            // responseVariable is empty
            {
                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { 0, "0" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 0, "0" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r");

                string STR_EXCEPT_CEE_VARIABLE_IS_EMPTY =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CEE_VARIABLE_IS_EMPTY"),
                        "response");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleClassifier(
                            featureVariables: featureVariables,
                            responseVariable: responseVariable);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_VARIABLE_IS_EMPTY,
                    expectedParameterName: "responseVariable");
            }

            // valid input
            {
                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                Assert.AreEqual(
                    expected: featureVariables.Count,
                    actual: classifier.FeatureVariables.Count);

                for (int i = 0; i < featureVariables.Count; i++)
                {
                    CategoricalVariableAssert.AreEqual(
                        expected: featureVariables[i],
                        actual: classifier.FeatureVariables[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: responseVariable,
                    actual: classifier.ResponseVariable);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(),
                    actual: new List<CategoricalEntailment>(classifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);
            }
        }

        [TestMethod]
        public void AddTest()
        {
            // featurePremises is null
            {
                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.Add(
                            featurePremises: null,
                            responseConclusion: 0,
                            truthValue: .5);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "featurePremises");
            }

            // featurePremises has not the same count of property FeatureVariables
            {
                string STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT");

                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.Add(
                            featurePremises: new List<SortedSet<double>>() {
                                new SortedSet<double>() { -1.0 }
                            },
                            responseConclusion: 0,
                            truthValue: .5);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT,
                    expectedParameterName: "featurePremises");
            }

            // a premise is not a subset of the corresponding feature variable
            {
                string STR_EXCEPT_CEE_PREMISE_IS_NOT_FEATURE_SUBSET =
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CEE_PREMISE_IS_NOT_FEATURE_SUBSET"),
                            1);

                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.Add(
                            featurePremises: new List<SortedSet<double>>() {
                                new SortedSet<double>() { -1.0 },
                                new SortedSet<double>() { 2.0 }
                            },
                            responseConclusion: 0,
                            truthValue: .5);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_PREMISE_IS_NOT_FEATURE_SUBSET,
                    expectedParameterName: "featurePremises");
            }

            // responseConclusion is not a category code for property ResponseVariable
            {
                string STR_EXCEPT_CEE_UNRECOGNIZED_RESPONSE_CODE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_UNRECOGNIZED_RESPONSE_CODE");

                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.Add(
                            featurePremises: new List<SortedSet<double>>() {
                                new SortedSet<double>() { -1.0 },
                                new SortedSet<double>() {  }
                            },
                            responseConclusion: 2,
                            truthValue: .5);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_UNRECOGNIZED_RESPONSE_CODE,
                    expectedParameterName: "responseConclusion");
            }

            // truthValue is negative
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"), 0.0, 1.0);

                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.Add(
                            featurePremises: new List<SortedSet<double>>() {
                                new SortedSet<double>() { -1.0 },
                                new SortedSet<double>() {  }
                            },
                            responseConclusion: 0,
                            truthValue: -.5);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "truthValue");
            }

            // truthValue is greater than 1.0
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"), 0.0, 1.0);

                var f0 = new CategoricalVariable(name: "F-0")
                {
                    { -1, "-1" }
                };

                var f1 = new CategoricalVariable(name: "F-1")
                {
                    { 1, "1" }
                };
                var featureVariables = new List<CategoricalVariable>()
                {
                    f0, f1
                };

                var responseVariable = new CategoricalVariable(name: "r")
                {
                    { 0, "0" }
                };

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.Add(
                            featurePremises: new List<SortedSet<double>>() {
                                new SortedSet<double>() { -1.0 },
                                new SortedSet<double>() {  }
                            },
                            responseConclusion: 0,
                            truthValue: 1.1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "truthValue");
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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>() {
                        f0, f1
                    },
                    responseVariable: r);

                List<CategoricalEntailment> expected =
                    new();

                List<SortedSet<double>> featurePremises;
                double responseConclusion;
                double truthValue;

                featurePremises = new List<SortedSet<double>>() {
                    new SortedSet<double>() { 0.0, 2.0 },
                    new SortedSet<double>() { 0.0 }
                };
                responseConclusion = 0;
                truthValue = .4;

                classifier.Add(
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                expected.Add(new CategoricalEntailment(
                    featureVariables: classifier.FeatureVariables,
                    responseVariable: classifier.ResponseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue));

                featurePremises = new List<SortedSet<double>>() {
                    new SortedSet<double>() { 1.0, 3.0 },
                    new SortedSet<double>() { 2.0 }
                };
                responseConclusion = 1;
                truthValue = .75;

                classifier.Add(
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                expected.Add(new CategoricalEntailment(
                    featureVariables: classifier.FeatureVariables,
                    responseVariable: classifier.ResponseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue));

                featurePremises = new List<SortedSet<double>>() {
                    new SortedSet<double>() { 4.0 },
                    new SortedSet<double>() { 1.0 }
                };
                responseConclusion = 2;
                truthValue = .9;

                classifier.Add(
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                expected.Add(new CategoricalEntailment(
                    featureVariables: classifier.FeatureVariables,
                    responseVariable: classifier.ResponseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue));

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: expected,
                    actual: new List<CategoricalEntailment>(classifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);
            }
        }

        [TestMethod]
        public void AddTrainedTest()
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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: null,
                            featureVariableIndexes: featureVariableIndexes,
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 3,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: null,
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 3,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(1, 3),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 3,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 2),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 3,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT,
                    expectedParameterName: "featureVariableIndexes");
            }

            // responseVariableIndex is negative
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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: -1,
                            numberOfTrainedCategoricalEntailments: 3,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "responseVariableIndex");
            }

            // responseVariableIndex is greater than the number of columns in dataSet
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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 3,
                            numberOfTrainedCategoricalEntailments: 3,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "responseVariableIndex");
            }

            // numberOfTrainedCategoricalEntailments is negative
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE
                    = ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: -1,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfTrainedCategoricalEntailments");
            }

            // numberOfTrainedCategoricalEntailments is zero
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE
                    = ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

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

                var classifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        classifier.AddTrained(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 0,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfTrainedCategoricalEntailments");
            }

            // Valid input - simultaneous training - empty initial ensemble
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureIndexes = IndexCollection.Range(0, 0);
                int responseIndex = 1;

                var actualClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                actualClassifier.AddTrained(
                    dataSet: dataSet,
                    featureVariableIndexes: featureIndexes,
                    responseVariableIndex: responseIndex,
                    numberOfTrainedCategoricalEntailments: 2,
                    allowEntailmentPartialTruthValues: false,
                    trainSequentially: false);

                var expectedClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 3.0, 4.0, 5.0 } },
                    responseConclusion: 1.0,
                    truthValue: 1.0);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expectedClassifier.Entailments),
                    actual: new List<CategoricalEntailment>(actualClassifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);

                Assert.AreEqual(
                    expected: 1.0,
                    actual: CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                        predictedDataSet: actualClassifier.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureIndexes),
                        predictedResponseVariableIndex: 0,
                        actualDataSet: dataSet,
                        actualResponseVariableIndex: 1),
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - simultaneous training - nonempty initial ensemble
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureIndexes = IndexCollection.Range(0, 0);
                int responseIndex = 1;

                var actualClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                actualClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                actualClassifier.AddTrained(
                    dataSet: dataSet,
                    featureVariableIndexes: featureIndexes,
                    responseVariableIndex: responseIndex,
                    numberOfTrainedCategoricalEntailments: 1,
                    allowEntailmentPartialTruthValues: false,
                    trainSequentially: false);

                var expectedClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 3.0, 4.0, 5.0 } },
                    responseConclusion: 1.0,
                    truthValue: 1.0);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expectedClassifier.Entailments),
                    actual: new List<CategoricalEntailment>(actualClassifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);

                Assert.AreEqual(
                    expected: 1.0,
                    actual: CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                        predictedDataSet: actualClassifier.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureIndexes),
                        predictedResponseVariableIndex: 0,
                        actualDataSet: dataSet,
                        actualResponseVariableIndex: 1),
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - sequential training - empty initial ensemble
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureIndexes = IndexCollection.Range(0, 0);
                int responseIndex = 1;

                var actualClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                actualClassifier.AddTrained(
                    dataSet: dataSet,
                    featureVariableIndexes: featureIndexes,
                    responseVariableIndex: responseIndex,
                    numberOfTrainedCategoricalEntailments: 2,
                    allowEntailmentPartialTruthValues: false,
                    trainSequentially: true);

                var expectedClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 3.0, 4.0, 5.0 } },
                    responseConclusion: 1.0,
                    truthValue: 1.0);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expectedClassifier.Entailments),
                    actual: new List<CategoricalEntailment>(actualClassifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);

                Assert.AreEqual(
                    expected: 1.0,
                    actual: CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                        predictedDataSet: actualClassifier.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureIndexes),
                        predictedResponseVariableIndex: 0,
                        actualDataSet: dataSet,
                        actualResponseVariableIndex: 1),
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - sequential training - nonempty initial ensemble
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureIndexes = IndexCollection.Range(0, 0);
                int responseIndex = 1;

                var actualClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                actualClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                actualClassifier.AddTrained(
                    dataSet: dataSet,
                    featureVariableIndexes: featureIndexes,
                    responseVariableIndex: responseIndex,
                    numberOfTrainedCategoricalEntailments: 1,
                    allowEntailmentPartialTruthValues: false,
                    trainSequentially: true);

                var expectedClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 3.0, 4.0, 5.0 } },
                    responseConclusion: 1.0,
                    truthValue: 1.0);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expectedClassifier.Entailments),
                    actual: new List<CategoricalEntailment>(actualClassifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);

                Assert.AreEqual(
                    expected: 1.0,
                    actual: CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                        predictedDataSet: actualClassifier.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureIndexes),
                        predictedResponseVariableIndex: 0,
                        actualDataSet: dataSet,
                        actualResponseVariableIndex: 1),
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ClassifyTest()
        {
            // dataSet is null
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                var target = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                var featureVariableIndexes = IndexCollection.Range(0, 0);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.Classify(
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
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                var target = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 0);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.Classify(
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
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                var target = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(2, 2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureVariableIndexes);
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
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                var target = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureVariableIndexes = IndexCollection.Range(0, 1);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureVariableIndexes);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT,
                    expectedParameterName: "featureVariableIndexes");
            }

            // valid input
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var target = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>() { f0 },
                    responseVariable: r);

                target.Add(
                    featurePremises: new List<SortedSet<double>>()
                        { new SortedSet<double>() { 0.0, 1.0, 2.0 } },
                    responseConclusion: 1,
                    truthValue: 1.0);

                target.Add(
                    featurePremises: new List<SortedSet<double>>()
                        { new SortedSet<double>() { 3.0, 4.0, 5.0 } },
                    responseConclusion: 0,
                    truthValue: 1.0);

                var actual = target.Classify(
                    dataSet: dataSet,
                    featureVariableIndexes: IndexCollection.Range(0, 0));

                variables = new List<CategoricalVariable>() { r };

                data = DoubleMatrix.Dense(
                    new double[24, 1] {
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 },

                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 },

                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 },

                        { 0 },
                        { 0 },
                        { 0 },
                        { 0 },

                        { 0 },
                        { 0 },
                        { 0 },
                        { 0 },

                        { 0 },
                        { 0 },
                        { 0 },
                        { 0 } });

                var expected = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                CategoricalDataSetAssert.AreEqual(
                    expected: expected,
                    actual: actual);
            }

            // valid input - random ties resolution
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var target = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>() { f0 },
                    responseVariable: r);

                int numberOfEvaluations = 10000;
                double delta = .01;

                // Generate responses

                var responses = new int[numberOfEvaluations * data.NumberOfRows];

                int index = 0;
                for (int i = 0; i < numberOfEvaluations; i++)
                {
                    var classifications = target.Classify(
                        dataSet: dataSet,
                        featureVariableIndexes: IndexCollection.Range(0, 0));

                    for (int j = 0; j < classifications.NumberOfRows; j++, index++)
                    {
                        responses[index] = Convert.ToInt32(classifications.Data[j, 0]);
                    }
                }

                // Compute the actual inclusion probabilities

                DoubleMatrix actualInclusionProbabilities =
                    DoubleMatrix.Dense(r.NumberOfCategories, 1);

                var sampleIndexes = IndexCollection.Default(numberOfEvaluations - 1);

                for (int j = 0; j < r.NumberOfCategories; j++)
                {
                    var samplesContainingCurrentUnit =
                    IndexPartition.Create(
                        sampleIndexes,
                        (i) => { return responses[i] == j; });

                    actualInclusionProbabilities[j] =
                        (double)samplesContainingCurrentUnit[true].Count
                        /
                        (double)numberOfEvaluations;
                }

                // Check the number of distinct generated samples

                var distinctSamples =
                    IndexPartition.Create(
                        responses);

                int numberOfDistinctSamples =
                    distinctSamples.Count;

                Assert.AreEqual(
                    expected: r.NumberOfCategories,
                    actual: numberOfDistinctSamples);

                // Check that the Chebyshev Inequality holds true
                // for each inclusion probability

                var expectedInclusionProbabilities =
                    DoubleMatrix.Dense(r.NumberOfCategories, 1,
                        1.0 / r.NumberOfCategories);

                for (int j = 0; j < r.NumberOfCategories; j++)
                {
                    ProbabilityDistributionTest.CheckChebyshevInequality(
                         new BernoulliDistribution(expectedInclusionProbabilities[j]),
                         actualInclusionProbabilities[j],
                         numberOfEvaluations,
                         delta);
                }

                // Check how good the actual inclusion probabilities fit 
                // the expected ones

                // The following assumes a number of response
                // categories equal to 4.
                //
                // The quantile of order .9 for
                // the chi-squared distribution having 4-1
                // degrees of freedom is 6.251389
                // (as from R function qchisq(.9, 3))
                var goodnessOfFitCriticalValue = 6.251389;

                ProbabilityDistributionTest.CheckGoodnessOfFit(
                    expectedInclusionProbabilities,
                    actualInclusionProbabilities,
                    goodnessOfFitCriticalValue);
            }
        }

        [TestMethod]
        public void EvaluateAccuracyTest()
        {
            // predictedDataSet is null
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: null,
                            predictedResponseVariableIndex: 0,
                            actualDataSet: dataSet,
                            actualResponseVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "predictedDataSet");
            }

            // actualDataSet is null
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: dataSet,
                            predictedResponseVariableIndex: 0,
                            actualDataSet: null,
                            actualResponseVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "actualDataSet");
            }

            // predictedResponseVariableIndex is negative
            {
                string STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS
                    = String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", "predictedDataSet");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: dataSet,
                            predictedResponseVariableIndex: -1,
                            actualDataSet: dataSet,
                            actualResponseVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "predictedResponseVariableIndex");
            }

            // predictedResponseVariableIndex is equal to the number of columns in predictedDataSet
            {
                string STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS
                    = String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", "predictedDataSet");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: dataSet,
                            predictedResponseVariableIndex: 2,
                            actualDataSet: dataSet,
                            actualResponseVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "predictedResponseVariableIndex");
            }

            // actualResponseVariableIndex is negative
            {
                string STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS
                    = String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", "actualDataSet");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: dataSet,
                            predictedResponseVariableIndex: 0,
                            actualDataSet: dataSet,
                            actualResponseVariableIndex: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "actualResponseVariableIndex");
            }

            // actualResponseVariableIndex is equal to the number of columns in actualDataSet
            {
                string STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS
                    = String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS"),
                        "column", "actualDataSet");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: dataSet,
                            predictedResponseVariableIndex: 0,
                            actualDataSet: dataSet,
                            actualResponseVariableIndex: 2);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "actualResponseVariableIndex");
            }

            // actualDataSet has not the same number of rows in predictedDataSet
            {
                string STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS
                    = String.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        "predictedDataSet");

                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                            predictedDataSet: dataSet,
                            predictedResponseVariableIndex: 0,
                            actualDataSet: dataSet[IndexCollection.Range(0, 10), ":"],
                            actualResponseVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: "actualDataSet");
            }

            // valid input - all actual codes partially predicted
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var predictedDataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var unaccurateResponseIndexes =
                    IndexCollection.Range(8, 15);

                foreach (var index in unaccurateResponseIndexes)
                {
                    data[index, 1] = data[index, 1] == 0.0 ? 1.0 : 0.0;
                }

                var actualDataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var actualAccuracy =
                     CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                         predictedDataSet: predictedDataSet,
                         predictedResponseVariableIndex: 1,
                         actualDataSet: actualDataSet,
                         actualResponseVariableIndex: 1);

                double numberOfPredictions = data.NumberOfRows;
                double numberOfExactPredictions =
                    numberOfPredictions - unaccurateResponseIndexes.Count;

                var expectedAccuracy =
                    numberOfExactPredictions / numberOfPredictions;

                Assert.AreEqual(
                    expected: expectedAccuracy,
                    actual: actualAccuracy,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // valid input - some actual codes totally unpredicted
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                var predictedDataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var unaccurateResponseIndexes =
                    IndexCollection.Range(0, 11);

                foreach (var index in unaccurateResponseIndexes)
                {
                    data[index, 1] = data[index, 1] == 0.0 ? 1.0 : 0.0;
                }

                var actualDataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var actualAccuracy =
                     CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                         predictedDataSet: predictedDataSet,
                         predictedResponseVariableIndex: 1,
                         actualDataSet: actualDataSet,
                         actualResponseVariableIndex: 1);

                double numberOfPredictions = data.NumberOfRows;
                double numberOfExactPredictions =
                    numberOfPredictions - unaccurateResponseIndexes.Count;

                var expectedAccuracy =
                    numberOfExactPredictions / numberOfPredictions;

                Assert.AreEqual(
                    expected: expectedAccuracy,
                    actual: actualAccuracy,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void TrainTest()
        {
            // dataSet is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: null,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 2,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

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

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: dataSet,
                            featureVariableIndexes: null,
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 2,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

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

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 3),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 2,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "featureVariableIndexes");
            }

            // responseVariableIndex is negative
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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

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

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: -1,
                            numberOfTrainedCategoricalEntailments: 2,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "responseVariableIndex");
            }

            // responseVariableIndex is the number of columns in dataSet
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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

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

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 3,
                            numberOfTrainedCategoricalEntailments: 2,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_INDEX_EXCEEDS_OTHER_PAR_DIMS,
                    expectedParameterName: "responseVariableIndex");
            }

            // numberOfTrainedCategoricalEntailments is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

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

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: 0,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfTrainedCategoricalEntailments");
            }

            // numberOfTrainedCategoricalEntailments is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

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

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CategoricalEntailmentEnsembleClassifier.Train(
                            dataSet: dataSet,
                            featureVariableIndexes: IndexCollection.Range(0, 1),
                            responseVariableIndex: 2,
                            numberOfTrainedCategoricalEntailments: -1,
                            allowEntailmentPartialTruthValues: false,
                            trainSequentially: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfTrainedCategoricalEntailments");
            }

            // Valid input - Simultaneous training
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                new double[24, 2] {
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },
                    { 0, 0 },

                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 0 },

                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },
                    { 2, 0 },

                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },
                    { 3, 1 },

                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },
                    { 4, 1 },

                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 },
                    { 5, 1 } });

                CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureIndexes = IndexCollection.Range(0, 0);
                int responseIndex = 1;

                var actualClassifier =
                   CategoricalEntailmentEnsembleClassifier.Train(
                       dataSet: dataSet,
                       featureVariableIndexes: featureIndexes,
                       responseVariableIndex: responseIndex,
                       numberOfTrainedCategoricalEntailments: 2,
                       allowEntailmentPartialTruthValues: false,
                       trainSequentially: false);

                var expectedClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0 },
                    responseVariable: r);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 3.0, 4.0, 5.0 } },
                    responseConclusion: 1.0,
                    truthValue: 1.0);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expectedClassifier.Entailments),
                    actual: new List<CategoricalEntailment>(actualClassifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);

                Assert.AreEqual(
                    expected: 1.0,
                    actual: CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                        predictedDataSet: actualClassifier.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureIndexes),
                        predictedResponseVariableIndex: 0,
                        actualDataSet: dataSet,
                        actualResponseVariableIndex: 1),
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - Sequential training
            {
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" }
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

                CategoricalVariable r = new("R")
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" }
                };
                r.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new() { f0, f1, r };

                DoubleMatrix data = DoubleMatrix.Dense(
                    new double[24, 3] {
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
                    { 4, 3, 1 },

                    { 5, 0, 1 },
                    { 5, 1, 1 },
                    { 5, 2, 1 },
                    { 5, 3, 1 } });

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var featureIndexes = IndexCollection.Range(0, 1);
                int responseIndex = 2;

                var actualClassifier =
                    CategoricalEntailmentEnsembleClassifier.Train(
                        dataSet: dataSet,
                        featureVariableIndexes: featureIndexes,
                        responseVariableIndex: responseIndex,
                        numberOfTrainedCategoricalEntailments: 3,
                        allowEntailmentPartialTruthValues: false,
                        trainSequentially: true);

                var expectedClassifier = new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: new List<CategoricalVariable>(1) { f0, f1 },
                    responseVariable: r);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(2) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 },
                        new SortedSet<double>(){ 0.0, 1.0 } },
                    responseConclusion: 0.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(2) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 },
                        new SortedSet<double>(){ 2.0, 3.0 } },
                    responseConclusion: 2.0,
                    truthValue: 1.0);

                expectedClassifier.Add(
                    featurePremises: new List<SortedSet<double>>(2) {
                        new SortedSet<double>(){ 3.0, 4.0, 5.0 },
                        new SortedSet<double>(){ 0.0, 1.0, 2.0, 3.0 } },
                    responseConclusion: 1.0,
                    truthValue: 1.0);

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expectedClassifier.Entailments),
                    actual: new List<CategoricalEntailment>(actualClassifier.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);

                Assert.AreEqual(
                    expected: 1.0,
                    actual: CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                        predictedDataSet: actualClassifier.Classify(
                            dataSet: dataSet,
                            featureVariableIndexes: featureIndexes),
                        predictedResponseVariableIndex: 0,
                        actualDataSet: dataSet,
                        actualResponseVariableIndex: 2),
                    DoubleMatrixTest.Accuracy);
            }
        }
    }
}
