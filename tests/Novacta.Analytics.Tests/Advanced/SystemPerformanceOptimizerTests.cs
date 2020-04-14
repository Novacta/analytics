// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.CrossEntropy;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class SystemPerformanceOptimizerTests
    {
        [TestMethod]
        public void RunTest()
        {
            // context is null
            {
                string parameterName = "context";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var sample = DoubleMatrix.Dense(1, 1);

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Run",
                            methodArgs: new object[] { null, 1, .01 });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }
        }

        [TestMethod]
        public void EvauatePerformancesTest()
        {
            // context is null
            {
                string parameterName = "context";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var sample = DoubleMatrix.Dense(1, 1);

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "EvaluatePerformances",
                            methodArgs: new object[] { null, sample });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }

            // sample is null
            {
                string parameterName = "sample";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "EvaluatePerformances",
                            methodArgs: new object[] { context, null });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }

            // sample is not context compatible
            {
                string parameterName = "sample";

                string innerMessage =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEP_SAMPLE_IS_CONTEXT_INCOMPATIBLE") +
                    Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "EvaluatePerformances",
                            methodArgs: new object[] {
                                context,
                                DoubleMatrix.Dense(1, context.StateDimension + 1)
                            });
                    },
                    expectedInnerType: typeof(ArgumentException),
                    expectedInnerMessage: innerMessage);
            }
        }

        [TestMethod]
        public void OptimizeTest()
        {
            // context is null
            {
                string parameterName = "context";

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.Optimize(
                            context: null,
                            rarity: 0.1,
                            sampleSize: 1000);
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

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.Optimize(
                            context:
                                TestableSystemPerformanceOptimizationContext00
                                    .Get().Context,
                            rarity: -0.1,
                            sampleSize: 1000);
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

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.Optimize(
                            context:
                                TestableSystemPerformanceOptimizationContext00
                                    .Get().Context,
                            rarity: 0.0,
                            sampleSize: 1000);
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

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.Optimize(
                            context:
                                TestableSystemPerformanceOptimizationContext00
                                    .Get().Context,
                            rarity: 1.1,
                            sampleSize: 1000);
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

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.Optimize(
                            context:
                                TestableSystemPerformanceOptimizationContext00
                                    .Get().Context,
                            rarity: 1.0,
                            sampleSize: 1000);
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

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.Optimize(
                            context:
                                TestableSystemPerformanceOptimizationContext00
                                    .Get().Context,
                            rarity: 0.1,
                            sampleSize: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }
        }

        [TestMethod]
        public void PerformanceEvaluationParallelOptionsTest()
        {
            // value is null
            {
                string parameterName = "value";

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.PerformanceEvaluationParallelOptions
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

                var optimizer = new SystemPerformanceOptimizer
                {
                    PerformanceEvaluationParallelOptions
                    = new System.Threading.Tasks.ParallelOptions()
                    {
                        MaxDegreeOfParallelism = maxDegreeOfParallelism
                    }
                };

                Assert.AreEqual(
                expected: 2,
                actual: optimizer
                    .PerformanceEvaluationParallelOptions.MaxDegreeOfParallelism);
            }
        }

        [TestMethod]
        public void SampleGenerationParallelOptionsTest()
        {
            // value is null
            {
                string parameterName = "value";

                var optimizer = new SystemPerformanceOptimizer();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        optimizer.SampleGenerationParallelOptions
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

                var optimizer = new SystemPerformanceOptimizer
                {
                    SampleGenerationParallelOptions
                    = new System.Threading.Tasks.ParallelOptions()
                    {
                        MaxDegreeOfParallelism = maxDegreeOfParallelism
                    }
                };

                Assert.AreEqual(
                expected: 2,
                actual: optimizer
                    .SampleGenerationParallelOptions.MaxDegreeOfParallelism);
            }
        }

        [TestMethod]
        public void SampleTest()
        {
            // context is null
            {
                string parameterName = "context";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var parameter = DoubleMatrix.Dense(1, 1);

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Sample",
                            methodArgs: new object[] { null, 1, parameter });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }

            // parameter is null
            {
                string parameterName = "parameter";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Sample",
                            methodArgs: new object[] { context, 1, null });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }

            // sampleSize is zero
            {
                string parameterName = "sampleSize";
                string innerMessage =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE") +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Sample",
                            methodArgs: new object[] {
                                context,
                                0,
                                context.InitialParameter });
                    },
                    expectedInnerType: typeof(ArgumentOutOfRangeException),
                    expectedInnerMessage: innerMessage);
            }

            // sampleSize is negative
            {
                string parameterName = "sampleSize";
                string innerMessage =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE") +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Sample",
                            methodArgs: new object[] {
                                context,
                                -1,
                                context.InitialParameter });
                    },
                    expectedInnerType: typeof(ArgumentOutOfRangeException),
                    expectedInnerMessage: innerMessage);
            }

            // parameter is not context compatible: wrong number of rows
            {
                string parameterName = "parameter";
                string innerMessage =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEP_PARAMETER_IS_CONTEXT_INCOMPATIBLE") +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Sample",
                            methodArgs: new object[] {
                                context,
                                1,
                                DoubleMatrix.Dense(
                                    context.InitialParameter.NumberOfRows + 1,
                                    context.InitialParameter.NumberOfColumns) });
                    },
                    expectedInnerType: typeof(ArgumentException),
                    expectedInnerMessage: innerMessage);
            }

            // parameter is not context compatible: wrong number of columns
            {
                string parameterName = "parameter";
                string innerMessage =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEP_PARAMETER_IS_CONTEXT_INCOMPATIBLE") +
                        Environment.NewLine + "Parameter name: " + parameterName;

                var optimizer = new SystemPerformanceOptimizer();

                var context = TestableSystemPerformanceOptimizationContext00.Get().Context;

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        Reflector.ExecuteBaseMember(
                            obj: optimizer,
                            methodName: "Sample",
                            methodArgs: new object[] { 
                                context, 
                                1, 
                                DoubleMatrix.Dense(
                                    context.InitialParameter.NumberOfRows,
                                    context.InitialParameter.NumberOfColumns + 1) });
                    },
                    expectedInnerType: typeof(ArgumentException),
                    expectedInnerMessage: innerMessage);
            }
        }
    }
}