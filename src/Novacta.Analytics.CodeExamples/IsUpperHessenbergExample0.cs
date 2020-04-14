using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsUpperHessenbergExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
                0,  1,  1,  1,
                0,  1,  0,  0,
                1,  0,  1,  1,
                0,  0,  1,  1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being upper Hessenberg.
            var isUpperHessenberg = matrix.IsUpperHessenberg;

            Console.WriteLine();
            Console.WriteLine((isUpperHessenberg ? "is" : "is not") + " upper Hessenberg.");
            Console.WriteLine();

            // Create a new matrix.
            data = new double[16] {
                0,  1,  0,  1,
                0,  1,  0,  0,
                0,  1,  1,  1,
                0,  0,  0,  1
            };
            matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being upper Hessenberg using a read-only
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isUpperHessenberg = readOnlyMatrix.IsUpperHessenberg;

            Console.WriteLine();
            Console.WriteLine((isUpperHessenberg ? "is" : "is not") + " upper Hessenberg.");
        }
    }
}