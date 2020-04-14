using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class ContinuousOptimizationExample1 : ICodeExample
    {
        public void Main()
        {
            // Define the parametric objective function as follows: 
            // f(x, a) = exp(-(x-2)^2) + (a) * exp(-(x+2)^2)).
            static double objectiveFunction(DoubleMatrix x, double alpha)
            {
                double y = 0.0;
                var x_0 = x[0];
                y += Math.Exp(-Math.Pow(x_0 - 2.0, 2.0));
                y += alpha * Math.Exp(-Math.Pow(x_0 + 2.0, 2.0));

                return y;
            }

            // Define the argument at which the method starts 
            // the search for optimality.
            var initialArgument = DoubleMatrix.Dense(1, 1, -6.0);

            // Define the function parameter.
            double functionParameter = .8;

            // Maximize the objective function.
            var maximizer = ContinuousOptimization.Maximize(
                objectiveFunction: objectiveFunction,
                initialArgument: initialArgument,
                functionParameter: functionParameter);

            // Show the results.
            Console.WriteLine();
            Console.WriteLine("The maximizer of the objective function is:");
            Console.WriteLine(maximizer);

            Console.WriteLine();

            Console.WriteLine("The maximum value of the objective function is:");
            Console.WriteLine(objectiveFunction(maximizer, functionParameter));
        }
    }
}