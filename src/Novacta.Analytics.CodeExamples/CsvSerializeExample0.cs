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
            string[] data = new string[4] {
                "Dense,2,3,MatrixName",
                ",column0,,column2",
                "row0,1.0,NaN,5.0",
                "row1,2.0,4.0,6.0"};

            // Show the matrix CSV representation.
            Console.WriteLine("CSV representation of the matrix:");
            for (int i = 0; i < data.Length; i++) {
                Console.WriteLine(data[i]);
            }
            Console.WriteLine();

            // Create a stream containing the CSV content.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < data.Length; i++) {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Create a reader for the stream.
            StreamReader reader = new StreamReader(stream);

            // Deserialize the CSV document contained in the stream.
            DoubleMatrix matrix = CsvMatrixSerializer.Deserialize(reader);

            // Show the matrix.
            Console.WriteLine("Deserialized matrix:");
            Console.WriteLine(matrix);
        }
    }
}
