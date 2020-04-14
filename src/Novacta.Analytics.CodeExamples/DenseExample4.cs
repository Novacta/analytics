using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class DenseExample4 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Set the value for each entry.
            double data = -1.0;

            // Create the matrix. All entries will be equal to the
            // same value.
            var matrix = DoubleMatrix.Dense(
                numberOfRows, numberOfColumns, data);
            Console.WriteLine("Each entry is equal to the same value.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}