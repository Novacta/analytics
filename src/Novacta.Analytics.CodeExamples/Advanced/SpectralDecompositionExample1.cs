using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class SpectralDecompositionExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix containing the
            // lower triangular part of the Hermitian matrix
            // whose eigenvalues must be computed.
            var matrix = ComplexMatrix.Dense(2, 2, [ 
                new(1, 0), new(-3, -2),
                new(5, 4), new(-6,  0)
            ], StorageOrder.RowMajor);

            // Set the relevant triangular part.
            bool lowerTriangularPart = false;

            // Compute the eigenvalues.
            var eigenvalues = SpectralDecomposition.GetEigenvalues(
                matrix,
                lowerTriangularPart);

            Console.WriteLine("Matrix whose eigenvalues must be computed:");
            Console.WriteLine(ComplexMatrix.Dense(2, 2, [
                new( 1, 0), new(-3, -2),
                new(-3, 2), new(-6,  0)
            ], StorageOrder.RowMajor));


            Console.WriteLine("Matrix eigenvalues:");
            Console.WriteLine(eigenvalues);
        }
    }
}
