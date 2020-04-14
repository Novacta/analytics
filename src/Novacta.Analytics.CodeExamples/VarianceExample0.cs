using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class VarianceExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[20] {
                1, 2, -3,  6, -2,
                2, 2,  2,  0,  7,
               -3, 2,  3,  2,  9,
                5, 2,  7, -1, -4
            };
            var matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Variance can be adjusted for bias.
            bool adjustForBias = true; 

            // Compute the variance on columns.
            var varianceOnColumns = Stat.Variance(matrix, adjustForBias, DataOperation.OnColumns);

            Console.WriteLine();
            Console.WriteLine("Variance on columns:");
            Console.WriteLine(varianceOnColumns);

            // Variance is overloaded to accept data as a read-only matrix:
            // compute the variance on rows using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var varianceOnRows = Stat.Variance(readOnlyMatrix, adjustForBias, DataOperation.OnRows);

            Console.WriteLine();
            Console.WriteLine("Variance on rows:");
            Console.WriteLine(varianceOnRows);
        }
    }
}