using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexRowsEnumeratorExample1 : ICodeExample
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

            // Specify the rows to enumerate.
            var rowIndexes = IndexCollection.FromArray(new int[6] { 0, 0, 1, 2, 3, 2 });

            // Get the collection of the specified matrix rows.
            var rows = matrix.AsRowCollection(rowIndexes);

            // Enumerate the specified matrix rows.
            foreach (var row in rows)
            {
                Console.WriteLine("Row {0}: ", row.Index);
                Console.WriteLine(row);
            }
        }
    }
}