using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexRowIndexDataExample1 : ICodeExample
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
            Console.WriteLine("Data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();

            // Get the collection of the first two matrix rows.
            var rows = matrix.AsRowCollection(IndexCollection.Range(0, 1));

            // Enumerate the specified matrix rows.
            foreach (var row in rows)
            {
                Console.WriteLine("Row {0}: ", row.Index);
                Console.WriteLine(row);
            }

            // Change the indexes of the rows in the collection,
            // so that they represent the last two matrix rows.
            rows[0].Index = 2;
            rows[1].Index = 3;

            // Enumerate the specified matrix rows.
            Console.WriteLine();
            foreach (var row in rows)
            {
                Console.WriteLine("Row {0}: ", row.Index);
                Console.WriteLine(row);
            }

        }
    }
}