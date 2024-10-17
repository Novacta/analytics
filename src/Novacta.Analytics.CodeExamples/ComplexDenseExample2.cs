using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDenseExample2 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Create the data.
            var data = new Complex[6] {
                new(1, -1),
                new(5, -5),
                new(2, -2),
                new(6, -6),
                new(3, -3),
                new(7, -7)
            };

            // Create the matrix. Data are assumed as ColMajor ordered.
            var matrix = ComplexMatrix.Dense(
                numberOfRows, numberOfColumns, data);
            Console.WriteLine("Assuming ColMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}