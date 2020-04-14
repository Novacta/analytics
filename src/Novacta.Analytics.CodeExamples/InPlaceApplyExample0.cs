using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class InPlaceApplyExample0 : ICodeExample
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

            // Square all matrix entries.
            matrix.InPlaceApply((x) => Math.Pow(x, 2.0));

            Console.WriteLine();
            Console.WriteLine("Matrix transformed by squaring its entries:");
            Console.WriteLine(matrix);
        }
    }
}