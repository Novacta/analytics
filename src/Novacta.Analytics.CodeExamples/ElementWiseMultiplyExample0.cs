using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class ElementWiseMultiplyExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new double[6] {
                0,  2, 4,
                1,  3, 5
            };
            var left = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            data = new double[6] {
                0,  20,  40,
               10,  30,  50
            };
            var right = DoubleMatrix.Dense(2, 3, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Element wise multiply left by right.
            var result = DoubleMatrix.ElementWiseMultiply(left, right);

            Console.WriteLine();
            Console.WriteLine("Element wise multiplication: left * right =");
            Console.WriteLine(result);

            // Class ReadOnlyDoubleMatrix supports element wise multiplications
            // where some arguments are read-only matrices.
            // Compute the product using a read-only wrapper of left.
            ReadOnlyDoubleMatrix readOnlyLeft = left.AsReadOnly();
            result = ReadOnlyDoubleMatrix.ElementWiseMultiply(readOnlyLeft, right);

            Console.WriteLine();
            Console.WriteLine("Element wise multiplication: readOnlyLeft * right =");
            Console.WriteLine(result);
        }
    }
}