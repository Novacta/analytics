// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Sampling;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class SimpleRandomSamplingTests
    {
        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                int populationSize = 9;

                int sampleSize = 4;

                var randomSampling = new SimpleRandomSampling(
                    populationSize: populationSize, 
                    sampleSize: sampleSize);

                Assert.AreEqual(
                    expected: populationSize,
                    actual: randomSampling.PopulationSize);
                Assert.AreEqual(
                    expected: sampleSize,
                    actual: randomSampling.SampleSize);
            }

            // populationSize <= 1
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                int populationSize = 1;

                int sampleSize = 4;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = new SimpleRandomSampling(
                            populationSize: populationSize,
                            sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: "populationSize");
            }

            // sampleSize <= 0
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                int populationSize = 9;

                int sampleSize = 0;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = new SimpleRandomSampling(
                            populationSize: populationSize,
                            sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "sampleSize");
            }

            // populationSize <= sampleSize
            {
                string STR_EXCEPT_PAR_MUST_BE_LESS_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_VALUE"),
                        "the population size");

                int populationSize = 9;

                int sampleSize = 9;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = new SimpleRandomSampling(
                            populationSize: populationSize,
                            sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_LESS_THAN_VALUE,
                    expectedParameterName: "sampleSize");
            }
        }

        #endregion

        [TestMethod()]
        public void GetInclusionProbabilityTest()
        {
            RandomSamplingTest.InclusionProbabilities.Succeed(
                TestableSimpleRandomSampling00.Get(),
                RandomSamplingTest.Accuracy);
        }

        [TestMethod()]
        public void NextIndexCollectionTest()
        {
            RandomSamplingTest.NextIndexCollection.Succeed(
                TestableSimpleRandomSampling00.Get(),
                // TestableSimpleRandomSampling00
                // represents a simple random sampling of
                // 4 units
                // from a population having 9 units.
                // The number of distinct samples is 126
                numberOfSamples: 126 * 100,
                RandomSamplingTest.Accuracy);
        }

        [TestMethod()]
        public void NextDoubleMatrixTest()
        {
            RandomSamplingTest.NextDoubleMatrix.Succeed(
                TestableSimpleRandomSampling00.Get(),
                // TestableSimpleRandomSampling00
                // represents a simple random sampling of
                // 4 units
                // from a population having 9 units.
                // The number of distinct samples is 126
                numberOfSamples: 126 * 100,
                RandomSamplingTest.Accuracy);
        }
    }
}
