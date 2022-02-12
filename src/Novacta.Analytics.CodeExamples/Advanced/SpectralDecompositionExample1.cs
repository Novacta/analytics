using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class SpectralDecompositionExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix containing the
            // lower triangular part of the Hermitian matrix
            // whose eigenvalues must be computed.
            var matrix = ComplexMatrix.Dense(2, 2, new Complex[4] { 
                new Complex(1, 0), new Complex(-3, -2),
                new Complex(5, 4), new Complex(-6,  0)
            }, StorageOrder.RowMajor);

            // Set the relevant triangular part.
            bool lowerTriangularPart = false;

            // Compute the eigenvalues.
            var eigenvalues = SpectralDecomposition.GetEigenvalues(
                matrix,
                lowerTriangularPart);

            Console.WriteLine("Matrix whose eigenvalues must be computed:");
            Console.WriteLine(ComplexMatrix.Dense(2, 2, new Complex[4] {
                new Complex( 1, 0), new Complex(-3, -2),
                new Complex(-3, 2), new Complex(-6,  0)
            }, StorageOrder.RowMajor));


            Console.WriteLine("Matrix eigenvalues:");
            Console.WriteLine(eigenvalues);
        }
    }
}
