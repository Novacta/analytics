using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SumOfSquaredDeviationsExample1 : ICodeExample
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

            // Compute the data sum of squared deviations.
            var sumOfSquaredDevs = Stat.SumOfSquaredDeviations(matrix);

            Console.WriteLine();
            Console.WriteLine("Data sum of squared deviations is:");
            Console.WriteLine(sumOfSquaredDevs);

            // SumOfSquaredDeviations is overloaded to accept data as a read-only matrix:
            // compute the sum of squared deviations using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var sumOfSquaredDevsOfReadOnlyData = Stat.SumOfSquaredDeviations(readOnlyMatrix);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The sum of squared deviations is:");
            Console.WriteLine(sumOfSquaredDevsOfReadOnlyData);
        }
    }
}