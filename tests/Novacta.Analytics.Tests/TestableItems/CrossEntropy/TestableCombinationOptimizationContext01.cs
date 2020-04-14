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
    /// (in the sense of a Dunn index maximization),
    /// aimed to explain the specified 
    /// partition of a given collection of items.
    /// </summary>
    class TestableCombinationOptimizationContext01 :
        TestableCombinationOptimizationContext
    {
        private static readonly DoubleMatrix data;
        private static readonly IndexPartition<double> partition;

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1810:Initialize reference type static fields inline",
            Justification = "Performance is not a concern.")]
        static TestableCombinationOptimizationContext01()
        {
            const int numberOfItems = 12;

            var target = DoubleMatrix.Dense(numberOfItems, 1,
                new double[numberOfItems]
                    { 0 ,0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 });

            partition = IndexPartition.Create(target);
            data = DoubleMatrix.Dense(numberOfItems, 7);

            // features 0 to 4
            var g = new GaussianDistribution(mu: 0, sigma: .1);
            for (int j = 0; j < 5; j++)
            {
                data[":", j] = g.Sample(sampleSize: numberOfItems);
            }

            var partIdentifiers = partition.Identifiers;

            // feature 5 to 6
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
        }

        // Define the performance function of the 
        // system under study (in this context, 
        // the Dunn index of the data subset
        // defined by the combination represented by 
        // parameter x.
        static double Performance(DoubleMatrix x)
        {
            IndexCollection selected = x.FindNonzero();

            double performance =
                IndexPartition.DunnIndex(
                    data: data[":", selected],
                    partition: partition);

            return performance;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableCombinationOptimizationContext01" /> class.
        /// </summary>
        TestableCombinationOptimizationContext01() : base(
            context: new CombinationOptimizationContext(
                objectiveFunction: Performance,
                stateDimension: 7,
                combinationDimension: 2,
                probabilitySmoothingCoefficient: .8,
                optimizationGoal: OptimizationGoal.Maximization,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000),
            objectiveFunction: Performance,
            stateDimension: 7,
            eliteSampleDefinition: EliteSampleDefinition.HigherThanLevel,
            traceExecution: false,
            optimizationGoal: OptimizationGoal.Maximization,
            initialParameter: DoubleMatrix.Dense(1, 7, .5),
            minimumNumberOfIterations: 3,
            maximumNumberOfIterations: 1000,
            optimalState: DoubleMatrix.Dense(1, 7,
                new double[7] { 0, 0, 0, 0, 0, 1, 1 }),
            optimalPerformance: Performance(DoubleMatrix.Dense(1, 7,
                new double[7] { 0, 0, 0, 0, 0, 1, 1 })),
            combinationDimension: 2,
            probabilitySmoothingCoefficient: .8)
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableCombinationOptimizationContext01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableCombinationOptimizationContext01"/> class.</returns>
        public static TestableCombinationOptimizationContext01 Get()
        {
            return new TestableCombinationOptimizationContext01();
        }
    }
}
