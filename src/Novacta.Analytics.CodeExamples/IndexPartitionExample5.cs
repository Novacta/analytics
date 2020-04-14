using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IndexPartitionExample5 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[16] {
               -3,  3,  3, -1,
                0,  2, -2,  2,
                2,  1, -4, -5,  
               -8,  2,  7, -1
            };
            var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);

            // Create the collection of linear indexes corresponding
            // to entries on the matrix main diagonal.
            var diagonalIndexes = 
                IndexCollection.Sequence(0, 1 + matrix.NumberOfRows, matrix.Count);

            // Create a partitioner which returns true if
            // the absolute value in a entry having the specified linear
            // index is less than 3, otherwise false.
            bool partitioner(int linearIndex)
            {
                return Math.Abs(matrix[linearIndex]) < 3.0;
            }

            // Partition the diagonal linear indexes through the
            // specified partitioner.
            var partition = IndexPartition.Create(diagonalIndexes, partitioner);

            // Two parts are created, one for diagonal
            // entries less than 3 in absolute value, the other for 
            // entries not satisfying that condition.
            Console.WriteLine();
            foreach (var identifier in partition.Identifiers) {
                Console.WriteLine("Part identifier: {0}", identifier);
                Console.WriteLine("     indexes: {0}", partition[identifier]);
                Console.WriteLine();
            }
        }
    }
}