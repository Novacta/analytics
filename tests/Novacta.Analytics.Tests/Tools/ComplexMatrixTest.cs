// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions about <see cref="ComplexMatrix"/> and 
    /// <see cref="ReadOnlyComplexMatrix"/> instances.
    /// </summary>
    /// <seealso cref="TestableComplexMatrix"/>
    static class ComplexMatrixTest
    {
        #region Initialize

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        public static Action<ComplexMatrix, ComplexMatrix> AreEqual { get; set; }

        //public static Action<Complex, Complex> ScalarsAreEqual { get; set; }
        static ComplexMatrixTest()
        {
            Accuracy = 1e-2;
            AreEqual = (expected, actual) => { ComplexMatrixAssert.AreEqual(expected, actual, Accuracy); };
            //ScalarsAreEqual = (expected, actual) =>
            //{
            //    if (Complex.IsNaN(expected))
            //    {
            //        Assert.IsTrue(Complex.IsNaN(actual));
            //    }
            //    else
            //    {
            //        Assert.AreEqual(expected.Real, actual.Real, Accuracy,
            //           String.Format("Unexpected real value."));

            //        Assert.AreEqual(expected.Imaginary, actual.Imaginary, Accuracy,
            //           String.Format("Unexpected imaginary value."));
            //    }
            //};
        }

        #endregion

        #region Apply

        /// <summary>
        /// Provides methods to test use cases of methods
        /// for application of functions to entries
        /// of <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Apply
        {
            /// <summary>
            /// Provides methods to test the 
            /// <see cref="ComplexMatrix.InPlaceApply(Func{Complex, Complex})"/> method.
            /// </summary>
            internal static class InPlace
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="ComplexMatrix.InPlaceApply(Func{Complex, Complex})"/> method
                /// succeeds when expected.
                /// </summary>
                /// <param name="testableMatrix">The testable matrix providing the instances 
                /// on which to invoke the methods.</param>
                public static void Succeed(TestableComplexMatrix testableMatrix)
                {
                    ComplexMatrix target, actual, expected;

                    Complex addOne(Complex x) { return x + 1.0; }
                    Complex subtractOne(Complex x) { return x - 1.0; }

                    // Dense
                    target = testableMatrix.AsDense;
                    target.InPlaceApply(addOne);
                    target.InPlaceApply(subtractOne);
                    actual = target;
                    expected = testableMatrix.AsDense;
                    ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                    // Sparse
                    target = testableMatrix.AsSparse;
                    target.InPlaceApply(addOne);
                    target.InPlaceApply(subtractOne);
                    actual = target;
                    expected = testableMatrix.AsSparse;
                    ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="ComplexMatrix.InPlaceApply(Func{Complex, Complex})"/> method
                /// fails when its parameter <i>func</i> is set through a value represented 
                /// by a <b>null</b> instance.
                /// </summary>
                public static void FuncIsNull(
                    TestableComplexMatrix testableMatrix)
                {
                    string parameterName = "func";

                    // Dense
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsDense.InPlaceApply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Sparse
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsSparse.InPlaceApply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }

            /// <summary>
            /// Provides methods to test the 
            /// <see cref="ComplexMatrix.Apply(Func{Complex, Complex})"/> and 
            /// <see cref="ReadOnlyComplexMatrix.Apply(Func{Complex, Complex})"/> methods.
            /// </summary>
            internal static class OutPlace
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="ComplexMatrix.Apply(Func{Complex, Complex})"/> and 
                /// <see cref="ReadOnlyComplexMatrix.Apply(Func{Complex, Complex})"/> methods
                /// succeed when expected.
                /// </summary>
                /// <param name="testableMatrix">The testable matrix providing the instances 
                /// on which to invoke the methods.</param>
                /// <param name="func">The function to apply.</param>
                /// <param name="expected">The expected result.</param>
                public static void Succeed(TestableComplexMatrix testableMatrix,
                    Func<Complex, Complex> func,
                    ComplexMatrixState expected)
                {
                    ComplexMatrix actual;

                    // Dense
                    actual = testableMatrix.AsDense.Apply(func);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                    // Sparse
                    actual = testableMatrix.AsSparse.Apply(func);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                    // Dense.AsReadOnly()
                    actual = testableMatrix.AsDense.AsReadOnly().Apply(func);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                    // Sparse.AsReadOnly()
                    actual = testableMatrix.AsSparse.AsReadOnly().Apply(func);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="ComplexMatrix.Apply(Func{Complex, Complex})"/> and 
                /// <see cref="ReadOnlyComplexMatrix.Apply(Func{Complex, Complex})"/> methods
                /// fail when parameter <i>func</i> is set through a value represented 
                /// by a <b>null</b> instance.
                /// </summary>
                public static void FuncIsNull(
                    TestableComplexMatrix testableMatrix)
                {
                    string parameterName = "func";

                    // Dense
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsDense.Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Sparse
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsSparse.Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Dense.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsDense.AsReadOnly().Apply(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // Sparse.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsSparse.AsReadOnly().Apply(null);
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
        /// <see cref="ComplexMatrix.AsColumnMajorDenseArray()"/> method.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void AsColumnMajorDenseArray(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.AsColumnMajorDenseArray;

            Complex[] actual;

            // Dense
            actual = testableMatrix.AsDense.AsColumnMajorDenseArray();
            ComplexArrayAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            // Sparse
            actual = testableMatrix.AsSparse.AsColumnMajorDenseArray();
            ComplexArrayAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            // Dense.AsReadOnly()
            actual = testableMatrix.AsDense.AsReadOnly().AsColumnMajorDenseArray();
            ComplexArrayAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            // Sparse.AsReadOnly()
            actual = testableMatrix.AsSparse.AsReadOnly().AsColumnMajorDenseArray();
            ComplexArrayAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
        }

        #endregion

        #region AsReadOnly

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.AsReadOnly()"/> method.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void AsReadOnly(TestableComplexMatrix testableMatrix)
        {
            ReadOnlyComplexMatrix actual;

            // Dense
            actual = testableMatrix.AsDense.AsReadOnly();
            ComplexMatrixAssert.AreEqual(testableMatrix.AsDense, actual.matrix, ComplexMatrixTest.Accuracy);

            // Sparse
            actual = testableMatrix.AsSparse.AsReadOnly();
            ComplexMatrixAssert.AreEqual(testableMatrix.AsSparse, actual.matrix, ComplexMatrixTest.Accuracy);
        }

        #endregion

        #region Clone

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.Clone()"/> method.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void Clone(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected;

            ComplexMatrix actual;

            // Dense
            actual = testableMatrix.AsDense.Clone();
            ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

            // Sparse
            actual = testableMatrix.AsSparse.Clone();
            ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
        }

        #endregion

        #region IComplexMatrixPatterns

        /// <summary>
        /// Tests the 
        /// <see cref="IComplexMatrixPatterns.IsHermitian"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsHermitian(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsHermitian;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsHermitian);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsHermitian);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsHermitian);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsHermitian);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IComplexMatrixPatterns.IsSkewHermitian"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSkewHermitian(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsSkewHermitian;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsSkewHermitian);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsSkewHermitian);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsSkewHermitian);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsSkewHermitian);
        }

        #region IMatrixPatterns

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsSymmetric"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSymmetric(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsSymmetric;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsSymmetric);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsSymmetric);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsSymmetric);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsSymmetric);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsSkewSymmetric"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSkewSymmetric(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsSkewSymmetric;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsSkewSymmetric);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsSkewSymmetric);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsSkewSymmetric);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsSkewSymmetric);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsVector"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsVector(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == 1
                ||
                testableMatrix.Expected.NumberOfColumns == 1;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsVector);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsVector);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsVector);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsVector);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsRowVector"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsRowVector(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfRows == 1;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsRowVector);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsRowVector);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsRowVector);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsRowVector);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsColumnVector"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsColumnVector(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfColumns == 1;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsColumnVector);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsColumnVector);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsColumnVector);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsColumnVector);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsScalar"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsScalar(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == 1
                &&
                testableMatrix.Expected.NumberOfColumns == 1;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsScalar);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsScalar);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsScalar);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsScalar);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsSquare"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsSquare(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows
                ==
                testableMatrix.Expected.NumberOfColumns;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsSquare);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsSquare);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsSquare);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsSquare);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsDiagonal"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsDiagonal(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerTriangular
                &&
                testableMatrix.Expected.IsUpperTriangular;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsDiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsDiagonal);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsDiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsDiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsHessenberg"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsHessenberg(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerHessenberg
                ||
                testableMatrix.Expected.IsUpperHessenberg;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsHessenberg);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsHessenberg);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsHessenberg);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsHessenberg);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsLowerHessenberg"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsLowerHessenberg(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsLowerHessenberg;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsLowerHessenberg);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsLowerHessenberg);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsLowerHessenberg);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsLowerHessenberg);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsUpperHessenberg"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsUpperHessenberg(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsUpperHessenberg;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsUpperHessenberg);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsUpperHessenberg);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsUpperHessenberg);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsUpperHessenberg);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsTriangular"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsTriangular(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerTriangular
                ||
                testableMatrix.Expected.IsUpperTriangular;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsTriangular);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsTriangular);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsTriangular);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsTriangular);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsLowerTriangular"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsLowerTriangular(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsLowerTriangular;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsLowerTriangular);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsLowerTriangular);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsLowerTriangular);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsLowerTriangular);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsUpperTriangular"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsUpperTriangular(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.IsUpperTriangular;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsUpperTriangular);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsUpperTriangular);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsUpperTriangular);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsUpperTriangular);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsTridiagonal"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsTridiagonal(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.IsLowerHessenberg
                &&
                testableMatrix.Expected.IsUpperHessenberg;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsTridiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsTridiagonal);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsTridiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsTridiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsBidiagonal"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsBidiagonal(TestableComplexMatrix testableMatrix)
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

            Assert.AreEqual(expected, testableMatrix.AsDense.IsBidiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsBidiagonal);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsBidiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsBidiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsLowerBidiagonal"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsLowerBidiagonal(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == testableMatrix.Expected.NumberOfColumns
                &&
                testableMatrix.Expected.LowerBandwidth <= 1
                &&
                testableMatrix.Expected.UpperBandwidth == 0;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsLowerBidiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsLowerBidiagonal);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsLowerBidiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsLowerBidiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.IsUpperBidiagonal"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void IsUpperBidiagonal(TestableComplexMatrix testableMatrix)
        {
            var expected =
                testableMatrix.Expected.NumberOfRows == testableMatrix.Expected.NumberOfColumns
                &&
                testableMatrix.Expected.LowerBandwidth == 0
                &&
                testableMatrix.Expected.UpperBandwidth <= 1;

            Assert.AreEqual(expected, testableMatrix.AsDense.IsUpperBidiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.IsUpperBidiagonal);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().IsUpperBidiagonal);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().IsUpperBidiagonal);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.LowerBandwidth"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void LowerBandwidth(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.LowerBandwidth;

            Assert.AreEqual(expected, testableMatrix.AsDense.LowerBandwidth);
            Assert.AreEqual(expected, testableMatrix.AsSparse.LowerBandwidth);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().LowerBandwidth);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().LowerBandwidth);
        }

        /// <summary>
        /// Tests the 
        /// <see cref="IMatrixPatterns.UpperBandwidth"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        public static void UpperBandwidth(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.UpperBandwidth;

            Assert.AreEqual(expected, testableMatrix.AsDense.UpperBandwidth);
            Assert.AreEqual(expected, testableMatrix.AsSparse.UpperBandwidth);

            Assert.AreEqual(expected, testableMatrix.AsDense.AsReadOnly().UpperBandwidth);
            Assert.AreEqual(expected, testableMatrix.AsSparse.AsReadOnly().UpperBandwidth);
        }

        #endregion

        #endregion

        #region ITabularCollection

        /// <summary>
        /// Tests the
        /// <see cref="ITabularCollection{TValue,TCollection}.NumberOfRows" /> property for
        /// <see cref="ComplexMatrix" /> and <see cref="ReadOnlyComplexMatrix" />
        /// instances.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the property getter.</param>
        public static void NumberOfRows(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfRows;

            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.AsDense);
            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.AsSparse);

            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.AsDense.AsReadOnly());
            TabularCollectionTest.NumberOfRows.Get(expected, testableMatrix.AsSparse.AsReadOnly());
        }

        /// <summary>
        /// Tests the 
        /// <see cref="ITabularCollection{TValue,TCollection}.NumberOfColumns"/> property for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the property getter.</param>
        public static void NumberOfColumns(TestableComplexMatrix testableMatrix)
        {
            var expected = testableMatrix.Expected.NumberOfColumns;

            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.AsDense);
            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.AsSparse);

            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.AsDense.AsReadOnly());
            TabularCollectionTest.NumberOfColumns.Get(expected, testableMatrix.AsSparse.AsReadOnly());
        }

        /// <summary>
        /// Provides methods to test use cases of getting property 
        /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
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
            public static void AnyRowIndexIsOutOfRange(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.AnyRowIndexIsOutOfRange(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Get.AnyRowIndexIsOutOfRange(testableMatrix.AsSparse);

                TabularCollectionTest.Indexer.Get
                    .AnyRowIndexIsOutOfRange(testableMatrix.AsDense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .AnyRowIndexIsOutOfRange(testableMatrix.AsSparse.AsReadOnly());
            }

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a column index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void AnyColumnIndexIsOutOfRange(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.AnyColumnIndexIsOutOfRange(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Get.AnyColumnIndexIsOutOfRange(testableMatrix.AsSparse);

                TabularCollectionTest.Indexer.Get
                    .AnyColumnIndexIsOutOfRange(testableMatrix.AsDense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .AnyColumnIndexIsOutOfRange(testableMatrix.AsSparse.AsReadOnly());
            }

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when row indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void RowIndexesIsNull(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.RowIndexesIsNull(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Get.RowIndexesIsNull(testableMatrix.AsSparse);

                TabularCollectionTest.Indexer.Get
                    .RowIndexesIsNull(testableMatrix.AsDense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .RowIndexesIsNull(testableMatrix.AsSparse.AsReadOnly());
            }

            /// <summary>
            /// Tests getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when column indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            public static void ColumnIndexesIsNull(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Get.ColumnIndexesIsNull(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Get.ColumnIndexesIsNull(testableMatrix.AsSparse);

                TabularCollectionTest.Indexer.Get
                    .ColumnIndexesIsNull(testableMatrix.AsDense.AsReadOnly());
                TabularCollectionTest.Indexer.Get
                    .ColumnIndexesIsNull(testableMatrix.AsSparse.AsReadOnly());
            }

            #endregion

            #region SubMatrix

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,int]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="Complex"/>.
            /// </summary>
            /// <param name="expected">The <see cref="Complex"/> expected to be returned by 
            /// the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndex">The row index to get.</param>
            /// <param name="columnIndex">The column index to get.</param>
            public static void SubMatrix(
                Complex expected,
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                int columnIndex)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                    expected: expected,
                    source: testableMatrix.AsDense,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                    expected: expected,
                    source: testableMatrix.AsSparse,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                    expected: expected,
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                    expected: expected,
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndex">The row index to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                IndexCollection columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,string]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndex">The row index to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                string columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,int]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndex">The column index to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                int columnIndex)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
            }

            /// <summary>
            /// Tests property
            /// <see cref="ComplexMatrix.this[IndexCollection,IndexCollection]" /> by
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
                Action<ComplexMatrix, ComplexMatrix> areEqual,
                ComplexMatrix expected,
                ComplexMatrix source,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,IndexCollection]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property
            /// <see cref="ComplexMatrix.this[IndexCollection,string]" /> by
            /// comparing the actual result of its getter method to an expected collection.
            /// </summary>
            /// <param name="areEqual">A method that verifies if its
            /// arguments can be considered having equivalent states, i.e. they
            /// represent the same matrix.</param>
            /// <param name="expected">The matrix expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                Action<ComplexMatrix, ComplexMatrix> areEqual,
                ComplexMatrix expected,
                ComplexMatrix source,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,string]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,int]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndex">The column index to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                int columnIndex)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
            }

            /// <summary>
            /// Tests property
            /// <see cref="ComplexMatrix.this[string,IndexCollection]" /> by
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
                Action<ComplexMatrix, ComplexMatrix> areEqual,
                ComplexMatrix expected,
                ComplexMatrix source,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,IndexCollection]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            /// <summary>
            /// Tests property
            /// <see cref="ComplexMatrix.this[string,string]" /> by
            /// comparing the actual result of its getter method to an expected collection.
            /// </summary>
            /// <param name="areEqual">A method that verifies if its
            /// arguments can be considered having equivalent states, i.e. they
            /// represent the same matrix.</param>
            /// <param name="expected">The matrix expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                Action<ComplexMatrix, ComplexMatrix> areEqual,
                ComplexMatrix expected,
                ComplexMatrix source,
                string rowIndexes,
                string columnIndexes)
            {
                Assert.IsNotNull(expected);
                Assert.IsNotNull(source);
                Assert.IsNotNull(rowIndexes);
                Assert.IsNotNull(columnIndexes);

                var actual = source[rowIndexes, columnIndexes];
                areEqual(expected, actual);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,string]"/> by 
            /// comparing the actual result of its getter method to an expected <see cref="ComplexMatrix"/>.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property getter.</param>
            /// <param name="rowIndexes">The row indexes to get.</param>
            /// <param name="columnIndexes">The column indexes to get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                string columnIndexes)
            {
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                SubMatrix(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);

                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsDense(),
                    source: testableMatrix.AsDense.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
                TabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: AreEqual,
                    expected: expectedState.AsSparse(),
                    source: testableMatrix.AsSparse.AsReadOnly(),
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            #endregion
        }

        /// <summary>
        /// Provides methods to test use cases of setting property 
        /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> for 
        /// <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
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
            public static void AnyRowIndexIsOutOrRange(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.AnyRowIndexIsOutOrRange(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Set.AnyRowIndexIsOutOrRange(testableMatrix.AsSparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a column index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void AnyColumnIndexIsOutOrRange(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.AnyColumnIndexIsOutOrRange(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Set.AnyColumnIndexIsOutOrRange(testableMatrix.AsSparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when row indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void RowIndexesIsnull(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.RowIndexesIsNull(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Set.RowIndexesIsNull(testableMatrix.AsSparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when column indexes are represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void ColumnIndexesIsnull(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.ColumnIndexesIsNull(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Set.ColumnIndexesIsNull(testableMatrix.AsSparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when a sub referenced collection is set through a value 
            /// represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void CollectionValueIsNull(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.CollectionValueIsNull(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Set.CollectionValueIsNull(testableMatrix.AsSparse);
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
            /// when there is an input size mismatch: the collection to assign and 
            /// the sub-referenced source are inconsistent.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter.</param>
            public static void MismatchedCollectionDimensions(TestableComplexMatrix testableMatrix)
            {
                TabularCollectionTest.Indexer.Set.MismatchedCollectionDimensions(testableMatrix.AsDense);
                TabularCollectionTest.Indexer.Set.MismatchedCollectionDimensions(testableMatrix.AsSparse);
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
            /// setting an expected <see cref="Complex"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expected">The <see cref="Complex"/> expected to be returned by 
            /// the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SubMatrix(
                Complex expected,
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                int columnIndex)
            {
                TabularCollectionTest.Indexer.Set.SubCollection(
                    areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                    expected: expected,
                    source: testableMatrix.AsDense,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);
                TabularCollectionTest.Indexer.Set.SubCollection(
                    areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                    expected: expected,
                    source: testableMatrix.AsSparse,
                    rowIndex: rowIndex,
                    columnIndex: columnIndex);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                            expected: expected,
                            source: testableMatrix.AsDense.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: (e, a) => ComplexAssert.AreEqual(e, a, Accuracy),
                            expected: expected,
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                IndexCollection columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[int,string]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndex">The row index to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                string columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndex: rowIndex,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndex: rowIndex,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndex: rowIndex,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,int]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                int columnIndex)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,IndexCollection]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,string]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                           expected: expectedState.AsSparse(),
                           source: testableMatrix.AsSparse.AsReadOnly(),
                           rowIndexes: rowIndexes,
                           columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,int]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndex">The column index to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                int columnIndex)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndexes: rowIndexes,
                       columnIndex: columnIndex);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndexes: rowIndexes,
                        columnIndex: columnIndex);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndex: columnIndex);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,IndexCollection]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                           areEqual: AreEqual,
                           expected: expectedState.AsDense(),
                           source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
                            rowIndexes: rowIndexes,
                            columnIndexes: columnIndexes);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: STR_EXCEPT_TAB_IS_READ_ONLY);
            }

            /// <summary>
            /// Tests property 
            /// <see cref="ITabularCollection{TValue, TCollection}.this[string,string]"/> by 
            /// setting an expected <see cref="ComplexMatrix"/> at the specified 
            /// row and column indexes and then comparing it with the actual result of getting 
            /// at the same indexes.
            /// </summary>
            /// <param name="expectedState">The state of the <see cref="ComplexMatrix"/> expected to be returned 
            /// by the property getter.</param>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the property setter and getter.</param>
            /// <param name="rowIndexes">The row indexes to set and get.</param>
            /// <param name="columnIndexes">The column indexes to set and get.</param>
            public static void SubMatrix(
                ComplexMatrixState expectedState,
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                string columnIndexes)
            {
                {
                    #region Dense

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsDense,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsDense,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion

                    #region Sparse

                    TabularCollectionTest.Indexer.Set.SubCollection(
                       areEqual: AreEqual,
                       expected: expectedState.AsDense(),
                       source: testableMatrix.AsSparse,
                       rowIndexes: rowIndexes,
                       columnIndexes: columnIndexes);
                    TabularCollectionTest.Indexer.Set.SubCollection(
                        areEqual: AreEqual,
                        expected: expectedState.AsSparse(),
                        source: testableMatrix.AsSparse,
                        rowIndexes: rowIndexes,
                        columnIndexes: columnIndexes);

                    #endregion
                }

                ExceptionAssert.Throw(
                    () =>
                    {
                        TabularCollectionTest.Indexer.Set.SubCollection(
                            areEqual: AreEqual,
                            expected: expectedState.AsDense(),
                            source: testableMatrix.AsDense.AsReadOnly(),
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
                            expected: expectedState.AsSparse(),
                            source: testableMatrix.AsSparse.AsReadOnly(),
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
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                IndexCollection columnIndexes)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndex, columnIndexes];
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
                TestableComplexMatrix testableMatrix,
                int rowIndex,
                string columnIndexes)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndex, columnIndexes];
                expected = source;
                source[rowIndex, columnIndexes] = expected;
                actual = source[rowIndex, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndex, columnIndexes];
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
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                int columnIndex)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndexes, columnIndex];
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
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                IndexCollection columnIndexes)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndexes, columnIndexes];
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
                TestableComplexMatrix testableMatrix,
                IndexCollection rowIndexes,
                string columnIndexes)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndexes, columnIndexes];
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
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                int columnIndex)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndexes, columnIndex];
                expected = source;
                source[rowIndexes, columnIndex] = expected;
                actual = source[rowIndexes, columnIndex];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndexes, columnIndex];
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
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                IndexCollection columnIndexes)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndexes, columnIndexes];
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
                TestableComplexMatrix testableMatrix,
                string rowIndexes,
                string columnIndexes)
            {
                ComplexMatrix source, actual, expected;

                source = testableMatrix.AsDense[rowIndexes, columnIndexes];
                expected = source;
                source[rowIndexes, columnIndexes] = expected;
                actual = source[rowIndexes, columnIndexes];
                AreEqual(expected, actual);

                source = testableMatrix.AsSparse[rowIndexes, columnIndexes];
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
        /// for <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Find
        {
            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.Find(Complex)"/> and 
            /// <see cref="ReadOnlyComplexMatrix.Find(Complex)"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void Value(TestableComplexMatrix testableMatrix,
                Complex value,
                IndexCollection expected)
            {
                IndexCollection actual;

                // Dense
                actual = testableMatrix.AsDense.Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse
                actual = testableMatrix.AsSparse.Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly().Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly().Find(value);
                IndexCollectionAssert.AreEqual(expected, actual);
            }

            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.FindNonzero()"/> and 
            /// <see cref="ReadOnlyComplexMatrix.FindNonzero()"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void Nonzero(TestableComplexMatrix testableMatrix,
                IndexCollection expected)
            {
                IndexCollection actual;

                // Dense
                actual = testableMatrix.AsDense.FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse
                actual = testableMatrix.AsSparse.FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly().FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly().FindNonzero();
                IndexCollectionAssert.AreEqual(expected, actual);
            }

            /// <summary>
            /// Provides methods to test the 
            /// <see cref="ComplexMatrix.FindWhile(Predicate{Complex})"/> and 
            /// <see cref="ReadOnlyComplexMatrix.FindWhile(Predicate{Complex})"/> methods.
            /// </summary>
            internal static class While
            {
                /// <summary>
                /// Tests the operation
                /// when its operand is set through a value represented by a <b>null</b> instance.
                /// </summary>
                public static void MatchIsNull(
                    TestableComplexMatrix testableMatrix)
                {
                    // Dense
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsDense.FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // Sparse
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsSparse.FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // Dense.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsDense.AsReadOnly().FindWhile(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "match");

                    // Sparse.AsReadOnly()
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsSparse.AsReadOnly().FindWhile(null);
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
                    TestableComplexMatrix testableMatrix,
                    Predicate<Complex> match,
                    IndexCollection expected)
                {
                    IndexCollection actual;

                    // Dense
                    actual = testableMatrix.AsDense.FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // Sparse
                    actual = testableMatrix.AsSparse.FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // Dense.AsReadOnly()
                    actual = testableMatrix.AsDense.AsReadOnly().FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);

                    // Sparse.AsReadOnly()
                    actual = testableMatrix.AsSparse.AsReadOnly().FindWhile(match);
                    IndexCollectionAssert.AreEqual(expected, actual);
                }
            }

        }

        #endregion

        #region IList

        /// <summary>
        /// Provides methods to test the 
        /// <see cref="ComplexMatrix.CopyTo(Complex[], int)"/> and 
        /// <see cref="ReadOnlyComplexMatrix.CopyTo(Complex[], int)"/> methods.
        /// </summary>
        internal static class CopyTo
        {
            /// <summary>
            /// Tests the method
            /// when its parameter <i>array</i> is set through a value represented by a <b>null</b> instance.
            /// </summary>
            public static void ArrayIsNull(
                TestableComplexMatrix testableMatrix)
            {
                string parameterName = "array";

                // Dense
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsDense.CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // Sparse
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsSparse.CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // Dense.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsDense.AsReadOnly().CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                // Sparse.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsSparse.AsReadOnly().CopyTo(null, 0);
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
                TestableComplexMatrix testableMatrix)
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
                        testableMatrix.AsDense.CopyTo(new Complex[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // Sparse
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsSparse.CopyTo(new Complex[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // Dense.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsDense.AsReadOnly().CopyTo(new Complex[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);

                // Sparse.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsSparse.AsReadOnly().CopyTo(new Complex[1], -1);
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
                TestableComplexMatrix testableMatrix)
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
                Complex[] array = new Complex[arrayLength];

                // Dense
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsDense.CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // Sparse
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsSparse.CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // Dense.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsDense.AsReadOnly().CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);

                // Sparse.AsReadOnly()
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        testableMatrix.AsSparse.AsReadOnly().CopyTo(array, arrayIndex);
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
                TestableComplexMatrix testableMatrix,
                Complex[] array,
                int arrayIndex,
                Complex[] expected,
                double delta)
            {
                // Dense
                testableMatrix.AsDense.CopyTo(array, arrayIndex);
                ComplexArrayAssert.AreEqual(expected, array, delta);

                // Sparse
                testableMatrix.AsSparse.CopyTo(array, arrayIndex);
                ComplexArrayAssert.AreEqual(expected, array, delta);

                // Dense.AsReadOnly()
                testableMatrix.AsDense.AsReadOnly().CopyTo(array, arrayIndex);
                ComplexArrayAssert.AreEqual(expected, array, delta);

                // Sparse.AsReadOnly()
                testableMatrix.AsSparse.AsReadOnly().CopyTo(array, arrayIndex);
                ComplexArrayAssert.AreEqual(expected, array, delta);
            }
        }

        #region Contains

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.Contains(Complex)"/> and 
        /// <see cref="ReadOnlyComplexMatrix.Contains(Complex)"/> methods.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void Contains(TestableComplexMatrix testableMatrix,
            Complex value,
            bool expected)
        {
            bool actual;

            // Dense
            actual = testableMatrix.AsDense.Contains(value);
            Assert.AreEqual(expected, actual);

            // Sparse
            actual = testableMatrix.AsSparse.Contains(value);
            Assert.AreEqual(expected, actual);

            // Dense.AsReadOnly()
            actual = testableMatrix.AsDense.AsReadOnly().Contains(value);
            Assert.AreEqual(expected, actual);

            // Sparse.AsReadOnly()
            actual = testableMatrix.AsSparse.AsReadOnly().Contains(value);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetItem

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.this[int]"/> and 
        /// <see cref="ReadOnlyComplexMatrix.this[int]"/> methods.
        /// </summary>
        internal static class GetItem
        {
            /// <summary>
            /// Tests getting property 
            /// <see cref="O:IList{Complex}.this"/> 
            /// when a linear index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void AnyLinearIndexIsOutOfRange(
                TestableComplexMatrix testableMatrix)
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
                            var sub = testableMatrix.AsDense[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.AsDense[testableMatrix.AsDense.Count];
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
                            var sub = testableMatrix.AsSparse[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.AsSparse[testableMatrix.AsSparse.Count];
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
                            var sub = testableMatrix.AsDense.AsReadOnly()[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.AsDense.AsReadOnly()[
                                testableMatrix.AsDense.AsReadOnly().Count];
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
                            var sub = testableMatrix.AsSparse.AsReadOnly()[-1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub = testableMatrix.AsSparse.AsReadOnly()[
                                testableMatrix.AsSparse.AsReadOnly().Count];
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
            public static void Succeed(TestableComplexMatrix testableMatrix,
                int linearIndex,
                Complex expected)
            {
                Complex actual;

                // Dense
                actual = testableMatrix.AsDense[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                actual = ((IList<Complex>)testableMatrix.AsDense)[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                // Sparse
                actual = testableMatrix.AsSparse[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                actual = ((IList<Complex>)testableMatrix.AsSparse)[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly()[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                actual = ((IList<Complex>)testableMatrix.AsDense.AsReadOnly())[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly()[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                actual = ((IList<Complex>)testableMatrix.AsSparse.AsReadOnly())[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);
            }
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.IndexOf(Complex)"/> and 
        /// <see cref="ReadOnlyComplexMatrix.IndexOf(Complex)"/> methods.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void IndexOf(TestableComplexMatrix testableMatrix,
            Complex value,
            int expected)
        {
            int actual;

            // Dense
            actual = testableMatrix.AsDense.IndexOf(value);
            Assert.AreEqual(expected, actual);

            // Sparse
            actual = testableMatrix.AsSparse.IndexOf(value);
            Assert.AreEqual(expected, actual);

            // Dense.AsReadOnly()
            actual = testableMatrix.AsDense.AsReadOnly().IndexOf(value);
            Assert.AreEqual(expected, actual);

            // Sparse.AsReadOnly()
            actual = testableMatrix.AsSparse.AsReadOnly().IndexOf(value);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region IsReadOnly

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.IsReadOnly"/> and 
        /// <see cref="ReadOnlyComplexMatrix.IsReadOnly"/> properties.
        /// </summary>
        /// <param name="testableMatrix">The testable matrix providing the instances 
        /// on which to invoke the methods.</param>
        public static void IsReadOnly(TestableComplexMatrix testableMatrix)
        {
            bool actual;

            // Dense
            actual = testableMatrix.AsDense.IsReadOnly;
            Assert.AreEqual(false, actual);

            // Sparse
            actual = testableMatrix.AsSparse.IsReadOnly;
            Assert.AreEqual(false, actual);

            // Dense.AsReadOnly()
            actual = testableMatrix.AsDense.AsReadOnly().IsReadOnly;
            Assert.AreEqual(true, actual);

            // Sparse.AsReadOnly()
            actual = testableMatrix.AsSparse.AsReadOnly().IsReadOnly;
            Assert.AreEqual(true, actual);
        }

        #endregion

        #region SetItem

        /// <summary>
        /// Tests the 
        /// <see cref="ComplexMatrix.this[int, Complex]"/> and 
        /// <see cref="ReadOnlyComplexMatrix.this[int, Complex]"/> methods.
        /// </summary>
        internal static class SetItem
        {
            /// <summary>
            /// Tests setting property 
            /// <see cref="O:IList{Complex}.this"/> 
            /// when a linear index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void AnyLinearIndexIsOutOfRange(
                TestableComplexMatrix testableMatrix)
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
                            testableMatrix.AsDense[-1] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsDense[testableMatrix.AsDense.Count] = 1.0;
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
                            testableMatrix.AsSparse[-1] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableMatrix.AsSparse[testableMatrix.AsSparse.Count] = 1.0;
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }
            }

            /// <summary>
            /// Tests setting property 
            /// <see cref="O:IList{Complex}.this"/> 
            /// when a linear index is out of range.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void InstanceIsReadOnly(
                TestableComplexMatrix testableMatrix)
            {
                Assert.IsNotNull(testableMatrix);

                string expectedMessage = "Specified method is not supported.";

                // Dense.AsReadOnly()
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var target = (IList<Complex>)testableMatrix.AsDense.AsReadOnly();
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
                            var target = (IList<Complex>)testableMatrix.AsSparse.AsReadOnly();
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
            public static void Succeed(TestableComplexMatrix testableMatrix,
                int linearIndex,
                Complex expected)
            {
                Complex actual;

                // Dense
                testableMatrix.AsDense[linearIndex] = expected;
                actual = testableMatrix.AsDense[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);

                // Sparse
                testableMatrix.AsSparse[linearIndex] = expected;
                actual = testableMatrix.AsSparse[linearIndex];
                ComplexAssert.AreEqual(expected, actual, Accuracy);
            }
        }

        #endregion

        #endregion

        #region Transpose

        /// <summary>
        /// Provides methods to test use cases of methods
        /// for matrix transposition
        /// of <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Transpose
        {
            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.InPlaceTranspose(Complex)"/> method.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void InPlace(TestableComplexMatrix testableMatrix)
            {
                ComplexMatrix target, actual, expected;

                // Dense
                target = testableMatrix.AsDense;
                target.InPlaceTranspose();
                target.InPlaceTranspose();
                actual = target;
                expected = testableMatrix.AsDense;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                target = testableMatrix.AsSparse;
                target.InPlaceTranspose();
                target.InPlaceTranspose();
                actual = target;
                expected = testableMatrix.AsSparse;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.Transpose()"/> and 
            /// <see cref="ReadOnlyComplexMatrix.Transpose()"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            /// <param name="expected">The expected result.</param>
            public static void OutPlace(TestableComplexMatrix testableMatrix,
                ComplexMatrixState expected)
            {
                ComplexMatrix actual;

                // Dense
                actual = testableMatrix.AsDense.Transpose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.AsSparse.Transpose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly().Transpose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly().Transpose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
            }
        }

        #endregion

        #region ConjugateTranspose

        /// <summary>
        /// Provides methods to test use cases of methods
        /// for matrix transposition
        /// of <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        internal static class ConjugateTranspose
        {
            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.InPlaceConjugateTranspose(Complex)"/> method.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void InPlace(TestableComplexMatrix testableMatrix)
            {
                ComplexMatrix target, actual, expected;

                // Dense
                target = testableMatrix.AsDense;
                target.InPlaceConjugateTranspose();
                target.InPlaceConjugateTranspose();
                actual = target;
                expected = testableMatrix.AsDense;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                target = testableMatrix.AsSparse;
                target.InPlaceConjugateTranspose();
                target.InPlaceConjugateTranspose();
                actual = target;
                expected = testableMatrix.AsSparse;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.ConjugateTranspose()"/> and 
            /// <see cref="ReadOnlyComplexMatrix.ConjugateTranspose()"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            /// <param name="expected">The expected result.</param>
            public static void OutPlace(TestableComplexMatrix testableMatrix,
                ComplexMatrixState expected)
            {
                ComplexMatrix actual;

                // Dense
                actual = testableMatrix.AsDense.ConjugateTranspose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.AsSparse.ConjugateTranspose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly().ConjugateTranspose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly().ConjugateTranspose();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
            }
        }

        #endregion

        #region Conjugate

        /// <summary>
        /// Provides methods to test use cases of methods
        /// for matrix transposition
        /// of <see cref="ComplexMatrix"/> and <see cref="ReadOnlyComplexMatrix"/> 
        /// instances.
        /// </summary>
        internal static class Conjugate
        {
            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.InPlaceConjugate(Complex)"/> method.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void InPlace(TestableComplexMatrix testableMatrix)
            {
                ComplexMatrix target, actual, expected;

                // Dense
                target = testableMatrix.AsDense;
                target.InPlaceConjugate();
                target.InPlaceConjugate();
                actual = target;
                expected = testableMatrix.AsDense;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                target = testableMatrix.AsSparse;
                target.InPlaceConjugate();
                target.InPlaceConjugate();
                actual = target;
                expected = testableMatrix.AsSparse;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            /// <summary>
            /// Tests the 
            /// <see cref="ComplexMatrix.Conjugate()"/> and 
            /// <see cref="ReadOnlyComplexMatrix.Conjugate()"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            /// <param name="expected">The expected result.</param>
            public static void OutPlace(TestableComplexMatrix testableMatrix,
                ComplexMatrixState expected)
            {
                ComplexMatrix actual;

                // Dense
                actual = testableMatrix.AsDense.Conjugate();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.AsSparse.Conjugate();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly().Conjugate();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly().Conjugate();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
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
            /// Tests <see cref="ComplexMatrix.Vec"/> and 
            /// <see cref="ReadOnlyComplexMatrix.Vec"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void AllIndexes(TestableComplexMatrix testableMatrix)
            {
                var expected = new ComplexMatrixState(
                    asColumnMajorDenseArray: testableMatrix.Expected.AsColumnMajorDenseArray,
                    numberOfRows: testableMatrix.Expected.NumberOfRows * testableMatrix.Expected.NumberOfColumns,
                    numberOfColumns: 1);

                ComplexMatrix actual;

                // Dense
                actual = testableMatrix.AsDense.Vec();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse
                actual = testableMatrix.AsSparse.Vec();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Dense.AsReadOnly()
                actual = testableMatrix.AsDense.AsReadOnly().Vec();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);

                // Sparse.AsReadOnly()
                actual = testableMatrix.AsSparse.AsReadOnly().Vec();
                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
            }

            /// <summary>
            /// Tests <see cref="ComplexMatrix.Vec(IndexCollection)"/> and 
            /// <see cref="ReadOnlyComplexMatrix.Vec(IndexCollection)"/> methods.
            /// </summary>
            /// <param name="testableMatrix">The testable matrix providing the instances 
            /// on which to invoke the methods.</param>
            public static void SpecificIndexes(TestableComplexMatrix testableMatrix)
            {
                int expectedCount = testableMatrix.Expected.NumberOfRows *
                    testableMatrix.Expected.NumberOfColumns;

                IndexCollection linearIndexes = IndexCollection.Sequence(
                    firstIndex: 0,
                    increment: 2,
                    indexBound: expectedCount - 1);

                ComplexMatrix actual;

                // Dense
                {
                    Complex[] asColumnMajorDenseExpectedArray = new Complex[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.AsDense[linearIndexes[i]];
                    }
                    var expected = new ComplexMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.AsDense.Vec(linearIndexes);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
                }

                // Sparse
                {
                    Complex[] asColumnMajorDenseExpectedArray = new Complex[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.AsSparse[linearIndexes[i]];
                    }
                    var expected = new ComplexMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.AsSparse.Vec(linearIndexes);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
                }

                // Dense.AsReadOnly()
                {
                    Complex[] asColumnMajorDenseExpectedArray = new Complex[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.AsDense[linearIndexes[i]];
                    }
                    var expected = new ComplexMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.AsDense.AsReadOnly().Vec(linearIndexes);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
                }

                // Sparse.AsReadOnly()
                {
                    Complex[] asColumnMajorDenseExpectedArray = new Complex[linearIndexes.Count];
                    for (int i = 0; i < linearIndexes.Count; i++)
                    {
                        asColumnMajorDenseExpectedArray[i] = testableMatrix.AsSparse[linearIndexes[i]];
                    }
                    var expected = new ComplexMatrixState(
                        asColumnMajorDenseArray: asColumnMajorDenseExpectedArray,
                        numberOfRows: linearIndexes.Count,
                        numberOfColumns: 1);

                    actual = testableMatrix.AsSparse.AsReadOnly().Vec(linearIndexes);
                    ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
                }
            }
        }

        #endregion
    }
}