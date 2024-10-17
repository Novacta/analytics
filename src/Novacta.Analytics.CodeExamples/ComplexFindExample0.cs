using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexFindExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[6] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(2, -2)
            };
            var matrix = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Set the value to search for.
            Complex value = new(2, -2);

            // Find entries equal to value (2, -2).
            var indexes = matrix.Find(value);

            Console.WriteLine();
            Console.WriteLine("Linear indexes of entries equal to (2, -2) in data:");
            Console.WriteLine(indexes);

            // Find is available for read-only matrices:
            // find entries equal to (2, -2) using a read-only wrapper of the data matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();
            indexes = readOnlyMatrix.Find(value);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Linear indexes of entries equal to (2, -2):");
            Console.WriteLine(indexes);
        }
    }
}