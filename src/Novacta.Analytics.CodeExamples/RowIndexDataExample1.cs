using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class RowIndexDataExample1 : ICodeExample
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

            // Get the collection of the first two matrix rows.
            var rows = matrix.AsRowCollection(IndexCollection.Range(0,1));

            // Enumerate the specified matrix rows.
            foreach (var row in rows) {
                Console.WriteLine("Row {0}: ", row.Index);
                Console.WriteLine(row);
            }

            // Change the indexes of the rows in the collection,
            // so that they represent the last two matrix rows.
            rows[0].Index = 2;
            rows[1].Index = 3;

            // Enumerate the specified matrix rows.
            Console.WriteLine();
            foreach (var row in rows) {
                Console.WriteLine("Row {0}: ", row.Index);
                Console.WriteLine(row);
            }

        }
    }
}