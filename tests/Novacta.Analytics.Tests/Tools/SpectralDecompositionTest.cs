// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="SpectralDecomposition"/> 
    /// instances.
    /// </summary>
    static class SpectralDecompositionTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static SpectralDecompositionTest()
        {
            SpectralDecompositionTest.Accuracy = 1e-3;
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="SpectralDecomposition
        /// .Decompose(ComplexMatrix, bool, out ComplexMatrix)"/>,
        /// and its overloads have
        /// been properly implemented.
        /// </summary>
        internal static class Decompose
        {
            /// Tests that method
            /// <see cref="SpectralDecomposition
            /// .Decompose(DoubleMatrix, bool, out DoubleMatrix)"/>,
            /// has been properly implemented.
            public static void Succeed(
                TestableSpectralDecomposition<TestableDoubleMatrix, DoubleMatrix> testableSD)
            {
                var testableMatrix = testableSD.TestableMatrix;

                #region Writable

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense,
                        lowerTriangularPart: true,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: true,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense,
                        lowerTriangularPart: false,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: false,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: true,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: true,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: false,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: false,
                        out DoubleMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.Transpose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                #endregion
            }

            /// Tests that method
            /// <see cref="SpectralDecomposition
            /// .Decompose(ComplexMatrix, bool, out ComplexMatrix)"/>,
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableSpectralDecomposition<TestableComplexMatrix, ComplexMatrix> testableSD)
            {
                var testableMatrix = testableSD.TestableMatrix;

                #region Writable

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense,
                        lowerTriangularPart: true,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: true,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense,
                        lowerTriangularPart: false,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: false,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: true,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: true,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfLower,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: false,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.Decompose(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: false,
                        out ComplexMatrix actualVectors);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableSD
                            .Values,
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .VectorsIfUpper,
                        actual: actualVectors,
                        delta: SpectralDecompositionTest.Accuracy);

                    ComplexMatrixAssert.AreEqual(
                        expected: testableSD
                            .TestableMatrix.AsDense,
                        actual: actualVectors * actualValues
                            * actualVectors.ConjugateTranspose(),
                        delta: SpectralDecompositionTest.Accuracy);
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
                        SpectralDecomposition.Decompose(
                            matrix: (DoubleMatrix)null,
                            lowerTriangularPart: true,
                            out DoubleMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: (ReadOnlyDoubleMatrix)null,
                            lowerTriangularPart: true,
                            out DoubleMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: (ComplexMatrix)null,
                            lowerTriangularPart: true,
                            out ComplexMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: (ReadOnlyComplexMatrix)null,
                            lowerTriangularPart: true,
                            out ComplexMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }

            /// <summary>
            /// Tests the operation
            /// when its operand is set through a value represented by an instance
            /// that is not a square matrix.
            /// </summary>
            public static void MatrixIsNotSquare()
            {
                var STR_EXCEPT_PAR_MUST_BE_SQUARE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE");

                string parameterName = "matrix";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: DoubleMatrix.Dense(2, 3),
                            lowerTriangularPart: true,
                            out DoubleMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: DoubleMatrix.Dense(2, 3).AsReadOnly(),
                            lowerTriangularPart: true,
                            out DoubleMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: ComplexMatrix.Dense(2, 3),
                            lowerTriangularPart: true,
                            out ComplexMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.Decompose(
                            matrix: ComplexMatrix.Dense(2, 3).AsReadOnly(),
                            lowerTriangularPart: true,
                            out ComplexMatrix eigenvectors);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);
            }

        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="SpectralDecomposition
        /// .GetEigenvalues(ComplexMatrix, bool)"/>,
        /// and its overloads have
        /// been properly implemented.
        /// </summary>
        internal static class GetEigenvalues
        {
            static DoubleMatrix GetMainDiagonal(DoubleMatrix matrix)
            {
                int m = matrix.NumberOfRows;
                var mainDiagonal = DoubleMatrix.Dense(m, 1);
                for (int i = 0; i < m; i++)
                {
                    mainDiagonal[i] = matrix[i, i];
                }

                return mainDiagonal;
            }

            /// Tests that method
            /// <see cref="SpectralDecomposition
            /// .GetEigenvalues(DoubleMatrix, bool)"/>,
            /// has been properly implemented.
            public static void Succeed(
                TestableSpectralDecomposition<TestableDoubleMatrix, DoubleMatrix> testableSD)
            {
                var testableMatrix = testableSD.TestableMatrix;

                #region Writable

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense,
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense,
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                #endregion
            }

            /// Tests that method
            /// <see cref="SpectralDecomposition
            /// .GetEigenvalues(ComplexMatrix)"/>,
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableSpectralDecomposition<TestableComplexMatrix, ComplexMatrix> testableSD)
            {
                var testableMatrix = testableSD.TestableMatrix;

                #region Writable

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense,
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense,
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse,
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                #endregion

                #region ReadOnly

                // Dense, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Lower
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: true);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Dense, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsDense.AsReadOnly(),
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
                }

                // Sparse, Upper
                {
                    var actualValues = SpectralDecomposition.GetEigenvalues(
                        testableMatrix.AsSparse.AsReadOnly(),
                        lowerTriangularPart: false);

                    DoubleMatrixAssert.AreEqual(
                        expected: GetMainDiagonal(testableSD.Values),
                        actual: actualValues,
                        delta: SpectralDecompositionTest.Accuracy);
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
                        SpectralDecomposition.GetEigenvalues(
                            matrix: (DoubleMatrix)null,
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: (ReadOnlyDoubleMatrix)null,
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: (ComplexMatrix)null,
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: (ReadOnlyComplexMatrix)null,
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }

            /// <summary>
            /// Tests the operation
            /// when its operand is set through a value represented by an instance
            /// that is not a square matrix.
            /// </summary>
            public static void MatrixIsNotSquare()
            {
                var STR_EXCEPT_PAR_MUST_BE_SQUARE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE");

                string parameterName = "matrix";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: DoubleMatrix.Dense(2, 3),
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: DoubleMatrix.Dense(2, 3).AsReadOnly(),
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: ComplexMatrix.Dense(2, 3),
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        SpectralDecomposition.GetEigenvalues(
                            matrix: ComplexMatrix.Dense(2, 3).AsReadOnly(),
                            lowerTriangularPart: true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SQUARE,
                    expectedParameterName: parameterName);
            }

        }
    }
}