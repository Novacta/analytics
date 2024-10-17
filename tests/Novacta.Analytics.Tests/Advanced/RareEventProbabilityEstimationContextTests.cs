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
    public class RareEventProbabilityEstimationContextTests
    {
        [TestMethod]
        public void StopExecutionTest()
        {
            // levels is null
            {
                string parameterName = "levels";

                var context =
                    TestableRareEventProbabilityEstimationContext00.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.StopExecution(
                            iteration: 1,
                            levels: null,
                            parameters: new LinkedList<DoubleMatrix>());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // parameters is null
            {
                string parameterName = "parameters";

                var context =
                    TestableRareEventProbabilityEstimationContext00.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.StopExecution(
                            iteration: 1,
                            levels: new LinkedList<double>(),
                            parameters: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }
        }

        [TestMethod]
        public void UpdateLevelTest()
        {
            // performances is null
            {
                string parameterName = "performances";

                var context =
                    TestableRareEventProbabilityEstimationContext00.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.UpdateLevel(
                            performances: null,
                            sample: DoubleMatrix.Dense(1, 1),
                            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
                            rarity: .01,
                            out DoubleMatrix eliteSample);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // sample is null
            {
                string parameterName = "sample";

                var context =
                    TestableRareEventProbabilityEstimationContext00.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.UpdateLevel(
                            performances: DoubleMatrix.Dense(1, 1),
                            sample: null,
                            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
                            rarity: .01,
                            out DoubleMatrix eliteSample);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }
        }

        [TestMethod]
        public void ConstructorTest()
        {
            // stateDimension is not positive
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "stateDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new RareEventProbabilityEstimationContext00(
                            stateDimension: 0,
                            thresholdLevel: 2.0,
                            rareEventPerformanceBoundedness:
                                RareEventPerformanceBoundedness.Lower,
                            initialParameter: DoubleMatrix.Dense(1, 5,
                                [0.25, 0.4, 0.1, 0.3, 0.2]));
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // rareEventPerformanceBoundedness is not a field of RareEventPerformanceBoundedness
            {
                var STR_EXCEPT_NOT_FIELD_OF_RARE_EVENT_PERFORMANCE_BOUNDEDNESS =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NOT_FIELD_OF_RARE_EVENT_PERFORMANCE_BOUNDEDNESS");

                string parameterName = "rareEventPerformanceBoundedness";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new RareEventProbabilityEstimationContext00(
                            stateDimension: 5,
                            thresholdLevel: 2.0,
                            rareEventPerformanceBoundedness:
                                (RareEventPerformanceBoundedness)(-1),
                            initialParameter: DoubleMatrix.Dense(1, 5,
                                [0.25, 0.4, 0.1, 0.3, 0.2]));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_NOT_FIELD_OF_RARE_EVENT_PERFORMANCE_BOUNDEDNESS,
                    expectedParameterName: parameterName);
            }

            // initialParameter is null
            {
                string parameterName = "initialParameter";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new RareEventProbabilityEstimationContext00(
                            stateDimension: 5,
                            thresholdLevel: 2.0,
                            rareEventPerformanceBoundedness:
                                RareEventPerformanceBoundedness.Lower,
                            initialParameter: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // valid input - HigherThanLevel
            {
                var testableContext = TestableRareEventProbabilityEstimationContext00.Get();

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
                    expected: testableContext.RareEventPerformanceBoundedness,
                    actual: context.RareEventPerformanceBoundedness);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.NominalParameter,
                    actual: context.InitialParameter,
                    DoubleMatrixTest.Accuracy);
            }

            // valid input - LowerThanLevel
            {
                var testableContext = TestableRareEventProbabilityEstimationContext01.Get();

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
                    expected: testableContext.RareEventPerformanceBoundedness,
                    actual: context.RareEventPerformanceBoundedness);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.NominalParameter,
                    actual: context.InitialParameter,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void TraceExecutionTest()
        {
            var stateDimension = 1;
            var thresholdLevel = -4.0;
            var rareEventPerformanceBoundedness =
                RareEventPerformanceBoundedness.Upper;
            var nominalParameter = DoubleMatrix.Dense(2, 1,
                [0, 1]);

            var context = new RareEventProbabilityEstimationContext01(
                        stateDimension: stateDimension,
                        thresholdLevel: thresholdLevel,
                        rareEventPerformanceBoundedness:
                            rareEventPerformanceBoundedness,
                        initialParameter: nominalParameter)
            {
                TraceExecution = true
            };

            Assert.AreEqual(
                expected: true,
                actual: context.TraceExecution);
        }
    }
}
