using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsDiagonalExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
                0,  0,  0,  0,
                0,  1,  0,  0,
                0,  0,  1,  0,
                0,  0,  0,  1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being diagonal.
            var isDiagonal = matrix.IsDiagonal;

            Console.WriteLine();
            Console.WriteLine((isDiagonal ? "is" :"is not") + " diagonal.");
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

            // Test the matrix for being diagonal using a read-only
            // wrapper of a matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isDiagonal = readOnlyMatrix.IsDiagonal;

            Console.WriteLine();
            Console.WriteLine((isDiagonal ? "is" : "is not") + " diagonal.");
        }
    }
}