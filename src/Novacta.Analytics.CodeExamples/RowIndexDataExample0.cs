using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class RowIndexDataExample0 : ICodeExample
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

            // Set the column corresponding to property XData.
            rows.XDataColumn = 2;

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