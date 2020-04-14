using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IndexPartitionExample2 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[8] {
                0, 1,-2,-3,
                0,-1, 2, 3
            };
            var matrix = DoubleMatrix.Dense(2, 4, data, StorageOrder.RowMajor);

            // Check the sign of its entries.
            var signs = DoubleMatrix.Dense(matrix.NumberOfRows, matrix.NumberOfColumns);
            for (int i = 0; i < matrix.Count; i++) {
                signs[i] = Math.Sign(matrix[i]);
            }

            // Partition the matrix linear indexes by the sign of each entry.
            var partition = IndexPartition.Create(signs);

            // The partition contains three parts, the zero part, identified by 0,
            // the negative part (identified by -1), and the positive one 
            // (identified by 1).
            Console.WriteLine();
            foreach (var identifier in partition.Identifiers) {
                Console.WriteLine("Part identifier: {0}", identifier);
                Console.WriteLine("     indexes: {0}", partition[identifier]);
                Console.WriteLine();
            }
        }
    }
}