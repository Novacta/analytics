using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IsSkewSymmetricExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[9] {
                0,  2, -3, 
               -2,  1,  0, 
                0,  0,  1
            };
            var matrix = DoubleMatrix.Dense(3, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being skew-symmetric.
            var isSkewSymmetric = matrix.IsSkewSymmetric;

            Console.WriteLine();
            Console.WriteLine((isSkewSymmetric ? "is" : "is not") + " skew-symmetric.");
            Console.WriteLine();

            // Create a new matrix.
            data = new double[9] {
                0,  2, -3,
               -2,  0,  0,
                3,  0,  0
            };
            matrix = DoubleMatrix.Dense(3, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("The matrix");
            Console.WriteLine(matrix);

            // Test the matrix for being skew-symmetric using a read-only
            // wrapper.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            isSkewSymmetric = readOnlyMatrix.IsSkewSymmetric;

            Console.WriteLine();
            Console.WriteLine((isSkewSymmetric ? "is" : "is not") + " skew-symmetric.");
        }
    }
}