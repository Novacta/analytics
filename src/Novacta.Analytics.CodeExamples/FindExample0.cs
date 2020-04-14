using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class FindExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
               -1,  2,
                2, -3,
                3,  4,
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Set the value to search for.
            double value = 2.0;

            // Find entries equal to value 2.0.
            var indexes = matrix.Find(value);

            Console.WriteLine();
            Console.WriteLine("Linear indexes of entries equal to 2.0 in data:");
            Console.WriteLine(indexes);

            // Find is available for read-only matrices:
            // find entries equal to 2.0 using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            indexes = readOnlyMatrix.Find(value);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Linear indexes of entries equal to 2.0:");
            Console.WriteLine(indexes);
        }
    }
}