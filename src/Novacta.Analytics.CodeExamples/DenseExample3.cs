using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.CodeExamples
{
    public class DenseExample3 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Create the data.
            var data = new List<double>(6) {
               1,  2,  3,  4,  5,  6
            } as IEnumerable<double>;

            // Create the matrix. Data are assumed as ColMajor ordered.
            var matrix = DoubleMatrix.Dense(
                numberOfRows, numberOfColumns, data);
            Console.WriteLine("Assuming ColMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}