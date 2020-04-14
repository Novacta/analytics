using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixIndexerExample01 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[12] {
                1, 5,  9,
                2, 6, 10,
                3, 7, 11,
                4, 8, 12
            };
            var matrix = DoubleMatrix.Dense(4, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Specify a row index.
            int rowIndex = 1;
            Console.WriteLine();
            Console.WriteLine("Row index: {0}", rowIndex);

            // Specify some column indexes.
            var columnIndexes = IndexCollection.Sequence(0,2,2);
            Console.WriteLine();
            Console.WriteLine("Column indexes: {0}", columnIndexes);

            // Specify the value matrix.
            var valueData = new double[2] {
                -20, -100
            };
            var value = DoubleMatrix.Dense(1, 2, valueData, StorageOrder.RowMajor);
            Console.WriteLine();
            Console.WriteLine("Value matrix:");
            Console.WriteLine(value);

            // Set the entries having the specified indexes to the value matrix.
            matrix[rowIndex, columnIndexes] = value;

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

            // Entries can also be accessed using a read-only wrapper 
            // of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entries:");
            Console.WriteLine(readOnlyMatrix[rowIndex, columnIndexes]);
        }
    }
}