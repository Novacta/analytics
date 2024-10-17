// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Data;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class NumericalDifferentiationTests
    {
        [TestMethod]
        public void NonparametricGradientTest()
        {
            // function is null
            {
                string parameterName = "function";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Gradient(
                            function: null,
                            argument: DoubleMatrix.Dense(2, 1));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // argument is null
            {
                string parameterName = "argument";

                double function(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    double f = x * x * y + Math.Exp(y);

                    return f;
                };

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Gradient(
                            function: function,
                            argument: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // valid input - Non zero arguments
            {
                double function(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    double f = x * x * y + Math.Exp(y);

                    return f;
                };

                DoubleMatrix gradient(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    DoubleMatrix g = DoubleMatrix.Dense(2, 1);
                    g[0] = 2.0 * x * y;
                    g[1] = x * x + Math.Exp(y);

                    return g;
                };

                var arg = DoubleMatrix.Dense(2, 1, [9.0, -2.1]);

                var expected =
                    gradient(
                        argument: arg);

                var actual =
                        NumericalDifferentiation.Gradient(
                            function: function,
                            argument: arg);

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // valid input - Some zero arguments
            {
                double function(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    double f = x * x * y + Math.Exp(y);

                    return f;
                };

                DoubleMatrix gradient(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    DoubleMatrix g = DoubleMatrix.Dense(2, 1);
                    g[0] = 2.0 * x * y;
                    g[1] = x * x + Math.Exp(y);

                    return g;
                };

                var arg = DoubleMatrix.Dense(2, 1, [0.0, -2.1]);

                var expected =
                    gradient(
                        argument: arg);

                var actual =
                        NumericalDifferentiation.Gradient(
                            function: function,
                            argument: arg);

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);

                arg = DoubleMatrix.Dense(2, 1, [9.0, 0.0]);

                expected =
                    gradient(
                        argument: arg);

                actual =
                        NumericalDifferentiation.Gradient(
                            function: function,
                            argument: arg);

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void NonparametricHessianTest()
        {
            // function is null
            {
                string parameterName = "function";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Hessian(
                            function: null,
                            argument: DoubleMatrix.Dense(2, 1));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // argument is null
            {
                string parameterName = "argument";

                double function(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    double f = x * x * y + Math.Exp(y);

                    return f;
                };

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Hessian(
                            function: function,
                            argument: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // valid input
            {
                double function(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    double f = x * x * y + Math.Exp(y);

                    return f;
                };

                DoubleMatrix hessian(DoubleMatrix argument)
                {
                    double x, y;
                    x = argument[0];
                    y = argument[1];

                    DoubleMatrix h = DoubleMatrix.Dense(2, 2);
                    h[0, 0] = 2.0 * y;
                    h[0, 1] = 2.0 * x;
                    h[1, 0] = h[0, 1];
                    h[1, 1] = Math.Exp(y);

                    return h;
                }

                var arg = DoubleMatrix.Dense(2, 1, [9.0, -2.1]);

                var expected =
                    hessian(
                        argument: arg);

                var actual =
                        NumericalDifferentiation.Hessian(
                            function: function,
                            argument: arg);

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ParametricGradientTest()
        {
            // function is null
            {
                string parameterName = "function";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Gradient(
                            function: null,
                            argument: EngineFailureModel.EstimatedParameters,
                            parameter: EngineFailureModel.Data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // argument is null
            {
                string parameterName = "argument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Gradient(
                            function: EngineFailureModel.LogLikelihood,
                            argument: null,
                            parameter: EngineFailureModel.Data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // valid input
            {
                var expected =
                    EngineFailureModel.LogLikelihoodGradient(
                        argument: EngineFailureModel.EstimatedParameters,
                        parameter: EngineFailureModel.Data);

                var actual =
                        NumericalDifferentiation.Gradient(
                            function: EngineFailureModel.LogLikelihood,
                            argument: EngineFailureModel.EstimatedParameters,
                            parameter: EngineFailureModel.Data);

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ParametricHessianTest()
        {
            // function is null
            {
                string parameterName = "function";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Hessian(
                            function: null,
                            argument: EngineFailureModel.EstimatedParameters,
                            parameter: EngineFailureModel.Data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // argument is null
            {
                string parameterName = "argument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NumericalDifferentiation.Hessian(
                            function: EngineFailureModel.LogLikelihood,
                            argument: null,
                            parameter: EngineFailureModel.Data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // valid input
            {
                var expected =
                    EngineFailureModel.LogLikelihoodHessian(
                        argument: EngineFailureModel.EstimatedParameters,
                        parameter: EngineFailureModel.Data);

                var actual =
                        NumericalDifferentiation.Hessian(
                            function: EngineFailureModel.LogLikelihood,
                            argument: EngineFailureModel.EstimatedParameters,
                            parameter: EngineFailureModel.Data);

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }
    }
}
