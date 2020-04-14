using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IndexPartitionExample0 : ICodeExample
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

            // Partition the matrix row indexes by the contents of column 0:
            // a part is created for each distinct value in column 0.
            var partition = IndexPartition.Create(matrix[":", 0]);

            // Each part is identified by its corresponding value and contains
            // the indexes of the rows in which the identifier
            // is positioned in column 0.
            Console.WriteLine();
            foreach (var identifier in partition.Identifiers) {
                Console.WriteLine("Part identifier: {0}", identifier);
                Console.WriteLine("     indexes: {0}", partition[identifier]);
                Console.WriteLine();
            }
        }
    }
}