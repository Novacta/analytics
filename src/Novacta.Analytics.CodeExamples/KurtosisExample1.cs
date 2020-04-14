using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class KurtosisExample1 : ICodeExample
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

            // Kurtosis can be adjusted for bias.
            bool adjustForBias = false;

            // Compute the data kurtosis.
            var kurtosis = Stat.Kurtosis(matrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Data kurtosis is:");
            Console.WriteLine(kurtosis);

            // Kurtosis is overloaded to accept data as a read-only matrix:
            // compute the kurtosis using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var kurtosisOfReadOnlyData = Stat.Kurtosis(readOnlyMatrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The kurtosis is:");
            Console.WriteLine(kurtosisOfReadOnlyData);
        }
    }
}
