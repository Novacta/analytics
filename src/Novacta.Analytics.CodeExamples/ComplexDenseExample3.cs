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
                new(1, -1),
                new(2, -2),
                new(3, -3),
                new(4, -4),
                new(5, -5),
                new(6, -6)
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