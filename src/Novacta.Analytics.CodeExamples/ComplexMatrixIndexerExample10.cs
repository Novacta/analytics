﻿using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexMatrixIndexerExample10 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[8] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7),
                new(4, -4), new(8, -8)
            };
            var matrix = ComplexMatrix.Dense(4, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Specify some row indexes.
            var rowIndexes = IndexCollection.Range(1, 3);
            Console.WriteLine();
            Console.WriteLine("Row indexes: {0}", rowIndexes);

            // Specify a column index.
            int columnIndex = 0;
            Console.WriteLine();
            Console.WriteLine("Column index: {0}", columnIndex);

            // Specify the value matrix.
            var valueData = new Complex[3] {
                new(20, -20),
                new(30, -30),
                new(40, -40)
            };
            var value = ComplexMatrix.Dense(3, 1, valueData, StorageOrder.RowMajor);
            Console.WriteLine();
            Console.WriteLine("Value matrix:");
            Console.WriteLine(value);

            // Set the entries having the specified indexes to the value matrix.
            matrix[rowIndexes, columnIndex] = value;

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

            // Entries can also be accessed using a read-only wrapper 
            // of the matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entries:");
            Console.WriteLine(readOnlyMatrix[rowIndexes, columnIndex]);
        }
    }
}