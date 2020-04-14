using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixIndexerExample00 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                10, 20, 30,
                40, 50, 60
            };
            var matrix = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Specify a row index.
            int rowIndex = 1;
            Console.WriteLine();
            Console.WriteLine("Row index: {0}", rowIndex);

            // Specify a column index.
            int columnIndex = 2;
            Console.WriteLine();
            Console.WriteLine("Column index: {0}", columnIndex);

            // Set the corresponding entry.
            matrix[rowIndex, columnIndex] = -60.0;

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

            // Entries can also be accessed using a read-only wrapper of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entry:");
            Console.WriteLine(readOnlyMatrix[rowIndex, columnIndex]);
        }
    }
}