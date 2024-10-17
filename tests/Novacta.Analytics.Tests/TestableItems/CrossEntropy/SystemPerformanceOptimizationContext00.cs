﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.CrossEntropy
{

    /// <summary>
    /// Represents
    /// a Cross-Entropy context for minimizing 
    /// the bi-dimensional Rosenbrock function.
    /// </summary>
    class SystemPerformanceOptimizationContext00
    : SystemPerformanceOptimizationContext
    {
        public SystemPerformanceOptimizationContext00(
            int stateDimension,
            OptimizationGoal optimizationGoal,
            DoubleMatrix initialParameter,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations)
    : base(
        // The function to be optimized is the bi-dimensional
        // Rosenbrock function, hence it has two arguments.
        // Thus it can be interpreted as the performance of
        // a system whose state can be represented as a vector
        // of length 2.
        stateDimension: stateDimension,
        // Set the optimization goal to minimization.
        optimizationGoal: optimizationGoal,
        // Define the initial parameter of the distribution
        // from which samples are drawn while searching
        // for the optimizer (sampling the state-space
        // of the system, i.e. the domain of the function).
        // The parameter is a matrix of two rows.
        // Its first row represents the means, its second one 
        // the standard deviations of two Gaussian distributions
        // from which the two arguments of the optimizing function,
        // respectively, are sampled while searching 
        // for the optimizer.
        initialParameter: initialParameter,
        minimumNumberOfIterations: minimumNumberOfIterations,
        maximumNumberOfIterations: maximumNumberOfIterations)
        {
        }

        public SystemPerformanceOptimizationContext00()
            : base(
                // The function to be optimized is the bi-dimensional
                // Rosenbrock function, hence it has two arguments.
                // Thus it can be interpreted as the performance of
                // a system whose state can be represented as a vector
                // of length 2.
                stateDimension: 2,
                // Set the optimization goal to minimization.
                optimizationGoal: OptimizationGoal.Minimization,
                // Define the initial parameter of the distribution
                // from which samples are drawn while searching
                // for the optimizer (sampling the state-space
                // of the system, i.e. the domain of the function).
                // The parameter is a matrix of two rows.
                // Its first row represents the means, its second one 
                // the standard deviations of two Gaussian distributions
                // from which the two arguments of the optimizing function,
                // respectively, are sampled while searching 
                // for the optimizer.
                initialParameter: DoubleMatrix.Dense(2, 2,
                    [-1.0, 10000.0, -1.0, 10000.0]),
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 10000)
        {
        }

        // Define the performance function of the 
        // system under study (in this context, 
        // a bi-dimensional Rosenbrock function to be minimized).
        protected internal override double Performance(DoubleMatrix x)
        {
            double performance = 0.0;
            for (int i = 0; i < this.StateDimension - 1; i++)
            {
                var x_i = x[i];
                var squared_x_i = Math.Pow(x[i], 2.0);
                performance += 100.0 * (Math.Pow(x[i + 1] - squared_x_i, 2.0));
                performance += Math.Pow(1.0 - x_i, 2.0);
            }
            return performance;
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
            int subSampleSize =
                sampleSubsetRange.Item2 - sampleSubsetRange.Item1;

            int leadingDimension = Convert.ToInt32(sampleSize);

            for (int j = 0; j < this.StateDimension; j++)
            {
                var distribution = new
                    GaussianDistribution(
                        mu: parameter[0, j],
                        sigma: parameter[1, j])
                {
                    RandomNumberGenerator = randomNumberGenerator
                };
                distribution.Sample(
                     subSampleSize,
                     destinationArray, j * leadingDimension + sampleSubsetRange.Item1);
            }
        }

        // Define how to get the optimal state given
        // a Cross-Entropy parameter.
        protected internal override DoubleMatrix GetOptimalState(
            DoubleMatrix parameter)
        {
            // The optimal state is the vector of
            // means.
            return parameter[0, ":"];
        }

        // Set how to update the parameter via 
        // the elite sample.
        protected internal override DoubleMatrix UpdateParameter(
            LinkedList<DoubleMatrix> parameters,
            DoubleMatrix eliteSample)
        {
            var newParameter = DoubleMatrix.Dense(2, this.StateDimension);
            newParameter[0, ":"] =
                Stat.Mean(
                    data: eliteSample,
                    dataOperation: DataOperation.OnColumns);
            newParameter[1, ":"] =
                Stat.StandardDeviation(
                    data: eliteSample,
                    dataOperation: DataOperation.OnColumns,
                    adjustForBias: false);

            return newParameter;
        }

        // Provide a smoothing scheme for updated sampling
        // parameters.
        protected internal override void SmoothParameter(
            LinkedList<DoubleMatrix> parameters)
        {
            double iteration = Convert.ToDouble(parameters.Count);
            if (iteration > 1)
            {
                DoubleMatrix currentParameter = parameters.Last.Value;
                DoubleMatrix previousParameter = parameters.Last.Previous.Value;

                // Smoothing means
                double meanAlpha = .7;
                var previousMeans = previousParameter[0, ":"];
                var currentMeans = currentParameter[0, ":"];
                currentParameter[0, ":"] =
                    meanAlpha * currentMeans + (1.0 - meanAlpha) * previousMeans;

                // Smoothing standard deviations
                var previousStdDevs = previousParameter[1, ":"];
                var currentStdDevs = currentParameter[1, ":"];
                double q = 6.0;
                double beta = .9;
                double stdDevAlpha = beta * (1.0 - Math.Pow(1.0 - 1.0 / iteration, q));

                currentParameter[1, ":"] =
                    stdDevAlpha * currentStdDevs + (1.0 - stdDevAlpha) * previousStdDevs;
            }
        }

        protected internal override bool StopAtIntermediateIteration(
            int iteration,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            // Stop the program when each 
            // standard deviation is less than .05.
            var stdDevs = parameters.Last.Value[1, ":"];
            for (int j = 0; j < stdDevs.Count; j++)
            {
                if (stdDevs[j] >= .05)
                {
                    return false;
                }
            }

            return true;
        }
    }
}