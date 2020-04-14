// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.CrossEntropy;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class ContinuousOptimizationTests
    {
        [TestMethod]
        public void MinimizeTest()
        {
            // objectiveFunction is null
            {
                string parameterName = "objectiveFunction";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Minimize(
                            objectiveFunction: null,
                            initialArgument: DoubleMatrix.Dense(1, 1, -6.0));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is null
            {
                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Minimize(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is not a row vector
            {
                var STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR");

                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Minimize(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(2, 1, -6.0));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                var testableContext =
                    TestableContinuousOptimizationContext00
                        .Get();

                double objectiveFunction(DoubleMatrix argument)
                {
                    return testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Minimize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, 1, -6.0));

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Valid input
            {
                int numberOfArguments = 10;

                var testableContext =
                    TestableContinuousOptimizationContext03
                        .Get(numberOfArguments);

                double objectiveFunction(DoubleMatrix argument)
                {
                    return testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Minimize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, numberOfArguments, 5.0));

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }

        }

        [TestMethod]
        public void MaximizeTest()
        {
            // objectiveFunction is null
            {
                string parameterName = "objectiveFunction";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Maximize(
                            objectiveFunction: null,
                            initialArgument: DoubleMatrix.Dense(1, 1, -6.0));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is null
            {
                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Maximize(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is not a row vector
            {
                var STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR");

                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Maximize(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(2, 1, -6.0));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                var testableContext =
                    TestableContinuousOptimizationContext01
                        .Get();

                double objectiveFunction(DoubleMatrix argument)
                {
                    return testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Maximize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, 1, -6.0));

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Valid input
            {
                int numberOfArguments = 10;

                var testableContext =
                    TestableContinuousOptimizationContext02
                        .Get(numberOfArguments);

                double objectiveFunction(DoubleMatrix argument)
                {
                    return testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Maximize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, numberOfArguments, 5.0));

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ParametricMaximizeTest()
        {
            // objectiveFunction is null
            {
                string parameterName = "objectiveFunction";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Maximize(
                            objectiveFunction: (Func<DoubleMatrix, DoubleMatrix, double>)null,
                            initialArgument: DoubleMatrix.Dense(1, 1, -6.0),
                            functionParameter: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is null
            {
                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Maximize<DoubleMatrix>(
                            objectiveFunction: (x, p) => Stat.Mean(x),
                            initialArgument: null,
                            functionParameter: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is not a row vector
            {
                var STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR");

                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Maximize<DoubleMatrix>(
                            objectiveFunction: (x, p) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(2, 1, -6.0),
                            functionParameter: null);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                var testableContext =
                    TestableContinuousOptimizationContext01
                        .Get();

                double objectiveFunction(DoubleMatrix argument, double parameter)
                {
                    return parameter * testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Maximize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, 1, -6.0),
                    functionParameter: 2.0);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Valid input
            {
                int numberOfArguments = 10;

                var testableContext =
                    TestableContinuousOptimizationContext02
                        .Get(numberOfArguments);

                double objectiveFunction(DoubleMatrix argument, double parameter)
                {
                    return parameter * testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Maximize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, numberOfArguments, 5.0),
                    functionParameter: 2.0);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ParametricMinimizeTest()
        {
            // objectiveFunction is null
            {
                string parameterName = "objectiveFunction";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Minimize(
                            objectiveFunction: (Func<DoubleMatrix, DoubleMatrix, double>)null,
                            initialArgument: DoubleMatrix.Dense(1, 1, -6.0),
                            functionParameter: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is null
            {
                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Minimize<DoubleMatrix>(
                            objectiveFunction: (x, p) => Stat.Mean(x),
                            initialArgument: null,
                            functionParameter: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is not a row vector
            {
                var STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR");

                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ContinuousOptimization.Minimize<DoubleMatrix>(
                            objectiveFunction: (x, p) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(2, 1, -6.0),
                            functionParameter: null);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                var testableContext =
                    TestableContinuousOptimizationContext00
                        .Get();

                double objectiveFunction(DoubleMatrix argument, double parameter)
                {
                    return parameter * testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Minimize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, 1, -3.0),
                    functionParameter: 2.0);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Valid input
            {
                int numberOfArguments = 10;

                var testableContext =
                    TestableContinuousOptimizationContext03
                        .Get(numberOfArguments);

                double objectiveFunction(DoubleMatrix argument, double parameter)
                {
                    return parameter * testableContext.Context.Performance(argument);
                };

                var actual = ContinuousOptimization.Minimize(
                    objectiveFunction: objectiveFunction,
                    initialArgument: DoubleMatrix.Dense(1, numberOfArguments, 5.0),
                    functionParameter: 2.0);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }
    }
}