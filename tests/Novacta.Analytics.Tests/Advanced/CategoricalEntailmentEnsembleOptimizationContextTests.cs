// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.CrossEntropy;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class CategoricalEntailmentEnsembleOptimizationContextTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // optimizationGoal is not a field of OptimizationGoal
            {
                var STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL");

                string parameterName = "optimizationGoal";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(2) { 5, 6 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: (OptimizationGoal)(-1),
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL,
                    expectedParameterName: parameterName);
            }

            // objectiveFunction is null
            {
                string parameterName = "objectiveFunction";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: null,
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // featureCategoryCounts is null
            {
                string parameterName = "featureCategoryCounts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: null,
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // featureCategoryCounts is empty
            {
                var STR_EXCEPT_PAR_MUST_BE_NON_EMPTY =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY");

                string parameterName = "featureCategoryCounts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(),
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_NON_EMPTY,
                    expectedParameterName: parameterName);
            }

            // featureCategoryCounts has zero entries
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE");

                string parameterName = "featureCategoryCounts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 0, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // featureCategoryCounts has negative entries
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE");

                string parameterName = "featureCategoryCounts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, -1, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // numberOfResponseCategories is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "numberOfResponseCategories";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 0,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // numberOfResponseCategories is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "numberOfResponseCategories";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: -1,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // numberOfCategoricalEntailments is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "numberOfCategoricalEntailments";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 0,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // numberOfCategoricalEntailments is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "numberOfCategoricalEntailments";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: -1,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "minimumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 0,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "minimumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: -1,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfIterations is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfIterations is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is greater than maximumNumberOfIterations
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "maximumNumberOfIterations",
                        "minimumNumberOfIterations");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 2);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: parameterName);
            }

            // probabilitySmoothingCoefficient is zero
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "probabilitySmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: .0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // probabilitySmoothingCoefficient is negative
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "probabilitySmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: -.1,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // probabilitySmoothingCoefficient is one
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "probabilitySmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: 1.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // probabilitySmoothingCoefficient is greater than one
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "probabilitySmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CategoricalEntailmentEnsembleOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            featureCategoryCounts: new List<int>(3) { 5, 6, 7 },
                            numberOfResponseCategories: 3,
                            numberOfCategoricalEntailments: 4,
                            allowEntailmentPartialTruthValues: true,
                            probabilitySmoothingCoefficient: 1.1,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // valid input - HigherThanLevel
            {
                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext00.Get();

                var context = testableContext.Context;

                Assert.AreEqual(
                    expected: testableContext.StateDimension,
                    actual: context.StateDimension);

                Assert.AreEqual(
                    expected: testableContext.TraceExecution,
                    actual: context.TraceExecution);

                Assert.AreEqual(
                    expected: testableContext.EliteSampleDefinition,
                    actual: context.EliteSampleDefinition);

                Assert.AreEqual(
                    expected: testableContext.OptimizationGoal,
                    actual: context.OptimizationGoal);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.InitialParameter,
                    actual: context.InitialParameter,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.MinimumNumberOfIterations,
                    actual: context.MinimumNumberOfIterations);

                Assert.AreEqual(
                    expected: testableContext.MaximumNumberOfIterations,
                    actual: context.MaximumNumberOfIterations);

                Assert.AreEqual(
                    expected: testableContext.FeatureCategoryCounts.Count,
                    actual: context.FeatureCategoryCounts.Count);

                for (int i = 0; i < context.FeatureCategoryCounts.Count; i++)
                {
                    Assert.AreEqual(
                        expected: testableContext.FeatureCategoryCounts[i],
                        actual: context.FeatureCategoryCounts[i]);
                }

                Assert.AreEqual(
                    expected: testableContext.NumberOfResponseCategories,
                    actual: context.NumberOfResponseCategories);

                Assert.AreEqual(
                    expected: testableContext.NumberOfCategoricalEntailments,
                    actual: context.NumberOfCategoricalEntailments);

                Assert.AreEqual(
                    expected: testableContext.AllowEntailmentPartialTruthValues,
                    actual: context.AllowEntailmentPartialTruthValues);

                Assert.AreEqual(
                    expected: testableContext.ProbabilitySmoothingCoefficient,
                    actual: context.ProbabilitySmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void RunTest()
        {
            var optimizer = new SystemPerformanceOptimizer();

            // Create the context.
            var testableContext =
                TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

            var context = testableContext.Context;

            context.TraceExecution = true;

            // Set optimization parameters.
            int sampleSize = 3600;
            double rarity = 0.01;

            // Solve the problem.
            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            Assert.AreEqual(
                expected: true,
                actual: results.HasConverged);

            var expectedClassifier =
                new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: testableContext.FeatureVariables,
                    responseVariable: testableContext.ResponseVariable);

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

            var actualClassifier =
                new CategoricalEntailmentEnsembleClassifier(
                    featureVariables: testableContext.FeatureVariables,
                    responseVariable: testableContext.ResponseVariable);

            actualClassifier.Add(
                featurePremises: new List<SortedSet<double>>(1) {
                        new SortedSet<double>(){ 0.0, 1.0, 2.0 } },
                responseConclusion: 0.0,
                truthValue: 1.0);

            actualClassifier.Add(
                context.GetCategoricalEntailmentEnsembleClassifier(
                    state: results.OptimalState,
                    featureVariables: testableContext.FeatureVariables,
                    responseVariable: testableContext.ResponseVariable)
                        .Entailments[0]);

            var expectedTrainedEntailment = expectedClassifier.Entailments[1];
            var actualTrainedEntailment = actualClassifier.Entailments[1];

            Assert.IsTrue(
                expectedTrainedEntailment.FeaturePremises[0]
                    .IsSubsetOf(
                        actualTrainedEntailment.FeaturePremises[0]));

            Assert.AreEqual(
                expected: expectedTrainedEntailment.ResponseConclusion,
                actual: actualTrainedEntailment.ResponseConclusion,
                DoubleMatrixTest.Accuracy);

            Assert.AreEqual(
                expected: testableContext.OptimalPerformance,
                actual: results.OptimalPerformance,
                DoubleMatrixTest.Accuracy);
        }

        [TestMethod]
        public void GetCategoricalEntailmentEnsembleClassifierTest()
        {
            // state is null
            {
                string parameterName = "state";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: null,
                            featureVariables: testableContext.FeatureVariables,
                            responseVariable: testableContext.ResponseVariable);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // state has wrong count
            {
                string STR_EXCEPT_CEE_INVALID_STATE_COUNT
                    = ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_INVALID_STATE_COUNT");

                string parameterName = "state";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: DoubleMatrix.Dense(1, 8),
                            featureVariables: testableContext.FeatureVariables,
                            responseVariable: testableContext.ResponseVariable);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_INVALID_STATE_COUNT,
                    expectedParameterName: parameterName);
            }

            // featureVariables is null
            {
                string parameterName = "featureVariables";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: DoubleMatrix.Dense(1, 9,
                                new double[9] {
                                    0, 0, 0, 1, 1, 1,    1, 0,    1.0 }),
                            featureVariables: null,
                            responseVariable: testableContext.ResponseVariable);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // featureVariables has wrong count
            {
                string STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT
                    = ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT");

                string parameterName = "featureVariables";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: DoubleMatrix.Dense(1, 9,
                                new double[9] {
                                    0, 0, 0, 1, 1, 1,    1, 0,    1.0 }),
                            featureVariables: new List<CategoricalVariable>(),
                            responseVariable: testableContext.ResponseVariable);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT,
                    expectedParameterName: parameterName);
            }

            // featureVariables contains a variable having wrong number of categories
            {
                string STR_EXCEPT_CEE_INVALID_FEATURE_CATEGORIES_COUNT
                    = ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_INVALID_FEATURE_CATEGORIES_COUNT");

                string parameterName = "featureVariables";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: DoubleMatrix.Dense(1, 9,
                                new double[9] {
                                    0, 0, 0, 1, 1, 1,    1, 0,    1.0 }),
                            featureVariables: new List<CategoricalVariable>()
                                { new CategoricalVariable("F") { { 0.0, "A" }, 1.0 } },
                            responseVariable: testableContext.ResponseVariable);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_INVALID_FEATURE_CATEGORIES_COUNT,
                    expectedParameterName: parameterName);
            }

            // responseVariable is null
            {
                string parameterName = "responseVariable";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: DoubleMatrix.Dense(1, 9,
                                new double[9] {
                                    0, 0, 0, 1, 1, 1,    1, 0,    1.0 }),
                            featureVariables: testableContext.FeatureVariables,
                            responseVariable: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // responseVariable has a wrong number of categories
            {
                string STR_EXCEPT_CEE_INVALID_RESPONSE_CATEGORIES_COUNT
                    = ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_INVALID_RESPONSE_CATEGORIES_COUNT");

                string parameterName = "responseVariable";

                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.GetCategoricalEntailmentEnsembleClassifier(
                            state: DoubleMatrix.Dense(1, 9,
                                new double[9] {
                                    0, 0, 0, 1, 1, 1,    1, 0,    1.0 }),
                            featureVariables: testableContext.FeatureVariables,
                            responseVariable: new CategoricalVariable("R") { { 0.0, "A" } });
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CEE_INVALID_RESPONSE_CATEGORIES_COUNT,
                    expectedParameterName: parameterName);
            }

            // valid input
            {
                var testableContext = TestableCategoricalEntailmentEnsembleOptimizationContext01.Get();

                var context = testableContext.Context;

                var actual = context.GetCategoricalEntailmentEnsembleClassifier(
                    state: DoubleMatrix.Dense(1, 9,
                        new double[9] {
                                    0, 0, 0, 1, 1, 1,    1, 0,    .99 }),
                    featureVariables: testableContext.FeatureVariables,
                    responseVariable: testableContext.ResponseVariable);

                var expected =
                    new CategoricalEntailmentEnsembleClassifier(
                        featureVariables: testableContext.FeatureVariables,
                        responseVariable: testableContext.ResponseVariable);

                expected.Add(
                    new CategoricalEntailment(
                        featureVariables: testableContext.FeatureVariables,
                        responseVariable: testableContext.ResponseVariable,
                        featurePremises: new List<SortedSet<double>>() {
                            new SortedSet<double>() { 3.0, 4.0, 5.0 } },
                        responseConclusion: 0.0,
                        truthValue: .99));

                ListAssert<CategoricalEntailment>.ContainSameItems(
                    expected: new List<CategoricalEntailment>(expected.Entailments),
                    actual: new List<CategoricalEntailment>(actual.Entailments),
                    areEqual: CategoricalEntailmentAssert.AreEqual);
            }
        }

        [TestMethod]
        public void GetOptimalStateTest()
        {
            // valid input - random ties resolution
            {
                var context = new CategoricalEntailmentEnsembleOptimizationContext(
                    objectiveFunction:
                        (DoubleMatrix state) => { return Double.PositiveInfinity; },
                    featureCategoryCounts: new List<int>(1) { 6 },
                    numberOfResponseCategories: 4,
                    numberOfCategoricalEntailments: 1,
                    allowEntailmentPartialTruthValues: true,
                    probabilitySmoothingCoefficient: .9,
                    optimizationGoal: OptimizationGoal.Maximization,
                    minimumNumberOfIterations: 5,
                    maximumNumberOfIterations: 1000);

                int numberOfEvaluations = 10000;
                double delta = .01;

                var parameter = DoubleMatrix.Dense(1, 10, new double[10] {
                    .5, .5, .5, .5, .5, .5, .25, .25, .25, .25 });

                // Generate states

                var states = new int[numberOfEvaluations];

                var responseIndexes = IndexCollection.Range(6, 9);

                for (int i = 0; i < numberOfEvaluations; i++)
                {
                    var state = context.GetOptimalState(parameter);

                    states[i] = state.Vec(responseIndexes).FindNonzero()[0];
                }

                // Compute the actual inclusion probabilities

                DoubleMatrix actualInclusionProbabilities =
                    DoubleMatrix.Dense(context.NumberOfResponseCategories, 1);

                var stateIndexes = IndexCollection.Default(numberOfEvaluations - 1);

                for (int j = 0; j < context.NumberOfResponseCategories; j++)
                {
                    var samplesContainingCurrentUnit =
                    IndexPartition.Create(
                        stateIndexes,
                        (i) => { return states[i] == j; });

                    actualInclusionProbabilities[j] =
                        (double)samplesContainingCurrentUnit[true].Count
                        /
                        (double)numberOfEvaluations;
                }

                // Check the number of distinct generated states

                var distinctStates =
                    IndexPartition.Create(
                        states);

                int numberOfDistinctStates =
                    distinctStates.Count;

                Assert.AreEqual(
                    expected: context.NumberOfResponseCategories,
                    actual: numberOfDistinctStates);

                // Check that the Chebyshev Inequality holds true
                // for each inclusion probability

                var expectedInclusionProbabilities =
                    DoubleMatrix.Dense(context.NumberOfResponseCategories, 1,
                        1.0 / context.NumberOfResponseCategories);

                for (int j = 0; j < context.NumberOfResponseCategories; j++)
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
    }
}