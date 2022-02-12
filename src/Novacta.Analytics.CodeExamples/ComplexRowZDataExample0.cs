﻿using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexRowZDataExample0 : ICodeExample
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
            Console.WriteLine("Data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();

            // Get the collection of matrix rows.
            var rows = matrix.AsRowCollection();

            // Set the column corresponding to property ZData.
            rows.ZDataColumn = 0;

            // Get the ZData value for each row.
            foreach (var row in rows)
            {
                Console.WriteLine("ZData for Row {0}: {1}", row.Index, row.ZData);
            }

            // Set the ZData property: this code updates the data matrix, too.
            foreach (var row in rows)
            {
                row.ZData = new Complex(row.Index * 100.0, 0);
            }

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();
        }
    }
}