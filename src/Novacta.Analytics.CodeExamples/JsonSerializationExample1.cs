using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Novacta.Analytics.CodeExamples
{
    public class JsonSerializationExample1 : ICodeExample
    {
        public void Main()
        {
            // Create the feature variables.
            CategoricalVariable f0 = new("F-0")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };
            f0.SetAsReadOnly();

            CategoricalVariable f1 = new("F-1")
                {
                    { 0, "I" },
                    { 1, "II" },
                    { 2, "III" },
                    { 3, "IV" }
                };
            f1.SetAsReadOnly();

            // Create the response variable.
            CategoricalVariable r = new("R")
                {
                    0,
                    1,
                    2
                };
            r.SetAsReadOnly();

            // Create a categorical data set containing
            // observations about such variables.
            List<CategoricalVariable> variables =
                new() { f0, f1, r };

            DoubleMatrix data = DoubleMatrix.Dense(
                new double[8, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 1 },
                    { 3, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

            CategoricalDataSet serializedDataSet = CategoricalDataSet.FromEncodedData(
                variables,
                data);

            // Set the data set name.
            serializedDataSet.Name = "MyData";

            // Add converters to a JsonSerializerOptions instance
            // to support the JSON serialization of data types
            // defined in the Novacta.Analytics namespace.
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            JsonSerialization.AddDataConverters(options);

            // Create a JSON representation of the data set.
            // The options previously defined must be passed as a parameter 
            // to method Serialize of class System.Text.Json.JsonSerializer.
            string json = JsonSerializer.Serialize(
                serializedDataSet,
                typeof(CategoricalDataSet),
                options);

            // Show the JSON value representing the data set.
            Console.WriteLine("JSON data set representation:");
            Console.WriteLine(json);
            Console.WriteLine();

            // Create a data set from the JSON representation.
            // The options previously defined must be passed as a parameter 
            // to method Deserialize of class System.Text.Json.JsonSerializer.
            var deserializedDataSet = JsonSerializer.Deserialize<CategoricalDataSet>(
                json,
                options);

            // Show the deserialized data set name.
            Console.WriteLine("Deserialized data set name:");
            Console.WriteLine(deserializedDataSet.Name);
        }
    }
}
