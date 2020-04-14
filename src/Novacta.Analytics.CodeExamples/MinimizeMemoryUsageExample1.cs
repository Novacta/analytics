using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class MinimizeMemoryUsageExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a dense, huge matrix.
            var matrix = DoubleMatrix.Dense(200, 200);

            // Measure the working set, i.e. the amount of
            // physical memory, in bytes, mapped to the current process.
            var workingSet0 = Environment.WorkingSet;
            Console.WriteLine();
            Console.WriteLine("Initial working set: {0}", workingSet0);
            Console.WriteLine();

            // Extract a sub-matrix allowing dense storage allocations.
            _ = matrix[":", ":"];

            // Verify how the working set changed.
            var workingSet1 = Environment.WorkingSet;
            Console.WriteLine();
            Console.WriteLine("Working set after extraction without minimization: {0}",
                workingSet1);
            Console.WriteLine();
            Console.WriteLine("Difference (in bytes) between working sets: {0}",
                workingSet1 - workingSet0);
        }
    }
}