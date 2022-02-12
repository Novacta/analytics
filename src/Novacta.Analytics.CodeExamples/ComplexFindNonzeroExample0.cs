using Novacta.Documentation.CodeExamples;
using System;
using System.Numerics;

namespace Novacta.Analytics.CodeExamples
{
    public class ComplexFindNonzeroExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new Complex[6] {
                new Complex(0,  0), new Complex(5, -5),
                new Complex(2, -2), new Complex(0,  0),
                new Complex(0,  0), new Complex(2, -2)
            };
            var matrix = ComplexMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // Find the linear indexes of nonzero entries in data.
            var indexes = matrix.FindNonzero();

            Console.WriteLine();
            Console.WriteLine("Linear indexes of nonzero entries in data:");
            Console.WriteLine(indexes);

            // FindNonzero is available for read-only matrices:
            // find nonzero entries using a read-only wrapper of the data matrix.
            ReadOnlyComplexMatrix readOnlyMatrix = matrix.AsReadOnly();
            indexes = readOnlyMatrix.FindNonzero();

            Console.WriteLine();
            Console.WriteLine("Using read-only data. Linear indexes of nonzero entries:");
            Console.WriteLine(indexes);
        }
    }
}