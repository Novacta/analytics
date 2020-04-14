// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics
{
    /// <summary>
    /// Defines properties to evaluate matrix patterns. 
    /// </summary>
    public interface IMatrixPatterns
    {
        /// <summary>
        /// Gets a value indicating whether this instance is a vector.
        /// </summary>
        /// <value><c>true</c> if this instance is a vector; otherwise, <c>false</c>.</value>
        /// <remarks>A matrix is a vector if its number of rows or its number of columns is 1.</remarks>
        bool IsVector
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a row vector.
        /// </summary>
        /// <value><c>true</c> if this instance is a row vector; otherwise, <c>false</c>.</value>
        /// <remarks>A matrix is a row vector if its number of rows is 1.</remarks>
        bool IsRowVector
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a column vector.
        /// </summary>
        /// <value><c>true</c> if this instance is a column vector; otherwise, <c>false</c>.</value>
        /// <remarks>A matrix is a column vector if its number of columns is 1.</remarks>
        bool IsColumnVector
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is scalar.
        /// </summary>
        /// <value><c>true</c> if this instance is scalar; otherwise, <c>false</c>.</value>
        /// <seealso href="https://en.wikipedia.org/wiki/Scalar_(mathematics)" />
        /// <remarks>A matrix is scalar if both its number of rows and its number of columns are equal to 1.</remarks>
        bool IsScalar
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is square.
        /// </summary>
        /// <value><c>true</c> if this instance is square; otherwise, <c>false</c>.</value>
        /// <seealso href="https://en.wikipedia.org/wiki/Square_matrix" />
        /// <remarks>A matrix is square if its number of rows equals its number of columns.</remarks>
        bool IsSquare
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is diagonal.
        /// </summary>
        /// <value><c>true</c> if this instance is diagonal; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// <para>
        /// A matrix is diagonal if it is square with zero elements outside the main diagonal.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='main']"/>
        /// <para>
        /// Hence <see cref="IsDiagonal"/> 
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">j \neq i</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being diagonal.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsDiagonalExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Diagonal_matrix"/>
        bool IsDiagonal
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is Hessenberg.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Hessenberg; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is Hessenberg if it is lower or upper Hessenberg.
        /// </para>
        /// </remarks>
        /// <seealso cref="IsLowerHessenberg"/>
        /// <seealso cref="IsUpperHessenberg"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Hessenberg_matrix"/>
        bool IsHessenberg
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is lower Hessenberg.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is lower Hessenberg; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is lower Hessenberg if it square and has zero entries above its first super-diagonal.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='super']"/>
        /// <para>
        /// Hence <see cref="IsLowerHessenberg"/>
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">j>i+1</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being lower Hessenberg.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsLowerHessenbergExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Hessenberg_matrix"/>
        bool IsLowerHessenberg
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is symmetric.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is symmetric; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Property <see cref="IsSymmetric"/> returns
        /// <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=A_{j,i}</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being symmetric.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsSymmetricExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Symmetric_matrix"/>
        bool IsSymmetric
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is skew symmetric.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is skew symmetric; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Property <see cref="IsSkewSymmetric"/> returns
        /// <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=-A_{j,i}</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being skew symmetric.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsSkewSymmetricExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Skew-symmetric_matrix"/>
        bool IsSkewSymmetric
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is triangular.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is triangular; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is triangular if it is lower or upper triangular.
        /// </para>
        /// </remarks>
        /// <seealso cref="IsLowerTriangular"/>
        /// <seealso cref="IsUpperTriangular"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Triangular_matrix"/>
        bool IsTriangular
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is lower triangular.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is lower triangular; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is lower triangular if it is square and the entries on its super-diagonals are zero.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='super']"/>
        /// <para>
        /// Hence <see cref="IsLowerTriangular"/>
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A{i,j}=0</latex> whenever <latex mode="inline">j>i</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being lower triangular.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsLowerTriangularExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Triangular_matrix"/>
        bool IsLowerTriangular
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper triangular.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is upper triangular; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is upper triangular if it is square and the entries on its sub-diagonals are zero.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='sub']"/>
        /// <para>
        /// Hence <see cref="IsUpperTriangular"/> 
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">i>j</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being upper triangular.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsUpperTriangularExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Triangular_matrix"/>
        bool IsUpperTriangular
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is tridiagonal.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is tridiagonal; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is tridiagonal if it is square and has zero entries outside 
        /// its main diagonal and its first sub and super diagonals.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='main']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='sub']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='super']"/>
        /// <para>
        /// Hence <see cref="IsTridiagonal"/>
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">i>j+1</latex> or
        /// <latex mode="inline">j>i+1</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being tridiagonal.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsTridiagonalExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Tridiagonal_matrix"/>
        bool IsTridiagonal
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is lower bidiagonal.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is lower bidiagonal; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is lower bidiagonal if it is square and has zero entries outside 
        /// its main diagonal and its first sub-diagonal.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='main']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='sub']"/>
        /// <para>
        /// Hence <see cref="IsLowerBidiagonal"/>
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">i>j+1</latex> or
        /// <latex mode="inline">j>i</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being lower bidiagonal.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsLowerBidiagonalExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Bidiagonal_matrix"/>
        bool IsLowerBidiagonal
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper bidiagonal.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is upper bidiagonal; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is upper bidiagonal if it is square and has zero entries outside 
        /// its main diagonal and its first super-diagonal.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='main']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='super']"/>
        /// <para>
        /// Hence <see cref="IsUpperBidiagonal"/>
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">j>i+1</latex> or
        /// <latex mode="inline">i>j</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being upper bidiagonal.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsUpperBidiagonalExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Bidiagonal_matrix"/>
        bool IsUpperBidiagonal
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is bidiagonal.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is bidiagonal; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is bidiagonal if it is lower or upper bidiagonal.
        /// </para>
        /// </remarks>
        /// <seealso cref="IsLowerBidiagonal"/>
        /// <seealso cref="IsUpperBidiagonal"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Bidiagonal_matrix"/>
        bool IsBidiagonal
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper Hessenberg.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is upper Hessenberg; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>
        /// A matrix is upper Hessenberg if it is square and has zero entries below its 
        /// first sub-diagonal.
        /// </para>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='sub']"/>
        /// <para>
        /// Hence <see cref="IsUpperHessenberg"/>
        /// returns <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=0</latex> whenever <latex mode="inline">i>j+1</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are tested
        /// for being upper Hessenberg.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IsUpperHessenbergExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Hessenberg_matrix"/>
        bool IsUpperHessenberg
        {
            get;
        }

        /// <summary>
        /// Gets the lower bandwidth of this instance.
        /// </summary>
        /// <value>The matrix lower bandwidth.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='lower']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the lower bandwidth is computed for some matrices.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\LowerBandwidthExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="UpperBandwidth"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Band_matrix"/>
        int LowerBandwidth
        {
            get;
        }

        /// <summary>
        /// Gets the upper bandwidth of this instance.
        /// </summary>
        /// <value>The matrix upper bandwidth.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='upper']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the upper bandwidth is computed for some matrices.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\UpperBandwidthExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="LowerBandwidth"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Band_matrix"/>
        int UpperBandwidth
        {
            get;
        }
    }
}
