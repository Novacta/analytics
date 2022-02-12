// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Interop;
using System;
using System.Numerics;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Provides methods to compute the Singular Value Decomposition
    /// of a matrix.
    /// </summary>
    /// <remarks>
    /// <para id='matrix'>
    /// Let <latex mode="inline">A</latex> be a matrix having
    /// <latex mode="inline">m</latex> rows and  
    /// <latex mode="inline">n</latex> columns.
    /// </para>
    /// <para>
    /// The Singular Value Decomposition (SVD) of <latex>A</latex> is a factorization 
    /// having the following form:
    /// <latex mode='display'>A = U \Sigma V^{\dagger},</latex>
    /// where <latex>X^\dagger</latex> returns the conjugate transpose of 
    /// <latex>X</latex>, <latex>U</latex> and <latex>V</latex> are unitary complex 
    /// matrices having sizes <latex>m \times m</latex> and <latex>n \times n</latex>,
    /// respectively, and
    /// <latex>\Sigma</latex> is a diagonal <latex>m \times n</latex> real matrix.
    /// Matrix <latex>U</latex> has 
    /// columns known as <i>left singular vectors</i>.
    /// Matrix <latex>\Sigma</latex> has diagonal entries known as the
    /// <i>singular values</i> of <latex>A</latex>. Finally, 
    /// matrix <latex>V^{\dagger}</latex> has rows that represent
    /// the conjugate transposed <i>right singular vectors</i>
    /// of <latex>A</latex>. 
    /// </para>
    /// <para>
    /// Method <see cref="Decompose(DoubleMatrix, out DoubleMatrix, out DoubleMatrix)"/>
    /// and its overloads compute matrices 
    /// <latex>U</latex>, <latex>\Sigma</latex>, and <latex>V^{\dagger}</latex>
    /// for double or complex matrices.
    /// </para>
    /// <para>
    /// Method <see cref="GetSingularValues(DoubleMatrix)"/> and its 
    /// overloads return the singular values involved in a decomposition,
    /// without computing the corresponding singular vectors.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, the SVD of a matrix is computed.
    /// </para>
    /// <para>
    /// <code title="Computing the SVD of a matrix"
    /// source="..\Novacta.Analytics.CodeExamples\Advanced\SingularValueDecompositionExample0.cs.txt" 
    /// language="cs" />
    /// </para>        
    /// </example>
    /// <seealso href="https://en.wikipedia.org/wiki/Singular_value_decomposition"/>
    public static class SingularValueDecomposition
    {
        #region Double

        /// <summary>
        /// Computes the Singular Value Decomposition of the 
        /// specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix to decompose.</param>
        /// <param name="leftSingularVectors">
        /// The matrix whose columns represent the left singular vectors.
        /// </param>
        /// <param name="conjugateTransposedRightSingularVectors">
        /// The matrix whose rows represent the conjugate transposed right 
        /// singular vectors.
        /// </param>
        /// <returns>
        /// The diagonal matrix of singular values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix Decompose(
                DoubleMatrix matrix,
                out DoubleMatrix leftSingularVectors,
                out DoubleMatrix conjugateTransposedRightSingularVectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            int n = matrix.NumberOfColumns;
            int min_m_n = m < n ? m : n;

            double[] a = matrix.AsColumnMajorDenseArray();
            double[] s = new double[min_m_n];
            double[] u = new double[m * m];
            double[] vt = new double[n * n];
            int lapackInfo;
            double[] superb = new double[min_m_n - 1];
            lapackInfo = SafeNativeMethods.LAPACK.DGESVD(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobu: 'A',
                jobvt: 'A',
                m,
                n,
                a,
                lda: m,
                s: s,
                u,
                ldu: m,
                vt,
                ldvt: n,
                superb);

            if (lapackInfo > 0)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix values = DoubleMatrix.Sparse(m, n, min_m_n);
            for (int i = 0; i < min_m_n; i++)
            {
                values[i, i] = s[i];
            }

            leftSingularVectors =
                DoubleMatrix.Dense(m, m, u, copyData: false);

            conjugateTransposedRightSingularVectors =
                DoubleMatrix.Dense(n, n, vt, copyData: false);

            return values;
        }

        /// <inheritdoc cref="SingularValueDecomposition.Decompose(
        /// DoubleMatrix, out DoubleMatrix, out DoubleMatrix)"/>
        public static DoubleMatrix Decompose(
            ReadOnlyDoubleMatrix matrix,
            out DoubleMatrix leftSingularVectors,
            out DoubleMatrix conjugateTransposedRightSingularVectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return Decompose(
                matrix.matrix,
                out leftSingularVectors,
                out conjugateTransposedRightSingularVectors);
        }

        /// <summary>
        /// Computes the singular values of the specified matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix whose singular values
        /// must be computed.
        /// </param>
        /// <returns>
        /// A column vector containing the singular values of the specified
        /// matrix, in descending order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix GetSingularValues(
                DoubleMatrix matrix)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            int n = matrix.NumberOfColumns;
            int min_m_n = m < n ? m : n;

            double[] a = matrix.AsColumnMajorDenseArray();
            double[] s = new double[min_m_n];
            double[] u = null;
            double[] vt = null;
            int lapackInfo;
            double[] superb = new double[min_m_n - 1];
            lapackInfo = SafeNativeMethods.LAPACK.DGESVD(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobu: 'N',
                jobvt: 'N',
                m,
                n,
                a,
                lda: m,
                s: s,
                u,
                ldu: m,
                vt,
                ldvt: n,
                superb);

            if (lapackInfo > 0)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix values =
                DoubleMatrix.Dense(min_m_n, 1, s, copyData: false);

            return values;
        }

        ///<inheritdoc cref="SingularValueDecomposition
        ///.GetSingularValues(DoubleMatrix)"/>
        public static DoubleMatrix GetSingularValues(
            ReadOnlyDoubleMatrix matrix)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return GetSingularValues(matrix.matrix);
        }

        #endregion

        #region Complex

        ///<inheritdoc cref="SingularValueDecomposition.Decompose(
        ///DoubleMatrix, out DoubleMatrix, out DoubleMatrix)"/>
        public static DoubleMatrix Decompose(
            ComplexMatrix matrix,
            out ComplexMatrix leftSingularVectors,
            out ComplexMatrix conjugateTransposedRightSingularVectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            int n = matrix.NumberOfColumns;
            int min_m_n = m < n ? m : n;

            Complex[] a = matrix.AsColumnMajorDenseArray();
            double[] s = new double[min_m_n];
            Complex[] u = new Complex[m * m];
            Complex[] vt = new Complex[n * n];
            int lapackInfo;
            double[] superb = new double[min_m_n - 1];
            lapackInfo = SafeNativeMethods.LAPACK.ZGESVD(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobu: 'A',
                jobvt: 'A',
                m,
                n,
                a,
                lda: m,
                s,
                u,
                ldu: m,
                vt,
                ldvt: n,
                superb);

            if (lapackInfo > 0)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix values = DoubleMatrix.Dense(m, n);
            for (int i = 0; i < min_m_n; i++)
            {
                values[i, i] = s[i];
            }

            leftSingularVectors =
                ComplexMatrix.Dense(m, m, u, copyData: false);

            conjugateTransposedRightSingularVectors =
                ComplexMatrix.Dense(n, n, vt, copyData: false);

            return values;
        }

        ///<inheritdoc cref="SingularValueDecomposition.Decompose(
        ///ComplexMatrix, out ComplexMatrix, out ComplexMatrix)"/>
        public static DoubleMatrix Decompose(
            ReadOnlyComplexMatrix matrix,
            out ComplexMatrix leftSingularVectors,
            out ComplexMatrix conjugateTransposedRightSingularVectors)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return Decompose(
                matrix.matrix,
                out leftSingularVectors,
                out conjugateTransposedRightSingularVectors);
        }

        ///<inheritdoc cref="SingularValueDecomposition
        ///.GetSingularValues(DoubleMatrix)"/>
        public static DoubleMatrix GetSingularValues(
                ComplexMatrix matrix)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            int m = matrix.NumberOfRows;
            int n = matrix.NumberOfColumns;
            int min_m_n = m < n ? m : n;

            Complex[] a = matrix.AsColumnMajorDenseArray();
            double[] s = new double[min_m_n];
            Complex[] u = null;
            Complex[] vt = null;
            int lapackInfo;
            double[] superb = new double[min_m_n - 1];
            lapackInfo = SafeNativeMethods.LAPACK.ZGESVD(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobu: 'N',
                jobvt: 'N',
                m,
                n,
                a,
                lda: m,
                s: s,
                u,
                ldu: m,
                vt,
                ldvt: n,
                superb);

            if (lapackInfo > 0)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ALG_DOES_NOT_CONVERGE"));
            }

            DoubleMatrix values =
                DoubleMatrix.Dense(min_m_n, 1, s, copyData: false);

            return values;
        }

        ///<inheritdoc cref="SingularValueDecomposition
        ///.GetSingularValues(DoubleMatrix)"/>
        public static DoubleMatrix GetSingularValues(
            ReadOnlyComplexMatrix matrix)
        {
            #region Input validation

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            #endregion

            return GetSingularValues(matrix.matrix);
        }

        #endregion
    }
}
