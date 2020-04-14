using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class LowerBandwidthExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[20] {
                0,  1,  1,  1,  1,
                0,  1,  1,  0,  1,
                0,  0,  1,  1,  1,
                0,  0,  1,  1,  1
            };
            var matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Compute the matrix lower bandwidth.
            var lowerBandWidth = matrix.LowerBandwidth;

            Console.WriteLine();
            Console.WriteLine("has lower bandwidth equal to: {0}.", lowerBandWidth);
            Console.WriteLine();

            // Create a new matrix.
            data = new double[20] {
                0,  1,  1,  0,  0,
                0,  1,  0,  0,  0,
                0,  0,  0,  0,  1,
                0,  1,  1,  1,  0
            };
            matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Compute the lower bandwidth using a read-only wrapper of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            lowerBandWidth = readOnlyMatrix.LowerBandwidth;

            Console.WriteLine();
            Console.WriteLine("has lower bandwidth equal to: {0}.", lowerBandWidth);
        }
    }
}