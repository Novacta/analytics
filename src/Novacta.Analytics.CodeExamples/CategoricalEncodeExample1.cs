using Novacta.Documentation.CodeExamples;
using System;
using System.Globalization;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class CategoricalEncodeExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a data stream.
            const int numberOfInstances = 27;
            string[] data = [
            "NUMERICAL,TARGET",
            "0,A",
            "0,A",
            "0,A",
            "1,B",
            "1,B",
            "1,B",
            "1,B",
            "2,B",
            "2,B",
            "3,C",
            "3,C",
            "3,C",
            "4,B",
            "4,B",
            "4,B",
            "4,C",
            "5,A",
            "5,A",
            "6,A",
            "7,C",
            "7,C",
            "7,C",
            "8,C",
            "8,C",
            "9,C",
            "9,C",
            "9,C" ];

            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++) {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Identify the special categorizer for variable NUMERICAL.
            StreamReader streamReader = new(stream);
            char columnDelimiter = ',';
            IndexCollection numericalColumns = IndexCollection.Range(0, 0);
            bool firstLineContainsColumnHeaders = true;
            int targetColumn = 1;
            IFormatProvider provider = CultureInfo.InvariantCulture;
            var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                streamReader,
                columnDelimiter,
                numericalColumns,
                firstLineContainsColumnHeaders,
                targetColumn,
                provider);

            // Encode the categorical data set using the special categorizer.
            stream.Position = 0;
            IndexCollection extractedColumns = IndexCollection.Range(0, 1);
            CategoricalDataSet dataset = CategoricalDataSet.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders,
                specialCategorizers,
                provider);

            // Decode and show the data set.
            Console.WriteLine("Decoded data set:");
            Console.WriteLine();
            var decodedDataSet = dataset.Decode();
            int numberOfVariables = dataset.Data.NumberOfColumns;

            foreach (var variable in dataset.Variables) {
                Console.Write(variable.Name + ",");
            }
            Console.WriteLine();

            for (int i = 0; i < numberOfInstances; i++) {
                for (int j = 0; j < numberOfVariables; j++) {
                    Console.Write(decodedDataSet[i][j] + ",");
                }
                Console.WriteLine();
            }
        }
    }
}