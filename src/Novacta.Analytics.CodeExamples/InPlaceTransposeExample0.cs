using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class InPlaceTransposeExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[20] {
                1, 8, -3,  6, -2,
                2, 2,  2,  0,  7,
               -3, 9,  3,  2,  9,
                5, 2, -5, -1, -4
            };
            var matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Transpose the matrix.
            matrix.InPlaceTranspose();

            Console.WriteLine();
            Console.WriteLine("Transposed data matrix:");
            Console.WriteLine(matrix);
        }
    }
}