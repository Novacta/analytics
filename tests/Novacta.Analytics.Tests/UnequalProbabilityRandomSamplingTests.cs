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
    public class UnequalProbabilityRandomSamplingTests
    {
        #region Specific tests

        [TestMethod()]
        public void FromBernoulliProbabilitiesTest()
        {
            // Valid input
            {
                const int populationSize = 9;
                var bernoulliProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        .1, .2, .3, .4, .5, .6, .7, .8, .9 });

                int sampleSize = 4;

                var randomSampling = UnequalProbabilityRandomSampling.
                    FromBernoulliProbabilities(
                        bernoulliProbabilities: bernoulliProbabilities,
                        sampleSize: sampleSize);

                Assert.AreEqual(
                    expected: populationSize,
                    actual: randomSampling.PopulationSize);
                Assert.AreEqual(
                    expected: sampleSize,
                    actual: randomSampling.SampleSize);
            }

            // bernoulliProbabilities is null
            {
                DoubleMatrix bernoulliProbabilities = null;

                int sampleSize = 4;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromBernoulliProbabilities(
                                bernoulliProbabilities: bernoulliProbabilities,
                                sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "bernoulliProbabilities");
            }

            // populationSize <= 1
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                const int populationSize = 1;
                var bernoulliProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        .1 });

                int sampleSize = 4;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromBernoulliProbabilities(
                                bernoulliProbabilities: bernoulliProbabilities,
                                sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: "bernoulliProbabilities");
            }

            // At least an entry in bernoulliProbabilities is not positive
            {
                string STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL"),
                        "0", "1");

                const int populationSize = 9;
                var bernoulliProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        .1, .2, .3, .4, .5, .6, .7, .0, .9 });

                int sampleSize = 4;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromBernoulliProbabilities(
                                bernoulliProbabilities: bernoulliProbabilities,
                                sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: "bernoulliProbabilities");
            }

            // At least an entry in bernoulliProbabilities is not less than 1
            {
                string STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL"),
                        "0", "1");

                const int populationSize = 9;
                var bernoulliProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        .1, .2, .3, .4, .5, .6, .7, 1.0, .9 });

                int sampleSize = 4;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromBernoulliProbabilities(
                                bernoulliProbabilities: bernoulliProbabilities,
                                sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: "bernoulliProbabilities");
            }

            // sampleSize <= 0
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                const int populationSize = 9;
                var bernoulliProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        .1, .2, .3, .4, .5, .6, .7, .8, .9 });

                int sampleSize = 0;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromBernoulliProbabilities(
                                bernoulliProbabilities: bernoulliProbabilities,
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
                        "the count of parameter bernoulliProbabilities");

                const int populationSize = 9;
                var bernoulliProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        .1, .2, .3, .4, .5, .6, .7, .8, .9 });

                int sampleSize = populationSize;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromBernoulliProbabilities(
                                bernoulliProbabilities: bernoulliProbabilities,
                                sampleSize: sampleSize);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_LESS_THAN_VALUE,
                    expectedParameterName: "sampleSize");
            }
        }

        [TestMethod()]
        public void FromInclusionProbabilitiesTest()
        {
            // Valid input
            {
                const int populationSize = 9;
                var inclusionProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        0.0602933142691422,
                        0.130083877944215,
                        0.211283730466511,
                        0.305874041451126,
                        0.415127817757125,
                        0.537249360967418,
                        0.661978741628504,
                        0.782520969486053,
                        0.895588146029904 });

                int sampleSize = Convert.ToInt32(
                    Stat.Sum(inclusionProbabilities));

                var randomSampling = UnequalProbabilityRandomSampling.
                    FromInclusionProbabilities(
                        inclusionProbabilities: inclusionProbabilities);

                Assert.AreEqual(
                    expected: populationSize,
                    actual: randomSampling.PopulationSize);
                Assert.AreEqual(
                    expected: sampleSize,
                    actual: randomSampling.SampleSize);
            }

            // inclusionProbabilities is null
            {
                DoubleMatrix inclusionProbabilities = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromInclusionProbabilities(
                                inclusionProbabilities: inclusionProbabilities);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "inclusionProbabilities");
            }

            // Count of inclusionProbabilities <= 1
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                const int populationSize = 1;
                var inclusionProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        0.895588146029904 });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromInclusionProbabilities(
                                inclusionProbabilities: inclusionProbabilities);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: "inclusionProbabilities");
            }

            // At least an entry in inclusionProbabilities is not positive
            {
                string STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL"),
                        "0", "1");

                const int populationSize = 9;
                var inclusionProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        0.0602933142691422,
                        0.130083877944215,
                        0.211283730466511,
                        0.305874041451126,
                        0.415127817757125,
                        0.537249360967418,
                        0.661978741628504,
                        0.0,
                        0.895588146029904 });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromInclusionProbabilities(
                                inclusionProbabilities: inclusionProbabilities);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: "inclusionProbabilities");
            }

            // At least an entry in inclusionProbabilities is not less than 1
            {
                string STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL"),
                        "0", "1");

                const int populationSize = 9;
                var inclusionProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        0.0602933142691422,
                        0.130083877944215,
                        0.211283730466511,
                        0.305874041451126,
                        0.415127817757125,
                        0.537249360967418,
                        0.661978741628504,
                        1.0,
                        0.895588146029904 });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromInclusionProbabilities(
                                inclusionProbabilities: inclusionProbabilities);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: "inclusionProbabilities");
            }

            // Entries in inclusionProbabilities does not sum up to an integer
            {
                string STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_INTEGER =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_INTEGER");

                const int populationSize = 9;
                var inclusionProbabilities = DoubleMatrix.Dense(populationSize, 1,
                    new double[populationSize] {
                        0.0602933142691422,
                        0.130083877944215,
                        0.211283730466511 + .5,
                        0.305874041451126,
                        0.415127817757125,
                        0.537249360967418,
                        0.661978741628504,
                        0.782520969486053,
                        0.895588146029904 });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var randomSampling = UnequalProbabilityRandomSampling.
                            FromInclusionProbabilities(
                                inclusionProbabilities: inclusionProbabilities);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_INTEGER,
                    expectedParameterName: "inclusionProbabilities");
            }
        }

        #endregion

        [TestMethod()]
        public void InclusionProbabilitiesTest()
        {
            RandomSamplingTest.InclusionProbabilities.Succeed(
                TestableUnequalProbabilityRandomSampling00.Get(),
                RandomSamplingTest.Accuracy);

            RandomSamplingTest.InclusionProbabilities.Succeed(
                TestableUnequalProbabilityRandomSampling01.Get(),
                RandomSamplingTest.Accuracy);
        }

        [TestMethod()]
        public void NextIndexCollectionTest()
        {
            RandomSamplingTest.NextIndexCollection.Succeed(
                TestableUnequalProbabilityRandomSampling00.Get(),
                // TestableUnequalProbabilityRandomSampling00
                // represents an unequal probability 
                // random sampling of
                // 4 units
                // from a population having 9 units.
                // The number of distinct samples is 126
                numberOfSamples: 126 * 700,
                RandomSamplingTest.Accuracy);

            RandomSamplingTest.NextIndexCollection.Succeed(
                TestableUnequalProbabilityRandomSampling01.Get(),
                // TestableUnequalProbabilityRandomSampling01
                // represents an equal probability 
                // random sampling of
                // 4 units
                // from a population having 9 units.
                // The number of distinct samples is 126
                numberOfSamples: 126 * 700,
                RandomSamplingTest.Accuracy);
        }

        [TestMethod()]
        public void NextDoubleMatrixTest()
        {
            RandomSamplingTest.NextDoubleMatrix.Succeed(
                TestableUnequalProbabilityRandomSampling00.Get(),
                // TestableUnequalProbabilityRandomSampling00
                // represents an unequal probability 
                // random sampling of
                // 4 units
                // from a population having 9 units.
                // The number of distinct samples is 126
                numberOfSamples: 126 * 700,
                RandomSamplingTest.Accuracy);

            RandomSamplingTest.NextDoubleMatrix.Succeed(
                TestableUnequalProbabilityRandomSampling01.Get(),
                // TestableUnequalProbabilityRandomSampling00
                // represents an unequal probability 
                // random sampling of
                // 4 units
                // from a population having 9 units.
                // The number of distinct samples is 126
                numberOfSamples: 126 * 700,
                RandomSamplingTest.Accuracy);
        }
    }
}
