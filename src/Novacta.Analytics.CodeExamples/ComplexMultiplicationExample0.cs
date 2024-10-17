using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexMultiplicationExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new Complex[6] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7)
            };
            var left = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            data = [
                new(10, -10), new(50, -50),
                new(20, -20), new(60, -60)
            ];
            var right = ComplexMatrix.Dense(2, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Multiply left by right.
            var result = left * right;

            Console.WriteLine();
            Console.WriteLine("left * right =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Multiply.
            result = ComplexMatrix.Multiply(left, right);

            Console.WriteLine();
            Console.WriteLine("ComplexMatrix.Multiply(left, right) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the product using a read-only wrapper of left.
            ReadOnlyComplexMatrix readOnlyLeft = left.AsReadOnly();
            result = readOnlyLeft * right;

            Console.WriteLine();
            Console.WriteLine("readOnlyLeft * right =");
            Console.WriteLine(result);
        }
    }
}