using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsLowerTriangularExample0 : ICodeExample
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

            // Test the matrix for being lower triangular.
            var isLowerTriangular = matrix.IsLowerTriangular;

            Console.WriteLine();
            Console.WriteLine((isLowerTriangular ? "is" : "is not") + " lower triangular.");
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

            // Test the matrix for being lower triangular using a read-only
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isLowerTriangular = readOnlyMatrix.IsLowerTriangular;

            Console.WriteLine();
            Console.WriteLine((isLowerTriangular ? "is" : "is not") + " lower triangular.");
        }
    }
}