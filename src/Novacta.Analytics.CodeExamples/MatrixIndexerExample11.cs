﻿using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixIndexerExample11 : ICodeExample
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

            // Specify some row indexes.
            var rowIndexes = IndexCollection.Range(1, 3);
            Console.WriteLine();
            Console.WriteLine("Row indexes: {0}", rowIndexes);

            // Specify some column indexes.
            var columnIndexes = IndexCollection.Sequence(0, 2, 2);
            Console.WriteLine();
            Console.WriteLine("Column indexes: {0}", columnIndexes);

            // Specify the value matrix.
            var valueData = new double[6] {
              -20, -100,
              -30, -110,
              -40, -120
            };
            var value = DoubleMatrix.Dense(3, 2, valueData, StorageOrder.RowMajor);
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