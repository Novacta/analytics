using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class DiagonalExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the main diagonal data.
            var data = new double[6] {
               1,  2,  3,  4,  5,  6
            };

            // Create a matrix storing the main diagonal data.
            // Note that such matrix can have any size: if it is not
            // a vector, its entries will be inserted in the main
            // diagonal of the diagonal matrix assuming ColMajor ordering.
            var mainDiagonal = DoubleMatrix.Dense(
                2, 3, data);

            Console.WriteLine("The matrix storing main diagonal data:");
            Console.WriteLine(mainDiagonal);

            Console.WriteLine();

            // Create the diagonal matrix.
            var diagonalMatrix = DoubleMatrix.Diagonal(
                mainDiagonal);
            Console.WriteLine("The diagonal matrix:");
            Console.WriteLine(diagonalMatrix);
        }
    }
}