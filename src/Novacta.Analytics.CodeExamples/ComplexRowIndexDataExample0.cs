using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexRowIndexDataExample0 : ICodeExample
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

            // Set the column corresponding to property XData.
            rows.XDataColumn = 1;

            // Get the a row in the collection
            var firstRow = rows[1];

            // Get the XData value of the row.
            Console.WriteLine("XData for Row {0}: {1}", firstRow.Index, firstRow.XData);
            Console.WriteLine();

            // Change the index of the row.
            firstRow.Index = 3;

            // Here the row represents the matrix row having index 3.

            // Get the XData value of the first row.
            Console.WriteLine("XData for Row {0}: {1}", firstRow.Index, firstRow.XData);
            Console.WriteLine();
        }
    }
}