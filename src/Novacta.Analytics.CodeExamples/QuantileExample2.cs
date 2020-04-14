using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class QuantileExample2 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var matrix = DoubleMatrix.Dense(3, 5);
            for (int m = 0; m < matrix.Count; m++) {
                matrix[m] = 1 + m;
            }
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Create the probabilities.
            var probabilities = DoubleMatrix.Dense(1, 2);
            probabilities[0] = .33;
            probabilities[1] = .66;

            Console.WriteLine();
            Console.WriteLine("Probabilities are:");
            Console.WriteLine();
            Console.WriteLine(probabilities);

            // Compute the quantiles on rows.
            var quantilesOnRows = Stat.Quantile(matrix, probabilities, DataOperation.OnRows);

            Console.WriteLine();
            for (int i = 0; i < matrix.NumberOfRows; i++) {
                Console.WriteLine("Row {0} quantiles:", i);
                Console.WriteLine();
                Console.WriteLine(quantilesOnRows[i]);
            }

            // Quantile is overloaded to accept data as a read-only matrix:
            // compute the quantiles on rows using a read-only wrapper of the 
            // data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var quantilesOnReadOnlyRows = Stat.Quantile(readOnlyMatrix, probabilities, DataOperation.OnRows);

            Console.WriteLine();
            Console.WriteLine("Using read-only data.");
            Console.WriteLine();
            for (int i = 0; i < readOnlyMatrix.NumberOfRows; i++) {
                Console.WriteLine("Row {0} quantiles:", i);
                Console.WriteLine();
                Console.WriteLine(quantilesOnReadOnlyRows[i]);
            }
        }
    }
}
