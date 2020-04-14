using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class RowsEnumeratorExample1 : ICodeExample
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

            // Specify the rows to enumerate.
            var rowIndexes = IndexCollection.FromArray(new int[6] { 0, 0, 1, 2, 3, 2 });
            
            // Get the collection of the specified matrix rows.
            var rows = matrix.AsRowCollection(rowIndexes);

            // Enumerate the specified matrix rows.
            foreach (var row in rows) {
                Console.WriteLine("Row {0}: ", row.Index);
                Console.WriteLine(row);
            }
        }
    }
}