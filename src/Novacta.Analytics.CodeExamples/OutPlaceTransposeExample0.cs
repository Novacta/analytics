using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class OutPlaceTransposeExample0 : ICodeExample
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
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Return its transpose.
            var transposedMatrix = matrix.Transpose();

            Console.WriteLine();
            Console.WriteLine("Matrix transpose:");
            Console.WriteLine(transposedMatrix);

            // Compute the transpose using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var transposedReadOnlyMatrix = readOnlyMatrix.Transpose();

            Console.WriteLine();
            Console.WriteLine("Read only matrix transpose:");
            Console.WriteLine(transposedReadOnlyMatrix);
        }
    }
}