
using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class MatrixEncodeExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a data stream.
            string[] data = [
            "TIME,NUMBER",
            "20200410 09:42:00.000 +00:00, -2.2",
            "20210511 11:51:00.010 +00:00,  0.0",
            "20220612 15:11:31.200 +00:00, -3.3",
            "20230713 17:32:10.749 +00:00, -1.1",
            "20240814 09:42:00.150 +00:00,  4.4" ];

            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Define a special codifier for variable TIME
            // using a local function.
            static double timeCodifier(string token, IFormatProvider provider)
            {
                double datum = DateTimeOffset.ParseExact(
                    input: token,
                    format: "yyyyMMdd HH:mm:ss.fff zzz",
                    formatProvider: provider).ToUnixTimeMilliseconds();

                return datum;
            }

            // Attach the special codifier to variable TIME.
            int numberColumnIndex = 0;
            var specialCodifiers = new Dictionary<int, Codifier>
            {
                { numberColumnIndex, timeCodifier }
            };

            // Encode the matrix.
            StreamReader streamReader = new(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Range(0, 1);
            bool firstLineContainsColumnHeaders = true;
            DoubleMatrix matrix = DoubleMatrix.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders,
                specialCodifiers,
                CultureInfo.InvariantCulture);

            // Show the matrix.
            Console.WriteLine("Encoded matrix:");
            Console.WriteLine();
            Console.Write(matrix);
            Console.WriteLine();

            // Decode variable TIME.
            Console.WriteLine("Decoded variable TIME:");
            Console.WriteLine();
            var time = matrix[":", 0];
            for (int i = 0; i < time.Count; i++)
            {
                Console.WriteLine(
                    "Time {0}: {1}",
                    i,
                    DateTimeOffset
                        .FromUnixTimeMilliseconds(Convert.ToInt64(time[i]))
                        .ToString("yyyyMMdd HH:mm:ss.fff zzz"));
            }
        }
    }
}
