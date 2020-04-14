using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class ClustersDiscoverExample0 : ICodeExample
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

            // Define the maximum number of parts allowed in the
            // partition to be discovered.
            int maximumNumberOfParts = 3;

            // Select the best partition.
            IndexPartition<double> optimalPartition =
                Clusters.Discover(
                    data,
                    maximumNumberOfParts);

            // Show the results.
            Console.WriteLine();
            Console.WriteLine(
                "The optimal partition:");
            Console.WriteLine(optimalPartition);

            Console.WriteLine();
            Console.WriteLine("The Davies-Bouldin Index for the optimal partition:");
            var dbi = IndexPartition.DaviesBouldinIndex(
                data,
                optimalPartition);
            Console.WriteLine(dbi);
        }
    }
}