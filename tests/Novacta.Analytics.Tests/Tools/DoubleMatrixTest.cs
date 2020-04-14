// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions about <see cref="DoubleMatrix"/> and 
    /// <see cref="ReadOnlyDoubleMatrix"/> instances.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrix"/>
    static class DoubleMatrixTest
    {
        #region Initialize

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        public static Action<DoubleMatrix, DoubleMatrix> AreEqual { get; set; }

        static DoubleMatrixTest()
        {
            Accuracy = 1e-2;
            AreEqual = (expected, actual) => { DoubleMatrixAssert.AreEqual(expected, actual, Accuracy); };
        }

        #endregion

        #region Apply

        /// <summary>
        /// Provides methods to test use cases of methods
        /// for application of functions to entries
        /// of <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Apply
        {
            /// <summary>
            /// Provides methods to test the 
            /// <see cref="DoubleMatrix.InPlaceApply(Func{double, double})"/> method.
            /// </summary>
            internal static class InPlace
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="DoubleMatrix.InPlaceApply(Func{double, double})"/> method
                /// succeeds when expected.
                /// </summary>
                /// <param name="testableMatrix">The testable matrix providing the instances 
                /// on which to invoke the methods.</param>
                public static void Succeed(TestableDoubleMatrix testableMatrix)
                {
                    DoubleMatrix target, actual, expected;

                    double addOne(double x) { return x + 1.0; }
                    double subtractOne(double x) { return x - 1.0; }

                    // Dense
                    target = testableMatrix.Dense;
                    target.InPlaceApply(addOne);
                    target.InPlaceApply(subtractOne);
                    actual = target;
                    expected = testableMatrix.Dense;
                    DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                    // Sparse
                    target = testableMatrix.Sparse;
                    target.InPlaceApply(addOne);
                    target.InPlaceApply(subtractOne);
                    actual = target;
                    expected = testableMatrix.Sparse;
                    DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                    // View
                    target = testableMatrix.View;
                    target.InPlaceApply(addOne);
                    target.InPlaceApply(subtractOne);
                    actual = target;
                    expected = testableMatrix.View;
                    DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="DoubleMatrix.InPlaceApply(Func{double, double})"/> method
                /// fails when its parameter <i>func</i> is set through a value represented 
                /// by a <b>null</b> instance.
                /// </summary>
                public static void FuncIsNull(
                    TestableDoubleMatrix testableMatrix)
                {
                    string parameterName = "func";

                    // Dense
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense.InPlaceApply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Sparse
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse.InPlaceApply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // View
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View.InPlaceApply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }

            /// <summary>
            /// Provides methods to test the 
            /// <see cref="DoubleMatrix.Apply(Func{double, double})"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.Apply(Func{double, double})"/> methods.
            /// </summary>
            internal static class OutPlace
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="DoubleMatrix.Apply(Func{double, double})"/> and 
                /// <see cref="ReadOnlyDoubleMatrix.Apply(Func{double, double})"/> methods
                /// succeed when expected.
                /// </summary>
                /// <param name="testableMatrix">The testable matrix providing the instances 
                /// on which to invoke the methods.</param>
                /// <param name="func">The function to apply.</param>
                /// <param name="expected">The expected result.</param>
                public static void Succeed(TestableDoubleMatrix testableMatrix,
                    Func<double, double> func,
                    DoubleMatrixState expected)
                {
                    DoubleMatrix actual;

                    // Dense
                    actual = testableMatrix.Dense.Apply(func);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                    // Sparse
                    actual = testableMatrix.Sparse.Apply(func);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                    // View
                    actual = testableMatrix.View.Apply(func);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                    // Dense.AsReadOnly()
                    actual = testableMatrix.Dense.AsReadOnly().Apply(func);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                    // Sparse.AsReadOnly()
                    actual = testableMatrix.Sparse.AsReadOnly().Apply(func);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                    // View.AsReadOnly()
                    actual = testableMatrix.View.AsReadOnly().Apply(func);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="DoubleMatrix.Apply(Func{double, double})"/> and 
                /// <see cref="ReadOnlyDoubleMatrix.Apply(Func{double, double})"/> methods
                /// fail when parameter <i>func</i> is set through a value represented 
                /// by a <b>null</b> instance.
                /// </summary>
                public static void FuncIsNull(
                    TestableDoubleMatrix testableMatrix)
                {
                    string parameterName = "func";

                    // Dense
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense.Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Sparse
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse.Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // View
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View.Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Dense.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense.AsReadOnly().Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Sparse.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse.AsReadOnly().Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // View.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View.AsReadOnly().Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }
        }

        #endregion

        #region AsColumnMajorDenseArray

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.AsColumnMajorDenseArray()"/> method.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void AsColumnMajorDenseArray(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.AsColumnMajorDenseArray;

            double[] actual;

            // Dense
            actual = testableMatrix.Dense.AsColumnMajorDenseArray();
            DoubleArrayAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // Sparse
            actual = testableMatrix.Sparse.AsColumnMajorDenseArray();
            DoubleArrayAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // View
            actual = testableMatrix.View.AsColumnMajorDenseArray();
            DoubleArrayAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // Dense.AsReadOnly()
            actual = testableMatrix.Dense.AsReadOnly().AsColumnMajorDenseArray();
            DoubleArrayAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // Sparse.AsReadOnly()
            actual = testableMatrix.Sparse.AsReadOnly().AsColumnMajorDenseArray();
            DoubleArrayAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // View.AsReadOnly()
            actual = testableMatrix.View.AsReadOnly().AsColumnMajorDenseArray();
            DoubleArrayAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
        }

        #endregion

        #region AsReadOnly

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.AsReadOnly()"/> method.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void AsReadOnly(TestableDoubleMatrix testableMatrix)
        {
            ReadOnlyDoubleMatrix actual;

            // Dense
            actual = testableMatrix.Dense.AsReadOnly();
            DoubleMatrixAssert.AreEqual(testableMatrix.Dense, actual.matrix, DoubleMatrixTest.Accuracy);

            // Sparse
            actual = testableMatrix.Sparse.AsReadOnly();
            DoubleMatrixAssert.AreEqual(testableMatrix.Sparse, actual.matrix, DoubleMatrixTest.Accuracy);

            // View
            actual = testableMatrix.View.AsReadOnly();
            DoubleMatrixAssert.AreEqual(testableMatrix.View, actual.matrix, DoubleMatrixTest.Accuracy);
        }

        #endregion

        #region Clone

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.Clone()"/> method.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void Clone(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected;

            DoubleMatrix actual;

            // Dense
            actual = testableMatrix.Dense.Clone();
            DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

            // Sparse
            actual = testableMatrix.Sparse.Clone();
            DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

            // View
            actual = testableMatrix.View.Clone();
            DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
        }

        #endregion

        #region IMatrixPatterns

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsSymmetric"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSymmetric(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsSymmetric;

            Assert.AreEqual(expected, testableMatrix.Dense.IsSymmetric);
            Assert.AreEqual(expected, testableMatrix.View.IsSymmetric);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsSymmetric);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsSymmetric);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsSymmetric);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsSymmetric);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsSkewSymmetric"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSkewSymmetric(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsSkewSymmetric;

            Assert.AreEqual(expected, testableMatrix.Dense.IsSkewSymmetric);
            Assert.AreEqual(expected, testableMatrix.View.IsSkewSymmetric);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsSkewSymmetric);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsSkewSymmetric);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsSkewSymmetric);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsSkewSymmetric);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsVector"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsVector(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == 1
                ||
                testableMatrix.Expected.NumberOfColumns == 1;

            Assert.AreEqual(expected, testableMatrix.Dense.IsVector);
            Assert.AreEqual(expected, testableMatrix.View.IsVector);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsVector);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsVector);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsVector);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsVector);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsRowVector"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsRowVector(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfRows == 1;

            Assert.AreEqual(expected, testableMatrix.Dense.IsRowVector);
            Assert.AreEqual(expected, testableMatrix.View.IsRowVector);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsRowVector);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsRowVector);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsRowVector);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsRowVector);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsColumnVector"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsColumnVector(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfColumns == 1;

            Assert.AreEqual(expected, testableMatrix.Dense.IsColumnVector);
            Assert.AreEqual(expected, testableMatrix.View.IsColumnVector);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsColumnVector);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsColumnVector);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsColumnVector);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsColumnVector);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsScalar"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsScalar(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == 1
                &&
                testableMatrix.Expected.NumberOfColumns == 1;

            Assert.AreEqual(expected, testableMatrix.Dense.IsScalar);
            Assert.AreEqual(expected, testableMatrix.View.IsScalar);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsScalar);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsScalar);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsScalar);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsScalar);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsSquare"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSquare(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows
                ==
                testableMatrix.Expected.NumberOfColumns;

            Assert.AreEqual(expected, testableMatrix.Dense.IsSquare);
            Assert.AreEqual(expected, testableMatrix.View.IsSquare);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsSquare);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsSquare);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsSquare);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsSquare);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsDiagonal"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsDiagonal(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerTriangular
                &&
                testableMatrix.Expected.IsUpperTriangular;

            Assert.AreEqual(expected, testableMatrix.Dense.IsDiagonal);
            Assert.AreEqual(expected, testableMatrix.View.IsDiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsDiagonal);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsDiagonal);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsDiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsDiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsHessenberg"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsHessenberg(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerHessenberg
                ||
                testableMatrix.Expected.IsUpperHessenberg;

            Assert.AreEqual(expected, testableMatrix.Dense.IsHessenberg);
            Assert.AreEqual(expected, testableMatrix.View.IsHessenberg);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsHessenberg);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsHessenberg);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsHessenberg);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsHessenberg);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsLowerHessenberg"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsLowerHessenberg(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsLowerHessenberg;

            Assert.AreEqual(expected, testableMatrix.Dense.IsLowerHessenberg);
            Assert.AreEqual(expected, testableMatrix.View.IsLowerHessenberg);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsLowerHessenberg);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsLowerHessenberg);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsLowerHessenberg);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsLowerHessenberg);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsUpperHessenberg"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsUpperHessenberg(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsUpperHessenberg;

            Assert.AreEqual(expected, testableMatrix.Dense.IsUpperHessenberg);
            Assert.AreEqual(expected, testableMatrix.View.IsUpperHessenberg);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsUpperHessenberg);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsUpperHessenberg);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsUpperHessenberg);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsUpperHessenberg);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsTriangular"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsTriangular(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerTriangular
                ||
                testableMatrix.Expected.IsUpperTriangular;

            Assert.AreEqual(expected, testableMatrix.Dense.IsTriangular);
            Assert.AreEqual(expected, testableMatrix.View.IsTriangular);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsTriangular);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsTriangular);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsTriangular);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsTriangular);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsLowerTriangular"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsLowerTriangular(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsLowerTriangular;

            Assert.AreEqual(expected, testableMatrix.Dense.IsLowerTriangular);
            Assert.AreEqual(expected, testableMatrix.View.IsLowerTriangular);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsLowerTriangular);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsLowerTriangular);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsLowerTriangular);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsLowerTriangular);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsUpperTriangular"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsUpperTriangular(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsUpperTriangular;

            Assert.AreEqual(expected, testableMatrix.Dense.IsUpperTriangular);
            Assert.AreEqual(expected, testableMatrix.View.IsUpperTriangular);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsUpperTriangular);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsUpperTriangular);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsUpperTriangular);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsUpperTriangular);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsTridiagonal"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsTridiagonal(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerHessenberg
                &&
                testableMatrix.Expected.IsUpperHessenberg;

            Assert.AreEqual(expected, testableMatrix.Dense.IsTridiagonal);
            Assert.AreEqual(expected, testableMatrix.View.IsTridiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsTridiagonal);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsTridiagonal);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsTridiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsTridiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsBidiagonal"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsBidiagonal(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == testableMatrix.Expected.NumberOfColumns
                &&
                (
                    (testableMatrix.Expected.LowerBandwidth == 0
                     &&
                     testableMatrix.Expected.UpperBandwidth <= 1)
                    ||
                    (testableMatrix.Expected.LowerBandwidth <= 1
                     &&
                     testableMatrix.Expected.UpperBandwidth == 0)
                );

            Assert.AreEqual(expected, testableMatrix.Dense.IsBidiagonal);
            Assert.AreEqual(expected, testableMatrix.View.IsBidiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsBidiagonal);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsBidiagonal);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsBidiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsBidiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsLowerBidiagonal"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsLowerBidiagonal(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == testableMatrix.Expected.NumberOfColumns
                &&
                testableMatrix.Expected.LowerBandwidth <= 1
                &&
                testableMatrix.Expected.UpperBandwidth == 0;

            Assert.AreEqual(expected, testableMatrix.Dense.IsLowerBidiagonal);
            Assert.AreEqual(expected, testableMatrix.View.IsLowerBidiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsLowerBidiagonal);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsLowerBidiagonal);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsLowerBidiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsLowerBidiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsUpperBidiagonal"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsUpperBidiagonal(TestableDoubleMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == testableMatrix.Expected.NumberOfColumns
                &&
                testableMatrix.Expected.LowerBandwidth == 0
                &&
                testableMatrix.Expected.UpperBandwidth <= 1;

            Assert.AreEqual(expected, testableMatrix.Dense.IsUpperBidiagonal);
            Assert.AreEqual(expected, testableMatrix.View.IsUpperBidiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.IsUpperBidiagonal);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().IsUpperBidiagonal);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().IsUpperBidiagonal);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().IsUpperBidiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.LowerBandwidth"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void LowerBandwidth(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.LowerBandwidth;

            Assert.AreEqual(expected, testableMatrix.Dense.LowerBandwidth);
            Assert.AreEqual(expected, testableMatrix.View.LowerBandwidth);
            Assert.AreEqual(expected, testableMatrix.Sparse.LowerBandwidth);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().LowerBandwidth);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().LowerBandwidth);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().LowerBandwidth);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.UpperBandwidth"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        public static void UpperBandwidth(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.UpperBandwidth;

            Assert.AreEqual(expected, testableMatrix.Dense.UpperBandwidth);
            Assert.AreEqual(expected, testableMatrix.View.UpperBandwidth);
            Assert.AreEqual(expected, testableMatrix.Sparse.UpperBandwidth);

            Assert.AreEqual(expected, testableMatrix.Dense.AsReadOnly().UpperBandwidth);
            Assert.AreEqual(expected, testableMatrix.View.AsReadOnly().UpperBandwidth);
            Assert.AreEqual(expected, testableMatrix.Sparse.AsReadOnly().UpperBandwidth);
        }

        #endregion

        #region ITabularCollection

        /// <summary>
        /// Tests the
        /// <see cref="ITabularCollection{TValue,TCollection}.NumberOfRows" /> property for
        /// <see cref="DoubleMatrix" /> and <see cref="ReadOnlyDoubleMatrix" />
        /// instances.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the property getter.</param>
        public static void NumberOfRows(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfRows;

            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.Dense);
            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.View);
            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.Sparse);

            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.Dense.AsReadOnly());
            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.View.AsReadOnly());
            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.Sparse.AsReadOnly());
        }

        /// <summary>
        /// Tests the 
        /// <see cref="ITabularCollection{TValue,TCollection}.NumberOfColumns"/> property for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the property getter.</param>
        public static void NumberOfColumns(TestableDoubleMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfColumns;

            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.Dense);
            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.View);
            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.Sparse);

            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.Dense.AsReadOnly());
            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.View.AsReadOnly());
            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.Sparse.AsReadOnly());
        }

        /// <summary>
        /// Provides methods to test use cases of getting property 
        /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        internal static class IndexerGet
        {
            #region Input Validation

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a row index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void AnyRowIndexIsOutOfRange(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.AnyRowIndexIsOutOfRange(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Get.AnyRowIndexIsOutOfRange(testableMatrix.View);
                TabularCollectionTest.Indexer.Get.AnyRowIndexIsOutOfRange(testableMatrix.Sparse);

                TabularCollectionTest.Indexer.Get
                    .AnyRowIndexIsOutOfRange(testableMatrix.Dense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .AnyRowIndexIsOutOfRange(testableMatrix.View.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .AnyRowIndexIsOutOfRange(testableMatrix.Sparse.AsReadOnly());

                #region Special Behavior of View Implementors

                {
                    // This region contains assertions about the expected
                    // behavior of dense implementors on which getters 
                    // having at least an IndexCollection argument are 
                    // invoked while avoiding dense storage allocations.

                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    var STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX" });

                    var source = testableMatrix.Dense;

                    var parameterName = "rowIndexes";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    IndexCollection.Range(0, source.NumberOfRows),
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    avoidDenseAllocations: true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows), ":",
                                avoidDenseAllocations: true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    "",
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    "end",
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["", ":", true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["end", ":", true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);
                }

                #endregion
            }

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a column index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void AnyColumnIndexIsOutOfRange(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.AnyColumnIndexIsOutOfRange(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Get.AnyColumnIndexIsOutOfRange(testableMatrix.View);
                TabularCollectionTest.Indexer.Get.AnyColumnIndexIsOutOfRange(testableMatrix.Sparse);

                TabularCollectionTest.Indexer.Get
                    .AnyColumnIndexIsOutOfRange(testableMatrix.Dense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .AnyColumnIndexIsOutOfRange(testableMatrix.View.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .AnyColumnIndexIsOutOfRange(testableMatrix.Sparse.AsReadOnly());

                #region Special Behavior of View Implementors

                {
                    // This region contains assertions about the expected
                    // behavior of dense implementors on which getters 
                    // having at least an IndexCollection argument are 
                    // invoked while avoiding dense storage allocations.

                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    var STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX" });

                    var source = testableMatrix.Dense;

                    var parameterName = "columnIndexes";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    IndexCollection.Range(0, source.NumberOfRows - 1),
                                    IndexCollection.Range(0, source.NumberOfColumns),
                                    avoidDenseAllocations: true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", IndexCollection.Range(0, source.NumberOfColumns),
                                avoidDenseAllocations: true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    IndexCollection.Range(0, source.NumberOfRows - 1),
                                    "",
                                    true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    IndexCollection.Range(0, source.NumberOfRows - 1),
                                    "end",
                                    true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", "", true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", "end", true];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);
                }

                #endregion
            }

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when row indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void RowIndexesIsNull(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.RowIndexesIsNull(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Get.RowIndexesIsNull(testableMatrix.View);
                TabularCollectionTest.Indexer.Get.RowIndexesIsNull(testableMatrix.Sparse);

                #region Special Behavior of View Implementors

                {
                    // This region contains assertions about the expected
                    // behavior of dense implementors on which getters 
                    // having at least an IndexCollection argument are 
                    // invoked while avoiding dense storage allocations.

                    string parameterName = "rowIndexes";
                    var source = testableMatrix.Dense;

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    (IndexCollection)null,
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(IndexCollection)null, ":", true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String 

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    (string)null,
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(string)null, ":", true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                #endregion

                TabularCollectionTest.Indexer.Get
                    .RowIndexesIsNull(testableMatrix.Dense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .RowIndexesIsNull(testableMatrix.View.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .RowIndexesIsNull(testableMatrix.Sparse.AsReadOnly());
            }

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when column indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void ColumnIndexesIsNull(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.ColumnIndexesIsNull(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Get.ColumnIndexesIsNull(testableMatrix.View);
                TabularCollectionTest.Indexer.Get.ColumnIndexesIsNull(testableMatrix.Sparse);

                #region Special Behavior of View Implementors

                {
                    // This region contains assertions about the expected
                    // behavior of dense implementors on which getters 
                    // having at least an IndexCollection argument are 
                    // invoked while avoiding dense storage allocations.

                    string parameterName = "columnIndexes";
                    var source = testableMatrix.Dense;

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    (IndexCollection)null,
                                    true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", (IndexCollection)null, true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[
                                    IndexCollection.Range(0, source.NumberOfColumns - 1),
                                    (string)null,
                                    true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", (string)null, true];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                #endregion

                TabularCollectionTest.Indexer.Get
                    .ColumnIndexesIsNull(testableMatrix.Dense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .ColumnIndexesIsNull(testableMatrix.View.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .ColumnIndexesIsNull(testableMatrix.Sparse.AsReadOnly());
            }

            #endregion

            #region SubMatrix

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,int]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="Double"/>.
            /// </summary>
            /// <param name="expected">The <see cref="Double"/> expected to be returned by 
            /// the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndex">The row index to get.</param>
            /// <param name="columnIndex">The column index to get.</param>
            public static void SubMatrix(
                double expected,
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                int columnIndex)
            {
                void areEqual(double expectedValue, double actualValue)
                { Assert.AreEqual(expectedValue, actualValue, Accuracy); }

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.Dense,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.View,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.Sparse,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndex">The row index to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                IndexCollection columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,string]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndex">The row index to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                string columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,int]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndex">The column index to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                int columnIndex)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
            }

            /// <summary>
            /// Tests property
            /// <see cref="DoubleMatrix.this[IndexCollection,IndexCollection,bool]" /> by
            /// comparing the actual result of its getter method to an expected collection.
            /// </summary>
            /// <param name="areEqual">A method that verifies if its
            /// arguments can be considered having equivalent states, i.e. they
            /// represent the same matrix.</param>
            /// <param name="expected">The matrix expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            /// <param name="avoidDenseAllocations">if set to <c>true</c> avoid dense allocations.</param>
            public static void SubMatrix(
                Action<DoubleMatrix, DoubleMatrix> areEqual,
                DoubleMatrix expected,
                DoubleMatrix source,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes,
                bool avoidDenseAllocations)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes, avoidDenseAllocations];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,IndexCollection]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: false);
                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: true);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property
            /// <see cref="DoubleMatrix.this[IndexCollection,string,bool]" /> by
            /// comparing the actual result of its getter method to an expected collection.
            /// </summary>
            /// <param name="areEqual">A method that verifies if its
            /// arguments can be considered having equivalent states, i.e. they
            /// represent the same matrix.</param>
            /// <param name="expected">The matrix expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            /// <param name="avoidDenseAllocations">if set to <c>true</c> avoid dense allocations.</param>
            public static void SubMatrix(
                Action<DoubleMatrix, DoubleMatrix> areEqual,
                DoubleMatrix expected,
                DoubleMatrix source,
                IndexCollection rowIndexes,
                string columnIndexes,
                bool avoidDenseAllocations)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes, avoidDenseAllocations];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,string]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: false);
                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: true);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,int]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndex">The column index to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                int columnIndex)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
            }

            /// <summary>
            /// Tests property
            /// <see cref="DoubleMatrix.this[string,IndexCollection,bool]" /> by
            /// comparing the actual result of its getter method to an expected collection.
            /// </summary>
            /// <param name="areEqual">A method that verifies if its
            /// arguments can be considered having equivalent states, i.e. they
            /// represent the same matrix.</param>
            /// <param name="expected">The matrix expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            /// <param name="avoidDenseAllocations">if set to <c>true</c> avoid dense allocations.</param>
            public static void SubMatrix(
                Action<DoubleMatrix, DoubleMatrix> areEqual,
                DoubleMatrix expected,
                DoubleMatrix source,
                string rowIndexes,
                IndexCollection columnIndexes,
                bool avoidDenseAllocations)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes, avoidDenseAllocations];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,IndexCollection]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: false);
                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: true);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property
            /// <see cref="DoubleMatrix.this[string,string,bool]" /> by
            /// comparing the actual result of its getter method to an expected collection.
            /// </summary>
            /// <param name="areEqual">A method that verifies if its
            /// arguments can be considered having equivalent states, i.e. they
            /// represent the same matrix.</param>
            /// <param name="expected">The matrix expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            /// <param name="avoidDenseAllocations">if set to <c>true</c> avoid dense allocations.</param>
            public static void SubMatrix(
                Action<DoubleMatrix, DoubleMatrix> areEqual,
                DoubleMatrix expected,
                DoubleMatrix source,
                string rowIndexes,
                string columnIndexes,
                bool avoidDenseAllocations)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes, avoidDenseAllocations];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,string]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="DoubleMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                string columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes,
                    avoidDenseAllocations: false);
                SubMatrix(
                   areEqual: AreEqual,
                   expected: expectedState.GetDenseDoubleMatrix(),
                   source: testableMatrix.Dense,
                   rowIndexes: rowIndexes,
                   columnIndexes: columnIndexes,
                   avoidDenseAllocations: true);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetDenseDoubleMatrix(),
                    source: testableMatrix.Dense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetViewDoubleMatrix(),
                    source: testableMatrix.View.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.GetSparseDoubleMatrix(),
                    source: testableMatrix.Sparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            #endregion
        }

        /// <summary>
        /// Provides methods to test use cases of setting property 
        /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> for 
        /// <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        internal static class IndexerSet
        {
            #region Input Validation

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a row index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void AnyRowIndexIsOutOrRange(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.AnyRowIndexIsOutOrRange(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Set.AnyRowIndexIsOutOrRange(testableMatrix.View);
                TabularCollectionTest.Indexer.Set.AnyRowIndexIsOutOrRange(testableMatrix.Sparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a column index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void AnyColumnIndexIsOutOrRange(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.AnyColumnIndexIsOutOrRange(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Set.AnyColumnIndexIsOutOrRange(testableMatrix.View);
                TabularCollectionTest.Indexer.Set.AnyColumnIndexIsOutOrRange(testableMatrix.Sparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when row indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void RowIndexesIsnull(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.RowIndexesIsNull(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Set.RowIndexesIsNull(testableMatrix.View);
                TabularCollectionTest.Indexer.Set.RowIndexesIsNull(testableMatrix.Sparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when column indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void ColumnIndexesIsnull(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.ColumnIndexesIsNull(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Set.ColumnIndexesIsNull(testableMatrix.View);
                TabularCollectionTest.Indexer.Set.ColumnIndexesIsNull(testableMatrix.Sparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a sub referenced collection is set through a value 
            /// represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void CollectionValueIsNull(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.CollectionValueIsNull(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Set.CollectionValueIsNull(testableMatrix.View);
                TabularCollectionTest.Indexer.Set.CollectionValueIsNull(testableMatrix.Sparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when there is an input size mismatch: the collection to assign and 
            /// the sub-referenced source are inconsistent.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void MismatchedCollectionDimensions(TestableDoubleMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.MismatchedCollectionDimensions(testableMatrix.Dense);
                TabularCollectionTest.Indexer.Set.MismatchedCollectionDimensions(testableMatrix.View);
                TabularCollectionTest.Indexer.Set.MismatchedCollectionDimensions(testableMatrix.Sparse);
            }

            #endregion

            static readonly string STR_EXCEPT_TAB_IS_READ_ONLY =
                (string)Reflector.ExecuteStaticMember(
                    typeof(ImplementationServices),
                    "GetResourceString",
                    new string[] { "STR_EXCEPT_TAB_IS_READ_ONLY" });

            #region SubMatrix

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,int]"/> by 
            /// setting an expected <see cref="Double"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expected">The <see cref="Double"/> expected to be returned by 
            /// the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SubMatrix(
                double expected,
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                int columnIndex)
            {
                void areEqual(double expectedValue, double actualValue)
                { Assert.AreEqual(expectedValue, actualValue, Accuracy); }

                TabularCollectionTest.Indexer.Set.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.Dense,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Set.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.View,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Set.SubCollection(
                    areEqual: areEqual,
                    expected: expected,
                    source: testableMatrix.Sparse,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: areEqual,
                            expected: expected,
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: areEqual,
                            expected: expected,
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                           areEqual: areEqual,
                           expected: expected,
                           source: testableMatrix.Sparse.AsReadOnly(),
                           rowIndex: rowIndex,
                           columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                IndexCollection columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,string]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                string columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,int]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                int columnIndex)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,IndexCollection]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,string]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                           areEqual: AreEqual,
                           expected: expectedState.GetSparseDoubleMatrix(),
                           source: testableMatrix.Sparse.AsReadOnly(),
                           rowIndexes: rowIndexes,
                           columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,int]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                int columnIndex)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,IndexCollection]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                           areEqual: AreEqual,
                           expected: expectedState.GetDenseDoubleMatrix(),
                           source: testableMatrix.Dense.AsReadOnly(),
                           rowIndexes: rowIndexes,
                           columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,string]"/> by 
            /// setting an expected <see cref="DoubleMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="DoubleMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                DoubleMatrixState expectedState,
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                string columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Dense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Dense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region View

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.View,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.View,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.GetDenseDoubleMatrix(),
                       source: testableMatrix.Sparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetViewDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.GetSparseDoubleMatrix(),
                        source: testableMatrix.Sparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetDenseDoubleMatrix(),
                            source: testableMatrix.Dense.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetViewDoubleMatrix(),
                            source: testableMatrix.View.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.GetSparseDoubleMatrix(),
                            source: testableMatrix.Sparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            #endregion

            #region Source Is Value

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                IndexCollection columnIndexes)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                int rowIndex,
                string columnIndexes)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                int columnIndex)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                int columnIndex)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> 
            /// when the property value is the instance on which the setter is invoked.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SourceIsValue(
                TestableDoubleMatrix testableMatrix,
                string rowIndexes,
                string columnIndexes)
            {
                DoubleMatrix source, actual, expected;

                source = testableMatrix.Dense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.View[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.Sparse[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);
            }

            #endregion
        }

        #endregion

        #region Find

        /// <summary>
        /// Provides methods to test use cases of Find methods
        /// for <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Find
        {
            /// <summary>
            /// Tests the 
            /// <see cref="DoubleMatrix.Find(double)"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.Find(double)"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void Value(TestableDoubleMatrix testableMatrix,
                double value,
                IndexCollection expected)
            {
                IndexCollection actual;

                // Dense
                actual = testableMatrix.Dense.Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse
                actual = testableMatrix.Sparse.Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // View
                actual = testableMatrix.View.Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // Dense.AsReadOnly()
                actual = testableMatrix.Dense.AsReadOnly().Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse.AsReadOnly()
                actual = testableMatrix.Sparse.AsReadOnly().Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // View.AsReadOnly()
                actual = testableMatrix.View.AsReadOnly().Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests the 
            /// <see cref="DoubleMatrix.FindNonzero()"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.FindNonzero()"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void Nonzero(TestableDoubleMatrix testableMatrix,
                IndexCollection expected)
            {
                IndexCollection actual;

                // Dense
                actual = testableMatrix.Dense.FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse
                actual = testableMatrix.Sparse.FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // View
                actual = testableMatrix.View.FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // Dense.AsReadOnly()
                actual = testableMatrix.Dense.AsReadOnly().FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse.AsReadOnly()
                actual = testableMatrix.Sparse.AsReadOnly().FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // View.AsReadOnly()
                actual = testableMatrix.View.AsReadOnly().FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);
            }

            /// <summary>
            /// Provides methods to test the 
            /// <see cref="DoubleMatrix.FindWhile(Predicate{double})"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.FindWhile(Predicate{double})"/> methods.
            /// </summary>
            internal static class While
            {
                /// <summary>
                /// Tests the operation
                /// when its operand is set through a value represented by a <b>null</b> instance.
                /// </summary>
                public static void MatchIsNull(
                    TestableDoubleMatrix testableMatrix)
                {
                    // Dense
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense.FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // Sparse
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse.FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // View
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View.FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // Dense.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense.AsReadOnly().FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // Sparse.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse.AsReadOnly().FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // View.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View.AsReadOnly().FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                }

                /// <summary>
                /// Determines whether the specified operation terminates successfully as expected.
                /// </summary>
                /// <param name="testableMatrix">The testable matrix providing the instances 
                /// on which to invoke the methods.</param>
                /// <param name="match">The match.</param>
                /// <param name="expected">The expected result.</param>
                public static void Succeed(
                    TestableDoubleMatrix testableMatrix,
                    Predicate<double> match,
                    IndexCollection expected)
                {
                    IndexCollection actual;

                    // Dense
                    actual = testableMatrix.Dense.FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // Sparse
                    actual = testableMatrix.Sparse.FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // View
                    actual = testableMatrix.View.FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // Dense.AsReadOnly()
                    actual = testableMatrix.Dense.AsReadOnly().FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // Sparse.AsReadOnly()
                    actual = testableMatrix.Sparse.AsReadOnly().FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // View.AsReadOnly()
                    actual = testableMatrix.View.AsReadOnly().FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);
                }
            }

        }

        #endregion

        #region IList

        /// <summary>
        /// Provides methods to test the 
        /// <see cref="DoubleMatrix.CopyTo(double[], int)"/> and 
        /// <see cref="ReadOnlyDoubleMatrix.CopyTo(double[], int)"/> methods.
        /// </summary>
        internal static class CopyTo
        {
            /// <summary>
            /// Tests the method
            /// when its parameter <i>array</i> is set through a value represented by a <b>null</b> instance.
            /// </summary>
            public static void ArrayIsNull(
                TestableDoubleMatrix testableMatrix)
            {
                string parameterName = "array";

                // Dense
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Dense.CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // Sparse
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Sparse.CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // View
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.View.CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // Dense.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Dense.AsReadOnly().CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // Sparse.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Sparse.AsReadOnly().CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // View.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.View.AsReadOnly().CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

            }

            /// <summary>
            /// Tests the method
            /// when its parameter <i>arrayIndex</i> is negative.
            /// </summary>
            public static void ArrayIndexIsNegative(
                TestableDoubleMatrix testableMatrix)
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                string parameterName = "arrayIndex";

                // Dense
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Dense.CopyTo(new double[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // Sparse
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Sparse.CopyTo(new double[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // View
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.View.CopyTo(new double[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // Dense.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Dense.AsReadOnly().CopyTo(new double[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // Sparse.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Sparse.AsReadOnly().CopyTo(new double[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // View.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.View.AsReadOnly().CopyTo(new double[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the method
            /// when its parameter <i>array</i> has not enough space to
            /// store the items to copy.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix.</param>
            /// <param name="arrayLength">The length of the array.</param>
            /// <param name="arrayIndex">The index of the array at which copying begins.</param>
            public static void ArrayHasNotEnoughSpace(
                TestableDoubleMatrix testableMatrix)
            {
                string STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY" });

                int count = testableMatrix.Expected.NumberOfRows *
                    testableMatrix.Expected.NumberOfColumns;

                int arrayIndex = 1;
                int arrayLength = count - arrayIndex;
                double[] array = new double[arrayLength];

                // Dense
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Dense.CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // Sparse
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Sparse.CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // View
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.View.CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // Dense.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Dense.AsReadOnly().CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // Sparse.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.Sparse.AsReadOnly().CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // View.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.View.AsReadOnly().CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);
            }

            /// <summary>
            /// Determines whether the specified method terminates successfully as expected.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances
            /// on which to invoke the methods.</param>
            /// <param name="array">The array.</param>
            /// <param name="arrayIndex">Index of the array.</param>
            /// <param name="expected">The expected result.</param>
            /// <param name="delta">The required accuracy.</param>
            public static void Succeed(
                TestableDoubleMatrix testableMatrix,
                double[] array,
                int arrayIndex,
                double[] expected,
                double delta)
            {
                // Dense
                testableMatrix.Dense.CopyTo(array, arrayIndex);
                DoubleArrayAssert.AreEqual(expected, array, delta);

                // Sparse
                testableMatrix.Sparse.CopyTo(array, arrayIndex);
                DoubleArrayAssert.AreEqual(expected, array, delta);

                // View
                testableMatrix.View.CopyTo(array, arrayIndex);
                DoubleArrayAssert.AreEqual(expected, array, delta);

                // Dense.AsReadOnly()
                testableMatrix.Dense.AsReadOnly().CopyTo(array, arrayIndex);
                DoubleArrayAssert.AreEqual(expected, array, delta);

                // Sparse.AsReadOnly()
                testableMatrix.Sparse.AsReadOnly().CopyTo(array, arrayIndex);
                DoubleArrayAssert.AreEqual(expected, array, delta);

                // View.AsReadOnly()
                testableMatrix.View.AsReadOnly().CopyTo(array, arrayIndex);
                DoubleArrayAssert.AreEqual(expected, array, delta);
            }
        }

        #region Contains

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.Contains(double)"/> and 
        /// <see cref="ReadOnlyDoubleMatrix.Contains(double)"/> methods.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void Contains(TestableDoubleMatrix testableMatrix,
            double value,
            bool expected)
        {
            bool actual;

            // Dense
            actual = testableMatrix.Dense.Contains(value);
            Assert.AreEqual(expected, actual);

            // Sparse
            actual = testableMatrix.Sparse.Contains(value);
            Assert.AreEqual(expected, actual);

            // View
            actual = testableMatrix.View.Contains(value);
            Assert.AreEqual(expected, actual);

            // Dense.AsReadOnly()
            actual = testableMatrix.Dense.AsReadOnly().Contains(value);
            Assert.AreEqual(expected, actual);

            // Sparse.AsReadOnly()
            actual = testableMatrix.Sparse.AsReadOnly().Contains(value);
            Assert.AreEqual(expected, actual);

            // View.AsReadOnly()
            actual = testableMatrix.View.AsReadOnly().Contains(value);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetItem

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.this[int]"/> and 
        /// <see cref="ReadOnlyDoubleMatrix.this[int]"/> methods.
        /// </summary>
        internal static class GetItem
        {
            /// <summary>
            /// Tests getting property 
            /// <see cref="O:IList{double}.this"/> 
            /// when a linear index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void AnyLinearIndexIsOutOfRange(
                TestableDoubleMatrix testableMatrix)
            {
                Assert.IsNotNull(testableMatrix);

                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                string parameterName = "linearIndex";

                // Dense
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Dense[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Dense[testableMatrix.Dense.Count];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // Sparse
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Sparse[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Sparse[testableMatrix.Sparse.Count];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // View
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.View[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.View[testableMatrix.View.Count];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // Dense.AsReadOnly()
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Dense.AsReadOnly()[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Dense.AsReadOnly()[
                                testableMatrix.Dense.AsReadOnly().Count];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // Sparse.AsReadOnly()
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Sparse.AsReadOnly()[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.Sparse.AsReadOnly()[
                                testableMatrix.Sparse.AsReadOnly().Count];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // View.AsReadOnly()
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.View.AsReadOnly()[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.View.AsReadOnly()[
                                testableMatrix.View.AsReadOnly().Count];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }
            }

            /// <summary>
            /// Determines whether the specified method terminates successfully as expected.
            /// </summary>
            /// <param name="testableMatrix">
            /// The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            /// <param name = "linearIndex" >The linear index of the element to get.</param>
            /// <param name = "expected" >The expected value to get.</param>
            public static void Succeed(TestableDoubleMatrix testableMatrix,
                int linearIndex,
                double expected)
            {
                double actual;

                // Dense
                actual = testableMatrix.Dense[linearIndex];
                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                actual = ((IList<double>)testableMatrix.Dense)[linearIndex];
                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.Sparse[linearIndex];
                Assert.AreEqual(expected, actual);

                actual = ((IList<double>)testableMatrix.Sparse)[linearIndex];
                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                // View
                actual = testableMatrix.View[linearIndex];
                Assert.AreEqual(expected, actual);

                actual = ((IList<double>)testableMatrix.View)[linearIndex];
                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.Dense.AsReadOnly()[linearIndex];
                Assert.AreEqual(expected, actual);

                actual = ((IList<double>)testableMatrix.Dense.AsReadOnly())[linearIndex];
                Assert.AreEqual(expected, actual);

                // Sparse.AsReadOnly()
                actual = testableMatrix.Sparse.AsReadOnly()[linearIndex];
                Assert.AreEqual(expected, actual);

                actual = ((IList<double>)testableMatrix.Sparse.AsReadOnly())[linearIndex];
                Assert.AreEqual(expected, actual);

                // View.AsReadOnly()
                actual = testableMatrix.View.AsReadOnly()[linearIndex];
                Assert.AreEqual(expected, actual);

                actual = ((IList<double>)testableMatrix.View.AsReadOnly())[linearIndex];
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.IndexOf(double)"/> and 
        /// <see cref="ReadOnlyDoubleMatrix.IndexOf(double)"/> methods.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void IndexOf(TestableDoubleMatrix testableMatrix,
            double value,
            int expected)
        {
            int actual;

            // Dense
            actual = testableMatrix.Dense.IndexOf(value);
            Assert.AreEqual(expected, actual);

            // Sparse
            actual = testableMatrix.Sparse.IndexOf(value);
            Assert.AreEqual(expected, actual);

            // View
            actual = testableMatrix.View.IndexOf(value);
            Assert.AreEqual(expected, actual);

            // Dense.AsReadOnly()
            actual = testableMatrix.Dense.AsReadOnly().IndexOf(value);
            Assert.AreEqual(expected, actual);

            // Sparse.AsReadOnly()
            actual = testableMatrix.Sparse.AsReadOnly().IndexOf(value);
            Assert.AreEqual(expected, actual);

            // View.AsReadOnly()
            actual = testableMatrix.View.AsReadOnly().IndexOf(value);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region IsReadOnly

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.IsReadOnly"/> and 
        /// <see cref="ReadOnlyDoubleMatrix.IsReadOnly"/> properties.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void IsReadOnly(TestableDoubleMatrix testableMatrix)
        {
            bool actual;

            // Dense
            actual = testableMatrix.Dense.IsReadOnly;
            Assert.AreEqual(false, actual);

            // Sparse
            actual = testableMatrix.Sparse.IsReadOnly;
            Assert.AreEqual(false, actual);

            // View
            actual = testableMatrix.View.IsReadOnly;
            Assert.AreEqual(false, actual);

            // Dense.AsReadOnly()
            actual = testableMatrix.Dense.AsReadOnly().IsReadOnly;
            Assert.AreEqual(true, actual);

            // Sparse.AsReadOnly()
            actual = testableMatrix.Sparse.AsReadOnly().IsReadOnly;
            Assert.AreEqual(true, actual);

            // View.AsReadOnly()
            actual = testableMatrix.View.AsReadOnly().IsReadOnly;
            Assert.AreEqual(true, actual);
        }

        #endregion

        #region SetItem

        /// <summary>
        /// Tests the 
        /// <see cref="DoubleMatrix.this[int, double]"/> and 
        /// <see cref="ReadOnlyDoubleMatrix.this[int, double]"/> methods.
        /// </summary>
        internal static class SetItem
        {
            /// <summary>
            /// Tests setting property 
            /// <see cref="O:IList{double}.this"/> 
            /// when a linear index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void AnyLinearIndexIsOutOfRange(
                TestableDoubleMatrix testableMatrix)
            {
                Assert.IsNotNull(testableMatrix);

                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                string parameterName = "linearIndex";

                // Dense
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense[-1] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Dense[testableMatrix.Dense.Count] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // Sparse
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse[-1] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.Sparse[testableMatrix.Sparse.Count] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // View
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View[-1] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.View[testableMatrix.View.Count] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:IList{double}.this"/> 
            /// when a linear index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void InstanceIsReadOnly(
                TestableDoubleMatrix testableMatrix)
            {
                Assert.IsNotNull(testableMatrix);

                string expectedMessage = "Specified method is not supported.";

                // Dense.AsReadOnly()
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var target = (IList<double>)testableMatrix.Dense.AsReadOnly();
                            target[0] = 1.0;
                        },
                        expectedType: typeof(NotSupportedException),
                        expectedMessage: expectedMessage);
                }

                // Sparse.AsReadOnly()
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var target = (IList<double>)testableMatrix.Sparse.AsReadOnly();
                            target[0] = 1.0;
                        },
                        expectedType: typeof(NotSupportedException),
                        expectedMessage: expectedMessage);
                }

                // View.AsReadOnly()
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var target = (IList<double>)testableMatrix.View.AsReadOnly();
                            target[0] = 1.0;
                        },
                        expectedType: typeof(NotSupportedException),
                        expectedMessage: expectedMessage);
                }
            }

            /// <summary>
            /// Determines whether the specified method terminates successfully as expected.
            /// </summary>
            /// <param name="testableMatrix">
            /// The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            /// <param name = "linearIndex" >The linear index of the element to get.</param>
            /// <param name = "expected" >The expected value to get.</param>
            public static void Succeed(TestableDoubleMatrix testableMatrix,
                int linearIndex,
                double expected)
            {
                double actual;

                // Dense
                testableMatrix.Dense[linearIndex] = expected;
                actual = testableMatrix.Dense[linearIndex];
                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse
                testableMatrix.Sparse[linearIndex] = expected;
                actual = testableMatrix.Sparse[linearIndex];
                Assert.AreEqual(expected, actual);

                // View
                testableMatrix.View[linearIndex] = expected;
                actual = testableMatrix.View[linearIndex];
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #endregion

        #region Transpose

        /// <summary>
        /// Provides methods to test use cases of methods
        /// for matrix transposition
        /// of <see cref="DoubleMatrix"/> and <see cref="ReadOnlyDoubleMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Transpose
        {
            /// <summary>
            /// Tests the 
            /// <see cref="DoubleMatrix.InPlaceTranspose(double)"/> method.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void InPlace(TestableDoubleMatrix testableMatrix)
            {
                DoubleMatrix target, actual, expected;

                // Dense
                target = testableMatrix.Dense;
                target.InPlaceTranspose();
                target.InPlaceTranspose();
                actual = target;
                expected = testableMatrix.Dense;
                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse
                target = testableMatrix.Sparse;
                target.InPlaceTranspose();
                target.InPlaceTranspose();
                actual = target;
                expected = testableMatrix.Sparse;
                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                // View
                target = testableMatrix.View;
                target.InPlaceTranspose();
                target.InPlaceTranspose();
                actual = target;
                expected = testableMatrix.View;
                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            /// <summary>
            /// Tests the 
            /// <see cref="DoubleMatrix.Transpose()"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.Transpose()"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            /// <param name="expected">The expected result.</param>
            public static void OutPlace(TestableDoubleMatrix testableMatrix,
                DoubleMatrixState expected)
            {
                DoubleMatrix actual;

                // Dense
                actual = testableMatrix.Dense.Transpose();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.Sparse.Transpose();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // View
                actual = testableMatrix.View.Transpose();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.Dense.AsReadOnly().Transpose();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.Sparse.AsReadOnly().Transpose();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // View.AsReadOnly()
                actual = testableMatrix.View.AsReadOnly().Transpose();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        #endregion

        #region Vec

        /// <summary>
        /// Provides methods to test vectorization methods.
        /// </summary>
        internal static class Vec
        {
            /// <summary>
            /// Tests <see cref="DoubleMatrix.Vec"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.Vec"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void AllIndexes(TestableDoubleMatrix testableMatrix)
            {
                var expected = new DoubleMatrixState(
                    asColumnMajorDenseArray: testableMatrix.Expected.AsColumnMajorDenseArray,
                    numberOfRows: testableMatrix.Expected.NumberOfRows * testableMatrix.Expected.NumberOfColumns,
                    numberOfColumns: 1);

                DoubleMatrix actual;

                // Dense
                actual = testableMatrix.Dense.Vec();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.Sparse.Vec();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // View
                actual = testableMatrix.View.Vec();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.Dense.AsReadOnly().Vec();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.Sparse.AsReadOnly().Vec();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);

                // View.AsReadOnly()
                actual = testableMatrix.View.AsReadOnly().Vec();
                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
            }

            /// <summary>
            /// Tests <see cref="DoubleMatrix.Vec(IndexCollection)"/> and 
            /// <see cref="ReadOnlyDoubleMatrix.Vec(IndexCollection)"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void SpecificIndexes(TestableDoubleMatrix testableMatrix)
            {
                int expectedCount = testableMatrix.Expected.NumberOfRows *
                    testableMatrix.Expected.NumberOfColumns;

                IndexCollection linearIndexes = IndexCollection.Sequence(
                    firstIndex: 0,
                    increment: 2,
                    indexBound: expectedCount - 1);

                DoubleMatrix actual;

                // Dense
                {
                    double[] asColumnMajorDenseExpectedArray = new double[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.Dense[linearIndexes[i]];
                    }
                    var expected = new DoubleMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.Dense.Vec(linearIndexes);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }

                // Sparse
                {
                    double[] asColumnMajorDenseExpectedArray = new double[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.Sparse[linearIndexes[i]];
                    }
                    var expected = new DoubleMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.Sparse.Vec(linearIndexes);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }

                // View
                {
                    double[] asColumnMajorDenseExpectedArray = new double[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.View[linearIndexes[i]];
                    }
                    var expected = new DoubleMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.View.Vec(linearIndexes);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }

                // Dense.AsReadOnly()
                {
                    double[] asColumnMajorDenseExpectedArray = new double[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.Dense[linearIndexes[i]];
                    }
                    var expected = new DoubleMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.Dense.AsReadOnly().Vec(linearIndexes);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }

                // Sparse.AsReadOnly()
                {
                    double[] asColumnMajorDenseExpectedArray = new double[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.Sparse[linearIndexes[i]];
                    }
                    var expected = new DoubleMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.Sparse.AsReadOnly().Vec(linearIndexes);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }

                // View.AsReadOnly()
                {
                    double[] asColumnMajorDenseExpectedArray = new double[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.View[linearIndexes[i]];
                    }
                    var expected = new DoubleMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.View.AsReadOnly().Vec(linearIndexes);
                    DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                }
            }
        }

        #endregion
    }
}