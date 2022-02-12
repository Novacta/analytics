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
    /// Represents a read only collection of complex values arranged in rows and columns.
    /// Provides methods to operate algebraically on matrices when operands are
    /// both writable and read only.
    ///</summary>
    ///<seealso cref="ComplexMatrix"/>
    public class ReadOnlyComplexMatrix :
        IEnumerable<Complex>,
        IList<Complex>,
        IReadOnlyList<Complex>,
        IComplexMatrixPatterns,
        IReadOnlyTabularCollection<Complex, ComplexMatrix>,
        ITabularCollection<Complex, ComplexMatrix>
    {
        internal ComplexMatrix matrix;

        #region Constructors

        internal ReadOnlyComplexMatrix(ComplexMatrix matrix)
        {
            this.matrix = matrix;
        }

        #endregion

        #region Conversion operators

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="ReadOnlyComplexMatrix"/> to <see cref="System.Double"/>.
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
        public static explicit operator Complex(ReadOnlyComplexMatrix value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return (Complex)value.matrix;
        }

        /// <summary>
        /// Converts 
        /// from <see cref="ReadOnlyComplexMatrix"/> to <see cref="System.Double"/>.
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
        public static Complex ToComplex(ReadOnlyComplexMatrix value)
        {
            return (Complex)value;
        }

        #endregion

        #region Names

        /// <inheritdoc cref="ComplexMatrix.Name"/>
        public String Name { get { return this.matrix.Name; } }

        #region Rows

        /// <inheritdoc cref="ComplexMatrix.HasRowNames"/>
        public bool HasRowNames { get { return this.matrix.HasRowNames; } }

        /// <inheritdoc cref="ComplexMatrix.TryGetRowName(int, out string)"/>
        public bool TryGetRowName(int rowIndex, out string rowName)
        {
            return this.matrix.TryGetRowName(rowIndex, out rowName);
        }

        /// <inheritdoc cref="ComplexMatrix.RowNames"/>
        public ReadOnlyDictionary<int, string> RowNames
        {
            get { return this.matrix.RowNames; }
        }

        #endregion

        #region Columns

        /// <inheritdoc cref="ComplexMatrix.HasColumnNames"/>
        public bool HasColumnNames { get { return this.matrix.HasColumnNames; } }

        /// <inheritdoc cref="ComplexMatrix.TryGetColumnName(int, out string)"/>
        public bool TryGetColumnName(int columnIndex, out string columnName)
        {
            return this.matrix.TryGetColumnName(columnIndex, out columnName);
        }

        /// <inheritdoc cref="ComplexMatrix.ColumnNames"/>
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

        /// <inheritdoc cref="ComplexMatrix.Find(Complex)"/>
        public IndexCollection Find(Complex value)
        {
            return this.matrix.Find(value);
        }

        #endregion

        #region Nonzero

        /// <inheritdoc cref="ComplexMatrix.FindNonzero()"/>
        public IndexCollection FindNonzero()
        {
            return this.matrix.FindNonzero();
        }

        #endregion

        #region While

        /// <inheritdoc cref="ComplexMatrix.FindWhile(Predicate{Complex})"/>
        public IndexCollection FindWhile(Predicate<Complex> match)
        {
            return this.matrix.FindWhile(match);
        }

        #endregion

        #endregion

        #region ElementWiseMultiply

        #region Complex, Complex

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(ComplexMatrix, ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return ComplexMatrix.ElementWiseMultiply(left.matrix, right.matrix);
        }

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(ComplexMatrix, ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return ComplexMatrix.ElementWiseMultiply(left.matrix, right);
        }

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(ComplexMatrix, ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return ComplexMatrix.ElementWiseMultiply(left, right.matrix);
        }

        #endregion

        #region Complex, Double

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(ComplexMatrix, DoubleMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return ComplexMatrix.ElementWiseMultiply(left.matrix, right.matrix);
        }

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(ComplexMatrix, DoubleMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return ComplexMatrix.ElementWiseMultiply(left.matrix, right);
        }

        #endregion

        #region Double, Complex

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(DoubleMatrix, ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return ComplexMatrix.ElementWiseMultiply(left.matrix, right.matrix);
        }

        /// <inheritdoc cref="ComplexMatrix.ElementWiseMultiply(DoubleMatrix, ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return ComplexMatrix.ElementWiseMultiply(left, right.matrix);
        }

        #endregion

        #endregion

        #region Add

        #region Complex, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator +(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix + right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator +(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator +(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left + right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,Complex)"/>
        public static ComplexMatrix operator +(ReadOnlyComplexMatrix left, Complex right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Add(ReadOnlyComplexMatrix left, Complex right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(Complex,ComplexMatrix)"/>
        public static ComplexMatrix operator +(Complex left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left + right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Add(Complex left, ReadOnlyComplexMatrix right)
        {
            return left + right;
        }

        #endregion

        #region Double, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator +(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix + right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator +(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left + right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left + right;
        }

        #endregion

        #region Complex, Double 

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator +(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix + right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Add(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator +(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix + right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Add(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            return left + right;
        }

        #endregion

        #endregion

        #region Divide

        #region Complex, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator /(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix / right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator /(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator /(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left / right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,Complex)"/>
        public static ComplexMatrix operator /(ReadOnlyComplexMatrix left, Complex right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Divide(ReadOnlyComplexMatrix left, Complex right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(Complex,ComplexMatrix)"/>
        public static ComplexMatrix operator /(Complex left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left / right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Divide(Complex left, ReadOnlyComplexMatrix right)
        {
            return left / right;
        }

        #endregion

        #region Double, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator /(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix / right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator /(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left / right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left / right;
        }

        #endregion

        #region Complex, Double 

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator /(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix / right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Divide(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator /(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix / right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Divide(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            return left / right;
        }

        #endregion

        #endregion

        #region Multiply

        #region Complex, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator *(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix * right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator *(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator *(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left * right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,Complex)"/>
        public static ComplexMatrix operator *(ReadOnlyComplexMatrix left, Complex right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Multiply(ReadOnlyComplexMatrix left, Complex right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(Complex,ComplexMatrix)"/>
        public static ComplexMatrix operator *(Complex left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left * right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(Complex left, ReadOnlyComplexMatrix right)
        {
            return left * right;
        }

        #endregion

        #region Double, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator *(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix * right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator *(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left * right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left * right;
        }

        #endregion

        #region Complex, Double 

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator *(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix * right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Multiply(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator *(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix * right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Multiply(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            return left * right;
        }

        #endregion

        #endregion

        #region Subtract

        #region Complex, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix - right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator -(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left - right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(ReadOnlyComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(ReadOnlyComplexMatrix left, ComplexMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(ComplexMatrix left, ReadOnlyComplexMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,Complex)"/>
        public static ComplexMatrix operator -(ReadOnlyComplexMatrix left, Complex right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Subtract(ReadOnlyComplexMatrix left, Complex right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(Complex,ComplexMatrix)"/>
        public static ComplexMatrix operator -(Complex left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left - right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(Complex left, ReadOnlyComplexMatrix right)
        {
            return left - right;
        }

        #endregion

        #region Double, Complex

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix - right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(ReadOnlyDoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator -(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left - right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(DoubleMatrix left, ReadOnlyComplexMatrix right)
        {
            return left - right;
        }

        #endregion

        #region Complex, Double 

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            return left.matrix - right.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Subtract(ReadOnlyComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            return left.matrix - right;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Subtract(ReadOnlyComplexMatrix left, DoubleMatrix right)
        {
            return left - right;
        }

        #endregion

        #endregion

        #region Negate

        /// <inheritdoc cref = "ComplexMatrix.op_UnaryNegation(ComplexMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyComplexMatrix matrix)
        {
            if (matrix is null)
                throw new ArgumentNullException(nameof(matrix));

            return -matrix.matrix;
        }

        /// <inheritdoc cref = "ComplexMatrix.op_UnaryNegation(ComplexMatrix)"/>
        public static ComplexMatrix Negate(ReadOnlyComplexMatrix matrix)
        {
            return -matrix;
        }

        #endregion

        #endregion

        #region Conjugate

        /// <inheritdoc cref = "ComplexMatrix.Conjugate()"/>
        public ComplexMatrix Conjugate()
        {
            return this.matrix.Conjugate();
        }

        #endregion

        #region ConjugateTranspose

        /// <inheritdoc cref = "ComplexMatrix.ConjugateTranspose()"/>
        public ComplexMatrix ConjugateTranspose()
        {
            return this.matrix.ConjugateTranspose();
        }

        #endregion

        #region Transpose

        /// <inheritdoc cref = "ComplexMatrix.Transpose()"/>
        public ComplexMatrix Transpose()
        {
            return this.matrix.Transpose();
        }

        #endregion

        #region Apply

        /// <inheritdoc cref = "ComplexMatrix.Apply(Func{Complex, Complex})"/>
        public ComplexMatrix Apply(Func<Complex, Complex> func)
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
        public IEnumerator<Complex> GetEnumerator()
        {
            return new ComplexMatrixEnumerator(this.matrix);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object 
        /// that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ComplexMatrixEnumerator(this.matrix);
        }

        #endregion

        #region Storage

        /// <inheritdoc cref="ComplexMatrix.AsColumnMajorDenseArray"/>
        public Complex[] AsColumnMajorDenseArray()
        {
            return this.matrix.AsColumnMajorDenseArray();
        }

        /// <inheritdoc cref="ComplexMatrix.StorageOrder"/>
        public StorageOrder StorageOrder
        {
            get { return this.matrix.StorageOrder; }
        }

        /// <inheritdoc cref="ComplexMatrix.StorageScheme"/>
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
        /// It can be used only when the <see cref="ReadOnlyComplexMatrix"/> instance is cast to
        /// an <see cref="ITabularCollection{TValue,TCollection}"/> interface.
        /// </para>
        /// </remarks>
        /// <exception cref="NotSupportedException">
        /// Always thrown if the property is set.
        /// </exception>
        /// <inheritdoc cref="IReadOnlyTabularCollection{TValue,TCollection}.this[string,string]" 
        /// path="param|value|exception"/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[string rowIndexes, string columnIndexes]
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
        /// It can be used only when the <see cref="ReadOnlyComplexMatrix"/> instance is cast to
        /// an <see cref="ITabularCollection{TValue,TCollection}"/> interface.
        /// </para>
        /// </remarks>
        /// <exception cref="NotSupportedException">
        /// Always thrown if the property is set.
        /// </exception>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[string rowIndexes, IndexCollection columnIndexes]
        {
            //    /// <inheritdoc cref="IReadOnlyTabularCollection{TValue,TCollection}.this[string,IndexCollection]" 
            ///// path="param|value|exception"/>

            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[string rowIndexes, int columnIndex]
        {
            get => this[rowIndexes, columnIndex];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[IndexCollection rowIndexes, string columnIndexes]
        {
            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get => this[rowIndexes, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[IndexCollection rowIndexes, int columnIndex]
        {
            get => this[rowIndexes, columnIndex];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[int rowIndex, string columnIndexes]
        {
            get => this[rowIndex, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        ComplexMatrix ITabularCollection<Complex, ComplexMatrix>.this[int rowIndex, IndexCollection columnIndexes]
        {
            get => this[rowIndex, columnIndexes];
            set => throw new NotSupportedException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_TAB_IS_READ_ONLY"));
        }

        /// <inheritdoc/>
        Complex ITabularCollection<Complex, ComplexMatrix>.this[int rowIndex, int columnIndex]
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
        /// <inheritdoc cref="ComplexMatrix.this[int]"/>
        public Complex this[int linearIndex]
        {
            get { return this.matrix[linearIndex]; }
        }

        /// <inheritdoc cref="ComplexMatrix.Vec()"/>
        public ComplexMatrix Vec()
        {
            return this.matrix.Vec();
        }

        /// <inheritdoc cref="ComplexMatrix.Vec(IndexCollection)"/>
        public ComplexMatrix Vec(IndexCollection linearIndexes)
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
        public Complex this[int rowIndex, int columnIndex]
        {
            get { return this.matrix[rowIndex, columnIndex]; }
        }

        /// <inheritdoc />
        public ComplexMatrix this[int rowIndex, IndexCollection columnIndexes]
        {
            get { return this.matrix[rowIndex, columnIndexes]; }
        }

        /// <inheritdoc />
        public ComplexMatrix this[int rowIndex, string columnIndexes]
        {
            get { return this.matrix[rowIndex, columnIndexes]; }
        }

        #endregion

        #region IndexCollection, *

        /// <inheritdoc />
        public ComplexMatrix this[IndexCollection rowIndexes, int columnIndex]
        {
            get { return this.matrix[rowIndexes, columnIndex]; }
        }

        /// <inheritdoc />
        public ComplexMatrix this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        /// <inheritdoc />
        public ComplexMatrix this[IndexCollection rowIndexes, string columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        #endregion

        #region string, *

        /// <inheritdoc />
        public ComplexMatrix this[string rowIndexes, int columnIndex]
        {
            get { return this.matrix[rowIndexes, columnIndex]; }
        }

        /// <inheritdoc />
        public ComplexMatrix this[string rowIndexes, IndexCollection columnIndexes]
        {
            get { return this.matrix[rowIndexes, columnIndexes]; }
        }

        /// <inheritdoc />
        public ComplexMatrix this[string rowIndexes, string columnIndexes]
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
        Complex IList<Complex>.this[int index]
        {
            get => this.matrix[index];
            set => throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public int IndexOf(Complex item)
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
        void IList<Complex>.Insert(int index, Complex item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes the <see cref="IList{T}"></see> item at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<Complex>.RemoveAt(int index)
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
        void ICollection<Complex>.Add(Complex item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<Complex>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(Complex item)
        {
            return this.matrix.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(Complex[] array, int arrayIndex)
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
        bool ICollection<Complex>.Remove(Complex item)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}