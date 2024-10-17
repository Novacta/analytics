using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class ComplexSingularValueDecompositionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[6] {
               new Complex( 1, 8), new Complex(-2, 0), 
               new Complex( 2, 2), new Complex( 1, 1),
               new Complex(-3, 9), new Complex(-1, 3)
            };
            var matrix = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Matrix to be decomposed:");
            Console.WriteLine(matrix);

            // Compute its SVD.
            var svd = SingularValueDecomposition.Decompose(
                matrix);

            Console.WriteLine();
            Console.WriteLine("Singular values:");
            Console.WriteLine(svd.Values);
            Console.WriteLine();
            Console.WriteLine("Left singular vectors:");
            Console.WriteLine(svd.LeftVectors);
            Console.WriteLine();
            Console.WriteLine("Conjugate transposed right singular vectors:");
            Console.WriteLine(svd.ConjugateTransposedRightVectors);
            Console.WriteLine();
            Console.WriteLine("Matrix reconstruction:");
            var v = svd.Values;
            var l = svd.LeftVectors;
            var rh = svd.ConjugateTransposedRightVectors;
            Console.WriteLine(l * v * rh);
        }
    }
}
