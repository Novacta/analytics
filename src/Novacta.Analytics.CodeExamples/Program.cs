using Novacta.Analytics.Runtime;
using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    class Program
    {
        static void Main(string[] _)
        {
            AnalyticsRuntime.Deploy();
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