using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SortIndexExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                1,  2,
                2, -3,
                3,  4,
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Sort the data in ascending order. Arrange their linear positions
            // accordingly.
            var sortIndexResults = Stat.SortIndex(matrix, SortDirection.Ascending);

            Console.WriteLine();
            Console.WriteLine("Data sorted in ascending order:");
            Console.WriteLine(sortIndexResults.SortedData);

            Console.WriteLine();
            Console.WriteLine("Data linear positions arranged accordingly:");
            Console.WriteLine(sortIndexResults.SortedIndexes);

            // SortIndex is overloaded to accept data as a read-only matrix:
            // sort in descending order using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var sortReadOnlyDataResults = Stat.SortIndex(readOnlyMatrix, SortDirection.Descending);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Data descending sort:");
            Console.WriteLine(sortReadOnlyDataResults.SortedData);
            
            Console.WriteLine();
            Console.WriteLine("Using read-only data. Data linear positions arrangement:");
            Console.WriteLine(sortReadOnlyDataResults.SortedIndexes);
        }
    }
}
