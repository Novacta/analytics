// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{

    /// <summary>
    /// Represents
    /// a Cross-Entropy context for estimating 
    /// the probability of the rare event described
    /// in Section 2.2.1 of Rubinstein and Kroese,
    /// The Cross-Entropy Method, 2004.
    /// </summary>
    class RareEventProbabilityEstimationContext00
    : RareEventProbabilityEstimationContext
    {
        public RareEventProbabilityEstimationContext00(
            int stateDimension,
            double thresholdLevel,
            RareEventPerformanceBoundedness rareEventPerformanceBoundedness,
            DoubleMatrix initialParameter)
            : base(
                // There are 5 edges in the network under study.
                // Hence we need a vector of length 5 to represent
                // the state of the system, i.e. the observed lengths 
                // corresponding to such edges.
                stateDimension: stateDimension,
                // Set the threshold level and the rare event 
                // boundedness in order to target the probability 
                // of the event {2.0 <= Performance(x)}.
                thresholdLevel: thresholdLevel,
                rareEventPerformanceBoundedness:
                    rareEventPerformanceBoundedness,
                // Define the nominal parameter of interest.
                initialParameter: initialParameter
                  )
        {
        }

        public RareEventProbabilityEstimationContext00()
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
        protected internal override double Performance(DoubleMatrix x)
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
        protected internal override void PartialSample(
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
        protected internal override double GetLikelihoodRatio(
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
        protected internal override DoubleMatrix UpdateParameter(
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
}