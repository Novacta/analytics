﻿using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixIndexerExample12 : ICodeExample
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

            // Specify all column indexes.
            var columnIndexes = ":";
            Console.WriteLine();
            Console.WriteLine("Column indexes: from 0 to {0}", matrix.NumberOfColumns - 1);

            // Specify the value matrix.
            var valueData = new double[9] {
              -20, -60, -100,
              -30, -70, -110,
              -40, -80, -120
            };
            var value = DoubleMatrix.Dense(3, 3, valueData, StorageOrder.RowMajor);
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