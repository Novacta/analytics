using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDiagonalExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the main diagonal data.
            var data = new Complex[2] {
                new(1, -1),
                new(2, -2)
            };

            // Create a matrix storing the main diagonal data.
            // Note that such matrix can have any size: if it is not
            // a vector, its entries will be inserted in the main
            // diagonal of the diagonal matrix assuming ColMajor ordering.
            var mainDiagonal = ComplexMatrix.Dense(
                2, 1, data);

            Console.WriteLine("The matrix storing main diagonal data:");
            Console.WriteLine(mainDiagonal);

            Console.WriteLine();

            // Create the diagonal matrix.
            var diagonalMatrix = ComplexMatrix.Diagonal(
                mainDiagonal);
            Console.WriteLine("The diagonal matrix:");
            Console.WriteLine(diagonalMatrix);
        }
    }
}