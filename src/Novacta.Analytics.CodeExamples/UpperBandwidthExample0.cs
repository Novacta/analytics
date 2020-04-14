using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class UpperBandwidthExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[20] {
                0,  2,  3,  0,  0,
                0,  2,  2,  0,  1,
                0,  0,  3,  2,  9,
                4,  3,  1,  1,  4
            };
            var matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Compute the matrix upper bandwidth.
            var upperBandWidth = matrix.UpperBandwidth;

            Console.WriteLine();
            Console.WriteLine("has upper bandwidth equal to: {0}.", upperBandWidth);
            Console.WriteLine();

            // Create a new matrix.
            data = new double[20] {
                0,  0,  0,  0,  0,
                0,  2,  0,  0,  0,
                0,  0,  3,  0,  0,
                9,  0,  1,  1,  0
            };
            matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Compute the upper bandwidth using a read-only wrapper of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            upperBandWidth = readOnlyMatrix.UpperBandwidth;

            Console.WriteLine();
            Console.WriteLine("has upper bandwidth equal to: {0}.", upperBandWidth);
        }
    }
}