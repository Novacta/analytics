using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class VecExample1 : ICodeExample
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

            // Get the linear indexes of elements on
            // the main diagonal.
            var mainDiagonalIndexes = IndexCollection.Sequence(0, 4, 10);
            
            // Get the vectorization of the matrix main diagonal.
            var mainDiagonal = matrix.Vec(mainDiagonalIndexes);

            Console.WriteLine();
            Console.WriteLine("Vectorized main diagonal:");
            Console.WriteLine(mainDiagonal);

            // Entries can also be vectorized using a read-only wrapper of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Vectorized main diagonal of a read-only data matrix:");
            Console.WriteLine(readOnlyMatrix.Vec(mainDiagonalIndexes));
        }
    }
}