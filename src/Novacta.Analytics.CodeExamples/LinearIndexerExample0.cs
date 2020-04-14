using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class LinearIndexerExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                10, 20, 30,
                40, 50, 60
            };
            var matrix = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Specify a linear index.
            int linearIndex = 3;
            Console.WriteLine();
            Console.WriteLine("Linear index: {0}", linearIndex);

            // Set the corresponding entry.
            matrix[linearIndex] = -50.0;

            Console.WriteLine();
            Console.WriteLine("Updated data matrix:");
            Console.WriteLine(matrix);

            // Entries can also be accessed using a read-only wrapper of the matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();

            Console.WriteLine();
            Console.WriteLine("Updated matrix entry:");
            Console.WriteLine(readOnlyMatrix[linearIndex]);
        }
    }
}