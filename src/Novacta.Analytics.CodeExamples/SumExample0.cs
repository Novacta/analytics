using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class SumExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                1, 2,
                2, 3,
                3, 4
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Compute the sum on columns.
            var sumOnColumns = Stat.Sum(matrix, DataOperation.OnColumns);

            Console.WriteLine();
            Console.WriteLine("Sum on columns:");
            Console.WriteLine(sumOnColumns);

            // Sum is overloaded to accept data as a read-only matrix:
            // compute the sum on rows using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var sumOnRows = Stat.Sum(readOnlyMatrix, DataOperation.OnRows);

            Console.WriteLine();
            Console.WriteLine("Sum on rows:");
            Console.WriteLine(sumOnRows);
        }
    }
}