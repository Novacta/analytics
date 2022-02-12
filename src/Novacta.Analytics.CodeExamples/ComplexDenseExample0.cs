using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDenseExample0 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Create the data.
            var data = new Complex[6] {
                new Complex(1, -1), new Complex(5, -5),
                new Complex(2, -2), new Complex(6, -6),
                new Complex(3, -3), new Complex(7, -7)
            };

            // Assume the data as RowMajor ordered.
            StorageOrder storageOrder = StorageOrder.RowMajor;

            // Create the matrix.
            var matrix = ComplexMatrix.Dense(
                numberOfRows, numberOfColumns, data, storageOrder);
            Console.WriteLine("Assuming RowMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Assume the data as ColMajor ordered.
            storageOrder = StorageOrder.ColumnMajor;

            // Create the matrix.
            matrix = ComplexMatrix.Dense(
                numberOfRows, numberOfColumns, data, storageOrder);
            Console.WriteLine("Assuming ColMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}
