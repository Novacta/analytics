using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class VarianceExample1 : ICodeExample
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

            // Variance can be adjusted for bias.
            bool adjustForBias = false; 

            // Compute the data variance.
            var variance = Stat.Variance(matrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Data variance is:");
            Console.WriteLine(variance);

            // Variance is overloaded to accept data as a read-only matrix:
            // compute the variance using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var varianceOfReadOnlyData = Stat.Variance(readOnlyMatrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The variance is:");
            Console.WriteLine(varianceOfReadOnlyData);
        }
    }
}