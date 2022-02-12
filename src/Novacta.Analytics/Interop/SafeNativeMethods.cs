// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Novacta.Analytics.Interop
{
    /// <summary>
    /// Provides support to operate with selected native routines.
    /// </summary>
    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        #region BLAS

        /// <summary>
        /// Provides support to operate with selected BLAS native routines.
        /// </summary>
        internal static class BLAS
        {
            /// <summary>
            /// Contains constants indicating a linear ordering by which 
            /// matrix entries are stored.
            /// </summary>
            internal static class ORDER
            {
                /// <summary>
                /// The order is by columns. Entries are ordered by their column index first,
                /// and entries laying on a given column are in turn ordered by their row index.
                /// </summary>
                internal static int ColMajor = 102;

                /// <summary>
                /// The order is by rows. Entries are ordered by their row index first,
                /// and entries laying on a given row are in turn ordered by their column index.
                /// </summary>
                internal static int RowMajor = 101;
            };

            /// <summary>
            /// Contains constants indicating an operation to be executed 
            /// on a matrix.
            /// </summary>
            internal static class TRANSPOSE
            {
                /// <summary>
                /// No matrix operation. 
                /// </summary>
                internal static int NoTrans = 111; /* trans='N' */

                /// <summary>
                /// Matrix transposition. 
                /// </summary>
                internal static int Trans = 112; /* trans='T' */

                /// <summary>
                /// Conjugate matrix transposition. 
                /// </summary>
                internal static int ConjTrans = 113; /* trans='C' */

                /// <summary>
                /// Conjugate matrix transposition. 
                /// </summary>
                internal static int Conj = 114; /* trans='R' */
            };

            /// <summary>
            /// Contains constants indicating whether a matrix is upper 
            /// or lower triangular.
            /// </summary>
            internal static class UPLO
            {
                /// <summary>
                /// The matrix is lower triangular. 
                /// </summary>
                internal static int Lower = 122; /* UPLO='L' */

                /// <summary>
                /// The matrix is upper triangular. 
                /// </summary>
                internal static int Upper = 121; /* UPLO='U' */
            };

            /// <summary>
            /// Contains constants indicating whether a matrix is unit  
            /// triangular.
            /// </summary>
            internal static class DIAG
            {
                /// <summary>
                /// The matrix is not unit triangular.
                /// </summary>
                internal static int NonUnit = 131; /* DIAG='N' */

                /// <summary>
                /// The matrix is unit triangular.
                /// </summary>
                internal static int Unit = 132; /* DIAG='U' */
            };

            /// <summary>
            /// Contains constants indicating whether a matrix must be   
            /// left or right multiplied by another one.
            /// </summary>
            internal static class SIDE
            {
                /// <summary>
                /// The matrix must be left multiplied.
                /// </summary>
                internal static int Left = 141; /* side='L' */

                /// <summary>
                /// The matrix must be right multiplied.
                /// </summary>
                internal static int Right = 142; /* side='R' */
            };

            #region Double (d)

            [DllImport("libna",
                EntryPoint = "cblas_dgemm",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern void DGEMM(
                     int order,
                     int transA,
                     int transB,
                     int m,
                     int n,
                     int k,
                     double alpha,
                     [In] double[] a,
                     int lda,
                     [In] double[] b,
                     int ldb,
                     double beta,
                     [In, Out] double[] c,
                     int ldc);

            [DllImport("libna",
                EntryPoint = "cblas_dtrsm",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern void DTRSM(
                    int order,
                    int side,
                    int uplo,
                    int transA,
                    int diag,
                    int m,
                    int n,
                    double alpha,
                    [In] double[] a,
                    int lda,
                    [In, Out] double[] b,
                    int ldb);

            #endregion

            #region Complex (z)

            [DllImport("libna",
                EntryPoint = "cblas_zgemm",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern unsafe void ZGEMM(
                     int order,
                     int transA,
                     int transB,
                     int m,
                     int n,
                     int k,
                     Complex* alpha,
                     Complex* a,
                     int lda,
                     Complex* b,
                     int ldb,
                     Complex* beta,
                     Complex* c,
                     int ldc);

            [DllImport("libna",
                EntryPoint = "cblas_ztrsm",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern unsafe void ZTRSM(
                    int order,
                    int side,
                    int uplo,
                    int transA,
                    int diag,
                    int m,
                    int n,
                    Complex* alpha,
                    Complex* a,
                    int lda,
                    Complex* b,
                    int ldb);

            #endregion
        }

        #endregion

        #region TRANS

        /// <summary>
        /// Provides support to operate with selected native transposition routines.
        /// </summary>
        internal static class TRANS
        {
            [DllImport("libna",
                EntryPoint = "MKL_Dimatcopy",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern void DIMATCOPY(
                    char ordering,
                    char trans,
                    int rows,
                    int cols,
                    double alpha,
                    [In, Out] double[] ab,
                    int lda,
                    int ldb);

            [DllImport("libna",
                EntryPoint = "MKL_Zimatcopy",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern void ZIMATCOPY(
                    char ordering,
                    char trans,
                    int rows,
                    int cols,
                    Complex alpha,
                    [In, Out] Complex[] ab,
                    int lda,
                    int ldb);
        }

        #endregion

        #region LAPACK

        /// <summary>
        /// Provides support to operate with selected LAPACK native routines.
        /// </summary>
        internal static class LAPACK
        {
            /// <summary>
            /// Contains constants indicating a linear ordering by which 
            /// matrix entries are stored.
            /// </summary>
            internal static class ORDER
            {
                /// <summary>
                /// The order is by columns. Entries are ordered by their column index first,
                /// and entries laying on a given column are in turn ordered by their row index.
                /// </summary>
                internal static int ColMajor = 102; //LAPACK_COL_MAJOR;

                /// <summary>
                /// The order is by rows. Entries are ordered by their row index first,
                /// and entries laying on a given row are in turn ordered by their column index.
                /// </summary>
                internal static int RowMajor = 101; //LAPACK_ROW_MAJOR;
            };

            #region Double (d)

            [DllImport("libna",
                EntryPoint = "LAPACKE_dgels",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DGELS(
                int matrix_order,
                char trans,
                int m,
                int n,
                int nrhs,
                [In, Out] double[] a,
                int lda,
                [In, Out] double[] b,
                int ldb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_dgetrs",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DGETRS(
                int matrix_order,
                char trans,
                int n,
                int nrhs,
                [In] double[] a,
                int lda,
                [In] int[] ipiv,
                [In, Out] double[] b,
                int ldb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_dgetrf",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DGETRF(
                int matrix_order,
                int m,
                int n,
                [In, Out] double[] a,
                int lda,
                [Out] int[] ipiv);

            [DllImport("libna",
                EntryPoint = "LAPACKE_dposv",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DPOSV(
                int matrix_order,
                char uplo,
                int n,
                int nrhs,
                [In, Out] double[] a,
                int lda,
                [In, Out] double[] b,
                int ldb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_dgesvd",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DGESVD(
                int matrix_layout,
                char jobu,
                char jobvt,
                int m,
                int n,
                [In, Out] double[] a,
                int lda,
                [In, Out] double[] s,
                [In, Out] double[] u,
                int ldu,
                [In, Out] double[] vt,
                int ldvt,
                [In, Out] double[] superb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_dsyev",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSYEV(
                int matrix_layout,
                char jobz,
                char uplo,
                int n,
                [In, Out] double[] a,
                int lda,
                [In, Out] double[] w);

            #endregion

            #region Complex (z)

            [DllImport("libna",
                EntryPoint = "LAPACKE_zgels",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int ZGELS(
                int matrix_order,
                char trans,
                int m,
                int n,
                int nrhs,
                [In, Out] Complex[] a,
                int lda,
                [In, Out] Complex[] b,
                int ldb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_zgetrs",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int ZGETRS(
                int matrix_order,
                char trans,
                int n,
                int nrhs,
                [In] Complex[] a,
                int lda,
                [In] int[] ipiv,
                [In, Out] Complex[] b,
                int ldb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_zgetrf",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int ZGETRF(
                int matrix_order,
                int m,
                int n,
                [In, Out] Complex[] a,
                int lda,
                [Out] int[] ipiv);

            [DllImport("libna",
                EntryPoint = "LAPACKE_zposv",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int ZPOSV(
                int matrix_order,
                char uplo,
                int n,
                int nrhs,
                [In, Out] Complex[] a,
                int lda,
                [In, Out] Complex[] b,
                int ldb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_zgesvd",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int ZGESVD(
                int matrix_layout,
                char jobu,
                char jobvt,
                int m,
                int n,
                [In, Out] Complex[] a,
                int lda,
                [In, Out] double[] s,
                [In, Out] Complex[] u,
                int ldu,
                [In, Out] Complex[] vt,
                int ldvt,
                [In, Out] double[] superb);

            [DllImport("libna",
                EntryPoint = "LAPACKE_zheev",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int ZHEEV(
                int matrix_layout,
                char jobz,
                char uplo,
                int n,
                [In, Out] Complex[] a,
                int lda,
                [In, Out] double[] w);

            #endregion
        }


        #endregion

        #region VML

        /// <summary>
        /// Provides support to operate with selected VML native routines.
        /// </summary>
        internal static class VML
        {
            [DllImport("libna",
                EntryPoint = "vdCdfNormInv",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern unsafe void vdCdfNormInv(
                int n,
                double* a,
                double* y);
        }

        #endregion

        #region VSL

        /// <summary>
        /// Provides support to operate with selected VSL native routines.
        /// </summary>
        internal static class VSL
        {
            /// <summary>
            /// Provides constants to represents VSL Status definitions.
            /// </summary>
            internal static class STATUS
            {
                /// <summary>
                /// Represents a "NO ERROR" status.
                /// </summary>
                internal static int VSL_STATUS_OK = 0;
            }

            /// <summary>
            /// Provides constants representing predefined names of VSL 
            /// Basic Random Number Generators (BRNG).
            /// </summary>
            internal class BRNG
            {
                // Static field initializers run in the order in which 
                // the fields are declared. 

                private const int VSL_BRNG_SHIFT = 20;
                private const int VSL_BRNG_INC = (1 << VSL_BRNG_SHIFT); // 1048576

                internal static int VSL_BRNG_MCG31 = VSL_BRNG_INC;
                internal static int VSL_BRNG_R250 = (VSL_BRNG_MCG31 + VSL_BRNG_INC);
                internal static int VSL_BRNG_MRG32K3A = (VSL_BRNG_R250 + VSL_BRNG_INC);
                internal static int VSL_BRNG_MCG59 = (VSL_BRNG_MRG32K3A + VSL_BRNG_INC);
                internal static int VSL_BRNG_WH = (VSL_BRNG_MCG59 + VSL_BRNG_INC);
                internal static int VSL_BRNG_SOBOL = (VSL_BRNG_WH + VSL_BRNG_INC);
                internal static int VSL_BRNG_NIEDERR = (VSL_BRNG_SOBOL + VSL_BRNG_INC);
                internal static int VSL_BRNG_MT19937 = (VSL_BRNG_NIEDERR + VSL_BRNG_INC);
                internal static int VSL_BRNG_MT2203 = (VSL_BRNG_MT19937 + VSL_BRNG_INC);
                internal static int VSL_BRNG_IABSTRACT = (VSL_BRNG_MT2203 + VSL_BRNG_INC);
                internal static int VSL_BRNG_DABSTRACT = (VSL_BRNG_IABSTRACT + VSL_BRNG_INC);
                internal static int VSL_BRNG_SABSTRACT = (VSL_BRNG_DABSTRACT + VSL_BRNG_INC);
                internal static int VSL_BRNG_SFMT19937 = (VSL_BRNG_SABSTRACT + VSL_BRNG_INC);
                internal static int VSL_BRNG_NONDETERM = (VSL_BRNG_SFMT19937 + VSL_BRNG_INC);
                internal static int VSL_BRNG_ARS5 = (VSL_BRNG_NONDETERM + VSL_BRNG_INC);
                internal static int VSL_BRNG_PHILOX4X32X10 = (VSL_BRNG_ARS5 + VSL_BRNG_INC);
            };

            #region Random streams

            // Methods to operate with VSL Random Streams.

            [DllImport("libna",
                EntryPoint = "vslNewStream",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NewStream(
                ref IntPtr stream,
                int brng,
                int seed);

            [DllImport("libna",
                EntryPoint = "vslDeleteStream",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DeleteStream(
                ref IntPtr stream);

            [DllImport("libna",
                EntryPoint = "vdRngUniform",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern unsafe int vdRngUniform(
                int method,
                void* stream,
                int n,
                double* r,
                double a,
                double b);

            [DllImport("libna",
                EntryPoint = "viRngUniform",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern unsafe int viRngUniform(
                int method, // Must be 0
                void* stream,
                int n,
                int* r,
                int a,
                int b);

            [DllImport("libna",
                EntryPoint = "vdRngGaussian",
                CallingConvention = CallingConvention.Cdecl)]
            internal static extern unsafe int vdRngGaussian(
                int method,
                void* stream,
                int n,
                double* r,
                double mu,
                double sigma);

            #endregion
        }

        #endregion
    }
}
