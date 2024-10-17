using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class DivisionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new double[4] {
                0,  2,
                1,  3
            };
            var left = DoubleMatrix.Dense(2, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            data = [
                0,  30,
               10,  40,
               20,  50
            ];
            var right = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Divide left by right.
            var result = left / right;

            Console.WriteLine();
            Console.WriteLine("left / right =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Divide.
            result = DoubleMatrix.Divide(left, right);

            Console.WriteLine();
            Console.WriteLine("DoubleMatrix.Divide(left, right) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the division using a read-only wrapper of left.
            ReadOnlyDoubleMatrix readOnlyLeft = left.AsReadOnly();
            result = readOnlyLeft / right;

            Console.WriteLine();
            Console.WriteLine("readOnlyLeft / right =");
            Console.WriteLine(result);
        }
    }
}