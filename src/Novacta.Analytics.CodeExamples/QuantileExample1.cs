using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class QuantileExample1 : ICodeExample
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

            Console.WriteLine();
            Console.WriteLine("Probabilities are:");
            Console.WriteLine(probabilities);
           
            // Compute the quantiles of data columns.
            var quantilesOnColumns = Stat.Quantile(matrix, probabilities, DataOperation.OnColumns);

            Console.WriteLine();
            for (int j = 0; j < matrix.NumberOfColumns; j++) {
                Console.WriteLine("Column {0} quantiles:", j);
                Console.WriteLine();
                Console.WriteLine(quantilesOnColumns[j]);
            }

            // Quantile is overloaded to accept data as a read-only matrix:
            // compute the quantiles on columns using a read-only wrapper of the 
            // data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var percentilesOnReadOnlyColumns = Stat.Quantile(readOnlyMatrix, probabilities,
                DataOperation.OnColumns);

            Console.WriteLine();
            Console.WriteLine("Using read-only data.");
            Console.WriteLine();
            for (int j = 0; j < readOnlyMatrix.NumberOfColumns; j++) {
                Console.WriteLine("Column {0} quantiles:", j);
                Console.WriteLine();
                Console.WriteLine(percentilesOnReadOnlyColumns[j]);
            }
        }
    }
}
