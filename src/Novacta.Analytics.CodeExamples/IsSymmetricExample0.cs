using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsSymmetricExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
                0,  0,  0,  0,
                0,  1,  0, -2,
                1,  0,  1,  0,
                0, -2,  0,  1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being symmetric.
            var isSymmetric = matrix.IsSymmetric;

            Console.WriteLine();
            Console.WriteLine((isSymmetric ? "is" : "is not") + " symmetric.");
            Console.WriteLine();

            // Create a new matrix.
            data = new double[16] {
                0,  0,  1,  0,
                0,  1,  0, -2,
                1,  0,  1,  0,
                0, -2,  0,  1
            };
            matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being symmetric using a read-only
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isSymmetric = readOnlyMatrix.IsSymmetric;

            Console.WriteLine();
            Console.WriteLine((isSymmetric ? "is" : "is not") + " symmetric.");
        }
    }
}