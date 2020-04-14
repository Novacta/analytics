// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.CrossEntropy;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class CombinationOptimizationContextTests
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: null,
                            stateDimension: 7,
                            combinationDimension: 2,
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

            // stateDimension is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "stateDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 0,
                            combinationDimension: 2,
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

            // stateDimension is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "stateDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: -1,
                            combinationDimension: 2,
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

            // combinationDimension is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "combinationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 0,
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

            // combinationDimension is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "combinationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: -1,
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

            // combinationDimension is equal to stateDimension
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER"),
                        "combinationDimension",
                        "stateDimension");

                string parameterName = "combinationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 7,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER,
                    expectedParameterName: parameterName);
            }

            // combinationDimension is greater than stateDimension
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER"),
                        "combinationDimension",
                        "stateDimension");

                string parameterName = "combinationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 8,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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
                        new CombinationOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            combinationDimension: 2,
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

            // valid input - LowerThanLevel
            {
                var testableContext =
                    TestableCombinationOptimizationContext00.Get();

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
                    expected: testableContext.CombinationDimension,
                    actual: context.CombinationDimension);

                Assert.AreEqual(
                    expected: testableContext.ProbabilitySmoothingCoefficient,
                    actual: context.ProbabilitySmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // valid input - HigherThanLevel
            {
                var testableContext = TestableCombinationOptimizationContext01.Get();

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
                    expected: testableContext.CombinationDimension,
                    actual: context.CombinationDimension);

                Assert.AreEqual(
                    expected: testableContext.ProbabilitySmoothingCoefficient,
                    actual: context.ProbabilitySmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);
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
                    TestableCombinationOptimizationContext00.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                int sampleSize = 300;
                double rarity = 0.01;

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

            // Valid input - Maximization 
            {
                var optimizer = new SystemPerformanceOptimizer();

                // Create the context.
                var testableContext =
                    TestableCombinationOptimizationContext01.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                int sampleSize = 300;
                double rarity = 0.01;

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
    }
}