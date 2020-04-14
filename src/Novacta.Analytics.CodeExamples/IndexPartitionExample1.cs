using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IndexPartitionExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[18] {
                0,0,1,
                0,0,1,
                0,1,0,
                0,1,0,
                1,0,0,
                1,0,0
            };
            var matrix = DoubleMatrix.Dense(6, 3, data, StorageOrder.RowMajor);

            // Partition the matrix row indexes by the contents of each row:
            // a part is created for each distinct row.
            var partition = IndexPartition.Create(matrix.AsRowCollection());

            // Each part is identified by its corresponding row and contains
            // the indexes of the rows which are equal to the identifier.
            Console.WriteLine();
            foreach (var identifier in partition.Identifiers) {
                Console.WriteLine("Part identifier: {0}", identifier);
                Console.WriteLine("     indexes: {0}", partition[identifier]);
                Console.WriteLine();
            }
        }
    }
}