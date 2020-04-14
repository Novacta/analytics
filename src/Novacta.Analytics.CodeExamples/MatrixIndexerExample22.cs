using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixIndexerExample22 : ICodeExample
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

            // Specify all row indexes.
            var rowIndexes = ":";
            Console.WriteLine();
            Console.WriteLine("Row indexes: from 0 to {0}", matrix.NumberOfRows - 1);

            // Specify all column indexes.
            var columnIndexes = ":";
            Console.WriteLine();
            Console.WriteLine("Column indexes: from 0 to {0}", matrix.NumberOfColumns - 1);

            // Specify the value matrix.
            var valueData = new double[12] {
              -10, -50,  -90,
              -20, -60, -100,
              -30, -70, -110,
              -40, -80, -120
            };
            var value = DoubleMatrix.Dense(4, 3, valueData, StorageOrder.RowMajor);
            Console.WriteLine();
            Console.WriteLine("Value matrix:");
            Console.WriteLine(value);

            // Set the entries having the specified indexes to the value matrix.
            matrix[rowIndexes, columnIndexes] = value;

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

            // Entries can also be accessed using a read-only wrapper 
            // of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entries:");
            Console.WriteLine(readOnlyMatrix[rowIndexes, columnIndexes]);
        }
    }
}