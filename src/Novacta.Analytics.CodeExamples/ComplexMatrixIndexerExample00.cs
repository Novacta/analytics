using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexMatrixIndexerExample00 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[8] {
                new Complex(1, -1), new Complex(5, -5),
                new Complex(2, -2), new Complex(6, -6),
                new Complex(3, -3), new Complex(7, -7),
                new Complex(4, -4), new Complex(8, -8)
            };
            var matrix = ComplexMatrix.Dense(4, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Specify a row index.
            int rowIndex = 1;
            Console.WriteLine();
            Console.WriteLine("Row index: {0}", rowIndex);

            // Specify a column index.
            int columnIndex = 0;
            Console.WriteLine();
            Console.WriteLine("Column index: {0}", columnIndex);

            // Set the corresponding entry.
            matrix[rowIndex, columnIndex] = new Complex(20, -20);

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

            // Entries can also be accessed using a read-only wrapper of the matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entry:");
            Console.WriteLine(readOnlyMatrix[rowIndex, columnIndex]);
        }
    }
}