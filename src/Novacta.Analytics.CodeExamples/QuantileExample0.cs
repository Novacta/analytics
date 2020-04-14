using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class QuantileExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var matrix = DoubleMatrix.Dense(25, 4);
            for (int m = 0; m < matrix.Count; m++) {
                matrix[m] = 1 + m;
            }
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Create the probabilities.
            var probabilities = DoubleMatrix.Dense(2, 2);
            probabilities[0, 0] = .005;
            probabilities[1, 0] = .50;
            probabilities[0, 1] = .75;
            probabilities[1, 1] = .999;

            // Compute the data quantiles.
            var quantiles = Stat.Quantile(matrix, probabilities);

            Console.WriteLine();
            Console.WriteLine("Probabilities are:");
            Console.WriteLine(probabilities);

            Console.WriteLine();
            Console.WriteLine("Corresponding data quantiles are:");
            Console.WriteLine(quantiles);

            // Quantile is overloaded to accept data as a read-only matrix:
            // compute the quantiles using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var quantilesOfReadOnlyData = Stat.Quantile(readOnlyMatrix, probabilities);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The quantiles are:");
            Console.WriteLine(quantilesOfReadOnlyData);
        }
    }
}
