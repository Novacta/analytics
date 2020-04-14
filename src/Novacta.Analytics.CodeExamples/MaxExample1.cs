using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MaxExample1 : ICodeExample
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

            // Return the largest entry of the specified data. 
            var dataMax = Stat.Max(matrix);

            Console.WriteLine();
            Console.WriteLine("Data maximum is {0} on linear position {1}",
                dataMax.Value, dataMax.Index);

            // Max is overloaded to accept data as a read-only matrix:
            // return the largest entry using a read-only wrapper of 
            // the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var readOnlyDataMax = Stat.Max(readOnlyMatrix);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Maximum is {0} on linear position {1}",
                readOnlyDataMax.Value, readOnlyDataMax.Index);
        }
    }
}