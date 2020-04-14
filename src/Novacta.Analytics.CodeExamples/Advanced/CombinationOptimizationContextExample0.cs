using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class CombinationOptimizationContextExample0 : ICodeExample
    {
        public void Main()
        {
            // Set the number of items and features under study.
            const int numberOfItems = 12;
            int numberOfFeatures = 7;

            // Define a partition that must be explained.
            // Three parts (clusters) are included,
            // containing, respectively, items 0 to 3,
            // 4 to 7, and 8 to 11.
            var partition = IndexPartition.Create(
                new double[numberOfItems]
                    { 0 ,0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 });

            // Create a matrix that will represent
            // an artificial data set,
            // having 12 items (rows) and 7 features (columns).
            // This will store the observations which
            // explanation will be based on.
            var data = DoubleMatrix.Dense(
                numberOfRows: numberOfItems,
                numberOfColumns: numberOfFeatures);

            // The first 5 features are built to be almost
            // surely non informative, since they result
            // as samples drawn from a same distribution.
            var g = new GaussianDistribution(mu: 0, sigma: .01);
            for (int j = 0; j < 5; j++)
            {
                data[":", j] = g.Sample(sampleSize: numberOfItems);
            }

            // Features 5 to 6 are instead built to be informative,
            // since they are sampled from different distributions
            // while filling rows whose indexes are in different parts
            // of the partition to be explained.
            var partIdentifiers = partition.Identifiers;
            double mu = 1.0;
            for (int i = 0; i < partIdentifiers.Count; i++)
            {
                var part = partition[partIdentifiers[i]];
                int partSize = part.Count;
                g.Mu = mu;
                data[part, 5] = g.Sample(sampleSize: partSize);
                mu += 2.0;
                g.Mu = mu;
                data[part, 6] = g.Sample(sampleSize: partSize);
                mu += 2.0;
            }

            Console.WriteLine("The data set:");
            Console.WriteLine(data);

            // Define the selection problem as
            // the maximization of the Dunn Index.
            double objectiveFunction(DoubleMatrix x)
            {
                // An argument x has entries equal to one,
                // signaling that the corresponding features 
                // are selected at x. Otherwise, the entries
                // are zero.
                IndexCollection selected = x.FindNonzero();

                double performance =
                    IndexPartition.DunnIndex(
                        data: data[":", selected],
                        partition: partition);

                return performance;
            }

            var optimizationGoal = OptimizationGoal.Maximization;

            // Define how many features must be selected
            // for explanation.
            int numberOfExplanatoryFeatures = 2;

            // Create the required context.
            var context = new CombinationOptimizationContext(
                objectiveFunction: objectiveFunction,
                stateDimension: numberOfFeatures,
                combinationDimension: numberOfExplanatoryFeatures,
                probabilitySmoothingCoefficient: .8,
                optimizationGoal: optimizationGoal,
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

            IndexCollection optimalExplanatoryFeatureIndexes =
                results.OptimalState.FindNonzero();

            // Show the results.
            Console.WriteLine(
                "The Cross-Entropy optimizer has converged: {0}.",
                results.HasConverged);

            Console.WriteLine();
            Console.WriteLine("Initial guess parameter:");
            Console.WriteLine(context.InitialParameter);

            Console.WriteLine();
            Console.WriteLine("The maximizer of the performance is:");
            Console.WriteLine(results.OptimalState);

            Console.WriteLine();
            Console.WriteLine(
                "The {0} features best explaining the given partition have column indexes:",
                numberOfExplanatoryFeatures);
            Console.WriteLine(optimalExplanatoryFeatureIndexes);

            Console.WriteLine();
            Console.WriteLine("The maximum performance is:");
            Console.WriteLine(results.OptimalPerformance);

            Console.WriteLine();
            Console.WriteLine("This is the Dunn Index for the selected features:");
            var di = IndexPartition.DunnIndex(
                data[":", optimalExplanatoryFeatureIndexes],
                partition);
            Console.WriteLine(di);
        }
    }
}