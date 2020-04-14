using Novacta.Analytics.Advanced;
using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class ContinuousOptimizationContextExample0 : ICodeExample
    {
        public void Main()
        {
            // Define the objective function as follows: 
            // f(x) = exp(-(x-2)^2) + (.8) * exp(-(x+2)^2)).
            static double objectiveFunction(DoubleMatrix x)
            {
                double y = 0.0;
                var x_0 = x[0];
                y += Math.Exp(-Math.Pow(x_0 - 2.0, 2.0));
                y += .8 * Math.Exp(-Math.Pow(x_0 + 2.0, 2.0));
                return y;
            }

            // Define the argument at which the method starts 
            // the search for optimality.
            var initialArgument = DoubleMatrix.Dense(1, 1, -6.0);

            // Create the context.
            var context = new ContinuousOptimizationContext(
                objectiveFunction: objectiveFunction,
                initialArgument: initialArgument,
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                optimizationGoal: OptimizationGoal.Maximization,
                terminationTolerance: 1.0e-3,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            // Create the optimizer.
            var optimizer = new SystemPerformanceOptimizer()
            {
                PerformanceEvaluationParallelOptions = { MaxDegreeOfParallelism = -1 },
                SampleGenerationParallelOptions = { MaxDegreeOfParallelism = -1 }
            };

            // Set optimization parameters.
            double rarity = 0.01;
            int sampleSize = 1000;

            // Solve the problem.
            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            var maximizer = results.OptimalState;

            // Show the results.
            Console.WriteLine(
                "The Cross-Entropy optimizer has converged: {0}.",
                results.HasConverged);

            Console.WriteLine();
            Console.WriteLine("Initial guess parameter:");
            Console.WriteLine(context.InitialParameter);

            Console.WriteLine();
            Console.WriteLine("The maximizer of the objective function is:");
            Console.WriteLine(maximizer);

            Console.WriteLine();

            Console.WriteLine("The maximum value of the objective function is:");
            Console.WriteLine(objectiveFunction(maximizer));
        }
    }
}