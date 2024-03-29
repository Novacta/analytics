﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Numerics;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Implements the three-array variation of the Compressed Sparse Row 
    /// storage format, with zero-based indexing.
    /// </summary>  
    [Serializable]
    internal sealed class SparseCsr3ComplexMatrixImplementor : MatrixImplementor<Complex>
    {
        internal Complex[] values;
        internal int[] columns;
        internal int[] rowIndex;
        internal int numberOfColumns;
        internal int numberOfRows;


        // Maximum number of storage positions
        internal int capacity;

        private static readonly Complex[] emptyValues = Array.Empty<Complex>();
        private static readonly int[] emptyColumns = Array.Empty<int>();

        #region Constructors

        internal SparseCsr3ComplexMatrixImplementor(int numberOfRows,
            int numberOfColumns, int capacity)
        {
            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);
            Debug.Assert(capacity >= 0);

            int length = numberOfRows * numberOfColumns;
            if (capacity > length)
                capacity = length;

            if (capacity == 0)
            {
                this.values = emptyValues;
                this.columns = emptyColumns;
            }
            else
            {
                this.values = new Complex[capacity];
                this.columns = new int[capacity];
            }

            this.rowIndex = new int[numberOfRows + 1];

            // rowIndex entries are all zero 
            // since actually there are no stored positions             

            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            this.capacity = capacity;
        }

        #endregion

        #region Conversion operators

        public static implicit operator DenseComplexMatrixImplementor(
            SparseCsr3ComplexMatrixImplementor sparseComplexMatrixImplementor)
        {
            return new DenseComplexMatrixImplementor(
                sparseComplexMatrixImplementor.numberOfRows,
                sparseComplexMatrixImplementor.numberOfColumns,
                sparseComplexMatrixImplementor.AsColumnMajorDenseArray(),
                StorageOrder.ColumnMajor);
        }

        #endregion

        #region ICloneable

        public sealed override object Clone()
        {
            var implementorClone = new SparseCsr3ComplexMatrixImplementor(
                this.numberOfRows, this.numberOfColumns, this.capacity);

            int numberOfStoredPositions = this.rowIndex[this.numberOfRows];
            if (0 < numberOfStoredPositions)
            {
                this.values.CopyTo(implementorClone.values, 0);
                this.columns.CopyTo(implementorClone.columns, 0);
                this.rowIndex.CopyTo(implementorClone.rowIndex, 0);
            }

            return implementorClone;
        }

        #endregion

        #region Indexers

        internal Complex GetValue(int row, int column)
        {
            var rowIndex = this.rowIndex;
            var columns = this.columns;
            Complex value = 0.0;
            int firstRowPosition = rowIndex[row];
            for (int i = firstRowPosition; i < rowIndex[row + 1]; i++)
            {
                if (columns[i] == column)
                {
                    value = this.values[i];
                    break;
                }
            }

            return value;
        }

        internal Complex GetValue(int linear)
        {
            IndexCollection.ConvertToTabularIndexes(
                linear,
                this.numberOfRows,
                out int row,
                out int column);
            return this.GetValue(row, column);
        }

        /// <summary>
        /// Tries to get the storage position of the matrix entry corresponding to a given row and column.
        /// </summary>
        /// <param name="row">The row of the given entry.</param>
        /// <param name="column">The column of the given entry.</param>
        /// <param name="positionIndex">Index of the position in the CSR3 storage.</param>
        /// <remarks>
        /// If the method returns <c>true</c>, then <paramref name="positionIndex"/> is actually valid in the current
        /// sparse allocated storage. Otherwise, the current storage is not saving a place for the given position.
        /// In this case, it must be reallocated with enhanced capacity, and <paramref name="positionIndex"/>
        /// represents the index which corresponds to the position in the new storage.
        /// </remarks>
        /// <returns><c>true</c> if the position is already stored, <c>false</c> otherwise.</returns>
        internal bool TryGetPosition(int row, int column, out int positionIndex)
        {
            bool isPositionStored = false;

            var rowIndex = this.rowIndex;
            var columns = this.columns;

            // The position index is the first index of the next row, 
            // if parameter column is greater than all indexes in this.columns 
            // corresponding to parameter row 
            positionIndex = rowIndex[row + 1];

            int firstRowPosition = rowIndex[row];
            int currentColumn;
            for (int i = firstRowPosition; i < rowIndex[row + 1]; i++)
            {
                currentColumn = columns[i];
                if (currentColumn >= column)
                {
                    positionIndex = i;
                    if (currentColumn == column)
                    {
                        isPositionStored = true;
                    }
                    break;
                }
            }

            return isPositionStored;
        }

        internal bool TryGetPosition(int linear, out int positionIndex)
        {
            IndexCollection.ConvertToTabularIndexes(
                linear,
                this.numberOfRows,
                out int row,
                out int column);

            return this.TryGetPosition(row, column, out positionIndex);
        }

        internal void SetValue(int row, int column, Complex value)
        {
            bool isPositionStored = this.TryGetPosition(row, column, out int positionIndex);
            var rowIndex = this.rowIndex;
            var columns = this.columns;
            var values = this.values;
            int numberOfStoredPositions = rowIndex[this.numberOfRows];

            if (!isPositionStored)
            {
                // No op for zero values: there's no point in adding storage
                // for a position corresponding to a zero
                if (0.0 == value)
                {
                    return;
                }

                // Reallocate sparse storage

                // Add capacity if capacity <= numberOfStoredPositions

                int newCapacity;
                if (this.capacity <= numberOfStoredPositions)
                {
                    newCapacity = (this.capacity == 0) ? this.numberOfRows : this.capacity * 2;

                    int length = this.Count;
                    if ((uint)newCapacity > length)
                        newCapacity = length;

                    var newColumns = new int[newCapacity];
                    var newValues = new Complex[newCapacity];

                    // Copy current storage from columns and values

                    for (int j = 0; j < positionIndex; j++)
                    {
                        newColumns[j] = columns[j];
                        newValues[j] = values[j];
                    }

                    for (int j = positionIndex + 1; j <= numberOfStoredPositions; j++)
                    {
                        newColumns[j] = columns[j - 1];
                        newValues[j] = values[j - 1];
                    }

                    // Update rowIndex info

                    for (int i = row + 1; i <= this.numberOfRows; i++)
                    {
                        rowIndex[i] += 1;
                    }

                    // Set as new storage

                    this.capacity = newCapacity;
                    this.columns = newColumns;
                    this.values = newValues;
                }
                else
                { // Here if and only if enough storage is already allocated

                    //  Update columns and values

                    for (int j = numberOfStoredPositions; j > positionIndex; j--)
                    {
                        columns[j] = columns[j - 1];
                        values[j] = values[j - 1];
                    }

                    // Update rowIndex info

                    for (int i = row + 1; i <= this.numberOfRows; i++)
                    {
                        rowIndex[i] += 1;
                    }

                }
            }

            this.columns[positionIndex] = column;
            this.values[positionIndex] = value;
        }

        internal void SetValue(int linear, Complex value)
        {
            IndexCollection.ConvertToTabularIndexes(
                linear,
                this.numberOfRows,
                out int row,
                out int column);
            this.SetValue(row, column, value);
        }

        #region Linear indexers

        internal sealed override Complex this[int linearIndex]
        {
            get
            {
                // Check if the linear index is outside the range defined by the matrix dimensions
                if (linearIndex < 0 || this.Count <= linearIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(linearIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                return this.GetValue(linearIndex);
            }
            set
            {
                if (linearIndex < 0 || this.Count <= linearIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(linearIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                this.SetValue(linearIndex, value);
            }
        }

        internal sealed override MatrixImplementor<Complex> this[IndexCollection linearIndexes]
        {
            get
            {
                // Check if any linearIndex is outside the range defined by the matrix dimensions
                if (linearIndexes.maxIndex >= this.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(linearIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int[] linears = linearIndexes.indexes;

                DenseComplexMatrixImplementor subMatrix = new
(linears.Length, 1);
                Complex[] subStorage = subMatrix.storage;
                var values = this.values;

                for (int l = 0; l < linears.Length; l++)
                {
                    if (this.TryGetPosition(linears[l], out int positionIndex))
                    {
                        subStorage[l] = values[positionIndex];
                    }
                }

                return subMatrix;
            }
        }

        internal sealed override MatrixImplementor<Complex> this[string linearIndexes]
        {
            get
            {
                int length = this.Count;
                DenseComplexMatrixImplementor subMatrix = new
(length, 1);
                Complex[] subStorage = subMatrix.storage;

                var values = this.values;

                for (int l = 0; l < length; l++)
                {
                    subStorage[l] = this.GetValue(l);
                    if (this.TryGetPosition(l, out int positionIndex))
                    {
                        subStorage[l] = values[positionIndex];
                    }
                }

                return subMatrix;
            }
        }

        #endregion

        #region Tabular indexers

        #region this[int, *]

        internal sealed override Complex this[int rowIndex, int columnIndex]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                return this.GetValue(rowIndex, columnIndex);
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                this.SetValue(rowIndex, columnIndex, value);
            }
        }

        internal sealed override MatrixImplementor<Complex> this[int rowIndex, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int[] columns = columnIndexes.indexes;
                int columnsLength = columns.Length;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(columnsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(1, columnsLength, subCapacity);

                var thisValues = this.values;

                for (int j = 0; j < columnsLength; j++)
                {
                    if (this.TryGetPosition(rowIndex, columns[j], out int positionIndex))
                    {
                        subMatrix.SetValue(0, j, thisValues[positionIndex]);
                    }
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(1, columnIndexes.Count, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int[] columns = columnIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;


                            for (int j = 0; j < columns.Length; j++)
                            {
                                if (source.TryGetPosition(0, j, out int index))
                                {
                                    this.SetValue(rowIndex, columns[j], sourceValues[index]);
                                }

                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                this.SetValue(rowIndex, columns[j], sourceStorage[j]);
                            }
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<Complex> this[int rowIndex, string columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int columnsLength = this.numberOfColumns;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(columnsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(1, columnsLength, subCapacity);

                var thisValues = this.values;

                for (int j = 0; j < columnsLength; j++)
                {
                    if (this.TryGetPosition(rowIndex, j, out int positionIndex))
                    {
                        subMatrix.SetValue(0, j, thisValues[positionIndex]);
                    }
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(1, this.numberOfColumns, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int columnsLength = this.numberOfColumns;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                if (source.TryGetPosition(0, j, out int index))
                                {
                                    this.SetValue(rowIndex, j, sourceValues[index]);
                                }

                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                this.SetValue(rowIndex, j, sourceStorage[j]);
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        #region this[IndexCollection, *]

        internal sealed override MatrixImplementor<Complex> this[IndexCollection rowIndexes, int columnIndex]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int[] rows = rowIndexes.indexes;
                int rowsLength = rows.Length;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(rowsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(rowsLength, 1, subCapacity);

                var thisValues = this.values;

                for (int i = 0; i < rowsLength; i++)
                {
                    if (this.TryGetPosition(rows[i], columnIndex, out int positionIndex))
                    {
                        subMatrix.SetValue(i, 0, thisValues[positionIndex]);
                    }
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(rowIndexes.Count, 1, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int[] rows = rowIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;
                            for (int i = 0; i < rows.Length; i++)
                                if (source.TryGetPosition(i, 0, out int index))
                                {
                                    this.SetValue(rows[i], columnIndex, sourceValues[index]);
                                }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            for (int i = 0; i < rows.Length; i++)
                                this.SetValue(rows[i], columnIndex, sourceStorage[i]);
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<Complex> this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int[] rows = rowIndexes.indexes;
                int[] columns = columnIndexes.indexes;
                int rowsLength = rows.Length;
                int columnsLength = columns.Length;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(rowsLength * columnsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(rowsLength, columnsLength, subCapacity);

                var thisValues = this.values;

                for (int i = 0; i < rowsLength; i++)
                {
                    for (int j = 0; j < columnsLength; j++)
                    {
                        if (this.TryGetPosition(rows[i], columns[j], out int positionIndex))
                        {
                            subMatrix.SetValue(i, j, thisValues[positionIndex]);
                        }
                    }
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    rowIndexes.Count, columnIndexes.Count, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int column;
                int[] rows = rowIndexes.indexes;
                int[] columns = columnIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                column = columns[j];
                                for (int i = 0; i < rows.Length; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.SetValue(rows[i], column, sourceValues[index]);
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            int index = 0;
                            for (int j = 0; j < columns.Length; j++)
                            {
                                column = columns[j];
                                for (int i = 0; i < rows.Length; i++, index++)
                                    this.SetValue(rows[i], column, sourceStorage[index]);
                            }
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<Complex> this[IndexCollection rowIndexes, string columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString("STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int[] rows = rowIndexes.indexes;
                int rowsLength = rows.Length;
                int columnsLength = this.numberOfColumns;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(rowsLength * columnsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(rowsLength, columnsLength, subCapacity);

                var thisValues = this.values;

                for (int i = 0; i < rowsLength; i++)
                {
                    for (int j = 0; j < columnsLength; j++)
                    {
                        if (this.TryGetPosition(rows[i], j, out int positionIndex))
                        {
                            subMatrix.SetValue(i, j, thisValues[positionIndex]);
                        }
                    }
                }


                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(rowIndexes.Count, this.numberOfColumns, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int[] rows = rowIndexes.indexes;
                int columnsLength = this.numberOfColumns;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                for (int i = 0; i < rows.Length; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.SetValue(rows[i], j, sourceValues[index]);
                                    }

                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            int index = 0;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                for (int i = 0; i < rows.Length; i++, index++)
                                    this.SetValue(rows[i], j, sourceStorage[index]);
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        #region this[string, *]

        internal sealed override MatrixImplementor<Complex> this[string rowIndexes, int columnIndex]
        {
            get
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int rowsLength = thisNumberOfRows;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(rowsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(rowsLength, 1, subCapacity);

                var thisValues = this.values;

                for (int i = 0; i < rowsLength; i++)
                {
                    if (this.TryGetPosition(i, columnIndex, out int positionIndex))
                    {
                        subMatrix.SetValue(i, 0, thisValues[positionIndex]);
                    }
                }

                return subMatrix;
            }
            set
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(thisNumberOfRows, 1, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int i = 0; i < thisNumberOfRows; i++)
                                if (source.TryGetPosition(i, 0, out int index))
                                {
                                    this.SetValue(i, columnIndex, sourceValues[index]);
                                }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            int index = 0;

                            for (int i = 0; i < thisNumberOfRows; i++, index++)
                                this.SetValue(i, columnIndex, sourceStorage[index]);
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<Complex> this[string rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int[] columns = columnIndexes.indexes;
                int rowsLength = thisNumberOfRows;
                int columnsLength = columns.Length;

                double sparsity = this.values.Length / this.Count;
                int subCapacity = Convert.ToInt32(Math.Ceiling(rowsLength * columnsLength * sparsity));
                var subMatrix = new
                    SparseCsr3ComplexMatrixImplementor(rowsLength, columnsLength, subCapacity);

                var thisValues = this.values;

                for (int i = 0; i < rowsLength; i++)
                {
                    for (int j = 0; j < columnsLength; j++)
                    {
                        if (this.TryGetPosition(i, columns[j], out int positionIndex))
                        {
                            subMatrix.SetValue(i, j, thisValues[positionIndex]);
                        }
                    }
                }


                return subMatrix;
            }
            set
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    thisNumberOfRows, columnIndexes.Count, value);

                MatrixImplementor<Complex> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<Complex>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int column;
                int[] columns = columnIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                column = columns[j];
                                for (int i = 0; i < thisNumberOfRows; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.SetValue(i, column, sourceValues[index]);
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            int index = 0;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                column = columns[j];
                                for (int i = 0; i < thisNumberOfRows; i++, index++)
                                    this.SetValue(i, column, sourceStorage[index]);
                            }
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<Complex> this[string rowIndexes, string columnIndexes]
        {
            get
            {
                return (SparseCsr3ComplexMatrixImplementor)this.Clone();
            }
            set
            {
                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    this.numberOfRows, this.numberOfColumns, value);

                // if the source is this, nothing has to be done
                if (object.ReferenceEquals(this, value))
                {
                    return;
                }

                MatrixImplementor<Complex> sourceImplementor = value;

                int thisNumberOfRows = this.numberOfRows;
                int rowsLength = thisNumberOfRows;
                int columnsLength = this.numberOfColumns;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3ComplexMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                for (int i = 0; i < rowsLength; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.SetValue(i, j, sourceValues[index]);
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            DenseComplexMatrixImplementor source
                                = (DenseComplexMatrixImplementor)sourceImplementor;
                            Complex[] sourceStorage = source.storage;

                            int index = 0;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                for (int i = 0; i < rowsLength; i++, index++)
                                    this.SetValue(i, j, sourceStorage[index]);
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Dimensions

        internal sealed override int NumberOfColumns
        {
            get
            {
                return this.numberOfColumns;
            }
        }

        internal sealed override int NumberOfRows
        {
            get
            {
                return this.numberOfRows;
            }
        }

        internal sealed override int Count
        {
            get
            {
                return this.numberOfRows * this.numberOfColumns;
            }
        }

        #endregion

        #region Patterns

        internal sealed override bool IsSymmetric
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                if (numberOfRows != numberOfColumns)
                    return false;

                int j;
                var values = this.values;
                var columns = this.columns;
                var rowIndex = this.rowIndex;
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        j = columns[p];
                        if (i != j)
                        {
                            if (this.TryGetPosition(j, i, out int symmetricPositionIndex))
                            {
                                if (values[p] != values[symmetricPositionIndex])
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (values[p] != 0.0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                return true;
            }
        }

        internal sealed override bool IsSkewSymmetric
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                if (numberOfRows != numberOfColumns)
                    return false;

                int j;
                var values = this.values;
                var columns = this.columns;
                var rowIndex = this.rowIndex;
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        j = columns[p];
                        if (i != j)
                        {
                            if (this.TryGetPosition(j, i, out int symmetricPositionIndex))
                            {
                                if (values[p] != -values[symmetricPositionIndex])
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (values[p] != 0.0)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (values[p] != 0.0)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
        }

        internal sealed override bool IsHermitian
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                if (numberOfRows != numberOfColumns)
                    return false;

                int j;
                var values = this.values;
                var columns = this.columns;
                var rowIndex = this.rowIndex;
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        var value = values[p];
                        j = columns[p];
                        if (i != j)
                        {
                            if (this.TryGetPosition(j, i, out int symmetricPositionIndex))
                            {
                                var oppositeValue = values[symmetricPositionIndex];
                                if (value.Real != oppositeValue.Real
                                    ||
                                    value.Imaginary != -oppositeValue.Imaginary)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (value.Real != 0.0 || value.Imaginary != 0.0)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (value.Imaginary != 0.0)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
        }

        internal sealed override bool IsSkewHermitian
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                if (numberOfRows != numberOfColumns)
                    return false;

                int j;
                var values = this.values;
                var columns = this.columns;
                var rowIndex = this.rowIndex;
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        var value = values[p];
                        j = columns[p];
                        if (i != j)
                        {
                            if (this.TryGetPosition(j, i, out int symmetricPositionIndex))
                            {
                                var oppositeValue = values[symmetricPositionIndex];
                                if (value.Real != -oppositeValue.Real
                                    ||
                                    value.Imaginary != oppositeValue.Imaginary)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (value.Real != 0.0 || value.Imaginary != 0.0)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (value.Real != 0.0)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
        }

        internal sealed override int UpperBandwidth
        {
            get
            {
                int upperBandwidth = 0;
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                int j, maxRowBandwidth, candidate;
                var values = this.values;
                var columns = this.columns;
                var rowIndex = this.rowIndex;
                for (int i = 0; i <= numberOfRows - 1; i++)
                {
                    maxRowBandwidth = numberOfColumns - i - 1;
                    if (upperBandwidth < maxRowBandwidth)
                    {
                        for (int p = rowIndex[i + 1] - 1; p >= rowIndex[i]; p--)
                        {
                            j = columns[p];
                            if (j > i && values[p] != 0.0)
                            {
                                candidate = j - i;
                                if (upperBandwidth < candidate)
                                {
                                    upperBandwidth = candidate;
                                    break;
                                }
                            }
                        }
                    }
                }

                return upperBandwidth;
            }
        }

        internal sealed override int LowerBandwidth
        {
            get
            {
                // The lower bandwidth of an MxN matrix A is the smallest number p, with 0<=p<M 
                // such that the entry A(i,j) vanishes whenever i > j + p. 

                int numberOfRows = this.numberOfRows;
                int lowerBandwidth = 0;

                int j, maxColumnBandwidth, candidate;
                var values = this.values;
                var columns = this.columns;
                var rowIndex = this.rowIndex;
                for (int i = numberOfRows - 1; i > 0; i--)
                {
                    maxColumnBandwidth = numberOfRows - i + 1;
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        j = columns[p];
                        if (lowerBandwidth < maxColumnBandwidth)
                        {
                            if (i > j && values[p] != 0.0)
                            {
                                candidate = i - j;
                                if (lowerBandwidth < candidate)
                                {
                                    lowerBandwidth = candidate;
                                    break;
                                }
                            }
                        }
                    }
                }

                return lowerBandwidth;
            }
        }

        #endregion

        #region Storage

        internal sealed override Complex[] AsColumnMajorDenseArray()
        {
            int length = this.Count;
            var fullStorage = new Complex[length];
            int numberOfRows = this.numberOfRows;
            int[] rowIndex = this.rowIndex;
            int numberOfStoredPositions = rowIndex[numberOfRows];
            if (numberOfStoredPositions != 0)
            {
                int[] columns = this.columns;
                Complex[] values = this.values;

                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        fullStorage[i + columns[p] * numberOfRows] = values[p];
                    }
                }
            }

            return fullStorage;
        }

        internal sealed override StorageOrder StorageOrder
        {
            get
            {
                return StorageOrder.RowMajor;
            }
        }

        internal sealed override Complex[] Storage
        {
            get
            {
                return this.values;
            }
        }

        internal sealed override StorageScheme StorageScheme
        {
            get
            {
                return StorageScheme.CompressedRow;
            }
        }

        #endregion
    }
}
