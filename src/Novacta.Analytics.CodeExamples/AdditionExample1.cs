using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class AdditionExample1 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new double[6] {
                0,  2,  4,
                1,  3,  5,
            };
            var left = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            double right = 10.0;
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Compute the sum of left and right.
            var result = left + right;

            Console.WriteLine();
            Console.WriteLine("left + right =");
            Console.WriteLine(result);

            // In .NET languages that do not support overloaded operators,
            // you can use the alternative methods named Add.
            result = DoubleMatrix.Add(left, right);

            Console.WriteLine();
            Console.WriteLine("DoubleMatrix.Add(left, right) returns");
            Console.WriteLine();
            Console.WriteLine(result);

            // Both operators and alternative methods are overloaded to 
            // support read-only matrix arguments.
            // Compute the sum using a read-only wrapper of left.
            ReadOnlyDoubleMatrix readOnlyLeft = left.AsReadOnly();
            result = readOnlyLeft + right;

            Console.WriteLine();
            Console.WriteLine("readOnlyLeft + right =");
            Console.WriteLine(result);
        }
    }
}