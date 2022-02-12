// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="SingularValueDecomposition"/> 
    /// instances.
    /// </summary>
    static class SingularValueDecompositionTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static SingularValueDecompositionTest()
        {
            SingularValueDecompositionTest.Accuracy = 1e-3;
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="SingularValueDecomposition
        /// .Decompose(ComplexMatrix, out ComplexMatrix, out ComplexMatrix)"/>,
        /// and its overloads have
        /// been properly implemented.
        /// </summary>
        internal static class Decompose
        {
            /// Tests that method
            /// <see cref="SingularValueDecomposition
            /// .Decompose(DoubleMatrix, out DoubleMatrix, out DoubleMatrix)"/>,
            /// has been properly implemented.
            public static void Succeed(
                TestableSingularValueDecomposition<TestableDoubleMatrix, DoubleMatrix> testableSvd)
            {
                var testableMatrix = testableSvd.TestableMatrix;

                #region Writable

                // Dense
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsDense,
                        out DoubleMatrix actualLeftVectors,
                        out DoubleMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsSparse,
                        out DoubleMatrix actualLeftVectors,
                        out DoubleMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsDense.AsReadOnly(),
                        out DoubleMatrix actualLeftVectors,
                        out DoubleMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsSparse.AsReadOnly(),
                        out DoubleMatrix actualLeftVectors,
                        out DoubleMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion
            }

            /// Tests that method
            /// <see cref="SingularValueDecomposition
            /// .Decompose(ComplexMatrix, out ComplexMatrix, out ComplexMatrix)"/>,
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableSingularValueDecomposition<TestableComplexMatrix, ComplexMatrix> testableSvd)
            {
                var testableMatrix = testableSvd.TestableMatrix;

                #region Writable

                // Dense
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsDense,
                        out ComplexMatrix actualLeftVectors,
                        out ComplexMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsSparse,
                        out ComplexMatrix actualLeftVectors,
                        out ComplexMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsDense.AsReadOnly(),
                        out ComplexMatrix actualLeftVectors,
                        out ComplexMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.Decompose(
                        testableMatrix.AsSparse.AsReadOnly(),
                        out ComplexMatrix actualLeftVectors,
                        out ComplexMatrix actualConjugateTransposedRightVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSvd
                            .Values,
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .LeftVectors,
                        actual: actualLeftVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .ConjugateTransposedRightVectors,
                        actual: actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSvd
                            .TestableMatrix.AsDense,
                        actual: actualLeftVectors * actualValues
                            * actualConjugateTransposedRightVectors,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion
            }

            /// <summary>
            /// Tests the operation
            /// when its operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            public static void MatrixIsNull()
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.Decompose(
                            matrix: (DoubleMatrix)null,
                            out DoubleMatrix leftSingularVectors,
                            out DoubleMatrix conjugateTransposedRightSingularVactors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.Decompose(
                            matrix: (ReadOnlyDoubleMatrix)null,
                            out DoubleMatrix leftSingularVectors,
                            out DoubleMatrix conjugateTransposedRightSingularVactors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.Decompose(
                            matrix: (ComplexMatrix)null,
                            out ComplexMatrix leftSingularVectors,
                            out ComplexMatrix conjugateTransposedRightSingularVactors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.Decompose(
                            matrix: (ReadOnlyComplexMatrix)null,
                            out ComplexMatrix leftSingularVectors,
                            out ComplexMatrix conjugateTransposedRightSingularVactors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="SingularValueDecomposition
        /// .GetSingularValues(ComplexMatrix)"/>,
        /// and its overloads have
        /// been properly implemented.
        /// </summary>
        internal static class GetSingularValues
        {
            static DoubleMatrix GetMainDiagonal(DoubleMatrix matrix)
            {
                int m = matrix.NumberOfRows;
                int n = matrix.NumberOfColumns;
                int min_m_n = m < n ? m : n;
                var mainDiagonal = DoubleMatrix.Dense(min_m_n, 1);
                for (int i = 0; i < min_m_n; i++)
                {
                    mainDiagonal[i] = matrix[i, i];
                }

                return mainDiagonal;
            }

            /// Tests that method
            /// <see cref="SingularValueDecomposition
            /// .GetSingularValues(DoubleMatrix)"/>,
            /// has been properly implemented.
            public static void Succeed(
                TestableSingularValueDecomposition<TestableDoubleMatrix, DoubleMatrix> testableSvd)
            {
                var testableMatrix = testableSvd.TestableMatrix;

                #region Writable

                // Dense
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsDense);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsSparse);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsDense.AsReadOnly());

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsSparse.AsReadOnly());

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion
            }

            /// Tests that method
            /// <see cref="SingularValueDecomposition
            /// .GetSingularValues(ComplexMatrix)"/>,
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableSingularValueDecomposition<TestableComplexMatrix, ComplexMatrix> testableSvd)
            {
                var testableMatrix = testableSvd.TestableMatrix;

                #region Writable

                // Dense
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsDense);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsSparse);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsDense.AsReadOnly());

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                // Sparse
                {
                    var actualValues = SingularValueDecomposition.GetSingularValues(
                        testableMatrix.AsSparse.AsReadOnly());

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSvd.Values),
                        actual: actualValues,
                        delta: SingularValueDecompositionTest.Accuracy);
                }

                #endregion
            }

            /// <summary>
            /// Tests the operation
            /// when its operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            public static void MatrixIsNull()
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.GetSingularValues(
                            matrix: (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.GetSingularValues(
                            matrix: (ReadOnlyDoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.GetSingularValues(
                            matrix: (ComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SingularValueDecomposition.GetSingularValues(
                            matrix: (ReadOnlyComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }
        }
    }
}