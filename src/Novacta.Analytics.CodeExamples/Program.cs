using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    class Program
    {
        static void Main(string[] _)
        {
            string codeBase = @"..\..\..\..\Novacta.Analytics.CodeExamples";
            string defaultNamespace = "Novacta.Analytics.CodeExamples";
            var analyzer = new CodeExamplesAnalyzer(
                codeBase,
                defaultNamespace);
            analyzer.Run();
            Console.ReadKey();
        }
    }
}