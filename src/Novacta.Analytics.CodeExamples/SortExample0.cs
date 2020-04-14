using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SortExample0 : ICodeExample
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

            // Sort the data in ascending order.
            var sortedData = Stat.Sort(matrix, SortDirection.Ascending);

            Console.WriteLine();
            Console.WriteLine("Data sorted in ascending order:");
            Console.WriteLine(sortedData);

            // Sort is overloaded to accept data as a read-only matrix:
            // sort in descending order using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var sortedReadOnlyData = Stat.Sort(readOnlyMatrix, SortDirection.Descending);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Descending sort:");
            Console.WriteLine(sortedReadOnlyData);
        }
    }
}
