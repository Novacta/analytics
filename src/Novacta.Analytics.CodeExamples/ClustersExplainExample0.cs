using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class ClustersExplainExample0 : ICodeExample
    {
        public void Main()
        {
            // Set the number of items and features under study.
            const int numberOfItems = 12;
            int numberOfFeatures = 7;

            // Define a partition that must be explained.
            // Three parts (clusters) are included,
            // containing, respectively, items 0 to 3,
            // 5 to 8, and 9 to 11.
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

            // Define how many features must be selected
            // for explanation.
            int numberOfExplanatoryFeatures = 2;

            // Select the best features.
            IndexCollection optimalExplanatoryFeatureIndexes =
                Clusters.Explain(
                    data,
                    partition,
                    numberOfExplanatoryFeatures);

            // Show the results.
            Console.WriteLine();
            Console.WriteLine(
                "The {0} features best explaining the given partition have column indexes:",
                numberOfExplanatoryFeatures);
            Console.WriteLine(optimalExplanatoryFeatureIndexes);

            Console.WriteLine();
            Console.WriteLine("The Davies-Bouldin Index for the selected features:");
            var dbi = IndexPartition.DaviesBouldinIndex(
                data[":", optimalExplanatoryFeatureIndexes],
                partition);
            Console.WriteLine(dbi);
        }
    }
}