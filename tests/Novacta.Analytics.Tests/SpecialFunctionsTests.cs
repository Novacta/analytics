// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class SpecialFunctionsTests
    {
        [TestMethod]
        public void BinomialCoefficientTest()
        {
            // n is negative
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpecialFunctions.BinomialCoefficient(
                            n: -1,
                            k: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: "n");
            }

            // k is negative
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpecialFunctions.BinomialCoefficient(
                            n: 0,
                            k: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: "k");
            }

            // n < k
            {
                string STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER =
                    string.Format(
                            (string)Reflector.ExecuteStaticMember(
                                typeof(ImplementationServices),
                                "GetResourceString",
                                new string[] { "STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER" }),
                            "k",
                            "n");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpecialFunctions.BinomialCoefficient(
                            n: 3,
                            k: 4);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER,
                    expectedParameterName: "k");
            }

            // valid input
            {
                var n = new int[]
                    {
                        0,
                        1,
                        2,
                        10,
                        20
                    };

                var k = new int[]
                    {
                        0,
                        1,
                        2,
                        7,
                        8
                    };

                var expected = new double[]
                    {
                        1.0,
                        1.0,
                        1.0,
                        120.0,
                        125970.0
                    };

                var actual = new double[5];

                for (int i = 0; i < n.Length; i++)
                {
                    actual[i] =
                        SpecialFunctions.BinomialCoefficient(n[i], k[i]);
                }

                DoubleArrayAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: 1e-8);
            }
        }

        [TestMethod]
        public void FactorialTest()
        {
            // n is negative
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpecialFunctions.Factorial(n: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: "n");
            }

            // valid input
            {
                var x = new int[]
                    {
                        0,
                        1,
                        2,
                        3,
                        10,
                        1000
                    };

                var expected = new double[]
                    {
                        1.0,
                        1.0,
                        2.0,
                        6.0,
                        3628800.0,
                        Double.PositiveInfinity
                    };

                var actual = new double[6];

                for (int i = 0; i < x.Length; i++)
                {
                    actual[i] =
                        SpecialFunctions.Factorial(x[i]);
                }

                DoubleArrayAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: 1e-8);
            }
        }

        [TestMethod]
        public void LogGammaTest()
        {
            // x is zero
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpecialFunctions.LogGamma(x: 0.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "x");
            }

            // x is negative
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpecialFunctions.LogGamma(x: -1.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "x");
            }

            // valid input
            {
                var x = new double[]
                    {
                        1.0,
                        10.0,
                        100.0,
                        1000.0
                    };

                var expected = new double[]
                    {
                        0.0,
                        12.80182748008147,
                        359.13420536957540,
                        5905.22042320918081
                    };

                var actual = new double[4];

                for (int i = 0; i < x.Length; i++)
                {
                    actual[i] =
                        SpecialFunctions.LogGamma(x[i]);
                }

                DoubleArrayAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: 1e-9);
            }
        }
    }
}