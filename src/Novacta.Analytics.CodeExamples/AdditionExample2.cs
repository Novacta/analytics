using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class AdditionExample2 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            double left = 10.0;
            Console.WriteLine("left =");
            Console.WriteLine(left);
            Console.WriteLine();

            // Create the right operand.
            var data = new double[6] {
                0,  20,  40,
               10,  30,  50,
            };
            var right = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
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
            // Compute the sum using a read-only wrapper of right.
            ReadOnlyDoubleMatrix readOnlyRight = right.AsReadOnly();
            result = left + readOnlyRight;

            Console.WriteLine();
            Console.WriteLine("left + readOnlyRight =");
            Console.WriteLine(result);
        }
    }
}