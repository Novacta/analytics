// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.GDA;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Globalization;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class CloudTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            // coordinates is null
            {
                string parameterName = "coordinates";

                DoubleMatrix coordinates = null;
                DoubleMatrix weights = DoubleMatrix.Dense(1, 1, 1.0 / 4.0);
                Basis basis = Basis.Standard(2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // weights is null
            {
                string parameterName = "weights";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = null;
                Basis basis = Basis.Standard(2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // basis is null
            {
                string parameterName = "basis";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(4, 1, 1.0 / 4.0);
                Basis basis = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
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

                string parameterName = "weights";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(1, 4);
                Basis basis = Basis.Standard(2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR,
                    expectedParameterName: parameterName);
            }

            // weights must have the number of rows of coordinates
            {
                var STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS =
                    string.Format(CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                            "coordinates");

                string parameterName = "weights";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(5, 1);
                Basis basis = Basis.Standard(2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);
            }

            // weights must have non negative entries
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE");

                string parameterName = "weights";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(4, 1, -1.0);
                Basis basis = Basis.Standard(2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);
            }

            // weights must have entries summing up to 1
            {
                var STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1 =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1");

                string parameterName = "weights";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(4, 1,
                    new double[4] { .3, .6, .2, .1 });
                Basis basis = Basis.Standard(2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: parameterName);
            }

            // basis must have dimension equal to the coordinates number of columns
            {
                var STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS =
                    string.Format(CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                            "coordinates");

                string parameterName = "basis";

                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(4, 1, 1.0 / 4.0);
                Basis basis = Basis.Standard(5);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis,
                            copyData: false);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new Cloud(
                            coordinates,
                            weights,
                            basis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: parameterName);
            }

            // Valid input and copyData is true
            {
                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(4, 1, 1.0 / 4.0);
                Basis basis = Basis.Standard(2);

                {
                    var cloud = new Cloud(
                        coordinates,
                        weights,
                        basis,
                        copyData: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: basis.GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: coordinates,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: weights,
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);

                    coordinates[0, 0] = Double.PositiveInfinity;

                    Assert.AreNotEqual(
                        notExpected: coordinates[0, 0],
                        actual: cloud.Coordinates[0, 0],
                        delta: CloudTest.Accuracy);
                }

                {
                    var cloud = new Cloud(
                        coordinates,
                        weights);

                    DoubleMatrixAssert.AreEqual(
                        expected: basis.GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: coordinates,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: weights,
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);
                }

                {
                    var cloud = new Cloud(
                        coordinates);

                    DoubleMatrixAssert.AreEqual(
                        expected: basis.GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: coordinates,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: weights,
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);
                }
            }

            // Valid input and copyData is false
            {
                DoubleMatrix coordinates = DoubleMatrix.Dense(4, 2);
                DoubleMatrix weights = DoubleMatrix.Dense(4, 1, 1.0 / 4.0);
                Basis basis = Basis.Standard(2);

                {
                    var cloud = new Cloud(
                        coordinates,
                        weights,
                        basis,
                        copyData: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: basis.GetBasisMatrix(),
                        actual: cloud.Basis.GetBasisMatrix(),
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: coordinates,
                        actual: cloud.Coordinates,
                        delta: CloudTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: weights,
                        actual: cloud.Weights,
                        delta: CloudTest.Accuracy);

                    coordinates[0, 0] = Double.PositiveInfinity;

                    Assert.AreEqual(
                        expected: coordinates[0, 0],
                        actual: cloud.Coordinates[0, 0],
                        delta: CloudTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void CoordinatesTest()
        {
            CloudTest.Coordinates.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void WeightsTest()
        {
            CloudTest.Weights.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void BasisTest()
        {
            CloudTest.Basis.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void GetPrincipalProjectionsTest()
        {
            var cloud = TestableCloud00.Get().Cloud;

            var principalProjections = cloud.GetPrincipalProjections();

            DoubleMatrixAssert.AreEqual(
                expected: cloud.Basis.GetBasisMatrix(),
                actual: principalProjections.ActiveCloud.Basis.GetBasisMatrix(),
                delta: CloudTest.Accuracy);

            DoubleMatrixAssert.AreEqual(
                expected: cloud.Coordinates,
                actual: principalProjections.ActiveCloud.Coordinates,
                delta: CloudTest.Accuracy);

            DoubleMatrixAssert.AreEqual(
                expected: cloud.Weights,
                actual: principalProjections.ActiveCloud.Weights,
                delta: CloudTest.Accuracy);
        }

        [TestMethod()]
        public void GetVariancesTest()
        {
            // supplementaryVariables is null - DoubleMatrix
            {
                string parameterName = "supplementaryVariables";

                var cloud = TestableCloud00.Get().Cloud;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        cloud.GetVariances((DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

            }

            // supplementaryVariables is null - ReadOnlyDoubleMatrix
            {
                string parameterName = "supplementaryVariables";

                var cloud = TestableCloud00.Get().Cloud;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        cloud.GetVariances((ReadOnlyDoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

            }

            // supplementaryVariables must have as number of rows the number of cloud points
            {
                var STR_EXCEPT_PAR_ROW_DIM_MUST_MATCH_INDIVIDUALS_COUNT =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ROW_DIM_MUST_MATCH_INDIVIDUALS_COUNT");

                string parameterName = "supplementaryVariables";

                var cloud = TestableCloud00.Get().Cloud;

                var supplementaryVariables = 
                    DoubleMatrix.Dense(
                        cloud.Coordinates.NumberOfRows + 1,
                        10);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        cloud.GetVariances(supplementaryVariables);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_ROW_DIM_MUST_MATCH_INDIVIDUALS_COUNT,
                    expectedParameterName: parameterName);
            }

            // Valid input - DoubleMatrix
            {
                var cloud = TestableCloud00.Get().Cloud;

                var actual = cloud.GetVariances((DoubleMatrix)cloud.Coordinates);

                var cov_sa = cloud.Covariance;
                var expected = DoubleMatrix.Dense(1, cloud.Coordinates.NumberOfColumns);
                for (int i = 0; i < expected.Count; i++)
                {
                    expected[i] = cov_sa[i, i];
                }

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: CloudTest.Accuracy);
            }

            // Valid input - ReadOnlyDoubleMatrix
            {
                var cloud = TestableCloud00.Get().Cloud;

                var actual = cloud.GetVariances(cloud.Coordinates);

                var cov_sa = cloud.Covariance;
                var expected = DoubleMatrix.Dense(1, cloud.Coordinates.NumberOfColumns);
                for (int i = 0; i < expected.Count; i++)
                {
                    expected[i] = cov_sa[i, i];
                }

                DoubleMatrixAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: CloudTest.Accuracy);
            }
        }

        [TestMethod()]
        public void MeanTest()
        {
            CloudTest.Mean.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            CloudTest.Variance.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void CovarianceTest()
        {
            CloudTest.Covariance.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void CentredTest()
        {
            CloudTest.Center.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void StandardizeTest()
        {
            CloudTest.Standardize.Succeed(
                TestableCloud00.Get());
        }

        [TestMethod()]
        public void RebaseTest()
        {
            // newBasis is null
            {
                var cloud = TestableCloud00.Get().Cloud;

                string parameterName = "newBasis";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        cloud.Rebase(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

            }

            // newBasis must have the same dimension of this instance
            {
                var STR_EXCEPT_PAR_BASES_MUST_SHARE_DIMENSION =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_BASES_MUST_SHARE_DIMENSION");

                var cloud = TestableCloud00.Get().Cloud;

                string parameterName = "newBasis";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        cloud.Rebase(Basis.Standard(cloud.Coordinates.NumberOfColumns + 1));
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_BASES_MUST_SHARE_DIMENSION,
                    expectedParameterName: parameterName);

            }

            CloudTest.Rebase.Succeed(
                TestableCloud00.Get());
        }
    }
}
