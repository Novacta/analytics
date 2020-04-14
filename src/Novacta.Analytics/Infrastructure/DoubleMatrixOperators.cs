// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Novacta.Analytics.Interop;
using System.Linq;

namespace Novacta.Analytics.Infrastructure
{
    internal static class DoubleMatrixOperators
    {
        #region CopyTo

        public static void Dense_CopyTo(
            MatrixImplementor<double> matrix,
            double[] array,
            int arrayIndex)
        {
            double[] matrixArray = matrix.Storage;

            for (int i = 0; i < matrixArray.Length; i++)
            {
                array[arrayIndex + i] = matrixArray[i];
            }
        }

        public static void Sparse_CopyTo(
            MatrixImplementor<double> matrix,
            double[] array,
            int arrayIndex)
        {
            Array.Clear(array, arrayIndex, matrix.Count);

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;

            int numberOfRows = sparseMatrix.numberOfRows;
            int[] rowIndex = sparseMatrix.rowIndex;
            int numberOfStoredPositions = rowIndex[numberOfRows];
            if (numberOfStoredPositions != 0)
            {
                int[] columns = sparseMatrix.columns;
                double[] values = sparseMatrix.values;
                int baseIndex = arrayIndex;
                for (int i = 0; i < numberOfRows; i++, baseIndex++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        array[baseIndex + columns[p] * numberOfRows] = values[p];
                    }
                }
            }
        }

        public static void View_CopyTo(
            MatrixImplementor<double> matrix,
            double[] array,
            int arrayIndex)
        {
            int numberOfRows = matrix.NumberOfRows;
            int numberOfColumns = matrix.NumberOfColumns;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(matrix);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);

            int index = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] columnSubIndexes = subIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (referredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] refArray = referredImplementor.Storage;

                        leadingDim = referredImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * columnSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                array[arrayIndex + index] = refArray[rowSubIndexes[i] + refIndex];
                        }
                    }
                    break;
            }
        }

        #endregion

        #region Find

        #region Nonzero

        public static IndexCollection Dense_FindNonzero(
            MatrixImplementor<double> matrix)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            double[] matrixArray = matrix.Storage;

            for (int l = 0; l < matrixArray.Length; l++)
                if (0.0 != matrixArray[l])
                    indexList.AddLast(l);

            if (indexList.Count == 0)
            {
                return null;
            }

            result = new IndexCollection(indexList.ToArray(), false);

            return result;
        }

        public static IndexCollection Sparse_FindNonzero(
            MatrixImplementor<double> matrix)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            int numberOfRows = sparseMatrix.numberOfRows;

            var rowIndex = sparseMatrix.rowIndex;
            var columns = sparseMatrix.columns;
            var values = sparseMatrix.values;

            int j;
            double value;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                {
                    value = values[p];
                    if (0.0 != value)
                    {
                        j = columns[p];
                        indexList.AddLast(i + j * numberOfRows);
                    }
                }
            }

            if (indexList.Count == 0)
            {
                return null;
            }

            var indexArray = indexList.ToArray();
            Array.Sort(indexArray);
            result = new IndexCollection(indexArray, false);

            return result;
        }

        public static IndexCollection View_FindNonzero
            (MatrixImplementor<double> matrix)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            int numberOfRows = matrix.NumberOfRows;
            int numberOfColumns = matrix.NumberOfColumns;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(matrix);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);

            int index = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] columnSubIndexes = subIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (referredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] refArray = referredImplementor.Storage;

                        leadingDim = referredImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * columnSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                if (0.0 != refArray[rowSubIndexes[i] + refIndex])
                                    indexList.AddLast(index);
                        }
                    }
                    break;
            }

            if (indexList.Count == 0)
            {
                return null;
            }

            result = new IndexCollection(indexList.ToArray(), false);

            return result;
        }

        #endregion

        #region While

        public static IndexCollection Dense_FindWhile(
            MatrixImplementor<double> matrix,
            Predicate<double> match)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            double[] matrixArray = matrix.Storage;

            for (int l = 0; l < matrixArray.Length; l++)
                if (match(matrixArray[l]))
                    indexList.AddLast(l);

            if (indexList.Count == 0)
            {
                return null;
            }

            result = new IndexCollection(indexList.ToArray(), false);

            return result;
        }

        public static IndexCollection Sparse_FindWhile(
            MatrixImplementor<double> matrix,
            Predicate<double> match)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            int numberOfRows = sparseMatrix.numberOfRows;

            var values = sparseMatrix.values;

            int j;
            double value;
            bool isZeroMatched = match(0.0);

            if (!isZeroMatched)
            {
                var rowIndex = sparseMatrix.rowIndex;
                var columns = sparseMatrix.columns;

                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        value = values[p];
                        if (match(value))
                        {
                            j = columns[p];
                            indexList.AddLast(i + j * numberOfRows);
                        }
                    }
                }
            }
            else
            {
                // Here if and only if match(0.0) returns <c>true</c>.
                int numberOfColumns = sparseMatrix.numberOfColumns;
                int offset;
                for (j = 0; j < numberOfColumns; j++)
                {
                    offset = j * numberOfRows;
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        // The following tries to get the storage position 
                        // of the matrix entry corresponding to i and j.
                        // It returns <c>true</c> if the position is already stored, 
                        // <c>false</c> otherwise.
                        // As a consequence, if it returns <c>true</c>, this implies that 
                        // values[positionIndex] is not zero and should be checked for
                        // insertion in the index list. 
                        // If it returns <c>false</c>, the corresponding entry is zero,
                        // and must be inserted in the list.
                        if (sparseMatrix.TryGetPosition(i, j, out int positionIndex))
                        {
                            value = values[positionIndex];
                            if (match(value))
                            {
                                indexList.AddLast(i + offset);
                            }
                        }
                        else
                        {
                            indexList.AddLast(i + offset);
                        }
                    }
                }
            }

            if (indexList.Count == 0)
            {
                return null;
            }

            var indexArray = indexList.ToArray();
            Array.Sort(indexArray);
            result = new IndexCollection(indexArray, false);

            return result;
        }

        public static IndexCollection View_FindWhile(
            MatrixImplementor<double> matrix,
            Predicate<double> match)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            int numberOfRows = matrix.NumberOfRows;
            int numberOfColumns = matrix.NumberOfColumns;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(matrix);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);

            int index = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] columnSubIndexes = subIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (referredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] refArray = referredImplementor.Storage;

                        leadingDim = referredImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * columnSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                if (match(refArray[rowSubIndexes[i] + refIndex]))
                                    indexList.AddLast(index);
                        }
                    }
                    break;
            }

            if (indexList.Count == 0)
            {
                return null;
            }

            result = new IndexCollection(indexList.ToArray(), false);

            return result;
        }

        #endregion

        #region Value

        public static IndexCollection Dense_FindValue(
            MatrixImplementor<double> matrix,
            double value)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            double[] matrixArray = matrix.Storage;

            for (int l = 0; l < matrixArray.Length; l++)
                if (value == matrixArray[l])
                    indexList.AddLast(l);

            if (indexList.Count == 0)
            {
                return null;
            }

            result = new IndexCollection(indexList.ToArray(), false);

            return result;
        }

        public static IndexCollection Sparse_FindValue(
            MatrixImplementor<double> matrix,
            double value)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            int numberOfRows = sparseMatrix.numberOfRows;

            var values = sparseMatrix.values;

            int j;

            if (value != 0.0)
            {
                var rowIndex = sparseMatrix.rowIndex;
                var columns = sparseMatrix.columns;

                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        if (value == values[p])
                        {
                            j = columns[p];
                            indexList.AddLast(i + j * numberOfRows);
                        }
                    }
                }
            }
            else
            {
                // Here if and only if value is zero
                int numberOfColumns = sparseMatrix.numberOfColumns;
                int offset;
                for (j = 0; j < numberOfColumns; j++)
                {
                    offset = j * numberOfRows;
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        // The following tries to get the storage position 
                        // of the matrix entry corresponding to i and j.
                        // It returns <c>true</c> if the position is already stored, 
                        // <c>false</c> otherwise.
                        // However, only non zero values are effectively stored.
                        // As a consequence, if it returns <c>true</c>, this implies that 
                        // values[positionIndex] is not zero and should not be inserted
                        // in the index list. 
                        // If it returns <c>false</c>, the corresponding entry is zero,
                        // and must be inserted in the list.
                        if (!sparseMatrix.TryGetPosition(i, j, out _))
                        {
                            indexList.AddLast(i + offset);
                        }
                    }
                }
            }

            if (indexList.Count == 0)
            {
                return null;
            }

            var indexArray = indexList.ToArray();
            Array.Sort(indexArray);
            result = new IndexCollection(indexArray, false);

            return result;
        }

        public static IndexCollection View_FindValue(
            MatrixImplementor<double> matrix,
            double value)
        {
            IndexCollection result;
            LinkedList<int> indexList = new LinkedList<int>();

            int numberOfRows = matrix.NumberOfRows;
            int numberOfColumns = matrix.NumberOfColumns;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(matrix);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);

            int index = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] columnSubIndexes = subIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (referredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] refArray = referredImplementor.Storage;

                        leadingDim = referredImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * columnSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                if (value == refArray[rowSubIndexes[i] + refIndex])
                                    indexList.AddLast(index);
                        }
                    }
                    break;
            }

            if (indexList.Count == 0)
            {
                return null;
            }

            result = new IndexCollection(indexList.ToArray(), false);

            return result;
        }

        #endregion

        #region IndexOf

        public static int Dense_IndexOf(
            MatrixImplementor<double> matrix,
            double value)
        {
            int index = -1;

            double[] matrixArray = matrix.Storage;

            if (Double.IsNaN(value))
            {
                for (int l = 0; l < matrixArray.Length; l++)
                    if (Double.IsNaN(matrixArray[l]))
                    {
                        index = l;
                        break;
                    } 
            }
            else
            {
                for (int l = 0; l < matrixArray.Length; l++)
                    if (value == matrixArray[l])
                    {
                        index = l;
                        break;
                    }  
            }

            return index;
        }

        public static int Sparse_IndexOf(
            MatrixImplementor<double> data,
            double value)
        {
            LinkedList<int> indexList = new LinkedList<int>();

            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;

            var values = sparseData.values;

            int j;

            if (value != 0.0)
            {
                var rowIndex = sparseData.rowIndex;
                var columns = sparseData.columns;

                if (Double.IsNaN(value))
                {
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                        {
                            if (Double.IsNaN(values[p]))
                            {
                                j = columns[p];
                                indexList.AddLast(i + j * numberOfRows);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                        {
                            if (value == values[p])
                            {
                                j = columns[p];
                                indexList.AddLast(i + j * numberOfRows);
                            }
                        }
                    }
                }
            }
            else
            {
                // Here if and only if value is zero
                int numberOfColumns = sparseData.numberOfColumns;
                int offset;
                for (j = 0; j < numberOfColumns; j++)
                {
                    offset = j * numberOfRows;
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        // The following tries to get the storage position 
                        // of the matrix entry corresponding to i and j.
                        // It returns <c>true</c> if the position is already stored, 
                        // <c>false</c> otherwise.
                        // However, only non zero values are effectively stored.
                        // As a consequence, if it returns <c>true</c>, this implies that 
                        // values[positionIndex] is not zero and should not be inserted
                        // in the index list. 
                        // If it returns <c>false</c>, the corresponding entry is zero,
                        // and must be inserted in the list.
                        if (!sparseData.TryGetPosition(i, j, out _))
                        {
                            indexList.AddLast(i + offset);
                        }
                    }
                }
            }

            if (indexList.Count == 0)
            {
                return -1;
            }

            return indexList.Min();
        }

        public static int View_IndexOf(
            MatrixImplementor<double> matrix,
            double value)
        {
            int numberOfRows = matrix.NumberOfRows;
            int numberOfColumns = matrix.NumberOfColumns;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(matrix);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);

            int index = -1, linearIndex = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] columnSubIndexes = subIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (referredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] refArray = referredImplementor.Storage;

                        leadingDim = referredImplementor.NumberOfRows;

                        if (Double.IsNaN(value))
                        {
                            for (int j = 0; j < numberOfColumns; j++)
                            {
                                refIndex = leadingDim * columnSubIndexes[j];

                                for (int i = 0; i < numberOfRows; i++, linearIndex++)
                                    if (Double.IsNaN(refArray[rowSubIndexes[i] + refIndex]))
                                    {
                                        index = linearIndex;
                                        goto Finish;
                                    }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < numberOfColumns; j++)
                            {
                                refIndex = leadingDim * columnSubIndexes[j];

                                for (int i = 0; i < numberOfRows; i++, linearIndex++)
                                    if (value == refArray[rowSubIndexes[i] + refIndex])
                                    {
                                        index = linearIndex;
                                        goto Finish;
                                    }
                            }
                        }
                    }
                    break;
            }

        Finish:
            return index;
        }

        #endregion

        #endregion

        #region Apply

        internal static void Dense_InPlaceApply(
            MatrixImplementor<double> matrix,
            Func<double, double> func)
        {
            var denseMatrix = (DenseDoubleMatrixImplementor)matrix;

            var storage = denseMatrix.Storage;
            for (int i = 0; i < storage.Length; i++)
            {
                storage[i] = func(storage[i]);
            }
        }

        internal static MatrixImplementor<double> Dense_OutPlaceApply(
            MatrixImplementor<double> matrix,
            Func<double, double> func)
        {
            var result = (DenseDoubleMatrixImplementor)matrix.Clone();

            Dense_InPlaceApply(result, func);

            return result;
        }

        internal static void Sparse_InPlaceApply(
            MatrixImplementor<double> matrix,
            Func<double, double> func)
        {
            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;

            if (0.0 != func(0.0))
            {
                for (int i = 0; i < sparseMatrix.Count; i++)
                    sparseMatrix[i] = func(sparseMatrix[i]);
            }
            else
            {
                var values = sparseMatrix.values;
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = func(values[i]);
                }
            }
        }

        internal static MatrixImplementor<double> Sparse_OutPlaceApply(
            MatrixImplementor<double> matrix,
            Func<double, double> func)
        {
            var result = (SparseCsr3DoubleMatrixImplementor)matrix.Clone();

            Sparse_InPlaceApply(result, func);

            return result;
        }

        internal static void View_InPlaceApply(
            MatrixImplementor<double> matrix,
            Func<double, double> func)
        {
            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            subMatrix.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (subMatrix.ChangingDataHandler);

            subMatrix.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (subMatrix.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor denseMatrix = subMatrix;

            var args = new ImplementorChangedEventArgs(denseMatrix);
            subMatrix.OnImplementorChanged(args);

            // Remark: denseMatrix has been created here, hence there are no 
            //         SubMatrixImplementor.parentImplementor fields
            //         pointing to it. As a consequence, there's no need to call
            //
            //         denseMatrix.OnChangingData()
            //
            //         before in-place transposing the dense matrix.

            Dense_InPlaceApply(denseMatrix, func);
        }

        internal static MatrixImplementor<double> View_OutPlaceApply(
            MatrixImplementor<double> matrix,
            Func<double, double> func)
        {
            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            subMatrix.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (subMatrix.ChangingDataHandler);

            subMatrix.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (subMatrix.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor denseMatrix = subMatrix;

            var args = new ImplementorChangedEventArgs(denseMatrix);

            subMatrix.OnImplementorChanged(args);

            return Dense_OutPlaceApply(denseMatrix, func);
        }

        #endregion

        #region Transpose

        /// <summary>
        /// Computes the in place transpose of a ColMajor ordered matrix having
        /// the specified number of rows and columns.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="matrixArray">On input: the ColMajor array representation 
        /// of a given matrix. On output: the ColMajor array representation 
        /// of its transpose.</param>
        internal static void ArrayInPlaceTranspose(
            int numberOfRows,
            int numberOfColumns,
            double[] matrixArray)
        {
            // Implements in-place transposition: A := alpha*op(A).

            SafeNativeMethods.MKL_Dimatcopy(
                'c', // ordering, 
                't', // trans, 
                numberOfRows,
                numberOfColumns,
                1.0, // alpha
                matrixArray,
                numberOfRows, // ldA
                numberOfColumns); // ldB
        }

        internal static void Dense_InPlaceTranspose(
            MatrixImplementor<double> matrix)
        {
            var denseMatrix = (DenseDoubleMatrixImplementor)matrix;
            int numberOfRows = denseMatrix.numberOfRows;
            int numberOfColumns = denseMatrix.numberOfColumns;

            ArrayInPlaceTranspose(matrix.NumberOfRows, matrix.NumberOfColumns, matrix.Storage);
            denseMatrix.numberOfRows = numberOfColumns;
            denseMatrix.numberOfColumns = numberOfRows;
        }

        internal static MatrixImplementor<double> Dense_OutPlaceTranspose(
            MatrixImplementor<double> matrix)
        {
            var result = (DenseDoubleMatrixImplementor)matrix.Clone();

            Dense_InPlaceTranspose(result);

            return result;
        }

        internal static void Sparse_InPlaceTranspose(
            MatrixImplementor<double> matrix)
        {
            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            int numberOfRows = sparseMatrix.numberOfRows;
            int numberOfColumns = sparseMatrix.numberOfColumns;
            var values = sparseMatrix.values;
            var columns = sparseMatrix.columns;
            var rowIndex = sparseMatrix.rowIndex;
            int capacity = sparseMatrix.capacity;

            // Number of non-zeros in each row of transpose is the number of non-zeros
            // in each column (i.e., the "size" of the column) of original matrix

            int j;

            List<int>[] columnRows = new List<int>[numberOfColumns];
            List<int>[] columnPositions = new List<int>[numberOfColumns];
            for (j = 0; j < columnRows.Length; j++)
            {
                columnRows[j] = new List<int>();
                columnPositions[j] = new List<int>();
            }

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                {
                    j = columns[p];
                    columnRows[j].Add(i);
                    columnPositions[j].Add(p);
                }
            }

            double[] transposeValues = new double[capacity];
            int[] transposeColumns = new int[capacity];
            int[] transposeRowIndex = new int[numberOfColumns + 1];
            int[] transposePositions, defaultTransposePositions = new int[1] { 0 };
            List<int> currentColumnRows, currentColumnPositions;
            int cumulativeSum = 0;
            int currentColumnRowsCount;
            for (j = 0; j < columnRows.Length; j++)
            {
                currentColumnRows = columnRows[j];
                currentColumnPositions = columnPositions[j];
                currentColumnRowsCount = currentColumnRows.Count;
                if (currentColumnRowsCount > 1)
                {
                    SortHelper.Sort(currentColumnRows.ToArray(), SortDirection.Ascending, out transposePositions);
                }
                else
                {
                    transposePositions = defaultTransposePositions;
                }
                for (int p = 0; p < currentColumnRowsCount; p++)
                {
                    transposeColumns[p + cumulativeSum] = currentColumnRows[transposePositions[p]];
                    transposeValues[p + cumulativeSum] = values[currentColumnPositions[transposePositions[p]]];
                }

                cumulativeSum += currentColumnRowsCount;

                transposeRowIndex[j + 1] = cumulativeSum;
            }

            sparseMatrix.values = transposeValues;
            sparseMatrix.columns = transposeColumns;
            sparseMatrix.rowIndex = transposeRowIndex;
            sparseMatrix.numberOfRows = numberOfColumns;
            sparseMatrix.numberOfColumns = numberOfRows;
        }

        internal static MatrixImplementor<double> Sparse_OutPlaceTranspose(
            MatrixImplementor<double> matrix)
        {
            var result = (SparseCsr3DoubleMatrixImplementor)matrix.Clone();

            Sparse_InPlaceTranspose(result);

            return result;
        }

        internal static void View_InPlaceTranspose(
            MatrixImplementor<double> matrix)
        {
            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            subMatrix.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (subMatrix.ChangingDataHandler);

            subMatrix.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (subMatrix.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor denseMatrix = subMatrix;

            var args = new ImplementorChangedEventArgs(denseMatrix);
            subMatrix.OnImplementorChanged(args);

            // Remark: denseMatrix has been created here, hence there are no 
            //         SubMatrixImplementor.parentImplementor fields
            //         pointing to it. As a consequence, there's no need to call
            //
            //         denseMatrix.OnChangingData()
            //
            //         before in-place transposing the dense matrix.

            Dense_InPlaceTranspose(denseMatrix);
        }

        internal static MatrixImplementor<double> View_OutPlaceTranspose(
            MatrixImplementor<double> matrix)
        {
            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            subMatrix.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (subMatrix.ChangingDataHandler);

            subMatrix.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (subMatrix.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor denseMatrix = subMatrix;

            var args = new ImplementorChangedEventArgs(denseMatrix);

            subMatrix.OnImplementorChanged(args);

            return Dense_OutPlaceTranspose(denseMatrix);
        }

        #endregion

        #region Scalar

        #region ScalarBinaryOperators - Sum

        internal static MatrixImplementor<double> Scalar_Dense_Sum(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            DenseDoubleMatrixImplementor result = (DenseDoubleMatrixImplementor)(matrix.Clone());

            if (0.0 != scalar)
            {
                double[] matrixArray = matrix.Storage;
                double[] resultArray = result.Storage;

                int index;
                int count = resultArray.Length;

                for (index = 0; index < count; index++)
                    resultArray[index] = matrixArray[index] + scalar;
            }
            return result;
        }

        internal static MatrixImplementor<double> Scalar_Sparse_Sum(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // M + s

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;

            if (0.0 == scalar)
                return (SparseCsr3DoubleMatrixImplementor)sparseMatrix.Clone();

            DenseDoubleMatrixImplementor result = (SparseCsr3DoubleMatrixImplementor)matrix;

            var resArray = result.storage;

            for (int i = 0; i < resArray.Length; i++)
                resArray[i] += scalar;

            return result;
        }

        internal static MatrixImplementor<double> Scalar_View_Sum(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            ViewDoubleMatrixImplementor subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            DenseDoubleMatrixImplementor result = subMatrix;

            if (0.0 != scalar)
            {
                double[] resultArray = result.Storage;

                int count = resultArray.Length;

                for (int Index = 0; Index < count; Index++)
                    resultArray[Index] += scalar;
            }

            return result;
        }

        #endregion

        #region MatrixUnaryOperators - Negation

        internal static MatrixImplementor<double> Matrix_Dense_Negation(
            MatrixImplementor<double> matrix)
        {
            MatrixImplementor<double> result =
                new DenseDoubleMatrixImplementor(matrix.NumberOfRows, matrix.NumberOfColumns);

            double[] matrixArray = matrix.Storage;
            double[] resultArray = result.Storage;

            int index;
            int count = resultArray.Length;

            for (index = 0; index < count; index++)
                resultArray[index] = -matrixArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Negation(
            MatrixImplementor<double> matrix)
        {
            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            int numberOfRows = sparseMatrix.numberOfRows;
            var result = new SparseCsr3DoubleMatrixImplementor(numberOfRows,
                sparseMatrix.numberOfColumns, sparseMatrix.capacity);

            sparseMatrix.columns.CopyTo(result.columns, 0);
            var rowIndex = sparseMatrix.rowIndex;
            rowIndex.CopyTo(result.rowIndex, 0);

            var sparseValues = sparseMatrix.values;
            var resultValues = result.values;

            int numberOfStoredPositions = sparseMatrix.rowIndex[numberOfRows];
            for (int p = 0; p < numberOfStoredPositions; p++)
                resultValues[p] = -sparseValues[p];

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Negation(
            MatrixImplementor<double> matrix)
        {
            var result = new DenseDoubleMatrixImplementor(matrix.NumberOfRows, matrix.NumberOfColumns);
            double[] resultArray = result.Storage;

            int count = resultArray.Length;

            for (int index = 0; index < count; index++)
                resultArray[index] = -matrix[index];

            return result;
        }

        #endregion

        #region ScalarBinaryOperators - Subtract

        internal static MatrixImplementor<double> Scalar_Dense_LeftSubtract(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            if (0.0 == scalar)
                return Matrix_Dense_Negation(matrix);

            var result = (DenseDoubleMatrixImplementor)(matrix.Clone());

            double[] matrixArray = matrix.Storage;
            double[] resultArray = result.Storage;

            int index;
            int count = resultArray.Length;

            for (index = 0; index < count; index++)
                resultArray[index] = scalar - matrixArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Sparse_LeftSubtract(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // s - M

            if (0.0 == scalar)
                return Matrix_Sparse_Negation(matrix);

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            int offset;
            int numberOfRows = sparseMatrix.numberOfRows;
            int numberOfColumns = sparseMatrix.numberOfColumns;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var resArray = result.storage;

            for (int j = 0; j < numberOfColumns; j++)
            {
                offset = j * numberOfRows;
                for (int i = 0; i < numberOfRows; i++)
                    resArray[i + offset] = scalar - sparseMatrix.GetValue(i, j);
            }

            return result;
        }

        internal static MatrixImplementor<double> Scalar_View_LeftSubtract(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // s - M

            if (0.0 == scalar)
                return Matrix_View_Negation(matrix);

            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            DenseDoubleMatrixImplementor result = subMatrix;
            double[] resultArray = result.Storage;

            int count = resultArray.Length;

            for (int index = 0; index < count; index++)
                resultArray[index] = scalar - resultArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Dense_RightSubtract(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // M - s

            var result = (DenseDoubleMatrixImplementor)(matrix.Clone());

            if (0.0 == scalar)
                return result;

            double[] matrixArray = matrix.Storage;
            double[] resultArray = result.Storage;

            int index;
            int count = resultArray.Length;

            for (index = 0; index < count; index++)
                resultArray[index] = matrixArray[index] - scalar;

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Sparse_RightSubtract(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // M - s

            if (0.0 == scalar)
                return (SparseCsr3DoubleMatrixImplementor)matrix.Clone();

            DenseDoubleMatrixImplementor result = (SparseCsr3DoubleMatrixImplementor)matrix;

            var resArray = result.storage;

            for (int i = 0; i < resArray.Length; i++)
                resArray[i] -= scalar;

            return result;
        }

        internal static MatrixImplementor<double> Scalar_View_RightSubtract(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            if (0.0 == scalar)
                return (ViewDoubleMatrixImplementor)matrix.Clone();

            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            DenseDoubleMatrixImplementor result = subMatrix;

            double[] resultArray = result.Storage;

            int count = resultArray.Length;

            for (int index = 0; index < count; index++)
                resultArray[index] -= scalar;

            return result;
        }

        #endregion

        #region ScalarBinaryOperators - Multiply

        internal static MatrixImplementor<double> Scalar_Dense_Multiply(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            var result = (DenseDoubleMatrixImplementor)(matrix.Clone());

            if (1.0 != scalar)
            {
                double[] matrixArray = matrix.Storage;
                double[] resultArray = result.Storage;

                int index;
                int count = resultArray.Length;

                for (index = 0; index < count; index++)
                    resultArray[index] = matrixArray[index] * scalar;
            }

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Sparse_Multiply(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;
            var result = (SparseCsr3DoubleMatrixImplementor)sparseMatrix.Clone();


            if (1.0 != scalar)
            {
                var resultValues = result.values;
                int numberOfStoredPositions = sparseMatrix.rowIndex[sparseMatrix.numberOfRows];

                for (int i = 0; i < numberOfStoredPositions; i++)
                    resultValues[i] *= scalar;
            }

            return result;
        }

        internal static MatrixImplementor<double> Scalar_View_Multiply(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            DenseDoubleMatrixImplementor result = subMatrix;

            if (1.0 != scalar)
            {
                double[] resultArray = result.Storage;

                int count = resultArray.Length;

                for (int index = 0; index < count; index++)
                    resultArray[index] *= scalar;
            }

            return result;
        }

        #endregion

        #region ScalarBinaryOperators - Divide

        internal static MatrixImplementor<double> Scalar_Dense_LeftDivide(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            var result = (DenseDoubleMatrixImplementor)(matrix.Clone());

            double[] matrixArray = matrix.Storage;
            double[] resultArray = result.Storage;

            int index;
            int NumOfOperations = resultArray.Length;

            for (index = 0; index < NumOfOperations; index++)
                resultArray[index] = scalar / matrixArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Sparse_LeftDivide(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // s / M

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;

            int offset;
            int numberOfRows = sparseMatrix.numberOfRows;
            int numberOfColumns = sparseMatrix.numberOfColumns;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var resArray = result.storage;

            for (int j = 0; j < numberOfColumns; j++)
            {
                offset = j * numberOfRows;
                for (int i = 0; i < numberOfRows; i++)
                    resArray[i + offset] = scalar / sparseMatrix.GetValue(i, j);
            }

            return result;
        }

        internal static MatrixImplementor<double> Scalar_View_LeftDivide(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            DenseDoubleMatrixImplementor result = subMatrix;
            double[] resultArray = result.Storage;

            int count = resultArray.Length;

            for (int index = 0; index < count; index++)
                resultArray[index] = scalar / resultArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Dense_RightDivide(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            var result = (DenseDoubleMatrixImplementor)(matrix.Clone());

            if (1.0 != scalar)
            {
                double[] matrixArray = matrix.Storage;
                double[] resultArray = result.Storage;

                int index;
                int count = resultArray.Length;

                for (index = 0; index < count; index++)
                    resultArray[index] = matrixArray[index] / scalar;
            }

            return result;
        }

        internal static MatrixImplementor<double> Scalar_Sparse_RightDivide(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            // M / s

            var sparseMatrix = (SparseCsr3DoubleMatrixImplementor)matrix;

            if (1.0 == scalar)
            {
                return (SparseCsr3DoubleMatrixImplementor)sparseMatrix.Clone();
            }

            int offset;
            int numberOfRows = sparseMatrix.numberOfRows;
            int numberOfColumns = sparseMatrix.numberOfColumns;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);
            var resArray = result.storage;

            for (int j = 0; j < numberOfColumns; j++)
            {
                offset = j * numberOfRows;
                for (int i = 0; i < numberOfRows; i++)
                    resArray[i + offset] = sparseMatrix.GetValue(i, j) / scalar;
            }

            return result;
        }

        internal static MatrixImplementor<double> Scalar_View_RightDivide(
            MatrixImplementor<double> matrix,
            double scalar)
        {
            if (1.0 == scalar)
            {
                return (ViewDoubleMatrixImplementor)matrix.Clone();
            }

            var subMatrix = (ViewDoubleMatrixImplementor)(matrix);

            DenseDoubleMatrixImplementor result = subMatrix;
            double[] resultArray = result.Storage;

            int count = resultArray.Length;

            for (int index = 0; index < count; index++)
                resultArray[index] /= scalar;

            return result;
        }

        #endregion

        #endregion

        #region Matrix

        #region ElementWiseMultiply

        internal static MatrixImplementor<double> Matrix_Dense_Dense_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;
            double[] rhsArray = right.Storage;
            double[] resultArray = result.Storage;

            int index;
            int count = resultArray.Length;

            {
                for (index = 0; index < count; index++)
                    resultArray[index] = lhsArray[index] * rhsArray[index];
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Dense_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;

            var result = new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, lhsSparse.capacity);
            lhsSparse.columns.CopyTo(result.columns, 0);
            lhsSparse.rowIndex.CopyTo(result.rowIndex, 0);

            double[] rhsArray = right.Storage;
            double[] lhsValues = lhsSparse.values;

            int[] columns = result.columns;
            int[] rowIndex = result.rowIndex;

            double[] resValues = result.values;

            int j;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                {
                    j = columns[p];

                    resValues[p] = lhsValues[p] * rhsArray[i + j * numberOfRows];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Sparse_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var lhsValues = lhsSparse.values;
            var lhsColumns = lhsSparse.columns;
            var lhsRowIndex = lhsSparse.rowIndex;
            int lhsNumberOfStoredPositions = lhsRowIndex[numberOfRows];

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;
            var rhsValues = rhsSparse.values;
            var rhsRowIndex = rhsSparse.rowIndex;
            int rhsNumberOfStoredPositions = rhsRowIndex[numberOfRows];

            double resultLength = rhsSparse.Count;
            int resultCapacity = Convert.ToInt32(Math.Ceiling(
                lhsNumberOfStoredPositions * rhsNumberOfStoredPositions / resultLength));
            var result = new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, resultCapacity);

            HashSet<int> inspectedSet = new HashSet<int>();

            double lhsValue, rhsValue;
            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {

                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    j = lhsColumns[p];
                    index = i + j * numberOfRows;
                    inspectedSet.Add(index);
                    lhsValue = lhsValues[p];
                    if (lhsValue != 0.0)
                    {
                        if (rhsSparse.TryGetPosition(i, j, out int positionIndex))
                        {
                            rhsValue = rhsValues[positionIndex];
                            if (rhsValue != 0.0)
                            {
                                result.SetValue(i, j, lhsValue * rhsValue);
                            }
                        }
                    }
                }

                // inspectedSet contains the positions which are stored at least in one matrix.
                // If position l is not in inspectedSet, then l is stored
                // neither in the left matrix, nor in the right one, and hence both matrices are zero
                // at l.
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_Sparse_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;

            var result = new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, rhsSparse.capacity);
            rhsSparse.columns.CopyTo(result.columns, 0);
            rhsSparse.rowIndex.CopyTo(result.rowIndex, 0);

            double[] lhsArray = left.Storage;
            double[] rhsValues = rhsSparse.values;

            int[] columns = result.columns;
            int[] rowIndex = result.rowIndex;

            double[] resValues = result.values;

            int j;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                {
                    j = columns[p];

                    resValues[p] = rhsValues[p] * lhsArray[i + j * numberOfRows];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_View_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var result = new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, lhsSparse.capacity);

            lhsSparse.columns.CopyTo(result.columns, 0);
            lhsSparse.rowIndex.CopyTo(result.rowIndex, 0);

            double[] lhsValues = lhsSparse.values;

            int[] columns = result.columns;
            int[] rowIndex = result.rowIndex;

            double[] resValues = result.values;


            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes =
                rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int refOffset, offset;

            switch (rhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] rhsArray = rhsParentImplementor.Storage;

                        refOffset = rhsParentImplementor.NumberOfRows;

                        int j;
                        for (int i = 0; i < numberOfRows; i++)
                        {
                            for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                            {
                                j = columns[p];
                                offset = refOffset * rhsColSubIndexes[j];

                                resValues[p] = lhsValues[p] * rhsArray[rhsRowSubIndexes[i] + offset];
                            }
                        }
                    }
                    break;
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Sparse_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;
            var result = new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, rhsSparse.capacity);

            rhsSparse.columns.CopyTo(result.columns, 0);
            rhsSparse.rowIndex.CopyTo(result.rowIndex, 0);

            double[] rhsValues = rhsSparse.values;

            int[] columns = result.columns;
            int[] rowIndex = result.rowIndex;

            double[] resValues = result.values;


            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);
            IndexCollection[] lhsSubIndexes =
                lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsReferredImplementor);

            int[] lhsRowSubIndexes = lhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = lhsSubIndexes[1].indexes;

            int refOffset, offset;

            switch (lhsReferredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] rhsArray = lhsReferredImplementor.Storage;

                        refOffset = lhsReferredImplementor.NumberOfRows;

                        int j;
                        for (int i = 0; i < numberOfRows; i++)
                        {
                            for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                            {
                                j = columns[p];
                                offset = refOffset * lhsColSubIndexes[j];

                                resValues[p] = rhsValues[p] * rhsArray[lhsRowSubIndexes[i] + offset];
                            }
                        }
                    }
                    break;
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Dense_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result =
               new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] rhsArray = right.Storage;
            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);
            IndexCollection[] LhsSubIndexes = lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsParentImplementor);

            int index = 0;
            int[] lhsRowSubIndexes = LhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = LhsSubIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (lhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] lhsArray = lhsParentImplementor.Storage;

                        leadingDim = lhsParentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * lhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                resultArray[index] = lhsArray[lhsRowSubIndexes[i] + refIndex] *
                                                    rhsArray[index];
                        }
                    }
                    break;
            }

            return result;
        }


        internal static MatrixImplementor<double> Matrix_Dense_View_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;
            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int index = 0;
            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (rhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] rhsArray = rhsParentImplementor.Storage;

                        leadingDim = rhsParentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * rhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                resultArray[index] = lhsArray[index] *
                                                    rhsArray[rhsRowSubIndexes[i] + refIndex];
                        }
                    }
                    break;
            }

            return result;
        }


        internal static MatrixImplementor<double> Matrix_View_View_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);


            IndexCollection[] lhsSubIndexes = lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsParentImplementor);

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int index = 0;
            int[] lhsRowSubIndexes = lhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = lhsSubIndexes[1].indexes;

            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int lhsLeadingDim, lhsRefIndex, rhsLeadingDim, rhsRefIndex;

            if (StorageScheme.Dense == lhsParentImplementor.StorageScheme)
            {
                if (StorageScheme.Dense == rhsParentImplementor.StorageScheme)
                {
                    double[] lhsArray = lhsParentImplementor.Storage;
                    double[] rhsArray = rhsParentImplementor.Storage;

                    lhsLeadingDim = lhsParentImplementor.NumberOfRows;
                    rhsLeadingDim = rhsParentImplementor.NumberOfRows;

                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        lhsRefIndex = lhsLeadingDim * lhsColSubIndexes[j];
                        rhsRefIndex = rhsLeadingDim * rhsColSubIndexes[j];

                        for (int i = 0; i < numberOfRows; i++, index++)
                            resultArray[index] = lhsArray[lhsRowSubIndexes[i] + lhsRefIndex] *
                                                rhsArray[rhsRowSubIndexes[i] + rhsRefIndex];
                    }
                }
            }

            return result;
        }

        #endregion

        #region Helper functions
        //
        // Helper functions
        //
        internal static void CheckElementwiseOperationParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right,
            out int numberOfRows,
            out int numberOfColumns)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = left.NumberOfColumns;

            int rhsNumberOfRows = right.NumberOfRows;
            int rhsNumberOfColumns = right.NumberOfColumns;

            if ((numberOfRows != rhsNumberOfRows) || (numberOfColumns != rhsNumberOfColumns))
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_ELEMENT_WISE_ALL_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckSubtractionParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right,
            out int numberOfRows,
            out int numberOfColumns)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = left.NumberOfColumns;

            int rhsNumberOfRows = right.NumberOfRows;
            int rhsNumberOfColumns = right.NumberOfColumns;

            if ((numberOfRows != rhsNumberOfRows) || (numberOfColumns != rhsNumberOfColumns))
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_SUBTRACT_ALL_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckAdditionParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right,
            out int numberOfRows,
            out int numberOfColumns)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = left.NumberOfColumns;

            int rhsNumberOfRows = right.NumberOfRows;
            int rhsNumberOfColumns = right.NumberOfColumns;

            if ((numberOfRows != rhsNumberOfRows) || (numberOfColumns != rhsNumberOfColumns))
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_ADD_ALL_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckDivisionParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right,
            out int numberOfRows,
            out int numberOfColumns,
            out int innerDimension)
        {
            // Implements L / u as the solution of L = X * u, or X = L * inverse( u )
            //                                    KxN KxM MxN

            // int thisNumberOfRows, thisNumberOfColumns, InnerDimension; // K, N, and M, respectively

            numberOfRows = left.NumberOfRows;
            numberOfColumns = right.NumberOfColumns;

            int lhsNumberOfColumns = left.NumberOfColumns;
            innerDimension = right.NumberOfRows;

            if (numberOfColumns != lhsNumberOfColumns)
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_DIVIDE_COLUMNS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckMultiplicationParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right,
            out int numberOfRows,
            out int numberOfColumns,
            out int innerDimension)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = right.NumberOfColumns;

            int lhsNumberOfColumns = left.NumberOfColumns;
            innerDimension = right.NumberOfRows;

            if (innerDimension != lhsNumberOfColumns)
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_MULTIPLY_INNER_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        #endregion

        #region Multiply

        internal static MatrixImplementor<double> Matrix_Sparse_Dense_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_Multiply(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_Multiply(left, right[0]);
            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var lhsValues = lhsSparse.values;
            var lhsColumns = lhsSparse.columns;
            var lhsRowIndex = lhsSparse.rowIndex;

            var rhsDense = (DenseDoubleMatrixImplementor)right;
            var rhsArray = rhsDense.storage;
            var resArray = result.storage;

            Parallel.For(0, numberOfRows, i =>
            {
                double lhsValue, value;
                int k;
                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    k = lhsColumns[p];
                    lhsValue = lhsValues[p];
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        value = lhsValue * rhsArray[k + j * innerDimension];
                        if (value != 0.0)
                            resArray[i + j * numberOfRows] += value;
                    }
                }
            });

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_Sparse_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_Multiply(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_Multiply(left, right[0]);
            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;
            var rhsValues = rhsSparse.values;
            var rhsColumns = rhsSparse.columns;
            var rhsRowIndex = rhsSparse.rowIndex;

            var lhsDense = (DenseDoubleMatrixImplementor)left.Clone();
            var lhsArray = lhsDense.storage;

            Parallel.For(0, numberOfRows, i =>
            {
                int j, offset;
                double rhsValue, value;
                for (int k = 0; k < innerDimension; k++)
                {
                    offset = k * numberOfRows;
                    for (int p = rhsRowIndex[k]; p < rhsRowIndex[k + 1]; p++)
                    {
                        j = rhsColumns[p];
                        rhsValue = rhsValues[p];
                        value = rhsValue * lhsArray[i + offset];
                        if (value != 0.0)
                            result[i + j * numberOfRows] += value;
                    }
                }
            });

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Sparse_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_Multiply(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_Multiply(left, right[0]);
            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var lhsValues = lhsSparse.values;
            var lhsColumns = lhsSparse.columns;
            var lhsRowIndex = lhsSparse.rowIndex;
            int lhsNumberOfStoredPositions = lhsRowIndex[numberOfRows];

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;
            var rhsValues = rhsSparse.values;
            var rhsColumns = rhsSparse.columns;
            var rhsRowIndex = rhsSparse.rowIndex;
            int rhsNumberOfStoredPositions = rhsRowIndex[innerDimension];

            var resArray = result.storage;

            Parallel.For(0, numberOfRows, i =>
            {
                double lhsValue, value;
                int k;
                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    k = lhsColumns[p];
                    lhsValue = lhsValues[p];
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        if (rhsSparse.TryGetPosition(k, j, out int positionIndex))
                        {
                            value = lhsValue * rhsValues[positionIndex];
                            if (value != 0.0)
                                result[i + j * numberOfRows] += value;
                        }
                    }
                }
            });

            // inspectedSet contains the positions which are stored at least in one matrix.
            // If position l is not in inspectedSet, then l is stored
            // neither in the left matrix, nor in the right one, and hence both matrices are zero
            // at l.

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_Dense_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_Multiply(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_Multiply(left, right[0]);
            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;

            double[] rhsArray = right.Storage;

            double[] resultArray = result.Storage;

            SafeNativeMethods.BLAS_dgemm(
                SafeNativeMethods.BLAS.ORDER.ColMajor,
                SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //'N', // transA
                SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //'N', // transB
                numberOfRows, // M 
                numberOfColumns, //N
                innerDimension, // K
                1.0, // alpha, 
                lhsArray, //constant double *A,
                numberOfRows,
                rhsArray, //constant double *B, 
                innerDimension,
                0.0, // beta, 
                resultArray, //double *C, 
                numberOfRows);

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_View_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);

            rhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (rhsSub.ChangingDataHandler);

            rhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (rhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor rhsDenseMatrix = rhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(rhsDenseMatrix);
            rhsSub.OnImplementorChanged(args);

            return Matrix_Dense_Dense_Multiply(left, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_Sparse_View_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);

            rhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (rhsSub.ChangingDataHandler);

            rhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (rhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor rhsDenseMatrix = rhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(rhsDenseMatrix);
            rhsSub.OnImplementorChanged(args);

            return Matrix_Sparse_Dense_Multiply(left, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_View_Dense_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);

            lhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (lhsSub.ChangingDataHandler);

            lhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (lhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor lhsDenseMatrix = lhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(lhsDenseMatrix);
            lhsSub.OnImplementorChanged(args);

            return Matrix_Dense_Dense_Multiply(lhsDenseMatrix, right);
        }

        internal static MatrixImplementor<double> Matrix_View_Sparse_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);

            lhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (lhsSub.ChangingDataHandler);

            lhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (lhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor lhsDenseMatrix = lhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(lhsDenseMatrix);
            lhsSub.OnImplementorChanged(args);

            return Matrix_Dense_Sparse_Multiply(lhsDenseMatrix, right);
        }

        internal static MatrixImplementor<double> Matrix_View_View_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);

            lhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (lhsSub.ChangingDataHandler);

            lhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (lhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor lhsDenseMatrix = lhsSub;

            ImplementorChangedEventArgs lArgs = new ImplementorChangedEventArgs(lhsDenseMatrix);
            lhsSub.OnImplementorChanged(lArgs);

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);

            rhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (rhsSub.ChangingDataHandler);

            rhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (rhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor rhsDenseMatrix = rhsSub;

            ImplementorChangedEventArgs rArgs = new ImplementorChangedEventArgs(rhsDenseMatrix);
            rhsSub.OnImplementorChanged(rArgs);

            return Matrix_Dense_Dense_Multiply(lhsDenseMatrix, rhsDenseMatrix);
        }

        #endregion

        #region Add

        internal static MatrixImplementor<double> Matrix_Sparse_Dense_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;

            double[] lhsValues = lhsSparse.values;
            int[] lhsColumns = lhsSparse.columns;
            int[] lhsRowIndex = lhsSparse.rowIndex;

            var result = (DenseDoubleMatrixImplementor)right.Clone();
            double[] resArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    j = lhsColumns[p];
                    index = i + j * numberOfRows;
                    resArray[index] += lhsValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Sparse_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var lhsValues = lhsSparse.values;
            var lhsColumns = lhsSparse.columns;
            var lhsRowIndex = lhsSparse.rowIndex;
            int lhsNumberOfStoredPositions = lhsRowIndex[numberOfRows];

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;
            var rhsValues = rhsSparse.values;
            var rhsColumns = rhsSparse.columns;
            var rhsRowIndex = rhsSparse.rowIndex;
            int rhsNumberOfStoredPositions = rhsRowIndex[numberOfRows];

            int resultCapacity = lhsNumberOfStoredPositions + rhsNumberOfStoredPositions;
            var result = new SparseCsr3DoubleMatrixImplementor(numberOfRows, numberOfColumns, resultCapacity);

            HashSet<int> inspectedSet = new HashSet<int>();

            double lhsValue, rhsValue, value;
            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    j = lhsColumns[p];
                    index = i + j * numberOfRows;
                    inspectedSet.Add(index);
                    lhsValue = lhsValues[p];
                    if (rhsSparse.TryGetPosition(i, j, out int positionIndex))
                    {
                        value = lhsValue + rhsValues[positionIndex];
                        if (value != 0.0)
                            result.SetValue(i, j, value);
                    }
                    else
                    {
                        if (lhsValue != 0.0)
                            result.SetValue(i, j, lhsValue);
                    }
                }

                for (int p = rhsRowIndex[i]; p < rhsRowIndex[i + 1]; p++)
                {
                    j = rhsColumns[p];
                    index = i + j * numberOfRows;
                    if (inspectedSet.Add(index))
                    {
                        rhsValue = rhsValues[p];
                        if (rhsValue != 0.0)
                            result.SetValue(i, j, rhsValue);
                    }
                }

                // inspectedSet contains the positions which are stored 
                // at least in one matrix.
                // If position l is not in inspectedSet, then l is stored
                // neither in the left matrix, nor in the right one, 
                // and hence both matrices are zero
                // at l.
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_Sparse_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;

            double[] rhsValues = rhsSparse.values;
            int[] rhsColumns = rhsSparse.columns;
            int[] rhsRowIndex = rhsSparse.rowIndex;

            var result = (DenseDoubleMatrixImplementor)left.Clone();
            double[] resArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rhsRowIndex[i]; p < rhsRowIndex[i + 1]; p++)
                {
                    j = rhsColumns[p];
                    index = i + j * numberOfRows;
                    resArray[index] += rhsValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_Dense_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;
            double[] rhsArray = right.Storage;
            double[] resultArray = result.Storage;

            int index;
            int count = resultArray.Length;

            for (index = 0; index < count; index++)
                resultArray[index] = lhsArray[index] + rhsArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_View_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_View_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;
            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int index = 0;
            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (rhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] rhsArray = rhsParentImplementor.Storage;

                        leadingDim = rhsParentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * rhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                resultArray[index] = lhsArray[index] +
                                                    rhsArray[rhsRowSubIndexes[i] + refIndex];
                        }
                    }
                    break;

            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Sparse_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_View_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;

            double[] rhsValues = rhsSparse.values;
            int[] rhsColumns = rhsSparse.columns;
            int[] rhsRowIndex = rhsSparse.rowIndex;

            DenseDoubleMatrixImplementor result = (ViewDoubleMatrixImplementor)left;
            double[] resArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rhsRowIndex[i]; p < rhsRowIndex[i + 1]; p++)
                {
                    j = rhsColumns[p];
                    index = i + j * numberOfRows;
                    resArray[index] += rhsValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_View_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_View_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;

            double[] lhsValues = lhsSparse.values;
            int[] lhsColumns = lhsSparse.columns;
            int[] lhsRowIndex = lhsSparse.rowIndex;

            DenseDoubleMatrixImplementor result = (ViewDoubleMatrixImplementor)right;
            double[] resArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    j = lhsColumns[p];
                    index = i + j * numberOfRows;
                    resArray[index] += lhsValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Dense_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_View_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] rhsArray = right.Storage;
            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);
            IndexCollection[] lhsSubIndexes = lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsParentImplementor);

            int index = 0;
            int[] lhsRowSubIndexes = lhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = lhsSubIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (lhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] lhsArray = lhsParentImplementor.Storage;

                        leadingDim = lhsParentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * lhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                resultArray[index] = lhsArray[lhsRowSubIndexes[i] + refIndex] +
                                                    rhsArray[index];
                        }
                    }
                    break;
            }

            return result;
        }


        //
        // SUM - DOUBLE - SUB - SUB
        //


        internal static MatrixImplementor<double> Matrix_View_View_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_View_Sum(right, left[0]);

            if (1 == right.Count)
                return Scalar_View_Sum(left, right[0]);
            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);
            IndexCollection[] lhsSubIndexes = lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsParentImplementor);

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int index = 0;
            int[] lhsRowSubIndexes = lhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = lhsSubIndexes[1].indexes;

            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int lhsLeadingDim, lhsRefIndex, rhsLeadingDim, rhsRefIndex;

            if (StorageScheme.Dense == lhsParentImplementor.StorageScheme)
            {
                if (StorageScheme.Dense == rhsParentImplementor.StorageScheme)
                {
                    double[] lhsArray = lhsParentImplementor.Storage;
                    double[] rhsArray = rhsParentImplementor.Storage;

                    lhsLeadingDim = lhsParentImplementor.NumberOfRows;
                    rhsLeadingDim = rhsParentImplementor.NumberOfRows;

                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        lhsRefIndex = lhsLeadingDim * lhsColSubIndexes[j];
                        rhsRefIndex = rhsLeadingDim * rhsColSubIndexes[j];

                        for (int i = 0; i < numberOfRows; i++, index++)
                            resultArray[index] = lhsArray[lhsRowSubIndexes[i] + lhsRefIndex] +
                                                rhsArray[rhsRowSubIndexes[i] + rhsRefIndex];
                    }
                }
            }

            return result;
        }

        #endregion

        #region Subtract

        internal static MatrixImplementor<double> Matrix_Dense_Dense_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;
            double[] rhsArray = right.Storage;
            double[] resultArray = result.Storage;

            int index;
            int count = resultArray.Length;

            for (index = 0; index < count; index++)
                resultArray[index] = lhsArray[index] - rhsArray[index];

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Sparse_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var lhsValues = lhsSparse.values;
            var lhsColumns = lhsSparse.columns;
            var lhsRowIndex = lhsSparse.rowIndex;
            int lhsNumberOfStoredPositions = lhsRowIndex[numberOfRows];

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;
            var rhsValues = rhsSparse.values;
            var rhsColumns = rhsSparse.columns;
            var rhsRowIndex = rhsSparse.rowIndex;
            int rhsNumberOfStoredPositions = rhsRowIndex[numberOfRows];

            int resultCapacity = lhsNumberOfStoredPositions + rhsNumberOfStoredPositions;
            var result = new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, resultCapacity);

            HashSet<int> inspectedSet = new HashSet<int>();

            double lhsValue, rhsValue, value;
            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {

                for (int p = lhsRowIndex[i]; p < lhsRowIndex[i + 1]; p++)
                {
                    j = lhsColumns[p];
                    index = i + j * numberOfRows;
                    inspectedSet.Add(index);
                    lhsValue = lhsValues[p];
                    if (rhsSparse.TryGetPosition(i, j, out int positionIndex))
                    {
                        value = lhsValue - rhsValues[positionIndex];
                        if (value != 0.0)
                            result.SetValue(i, j, value);
                    }
                    else
                    {
                        if (lhsValue != 0.0)
                            result.SetValue(i, j, lhsValue);
                    }
                }

                for (int p = rhsRowIndex[i]; p < rhsRowIndex[i + 1]; p++)
                {
                    j = rhsColumns[p];
                    index = i + j * numberOfRows;
                    if (inspectedSet.Add(index))
                    {
                        rhsValue = rhsValues[p];
                        if (rhsValue != 0.0)
                            result.SetValue(i, j, -rhsValue);
                    }
                }

                // inspectedSet contains the positions which are stored at least in one matrix.
                // If position l is not in inspectedSet, then l is stored
                // neither in the left matrix, nor in the right one, and hence both matrices are zero
                // at l.
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_Sparse_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;

            double[] rhsValues = rhsSparse.values;
            int[] rhsColumns = rhsSparse.columns;
            int[] rhsRowIndex = rhsSparse.rowIndex;

            var result = (DenseDoubleMatrixImplementor)left.Clone();
            double[] resArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rhsRowIndex[i]; p < rhsRowIndex[i + 1]; p++)
                {
                    j = rhsColumns[p];
                    index = i + j * numberOfRows;
                    resArray[index] -= rhsValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Dense_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;

            double[] rhsArray = right.Storage;
            double[] resArray = result.Storage;

            int offset, index;
            for (int j = 0; j < numberOfColumns; j++)
            {
                offset = j * numberOfRows;
                for (int i = 0; i < numberOfRows; i++)
                {
                    index = i + offset;
                    resArray[index] = lhsSparse.GetValue(i, j) - rhsArray[index];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Dense_View_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_View_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] lhsArray = left.Storage;
            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int index = 0;
            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (rhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] rhsArray = rhsParentImplementor.Storage;

                        leadingDim = rhsParentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * rhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                resultArray[index] = lhsArray[index] -
                                                    rhsArray[rhsRowSubIndexes[i] + refIndex];
                        }
                    }
                    break;
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Sparse_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Sparse_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_View_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var rhsSparse = (SparseCsr3DoubleMatrixImplementor)right;

            double[] rhsValues = rhsSparse.values;
            int[] rhsColumns = rhsSparse.columns;
            int[] rhsRowIndex = rhsSparse.rowIndex;

            DenseDoubleMatrixImplementor result = (ViewDoubleMatrixImplementor)left;
            double[] resArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rhsRowIndex[i]; p < rhsRowIndex[i + 1]; p++)
                {
                    j = rhsColumns[p];
                    index = i + j * numberOfRows;
                    resArray[index] -= rhsValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_View_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_View_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_Sparse_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            var lhsSparse = (SparseCsr3DoubleMatrixImplementor)left;
            double[] resArray = result.Storage;

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            // = RhsSub.m_parentImplementor;
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsReferredImplementor);

            int l = 0;
            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int refOffset, offset;

            switch (rhsReferredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] rhsArray = rhsReferredImplementor.Storage;

                        refOffset = rhsReferredImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            offset = refOffset * rhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, l++)
                                resArray[l] = lhsSparse.GetValue(i, j) -
                                    rhsArray[rhsRowSubIndexes[i] + offset];
                        }
                    }
                    break;
            }

            return result;
        }

        internal static MatrixImplementor<double> Matrix_View_Dense_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_View_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);


            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] rhsArray = right.Storage;
            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);
            IndexCollection[] lhsSubIndexes = lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsParentImplementor);

            int index = 0;
            int[] lhsRowSubIndexes = lhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = lhsSubIndexes[1].indexes;

            int leadingDim, refIndex;

            switch (lhsParentImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        double[] lhsArray = lhsParentImplementor.Storage;

                        leadingDim = lhsParentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            refIndex = leadingDim * lhsColSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, index++)
                                resultArray[index] = lhsArray[lhsRowSubIndexes[i] + refIndex] -
                                                    rhsArray[index];
                        }
                    }
                    break;
            }

            return result;
        }


        //
        // SUBTRACT - DOUBLE - SUB - SUB
        //

        internal static MatrixImplementor<double> Matrix_View_View_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_View_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return Scalar_View_RightSubtract(left, right[0]);
            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseDoubleMatrixImplementor(numberOfRows, numberOfColumns);

            double[] resultArray = result.Storage;

            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);
            IndexCollection[] lhsSubIndexes = lhsSub.GetReferredImplementor(out MatrixImplementor<double> lhsParentImplementor);

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);
            IndexCollection[] rhsSubIndexes = rhsSub.GetReferredImplementor(out MatrixImplementor<double> rhsParentImplementor);

            int index = 0;
            int[] lhsRowSubIndexes = lhsSubIndexes[0].indexes;
            int[] lhsColSubIndexes = lhsSubIndexes[1].indexes;

            int[] rhsRowSubIndexes = rhsSubIndexes[0].indexes;
            int[] rhsColSubIndexes = rhsSubIndexes[1].indexes;

            int lhsLeadingDim, lhsRefIndex, rhsLeadingDim, rhsRefIndex;

            if (StorageScheme.Dense == lhsParentImplementor.StorageScheme)
            {
                if (StorageScheme.Dense == rhsParentImplementor.StorageScheme)
                {
                    double[] lhsArray = lhsParentImplementor.Storage;
                    double[] rhsArray = rhsParentImplementor.Storage;

                    lhsLeadingDim = lhsParentImplementor.NumberOfRows;
                    rhsLeadingDim = rhsParentImplementor.NumberOfRows;

                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        lhsRefIndex = lhsLeadingDim * lhsColSubIndexes[j];
                        rhsRefIndex = rhsLeadingDim * rhsColSubIndexes[j];

                        for (int i = 0; i < numberOfRows; i++, index++)
                            resultArray[index] = lhsArray[lhsRowSubIndexes[i] + lhsRefIndex] -
                                                rhsArray[rhsRowSubIndexes[i] + rhsRefIndex];
                    }
                }
            }

            return result;
        }

        #endregion

        #region Divide

        internal static MatrixImplementor<double> Matrix_Dense_Dense_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            if (1 == left.Count)
                return Scalar_Dense_LeftDivide(right, left[0]);

            if (1 == right.Count)
                return Scalar_Dense_RightDivide(left, right[0]);

            /* REMARKS
             * 
             * Local variable "numberOfColumns" represents the number of columns in L, which has 
             * the same number of columns of R.
             * 
             * The "quotient" matrix has size leftNumberOfRows by rightNumberOfRows
             */

            CheckDivisionParameters(
                left,
                right,
                out int leftNumberOfRows,
                out int numberOfColumns,
                out int rightNumberOfRows);

            MatrixImplementor<double> result;
            double[] leftArray = left.Storage;

            double[] rightArray = right.Storage;

            if (rightNumberOfRows == numberOfColumns) // R is square
            {
                bool isSpecializedAlgorithmSuccessfull = false;
                result = new DenseDoubleMatrixImplementor(leftNumberOfRows, rightNumberOfRows);

                double[] resultArray = result.Storage;

                int rhsLowerBandwidth = right.LowerBandwidth;

                if (0 == rhsLowerBandwidth) // ( right.IsUpperTriangular ) // Execute back substitution
                {
                    leftArray.CopyTo(resultArray, 0);

                    SafeNativeMethods.BLAS_dtrsm(
                        SafeNativeMethods.BLAS.ORDER.ColMajor,
                        SafeNativeMethods.BLAS.SIDE.Right, //CblasRight,
                        SafeNativeMethods.BLAS.UPLO.Upper, //CblasUpper, 
                        SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //CblasNoTrans,
                        SafeNativeMethods.BLAS.DIAG.NonUnit, //CblasNonUnit, 
                        leftNumberOfRows, // K, 
                        numberOfColumns, // N,
                        1.0, // alpha, 
                        rightArray, //constant double *A, 
                        rightNumberOfRows,
                        resultArray, // double *B, 
                        leftNumberOfRows);

                    isSpecializedAlgorithmSuccessfull = true;

                }
                else  // Not UpperTriangular
                {
                    if (right.UpperBandwidth == 0) // IsLowerTriangular? Execute forward substitution
                    {
                        leftArray.CopyTo(resultArray, 0);

                        SafeNativeMethods.BLAS_dtrsm(
                            SafeNativeMethods.BLAS.ORDER.ColMajor,
                            SafeNativeMethods.BLAS.SIDE.Right, //CblasRight,
                            SafeNativeMethods.BLAS.UPLO.Lower, //CblasLower, 
                            SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //CblasNoTrans,
                            SafeNativeMethods.BLAS.DIAG.NonUnit, //CblasNonUnit, 
                            leftNumberOfRows, // K, 
                            numberOfColumns, // N,
                            1.0, // alpha, 
                            rightArray, //constant double *A, 
                            rightNumberOfRows,
                            resultArray, // double *B, 
                            leftNumberOfRows);

                        isSpecializedAlgorithmSuccessfull = true;

                    }
                    else  // Not LowerTriangular nor UpperTriangular 
                    {
                        if (right.IsSymmetric) // Try a Cholesky decomposition
                        {
                            //SByte upperLower = Convert.ToSByte('U');
                            char uplo = 'U';
                            leftArray.CopyTo(resultArray, 0);

                            // We need to clone right, since DPOSV overwrites it
                            double[] clonedRightArray = new double[rightArray.Length];
                            rightArray.CopyTo(clonedRightArray, 0);

                            // we're solving L / R as the solution of L = X * R, or X = L * inverse( R )
                            //                                       KxN KxM MxN
                            // 
                            // R being a symmetric matrix,
                            // but DPOSV solves systems like A * X = B, hence we need to solve
                            // our system as the equivalent R' * X' = L'.
                            //                             NxM  MxK  NxK
                            //
                            // In order to do so, it's better to set DPOSV parameter "matrix_order" 
                            // to LAPACK.ORDER.RowMajor.
                            //
                            // In this way, 
                            // we can pass bi-dimensional parameters as follows: 
                            //  A <- col(R) = row(R')
                            //  B <- col(L) = row(L')
                            // and then the returned solution will be row(X'), which is col(X), i.e.
                            // what we need.
                            //
                            // Note also that, since "matrix_order" is RowMajor, leadingA and leadingB must 
                            // represent the number of columns in R' and L', respectively.

                            int lapackInfo = SafeNativeMethods.LAPACK_dposv(
                                SafeNativeMethods.LAPACK.ORDER.RowMajor,
                                uplo,
                                numberOfColumns, // numberOfIndividuals, 
                                leftNumberOfRows, // rhsNumber, 
                                clonedRightArray, // a, the R matrix
                                rightNumberOfRows, // ldA
                                resultArray, // b, the L' matrix
                                leftNumberOfRows);

                            if (lapackInfo == 0)
                            { // all is OK
                                isSpecializedAlgorithmSuccessfull = true;
                            }
                        } // End - IsSymmetric
                    }  // End - IsLowerTriangual
                }  // End - IsUpperTriangular

                if (false == isSpecializedAlgorithmSuccessfull)
                {
                    // Here R is SQUARE, but not LOWER or UPPER TRIANGULAR, nor SYMMETRIC
                    // hence apply LU decomposition

                    // Will store the LU factors of the ROW-ordered representation of the transposed of matrix right
                    var transposedRightFactors = (DenseDoubleMatrixImplementor)right.Clone();

                    /* REMARK
                     * 
                     * The transposition of right is not needed since in the following call to DGETRS
                     * we need to pass row(R'), which is col(R).
                     */

                    double[] transposedRightFactorsArray = transposedRightFactors.Storage;
                    int[] ipiv = new int[numberOfColumns];

                    int lapackInfo = SafeNativeMethods.LAPACK_dgetrf(
                        SafeNativeMethods.LAPACK.ORDER.RowMajor,
                        rightNumberOfRows,
                        numberOfColumns,
                        transposedRightFactorsArray, // = col(R) = row(R')
                        rightNumberOfRows,
                        ipiv);

                    if (0 == lapackInfo)
                    {
                        // Here LU is col( R-decomposed ) 

                        // we're solving L / R as the solution of L = X * R, or X = L * inverse( R )
                        //                                       KxN KxM MxN
                        // 
                        // but DGETRS solves systems like op(A) * X = B, hence we need to solve
                        // our system as the equivalent R' * X' = L'.
                        //                             NxM  MxK  NxK
                        //
                        // In order to do so, it's better to set DGETRS parameter "matrix_order" 
                        // to LAPACK.ORDER.RowMajor.
                        //
                        // In this way, 
                        // we can pass bi-dimensional parameters as follows: 
                        //  A <- col(R) = row(R')
                        //  B <- col(L) = row(L')
                        // and then the returned solution will be row(X'), which is col(X), i.e.
                        // what we need.
                        //
                        // Note also that, since "matrix_order" is RowMajor, leadingA and leadingB must 
                        // represent the number of columns in R' and L', respectively.

                        result = (DenseDoubleMatrixImplementor)left.Clone();
                        resultArray = result.Storage;

                        //SByte trans = Convert.ToSByte('N');
                        char trans = 'N';

                        _ = SafeNativeMethods.LAPACK_dgetrs(
                            SafeNativeMethods.LAPACK.ORDER.RowMajor,
                            trans,
                            rightNumberOfRows,
                            leftNumberOfRows,
                            transposedRightFactorsArray,
                            rightNumberOfRows,
                            ipiv,
                            resultArray,
                            leftNumberOfRows);
                    }
                }  // End - ~IsSpecializedAlgorithmSuccessfull
            }  // End - IsSquare
            else  // Not IsSquare
            {
                // Here if R is not square, use QR decomposition

                // we're solving L / R as the solution of L = X * R, or X = L * inverse( R )
                //                                       KxN KxM MxN
                // 
                // where
                // 
                // K == leftNumberOfRows, N = numberOfColumns, M = rightNumberOfRows,
                //
                // but DGELS solves systems like op(A) * X = B, hence we need to solve
                // our system as the equivalent R' * X' = L'.
                //                             NxM  MxK  NxK
                //
                // In order to do so, it's better to set DGETRS parameter "matrix_order" 
                // to LAPACK.ORDER.RowMajor.
                //
                // In this way, 
                // we can pass bi-dimensional parameters as follows: 
                //  A <- col(R) = row(R')
                //  B <- col(L) = row(L')
                // and then the returned solution will be row(X'), which is col(X), i.e.
                // what we need.
                //
                // Additional parameters of DGELS must be passed as follows:
                //
                // trans = 'N' (since col(R) = row(R'), hence we don't need to transpose).
                // m = N = numberOfColumns (the number of rows of R').
                // n = M = rightNumberOfRows (number of columns of R').
                // nRHS = K = leftNumberOfRows (number of columns of L').
                // a = row major ordered array which represents R'.
                // ldA = rightNumberOfRows (number of columns in R', since "matrix_order" is RowMajor). 
                // b = row major ordered array which represents a matrix of dimensions max(m,n) by nRHS,
                //     containing the information described below.
                //
                //
                //     CASE i: m < n (i.e. N < M, numberOfColumns < rightNumberOfRows).
                //
                //     --> ON ENTRY
                //     b represents the following matrix:
                //                                        nRHS, K, leftNumberOfRows
                //                                     
                //                                           --       --
                //                                           |         |
                //             m, N, numberOfColumns         | row(L') |    
                //                                           |         |
                //                                           |---------|
                //                     n - m                 |         |    
                //                     M - N                 |    O    |    (O a zeroed matrix)
                //     rightNumberOfRows - numberOfColumns   |         |
                //                                           --       --
                //     --> ON EXIT
                //     b represents row(X'), hence col(X), the (n by nRHS) solution.
                //
                //
                //     CASE ii: m >= n (i.e. N >= M, numberOfColumns >= rightNumberOfRows).
                //
                //     --> ON ENTRY
                //     b represents row(L'), hence col(L), the (m by nRHS) matrix of right hand side vectors.
                //
                //     --> ON EXIT
                //     b represents the following matrix:
                //                                        nRHS, K, leftNumberOfRows
                //                                     
                //                                           --       --
                //                                           |         |
                //             n, M, rightNumberOfRows       | row(X') |     
                //                                           |         |
                //                                           |---------|
                //                     m - n                 |         |    
                //                     N - M                 |    O    |    (O a zeroed matrix)
                //     numberOfColumns - rightNumberOfRows   |         |
                //                                           --       --
                //
                // ldB = leftNumberOfRows (number of columns in L', since "matrix_order" is RowMajor)

                /* REMARK
                 * 1. If trans = 'N' and m ≥ n: 
                 * 
                 * find the least squares solution of an overdetermined system, 
                 * 
                 * that is, solve the least squares problem:
                 * 
                 * minimize ||b - A*x||
                 *                     2
                 *                     
                 * 2. If trans = 'N' and m < n: 
                 * 
                 * find the minimum norm solution of an under-determined system A*X = B.
                 */

                int lapackInfo;

                double[] clonedRightArray = new double[rightArray.Length];
                rightArray.CopyTo(clonedRightArray, 0);

                char trans = 'N';

                // Parameter n less than m if and only if m_LT_n == true
                bool m_LT_n = numberOfColumns < rightNumberOfRows;

                // maxRightDimension == max{m,n}
                int maxRightDimension = (m_LT_n) ? rightNumberOfRows : numberOfColumns;

                double[] clonedLeftArray = new double[maxRightDimension * leftNumberOfRows];
                leftArray.CopyTo(clonedLeftArray, 0);

                lapackInfo = SafeNativeMethods.LAPACK_dgels(
                    SafeNativeMethods.LAPACK.ORDER.RowMajor,
                    trans, // 'N'
                    numberOfColumns, // m
                    rightNumberOfRows, // n
                    leftNumberOfRows, // nRHS
                    clonedRightArray, // a = col(R) = row(R')
                    rightNumberOfRows,
                    clonedLeftArray,
                    leftNumberOfRows);

                if (0 == lapackInfo)
                {
                    if (m_LT_n)
                    {
                        result = new DenseDoubleMatrixImplementor(leftNumberOfRows, rightNumberOfRows, clonedLeftArray,
                            StorageOrder.ColumnMajor);
                    }
                    else
                    {
                        result = new DenseDoubleMatrixImplementor(leftNumberOfRows, rightNumberOfRows);
                        double[] resultArray = result.Storage;

                        Array.Copy(clonedLeftArray, 0, resultArray, 0, resultArray.Length);
                    }
                }
                else
                {
                    throw new ArgumentException(
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_MAT_DIVIDE_RANK_DEFICIENT_OPERATION"),
                       nameof(right));
                }
            }  // End - NOT - IsSquare

            return result;
        }

        internal static MatrixImplementor<double> Matrix_Sparse_View_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);

            rhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (rhsSub.ChangingDataHandler);

            rhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (rhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor rhsDenseMatrix = rhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(rhsDenseMatrix);
            rhsSub.OnImplementorChanged(args);

            DenseDoubleMatrixImplementor denseLhsMatrix = (SparseCsr3DoubleMatrixImplementor)left;

            return Matrix_Dense_Dense_Divide(denseLhsMatrix, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_Dense_View_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);

            rhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (rhsSub.ChangingDataHandler);

            rhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (rhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor rhsDenseMatrix = rhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(rhsDenseMatrix);
            rhsSub.OnImplementorChanged(args);

            return Matrix_Dense_Dense_Divide(left, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_View_Dense_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);

            lhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (lhsSub.ChangingDataHandler);

            lhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (lhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor lhsDenseMatrix = lhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(lhsDenseMatrix);
            lhsSub.OnImplementorChanged(args);

            return Matrix_Dense_Dense_Divide(lhsDenseMatrix, right);
        }

        internal static MatrixImplementor<double> Matrix_View_Sparse_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);

            lhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (lhsSub.ChangingDataHandler);

            lhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (lhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor lhsDenseMatrix = lhsSub;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(lhsDenseMatrix);
            lhsSub.OnImplementorChanged(args);

            DenseDoubleMatrixImplementor rhsDenseMatrix = (SparseCsr3DoubleMatrixImplementor)right;

            return Matrix_Dense_Dense_Divide(lhsDenseMatrix, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Sparse_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            DenseDoubleMatrixImplementor lhsDenseMatrix = (SparseCsr3DoubleMatrixImplementor)left;

            DenseDoubleMatrixImplementor rhsDenseMatrix = (SparseCsr3DoubleMatrixImplementor)right;

            return Matrix_Dense_Dense_Divide(lhsDenseMatrix, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_Dense_Sparse_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            DenseDoubleMatrixImplementor rhsDenseMatrix = (SparseCsr3DoubleMatrixImplementor)right;

            return Matrix_Dense_Dense_Divide(left, rhsDenseMatrix);
        }

        internal static MatrixImplementor<double> Matrix_Sparse_Dense_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            DenseDoubleMatrixImplementor lhsDenseMatrix = (SparseCsr3DoubleMatrixImplementor)left;

            return Matrix_Dense_Dense_Divide(lhsDenseMatrix, right);
        }

        internal static MatrixImplementor<double> Matrix_View_View_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<double> right)
        {
            ViewDoubleMatrixImplementor lhsSub = (ViewDoubleMatrixImplementor)(left);

            lhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (lhsSub.ChangingDataHandler);

            lhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (lhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor lhsDenseMatrix = lhsSub;

            ImplementorChangedEventArgs lArgs = new ImplementorChangedEventArgs(lhsDenseMatrix);
            lhsSub.OnImplementorChanged(lArgs);

            ViewDoubleMatrixImplementor rhsSub = (ViewDoubleMatrixImplementor)(right);

            rhsSub.parentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (rhsSub.ChangingDataHandler);

            rhsSub.parentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (rhsSub.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor rhsDenseMatrix = rhsSub;

            ImplementorChangedEventArgs rArgs = new ImplementorChangedEventArgs(rhsDenseMatrix);
            rhsSub.OnImplementorChanged(rArgs);

            return Matrix_Dense_Dense_Divide(lhsDenseMatrix, rhsDenseMatrix);
        }

        #endregion

        #endregion
    }
}












