using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexVecExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[8] {
                new Complex(1, -1), new Complex(5, -5),
                new Complex(2, -2), new Complex(6, -6),
                new Complex(3, -3), new Complex(7, -7),
                new Complex(4, -4), new Complex(8, -8)
            };
            var matrix = ComplexMatrix.Dense(4, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Get the vectorization of the data matrix.
            var vectorized = matrix.Vec();

            Console.WriteLine();
            Console.WriteLine("Vectorized data matrix:");
            Console.WriteLine(vectorized);

            // Entries can also be vectorized using a read-only wrapper of the matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Vectorized read-only data matrix :");
            Console.WriteLine(readOnlyMatrix.Vec());
        }
    }
}