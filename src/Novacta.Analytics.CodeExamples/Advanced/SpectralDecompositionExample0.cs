using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class SpectralDecompositionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix containing the
            // lower triangular part of the symmetric matrix
            // to be decomposed.
            var data = new double[9] {
                2,
                2,
                0,
                0,
                0,
                0,
                1,
                0,
                5
            };
            var matrix = DoubleMatrix.Dense(3, 3, data);

            // Set the relevant triangular part.
            bool lowerTriangularPart = true;

            Console.WriteLine("Matrix to be decomposed:");
            Console.WriteLine(DoubleMatrix.Dense(3, 3, [
                2,
                2,
                0,
                2,
                0,
                0,
                0,
                0,
                5
            ]));

            // Compute the Spectral decomposition.
            var eigenvalues = SpectralDecomposition.Decompose(
                matrix,
                lowerTriangularPart,
                out DoubleMatrix eigenvectors);

            Console.WriteLine();
            Console.WriteLine("Eigenvalues:");
            Console.WriteLine(eigenvalues);
            Console.WriteLine();
            Console.WriteLine("Eigenvectors:");
            Console.WriteLine(eigenvectors);
            Console.WriteLine();
            Console.WriteLine("Matrix reconstruction:");
            var l = eigenvalues;
            var v = eigenvectors;
            Console.WriteLine(v * l * v.Transpose());
        }
    }
}
