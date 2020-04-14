using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class CorrelationExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[20] {
                1, 2, -3,  6, -2,
                2, 2,  2,  0,  7,
               -3, 2,  3,  2,  9,
                5, 2,  7, -1, -4
            };
            var matrix = DoubleMatrix.Dense(4, 5, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Compute the correlations among columns.
            var correlationOnColumns = Stat.Correlation(matrix, DataOperation.OnColumns);

            Console.WriteLine();
            Console.WriteLine("Correlations among columns:");
            Console.WriteLine(correlationOnColumns);

            // Correlation is overloaded to accept data as a read-only matrix:
            // compute the correlations among rows using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var correlationOnRows = Stat.Correlation(readOnlyMatrix,  DataOperation.OnRows);

            Console.WriteLine();
            Console.WriteLine("Correlations among rows:");
            Console.WriteLine(correlationOnRows);
        }
    }
}