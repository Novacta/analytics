using Novacta.Documentation.CodeExamples;
using System;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class CategoricalEncodeExample2 : ICodeExample
    {
        public void Main()
        {
            // Create a data stream.
            string[] data = new string[6] {
            "COLOR,NUMBER",
            "Red,Negative",
            "Green,Zero",
            "Red,Negative",
            "Black,Negative",
            "Black,Positive" };

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < data.Length; i++) {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Encode the categorical data set.
            StreamReader streamReader = new StreamReader(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Range(0, 1);
            bool firstLineContainsColumnHeaders = true;
            CategoricalDataSet dataset = CategoricalDataSet.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders);

            // Decode and show the data set. 
            Console.WriteLine("Decoded data set:");
            Console.WriteLine();
            var decodedDataSet = dataset.Decode();
            int numberOfInstances = dataset.Data.NumberOfRows;
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