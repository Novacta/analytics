using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class BasisExample0 : ICodeExample
    {
        public void Main()
        {
            Console.WriteLine("Basis Example");
            Console.WriteLine();
            Console.WriteLine("Standard Basis of dimension 2:");

            Basis standard = Basis.Standard(2);

            Console.WriteLine("Basis matrix:");
            Console.WriteLine(standard.GetBasisMatrix().ToString());
            Console.WriteLine();
        }
    }
}
