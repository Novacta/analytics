﻿using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexOutPlaceTransposeExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[6] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7)
            };
            var matrix = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Return its transpose.
            var transposedMatrix = matrix.Transpose();

            Console.WriteLine();
            Console.WriteLine("Matrix transpose:");
            Console.WriteLine(transposedMatrix);

            // Compute the transpose using a read-only wrapper of the data matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();
            var transposedReadOnlyMatrix = readOnlyMatrix.Transpose();

            Console.WriteLine();
            Console.WriteLine("Read only matrix transpose:");
            Console.WriteLine(transposedReadOnlyMatrix);
        }
    }
}