using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDenseExample3 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Create the data.
            var data = new List<Complex>(6) {
                new Complex(1, -1),
                new Complex(2, -2),
                new Complex(3, -3),
                new Complex(4, -4),
                new Complex(5, -5),
                new Complex(6, -6)
            } as IEnumerable<Complex>;

            // Create the matrix. Data are assumed as ColMajor ordered.
            var matrix = ComplexMatrix.Dense(
                numberOfRows, numberOfColumns, data);
            Console.WriteLine("Assuming ColMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}