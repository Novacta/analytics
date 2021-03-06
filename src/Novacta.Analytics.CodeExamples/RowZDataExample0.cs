﻿using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class RowZDataExample0 : ICodeExample
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
            Console.WriteLine("Data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();

            // Get the collection of matrix rows.
            var rows = matrix.AsRowCollection();

            // Set the column corresponding to property ZData.
            rows.ZDataColumn = 0;

            // Get the ZData value for each row.
            foreach (var row in rows) {
                Console.WriteLine("ZData for Row {0}: {1}", row.Index, row.ZData);
            }

            // Set the ZData property: this code updates the data matrix, too.
            foreach (var row in rows) {
                row.ZData = row.Index * 100.0;
            }

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();
        }
    }
}