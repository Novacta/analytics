using Novacta.Documentation.CodeExamples;
using System;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixEncodeExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a data stream.
            string[] data = [
            "V0,V1,V2",
            "1, 10, -2.2",
            "2, 20,  0.0",
            "3, 30, -3.3",
            "4, 40, -1.1",
            "5, 50,  4.4" ];

            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Encode a matrix containing columns V0 and V2.
            StreamReader streamReader = new(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
            bool firstLineContainsColumnHeaders = true;
            DoubleMatrix matrix = DoubleMatrix.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders);

            // Show the matrix.
            Console.WriteLine("Encoded matrix:");
            Console.WriteLine();
            Console.Write(matrix);
            Console.WriteLine();
        }
    }
}
