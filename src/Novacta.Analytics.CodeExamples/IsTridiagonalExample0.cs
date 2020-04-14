using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsTridiagonalExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
                0,  1,  0,  0,
                0,  1,  0,  0,
                1,  0,  1,  1,
                0,  0,  1,  1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being tridiagonal.
            var isTridiagonal = matrix.IsTridiagonal;

            Console.WriteLine();
            Console.WriteLine((isTridiagonal ? "is" :"is not") + " tridiagonal.");
            Console.WriteLine();

            // Create a new matrix.
            data = new double[16] {
                0,  1,  0,  0,
                0,  1,  0,  0,
                0,  0,  1,  1,
                0,  0,  0,  1
            };
            matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // test the matrix for being tridiagonal using a read-only
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isTridiagonal = readOnlyMatrix.IsTridiagonal;

            Console.WriteLine();
            Console.WriteLine((isTridiagonal ? "is" : "is not") + " tridiagonal.");
        }
    }
}