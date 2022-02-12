// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text.Json;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class JsonSerializationTests
    {
        [TestMethod]
        public void AddDataConvertersTest()
        {
            // options is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerialization.AddDataConverters((JsonSerializerOptions)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "options");
            }
        }
        
        [TestMethod]
        public void JsonDictionaryInt32StringConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""0"": ""Row-0"",",
                      @"""2"": ""Row-2"",",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Dictionary<int, string>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Pairs

            // No Key int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""A"": ""Row-0"",",
                      @"""2"": ""Row-2""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Dictionary<int, string>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion
        }

        #region Categorical data

        [TestMethod]
        public void JsonCategoricalEntailmentConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region FeatureVariables

            // No FeatureVariables property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong FeatureVariables property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables1"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region ResponseVariable

            // No ResponseVariable property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong ResponseVariable property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable2"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region FeaturePremises

            // No FeaturePremises property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong ResponseVariable property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises3"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region ResponseConclusion

            // No ResponseConclusion property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong ResponseConclusion property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion4"": 1,",
                      @"""TruthValue"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No ResponseConclusion number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": null,",
                      @"""TruthValue"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region TruthValue

            // No TruthValue property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong TruthValue property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue5"": 0.9",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No TruthValue number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""FeatureVariables"": [",
                        "{",
                          @"""Name"": ""F-0"",",
                          @"""Categories"": [",
                            "{",
                              @"""Code"": 0,",
                              @"""Label"": ""0""",
                            "},",
                            "{",
                              @"""Code"": 1,",
                              @"""Label"": ""1""",
                            "}",
                          "],",
                          @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""ResponseVariable"": {",
                        @"""Name"": ""R"",",
                        @"""Categories"": [",
                          "{",
                            @"""Code"": 0,",
                            @"""Label"": ""a""",
                          "},",
                          "{",
                            @"""Code"": 1,",
                            @"""Label"": ""b""",
                          "}",
                        "],",
                        @"""IsReadOnly"": true",
                      "},",
                      @"""FeaturePremises"": [",
                        "[",
                          "0",
                        "]",
                      "],",
                      @"""ResponseConclusion"": 1,",
                      @"""TruthValue"": 0.9,",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalEntailment>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonCategoricalEntailmentSerializationTest()
        {
            // Some premises are empty
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

                var serializedEntailment = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedEntailment,
                    typeof(CategoricalEntailment),
                    options);

                var deserializedEntailment = JsonSerializer.Deserialize<CategoricalEntailment>(
                    json,
                    options);

                CategoricalEntailmentAssert.IsStateAsExpected(
                    target: deserializedEntailment,
                    expectedFeatureVariables: featureVariables,
                    expectedResponseVariable: responseVariable,
                    expectedFeaturePremises: featurePremises,
                    expectedResponseConclusion: responseConclusion,
                    expectedTruthValue: truthValue);
            }

            // No premises are empty
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
                        new SortedSet<double>() { 2.0 } };

                double responseConclusion = responseVariable.Categories[1].Code;
                double truthValue = .9;

                var serializedEntailment = new CategoricalEntailment(
                    featureVariables: featureVariables,
                    responseVariable: responseVariable,
                    featurePremises: featurePremises,
                    responseConclusion: responseConclusion,
                    truthValue: truthValue);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedEntailment,
                    typeof(CategoricalEntailment),
                    options);

                var deserializedEntailment = JsonSerializer.Deserialize<CategoricalEntailment>(
                    json,
                    options);

                CategoricalEntailmentAssert.IsStateAsExpected(
                    target: deserializedEntailment,
                    expectedFeatureVariables: featureVariables,
                    expectedResponseVariable: responseVariable,
                    expectedFeaturePremises: featurePremises,
                    expectedResponseConclusion: responseConclusion,
                    expectedTruthValue: truthValue);
            }
        }

        [TestMethod]
        public void JsonCategoricalVariableSerializationTest()
        {
            // Has not categories - IsReadOnly is true
            {
                CategoricalVariable serializedVariable = new("Variable");

                serializedVariable.SetAsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedVariable,
                    typeof(CategoricalVariable),
                    options);

                var deserializedVariable = JsonSerializer.Deserialize<CategoricalVariable>(
                    json,
                    options);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }

            // Has not categories - IsReadOnly is false
            {
                CategoricalVariable serializedVariable = new("Variable");

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedVariable,
                    typeof(CategoricalVariable),
                    options);

                var deserializedVariable = JsonSerializer.Deserialize<CategoricalVariable>(
                    json,
                    options);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }
                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }

            // Has categories - IsReadOnly is true
            {
                CategoricalVariable serializedVariable = new("Variable")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };
                serializedVariable.SetAsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedVariable,
                    typeof(CategoricalVariable),
                    options);

                var deserializedVariable = JsonSerializer.Deserialize<CategoricalVariable>(
                    json,
                    options);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }

            // Has categories - IsReadOnly is false
            {
                CategoricalVariable serializedVariable = new("Variable")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedVariable,
                    typeof(CategoricalVariable),
                    options);

                var deserializedVariable = JsonSerializer.Deserialize<CategoricalVariable>(
                    json,
                    options);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }
        }

        [TestMethod]
        public void JsonCategoricalDataSetConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {                    
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "},",
                      @"""Name"": ""MyData""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Variables

            // No Variables property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Variables property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables1"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "},",
                      @"""Name"": ""MyData""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Data

            // No Data property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Data property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data2"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "},",
                      @"""Name"": ""MyData""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Name

            // No Name property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Name property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "},",
                      @"""Name3"": ""MyData""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Name string or null value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "},",
                      @"""Name"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Variables"": [",
                        "{",
                            @"""Name"": ""F-0"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""A""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""F-1"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""I""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "},",
                        "{",
                            @"""Name"": ""R"",",
                            @"""Categories"": [",
                            "{",
                                @"""Code"": 0,",
                                @"""Label"": ""0""",
                            "}",
                            "],",
                            @"""IsReadOnly"": true",
                        "}",
                      "],",
                      @"""Data"": {",
                        @"""Matrix"": {",
                          @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 3,",
                          @"""Storage"": [",
                              "0,",
                              "0,",
                              "0",
                          "]",
                          "},",
                          @"""Name"": ""MyData"",",
                          @"""RowNames"": null,",
                          @"""ColumnNames"": {",
                            @"""0"": ""F-02"",",
                            @"""1"": ""F-1"",",
                            @"""2"": ""R""",
                          "}",
                        "}",
                      "},",
                      @"""Name"": ""MyData"",",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalDataSet>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonCategoricalDataSetSerializationTest()
        {
            #region FromEncodedData

            // Name is not null
            {
                // Create the feature variables
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "I" },
                    { 1, "II" },
                    { 2, "III" },
                    { 3, "IV" }
                };
                f1.SetAsReadOnly();

                // Create the response variable
                CategoricalVariable r = new("R")
                {
                    0,
                    1,
                    2
                };
                r.SetAsReadOnly();

                // Create a categorical data set containing
                // observations about such variables
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

                CategoricalDataSet serializedDataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                serializedDataSet.Name = "Name";

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedDataSet,
                    typeof(CategoricalDataSet),
                    options);

                var deserializedDataSet = JsonSerializer.Deserialize<CategoricalDataSet>(
                    json,
                    options);

                CategoricalDataSetAssert.AreEqual(
                    expected: serializedDataSet,
                    actual: deserializedDataSet);
            }

            // Name is null
            {
                // Create the feature variables
                CategoricalVariable f0 = new("F-0")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };
                f0.SetAsReadOnly();

                CategoricalVariable f1 = new("F-1")
                {
                    { 0, "I" },
                    { 1, "II" },
                    { 2, "III" },
                    { 3, "IV" }
                };
                f1.SetAsReadOnly();

                // Create the response variable
                CategoricalVariable r = new("R")
                {
                    0,
                    1,
                    2
                };
                r.SetAsReadOnly();

                // Create a categorical data set containing
                // observations about such variables
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

                CategoricalDataSet serializedDataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedDataSet,
                    typeof(CategoricalDataSet),
                    options);

                var deserializedDataSet = JsonSerializer.Deserialize<CategoricalDataSet>(
                    json,
                    options);

                var deserializedData =
                    (DoubleMatrix)Reflector.GetField(deserializedDataSet, "data");

                DoubleMatrixAssert.AreEqual(
                    expected: deserializedData,
                    actual: deserializedDataSet.Data,
                    DoubleMatrixTest.Accuracy);

                var deserializedVariables =
                    (List<CategoricalVariable>)
                        Reflector.GetField(deserializedDataSet, "variables");

                Assert.AreEqual(
                    deserializedDataSet.Variables.Count,
                    deserializedVariables.Count);

                for (int i = 0; i < deserializedVariables.Count; i++)
                {
                    CategoricalVariableAssert.AreEqual(
                        expected: deserializedVariables[i],
                        actual: deserializedDataSet.Variables[i]);
                }

                CategoricalDataSetAssert.AreEqual(
                    expected: serializedDataSet,
                    actual: deserializedDataSet);
            }

            #endregion

            #region Encode

            // Name is not null
            {
                // Create a data stream 
                string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Define a special categorizer for variable NUMBER
                string numberCategorizer(string token, IFormatProvider provider)
                {
                    double datum = Convert.ToDouble(token, provider);
                    if (datum == 0)
                    {
                        return "Zero";
                    }
                    else if (datum < 0)
                    {
                        return "Negative";
                    }
                    else
                    {
                        return "Positive";
                    }
                }

                // Attach the special categorizer to variable NUMBER
                int numberColumnIndex = 2;
                var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                // Encode the categorical data set
                StreamReader reader = new(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                bool firstLineContainsColumnHeaders = true;
                CategoricalDataSet serializedDataSet = CategoricalDataSet.Encode(
                    reader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders,
                    specialCategorizers,
                    CultureInfo.InvariantCulture);

                serializedDataSet.Name = "Name";

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedDataSet,
                    typeof(CategoricalDataSet),
                    options);

                var deserializedDataSet = JsonSerializer.Deserialize<CategoricalDataSet>(
                    json,
                    options);

                CategoricalDataSetAssert.AreEqual(
                    expected: serializedDataSet,
                    actual: deserializedDataSet);
            }

            // Name is null
            {
                // Create a data stream 
                string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Define a special categorizer for variable NUMBER
                string numberCategorizer(string token, IFormatProvider provider)
                {
                    double datum = Convert.ToDouble(token, provider);
                    if (datum == 0)
                    {
                        return "Zero";
                    }
                    else if (datum < 0)
                    {
                        return "Negative";
                    }
                    else
                    {
                        return "Positive";
                    }
                }

                // Attach the special categorizer to variable NUMBER
                int numberColumnIndex = 2;
                var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                // Encode the categorical data set
                StreamReader reader = new(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                bool firstLineContainsColumnHeaders = true;
                CategoricalDataSet serializedDataSet = CategoricalDataSet.Encode(
                    reader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders,
                    specialCategorizers,
                    CultureInfo.InvariantCulture);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedDataSet,
                    typeof(CategoricalDataSet),
                    options);

                var deserializedDataSet = JsonSerializer.Deserialize<CategoricalDataSet>(
                    json,
                    options);

                var deserializedData =
                    (DoubleMatrix)Reflector.GetField(deserializedDataSet, "data");

                DoubleMatrixAssert.AreEqual(
                    expected: deserializedData,
                    actual: deserializedDataSet.Data,
                    DoubleMatrixTest.Accuracy);

                var deserializedVariables =
                    (List<CategoricalVariable>)
                        Reflector.GetField(deserializedDataSet, "variables");

                Assert.AreEqual(
                    deserializedDataSet.Variables.Count,
                    deserializedVariables.Count);

                for (int i = 0; i < deserializedVariables.Count; i++)
                {
                    CategoricalVariableAssert.AreEqual(
                        expected: deserializedVariables[i],
                        actual: deserializedDataSet.Variables[i]);
                }

                CategoricalDataSetAssert.AreEqual(
                    expected: serializedDataSet,
                    actual: deserializedDataSet);
            }

            #endregion
        }

        [TestMethod]
        public void JsonCategoricalVariableConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                        @"""Name"": ""F-0"",",
                        @"""Categories"": [",
                        "],",
                        @"""IsReadOnly"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Name

            // No Name property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Name property name
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name1"": ""F-0"",",
                        @"""Categories"": [",
                        "],",
                        @"""IsReadOnly"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Name string value
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": true,",
                        @"""Categories"": [",
                        "],",
                        @"""IsReadOnly"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Categories

            // No Categories property name
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": ""F-0""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Categories property name
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": ""F-0"",",
                        @"""Categories2"": [",
                        "],",
                        @"""IsReadOnly"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Categories array value
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": ""F-0"",",
                        @"""Categories"": 0",
                        @"""IsReadOnly"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region IsReadOnly

            // No IsReadOnly property name
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": ""F-0"",",
                        @"""Categories"": [",
                        "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong IsReadOnly property name
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": ""F-0"",",
                        @"""Categories"": [",
                        "],",
                        @"""IsReadOnly3"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No IsReadOnly boolean value
            {
                var strings = new string[]
                {
                    "{",
                        @"""Name"": ""F-0"",",
                        @"""Categories"": [",
                        "],",
                        @"""IsReadOnly"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Name"": ""F-0"",",
                      @"""Categories"": [",
                      "],",
                      @"""IsReadOnly"": true,",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<CategoricalVariable>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonCategoryConverterTest() 
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""Code"": 0,",
                      @"""Label"": ""A""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Code

            // No Code property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Code property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Code1"": 0,",
                      @"""Label"": ""A""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Code number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Code"": null,",
                      @"""Label"": ""A""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Label

            // No Label property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Code"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Label property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Code"": 0,",
                      @"""Label2"": ""A""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Label string value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Code"": 0,",
                      @"""Label"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Code"": 0,",
                      @"""Label"": ""A"",",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Category>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        #endregion

        #region Double

        [TestMethod]
        public void JsonDoubleMatrixImplementorConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region StorageScheme

            // No StorageScheme property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong StorageScheme property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme1"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No StorageScheme int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": null,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Unknown StorageScheme int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": -1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region NumberOfRows

            // No NumberOfRows property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong NumberOfRows property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows2"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No NumberOfRows int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": null,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region NumberOfColumns

            // No NumberOfColumns property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong NumberOfColumns property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns3"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No NumberOfColumns int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": null,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Dense

            #region Storage

            // No Storage property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Storage property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage4"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Storage start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Storage number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "null,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Storage end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1,",
                      "[",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #endregion

            #region Sparse

            #region Capacity

            // No Capacity property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Capacity property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity4"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Capacity int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": null,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Values

            // No Values property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Values property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values5"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Values start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Values number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "null,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Values end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0,",
                      "[,",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Columns

            // No Columns property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Columns property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns6"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Columns start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Columns number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "null,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Columns end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0,",
                      "[,",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region RowIndex

            // No RowIndex property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                       @"""Columns"": [",
                        "1,",
                        "0",
                      "]",
                   "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong RowIndex property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex7"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No RowIndex start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No RowIndex number value
            {
                var strings = new string[]
                {
                    "{",
                    @"""StorageScheme"": 1,",
                    @"""NumberOfRows"": 1,",
                    @"""NumberOfColumns"": 2,",
                    @"""Capacity"": 2,",
                    @"""Values"": [",
                      "1,",
                      "0",
                    "],",
                    @"""Columns"": [",
                      "1,",
                      "0",
                    "],",
                    @"""RowIndex"": [",
                      "null,",
                      "1",
                    "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No RowIndex end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "1,",
                        "0",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1,",
                      "[",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "0,",
                        "1",
                      "],",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<double>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonDoubleMatrixConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Implementor

            // No Implementor property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Implementor property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor1"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Name

            // No Name property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Name property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name2"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Name string or null value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": true",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region RowNames

            // No RowNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong RowNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames3"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region ColumnNames

            // No ColumnNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong ColumnNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames4"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "1,",
                          "0",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null,",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<DoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonReadOnlyDoubleMatrixConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""Matrix"": {",
                        @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 2,",
                          @"""Storage"": [",
                            "1,",
                            "0",
                          "]",
                        "},",
                        @"""Name"": ""MyName"",",
                        @"""RowNames"": null,",
                        @"""ColumnNames"": null",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Matrix

            // No Matrix property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Matrix property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Matrix1"": {",
                        @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 2,",
                          @"""Storage"": [",
                            "1,",
                            "0",
                          "]",
                        "},",
                        @"""Name"": ""MyName"",",
                        @"""RowNames"": null,",
                        @"""ColumnNames"": null",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Matrix"": {",
                        @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 2,",
                          @"""Storage"": [",
                            "1,",
                            "0",
                          "]",
                        "},",
                        @"""Name"": ""MyName"",",
                        @"""RowNames"": null,",
                        @"""ColumnNames"": null",
                      "},",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonDoubleMatrixSerializationTest()
        {
            #region DoubleMatrix 

            #region Without names

            // Dense
            {
                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.AsDense;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(DoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<DoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.AsSparse;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(DoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<DoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region With names

            // Dense
            {
                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.AsDense;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(DoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<DoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.AsSparse;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(DoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<DoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #endregion

            #region ReadOnlyDoubleMatrix 

            #region Without names

            // Dense
            {
                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.AsDense.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyDoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.AsSparse.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyDoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region With names

            // Dense
            {
                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.AsDense.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyDoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.AsSparse.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyDoubleMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                    json,
                    options);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #endregion
        }

        #endregion

        #region Complex

        [TestMethod]
        public void JsonComplexConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""Real"": 0,",
                      @"""Imag"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Real

            // No Real property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Real property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Real1"": 0,",
                      @"""Imag"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Real number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Real"": null,",
                      @"""Imag"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Imag

            // No Imag property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Real"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Imag property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Real"": 0,",
                      @"""Imag2"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Imag number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Real"": 0,",
                      @"""Imag"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Real"": 0,",
                      @"""Imag"": 0,",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<Complex>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonComplexMatrixImplementorConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region StorageScheme

            // No StorageScheme property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong StorageScheme property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme1"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No StorageScheme int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": null,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Unknown StorageScheme int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": -1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region NumberOfRows

            // No NumberOfRows property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong NumberOfRows property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows2"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No NumberOfRows int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": null,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region NumberOfColumns

            // No NumberOfColumns property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong NumberOfColumns property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns3"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No NumberOfColumns int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": null,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Dense

            #region Storage

            // No Storage property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Storage property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage4"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Storage start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Storage number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "null,",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Storage end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 0,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                      "[",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #endregion

            #region Sparse

            #region Capacity

            // No Capacity property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Capacity property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity4"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Capacity int value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": null,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Values

            // No Values property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Values property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values5"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Values start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Values number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": null,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Values end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                      "[,",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Columns

            // No Columns property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Columns property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns6"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Columns start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Columns number value
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "null,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Columns end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0,",
                      "[,",
                      @"""RowIndex"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region RowIndex

            // No RowIndex property name 
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                       @"""Columns"": [",
                        "1,",
                        "0",
                      "]",
                   "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong RowIndex property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex7"": [",
                        "0,",
                        "1",
                      "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No RowIndex start array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": true",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No RowIndex number value
            {
                var strings = new string[]
                {
                    "{",
                    @"""StorageScheme"": 1,",
                    @"""NumberOfRows"": 1,",
                    @"""NumberOfColumns"": 2,",
                    @"""Capacity"": 2,",
                    @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                    "],",
                    @"""Columns"": [",
                      "1,",
                      "0",
                    "],",
                    @"""RowIndex"": [",
                      "null,",
                      "1",
                    "]",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No RowIndex end array
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Capacity"": 2,",
                      @"""Values"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Columns"": [",
                        "1,",
                        "0",
                      "],",
                      @"""RowIndex"": [",
                        "0,",
                        "1,",
                      "[",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""StorageScheme"": 1,",
                      @"""NumberOfRows"": 1,",
                      @"""NumberOfColumns"": 2,",
                      @"""Storage"": [",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "},",
                        "{",
                          @"""Real"": 0,",
                          @"""Imag"": 0",
                        "}",
                      "],",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<MatrixImplementor<Complex>>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonComplexMatrixConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Implementor

            // No Implementor property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Implementor property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor1"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region Name

            // No Name property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Name property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name2"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // No Name string or null value
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": true",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region RowNames

            // No RowNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName""",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong RowNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames3"": null,",
                      @"""ColumnNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            #region ColumnNames

            // No ColumnNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong ColumnNames property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames4"": null",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Implementor"": {",
                        @"""StorageScheme"": 0,",
                        @"""NumberOfRows"": 1,",
                        @"""NumberOfColumns"": 2,",
                        @"""Storage"": [",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "},",
                          "{",
                            @"""Real"": 0,",
                            @"""Imag"": 0",
                          "}",
                        "]",
                      "},",
                      @"""Name"": ""MyName"",",
                      @"""RowNames"": null,",
                      @"""ColumnNames"": null,",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonReadOnlyComplexMatrixConverterTest()
        {
            // No start object
            {
                var strings = new string[]
                {
                      @"""Matrix"": {",
                        @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 2,",
                          @"""Storage"": [",
                            "{",
                              @"""Real"": 0,",
                              @"""Imag"": 0",
                            "},",
                            "{",
                              @"""Real"": 0,",
                              @"""Imag"": 0",
                            "}",
                          "]",
                        "},",
                        @"""Name"": ""MyName"",",
                        @"""RowNames"": null,",
                        @"""ColumnNames"": null",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #region Matrix

            // No Matrix property name
            {
                var strings = new string[]
                {
                    "{",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            // Wrong Matrix property name
            {
                var strings = new string[]
                {
                    "{",
                      @"""Matrix1"": {",
                        @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 2,",
                          @"""Storage"": [",
                            "{",
                              @"""Real"": 0,",
                              @"""Imag"": 0",
                            "},",
                            "{",
                              @"""Real"": 0,",
                              @"""Imag"": 0",
                            "}",
                          "]",
                        "},",
                        @"""Name"": ""MyName"",",
                        @"""RowNames"": null,",
                        @"""ColumnNames"": null",
                      "}",
                    "}"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }

            #endregion

            // No end object
            {
                var strings = new string[]
                {
                    "{",
                      @"""Matrix"": {",
                        @"""Implementor"": {",
                          @"""StorageScheme"": 0,",
                          @"""NumberOfRows"": 1,",
                          @"""NumberOfColumns"": 2,",
                          @"""Storage"": [",
                            "{",
                              @"""Real"": 0,",
                              @"""Imag"": 0",
                            "},",
                            "{",
                              @"""Real"": 0,",
                              @"""Imag"": 0",
                            "}",
                          "]",
                        "},",
                        @"""Name"": ""MyName"",",
                        @"""RowNames"": null,",
                        @"""ColumnNames"": null",
                      "},",
                      @"""Go"": 1"
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = String.Concat(strings);

                ExceptionAssert.Throw(
                    () =>
                    {
                        JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                            json,
                            options);
                    },
                    expectedType: typeof(JsonException));
            }
        }

        [TestMethod]
        public void JsonComplexMatrixSerializationTest()
        {
            #region ComplexMatrix 

            #region Without names

            // Dense
            {
                var testableMatrix = TestableComplexMatrix11.Get();
                var serializedMatrix = testableMatrix.AsDense;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableComplexMatrix11.Get();
                var serializedMatrix = testableMatrix.AsSparse;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region With names

            // Dense
            {
                var testableMatrix = TestableComplexMatrix16.Get();
                var serializedMatrix = testableMatrix.AsDense;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableComplexMatrix16.Get();
                var serializedMatrix = testableMatrix.AsSparse;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            #endregion

            #endregion

            #region ReadOnlyComplexMatrix 

            #region Without names

            // Dense
            {
                var testableMatrix = TestableComplexMatrix11.Get();
                var serializedMatrix = testableMatrix.AsDense.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableComplexMatrix11.Get();
                var serializedMatrix = testableMatrix.AsSparse.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region With names

            // Dense
            {
                var testableMatrix = TestableComplexMatrix16.Get();
                var serializedMatrix = testableMatrix.AsDense.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            // Sparse
            {
                var testableMatrix = TestableComplexMatrix16.Get();
                var serializedMatrix = testableMatrix.AsSparse.AsReadOnly();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerialization.AddDataConverters(options);

                string json = JsonSerializer.Serialize(
                    serializedMatrix,
                    typeof(ReadOnlyComplexMatrix),
                    options);

                var deserializedMatrix = JsonSerializer.Deserialize<ReadOnlyComplexMatrix>(
                    json,
                    options);

                ComplexMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: ComplexMatrixTest.Accuracy);
            }

            #endregion

            #endregion
        }

        #endregion
    }
}