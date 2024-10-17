using Novacta.Documentation.CodeExamples;
using System;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class CsvSerializeExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the CSV representation of a dense matrix.
            string[] data = [
                "Dense|Complex,3,2,MatrixName",
                ",column0,column1",
                "row0,(1.0 -2.0),(NaN NaN)",
                "row1,(2.0 -3.0),(4.0 -1.0)",
                "row2,(0 0),(5.0 7.0)"
            ];

            // Show the matrix CSV representation.
            Console.WriteLine("CSV representation of the matrix:");
            for (int i = 0; i < data.Length; i++) {
                Console.WriteLine(data[i]);
            }
            Console.WriteLine();

            // Create a stream containing the CSV content.
            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++) {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Create a reader for the stream.
            StreamReader reader = new(stream);

            // Deserialize the CSV document contained in the stream.
            ComplexMatrix matrix = 
                CsvComplexMatrixSerializer.Deserialize(reader);

            // Show the matrix.
            Console.WriteLine("Deserialized matrix:");
            Console.WriteLine(matrix);
        }
    }
}
