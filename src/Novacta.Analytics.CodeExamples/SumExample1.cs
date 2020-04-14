using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SumExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                1, 2,
                2, 3,
                3, 4
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Compute the data sum.
            var total = Stat.Sum(matrix);

            Console.WriteLine();
            Console.WriteLine("Data total is:");
            Console.WriteLine(total);

            // Sum is overloaded to accept data as a read-only matrix:
            // compute the sum using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var totalOfReadOnlyData = Stat.Sum(readOnlyMatrix);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The total is:");
            Console.WriteLine(totalOfReadOnlyData);
        }
    }
}