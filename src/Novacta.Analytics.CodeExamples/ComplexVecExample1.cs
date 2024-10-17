using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexVecExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[4] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6)
            };
            var matrix = ComplexMatrix.Dense(2, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Get the linear indexes of elements on
            // the main diagonal.
            var mainDiagonalIndexes = IndexCollection.Sequence(0, 3, 3);

            // Get the vectorization of the matrix main diagonal.
            var mainDiagonal = matrix.Vec(mainDiagonalIndexes);

            Console.WriteLine();
            Console.WriteLine("Vectorized main diagonal:");
            Console.WriteLine(mainDiagonal);

            // Entries can also be vectorized using a read-only wrapper of the matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Vectorized main diagonal of a read-only data matrix:");
            Console.WriteLine(readOnlyMatrix.Vec(mainDiagonalIndexes));
        }
    }
}