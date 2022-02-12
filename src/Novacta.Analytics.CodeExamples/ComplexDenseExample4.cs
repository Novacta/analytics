using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDenseExample4 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Set the value for each entry.
            Complex data = new(1, -1);

            // Create the matrix. All entries will be equal to the
            // same value.
            var matrix = ComplexMatrix.Dense(
                numberOfRows, numberOfColumns, data);
            Console.WriteLine("Each entry is equal to the same value.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}