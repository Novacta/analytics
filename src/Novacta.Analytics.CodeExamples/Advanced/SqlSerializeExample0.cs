#if ALPHA
using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;
using System.IO;

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class SqlSerializeExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
               -1,  Double.NaN,
                2, -3,
                3,  4,
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);

            // Add a name to the matrix.
            matrix.Name = "My matrix";

            // Add names to rows and columns.
            matrix.SetRowName(0, "row0");
            matrix.SetRowName(1, "row1");
            matrix.SetRowName(2, "row2");
            matrix.SetColumnName(0, "column0");
            matrix.SetColumnName(1, "column1");

            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
            Console.WriteLine();

            // Create a SQL matrix serializer.
            SqlMatrixSerializer serializer = new SqlMatrixSerializer();

            // Eventually set the connection string.
            serializer.ConnectionString = "Data Source=(local);" +
                "Initial Catalog=Statisnet_MatrixRepository;" + 
                "Integrated Security=True;Connect Timeout=30;";

            // Eventually set the application name.
            serializer.ApplicationName = "DefaultApplication";

            // Set the repository name.
            string repositoryName = "My repository";

            // Set the matrix name in the repository.
            string matrixName = matrix.Name;

            // Serialize the matrix.
            serializer.Serialize(repositoryName, matrixName, matrix);

            // Deserialize the matrix from the repository.
            var deserializedMatrix = serializer.Deserialize(repositoryName, matrixName);

            // Show the deserialized matrix.
            Console.WriteLine("Deserialized matrix:");
            Console.WriteLine(deserializedMatrix);

            // Serialize is overloaded to accept data as a read-only matrix:
            // serialize a read-only wrapper of the matrix.
            serializer.Serialize(repositoryName, matrixName, matrix.AsReadOnly());

            // Deserialize again the matrix.
            deserializedMatrix = serializer.Deserialize(repositoryName, matrixName);

            // Show the deserialized matrix.
            Console.WriteLine("Deserialized matrix after serialization of a read-only matrix:");
            Console.WriteLine(deserializedMatrix);

        }
    }
}
#endif