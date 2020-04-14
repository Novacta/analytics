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
    public class PartitionOptimizationContextTests
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: null,
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 0,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: -1,
                            partitionDimension: 2,
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

            // partitionDimension is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                string parameterName = "partitionDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 0,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: parameterName);
            }

            // partitionDimension is one
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                string parameterName = "partitionDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 1,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: parameterName);
            }

            // partitionDimension is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                string parameterName = "partitionDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: -1,
                            probabilitySmoothingCoefficient: .8,
                            optimizationGoal: OptimizationGoal.Minimization,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: parameterName);
            }

            // partitionDimension is equal to stateDimension
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER"),
                        "partitionDimension",
                        "stateDimension");

                string parameterName = "partitionDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 7,
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

            // partitionDimension is greater than stateDimension
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER"),
                        "partitionDimension",
                        "stateDimension");

                string parameterName = "partitionDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 8,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                        new PartitionOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            stateDimension: 7,
                            partitionDimension: 2,
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
                    TestablePartitionOptimizationContext00.Get();

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
                    expected: testableContext.PartitionDimension,
                    actual: context.PartitionDimension);

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
                    TestablePartitionOptimizationContext00.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                int sampleSize = 2000;
                double rarity = 0.01;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                var expectedPartition = IndexPartition.Create(
                    testableContext.OptimalState);

                var actualPartition = IndexPartition.Create(
                    results.OptimalState);

                IndexPartitionAssert.HaveEqualIdentifiers(
                    expected: expectedPartition,
                    actual: actualPartition);

                IndexPartitionAssert.HaveEqualParts(
                    expected: expectedPartition,
                    actual: actualPartition);

                Assert.AreEqual(
                    expected: testableContext.OptimalPerformance,
                    actual: results.OptimalPerformance,
                    DoubleMatrixTest.Accuracy);
            }
        }
    }
}