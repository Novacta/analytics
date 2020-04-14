using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class FindWhileExample0 : ICodeExample
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

            // Match negative entries.
            static bool match(double value) { return value < 0.0; }

            // Find the linear indexes of negative entries in data.
            var indexes = matrix.FindWhile(match);

            Console.WriteLine();
            Console.WriteLine("Linear indexes of negative entries in data:");
            Console.WriteLine(indexes);

            // FindWhile is available for read-only matrices:
            // find negative entries using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            indexes = readOnlyMatrix.FindWhile(match);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Linear indexes of negative entries:");
            Console.WriteLine(indexes);
        }
    }
}
