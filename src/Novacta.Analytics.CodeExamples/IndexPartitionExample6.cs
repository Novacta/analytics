using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IndexPartitionExample6 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                1,3,
                0,2,
                2,1
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);

            // Partition the matrix linear indexes by the content of 
            // matrix entries: a part is created for each distinct matrix value.
            var partition = IndexPartition.Create(matrix);

            // Each part is identified by its corresponding value and contains
            // the linear indexes of the entries in which the identifier
            // is positioned.
            Console.WriteLine();
            foreach (var identifier in partition.Identifiers)
            {
                Console.WriteLine("Part identifier: {0}", identifier);
                Console.WriteLine("     indexes: {0}", partition[identifier]);
                Console.WriteLine();
            }

            // Convert the partition to a matrix.
            var fromPartition = DoubleMatrix.FromIndexPartition(partition);
            Console.WriteLine("Conversion of a partition to a matrix:");
            Console.WriteLine(fromPartition);
        }
    }
}