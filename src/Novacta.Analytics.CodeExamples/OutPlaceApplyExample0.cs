using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class OutPlaceApplyExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[20] {
                1, 8, -3,  6, -2,
                2, 2,  2,  0,  7,
               -3, 9,  3,  2,  9,
                5, 2, -5, -1, -4
            };
            var matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Return a matrix obtained by adding 1 to each entry
            // of the initial matrix.
            var plusOneMatrix = matrix.Apply((x) => x + 1);

            Console.WriteLine();
            Console.WriteLine("Matrix obtained by adding 1 to each entry:");
            Console.WriteLine(plusOneMatrix);

            // Add one using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var plusOneReadOnlyMatrix = readOnlyMatrix.Apply((x) => x + 1);

            Console.WriteLine();
            Console.WriteLine("Adding 1 to each entry of a read only matrix:");
            Console.WriteLine(plusOneReadOnlyMatrix);
        }
    }
}