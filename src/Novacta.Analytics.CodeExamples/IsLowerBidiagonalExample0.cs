using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsLowerBidiagonalExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
                0,  1,  1,  1,
                0,  1,  1,  0,
                1,  0,  1,  1,
                0,  0,  1,  1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being lower bidiagonal.
            var isLowerBidiagonal = matrix.IsLowerBidiagonal;

            Console.WriteLine();
            Console.WriteLine((isLowerBidiagonal ? "is" :"is not") + " lower bidiagonal.");
            Console.WriteLine();

            // Create a new matrix.
            data = new double[16] {
                0,  0,  0,  0,
                1,  1,  0,  0,
                0,  0,  1,  0,
                0,  0,  1,  1
            };
            matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being lower bidiagonal using a read-only 
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isLowerBidiagonal = readOnlyMatrix.IsLowerBidiagonal;

            Console.WriteLine();
            Console.WriteLine((isLowerBidiagonal ? "is" : "is not") + " lower bidiagonal.");
        }
    }
}