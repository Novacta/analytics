﻿using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexAdditionExample2 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            Complex left = new(7, -7);
            Console.WriteLine("left =");
            Console.WriteLine(left);
            Console.WriteLine();

            // Create the right operand.
            var data = new Complex[6] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7)
            };
            var right = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Compute the sum of left and right.
            var result = left + right;

            Console.WriteLine();
            Console.WriteLine("left + right =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Add.
            result = ComplexMatrix.Add(left, right);

            Console.WriteLine();
            Console.WriteLine("ComplexMatrix.Add(left, right) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the sum using a read-only wrapper of right.
            ReadOnlyComplexMatrix readOnlyRight = right.AsReadOnly();
            result = left + readOnlyRight;

            Console.WriteLine();
            Console.WriteLine("left + readOnlyRight =");
            Console.WriteLine(result);
        }
    }
}