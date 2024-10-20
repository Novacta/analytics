﻿using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexSubtractionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new Complex[8] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7),
                new(4, -4), new(8, -8)
            };
            var left = ComplexMatrix.Dense(4, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            data = [
                new(10, -1), new(50, -5),
                new(20, -2), new(60, -6),
                new(30, -3), new(70, -7),
                new(40, -4), new(80, -8)
            ];
            var right = ComplexMatrix.Dense(4, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Subtract right from left.
            var result = left - right;

            Console.WriteLine();
            Console.WriteLine("left - right =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Subtract.
            result = ComplexMatrix.Subtract(left, right);

            Console.WriteLine();
            Console.WriteLine("ComplexMatrix.Subtract(left, right) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the subtraction using a read-only wrapper of left.
            ReadOnlyComplexMatrix readOnlyLeft = left.AsReadOnly();
            result = readOnlyLeft - right;

            Console.WriteLine();
            Console.WriteLine("readOnlyLeft - right =");
            Console.WriteLine(result);
        }
    }
}