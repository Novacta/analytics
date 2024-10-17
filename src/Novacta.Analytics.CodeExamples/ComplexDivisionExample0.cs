using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexDivisionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new Complex[4] {
                new(0, 0),  new(2, 2),
                new(1, 1),  new(3, 3)
            };
            var left = ComplexMatrix.Dense(2, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            data = [
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7)
            ];
            var right = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Divide left by right.
            var result = left / right;

            Console.WriteLine();
            Console.WriteLine("left / right =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Divide.
            result = ComplexMatrix.Divide(left, right);

            Console.WriteLine();
            Console.WriteLine("ComplexMatrix.Divide(left, right) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the division using a read-only wrapper of left.
            ReadOnlyComplexMatrix readOnlyLeft = left.AsReadOnly();
            result = readOnlyLeft / right;

            Console.WriteLine();
            Console.WriteLine("readOnlyLeft / right =");
            Console.WriteLine(result);
        }
    }
}