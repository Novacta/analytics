// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.Tools;
using System.Collections.Generic;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the possible implementations and the expected state of a 
    /// matrix to be tested 
    /// with <see cref="ComplexMatrixTest"/>.
    /// </summary>
    class TestableComplexMatrix
    {
        readonly ExtendedComplexMatrixState state;

        protected TestableComplexMatrix(ExtendedComplexMatrixState state)
        {
            this.state = state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix" /> class.
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
        public TestableComplexMatrix(
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
        {
            this.state = new ExtendedComplexMatrixState(
                asColumnMajorDenseArray,
                numberOfRows,
                numberOfColumns,
                isUpperHessenberg,
                isLowerHessenberg,
                isUpperTriangular,
                isLowerTriangular,
                isSymmetric,
                isSkewSymmetric,
                isHermitian,
                isSkewHermitian,
                upperBandwidth,
                lowerBandwidth,
                name,
                rowNames,
                columnNames);
        }

        /// <summary>
        /// Returns a dense implemented <see cref="ComplexMatrix"/> 
        /// having the state expected by this instance.
        /// </summary>
        /// <value>
        /// A dense implemented matrix to test.</value>
        public virtual ComplexMatrix AsDense
        {
            get
            {
                return this.Expected.AsDense();
            }
        }

        /// <summary>
        /// Returns a sparse implemented <see cref="ComplexMatrix"/> 
        /// having the state expected by this instance.
        /// </summary>
        /// <value>A sparse implemented matrix to test.</value>
        public virtual ComplexMatrix AsSparse
        {
            get
            {
                return this.Expected.AsSparse();
            }
        }

        /// <summary>
        /// Gets the expected state of the <see cref="ComplexMatrix"/> to test.
        /// </summary>
        /// <value>The expected state of the matrix to test.</value>
        public ExtendedComplexMatrixState Expected
        {
            get
            {
                return this.state;
            }
        }
    }
}
