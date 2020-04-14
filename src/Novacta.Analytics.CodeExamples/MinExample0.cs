using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MinExample0 : ICodeExample
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

            // Return the smallest entries in columns of the specified data. 
            var columnMins = Stat.Min(matrix, DataOperation.OnColumns);

            Console.WriteLine();
            for (int j = 0; j < matrix.NumberOfColumns; j++) {
                Console.WriteLine("Column {0}: minimum is {1} on row {2}",
                    j, columnMins[j].Value, columnMins[j].Index);
            }

            // Min is overloaded to accept data as a read-only matrix:
            // return the smallest entries in rows using a read-only wrapper of 
            // the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var rowMins = Stat.Min(readOnlyMatrix, DataOperation.OnRows);

            Console.WriteLine();
            for (int i = 0; i < readOnlyMatrix.NumberOfRows; i++) {
                Console.WriteLine("Row {0}: minimum is {1} on column {2}",
                    i, rowMins[i].Value, rowMins[i].Index);
            }
        }
    }
}