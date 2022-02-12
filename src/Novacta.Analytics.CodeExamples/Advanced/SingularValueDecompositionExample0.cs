using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class SingularValueDecompositionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                1, 8,
                2, 2,
               -3, 9
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Matrix to be decomposed:");
            Console.WriteLine(matrix);

            // Compute its SVD.
            var singularValues = SingularValueDecomposition.Decompose(
                matrix,
                out DoubleMatrix leftSingularVectors,
                out DoubleMatrix conjugateTransposedRightSingularVectors);

            Console.WriteLine(); 
            Console.WriteLine("Singular values:");
            Console.WriteLine(singularValues);
            Console.WriteLine();
            Console.WriteLine("Left singular vectors:");
            Console.WriteLine(leftSingularVectors);
            Console.WriteLine();
            Console.WriteLine("Conjugate transposed right singular vectors:");
            Console.WriteLine(conjugateTransposedRightSingularVectors);
            Console.WriteLine();
            Console.WriteLine("Matrix reconstruction:");
            var v = singularValues;
            var l = leftSingularVectors;
            var rh = conjugateTransposedRightSingularVectors;
            Console.WriteLine(l * v * rh);
        }
    }
}
