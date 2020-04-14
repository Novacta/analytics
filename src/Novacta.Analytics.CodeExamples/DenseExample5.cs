using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class DenseExample5 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Create the matrix. All entries will be equal to zero.
            var matrix = DoubleMatrix.Dense(
                numberOfRows, numberOfColumns);
            Console.WriteLine("Each entry is equal to zero.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}