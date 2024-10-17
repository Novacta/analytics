using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexOutPlaceApplyExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[6] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7)
            };
            var matrix = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Return a matrix obtained by adding 1 to each entry
            // of the initial matrix.
            var plusOneMatrix = matrix.Apply((x) => x + 1);

            Console.WriteLine();
            Console.WriteLine("Matrix obtained by adding 1 to each entry:");
            Console.WriteLine(plusOneMatrix);

            // Add one using a read-only wrapper of the data matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();
            var plusOneReadOnlyMatrix = readOnlyMatrix.Apply((x) => x + 1);

            Console.WriteLine();
            Console.WriteLine("Adding 1 to each entry of a read only matrix:");
            Console.WriteLine(plusOneReadOnlyMatrix);
        }
    }
}