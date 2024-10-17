using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDenseExample6 : ICodeExample
    {
        public void Main()
        {
            // Set matrix dimensions.
            const int numberOfRows = 3;
            const int numberOfColumns = 2;

            // Create the data as an array having lower bounds equal to zero.
            var data = new Complex[numberOfRows, numberOfColumns]
                { { new Complex(1, -1), new Complex(2, -2) },
                  { new Complex(3, -3), new Complex(4, -4) },
                  { new Complex(5, -5), new Complex(6, -6) } };

            // Create the matrix. 
            var matrix = ComplexMatrix.Dense(data);
            Console.WriteLine("Creating from an array having zero lower bounds.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            Console.WriteLine();

            // Create the data as an array having non-zero lower bounds.
            int[] lowerBounds = [2005, 1];
            int[] lengths = [numberOfRows, numberOfColumns];
            data = (Complex[,])Array.CreateInstance(typeof(Complex),
                    lengths, lowerBounds);

            data[2005, 1] = new Complex(1, -1); 
            data[2006, 1] = new Complex(3, -3); 
            data[2007, 1] = new Complex(5, -5);

            data[2005, 2] = new Complex(2, -2);
            data[2006, 2] = new Complex(4, -4);
            data[2007, 2] = new Complex(6, -6);

            // Create the matrix. 
            matrix = ComplexMatrix.Dense(data);
            Console.WriteLine("Creating from an array having non-zero lower bounds.");
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);
        }
    }
}