// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;

using Novacta.Analytics.Infrastructure;
using System.Numerics;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a read only collection of doubles arranged in rows and columns.
    /// Provides methods to operate algebraically on matrices when operands are
    /// both writable and read only.
    ///</summary>
    ///<seealso cref="DoubleMatrix"/>
    public class ReadOnlyDoubleMatrix :
        IEnumerable<double>,
        IList<double>,
        IReadOnlyList<double>,
        IComplexMatrixPatterns,
        IReadOnlyTabularCollection<double, DoubleMatrix>,
        ITabularCollection<double, DoubleMatrix>
    {
        internal DoubleMatrix matrix;

        #region Constructors

        internal ReadOnlyDoubleMatrix(DoubleMatrix matrix)
        {
            this.matrix = matrix;
        }

        #endregion

        #region Conversion operators

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="ReadOnlyDoubleMatrix"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <remarks>
        /// <para>
        /// Only scalar matrices are successfully converted to a double.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value"/> is not scalar.
        /// </exception>
        public static explicit operator double(ReadOnlyDoubleMatrix value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return (double)value.matrix;
        }

        /// <summary>
        /// Converts 
        /// from <see cref="ReadOnlyDoubleMatrix"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <remarks>
        /// <para>
        /// Only scalar matrices are successfully converted to a double.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value"/> is not scalar.
        /// </exception>
        public static double ToDouble(ReadOnlyDoubleMatrix value)
        {
            return (double)value;
        }

        #endregion

        #region Names

        /// <inheritdoc cref="DoubleMatrix.Name"/>
        public String Name { get { return this.matrix.Name; } }

        #region Rows

        /// <inheritdoc cref="DoubleMatrix.HasRowNames"/>
        public bool HasRowNames { get { return this.matrix.HasRowNames; } }

        /// <inheritdoc cref="DoubleMatrix.TryGetRowName(int, out string)"/>
        public bool TryGetRowName(int rowIndex, out string rowName)
        {
            return this.matrix.TryGetRowName(rowIndex, out rowName);
        }

        /// <inheritdoc cref="DoubleMatrix.RowNames"/>
        public ReadOnlyDictionary<int, string> RowNames
        {
            get { return this.matrix.RowNames; }
        }

        #endregion

        #region Columns

        /// <inheritdoc cref="DoubleMatrix.HasColumnNames"/>
        public bool HasColumnNames { get { return this.matrix.HasColumnNames; } }

        /// <inheritdoc cref="DoubleMatrix.TryGetColumnName(int, out string)"/>
        public bool TryGetColumnName(int columnIndex, out string columnName)
        {
            return this.matrix.TryGetColumnName(columnIndex, out columnName);
        }

        /// <inheritdoc cref="DoubleMatrix.ColumnNames"/>
        public ReadOnlyDictionary<int, string> ColumnNames
        {
            get { return this.matrix.ColumnNames; }
        }

        #endregion

        #endregion

        #region Dimensions        

        /// <summary>
        /// Gets the number of entries in this instance.
        /// </summary>
        /// <value>The matrix number of entries.</value>
        public int Count
        {
            get { return this.matrix.implementor.Count; }
        }

        #endregion

        #region Object

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.matrix.ToString();
        }

        #endregion

        #region Operations

        #region Find

        #region Value

        /// <inheritdoc cref="DoubleMatrix.Find(double)"/>
        public IndexCollection Find(double value)
        {
            return this.matrix.Find(value);
        }

        #endregion

        #region Nonzero

        /// <inheritdoc cref="DoubleMatrix.FindNonzero()"/>
        public IndexCollection FindNonzero()
        {
            return this.matrix.FindNonzero();
        }

        #endregion

        #region While

        /// <inheritdoc cref="DoubleMatrix.FindWhile(Predicate{double})"/>
        public IndexCollection FindWhile(Predicate<double> match)
        {
            return this.matrix.FindWhile(match);
        }

        #endregion

        #endregion

        #region ElementWiseMultiply

        /// <inheritdoc cref="DoubleMatrix.ElementWiseMultiply(DoubleMatrix, DoubleMatrix)"/>
        public static DoubleMatrix ElementWiseMultiply(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            ArgumentNullException.ThrowIfNull(right);

            return DoubleMatrix.ElementWiseMultiply(left.matrix, right.matrix);
        }

        /// <inheritdoc cref="DoubleMatrix.ElementWiseMultiply(DoubleMatrix, DoubleMatrix)"/>
        public static DoubleMatrix ElementWiseMultiply(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return DoubleMatrix.ElementWiseMultiply(left.matrix, right);
        }

        /// <inheritdoc cref="DoubleMatrix.ElementWiseMultiply(DoubleMatrix, DoubleMatrix)"/>
        public static DoubleMatrix ElementWiseMultiply(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return DoubleMatrix.ElementWiseMultiply(left, right.matrix);
        }

        #endregion

        #region Add

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator +(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            ArgumentNullException.ThrowIfNull(right);

            return left.matrix + right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Add(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator +(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Add(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator +(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left + right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Add(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left + right;
        }

        #region Double

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,double)"/>
        public static DoubleMatrix operator +(ReadOnlyDoubleMatrix left, double right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,double)"/>
        public static DoubleMatrix Add(ReadOnlyDoubleMatrix left, double right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(double,DoubleMatrix)"/>
        public static DoubleMatrix operator +(double left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left + right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(double,DoubleMatrix)"/>
        public static DoubleMatrix Add(double left, ReadOnlyDoubleMatrix right)
        {
            return left + right;
        }

        #endregion

        #region Complex

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,Complex)"/>
        public static ComplexMatrix operator +(ReadOnlyDoubleMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(DoubleMatrix,Complex)"/>
        public static ComplexMatrix Add(ReadOnlyDoubleMatrix left, Complex right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(Complex,DoubleMatrix)"/>
        public static ComplexMatrix operator +(Complex left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left + right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Addition(Complex,DoubleMatrix)"/>
        public static ComplexMatrix Add(Complex left, ReadOnlyDoubleMatrix right)
        {
            return left + right;
        }

        #endregion

        #endregion

        #region Divide

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator /(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            ArgumentNullException.ThrowIfNull(right);

            return left.matrix / right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator /(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator /(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left / right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Divide(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Divide(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Divide(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left / right;
        }

        #region Double

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,double)"/>
        public static DoubleMatrix operator /(ReadOnlyDoubleMatrix left, double right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,double)"/>
        public static DoubleMatrix Divide(ReadOnlyDoubleMatrix left, double right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(double,DoubleMatrix)"/>
        public static DoubleMatrix operator /(double left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left / right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(double,DoubleMatrix)"/>
        public static DoubleMatrix Divide(double left, ReadOnlyDoubleMatrix right)
        {
            return left / right;
        }

        #endregion

        #region Complex

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,Complex)"/>
        public static ComplexMatrix operator /(ReadOnlyDoubleMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(DoubleMatrix,Complex)"/>
        public static ComplexMatrix Divide(ReadOnlyDoubleMatrix left, Complex right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(Complex,DoubleMatrix)"/>
        public static ComplexMatrix operator /(Complex left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left / right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Division(Complex,DoubleMatrix)"/>
        public static ComplexMatrix Divide(Complex left, ReadOnlyDoubleMatrix right)
        {
            return left / right;
        }

        #endregion

        #endregion

        #region Multiply

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator *(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            ArgumentNullException.ThrowIfNull(right);

            return left.matrix * right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator *(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator *(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left * right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Multiply(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Multiply(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Multiply(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left * right;
        }

        #region Double

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,double)"/>
        public static DoubleMatrix operator *(ReadOnlyDoubleMatrix left, double right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,double)"/>
        public static DoubleMatrix Multiply(ReadOnlyDoubleMatrix left, double right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(double,DoubleMatrix)"/>
        public static DoubleMatrix operator *(double left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left * right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(double,DoubleMatrix)"/>
        public static DoubleMatrix Multiply(double left, ReadOnlyDoubleMatrix right)
        {
            return left * right;
        }

        #endregion

        #region Complex

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,Complex)"/>
        public static ComplexMatrix operator *(ReadOnlyDoubleMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(DoubleMatrix,Complex)"/>
        public static ComplexMatrix Multiply(ReadOnlyDoubleMatrix left, Complex right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(Complex,DoubleMatrix)"/>
        public static ComplexMatrix operator *(Complex left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left * right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Multiply(Complex,DoubleMatrix)"/>
        public static ComplexMatrix Multiply(Complex left, ReadOnlyDoubleMatrix right)
        {
            return left * right;
        }

        #endregion

        #endregion

        #region Subtract

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator -(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            ArgumentNullException.ThrowIfNull(right);

            return left.matrix - right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator -(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix operator -(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left - right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Subtract(ReadOnlyDoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Subtract(ReadOnlyDoubleMatrix left, DoubleMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Subtract(DoubleMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left - right;
        }

        #region Double

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,double)"/>
        public static DoubleMatrix operator -(ReadOnlyDoubleMatrix left, double right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,double)"/>
        public static DoubleMatrix Subtract(ReadOnlyDoubleMatrix left, double right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(double,DoubleMatrix)"/>
        public static DoubleMatrix operator -(double left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left - right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(double,DoubleMatrix)"/>
        public static DoubleMatrix Subtract(double left, ReadOnlyDoubleMatrix right)
        {
            return left - right;
        }

        #endregion

        #region Complex

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,Complex)"/>
        public static ComplexMatrix operator -(ReadOnlyDoubleMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(DoubleMatrix,Complex)"/>
        public static ComplexMatrix Subtract(ReadOnlyDoubleMatrix left, Complex right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(Complex,DoubleMatrix)"/>
        public static ComplexMatrix operator -(Complex left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left - right.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_Subtraction(Complex,DoubleMatrix)"/>
        public static ComplexMatrix Subtract(Complex left, ReadOnlyDoubleMatrix right)
        {
            return left - right;
        }

        #endregion

        #endregion

        #region Negate

        /// <inheritdoc cref = "DoubleMatrix.op_UnaryNegation(DoubleMatrix)"/>
        public static DoubleMatrix operator -(ReadOnlyDoubleMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            return -matrix.matrix;
        }

        /// <inheritdoc cref = "DoubleMatrix.op_UnaryNegation(DoubleMatrix)"/>
        public static DoubleMatrix Negate(ReadOnlyDoubleMatrix matrix)
        {
            return -matrix;
        }

        #endregion

        #endregion

        #region Transpose

        /// <inheritdoc cref = "DoubleMatrix.Transpose()"/>
        public DoubleMatrix Transpose()
        {
            return this.matrix.Transpose();
        }

        #endregion

        #region Apply

        /// <inheritdoc cref = "DoubleMatrix.Apply(Func{double, double})"/>
        public DoubleMatrix Apply(Func<double, double> func)
        {
            return this.matrix.Apply(func);
        }

        #endregion

        #region IcomplexMatrixPatterns 

        /// <inheritdoc/>
        public bool IsHermitian
        {
            get { return this.matrix.IsHermitian; }
        }

        /// <inheritdoc/>
        public bool IsSkewHermitian
        {
            get { return this.matrix.IsSkewHermitian; }
        }

        #region IMatrixPatterns

        /// <inheritdoc/>
        public bool IsVector
        {
            get { return this.matrix.IsVector; }
        }

        /// <inheritdoc/>
        public bool IsRowVector
        {
            get { return this.matrix.IsRowVector; }
        }

        /// <inheritdoc/>
        public bool IsColumnVector
        {
            get { return this.matrix.IsColumnVector; }
        }

        /// <inheritdoc/>
        public bool IsScalar
        {
            get { return this.matrix.IsScalar; }
        }

        /// <inheritdoc/>
        public bool IsSquare
        {
            get { return this.matrix.IsSquare; }
        }

        /// <inheritdoc/>
        public bool IsDiagonal
        {
            get { return this.matrix.IsDiagonal; }
        }

        /// <inheritdoc/>
        public bool IsHessenberg
        {
            get { return this.matrix.IsHessenberg; }
        }

        /// <inheritdoc/>
        public bool IsLowerHessenberg
        {
            get { return this.matrix.IsLowerHessenberg; }
        }

        /// <inheritdoc/>
        public bool IsSymmetric
        {
            get { return this.matrix.IsSymmetric; }
        }

        /// <inheritdoc/>
        public bool IsSkewSymmetric
        {
            get { return this.matrix.IsSkewSymmetric; }
        }

        /// <inheritdoc/>
        public bool IsTriangular
        {
            get { return this.matrix.IsTriangular; }
        }

        /// <inheritdoc/>
        public bool IsLowerTriangular
        {
            get { return this.matrix.IsLowerTriangular; }
        }

        /// <inheritdoc/>
        public bool IsUpperTriangular
        {
            get { return this.matrix.IsUpperTriangular; }
        }

        /// <inheritdoc/>
        public bool IsTridiagonal
        {
            get { return this.matrix.IsTridiagonal; }
        }

        /// <inheritdoc/>
        public bool IsLowerBidiagonal
        {
            get
            {
                return this.matrix.IsLowerBidiagonal;
            }
        }

        /// <inheritdoc/>
        public bool IsUpperBidiagonal
        {
            get
            {
                return this.matrix.IsUpperBidiagonal;
            }
        }

        /// <inheritdoc/>
        public bool IsBidiagonal
        {
            get
            {
                return (this.IsLowerBidiagonal || this.IsUpperBidiagonal);
            }
        }

        /// <inheritdoc/>
        public bool IsUpperHessenberg
        {
            get { return this.matrix.IsUpperHessenberg; }
        }

        /// <inheritdoc/>
        public int LowerBandwidth
        {
            get { return this.matrix.LowerBandwidth; }
        }

        /// <inheritdoc/>
        public int UpperBandwidth
        {
            get { return this.matrix.UpperBandwidth; }
        }

        #endregion

        #endregion

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}" /> that can be used 
        /// to iterate through the collection.</returns>
        public IEnumerator<Double> GetEnumerator()
        {
            return new DoubleMatrixEnumerator(this.matrix);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object 
        /// that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DoubleMatrixEnumerator(this.matrix);
        }

        #endregion

        #region Storage

        /// <inheritdoc cref="DoubleMatrix.AsColumnMajorDenseArray"/>
        public Double[] AsColumnMajorDenseArray()
        {
            return this.matrix.AsColumnMajorDenseArray();
        }

        /// <inheritdoc cref="DoubleMatrix.StorageOrder"/>
        public StorageOrder StorageOrder
        {
            get { return this.matrix.StorageOrder; }
        }

        /// <inheritdoc cref="DoubleMatrix.StorageScheme"/>
        public StorageScheme StorageScheme
        {
            get { return this.matrix.StorageScheme; }
        }

        #endregion

        #region ITabularCollection

        /// <summary>
        /// Gets the elements of this instance corresponding to the 
        /// specified row and column indexes. 
        /// A <see cref="NotSupportedException"/> occurs if 
        /// you try to set the elements at the specified indexes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Because the collection is read-only, you can only get this item at the specified index. 
        /// An exception will occur if you try to set the item. 
        /// This member is an explicit interface member implementation. 
        /// It can be used only when the <see cref="ReadOnlyDoubleMatrix"/> instance is cast to
        /// an <see cref="ITabularCollection{TValue,TCollection}"/> interface.
        /// </para>
        /// </remarks>
        /// <exception cref="NotSupportedException">
        /// Always thrown if the property is set.
        /// </exception>
        /// <inheritdoc cref="IReadOnlyTabularCollection{TValue,TCollection}.this[string,string]" 
        /// path="param|value|exception"/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[string rowIndexes, string columnIndexes]
        {
            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <summary>
        /// Gets the elements of this instance corresponding to the 
        /// specified row and column indexes. 
        /// An<see cref="NotSupportedException"/> occurs if 
        /// you try to set the elements at the specified indexes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Because the collection is read-only, you can only get this item at the specified index. 
        /// An exception will occur if you try to set the item. 
        /// This member is an explicit interface member implementation. 
        /// It can be used only when the <see cref="ReadOnlyDoubleMatrix"/> instance is cast to
        /// an <see cref="ITabularCollection{TValue,TCollection}"/> interface.
        /// </para>
        /// </remarks>
        /// <exception cref="NotSupportedException">
        /// Always thrown if the property is set.
        /// </exception>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[string rowIndexes, IndexCollection columnIndexes]
        {
            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[string rowIndexes, int columnIndex]
        {
            get => this[rowIndexes, columnIndex];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[IndexCollection rowIndexes, string columnIndexes]
        {
            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[IndexCollection rowIndexes, int columnIndex]
        {
            get => this[rowIndexes, columnIndex];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[int rowIndex, string columnIndexes]
        {
            get => this[rowIndex, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        DoubleMatrix ITabularCollection<double, DoubleMatrix>.this[int rowIndex, IndexCollection columnIndexes]
        {
            get => this[rowIndex, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        double ITabularCollection<double, DoubleMatrix>.this[int rowIndex, int columnIndex]
        {
            get => this[rowIndex, columnIndex];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        #endregion

        #region Vec

        /// <summary>
        /// Gets the entry of this instance corresponding to the specified linear
        /// index.
        /// </summary>
        /// <param name="linearIndex">The zero-based linear index of the entry to get.</param>
        /// <inheritdoc cref="DoubleMatrix.this[int]"/>
        public double this[int linearIndex]
        {
            get { return this.matrix[linearIndex]; }
        }

        /// <inheritdoc cref="DoubleMatrix.Vec()"/>
        public DoubleMatrix Vec()
        {
            return this.matrix.Vec();
        }

        /// <inheritdoc cref="DoubleMatrix.Vec(IndexCollection)"/>
        public DoubleMatrix Vec(IndexCollection linearIndexes)
        {
            return this.matrix.Vec(linearIndexes);
        }

        #endregion

        #region IReadOnlyTabularCollection

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.NumberOfColumns"/>
        public int NumberOfColumns
        {
            get { return this.matrix.implementor.NumberOfColumns; }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.NumberOfRows"/>
        public int NumberOfRows
        {
            get { return this.matrix.implementor.NumberOfRows; }
        }

        #region this[int, *]

        /// <inheritdoc />
        public double this[int rowIndex, int columnIndex]
        {
            get { return this.matrix[rowIndex, columnIndex]; }
        }

        /// <inheritdoc />
        public DoubleMatrix this[int rowIndex, IndexCollection columnIndexes]
        {
            get { return this.matrix[rowIndex, columnIndexes]; }
        }

        /// <inheritdoc />
        public DoubleMatrix this[int rowIndex, string columnIndexes]
        {
            get { return this.matrix[rowIndex, columnIndexes]; }
        }

        #endregion

        #region IndexCollection, *

        /// <inheritdoc />
        public DoubleMatrix this[IndexCollection rowIndexes, int columnIndex]
        {
            get { return this.matrix[rowIndexes, columnIndex]; }
        }

        /// <inheritdoc />
        public DoubleMatrix this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        /// <inheritdoc />
        public DoubleMatrix this[IndexCollection rowIndexes, string columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        #endregion

        #region string, *

        /// <inheritdoc />
        public DoubleMatrix this[string rowIndexes, int columnIndex]
        {
            get { return this.matrix[rowIndexes, columnIndex]; }
        }

        /// <inheritdoc />
        public DoubleMatrix this[string rowIndexes, IndexCollection columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        /// <inheritdoc />
        public DoubleMatrix this[string rowIndexes, string columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        #endregion

        #endregion

        #region IList

        /// <summary>
        /// Gets a value indicating whether this instance is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get => true; }


        /// <summary>
        /// Gets the element at the specified index. 
        /// A <see cref="NotSupportedException"/> occurs if you try to set the item 
        /// at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <value>The element at the specified index.</value>
        /// <exception cref="NotSupportedException">
        /// Always thrown if the property is set.
        /// </exception>
        double IList<double>.this[int index]
        {
            get => this.matrix[index];
            set => throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public int IndexOf(double item)
        {
            return this.matrix.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"></see> at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="IList{T}"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<double>.Insert(int index, double item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes the <see cref="IList{T}"></see> item at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<double>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region ICollection

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item">The object to add to 
        /// the <see cref="ICollection{T}"></see>.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<double>.Add(double item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<double>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(double item)
        {
            return this.matrix.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(double[] array, int arrayIndex)
        {
            this.matrix.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from 
        /// the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item">The object to remove from 
        /// the <see cref="ICollection{T}"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> was successfully 
        /// removed from the <see cref="ICollection{T}"></see>; 
        /// otherwise, false. This method also returns false 
        /// if <paramref name="item">item</paramref> is not found in the 
        /// original <see cref="ICollection{T}"></see>.</returns>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        bool ICollection<double>.Remove(double item)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
