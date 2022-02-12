using Novacta.Documentation.CodeExamples;
using System;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class CategoricalGetContingencyTableExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a data stream.
            string[] data = new string[7] {
                        "COLOR,NUMBER",
                        "Red,Negative",
                        "Green,Zero",
                        "White,Positive",
                        "Red,Negative",
                        "Blue,Negative",
                        "Blue,Positive" };

            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Encode the categorical data set.
            StreamReader streamReader = new(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Range(0, 1);
            bool firstLineContainsColumnHeaders = true;
            var dataSet = CategoricalDataSet.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders);

            // Assign the categories of variable NUMBER
            // to the rows of the table.
            int rowVariableIndex = 1;

            // Assign the categories of variable COLOR
            // to the columns of the table.
            int columnVariableIndex = 0;

            // Get the NUMBER-by-COLOR table.
            var table = dataSet.GetContingencyTable(
                rowVariableIndex, 
                columnVariableIndex);

            // Show the table.
            Console.WriteLine("Contingency table:");
            Console.WriteLine(table);
        }
    }
}