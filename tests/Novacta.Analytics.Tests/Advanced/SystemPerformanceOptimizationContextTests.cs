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
    public class SystemPerformanceOptimizationContextTests
    {
        [TestMethod]
        public void SmoothParameterTest()
        {
            // parameters is null
            {
                string parameterName = "parameters";

                var context =
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.SmoothParameter(
                            parameters: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }
        }

        [TestMethod]
        public void StopAtIntermediateIterationTest()
        {
            // levels is null
            {
                string parameterName = "levels";

                var context =
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.StopAtIntermediateIteration(
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
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        context.StopAtIntermediateIteration(
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
        public void StopExecutionTest()
        {
            // levels is null
            {
                string parameterName = "levels";

                var context =
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

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
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

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
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

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
                    TestableSystemPerformanceOptimizationContext02.Get().Context;

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
                        new SystemPerformanceOptimizationContext00(
                            stateDimension: 0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            initialParameter: DoubleMatrix.Dense(2, 2,
                                new double[] { -1.0, 10000.0, -1.0, 10000.0 }),
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // optimizationGoal is not a field of OptimizationGoal
            {
                var STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL");

                string parameterName = "optimizationGoal";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new SystemPerformanceOptimizationContext00(
                            stateDimension: 2,
                            optimizationGoal: (OptimizationGoal)(-1),
                            initialParameter: DoubleMatrix.Dense(2, 2,
                                new double[] { -1.0, 10000.0, -1.0, 10000.0 }),
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 10000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL,
                    expectedParameterName: parameterName);
            }

            // initialParameter is null
            {
                string parameterName = "initialParameter";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new SystemPerformanceOptimizationContext00(
                            stateDimension: 2,
                            optimizationGoal: OptimizationGoal.Minimization,
                            initialParameter: null,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 10000);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is not positive
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "minimumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new SystemPerformanceOptimizationContext00(
                            stateDimension: 2,
                            optimizationGoal: OptimizationGoal.Minimization,
                            initialParameter: DoubleMatrix.Dense(2, 2,
                                new double[] { -1.0, 10000.0, -1.0, 10000.0 }),
                            minimumNumberOfIterations: 0,
                            maximumNumberOfIterations: 10000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfIterations is not positive
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new SystemPerformanceOptimizationContext00(
                            stateDimension: 2,
                            optimizationGoal: OptimizationGoal.Minimization,
                            initialParameter: DoubleMatrix.Dense(2, 2,
                                new double[] { -1.0, 10000.0, -1.0, 10000.0 }),
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 0);
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
                        new SystemPerformanceOptimizationContext00(
                            stateDimension: 2,
                            optimizationGoal: OptimizationGoal.Minimization,
                            initialParameter: DoubleMatrix.Dense(2, 2,
                                new double[] { -1.0, 10000.0, -1.0, 10000.0 }),
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 2);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: parameterName);
            }

            // valid input - LowerThanLevel
            {
                var testableContext =
                    TestableSystemPerformanceOptimizationContext00.Get();

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
            }

            // valid input - HigherThanLevel
            {
                var testableContext = TestableSystemPerformanceOptimizationContext01.Get();

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
            }
        }

        [TestMethod]
        public void RunTest()
        {
            // Valid input - Minimization
            {
                var optimizer = new SystemPerformanceOptimizer();

                // Create the context.
                var testableContext =
                    TestableSystemPerformanceOptimizationContext00.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                double rarity = 0.05;
                int sampleSize = 1000;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: results.OptimalState,
                    delta: .03);

                Assert.AreEqual(
                    expected: testableContext.OptimalPerformance,
                    actual: results.OptimalPerformance,
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - Maximization - with overrides
            {
                var optimizer = new SystemPerformanceOptimizer()
                {
                    PerformanceEvaluationParallelOptions = { MaxDegreeOfParallelism = 1 },
                    SampleGenerationParallelOptions = { MaxDegreeOfParallelism = 1 }
                };

                // Create the context.
                var testableContext =
                    TestableSystemPerformanceOptimizationContext01.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                double rarity = 0.1;
                int sampleSize = 1000;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: results.OptimalState,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.OptimalPerformance,
                    actual: results.OptimalPerformance,
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - Maximization - without overrides
            {
                var optimizer = new SystemPerformanceOptimizer()
                {
                    PerformanceEvaluationParallelOptions = { MaxDegreeOfParallelism = 1 },
                    SampleGenerationParallelOptions = { MaxDegreeOfParallelism = 1 }
                };

                // Create the context.
                var testableContext =
                    TestableSystemPerformanceOptimizationContext02.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                double rarity = 0.1;
                int sampleSize = 1000;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: results.OptimalState,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.OptimalPerformance,
                    actual: results.OptimalPerformance,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void TraceExecutionTest()
        {
            var stateDimension = 1;
            var optimizationGoal = OptimizationGoal.Maximization;
            var initialParameter = DoubleMatrix.Dense(2, 1,
                new double[] { -6.0, 10000.0 });
            var minimumNumberOfIterations = 3;
            var maximumNumberOfIterations = 10000;

            var context = new SystemPerformanceOptimizationContext01(
                        stateDimension,
                        optimizationGoal,
                        initialParameter,
                        minimumNumberOfIterations,
                        maximumNumberOfIterations)
            {
                TraceExecution = true
            };

            Assert.AreEqual(
                expected: true,
                actual: context.TraceExecution);
        }
    }
}