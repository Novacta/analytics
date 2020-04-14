// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class RandomIndexPermutationTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var indexes = IndexCollection.Default(lastIndex: 5);

                var randomIndexPermutation = new
                    RandomIndexPermutation(indexes: indexes);

                int i = 0;
                foreach (var index in randomIndexPermutation.Indexes)
                {
                    Assert.AreEqual(
                        expected: indexes[i],
                        actual: index);
                    i++;
                }
            }

            // indexes is null
            {
                IndexCollection indexes = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new RandomIndexPermutation(
                                indexes: indexes);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "indexes");
            }
        }

        /// <summary>
        /// Provides methods to test that method
        /// <see cref="RandomIndexPermutation.Next"/> 
        /// has been properly implemented.
        /// </summary>
        static class Next
        {
            /// <summary>
            /// Tests that method
            /// <see cref="RandomIndexPermutation.Next"/> 
            /// terminates successfully as expected.
            /// </summary>
            /// <param name="indexes">
            /// The indexes to permute.
            /// </param>
            /// <param name="numberOfRandomPermutations">
            /// The number of permutations to draw.
            /// </param>
            /// <param name="criticalValue">
            /// A quantile of the chi-squared distribution with a number of
            /// degrees of freedom equal to the <see cref="IndexCollection.Count"/>
            /// of <paramref name="indexes"/>
            /// minus <c>1</c>. 
            /// To serve as the critical value for the Pearson's 
            /// chi-squared test whose null hypothesis assume that the 
            /// the distinct possible permutations 
            /// are equiprobable.
            /// </param>
            /// <param name="delta">The required accuracy.
            /// Defaults to <c>.01</c>.</param>
            public static void Succeed(
                IndexCollection indexes,
                int numberOfRandomPermutations,
                double criticalValue,
                double delta = .01)
            {
                var randomPermutation = new RandomIndexPermutation(indexes);

                // Generate permutations

                var permutations = new IndexCollection[numberOfRandomPermutations];

                for (int i = 0; i < numberOfRandomPermutations; i++)
                {
                    permutations[i] = randomPermutation.Next();
                }

                // Check the number of distinct generated permutations

                var permutationIdentifiers =
                    IndexCollection.Default(numberOfRandomPermutations - 1);

                var actualDistinctPermutations =
                    IndexPartition.Create(
                        permutationIdentifiers,
                        (i) => { return permutations[i]; });

                int numberOfActualDistinctPermutations =
                    actualDistinctPermutations.Count;

                Assert.AreEqual(
                    expected: SpecialFunctions.Factorial(indexes.Count),
                    actual: numberOfActualDistinctPermutations);

                // Compute the actual permutation probabilities

                DoubleMatrix actualPermutationProbabilities =
                    DoubleMatrix.Dense(
                        numberOfActualDistinctPermutations, 1);

                int j = 0;
                foreach (var identifier in actualDistinctPermutations.Identifiers)
                {
                    actualPermutationProbabilities[j] =
                        (double)actualDistinctPermutations[identifier].Count
                        /
                        (double)numberOfRandomPermutations;
                    j++;
                }

                // Check that the Chebyshev Inequality holds true
                // for each permutation probability

                var expectedPermutationProbabilities =
                    DoubleMatrix.Dense(
                        numberOfActualDistinctPermutations,
                        1,
                        1.0 
                        / 
                        (double)numberOfActualDistinctPermutations);

                for (int i = 0; i < numberOfActualDistinctPermutations; i++)
                {
                    ProbabilityDistributionTest.CheckChebyshevInequality(
                         new BernoulliDistribution(expectedPermutationProbabilities[i]),
                         actualPermutationProbabilities[i],
                         numberOfRandomPermutations,
                         delta);
                }

                // Check how good the actual permutation probabilities fit 
                // the expected ones

                ProbabilityDistributionTest.CheckGoodnessOfFit(
                    expectedPermutationProbabilities,
                    actualPermutationProbabilities,
                    criticalValue);
            }
        }

        [TestMethod()]
        public void NextTest()
        {
            // In what follows we are sampling
            // permutations of 5 indexes.
            // Their number is 5! = 120.
            // The quantile of order .9 for
            // the Chi Squared distribution having 120-1
            // degrees of freedom is 139.1495
            // (as from R function qchisq(.9,119))

            int[] indexesArray = new int[] { 2, 4, 10, 8, 3 };
            var indexes = IndexCollection.FromArray(indexesArray);

            int numberOfRandomPermutations = 12000;

            double criticalValue = 139.1495;

            Next.Succeed(
                indexes: indexes,
                numberOfRandomPermutations: numberOfRandomPermutations,
                criticalValue: criticalValue);
        }
    }
}