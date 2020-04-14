// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="RandomSampling"/> 
    /// instances.
    /// </summary>
    static class RandomSamplingTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static RandomSamplingTest()
        {
            RandomSamplingTest.Accuracy = 1e-6;
        }

        #endregion

        #region Helpers 

        /// <summary>
        /// Tests the specified <see cref="Action"/> for each item in the 
        /// given list of <see cref="TestableRandomSampling"/> instances.
        /// </summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="testableItems">The list of 
        /// <see cref="TestableRandomSampling"/> instances 
        /// to test.</param>
        public static void TestAction(
            Action<TestableRandomSampling> test,
            List<TestableRandomSampling> testableItems)
        {
            for (int i = 0; i < testableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing random sampling {0}", i);
#endif
                test(testableItems[i]);
            }
        }

        /// <summary>Tests the specified sample <see cref="Action"/> for each item in the
        /// given list of <see cref="TestableRandomSampling"/> instances.</summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="testableItems">The list of
        /// <see cref="TestableRandomSampling"/> instances
        /// to test.</param>
        /// <param name="sampleSize">The size of the sample to test.</param>
        /// <param name="delta">The required accuracy in testing conditions.</param>
        public static void TestSampleAction(
            Action<TestableRandomSampling, int, double> test,
            List<TestableRandomSampling> testableItems,
            int sampleSize,
            double delta)
        {
            for (int i = 0; i < testableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing random sampling {0}", i);
#endif
                test(testableItems[i], sampleSize, delta);
            }
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="RandomSampling.PopulationSize"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class PopulationSize
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="RandomSampling.PopulationSize"/> 
            /// property terminates successfully when expected.
            public static void Succeed(
                TestableRandomSampling testableRandomSampling)
            {
                var randomSampling = testableRandomSampling.RandomSampling;

                Assert.AreEqual(
                    expected: testableRandomSampling.PopulationSize,
                    actual: randomSampling.PopulationSize);
            }
        }

        /// <summary>
        /// Provides methods to test that the
        /// <see cref="RandomSampling.SampleSize"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class SampleSize
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="RandomSampling.SampleSize"/> 
            /// property terminates successfully when expected.
            public static void Succeed(
                TestableRandomSampling testableRandomSampling)
            {
                var randomSampling = testableRandomSampling.RandomSampling;

                Assert.AreEqual(
                    expected: testableRandomSampling.SampleSize,
                    actual: randomSampling.SampleSize);
            }
        }

        /// <summary>
        /// Provides methods to test that method
        /// <see cref="RandomSampling.InclusionProbabilities"/> 
        /// has been properly implemented.
        /// </summary>
        public static class InclusionProbabilities
        {
            /// <summary>
            /// Tests that method
            /// <see cref="RandomSampling.InclusionProbabilities"/> 
            /// terminates successfully when expected.
            public static void Succeed(
                TestableRandomSampling testableRandomSampling,
                double delta)
            {
                var randomSampling = testableRandomSampling.RandomSampling;

                for (int i = 0; i < testableRandomSampling.PopulationSize; i++)
                {
                    Assert.AreEqual(
                        expected: testableRandomSampling.InclusionProbabilities[i],
                        actual: randomSampling.InclusionProbabilities[i],
                        delta);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that method
        /// <see cref="RandomSampling.NextIndexCollection"/> 
        /// has been properly implemented.
        /// </summary>
        internal static class NextIndexCollection
        {
            /// <summary>
            /// Tests that method
            /// <see cref="RandomSampling.NextIndexCollection"/> 
            /// terminates successfully as expected.
            /// </summary>
            /// <param name="testableRandomSampling">
            /// The testable random sampling providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            /// <param name="numberOfSamples">
            /// The number of samples to draw.
            /// </param>
            /// <param name="delta">The required accuracy.
            /// Defaults to <c>.01</c>.</param>
            public static void Succeed(
                TestableRandomSampling testableRandomSampling,
                int numberOfSamples,
                double delta = .01)
            {
                var randomSampling = testableRandomSampling.RandomSampling;

                // Generate samples

                var samples = new IndexCollection[numberOfSamples];

                for (int i = 0; i < numberOfSamples; i++)
                {
                    samples[i] = randomSampling.NextIndexCollection();
                    samples[i].Sort();
                }

                // Compute the actual inclusion probabilities

                DoubleMatrix actualInclusionProbabilities =
                    DoubleMatrix.Dense(randomSampling.PopulationSize, 1);

                var sampleIndexes = IndexCollection.Default(numberOfSamples - 1);

                for (int j = 0; j < randomSampling.PopulationSize; j++)
                {
                    var samplesContainingCurrentUnit =
                    IndexPartition.Create(
                        sampleIndexes,
                        (i) => { return samples[i].Contains(j); });

                    actualInclusionProbabilities[j] =
                        (double)samplesContainingCurrentUnit[true].Count
                        /
                        (double)numberOfSamples;
                }

                // Check the number of distinct generated samples

                var distinctSamples =
                    IndexPartition.Create(
                        sampleIndexes,
                        (i) => { return samples[i]; });

                int numberOfDistinctSamples =
                    distinctSamples.Count;

                Assert.AreEqual(
                    SpecialFunctions.BinomialCoefficient(
                        randomSampling.PopulationSize,
                        randomSampling.SampleSize),
                    numberOfDistinctSamples);

                // Check that the Chebyshev Inequality holds true
                // for each inclusion probability

                var expectedInclusionProbabilities =
                    testableRandomSampling.InclusionProbabilities;

                for (int j = 0; j < randomSampling.PopulationSize; j++)
                {
                    ProbabilityDistributionTest.CheckChebyshevInequality(
                         new BernoulliDistribution(expectedInclusionProbabilities[j]),
                         actualInclusionProbabilities[j],
                         numberOfSamples,
                         delta);
                }

                // Check how good the actual inclusion probabilities fit 
                // the expected ones

                ProbabilityDistributionTest.CheckGoodnessOfFit(
                    expectedInclusionProbabilities,
                    actualInclusionProbabilities,
                    testableRandomSampling.GoodnessOfFitCriticalValue);
            }
        }

        /// <summary>
        /// Provides methods to test that method
        /// <see cref="RandomSampling.NextDoubleMatrix"/> 
        /// has been properly implemented.
        /// </summary>
        public static class NextDoubleMatrix
        {
            /// <summary>
            /// Tests that method
            /// <see cref="RandomSampling.NextDoubleMatrix"/> 
            /// terminates successfully as expected.
            /// </summary>
            /// <param name="testableRandomSampling">
            /// The testable random sampling providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            /// <param name="numberOfSamples">
            /// The number of samples to draw.
            /// </param>
            /// <param name="delta">The required accuracy.
            /// Defaults to <c>.01</c>.</param>
            public static void Succeed(
                TestableRandomSampling testableRandomSampling,
                int numberOfSamples,
                double delta = .01)
            {
                var randomSampling = testableRandomSampling.RandomSampling;

                // Generate samples

                var samples = DoubleMatrix.Dense(
                    numberOfSamples,
                    randomSampling.PopulationSize);

                for (int i = 0; i < numberOfSamples; i++)
                {
                    samples[i, ":"] = randomSampling.NextDoubleMatrix();
                }

                // Compute the actual inclusion probabilities

                DoubleMatrix actualInclusionProbabilities =
                    DoubleMatrix.Dense(randomSampling.PopulationSize, 1);

                var sampleIndexes = IndexCollection.Default(numberOfSamples - 1);

                for (int j = 0; j < randomSampling.PopulationSize; j++)
                {
                    var samplesContainingUnit =
                    IndexPartition.Create(
                        sampleIndexes,
                        (i) => { return samples[i, j] == 1.0; });

                    actualInclusionProbabilities[j] =
                        (double)samplesContainingUnit[true].Count 
                        / 
                        (double)numberOfSamples;
                }

                // Check the number of distinct generated samples

                var distinctSamples =
                    IndexPartition.Create(samples.AsRowCollection());

                int numberOfDistinctSamples =
                    distinctSamples.Count;

                Assert.AreEqual(
                    SpecialFunctions.BinomialCoefficient(
                        randomSampling.PopulationSize,
                        randomSampling.SampleSize),
                    numberOfDistinctSamples);

                // Check that the Chebyshev Inequality holds true
                // for each inclusion probability

                var expectedInclusionProbabilities =
                    testableRandomSampling.InclusionProbabilities;

                for (int j = 0; j < randomSampling.PopulationSize; j++)
                {
                   ProbabilityDistributionTest.CheckChebyshevInequality(
                        new BernoulliDistribution(expectedInclusionProbabilities[j]),
                        actualInclusionProbabilities[j],
                        numberOfSamples,
                        delta);
                }

                // Check how good the actual inclusion probabilities fit 
                // the expected ones

                ProbabilityDistributionTest.CheckGoodnessOfFit(
                    expectedInclusionProbabilities,
                    actualInclusionProbabilities,
                    testableRandomSampling.GoodnessOfFitCriticalValue);
            }
        }
    }
}
