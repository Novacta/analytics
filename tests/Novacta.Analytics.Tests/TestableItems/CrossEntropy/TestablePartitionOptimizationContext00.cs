// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{
    /// <summary>
    /// Provides methods to test
    /// a Cross-Entropy context for selecting 
    /// the best 2 features among the available 7 
    /// (in the sense of a Davies–Bouldin index minimization),
    /// aimed to explain the specified 
    /// partition of a given collection of items.
    /// </summary>
    class TestablePartitionOptimizationContext00 :
        TestablePartitionOptimizationContext
    {
        private static readonly DoubleMatrix data;
        private static readonly IndexPartition<double> partition;

        static TestablePartitionOptimizationContext00()
        {
            const int numberOfItems = 12;
            const int numberOfFeatures = 7;

            var target = DoubleMatrix.Dense(numberOfItems, 1,
                [0, 0, 0, 0, 2, 2, 2, 2, 1, 1, 1, 1]);

            partition = IndexPartition.Create(target);
            data = DoubleMatrix.Dense(numberOfItems, numberOfFeatures);

            double mu = 1.0;

            var partIdentifiers = partition.Identifiers;

            for (int i = 0; i < partIdentifiers.Count; i++)
            {
                var part = partition[partIdentifiers[i]];
                int partSize = part.Count;
                for (int j = 0; j < partSize; j++)
                {
                    data[part[j], ":"] += mu;
                }
                mu += 5.0;
            }
        }

        // Define the performance function of the 
        // system under study (in this context, 
        // the sum of squared errors corresponding
        // to the partition represented by 
        // parameter x.
        static double Performance(DoubleMatrix x)
        {
            double performance = 0.0;
            var partition = IndexPartition.Create(x);

            foreach (double category in partition.Identifiers)
            {
                performance += Stat.Sum(
                    Stat.SumOfSquaredDeviations(
                        data[partition[category], ":"],
                        DataOperation.OnColumns));
            }

            return performance;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestablePartitionOptimizationContext00" /> class.
        /// </summary>
        TestablePartitionOptimizationContext00() : base(
            context: new PartitionOptimizationContext(
                objectiveFunction: Performance,
                stateDimension: 12,
                partitionDimension: 3,
                probabilitySmoothingCoefficient: .8,
                optimizationGoal: OptimizationGoal.Minimization,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000),
            objectiveFunction: Performance,
            stateDimension: 12,
            eliteSampleDefinition: EliteSampleDefinition.LowerThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Minimization,
            initialParameter: DoubleMatrix.Dense(3, 12, 1.0 / 3.0),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 1000,
            optimalState: DoubleMatrix.Dense(1, 12,
                [0, 0, 0, 0, 2, 2, 2, 2, 1, 1, 1, 1]),
            optimalPerformance: Performance(DoubleMatrix.Dense(1, 12,
                [0, 0, 0, 0, 2, 2, 2, 2, 1, 1, 1, 1])),
            partitionDimension: 3,
            probabilitySmoothingCoefficient: .8)
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestablePartitionOptimizationContext00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestablePartitionOptimizationContext00"/> class.</returns>
        public static TestablePartitionOptimizationContext00 Get()
        {
            return new TestablePartitionOptimizationContext00();
        }
    }
}
