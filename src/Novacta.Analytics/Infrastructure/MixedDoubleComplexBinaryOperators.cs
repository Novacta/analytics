using Novacta.Analytics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Provides implementations of binary operators having
    /// <see cref="double"/> type-based left operands and
    /// <see cref="Complex"/> type-based right operands.
    /// </summary>
    internal class MixedDoubleComplexBinaryOperators
    {
        #region Matrix

        #region Helper functions

        internal static void CheckElementwiseOperationParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right,
            out int numberOfRows,
            out int numberOfColumns)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = left.NumberOfColumns;

            int rightNumberOfRows = right.NumberOfRows;
            int rightNumberOfColumns = right.NumberOfColumns;

            if ((numberOfRows != rightNumberOfRows) || (numberOfColumns != rightNumberOfColumns))
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_ELEMENT_WISE_ALL_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckMultiplicationParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right,
            out int numberOfRows,
            out int numberOfColumns,
            out int innerDimension)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = right.NumberOfColumns;

            int leftNumberOfColumns = left.NumberOfColumns;
            innerDimension = right.NumberOfRows;

            if (innerDimension != leftNumberOfColumns)
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_MULTIPLY_INNER_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckAdditionParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right,
            out int numberOfRows,
            out int numberOfColumns)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = left.NumberOfColumns;

            int rightNumberOfRows = right.NumberOfRows;
            int rightNumberOfColumns = right.NumberOfColumns;

            if ((numberOfRows != rightNumberOfRows) || (numberOfColumns != rightNumberOfColumns))
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_ADD_ALL_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckSubtractionParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right,
            out int numberOfRows,
            out int numberOfColumns)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = left.NumberOfColumns;

            int rightNumberOfRows = right.NumberOfRows;
            int rightNumberOfColumns = right.NumberOfColumns;

            if ((numberOfRows != rightNumberOfRows) || (numberOfColumns != rightNumberOfColumns))
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_SUBTRACT_ALL_DIMS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        internal static void CheckDivisionParameters(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right,
            out int numberOfRows,
            out int numberOfColumns,
            out int innerDimension)
        {
            numberOfRows = left.NumberOfRows;
            numberOfColumns = right.NumberOfColumns;

            int leftNumberOfColumns = left.NumberOfColumns;
            innerDimension = right.NumberOfRows;

            if (numberOfColumns != leftNumberOfColumns)
            {
                throw new ArgumentException(
                   message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_MAT_DIVIDE_COLUMNS_MUST_MATCH"),
                   paramName: nameof(right));
            }
        }

        #endregion

        #region ElementWiseMultiply

        internal static MatrixImplementor<Complex> Matrix_Dense_Dense_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            double[] leftArray = left.Storage;
            Complex[] rightArray = right.Storage;
            Complex[] resultArray = result.Storage;

            for (int i = 0; i < resultArray.Length; i++)
                resultArray[i] = leftArray[i] * rightArray[i];

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Dense_Sparse_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;

            var result = new SparseCsr3ComplexMatrixImplementor(
                numberOfRows, numberOfColumns, rightSparse.capacity);
            rightSparse.columns.CopyTo(result.columns, 0);
            rightSparse.rowIndex.CopyTo(result.rowIndex, 0);

            double[] leftArray = left.Storage;
            Complex[] rightValues = rightSparse.values;

            int[] columns = result.columns;
            int[] rowIndex = result.rowIndex;

            Complex[] resValues = result.values;

            int j;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                {
                    j = columns[p];

                    resValues[p] = rightValues[p] * leftArray[i + j * numberOfRows];
                }
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Dense_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var leftSparse = (SparseCsr3DoubleMatrixImplementor)left;

            var result = new SparseCsr3ComplexMatrixImplementor(
                numberOfRows, numberOfColumns, leftSparse.capacity);
            leftSparse.columns.CopyTo(result.columns, 0);
            leftSparse.rowIndex.CopyTo(result.rowIndex, 0);

            Complex[] rightArray = right.Storage;
            double[] leftValues = leftSparse.values;

            int[] columns = result.columns;
            int[] rowIndex = result.rowIndex;

            Complex[] resValues = result.values;

            int j;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                {
                    j = columns[p];

                    resValues[p] = leftValues[p] * rightArray[i + j * numberOfRows];
                }
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Sparse_ElementWiseMultiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            CheckElementwiseOperationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var leftSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var leftValues = leftSparse.values;
            var leftColumns = leftSparse.columns;
            var leftRowIndex = leftSparse.rowIndex;
            int leftNumberOfStoredPositions = leftRowIndex[numberOfRows];

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;
            var rightValues = rightSparse.values;
            var rightRowIndex = rightSparse.rowIndex;
            int rightNumberOfStoredPositions = rightRowIndex[numberOfRows];

            double resultLength = rightSparse.Count;
            int resultCapacity = Convert.ToInt32(Math.Ceiling(
                leftNumberOfStoredPositions * rightNumberOfStoredPositions / resultLength));
            var result = new SparseCsr3ComplexMatrixImplementor(
                numberOfRows, numberOfColumns, resultCapacity);

            HashSet<int> inspectedSet = [];

            Complex leftValue, rightValue;
            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {

                for (int p = leftRowIndex[i]; p < leftRowIndex[i + 1]; p++)
                {
                    j = leftColumns[p];
                    index = i + j * numberOfRows;
                    inspectedSet.Add(index);
                    leftValue = leftValues[p];
                    if (leftValue != 0.0)
                    {
                        if (rightSparse.TryGetPosition(i, j, out int positionIndex))
                        {
                            rightValue = rightValues[positionIndex];
                            if (rightValue != 0.0)
                            {
                                result.SetValue(i, j, leftValue * rightValue);
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

        #endregion

        #region Multiply

        internal static MatrixImplementor<Complex> Matrix_Dense_Dense_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_Multiply(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_Multiply(left, right[0]);

            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            Complex[] leftArray = ImplementationServices.ToComplexArray(left.Storage);

            Complex[] rightArray = right.Storage;

            Complex[] resultArray = result.Storage;

            Complex one = Complex.One;
            Complex zero = Complex.Zero;

            unsafe
            {
                fixed (Complex* leftPointer = &leftArray[0])
                fixed (Complex* rightPointer = &rightArray[0])
                fixed (Complex* resultPointer = &resultArray[0])

                    SafeNativeMethods.BLAS.ZGEMM(
                        SafeNativeMethods.BLAS.ORDER.ColMajor,
                        SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //'N', // transA
                        SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //'N', // transB
                        numberOfRows, // M 
                        numberOfColumns, //N
                        innerDimension, // K
                        &one, // alpha, 
                        leftPointer, //constant double *A,
                        numberOfRows,
                        rightPointer, //constant double *B, 
                        innerDimension,
                        &zero, // beta, 
                        resultPointer, //double *C, 
                        numberOfRows);
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Dense_Sparse_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Sparse_Multiply(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_Multiply(left, right[0]);

            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;
            var rightValues = rightSparse.values;
            var rightColumns = rightSparse.columns;
            var rightRowIndex = rightSparse.rowIndex;

            var leftDense = (DenseDoubleMatrixImplementor)left;
            var leftArray = leftDense.storage;

            Parallel.For(0, numberOfRows, i =>
            {
                int j, offset;
                Complex rightValue, value;
                for (int k = 0; k < innerDimension; k++)
                {
                    offset = k * numberOfRows;
                    for (int p = rightRowIndex[k]; p < rightRowIndex[k + 1]; p++)
                    {
                        j = rightColumns[p];
                        rightValue = rightValues[p];
                        value = rightValue * leftArray[i + offset];
                        if (value != 0.0)
                            result[i + j * numberOfRows] += value;
                    }
                }
            });

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Dense_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_Multiply(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Sparse_Multiply(left, right[0]);

            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            var leftSparse =
                (SparseCsr3ComplexMatrixImplementor)
                    (SparseCsr3DoubleMatrixImplementor)left;
            var leftValues = leftSparse.values;
            var leftColumns = leftSparse.columns;
            var leftRowIndex = leftSparse.rowIndex;

            var rightDense = (DenseComplexMatrixImplementor)right;
            var rightArray = rightDense.storage;
            var resultArray = result.storage;

            Parallel.For(0, numberOfRows, i =>
            {
                Complex leftValue, value;
                int k;
                for (int p = leftRowIndex[i]; p < leftRowIndex[i + 1]; p++)
                {
                    k = leftColumns[p];
                    leftValue = leftValues[p];
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        value = leftValue * rightArray[k + j * innerDimension];
                        if (value != 0.0)
                            resultArray[i + j * numberOfRows] += value;
                    }
                }
            });

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Sparse_Multiply(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Sparse_Multiply(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Sparse_Multiply(left, right[0]);

            CheckMultiplicationParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns,
                out int innerDimension);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            var leftSparse = (SparseCsr3ComplexMatrixImplementor)
                (SparseCsr3DoubleMatrixImplementor)left;
            var leftValues = leftSparse.values;
            var leftColumns = leftSparse.columns;
            var leftRowIndex = leftSparse.rowIndex;
            int leftNumberOfStoredPositions = leftRowIndex[numberOfRows];

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;
            var rightValues = rightSparse.values;
            var rightColumns = rightSparse.columns;
            var rightRowIndex = rightSparse.rowIndex;
            int rightNumberOfStoredPositions = rightRowIndex[innerDimension];

            var resultArray = result.storage;

            Parallel.For(0, numberOfRows, i =>
            {
                Complex leftValue, value;
                int k;
                for (int p = leftRowIndex[i]; p < leftRowIndex[i + 1]; p++)
                {
                    k = leftColumns[p];
                    leftValue = leftValues[p];
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        if (rightSparse.TryGetPosition(k, j, out int positionIndex))
                        {
                            value = leftValue * rightValues[positionIndex];
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

        #endregion

        #region Add

        internal static MatrixImplementor<Complex> Matrix_Dense_Dense_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_Sum(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_Sum(left, right[0]);

            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            double[] leftArray = left.Storage;
            Complex[] rightArray = right.Storage;
            Complex[] resultArray = result.Storage;

            for (int i = 0; i < resultArray.Length; i++)
                resultArray[i] = leftArray[i] + rightArray[i];

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Dense_Sparse_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Sparse_Sum(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_Sum(left, right[0]);

            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;

            Complex[] rightValues = rightSparse.values;
            int[] rightColumns = rightSparse.columns;
            int[] rightRowIndex = rightSparse.rowIndex;

            var result =
                (DenseComplexMatrixImplementor)
                    (DenseDoubleMatrixImplementor)left;
            Complex[] resultArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rightRowIndex[i]; p < rightRowIndex[i + 1]; p++)
                {
                    j = rightColumns[p];
                    index = i + j * numberOfRows;
                    resultArray[index] += rightValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Dense_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_Sum(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Sparse_Sum(left, right[0]);

            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var leftSparse = (SparseCsr3DoubleMatrixImplementor)left;

            double[] leftValues = leftSparse.values;
            int[] leftColumns = leftSparse.columns;
            int[] leftRowIndex = leftSparse.rowIndex;

            var result = (DenseComplexMatrixImplementor)right.Clone();
            Complex[] resultArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = leftRowIndex[i]; p < leftRowIndex[i + 1]; p++)
                {
                    j = leftColumns[p];
                    index = i + j * numberOfRows;
                    resultArray[index] += leftValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Sparse_Sum(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Sparse_Sum(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Sparse_Sum(left, right[0]);

            CheckAdditionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var leftSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var leftValues = leftSparse.values;
            var leftColumns = leftSparse.columns;
            var leftRowIndex = leftSparse.rowIndex;
            int leftNumberOfStoredPositions = leftRowIndex[numberOfRows];

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;
            var rightValues = rightSparse.values;
            var rightColumns = rightSparse.columns;
            var rightRowIndex = rightSparse.rowIndex;
            int rightNumberOfStoredPositions = rightRowIndex[numberOfRows];

            int resultCapacity = leftNumberOfStoredPositions + rightNumberOfStoredPositions;
            var result = new SparseCsr3ComplexMatrixImplementor(numberOfRows, numberOfColumns, resultCapacity);

            HashSet<int> inspectedSet = [];

            Complex leftValue, rightValue, value;
            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = leftRowIndex[i]; p < leftRowIndex[i + 1]; p++)
                {
                    j = leftColumns[p];
                    index = i + j * numberOfRows;
                    inspectedSet.Add(index);
                    leftValue = leftValues[p];
                    if (rightSparse.TryGetPosition(i, j, out int positionIndex))
                    {
                        value = leftValue + rightValues[positionIndex];
                        if (value != 0.0)
                            result.SetValue(i, j, value);
                    }
                    else
                    {
                        if (leftValue != 0.0)
                            result.SetValue(i, j, leftValue);
                    }
                }

                for (int p = rightRowIndex[i]; p < rightRowIndex[i + 1]; p++)
                {
                    j = rightColumns[p];
                    index = i + j * numberOfRows;
                    if (inspectedSet.Add(index))
                    {
                        rightValue = rightValues[p];
                        if (rightValue != 0.0)
                            result.SetValue(i, j, rightValue);
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

        #endregion

        #region Subtract

        internal static MatrixImplementor<Complex> Matrix_Dense_Dense_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_RightSubtract(left, right[0]);

            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            double[] leftArray = left.Storage;
            Complex[] rightArray = right.Storage;
            Complex[] resultArray = result.Storage;

            for (int i = 0; i < resultArray.Length; i++)
                resultArray[i] = leftArray[i] - rightArray[i];

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Dense_Sparse_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Sparse_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_RightSubtract(left, right[0]);

            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out _);

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;

            Complex[] rightValues = rightSparse.values;
            int[] rightColumns = rightSparse.columns;
            int[] rightRowIndex = rightSparse.rowIndex;

            var result = (DenseComplexMatrixImplementor)
                (DenseDoubleMatrixImplementor)left;
            Complex[] resultArray = result.Storage;

            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int p = rightRowIndex[i]; p < rightRowIndex[i + 1]; p++)
                {
                    j = rightColumns[p];
                    index = i + j * numberOfRows;
                    resultArray[index] -= rightValues[p];
                }
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Dense_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Sparse_RightSubtract(left, right[0]);

            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var result = new DenseComplexMatrixImplementor(numberOfRows, numberOfColumns);

            var leftSparse = (SparseCsr3DoubleMatrixImplementor)left;

            Complex[] rightArray = right.Storage;
            Complex[] resultArray = result.storage;

            int offset, index;
            for (int j = 0; j < numberOfColumns; j++)
            {
                offset = j * numberOfRows;
                for (int i = 0; i < numberOfRows; i++)
                {
                    index = i + offset;
                    resultArray[index] = leftSparse.GetValue(i, j) - rightArray[index];
                }
            }

            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Sparse_Subtract(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Sparse_LeftSubtract(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Sparse_RightSubtract(left, right[0]);

            CheckSubtractionParameters(
                left,
                right,
                out int numberOfRows,
                out int numberOfColumns);

            var leftSparse = (SparseCsr3DoubleMatrixImplementor)left;
            var leftValues = leftSparse.values;
            var leftColumns = leftSparse.columns;
            var leftRowIndex = leftSparse.rowIndex;
            int leftNumberOfStoredPositions = leftRowIndex[numberOfRows];

            var rightSparse = (SparseCsr3ComplexMatrixImplementor)right;
            var rightValues = rightSparse.values;
            var rightColumns = rightSparse.columns;
            var rightRowIndex = rightSparse.rowIndex;
            int rightNumberOfStoredPositions = rightRowIndex[numberOfRows];

            int resultCapacity = leftNumberOfStoredPositions + rightNumberOfStoredPositions;
            var result = new SparseCsr3ComplexMatrixImplementor(
                numberOfRows, numberOfColumns, resultCapacity);

            HashSet<int> inspectedSet = [];

            Complex leftValue, rightValue, value;
            int j, index;
            for (int i = 0; i < numberOfRows; i++)
            {

                for (int p = leftRowIndex[i]; p < leftRowIndex[i + 1]; p++)
                {
                    j = leftColumns[p];
                    index = i + j * numberOfRows;
                    inspectedSet.Add(index);
                    leftValue = leftValues[p];
                    if (rightSparse.TryGetPosition(i, j, out int positionIndex))
                    {
                        value = leftValue - rightValues[positionIndex];
                        if (value != 0.0)
                            result.SetValue(i, j, value);
                    }
                    else
                    {
                        if (leftValue != 0.0)
                            result.SetValue(i, j, leftValue);
                    }
                }

                for (int p = rightRowIndex[i]; p < rightRowIndex[i + 1]; p++)
                {
                    j = rightColumns[p];
                    index = i + j * numberOfRows;
                    if (inspectedSet.Add(index))
                    {
                        rightValue = rightValues[p];
                        if (rightValue != 0.0)
                            result.SetValue(i, j, -rightValue);
                    }
                }

                // inspectedSet contains the positions which are stored at least in one matrix.
                // If position l is not in inspectedSet, then l is stored
                // neither in the left matrix, nor in the right one, and hence both matrices are zero
                // at l.
            }

            return result;
        }

        #endregion

        #region Divide

        internal static MatrixImplementor<Complex> Matrix_Dense_Dense_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            if (1 == left.Count)
                return ComplexMatrixOperators.Scalar_Dense_LeftDivide(right, left[0]);

            if (1 == right.Count)
                return DoubleMatrixOperators.Scalar_Dense_RightDivide(left, right[0]);

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

            MatrixImplementor<Complex> result = null;
            double[] leftArray = left.Storage;

            Complex[] rightArray = right.Storage;

            if (rightNumberOfRows == numberOfColumns) // R is square
            {
                bool isSpecializedAlgorithmSuccessfull = false;

                int rightLowerBandwidth = right.LowerBandwidth;

                if (0 == rightLowerBandwidth) // ( right.IsUpperTriangular ) // Execute back substitution
                {
                    result = new DenseComplexMatrixImplementor(
                        numberOfRows: leftNumberOfRows,
                        numberOfColumns: rightNumberOfRows,
                        data: ImplementationServices.ToComplexArray(leftArray),
                        copyData: false);

                    Complex[] resultArray = result.Storage;

                    Complex one = Complex.One;

                    unsafe
                    {
                        fixed (Complex* rightPointer = &rightArray[0])
                        fixed (Complex* resultPointer = &resultArray[0])

                            SafeNativeMethods.BLAS.ZTRSM(
                                SafeNativeMethods.BLAS.ORDER.ColMajor,
                                SafeNativeMethods.BLAS.SIDE.Right, //CblasRight,
                                SafeNativeMethods.BLAS.UPLO.Upper, //CblasUpper, 
                                SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //CblasNoTrans,
                                SafeNativeMethods.BLAS.DIAG.NonUnit, //CblasNonUnit, 
                                leftNumberOfRows, // K, 
                                numberOfColumns, // N,
                                &one, // alpha, 
                                rightPointer, //constant double *A, 
                                rightNumberOfRows,
                                resultPointer, // double *B, 
                                leftNumberOfRows);
                    }

                    isSpecializedAlgorithmSuccessfull = true;

                }
                else  // Not UpperTriangular
                {
                    if (right.UpperBandwidth == 0) // IsLowerTriangular? Execute forward substitution
                    {
                        result = new DenseComplexMatrixImplementor(
                            numberOfRows: leftNumberOfRows,
                            numberOfColumns: rightNumberOfRows,
                            data: ImplementationServices.ToComplexArray(leftArray),
                            copyData: false);

                        Complex[] resultArray = result.Storage;

                        Complex one = Complex.One;
                        unsafe
                        {
                            fixed (Complex* rightPointer = &rightArray[0])
                            fixed (Complex* resultPointer = &resultArray[0])

                                SafeNativeMethods.BLAS.ZTRSM(
                                    SafeNativeMethods.BLAS.ORDER.ColMajor,
                                    SafeNativeMethods.BLAS.SIDE.Right, //CblasRight,
                                    SafeNativeMethods.BLAS.UPLO.Lower, //CblasLower, 
                                    SafeNativeMethods.BLAS.TRANSPOSE.NoTrans, //CblasNoTrans,
                                    SafeNativeMethods.BLAS.DIAG.NonUnit, //CblasNonUnit, 
                                    leftNumberOfRows, // K, 
                                    numberOfColumns, // N,
                                    &one, // alpha, 
                                    rightPointer, //constant double *A, 
                                    rightNumberOfRows,
                                    resultPointer, // double *B, 
                                    leftNumberOfRows);
                        }

                        isSpecializedAlgorithmSuccessfull = true;

                    }
                    else  // Not LowerTriangular nor UpperTriangular 
                    {
                        if (right.IsSymmetric) // Try a Cholesky decomposition
                        {
                            result = new DenseComplexMatrixImplementor(
                                numberOfRows: leftNumberOfRows,
                                numberOfColumns: rightNumberOfRows,
                                data: ImplementationServices.ToComplexArray(leftArray),
                                copyData: false);

                            Complex[] resultArray = result.Storage;

                            //SByte upperLower = Convert.ToSByte('U');
                            char uplo = 'U';

                            // We need to clone right, since DPOSV overwrites it
                            Complex[] clonedRightArray = new Complex[rightArray.Length];
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

                            int lapackInfo = SafeNativeMethods.LAPACK.ZPOSV(
                                SafeNativeMethods.LAPACK.ORDER.RowMajor,
                                uplo,
                                numberOfColumns, // numberOfIndividuals, 
                                leftNumberOfRows, // rightNumber, 
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
                    var transposedRightFactors = (DenseComplexMatrixImplementor)right.Clone();

                    /* REMARK
                     * 
                     * The transposition of right is not needed since in the following call to DGETRS
                     * we need to pass row(R'), which is col(R).
                     */

                    Complex[] transposedRightFactorsArray = transposedRightFactors.Storage;
                    int[] ipiv = new int[numberOfColumns];

                    int lapackInfo = SafeNativeMethods.LAPACK.ZGETRF(
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

                        result = (DenseComplexMatrixImplementor)
                            (DenseDoubleMatrixImplementor)left;
                        var resultArray = result.Storage;

                        //SByte trans = Convert.ToSByte('N');
                        char trans = 'N';

                        _ = SafeNativeMethods.LAPACK.ZGETRS(
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
                // nright = K = leftNumberOfRows (number of columns of L').
                // a = row major ordered array which represents R'.
                // ldA = rightNumberOfRows (number of columns in R', since "matrix_order" is RowMajor). 
                // b = row major ordered array which represents a matrix of dimensions max(m,n) by nright,
                //     containing the information described below.
                //
                //
                //     CASE i: m < n (i.e. N < M, numberOfColumns < rightNumberOfRows).
                //
                //     --> ON ENTRY
                //     b represents the following matrix:
                //                                        nright, K, leftNumberOfRows
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
                //     b represents row(X'), hence col(X), the (n by nright) solution.
                //
                //
                //     CASE ii: m >= n (i.e. N >= M, numberOfColumns >= rightNumberOfRows).
                //
                //     --> ON ENTRY
                //     b represents row(L'), hence col(L), the (m by nright) matrix of right hand side vectors.
                //
                //     --> ON EXIT
                //     b represents the following matrix:
                //                                        nright, K, leftNumberOfRows
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

                Complex[] clonedRightArray = new Complex[rightArray.Length];
                rightArray.CopyTo(clonedRightArray, 0);

                char trans = 'N';

                // Parameter n less than m if and only if m_LT_n == true
                bool m_LT_n = numberOfColumns < rightNumberOfRows;

                // maxRightDimension == max{m,n}
                int maxRightDimension = (m_LT_n) ? rightNumberOfRows : numberOfColumns;

                Complex[] clonedLeftArray = new Complex[maxRightDimension * leftNumberOfRows];

                unsafe
                {
                    fixed (void* complexArrayPtr = clonedLeftArray)
                    {
                        var unfixedComplexArrayPtr = (double*)complexArrayPtr;
                        for (int i = 0; i < leftArray.Length; i++, unfixedComplexArrayPtr += 2)
                        {
                            *unfixedComplexArrayPtr = leftArray[i];
                        }
                    }
                }

                lapackInfo = SafeNativeMethods.LAPACK.ZGELS(
                    SafeNativeMethods.LAPACK.ORDER.RowMajor,
                    trans, // 'N'
                    numberOfColumns, // m
                    rightNumberOfRows, // n
                    leftNumberOfRows, // nright
                    clonedRightArray, // a = col(R) = row(R')
                    rightNumberOfRows,
                    clonedLeftArray,
                    leftNumberOfRows);                

                if (0 == lapackInfo)
                {
                    if (m_LT_n)
                    {
                        result = new DenseComplexMatrixImplementor(
                            leftNumberOfRows,
                            rightNumberOfRows,
                            clonedLeftArray,
                            StorageOrder.ColumnMajor);
                    }
                    else
                    {
                        result = new DenseComplexMatrixImplementor(
                            leftNumberOfRows,
                            rightNumberOfRows);

                        Complex[] resultArray = result.Storage;

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

#if DEBUG
            Debug.Assert(result is not null);
#endif
            return result;
        }

        internal static MatrixImplementor<Complex> Matrix_Dense_Sparse_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            DenseComplexMatrixImplementor rightDenseMatrix = (SparseCsr3ComplexMatrixImplementor)right;

            return Matrix_Dense_Dense_Divide(left, rightDenseMatrix);
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Dense_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            DenseDoubleMatrixImplementor leftDenseMatrix = (SparseCsr3DoubleMatrixImplementor)left;

            return Matrix_Dense_Dense_Divide(leftDenseMatrix, right);
        }

        internal static MatrixImplementor<Complex> Matrix_Sparse_Sparse_Divide(
            MatrixImplementor<double> left,
            MatrixImplementor<Complex> right)
        {
            DenseDoubleMatrixImplementor leftDenseMatrix = (SparseCsr3DoubleMatrixImplementor)left;

            DenseComplexMatrixImplementor rightDenseMatrix = (SparseCsr3ComplexMatrixImplementor)right;

            return Matrix_Dense_Dense_Divide(leftDenseMatrix, rightDenseMatrix);
        }

        #endregion

        #endregion
    }
}
