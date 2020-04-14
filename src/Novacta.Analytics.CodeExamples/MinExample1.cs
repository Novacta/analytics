using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MinExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
               -1, -2,
                2,  3,
                3, -4
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Return the smallest entry of the specified data. 
            var dataMin = Stat.Min(matrix);

            Console.WriteLine();
            Console.WriteLine("Data minimum is {0} on linear position {1}",
                dataMin.Value, dataMin.Index);

            // Min is overloaded to accept data as a read-only matrix:
            // return the smallest entry using a read-only wrapper of 
            // the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var readOnlyDataMin = Stat.Min(readOnlyMatrix);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Minimum is {0} on linear position {1}",
                readOnlyDataMin.Value, readOnlyDataMin.Index);
        }
    }
}