using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SumOfSquaredDeviationsExample0 : ICodeExample
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

            // Compute the sum of squared deviations on columns.
            var sumOfSquaredDevsOnColumns = Stat.SumOfSquaredDeviations(matrix, DataOperation.OnColumns);

            Console.WriteLine();
            Console.WriteLine("SumOfSquaredDeviations on columns:");
            Console.WriteLine(sumOfSquaredDevsOnColumns);

            // SumOfSquaredDeviations is overloaded to accept data as a read-only matrix:
            // compute the sum of squared deviations on rows using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var sumOfSquaredDevsOnRows = Stat.SumOfSquaredDeviations(readOnlyMatrix, DataOperation.OnRows);

            Console.WriteLine();
            Console.WriteLine("SumOfSquaredDeviations on rows:");
            Console.WriteLine(sumOfSquaredDevsOnRows);
        }
    }
}