using Novacta.Documentation.CodeExamples;
using System;
using System.Text.Json;

namespace Novacta.Analytics.CodeExamples
{
    public class JsonSerializationExample0 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            const int numberOfRows = 3;
            const int numberOfColumns = 2;

            // Create the data as an array having lower bounds equal to zero.
            var data = new double[numberOfRows, numberOfColumns]
                { { 1, 2 },
                  { 3, 4 },
                  { 5, 6 } };

            // Create the matrix. 
            var serializedMatrix = DoubleMatrix.Dense(data);

            // Set the matrix name.
            serializedMatrix.Name = "MyData";

            // Set names for some rows and columns.
            serializedMatrix.SetRowName(0, "Row-0");
            serializedMatrix.SetRowName(2, "Row-2");
            serializedMatrix.SetColumnName(1, "Col-0");

            // Show the matrix.
            Console.WriteLine("Serialized matrix:");
            Console.WriteLine(serializedMatrix);
            Console.WriteLine();

            // Add converters to a JsonSerializerOptions instance
            // to support the JSON serialization of data types
            // defined in the Novacta.Analytics namespace.
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            JsonSerialization.AddDataConverters(options);

            // Create a JSON representation of the matrix.
            // The options previously defined must be passed as a parameter 
            // to method Serialize of class System.Text.Json.JsonSerializer.
            string json = JsonSerializer.Serialize(
                serializedMatrix,
                typeof(DoubleMatrix),
                options);

            // Show the JSON value representing the matrix.
            Console.WriteLine("JSON matrix representation:");
            Console.WriteLine(json);
            Console.WriteLine();

            // Create a matrix from the JSON representation.
            // The options previously defined must be passed as a parameter 
            // to method Deserialize of class System.Text.Json.JsonSerializer.
            var deserializedMatrix = JsonSerializer.Deserialize<DoubleMatrix>(
                json,
                options);

            // Show the deserialized matrix.
            Console.WriteLine("Deserialized matrix:");
            Console.WriteLine(deserializedMatrix);
        }
    }
}
