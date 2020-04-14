using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MeanExample1 : ICodeExample
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

            // Compute the data mean.
            var mean = Stat.Mean(matrix);

            Console.WriteLine();
            Console.WriteLine("Data mean is:");
            Console.WriteLine(mean);

            // Mean is overloaded to accept data as a read-only matrix:
            // compute the mean using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var meanOfReadOnlyData = Stat.Mean(readOnlyMatrix);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The mean is:");
            Console.WriteLine(meanOfReadOnlyData);
        }
    }
}