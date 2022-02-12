// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Numerics;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Represents the state of a <see cref="ComplexMatrix"/> and 
    /// provides extended information about the patterns 
    /// observable in the matrix and the row and 
    /// column nomenclatures.
    /// </summary>
    class ExtendedComplexMatrixState : ComplexMatrixState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedComplexMatrixState" /> class.
        /// </summary>
        /// <param name="asColumnMajorDenseArray">A column major dense representation of
        /// the matrix data.</param>
        /// <param name="numberOfRows">The matrix number of rows.</param>
        /// <param name="numberOfColumns">The matrix number of columns.</param>
        /// <param name="isUpperHessenberg">If set to <c>true</c> the matrix is upper Hessenberg.</param>
        /// <param name="isLowerHessenberg">If set to <c>true</c> the matrix is lower Hessenberg.</param>
        /// <param name="isUpperTriangular">If set to <c>true</c> the matrix is upper triangular.</param>
        /// <param name="isLowerTriangular">If set to <c>true</c> the matrix is lower triangular.</param>
        /// <param name="isSymmetric">If set to <c>true</c> the matrix is symmetric.</param>
        /// <param name="isSkewSymmetric">If set to <c>true</c> the matrix is skew symmetric.</param>
        /// <param name="isHermitian">If set to <c>true</c> the matrix is Hermitian.</param>
        /// <param name="isSkewHermitian">If set to <c>true</c> the matrix is skew Hermitian.</param>
        /// <param name="upperBandwidth">The matrix upper bandwidth.</param>
        /// <param name="lowerBandwidth">The matrix lower bandwidth.</param>
        /// <param name="name">The name of the matrix.</param>
        /// <param name="rowNames">The matrix row names.</param>
        /// <param name="columnNames">The matrix column names.</param>
        public ExtendedComplexMatrixState(
            Complex[] asColumnMajorDenseArray,
            int numberOfRows,
            int numberOfColumns,
            bool isUpperHessenberg,
            bool isLowerHessenberg,
            bool isUpperTriangular,
            bool isLowerTriangular,
            bool isSymmetric,
            bool isSkewSymmetric,
            bool isHermitian,
            bool isSkewHermitian,
            int upperBandwidth,
            int lowerBandwidth,
            string name = null,
            Dictionary<int, string> rowNames = null,
            Dictionary<int, string> columnNames = null)
            : base(
                  asColumnMajorDenseArray,
                  numberOfRows,
                  numberOfColumns,
                  name,
                  rowNames,
                  columnNames)
        {
            this.IsUpperHessenberg = isUpperHessenberg;
            this.IsLowerHessenberg = isLowerHessenberg;
            this.IsUpperTriangular = isUpperTriangular;
            this.IsLowerTriangular = isLowerTriangular;
            this.IsSymmetric = isSymmetric;
            this.IsSkewSymmetric = isSkewSymmetric;
            this.IsHermitian = isHermitian;
            this.IsSkewHermitian = isSkewHermitian;
            this.UpperBandwidth = upperBandwidth;
            this.LowerBandwidth = lowerBandwidth;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper Hessenberg.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is upper Hessenberg; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpperHessenberg
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is lower Hessenberg.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is lower Hessenberg; otherwise, <c>false</c>.
        /// </value>
        public bool IsLowerHessenberg
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is upper triangular.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is upper triangular; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpperTriangular
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is lower triangular.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is lower triangular; otherwise, <c>false</c>.
        /// </value>
        public bool IsLowerTriangular
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is symmetric.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is symmetric; otherwise, <c>false</c>.
        /// </value>
        public bool IsSymmetric
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is skew symmetric.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is skew symmetric; otherwise, <c>false</c>.
        /// </value>
        public bool IsSkewSymmetric
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is Hermitian.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Hermitian; otherwise, <c>false</c>.
        /// </value>
        public bool IsHermitian
        { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is skew Hermitian.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is skew Hermitian; otherwise, <c>false</c>.
        /// </value>
        public bool IsSkewHermitian
        { get; private set; }

        /// <summary>
        /// Gets the upper bandwidth of this instance.
        /// </summary>
        /// <value>The matrix upper bandwidth.</value>
        public int UpperBandwidth
        { get; private set; }

        /// <summary>
        /// Gets the lower bandwidth of this instance.
        /// </summary>
        /// <value>The matrix lower bandwidth.</value>
        public int LowerBandwidth
        { get; private set; }
    }
}
