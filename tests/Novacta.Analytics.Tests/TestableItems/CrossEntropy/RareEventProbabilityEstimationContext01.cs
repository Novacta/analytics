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
    /// as { x < -4 }, under a nominal 
    /// Gaussian Standard distribution.
    /// </summary>
    class RareEventProbabilityEstimationContext01
    : RareEventProbabilityEstimationContext
    {
        public RareEventProbabilityEstimationContext01(
            int stateDimension,
            double thresholdLevel,
            RareEventPerformanceBoundedness rareEventPerformanceBoundedness,
            DoubleMatrix initialParameter)
            : base(
                // The target probability is Pr{ x <= -4 } for
                // real x, hence the dimension is 1. 
                stateDimension: stateDimension,
                // Set the threshold level and the rare event 
                // boundedness in order to target the probability 
                // of the event { x <= -4 }.
                thresholdLevel: thresholdLevel,
                rareEventPerformanceBoundedness:
                    rareEventPerformanceBoundedness,
                // Define the nominal parameter of interest.
                // (Gaussian Standard distribution).
                initialParameter: initialParameter
                  )
        {
        }

        public RareEventProbabilityEstimationContext01()
            : base(
                // The target probability is Pr{ x <= -4 } for
                // real x, hence the dimension is 1. 
                stateDimension: 1,
                // Set the threshold level and the rare event 
                // boundedness in order to target the probability 
                // of the event { x <= -4 }.
                thresholdLevel: -4.0,
                rareEventPerformanceBoundedness:
                    RareEventPerformanceBoundedness.Upper,
                // Define the nominal parameter of interest
                // (Gaussian Standard distribution).
                initialParameter: DoubleMatrix.Dense(2, 1,
                    new double[] { 0, 1 })
                  )
        {
        }

        // Define the performance function of the 
        // system under study
        // (The identity function, so that
        // { H(x) <= -4 } == { x <= -4 }).
        protected internal override double Performance(DoubleMatrix x)
        {
            return x[0];
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
            var distribution = new GaussianDistribution(
                parameter[0], parameter[1])
            {
                RandomNumberGenerator = randomNumberGenerator
            };
            distribution.Sample(
                 subSampleSize,
                 destinationArray, sampleSubsetRange.Item1);
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

            var ug = new GaussianDistribution(u[0], u[1]);
            var vg = new GaussianDistribution(v[0], v[1]);

            return ug.Pdf(x[0]) / vg.Pdf(x[0]);
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

            var sumOfRatios = Stat.Sum(ratios);

            var r_by_e = (ratios * eliteSample);
            var newMu = (double)r_by_e / sumOfRatios;

            var newParameter = DoubleMatrix.Dense(2, 1,
                new double[] { newMu, 1.0 });

            return newParameter;
        }
    }
}