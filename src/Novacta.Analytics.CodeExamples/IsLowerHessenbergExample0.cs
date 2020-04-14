using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsLowerHessenbergExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
                0,  1,  1,  1,
                1,  1,  0,  0,
                1,  1,  1,  1,
                1,  1,  1,  1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being lower Hessenberg.
            var isLowerHessenberg = matrix.IsUpperHessenberg;

            Console.WriteLine();
            Console.WriteLine((isLowerHessenberg ? "is" : "is not") + " lower Hessenberg.");
            Console.WriteLine();

            // Create a new matrix.
            data = new double[16] {
                0,  1,  0,  0,
                0,  1,  0,  0,
                0,  1,  1,  1,
                1,  1,  1,  1
            };
            matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being lower Hessenberg using a read-only
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isLowerHessenberg = readOnlyMatrix.IsLowerHessenberg;

            Console.WriteLine();
            Console.WriteLine((isLowerHessenberg ? "is" : "is not") + " lower Hessenberg.");
        }
    }
}