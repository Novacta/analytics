using Novacta.Documentation.CodeExamples;
using System;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class PartitionOptimizationContextExample0 : ICodeExample
    {
        public void Main()
        {
            // Set the number of items and features under study.
            const int numberOfItems = 12;
            int numberOfFeatures = 7;

            // Create a matrix that will represent
            // an artificial data set,
            // having 12 items (rows) and 7 features (columns).
            // This will store the observations which
            // partition discovery will be based on.
            var data = DoubleMatrix.Dense(
                numberOfRows: numberOfItems,
                numberOfColumns: numberOfFeatures);

            // Fill the data rows by sampling from a different 
            // distribution while, respectively, drawing observations 
            // for items 0 to 3, 4 to 7, and 8 to 11: these will be the 
            // three different parts expected to be included in the 
            // optimal partition.
            double mu = 1.0;
            var g = new GaussianDistribution(mu: mu, sigma: .01);

            IndexCollection range = IndexCollection.Range(0, 3);
            for (int j = 0; j < numberOfFeatures; j++)
            {
                data[range, j] = g.Sample(sampleSize: range.Count);
            }

            mu += 5.0;
            g.Mu = mu;
            range = IndexCollection.Range(4, 7);
            for (int j = 0; j < numberOfFeatures; j++)
            {
                data[range, j] = g.Sample(sampleSize: range.Count);
            }

            mu += 5.0;
            g.Mu = mu;
            range = IndexCollection.Range(8, 11);
            for (int j = 0; j < numberOfFeatures; j++)
            {
                data[range, j] = g.Sample(sampleSize: range.Count);
            }

            Console.WriteLine("The data set:");
            Console.WriteLine(data);

            // Define the optimization problem as
            // the minimization of the Davies-Bouldin Index
            // of a candidate partition.
            double objectiveFunction(DoubleMatrix x)
            {
                // An argument x has 12 entries, each belonging
                // to the set {0,...,k-1}, where k is the
                // maximum number of allowed parts, so
                // x[j]==i signals that, at x, item j
                // has been assigned to part i.
                IndexPartition<double> selected =
                    IndexPartition.Create(x);

                var performance = IndexPartition.DaviesBouldinIndex(
                    data: data,
                    partition: selected);

                return performance;
            }

            var optimizationGoal = OptimizationGoal.Minimization;

            // Define the maximum number of parts allowed in the
            // partition to be discovered.
            int maximumNumberOfParts = 3;

            // Create the required context.
            var context = new PartitionOptimizationContext(
                objectiveFunction: objectiveFunction,
                stateDimension: numberOfItems,
                partitionDimension: maximumNumberOfParts,
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
            int sampleSize = 2000;

            // Solve the problem.
            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            IndexPartition<double> optimalPartition =
                IndexPartition.Create(results.OptimalState);

            // Show the results.
            Console.WriteLine(
                "The Cross-Entropy optimizer has converged: {0}.",
                results.HasConverged);

            Console.WriteLine();
            Console.WriteLine("Initial guess parameter:");
            Console.WriteLine(context.InitialParameter);

            Console.WriteLine();
            Console.WriteLine("The minimizer of the performance is:");
            Console.WriteLine(results.OptimalState);

            Console.WriteLine();
            Console.WriteLine(
                "The optimal partition is:");
            Console.WriteLine(optimalPartition);

            Console.WriteLine();
            Console.WriteLine("The minimum performance is:");
            Console.WriteLine(results.OptimalPerformance);

            Console.WriteLine();
            Console.WriteLine("The Dunn Index for the optimal partition is:");
            var di = IndexPartition.DunnIndex(
                data,
                optimalPartition);
            Console.WriteLine(di);
        }
    }
}