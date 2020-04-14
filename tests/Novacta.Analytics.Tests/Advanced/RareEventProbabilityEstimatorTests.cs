// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.CrossEntropy;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class RareEventProbabilityEstimatorTests
    {
        [TestMethod]
        public void EstimateTest()
        {
            // context is null
            {
                string parameterName = "context";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context: null,
                            rarity: 0.1,
                            sampleSize: 1000,
                            estimationSampleSize: 10000);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // rarity is less than 0
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    String.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"), 0.0, 1.0);

                string parameterName = "rarity";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context:
                                TestableRareEventProbabilityEstimationContext00
                                    .Get().Context,
                            rarity: -0.1,
                            sampleSize: 1000,
                            estimationSampleSize: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // rarity is equal to 0
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    String.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"), 0.0, 1.0);

                string parameterName = "rarity";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context:
                                TestableRareEventProbabilityEstimationContext00
                                    .Get().Context,
                            rarity: 0.0,
                            sampleSize: 1000,
                            estimationSampleSize: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // rarity is greater than 1
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    String.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"), 0.0, 1.0);

                string parameterName = "rarity";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context:
                                TestableRareEventProbabilityEstimationContext00
                                    .Get().Context,
                            rarity: 1.1,
                            sampleSize: 1000,
                            estimationSampleSize: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // rarity is equal to 1
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    String.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"), 0.0, 1.0);

                string parameterName = "rarity";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context:
                                TestableRareEventProbabilityEstimationContext00
                                    .Get().Context,
                            rarity: 1.0,
                            sampleSize: 1000,
                            estimationSampleSize: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // sampleSize is not positive
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "sampleSize";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context:
                                TestableRareEventProbabilityEstimationContext00
                                    .Get().Context,
                            rarity: 0.1,
                            sampleSize: 0,
                            estimationSampleSize: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // estimationSampleSize is not positive
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "estimationSampleSize";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.Estimate(
                            context:
                                TestableRareEventProbabilityEstimationContext00
                                    .Get().Context,
                            rarity: 0.1,
                            sampleSize: 1000,
                            estimationSampleSize: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // Valid input - HigherThanLevel
            {
                var estimator = new RareEventProbabilityEstimator()
                {
                    PerformanceEvaluationParallelOptions = { MaxDegreeOfParallelism = 1 },
                    SampleGenerationParallelOptions = { MaxDegreeOfParallelism = 1 }
                };

                // Create the context.
                var testableContext =
                    TestableRareEventProbabilityEstimationContext00.Get();

                var context = testableContext.Context;

                // Check debug info
                context.TraceExecution = true;
                Trace.Listeners.RemoveAt(0);
                var defaultListener = new DefaultTraceListener();
                Trace.Listeners.Add(defaultListener);

                // Set estimation parameters.
                double rarity = 0.1;
                int sampleSize = 1000;
                int estimationSampleSize = 10000;

                // Solve the problem.
                var results = estimator.Estimate(
                    context,
                    rarity,
                    sampleSize,
                    estimationSampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                Assert.AreEqual(
                    expected: testableContext.RareEventProbability,
                    actual: results.RareEventProbability,
                    delta: 1e-6);
            }

            // Valid input - LowerThanLevel
            {
                var estimator = new RareEventProbabilityEstimator();

                // Create the context.
                var testableContext =
                    TestableRareEventProbabilityEstimationContext01.Get();

                var context = testableContext.Context;

                // Check debug info
                context.TraceExecution = true;
                Trace.Listeners.RemoveAt(0);
                var defaultListener = new DefaultTraceListener();
                Trace.Listeners.Add(defaultListener);

                // Set estimation parameters.
                double rarity = 0.1;
                int sampleSize = 1000;
                int estimationSampleSize = 10000;

                // Solve the problem.
                var results = estimator.Estimate(
                    context,
                    rarity,
                    sampleSize,
                    estimationSampleSize);

                Assert.AreEqual(
                    expected: testableContext.RareEventProbability,
                    actual: results.RareEventProbability,
                    delta: 1e-5);
            }
        }

        [TestMethod]
        public void PerformanceEvaluationParallelOptionsTest()
        {
            // value is null
            {
                string parameterName = "value";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.PerformanceEvaluationParallelOptions
                        = null;
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // value is valid
            {
                int maxDegreeOfParallelism = 2;

                var estimator = new RareEventProbabilityEstimator
                {
                    PerformanceEvaluationParallelOptions
                    = new System.Threading.Tasks.ParallelOptions()
                    {
                        MaxDegreeOfParallelism = maxDegreeOfParallelism
                    }
                };

                Assert.AreEqual(
                expected: 2,
                actual: estimator
                    .PerformanceEvaluationParallelOptions
                    .MaxDegreeOfParallelism);
            }
        }

        [TestMethod]
        public void SampleGenerationParallelOptionsTest()
        {
            // value is null
            {
                string parameterName = "value";

                var estimator = new RareEventProbabilityEstimator();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        estimator.SampleGenerationParallelOptions
                        = null;
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // value is valid
            {
                int maxDegreeOfParallelism = 2;

                var estimator = new RareEventProbabilityEstimator
                {
                    SampleGenerationParallelOptions
                    = new System.Threading.Tasks.ParallelOptions()
                    {
                        MaxDegreeOfParallelism = maxDegreeOfParallelism
                    }
                };

                Assert.AreEqual(
                expected: 2,
                actual: estimator
                    .SampleGenerationParallelOptions
                    .MaxDegreeOfParallelism);
            }
        }
    }
}