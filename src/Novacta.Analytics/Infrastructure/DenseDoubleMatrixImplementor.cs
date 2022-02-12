// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Novacta.Analytics.Infrastructure
{
    internal sealed class DenseDoubleMatrixImplementor :
        MatrixImplementor<double>
    {
        internal double[] storage;
        internal int numberOfColumns;
        internal int numberOfRows;
        internal StorageOrder storageOrder;
        internal StorageScheme storageScheme;

        #region Constructors

        internal DenseDoubleMatrixImplementor(double[,] data)
        {
            Debug.Assert(data is not null);

            int numberOfRows = data.GetLength(0);
            int numberOfColumns = data.GetLength(1);

            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);

            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;

            this.storageOrder = StorageOrder.ColumnMajor;
            this.storageScheme = StorageScheme.Dense;

            this.storage = new double[data.Length];

            int offset;
            int firstDataRow = data.GetLowerBound(0);
            int dataColumn = data.GetLowerBound(1);

            for (int j = 0; j < this.numberOfColumns; j++, dataColumn++)
            {
                offset = j * this.numberOfRows;
                int dataRow = firstDataRow;
                for (int i = 0; i < this.numberOfRows; i++, dataRow++)
                    this.storage[i + offset] = (double)(data.GetValue(dataRow, dataColumn));
            }
        }

        internal DenseDoubleMatrixImplementor(int numberOfRows, int numberOfColumns)
        {
            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);

            this.storage = new double[numberOfRows * numberOfColumns];
            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            this.storageOrder = StorageOrder.ColumnMajor;
            this.storageScheme = StorageScheme.Dense;
        }

        internal DenseDoubleMatrixImplementor(
            int numberOfRows,
            int numberOfColumns,
            double[] data,
            bool copyData)
        {
            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);

            Debug.Assert(data is not null);

            int matrixLength = numberOfRows * numberOfColumns;

            Debug.Assert(data.Length == matrixLength);

            if (copyData)
            {
                this.storage = new double[matrixLength];
                data.CopyTo(this.storage, 0);
            }
            else
            {
                this.storage = data;
            }

            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            this.storageOrder = StorageOrder.ColumnMajor;
            this.storageScheme = StorageScheme.Dense;
        }

        internal DenseDoubleMatrixImplementor(
            int numberOfRows,
            int numberOfColumns,
            double[] data,
            StorageOrder storageOrder)
        {
            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);

            Debug.Assert(data is not null);

            Debug.Assert(
                (StorageOrder.ColumnMajor == storageOrder)
                || (StorageOrder.RowMajor == storageOrder));

            int matrixLength = numberOfRows * numberOfColumns;

            Debug.Assert(data.Length == matrixLength);

            this.storage = new double[matrixLength];
            data.CopyTo(this.storage, 0);
            if (StorageOrder.RowMajor == storageOrder)
            {
                ImplementationServices.ConvertStorageToColMajorOrdered(
                    numberOfRows, numberOfColumns, this.storage);
            }

            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            this.storageOrder = StorageOrder.ColumnMajor;
            this.storageScheme = StorageScheme.Dense;
        }

        internal DenseDoubleMatrixImplementor(
            int numberOfRows,
            int numberOfColumns,
            IEnumerable<double> data,
            StorageOrder storageOrder)
        {
            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);
            Debug.Assert(data is not null);
            Debug.Assert(
                (StorageOrder.ColumnMajor == storageOrder)
                || (StorageOrder.RowMajor == storageOrder));

            int matrixLength = numberOfRows * numberOfColumns;

            Debug.Assert(data.Count() == matrixLength);

            double[] dataStorage = new double[matrixLength];

            int l = 0;
            foreach (double d in data)
            {
                dataStorage[l] = d;
                l++;
            }

            if (StorageOrder.RowMajor == storageOrder)
            {
                ImplementationServices.ConvertStorageToColMajorOrdered(
                    numberOfRows, numberOfColumns, dataStorage);
            }
            this.storage = dataStorage;


            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            this.storageOrder = StorageOrder.ColumnMajor;
            this.storageScheme = StorageScheme.Dense;
        }

        #endregion

        #region Conversion operators

        public static explicit operator DenseComplexMatrixImplementor(
            DenseDoubleMatrixImplementor denseDoubleMatrixImplementor)
        {
            return new DenseComplexMatrixImplementor(
                denseDoubleMatrixImplementor.numberOfRows,
                denseDoubleMatrixImplementor.numberOfColumns,
                ImplementationServices.ToComplexArray(
                    denseDoubleMatrixImplementor.storage),
                StorageOrder.ColumnMajor);
        }

        #endregion

        #region ICloneable

        public sealed override object Clone()
        {
            var implementorClone =
                new DenseDoubleMatrixImplementor(
                    this.numberOfRows,
                    this.numberOfColumns);
            this.storage.CopyTo(implementorClone.storage, 0);

            return implementorClone;
        }

        #endregion

        #region Indexers

        #region Linear indexers

        internal sealed override double this[int linearIndex]
        {
            get
            {
                // Check if the linear index is outside the range defined by the matrix dimensions
                if (linearIndex < 0 || this.storage.Length <= linearIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(linearIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                return this.storage[linearIndex];
            }
            set
            {
                if (linearIndex < 0 || this.storage.Length <= linearIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(linearIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                this.storage[linearIndex] = value;
            }
        }

        internal sealed override MatrixImplementor<double> this[IndexCollection linearIndexes]
        {
            get
            {
                // Check if any linearIndex is outside the range defined by the matrix dimensions
                if (linearIndexes.maxIndex >= this.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(linearIndexes),
                        ImplementationServices.GetResourceString("STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int[] linears = linearIndexes.indexes;

                DenseDoubleMatrixImplementor subMatrix = new(linears.Length, 1);
                double[] subStorage = subMatrix.storage;

                for (int l = 0; l < linears.Length; l++)
                {
                    subStorage[l] = this.storage[linears[l]];
                }

                return subMatrix;
            }
        }

        internal sealed override MatrixImplementor<double> this[string linearIndexes]
        {
            get
            {
                int storageLength = this.storage.Length;
                DenseDoubleMatrixImplementor subMatrix = new(this.storage.Length, 1);
                double[] subStorage = subMatrix.storage;

                for (int l = 0; l < storageLength; l++)
                {
                    subStorage[l] = this.storage[l];
                }

                return subMatrix;
            }
        }

        #endregion

        #region Tabular indexers

        #region this[int, *]

        internal sealed override double this[int rowIndex, int columnIndex]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                return this.storage[rowIndex + columnIndex * this.numberOfRows];
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                this.storage[rowIndex + columnIndex * this.numberOfRows] = value;
            }
        }

        internal sealed override MatrixImplementor<double> this[int rowIndex, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int[] columns = columnIndexes.indexes;
                int columnsLength = columns.Length;
                DenseDoubleMatrixImplementor subMatrix = new(1, columnsLength);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset;

                for (int j = 0; j < columnsLength; j++)
                {
                    offset = thisNumberOfRows * columns[j];
                    subStorage[j] = thisStorage[rowIndex + offset];
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    1, columnIndexes.Count, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int thisNumberOfRows = this.numberOfRows;

                int offset;
                int[] columns = columnIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                offset = thisNumberOfRows * columns[j];
                                this.storage[rowIndex + offset] = source.GetValue(j);
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                offset = thisNumberOfRows * columns[j];
                                this.storage[rowIndex + offset] = sourceStorage[j];
                            }
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<double> this[int rowIndex, string columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int columnsLength = this.numberOfColumns;
                var subMatrix = new DenseDoubleMatrixImplementor(1, columnsLength);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset;

                for (int j = 0; j < columnsLength; j++)
                {
                    offset = thisNumberOfRows * j;
                    subStorage[j] = thisStorage[rowIndex + offset];
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    1, this.numberOfColumns, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int thisNumberOfRows = this.numberOfRows;

                int offset;
                int columnsLength = this.numberOfColumns;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                offset = thisNumberOfRows * j;
                                this.storage[rowIndex + offset] = source.GetValue(j);
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                offset = thisNumberOfRows * j;
                                this.storage[rowIndex + offset] = sourceStorage[j];
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        #region this[IndexCollection, *]

        internal sealed override MatrixImplementor<double> this[IndexCollection rowIndexes, int columnIndex]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int[] rows = rowIndexes.indexes;
                int rowsLength = rows.Length;
                var subMatrix = new DenseDoubleMatrixImplementor(rowsLength, 1);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset = columnIndex * thisNumberOfRows;
                for (int i = 0; i < rowsLength; i++)
                    subStorage[i] = thisStorage[rows[i] + offset];

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    rowIndexes.Count, 1, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int thisNumberOfRows = this.numberOfRows;

                int offset;
                int[] rows = rowIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;
                            offset = thisNumberOfRows * columnIndex;
                            for (int i = 0; i < rows.Length; i++)
                                if (source.TryGetPosition(i, 0, out int index))
                                {
                                    this.storage[rows[i] + offset] = sourceValues[index];
                                }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;

                            offset = thisNumberOfRows * columnIndex;
                            for (int i = 0; i < rows.Length; i++)
                                this.storage[rows[i] + offset] = sourceStorage[i];
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<double> this[
            IndexCollection rowIndexes,
            IndexCollection columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int[] rows = rowIndexes.indexes;
                int[] columns = columnIndexes.indexes;
                int rowsLength = rows.Length;
                int columnsLength = columns.Length;
                var subMatrix = new DenseDoubleMatrixImplementor(rowsLength, columnsLength);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset, index = 0;

                for (int j = 0; j < columnsLength; j++)
                {
                    offset = thisNumberOfRows * columns[j];
                    for (int i = 0; i < rowsLength; i++, index++)
                        subStorage[index] = thisStorage[rows[i] + offset];
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    rowIndexes.Count, columnIndexes.Count, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int thisNumberOfRows = this.numberOfRows;

                int offset;
                int[] rows = rowIndexes.indexes;
                int[] columns = columnIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                offset = thisNumberOfRows * columns[j];
                                for (int i = 0; i < rows.Length; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.storage[rows[i] + offset] = sourceValues[index];
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;
                            int index = 0;
                            for (int j = 0; j < columns.Length; j++)
                            {
                                offset = thisNumberOfRows * columns[j];
                                for (int i = 0; i < rows.Length; i++, index++)
                                    this.storage[rows[i] + offset] = sourceStorage[index];
                            }
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<double> this[
            IndexCollection rowIndexes,
            string columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int[] rows = rowIndexes.indexes;
                int rowsLength = rows.Length;
                int columnsLength = this.numberOfColumns;
                var subMatrix = new DenseDoubleMatrixImplementor(rowsLength, columnsLength);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset, index = 0;

                for (int j = 0; j < columnsLength; j++)
                {
                    offset = thisNumberOfRows * j;
                    for (int i = 0; i < rowsLength; i++, index++)
                        subStorage[index] = thisStorage[rows[i] + offset];
                }

                return subMatrix;
            }
            set
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(
                    rowIndexes.Count, this.numberOfColumns, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int thisNumberOfRows = this.numberOfRows;

                int offset;
                int[] rows = rowIndexes.indexes;
                int columnsLength = this.numberOfColumns;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                offset = thisNumberOfRows * j;
                                for (int i = 0; i < rows.Length; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.storage[rows[i] + offset] = sourceValues[index];
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;
                            int index = 0;
                            for (int j = 0; j < columnsLength; j++)
                            {
                                offset = thisNumberOfRows * j;
                                for (int i = 0; i < rows.Length; i++, index++)
                                    this.storage[rows[i] + offset] = sourceStorage[index];
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        #region this[string, *]

        internal sealed override MatrixImplementor<double> this[string rowIndexes, int columnIndex]
        {
            get
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int rowsLength = thisNumberOfRows;
                var subMatrix = new DenseDoubleMatrixImplementor(rowsLength, 1);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset, index = 0;

                offset = thisNumberOfRows * columnIndex;
                for (int i = 0; i < rowsLength; i++, index++)
                    subStorage[index] = thisStorage[i + offset];

                return subMatrix;
            }
            set
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(thisNumberOfRows, 1, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int offset;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            offset = thisNumberOfRows * columnIndex;
                            for (int i = 0; i < thisNumberOfRows; i++)
                                if (source.TryGetPosition(i, 0, out int index))
                                {
                                    this.storage[i + offset] = sourceValues[index];
                                }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;

                            offset = thisNumberOfRows * columnIndex;
                            int index = 0;
                            for (int i = 0; i < thisNumberOfRows; i++, index++)
                                this.storage[i + offset] = sourceStorage[index];
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<double> this[string rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                int[] columns = columnIndexes.indexes;
                int rowsLength = thisNumberOfRows;
                int columnsLength = columns.Length;
                var subMatrix = new DenseDoubleMatrixImplementor(rowsLength, columnsLength);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset, index = 0;

                for (int j = 0; j < columnsLength; j++)
                {
                    offset = thisNumberOfRows * columns[j];
                    for (int i = 0; i < rowsLength; i++, index++)
                        subStorage[index] = thisStorage[i + offset];
                }

                return subMatrix;
            }
            set
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int thisNumberOfRows = this.numberOfRows;

                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(thisNumberOfRows, columnIndexes.Count, value);

                MatrixImplementor<double> sourceImplementor;

                // if the source is this, clone the data before writing
                if (object.ReferenceEquals(this, value))
                {
                    sourceImplementor = (MatrixImplementor<double>)value.Clone();
                }
                else
                {
                    sourceImplementor = value;
                }

                int offset;
                int[] columns = columnIndexes.indexes;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columns.Length; j++)
                            {
                                offset = thisNumberOfRows * columns[j];
                                for (int i = 0; i < thisNumberOfRows; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.storage[i + offset] = sourceValues[index];
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;
                            int index = 0;
                            for (int j = 0; j < columns.Length; j++)
                            {
                                offset = thisNumberOfRows * columns[j];
                                for (int i = 0; i < thisNumberOfRows; i++, index++)
                                    this.storage[i + offset] = sourceStorage[index];
                            }
                        }
                        break;
                }
            }
        }

        internal sealed override MatrixImplementor<double> this[string rowIndexes, string columnIndexes]
        {
            get
            {
                int thisNumberOfRows = this.numberOfRows;

                int rowsLength = thisNumberOfRows;
                int columnsLength = this.numberOfColumns;
                var subMatrix = new DenseDoubleMatrixImplementor(rowsLength, columnsLength);
                double[] subStorage = subMatrix.storage;

                double[] thisStorage = this.storage;

                int offset, index = 0;

                for (int j = 0; j < columnsLength; j++)
                {
                    offset = thisNumberOfRows * j;
                    for (int i = 0; i < rowsLength; i++, index++)
                        subStorage[index] = thisStorage[i + offset];
                }

                return subMatrix;
            }
            set
            {
                // Test for mismatched matrix dimensions
                ImplementationServices.ThrowOnMismatchedMatrixDimensions(this.numberOfRows, this.numberOfColumns, value);

                // if the source is this, nothing has to be done
                if (object.ReferenceEquals(this, value))
                {
                    return;
                }

                MatrixImplementor<double> sourceImplementor = value;

                int offset;

                int thisNumberOfRows = this.numberOfRows;
                int rowsLength = thisNumberOfRows;
                int columnsLength = this.numberOfColumns;

                switch (sourceImplementor.StorageScheme)
                {
                    case StorageScheme.CompressedRow:
                        {
                            var source = (SparseCsr3DoubleMatrixImplementor)sourceImplementor;
                            var sourceValues = source.values;

                            for (int j = 0; j < columnsLength; j++)
                            {
                                offset = thisNumberOfRows * j;
                                for (int i = 0; i < rowsLength; i++)
                                    if (source.TryGetPosition(i, j, out int index))
                                    {
                                        this.storage[i + offset] = sourceValues[index];
                                    }
                            }
                        }
                        break;
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)sourceImplementor;
                            double[] sourceStorage = source.storage;
                            int index = 0;
                            for (int j = 0; j < columnsLength; j++)
                            {
                                offset = thisNumberOfRows * j;
                                for (int i = 0; i < rowsLength; i++, index++)
                                    this.storage[i + offset] = sourceStorage[index];
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
                return this.storage.Length;
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

                int offset;
                double[] thisStorage = this.storage;

                for (int i = 1; i < numberOfRows; i++)
                {
                    offset = i * numberOfRows;
                    for (int j = 0; j < i; j++)
                    {
                        if (thisStorage[i + numberOfRows * j] != thisStorage[j + offset])
                            return false;
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

                int offset;
                double[] thisStorage = this.storage;

                for (int i = 1; i < numberOfRows; i++)
                {
                    offset = i * numberOfRows;
                    for (int j = 0; j < i; j++)
                    {
                        if (thisStorage[i + numberOfRows * j] != -thisStorage[j + offset])
                            return false;
                    }
                }

                // Diagonal entries
                for (int i = 0; i < numberOfRows; i++)
                {
                    offset = i * numberOfRows;
                    if (thisStorage[i + offset] != 0.0)
                        return false;
                }

                return true;
            }
        }

        internal sealed override bool IsHermitian
        {
            get
            {
                return this.IsSymmetric;
            }
        }

        internal sealed override bool IsSkewHermitian
        {
            get
            {
                return this.IsSkewSymmetric;
            }
        }

        internal sealed override int UpperBandwidth
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                int offset, column;
                double[] thisStorage = this.storage;

                for (int j = numberOfColumns - 1; j > 0; j--)
                {
                    column = j;
                    offset = j * numberOfRows;
                    for (int i = 0; i <= (numberOfColumns - numberOfRows < j ? numberOfColumns - 1 - j : numberOfRows - 1); i++)
                    {
                        if (0.0 != thisStorage[i + offset])
                            return column - i;
                        offset += numberOfRows;
                        column++;
                    }
                }

                return 0;
            }
        }

        internal sealed override int LowerBandwidth
        {
            get
            {
                // The lower bandwidth of an MxN matrix A is the smallest number p, s.t. 0<=p<M 
                // such that the entry a(i,j) vanishes whenever i > j + p. 

                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                int offset;
                double[] thisStorage = this.storage;

                for (int j = 0; j <= numberOfColumns - 1; j++)
                {
                    offset = j * numberOfRows;
                    for (int i = numberOfRows - 1; i > j; i--)
                        if (0.0 != thisStorage[i + offset])
                            return i - j;
                }

                return 0;
            }
        }

        #endregion

        #region Storage

        internal sealed override double[] AsColumnMajorDenseArray()
        {
            var array = new double[this.Count];
            this.storage.CopyTo(array, 0);
            return array;
        }

        internal sealed override StorageOrder StorageOrder
        {
            get
            {
                return this.storageOrder;
            }
        }

        internal sealed override double[] Storage
        {
            get
            {
                return this.storage;
            }
        }

        internal sealed override StorageScheme StorageScheme
        {
            get
            {
                return this.storageScheme;
            }
        }

        #endregion
    }
}
