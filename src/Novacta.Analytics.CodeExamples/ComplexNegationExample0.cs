using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexNegationExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the operand.
            var data = new Complex[8] {
                new(1, -1), new(5, -5),
                new(2, -2), new(6, -6),
                new(3, -3), new(7, -7),
                new(4, -4), new(8, -8)
            };
            var operand = ComplexMatrix.Dense(4, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("operand =");
            Console.WriteLine(operand);

            // Compute the negation of operand.
            var result = -operand;

            Console.WriteLine();
            Console.WriteLine("- operand =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Negate.
            result = ComplexMatrix.Negate(operand);

            Console.WriteLine();
            Console.WriteLine("DoubleMatrix.Negate(operand) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the negation using a read-only wrapper of operand.
            ReadOnlyComplexMatrix readOnlyOperand = operand.AsReadOnly();
            result = -readOnlyOperand;

            Console.WriteLine();
            Console.WriteLine("- readOnlyOperand =");
            Console.WriteLine(result);
        }
    }
}