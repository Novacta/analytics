using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;
using Novacta.Analytics.Advanced;
using System.Linq;

namespace Novacta.Analytics.CodeExamples.Advanced
{
    public class CrossEntropyEstimatorExample0 : ICodeExample
    {
        class RareShortestPathProbabilityEstimation
            : RareEventProbabilityEstimationContext
        {
            public RareShortestPathProbabilityEstimation()
                : base(
                    // There are 5 edges in the network under study.
                    // Hence we need a vector of length 5 to represent
                    // the state of the system, i.e. the observed lengths 
                    // corresponding to such edges.
                    stateDimension: 5,
                    // Set the threshold level and the rare event 
                    // boundedness in order to target the probability 
                    // of the event {2.0 <= Performance(x)}.
                    thresholdLevel: 2.0,
                    rareEventPerformanceBoundedness:
                        RareEventPerformanceBoundedness.Lower,
                    // Define the nominal parameter of interest.
                    initialParameter: DoubleMatrix.Dense(1, 5,
                        new double[] { 0.25, 0.4, 0.1, 0.3, 0.2 })
                      )
            {
            }

            // Define the performance function of the 
            // system under study.
            protected override double Performance(DoubleMatrix x)
            {
                // Compute the lengths of the possible 
                // paths when the state of the system is x.
                DoubleMatrix paths = DoubleMatrix.Dense(4, 1);
                paths[0] = x[0] + x[3];
                paths[1] = x[0] + x[2] + x[4];
                paths[2] = x[1] + x[4];
                paths[3] = x[1] + x[2] + x[3];

                // Compute the shortest path.
                var indexValuePair = Stat.Min(paths);

                // Return the shortest path.
                return indexValuePair.Value;
            }

            // Set how to sample system states
            // given the specified parameter. 
            protected override void PartialSample(
                double[] destinationArray,
                Tuple<int, int> sampleSubsetRange,
                RandomNumberGenerator randomNumberGenerator,
                DoubleMatrix parameter,
                int sampleSize)
            {
                // Must be Item1 included, Item2 excluded
                int subSampleSize = sampleSubsetRange.Item2 - sampleSubsetRange.Item1;
                int leadingDimension = Convert.ToInt32(sampleSize);
                for (int j = 0; j < this.StateDimension; j++)
                {
                    var distribution = new ExponentialDistribution(1.0 / parameter[j])
                    {
                        RandomNumberGenerator = randomNumberGenerator
                    };
                    distribution.Sample(
                         subSampleSize,
                         destinationArray, j * leadingDimension + sampleSubsetRange.Item1);
                }
            }

            // Define the Likelihood ratio for the 
            // problem under study.
            protected override double GetLikelihoodRatio(
                DoubleMatrix samplePoint,
                DoubleMatrix nominalParameter,
                DoubleMatrix referenceParameter)
            {
                var x = samplePoint;
                var u = nominalParameter;
                var v = referenceParameter;
                var prod = 1.0;
                var sum = 0.0;
                for (int j = 0; j < x.Count; j++)
                {
                    prod *= v[j] / u[j];
                    sum += x[j] * (1.0 / u[j] - 1.0 / v[j]);
                }

                return Math.Exp(-sum) * prod;
            }

            // Set how to update the parameter via 
            // the elite sample.
            protected override DoubleMatrix UpdateParameter(
                LinkedList<DoubleMatrix> parameters,
                DoubleMatrix eliteSample)
            {
                var nominalParameter = parameters.First.Value;
                var referenceParameter = parameters.Last.Value;

                var ratios = DoubleMatrix.Dense(1, eliteSample.NumberOfRows);

                var u = nominalParameter;
                var w = referenceParameter;
                for (int i = 0; i < eliteSample.NumberOfRows; i++)
                {
                    var x = eliteSample[i, ":"];
                    ratios[i] = this.GetLikelihoodRatio(x, u, w);
                }

                var newParameter = (ratios * eliteSample);
                var sumOfRatios = Stat.Sum(ratios);
                newParameter.InPlaceApply(d => d / sumOfRatios);

                return newParameter;
            }
        }

        public void Main()
        {
            // Create the context.
            var context = new RareShortestPathProbabilityEstimation();

            // Create the estimator.
            var estimator = new RareEventProbabilityEstimator()
            {
                PerformanceEvaluationParallelOptions = { MaxDegreeOfParallelism = 1 },
                SampleGenerationParallelOptions = { MaxDegreeOfParallelism = 1 }
            };

            // Set estimation parameters.
            double rarity = 0.1;
            int sampleSize = 1000;
            int finalSampleSize = 10000;

            // Solve the problem.
            var results = estimator.Estimate(
                context,
                rarity,
                sampleSize,
                finalSampleSize);

            // Show the results.
            Console.WriteLine("Under the nominal parameter:");
            Console.WriteLine(context.InitialParameter);
            Console.WriteLine("the estimated probability of observing");
            Console.WriteLine("a shortest path greater than 2.0 is:");
            Console.WriteLine(results.RareEventProbability);

            Console.WriteLine();
            Console.WriteLine("Details on iterations:");

            var info = DoubleMatrix.Dense(
                -1 + results.Parameters.Count,
                1 + results.Parameters.Last.Value.Count);

            info.SetColumnName(0, "Level");
            for (int j = 1; j < info.NumberOfColumns; j++)
            {
                info.SetColumnName(j, "Param" + (j - 1).ToString());
            }

            int i = 0;
            foreach (var level in results.Levels)
            {
                info[i++, 0] = level;
            }

            var referenceParameters = results.Parameters.Skip(1).ToList();
            var paramIndexes = IndexCollection.Range(1, info.NumberOfColumns - 1);
            for (i = 0; i < info.NumberOfRows; i++)
            {
                info[i, paramIndexes] = referenceParameters[i];
            }

            Console.WriteLine();
            Console.WriteLine(info);
        }
    }
}