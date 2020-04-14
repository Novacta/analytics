using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SkewnessExample1 : ICodeExample
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

            // Skewness can be adjusted for bias.
            bool adjustForBias = false; 

            // Compute the data skewness.
            var skewness = Stat.Skewness(matrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Data skewness is:");
            Console.WriteLine(skewness);

            // Skewness is overloaded to accept data as a read-only matrix:
            // compute the skewness using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var skewnessOfReadOnlyData = Stat.Skewness(readOnlyMatrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The skewness is:");
            Console.WriteLine(skewnessOfReadOnlyData);
        }
    }
}