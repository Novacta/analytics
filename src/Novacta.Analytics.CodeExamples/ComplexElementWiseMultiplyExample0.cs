using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexElementWiseMultiplyExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the left operand.
            var data = new Complex[6] {
                new Complex(1, -1), new Complex(5, -5),
                new Complex(2, -2), new Complex(6, -6),
                new Complex(3, -3), new Complex(7, -7)
            };
            var left = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("left =");
            Console.WriteLine(left);

            // Create the right operand.
            data = new Complex[6] {
                new Complex(-1, 1), new Complex(-5, 5),
                new Complex(-2, 2), new Complex(-6, 6),
                new Complex(-3, 3), new Complex(-7, 7)
            };
            var right = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("right =");
            Console.WriteLine(right);

            // Element wise multiply left by right.
            var result = ComplexMatrix.ElementWiseMultiply(left, right);

            Console.WriteLine();
            Console.WriteLine("Element wise multiplication: left * right =");
            Console.WriteLine(result);

            // Class ReadOnlyComplexMatrix supports element wise multiplications
            // where some arguments are read-only matrices.
            // Compute the product using a read-only wrapper of left.
            ReadOnlyComplexMatrix readOnlyLeft = left.AsReadOnly();
            result = ReadOnlyComplexMatrix.ElementWiseMultiply(readOnlyLeft, right);

            Console.WriteLine();
            Console.WriteLine("Element wise multiplication: readOnlyLeft * right =");
            Console.WriteLine(result);
        }
    }
}