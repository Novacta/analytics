﻿using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexInPlaceConjugateExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[6] {
                new Complex(1, -1), new Complex(5, -5),
                new Complex(2, -2), new Complex(6, -6),
                new Complex(3, -3), new Complex(7, -7)
            };
            var matrix = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("Initial data matrix:");
            Console.WriteLine(matrix);

            // Transform in the conjugate matrix.
            matrix.InPlaceConjugate();

            Console.WriteLine();
            Console.WriteLine("Conjugate data matrix:");
            Console.WriteLine(matrix);
        }
    }
}