// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Interop;
using System;
using System.Diagnostics;
using System.Numerics;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Provides methods to compute the Spectral Decomposition
    /// of symmetric real or Hermitian complex matrices.
    /// </summary>
    /// <remarks>
    /// <para id='matrix'>
    /// Let <latex mode="inline">A</latex> be a normal matrix, i.e., it 
    /// satisfies 
    /// <latex mode="display">
    /// A^{\dagger}\,A = A\,A^{\dagger},
    /// </latex>
    /// where <latex>X^\dagger</latex> returns the conjugate transpose of 
    /// <latex>X</latex>.
    /// </para>
    /// <para>
    /// The Spectral Decomposition of <latex>A</latex> is a factorization 
    /// having the following form:
    /// <latex mode='display'>A = U \Sigma U^{\dagger},</latex>
    /// where <latex>U</latex> is a orthonormal  
    /// matrix whose columns are eigenvectors of <latex>A</latex>, and
    /// <latex>\Sigma</latex> is a diagonal matrix, whose main diagonal 
    /// entries correspond to the eigenvalues of <latex>A</latex>.
    /// </para>
    /// <para>
    /// Method <see cref="Decompose(ComplexMatrix, bool, out ComplexMatrix)"/>
    /// returns matrices <latex>\Sigma</latex> and <latex>U</latex> when
    /// <latex>A</latex> is a Hermitian complex matrix, while
    /// <see cref="Decompose(DoubleMatrix, bool, out DoubleMatrix)"/> returns
    /// the same matrices for a symmetric real matrix.
    /// </para>
    /// <para>
    /// Method <see cref="GetEigenvalues(DoubleMatrix, bool)"/> and its 
    /// overloads return the eigenvalues involved in a spectral decomposition,
    /// without computing the corresponding eigenvectors.
    /// </para>
    /// </remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Normal_matrix"/>
    public static class SpectralDecomposition
    {
        #region Symmetric real matrices

        /// <summary>
        /// Computes eigenvalues and eigenvectors of the 
        /// specified symmetric real matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix containing the lower or upper triangular part of 
        /// the matrix whose spectral decomposition must be computed.
        /// </param>
        /// <param name="lowerTriangularPart">
        /// <c>true</c> if <paramref name="matrix"/> contains the lower
        /// triangular part of the matrix to be decomposed;
        /// <c>false</c> if <paramref name="matrix"/> contains 
        /// its upper triangular part.
        /// </param>
        /// <param name="eigenvectors">
        /// A matrix whose columns represent the eigenvectors
        /// of the decomposed matrix.
        /// </param>
        /// <returns>
        /// A diagonal matrix containing the eigenvalues 
        /// of the decomposed matrix, in ascending order.
        /// </returns>
        /// <example>
        /// <para>
        /// In the following example, the spectral decomposition of a matrix is computed.
        /// </para>
        /// <para>
        /// <code title="Computing the Spectral decomposition of a matrix"
        /// source="..\Novacta.Analytics.CodeExamples\Advanced\SpectralDecompositionExample0.cs.txt" 
        /// language="cs" />
        /// </para>        
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="matrix"/> is not square.
        /// </exception>
        public static DoubleMatrix Decompose(
            DoubleMatrix matrix,
            bool lowerTriangularPart,
            out DoubleMatrix eigenvectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (!matrix.IsSquare)
            {
                throw new ArgumentException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE"),
                    paramName: nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            double[] valuesArray = new double[m];
            double[] vectorsArray = matrix.AsColumnMajorDenseArray();

            int info = SafeNativeMethods.LAPACK.DSYEV(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobz: 'V',
                uplo: lowerTriangularPart ? 'L' : 'U',
                n: m,
                a: vectorsArray,
                lda: m,
                w: valuesArray);

            if (info > 0)
            {
                throw new InvalidOperationException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix eigenvalues = DoubleMatrix.Sparse(m, m, m);
            for (int i = 0; i < m; i++)
            {
                eigenvalues[i, i] = valuesArray[i];
            }

            eigenvectors =
                DoubleMatrix.Dense(m, m, vectorsArray, copyData: false);

            Debug.Assert(eigenvalues != null);
            Debug.Assert(eigenvectors != null);

            return eigenvalues;
        }

        /// <inheritdoc cref="SpectralDecomposition.Decompose(DoubleMatrix, bool, out DoubleMatrix)"/>
        public static DoubleMatrix Decompose(
            ReadOnlyDoubleMatrix matrix,
            bool lowerTriangularPart,
            out DoubleMatrix eigenvectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return Decompose(
                matrix.matrix,
                lowerTriangularPart,
                out eigenvectors);
        }

        /// <summary>
        /// Computes the eigenvalues of the specified symmetric real matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix containing the lower or upper triangular part of 
        /// the matrix whose eigenvalues must be computed.
        /// </param>
        /// <param name="lowerTriangularPart">
        /// <c>true</c> if <paramref name="matrix"/> contains the lower
        /// triangular part of the matrix to be decomposed;
        /// <c>false</c> if <paramref name="matrix"/> contains 
        /// its upper triangular part.
        /// </param>
        /// <returns>
        /// A column vector containing the eigenvalues 
        /// of the decomposed matrix, in ascending order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="matrix"/> is not square.
        /// </exception>
        public static DoubleMatrix GetEigenvalues(
            DoubleMatrix matrix,
            bool lowerTriangularPart)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (!matrix.IsSquare)
            {
                throw new ArgumentException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE"),
                    paramName: nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            double[] valuesArray = new double[m];
            double[] vectorsArray = matrix.AsColumnMajorDenseArray();

            int info = SafeNativeMethods.LAPACK.DSYEV(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobz: 'N',
                uplo: lowerTriangularPart ? 'L' : 'U',
                n: m,
                a: vectorsArray,
                lda: m,
                w: valuesArray);

            if (info > 0)
            {
                throw new InvalidOperationException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix eigenvalues =
                DoubleMatrix.Dense(m, 1, valuesArray, copyData: false);

            Debug.Assert(eigenvalues != null);

            return eigenvalues;
        }

        /// <inheritdoc cref="SpectralDecomposition.GetEigenvalues(DoubleMatrix, bool)"/>
        public static DoubleMatrix GetEigenvalues(
            ReadOnlyDoubleMatrix matrix,
            bool lowerTriangularPart)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return GetEigenvalues(
                matrix.matrix,
                lowerTriangularPart);
        }

        #endregion

        #region Hermitian complex matrices

        /// <summary>
        /// Computes eigenvalues and eigenvectors of the 
        /// specified Hermitian complex matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix containing the lower or upper triangular part of the matrix 
        /// whose spectral decomposition must be computed.
        /// </param>
        /// <param name="lowerTriangularPart">
        /// <c>true</c> if <paramref name="matrix"/> contains the lower
        /// triangular part of the matrix to be decomposed;
        /// <c>false</c> if <paramref name="matrix"/> contains 
        /// its upper triangular part.
        /// </param>
        /// <param name="eigenvectors">
        /// A matrix whose columns represent the eigenvectors
        /// of the decomposed matrix.
        /// </param>
        /// <returns>
        /// A diagonal matrix containing the eigenvalues 
        /// of the decomposed matrix, in ascending order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="matrix"/> is not square.
        /// </exception>
        public static DoubleMatrix Decompose(
        ComplexMatrix matrix,
        bool lowerTriangularPart,
        out ComplexMatrix eigenvectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (!matrix.IsSquare)
            {
                throw new ArgumentException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE"),
                    paramName: nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            double[] valuesArray = new double[m];
            Complex[] vectorsArray = matrix.AsColumnMajorDenseArray();

            int info = SafeNativeMethods.LAPACK.ZHEEV(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobz: 'V',
                uplo: lowerTriangularPart ? 'L' : 'U',
                n: m,
                a: vectorsArray,
                lda: m,
                w: valuesArray);

            if (info > 0)
            {
                throw new InvalidOperationException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix eigenvalues = DoubleMatrix.Sparse(m, m, m);
            for (int i = 0; i < m; i++)
            {
                eigenvalues[i, i] = valuesArray[i];
            }

            eigenvectors =
                ComplexMatrix.Dense(m, m, vectorsArray, copyData: false);

            Debug.Assert(eigenvalues != null);
            Debug.Assert(eigenvectors != null);

            return eigenvalues;
        }

        /// <inheritdoc cref="SpectralDecomposition.Decompose(ComplexMatrix, bool, out ComplexMatrix)"/>
        public static DoubleMatrix Decompose(
            ReadOnlyComplexMatrix matrix,
            bool lowerTriangularPart,
            out ComplexMatrix eigenvectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return Decompose(
                matrix.matrix,
                lowerTriangularPart,
                out eigenvectors);
        }

        /// <summary>
        /// Computes the eigenvalues of the 
        /// specified Hermitian complex matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix containing the lower or upper triangular part of the matrix 
        /// whose eigenvalues must be computed.
        /// </param>
        /// <param name="lowerTriangularPart">
        /// <c>true</c> if <paramref name="matrix"/> contains the lower
        /// triangular part of the matrix to be decomposed;
        /// <c>false</c> if <paramref name="matrix"/> contains 
        /// its upper triangular part.
        /// </param>
        /// <returns>
        /// A column vector containing the eigenvalues 
        /// of the decomposed matrix, in ascending order.
        /// </returns>
        /// <example>
        /// <para>
        /// In the following example, the eigenvalues of a matrix are computed.
        /// </para>
        /// <para>
        /// <code title="Computing the eigenvalues of a matrix"
        /// source="..\Novacta.Analytics.CodeExamples\Advanced\SpectralDecompositionExample1.cs.txt" 
        /// language="cs" />
        /// </para>        
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="matrix"/> is not square.
        /// </exception>
        public static DoubleMatrix GetEigenvalues(
            ComplexMatrix matrix,
            bool lowerTriangularPart)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (!matrix.IsSquare)
            {
                throw new ArgumentException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE"),
                    paramName: nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            double[] valuesArray = new double[m];
            Complex[] vectorsArray = matrix.AsColumnMajorDenseArray();

            int info = SafeNativeMethods.LAPACK.ZHEEV(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobz: 'V',
                uplo: lowerTriangularPart ? 'L' : 'U',
                n: m,
                a: vectorsArray,
                lda: m,
                w: valuesArray);

            if (info > 0)
            {
                throw new InvalidOperationException(
                    message: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix eigenvalues =
                DoubleMatrix.Dense(m, 1, valuesArray, copyData: false);

            Debug.Assert(eigenvalues != null);

            return eigenvalues;
        }

        /// <inheritdoc cref="SpectralDecomposition.GetEigenvalues(ComplexMatrix, bool)"/>
        public static DoubleMatrix GetEigenvalues(
            ReadOnlyComplexMatrix matrix,
            bool lowerTriangularPart)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return GetEigenvalues(
                matrix.matrix,
                lowerTriangularPart);
        }

        #endregion
    }
}
