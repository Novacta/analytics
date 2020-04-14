using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class IndexPartitionExample3 : ICodeExample
    {
        public void Main()
        {
            // Create an array of strings.
            var data = new string[6] {
                "one",
                "two",
                "one",
                "one",
                "three",
                "three"
            };

            // Partition the array positions by their contents.
            var partition = IndexPartition.Create(data);

            // The partition contains three parts, identified, respectively,
            // by the strings "one", "two", and "three".
            Console.WriteLine();
            foreach (var identifier in partition.Identifiers) {
                Console.WriteLine("Part identifier: {0}", identifier);
                Console.WriteLine("     indexes: {0}", partition[identifier]);
                Console.WriteLine();
            }
        }
    }
}