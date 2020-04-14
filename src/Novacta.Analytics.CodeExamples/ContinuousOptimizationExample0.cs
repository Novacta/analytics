using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class ContinuousOptimizationExample0 : ICodeExample
    {
        public void Main()
        {
            // Define the objective function as follows: 
            // f(x) = exp(-(x-2)^2) + (.8) * exp(-(x+2)^2)),
            // penalized for minimization if x is not in [-3, 3].
            static double objectiveFunction(DoubleMatrix x)
            {
                double y = 0.0;
                var x_0 = x[0];
                y += Math.Exp(-Math.Pow(x_0 - 2.0, 2.0));
                y += .8 * Math.Exp(-Math.Pow(x_0 + 2.0, 2.0));

                // Penalize arguments not in [-3, 3].
                if (x_0 < -3.0 || 3.0 < x_0)
                {
                    y += 100.0 * Math.Abs(x_0 - 3.0);
                }
                return y;
            }

            // Define the argument at which the method starts 
            // the search for optimality.
            var initialArgument = DoubleMatrix.Dense(1, 1, -3.0);

            // Minimize the objective function.
            var minimizer = ContinuousOptimization.Minimize(
                objectiveFunction: objectiveFunction,
                initialArgument: initialArgument);

            // Show the results.
            Console.WriteLine();
            Console.WriteLine("The minimizer of the objective function is:");
            Console.WriteLine(minimizer);

            Console.WriteLine();

            Console.WriteLine("The minimum value of the objective function is:");
            Console.WriteLine(objectiveFunction(minimizer));
        }
    }
}