using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexMultiplicationExample2 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            Complex left = new(2, -2);
            Console.WriteLine("left =");
            Console.WriteLine(left);
            Console.WriteLine();

            // Create the right operand.
            var data = new Complex[6] {
                new Complex(1, -1), new Complex(5, -5),
                new Complex(2, -2), new Complex(6, -6),
                new Complex(3, -3), new Complex(7, -7)
            };
            var right = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
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
            // Compute the product using a read-only wrapper of right.
            ReadOnlyComplexMatrix readOnlyRight = right.AsReadOnly();
            result = left * readOnlyRight;

            Console.WriteLine();
            Console.WriteLine("left * readOnlyRight =");
            Console.WriteLine(result);
        }
    }
}