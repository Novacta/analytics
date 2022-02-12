using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexMatrixIndexerExample22 : ICodeExample
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

            // Specify all row indexes.
            var rowIndexes = ":";
            Console.WriteLine();
            Console.WriteLine("Row indexes: from 0 to {0}", matrix.NumberOfRows - 1);

            // Specify all column indexes.
            var columnIndexes = ":";
            Console.WriteLine();
            Console.WriteLine("Column indexes: from 0 to {0}", matrix.NumberOfColumns - 1);

            // Specify the value matrix.
            var valueData = new Complex[8] {
                new Complex(10, -10), new Complex(50, -50),
                new Complex(20, -20), new Complex(60, -60),
                new Complex(30, -30), new Complex(70, -70),
                new Complex(40, -40), new Complex(80, -80)
            };
            var value = ComplexMatrix.Dense(4, 2, valueData, StorageOrder.RowMajor);
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
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entries:");
            Console.WriteLine(readOnlyMatrix[rowIndexes, columnIndexes]);
        }
    }
}