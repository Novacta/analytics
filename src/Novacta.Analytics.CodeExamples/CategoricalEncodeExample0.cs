using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class CategoricalEncodeExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a data stream.
            string[] data = new string[6] {
            "COLOR,NUMBER",
            "Red,  -2.2",
            "Green, 0.0",
            "Red,  -3.3",
            "Black,-1.1",
            "Black, 4.4" };

            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++) {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Define a special categorizer for variable NUMBER
            // using a local function.
            static string numberCategorizer(string token, IFormatProvider provider)
            {
                double datum = Convert.ToDouble(token, provider);
                if (datum == 0)
                {
                    return "Zero";
                }
                else if (datum < 0)
                {
                    return "Negative";
                }
                else
                {
                    return "Positive";
                }
            }

            // Attach the special categorizer to variable NUMBER.
            int numberColumnIndex = 1;
            var specialCategorizers = new Dictionary<int, Categorizer>
            {
                { numberColumnIndex, numberCategorizer }
            };

            // Encode the categorical data set.
            StreamReader streamReader = new(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Range(0, 1);
            bool firstLineContainsColumnHeaders = true;
            CategoricalDataSet dataset = CategoricalDataSet.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders,
                specialCategorizers,
                CultureInfo.InvariantCulture);

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
