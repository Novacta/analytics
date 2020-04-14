using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class NegationExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the operand.
            var data = new double[6] {
                0,  2,  4,
                1,  3,  5,
            };
            var operand = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("operand =");
            Console.WriteLine(operand);

            // Compute the negation of operand.
            var result = - operand;

            Console.WriteLine();
            Console.WriteLine("- operand =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Negate.
            result = DoubleMatrix.Negate(operand);

            Console.WriteLine();
            Console.WriteLine("DoubleMatrix.Negate(operand) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the negation using a read-only wrapper of operand.
            ReadOnlyDoubleMatrix readOnlyOperand = operand.AsReadOnly();
            result = - readOnlyOperand;

            Console.WriteLine();
            Console.WriteLine("- readOnlyOperand =");
            Console.WriteLine(result);
        }
    }
}