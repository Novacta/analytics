using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class VecExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[9] {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            };
            var matrix = DoubleMatrix.Dense(3, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Get the vectorization of the data matrix.
            var vectorized = matrix.Vec();

            Console.WriteLine();
            Console.WriteLine("Vectorized data matrix:");
            Console.WriteLine(vectorized);

            // Entries can also be vectorized using a read-only wrapper of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Vectorized read-only data matrix :");
            Console.WriteLine(readOnlyMatrix.Vec());
        }
    }
}