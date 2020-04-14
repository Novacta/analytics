using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MeanExample0 : ICodeExample
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

            // Compute the mean on columns.
            var meanOnColumns = Stat.Mean(matrix, DataOperation.OnColumns);

            Console.WriteLine();
            Console.WriteLine("Mean on columns:");
            Console.WriteLine(meanOnColumns);

            // Mean is overloaded to accept data as a read-only matrix:
            // compute the mean on rows using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var meanOnRows = Stat.Mean(readOnlyMatrix, DataOperation.OnRows);

            Console.WriteLine();
            Console.WriteLine("Mean on rows:");
            Console.WriteLine(meanOnRows);
        }
    }
}