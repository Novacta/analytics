// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Advanced;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.GDA;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Globalization;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class PrincipalComponentsTests
    {
        [TestMethod()]
        public void AnalyzeTest()
        {
            // data is null
            {
                string parameterName = "data";

                DoubleMatrix data = null;
                DoubleMatrix individualWeights = DoubleMatrix.Dense(1, 1, 1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // individualWeights is null
            {
                string parameterName = "individualWeights";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = null;
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // variableCoefficients is null
            {
                string parameterName = "variableCoefficients";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1, 1.0 / 4.0); ;
                DoubleMatrix variableCoefficients = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // weights is not a column vector
            {
                var STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR");

                string parameterName = "individualWeights";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(1, 4, 1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR,
                    expectedParameterName: parameterName);
            }

            // individualWeights must have the number of rows of data
            {
                var STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS =
                    string.Format(CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                            "data");

                string parameterName = "individualWeights";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(5, 1, 1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);
            }

            // individualWeights must have non negative entries
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE");

                string parameterName = "individualWeights";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1, -1.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);
            }

            // individualWeights must have entries summing up to 1
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1 =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1");

                string parameterName = "individualWeights";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                    [.3, .6, .2, .1]);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: parameterName);

            }

            // variableCoefficients must be a row vector
            {
                var STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR");

                string parameterName = "variableCoefficients";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                    1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    2, 1, [9, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR,
                    expectedParameterName: parameterName);
            }

            // variableCoefficients must have the number of columns of data
            {
                var STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS =
                    string.Format(CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "data");

                string parameterName = "variableCoefficients";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                    1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 3, [9, 4, 5]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS,
                    expectedParameterName: parameterName);
            }

            // variableCoefficients must have positive entries
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE");

                string parameterName = "variableCoefficients";

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                    1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [9, 0]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // SVD cannot be executed or does not converge
            {
                var STR_EXCEPT_SVD_ERRORS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_SVD_ERRORS");

                DoubleMatrix data = DoubleMatrix.Dense(2, 2,
                    [Double.NegativeInfinity, 0, 0, 1]);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(2, 1,
                    1.0 / 2.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [1, 1]);

                ExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_SVD_ERRORS);
            }

            // No positive principal variances
            {
                var STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES");

                DoubleMatrix data = DoubleMatrix.Dense(4, 2);
                DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                    1.0 / 4.0);
                DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                    1, 2, [1, 1]);

                ExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES);

                ExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES);

                ExceptionAssert.Throw(
                    () =>
                    {
                        PrincipalComponents.Analyze(
                            data);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES);
            }

            // Valid input 
            {
                {
                    DoubleMatrix data = DoubleMatrix.Dense(4, 2,
                        [1, 2, 3, 4, 5, 6, 7, 0]);
                    DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                        1.0 / 4.0);
                    DoubleMatrix variableCoefficients = DoubleMatrix.Dense(
                        1, 2, [9, 4]);

                    var principalComponents =
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights,
                            variableCoefficients);

                    var cloud = principalComponents.ActiveCloud;

                    DoubleMatrixAssert.AreEqual(
                        expected: new Basis(
                            DoubleMatrix.Dense(2, 2, [3, 0, 0, 2]))
                                .GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: data,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: individualWeights,
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);

                    data[0, 0] = Double.PositiveInfinity;

                    Assert.AreNotEqual(
                        notExpected: data[0, 0],
                        actual: cloud.Coordinates[0, 0],
                        delta: CloudTest.Accuracy);
                }

                {
                    DoubleMatrix data = DoubleMatrix.Dense(4, 2,
                        [1, 2, 3, 4, 5, 6, 7, 0]);
                    DoubleMatrix individualWeights = DoubleMatrix.Dense(4, 1,
                        1.0 / 4.0);

                    var principalComponents =
                        PrincipalComponents.Analyze(
                            data,
                            individualWeights);

                    var cloud = principalComponents.ActiveCloud;

                    DoubleMatrixAssert.AreEqual(
                        expected: Basis.Standard(2)
                                .GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: data,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: individualWeights,
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);

                    data[0, 0] = Double.PositiveInfinity;

                    Assert.AreNotEqual(
                        notExpected: data[0, 0],
                        actual: cloud.Coordinates[0, 0],
                        delta: CloudTest.Accuracy);
                }

                {
                    DoubleMatrix data = DoubleMatrix.Dense(4, 2,
                        [1, 2, 3, 4, 5, 6, 7, 0]);

                    var principalComponents =
                        PrincipalComponents.Analyze(
                            data);

                    var cloud = principalComponents.ActiveCloud;

                    DoubleMatrixAssert.AreEqual(
                        expected: Basis.Standard(2)
                                .GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: data,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: DoubleMatrix.Dense(4, 1,
                            1.0 / 4.0),
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);

                    data[0, 0] = Double.PositiveInfinity;

                    Assert.AreNotEqual(
                        notExpected: data[0, 0],
                        actual: cloud.Coordinates[0, 0],
                        delta: CloudTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void ActiveCloudTest()
        {
            PrincipalProjectionsTest.ActiveCloud.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void ContributionsTest()
        {
            PrincipalProjectionsTest.Contributions.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void CoordinatesTest()
        {
            PrincipalProjectionsTest.Coordinates.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void CorrelationsTest()
        {
            PrincipalProjectionsTest.Correlations.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void DirectionsTest()
        {
            PrincipalProjectionsTest.Directions.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void RegressionCoefficientsTest()
        {
            PrincipalProjectionsTest.RegressionCoefficients.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void RepresentationQualitiesTest()
        {
            PrincipalProjectionsTest.RepresentationQualities.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }

        [TestMethod()]
        public void VariancesTest()
        {
            PrincipalProjectionsTest.Variances.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: true));
        }
    }
}
