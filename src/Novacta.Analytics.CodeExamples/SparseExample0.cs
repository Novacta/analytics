using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SparseExample0 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Set the initial capacity of the sparse instance.
            int capacity = 0;

            // Create the matrix. All entries will be equal to zero.
            var matrix = DoubleMatrix.Sparse(
                numberOfRows, numberOfColumns, capacity);
            Console.WriteLine("Initially, each entry is equal to zero.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            Console.WriteLine();

            // Set some entries as non-zero values.
            // If needed, the initial capacity is automatically
            // incremented.
            matrix[0, 0] = 1.0;
            matrix[2, 1] = -2.0;

            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

        }
    }
}