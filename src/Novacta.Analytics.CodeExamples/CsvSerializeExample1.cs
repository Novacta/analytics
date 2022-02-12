using Novacta.Documentation.CodeExamples;
using System;
using System.IO;

namespace Novacta.Analytics.CodeExamples
{
    public class CsvSerializeExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var matrix = DoubleMatrix.Sparse(3, 2, capacity: 2);
            matrix[0, 1] = Double.NaN;
            matrix[2, 0] = -2.2;

            // Add a name to the matrix.
            matrix.Name = "My sparse matrix";

            // Add names to rows and columns.
            matrix.SetRowName(0, "row0");
            matrix.SetRowName(1, "row1");
            matrix.SetRowName(2, "row2");
            matrix.SetColumnName(0, "column0");
            matrix.SetColumnName(1, "column1");

            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();

            // Create a stream to serialize the matrix.
            MemoryStream stream = new();
            StreamWriter writer = new(stream);

            // Serialize the matrix.
            CsvDoubleMatrixSerializer.Serialize(writer, matrix);

            // Read the CSV representation of the matrix from the stream.
            stream.Position = 0;
            StreamReader reader = new(stream);
            string line;
            Console.WriteLine("CSV representation of the matrix:");
            while ((line = reader.ReadLine()) != null) {
                Console.WriteLine(line);
            }
            Console.WriteLine();

            // Deserialize the matrix from the stream.
            stream.Position = 0;
            var deserializedMatrix = 
                CsvDoubleMatrixSerializer.Deserialize(reader);

            // Show the deserialized matrix.
            Console.WriteLine("Deserialized matrix:");
            Console.WriteLine(deserializedMatrix);

            // Serialize is overloaded to accept data as a read-only matrix:
            // serialize a read-only wrapper of the matrix.
            stream.Position = 0;
            CsvDoubleMatrixSerializer.Serialize(writer, matrix.AsReadOnly());

            // Deserialize again the matrix.
            stream.Position = 0;
            var deserializedReadOnlyMatrix = 
                CsvDoubleMatrixSerializer.Deserialize(reader);

            // Show the deserialized matrix.
            Console.WriteLine("Deserialized matrix after serialization of a read-only matrix:");
            Console.WriteLine(deserializedReadOnlyMatrix);
        }
    }
}
