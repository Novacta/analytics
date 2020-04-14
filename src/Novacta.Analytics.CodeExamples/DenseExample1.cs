using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.CodeExamples
{
    public class DenseExample1 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            int numberOfRows = 3;
            int numberOfColumns = 2;

            // Create the data.
            var data = new List<double>(6) {
               1,  2,  3,  4,  5,  6
            } as IEnumerable<double>;

            // Assume the data as RowMajor ordered.
            StorageOrder storageOrder = StorageOrder.RowMajor;

            // Create the matrix.
            var matrix = DoubleMatrix.Dense(
                numberOfRows, numberOfColumns, data, storageOrder);
            Console.WriteLine("Assuming RowMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Assume the data as ColMajor ordered.
            storageOrder = StorageOrder.ColumnMajor;

            // Create the matrix.
            matrix = DoubleMatrix.Dense(
                numberOfRows, numberOfColumns, data, storageOrder);
            Console.WriteLine("Assuming ColMajor ordered data.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}
