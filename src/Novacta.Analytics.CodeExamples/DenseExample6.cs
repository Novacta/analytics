using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class DenseExample6 : ICodeExample
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
            var matrix = DoubleMatrix.Dense(data);
            Console.WriteLine("Creating from an array having zero lower bounds.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            Console.WriteLine();

            // Create the data as an array having non-zero lower bounds.
            int[] lowerBounds = [2005, 1];
            int[] lengths = [numberOfRows, numberOfColumns];
            data = (double[,])Array.CreateInstance(typeof(double),
                    lengths, lowerBounds);

            data[2005, 1] = 1.0; data[2005, 2] = 2.0; 
            data[2006, 1] = 3.0; data[2006, 2] = 4.0; 
            data[2007, 1] = 5.0; data[2007, 2] = 6.0;

            // Create the matrix. 
            matrix = DoubleMatrix.Dense(data);
            Console.WriteLine("Creating from an array having non-zero lower bounds.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}