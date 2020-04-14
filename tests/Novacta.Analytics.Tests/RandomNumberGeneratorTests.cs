// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class RandomNumberGeneratorTests
    {
        #region Helpers

        /// <summary>
        /// Gets the list of 
        /// <see cref="RandomNumberGenerator"/> instances to test.
        /// </summary>
        /// <returns>The list of  <see cref="RandomNumberGenerator"/> instances
        /// to test.</returns>
        private static List<RandomNumberGenerator> GetRandomNumberGenerators()
        {
            return new List<RandomNumberGenerator>{
                RandomNumberGenerator.CreateMT19937(777777),
                RandomNumberGenerator.CreateSFMT19937(777777),
                RandomNumberGenerator.CreateNextMT2203(777777),
                RandomNumberGenerator.CreateNextMT2203(777777)};
        }

        #endregion

        #region Gaussian

        [TestMethod()]
        public void GaussianSamplePointTest()
        {
            #region Specific tests

            // Non positive sigma
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .Gaussian(
                                mu: 1.0,
                                sigma: 0.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "sigma");
            }

            #endregion

            #region Generic tests

            // Valid input
            {
                double mu = -1.1;
                double sigma = 2.2;
                double[] distributionParameters = new double[2] { mu, sigma };
                double distributionMean = mu;
                double distributionVariance = Math.Pow(sigma, 2.0);

                int sampleSize = 10000;
                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    double sampler(params double[] parameters)
                    {
                        return rng.Gaussian(parameters[0], parameters[1]);
                    };

                    RandomNumberGeneratorTest.SamplePoint.DoubleSucceed(
                        distributionMean,
                        distributionVariance,
                        sampleSize,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            #endregion

        }

        [TestMethod()]
        public void GaussianArrayTest()
        {
            #region Specific tests

            // Non positive sigma
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .Gaussian(
                                sampleSize: 1,
                                destinationArray: new double[10],
                                destinationIndex: 0,
                                mu: 1.0,
                                sigma: 0.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "sigma");
            }

            #endregion

            #region Generic tests

            double mu = -1.1;
            double sigma = 2.2;
            double[] distributionParameters = new double[2] { mu, sigma };

            // Valid input
            {
                double distributionMean = mu;
                double distributionVariance = Math.Pow(sigma, 2.0);

                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    void sampler(
                        int sampleSize,
                        double[] destinationArray,
                        int destinationIndex,
                        params double[] parameters)
                    {
                        rng.Gaussian(
                            sampleSize,
                            destinationArray,
                            destinationIndex,
                            parameters[0],
                            parameters[1]);
                    };

                    RandomNumberGeneratorTest.SampleArray.DoubleSucceed(
                        distributionMean,
                        distributionVariance,
                        sampleSize: 10000,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            // Invalid input
            {
                void sampler(
                    int sampleSize,
                    double[] destinationArray,
                    int destinationIndex,
                    params double[] parameters)
                {
                    RandomNumberGenerator.CreateMT19937(777777)
                        .Gaussian(
                            sampleSize,
                            destinationArray,
                            destinationIndex,
                            parameters[0],
                            parameters[1]);
                };

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationArrayIsNull<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationIndexIsNegative<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsNotPositive<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsTooBig<double, double>(
                        sampler,
                        distributionParameters);
            }

            #endregion
        }

        #endregion

        #region Uniform

        [TestMethod()]
        public void UniformSamplePointTest()
        {
            #region Specific tests

            // upperBound == lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .Uniform(
                                lowerBound: 1.0,
                                upperBound: 1.0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            // upperBound < lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .Uniform(
                                lowerBound: 2.0,
                                upperBound: 1.0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            #endregion

            #region Generic tests

            // Valid input
            {
                var lowerBound = -1.1;
                var upperBound = 2.2;
                double[] distributionParameters =
                    new double[2] { lowerBound, upperBound };
                double distributionMean = (lowerBound + upperBound) / 2.0;
                double distributionVariance =
                    Math.Pow(upperBound - lowerBound, 2.0) / 12.0;

                int sampleSize = 10000;
                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    double sampler(params double[] parameters)
                    {
                        return rng.Uniform(
                            parameters[0],
                            parameters[1]);
                    };

                    RandomNumberGeneratorTest.SamplePoint.DoubleSucceed(
                        distributionMean,
                        distributionVariance,
                        sampleSize,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            #endregion

        }

        [TestMethod()]
        public void UniformArrayTest()
        {
            #region Specific tests

            // upperBound == lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .Uniform(
                                sampleSize: 1,
                                destinationArray: new double[10],
                                destinationIndex: 0,
                                lowerBound: 1.0,
                                upperBound: 1.0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            // upperBound < lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .Uniform(
                                sampleSize: 1,
                                destinationArray: new double[10],
                                destinationIndex: 0,
                                lowerBound: 2.0,
                                upperBound: 1.0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            #endregion

            #region Generic tests

            var lowerBound = -1.1;
            var upperBound = 2.2;
            double[] distributionParameters =
                new double[2] { lowerBound, upperBound };

            // Valid input
            {
                double distributionMean = (lowerBound + upperBound) / 2.0;
                double distributionVariance =
                    Math.Pow(upperBound - lowerBound, 2.0) / 12.0;

                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    void sampler(
                        int sampleSize,
                        double[] destinationArray,
                        int destinationIndex,
                        params double[] parameters)
                    {
                        rng.Uniform(
                            sampleSize,
                            destinationArray,
                            destinationIndex,
                            parameters[0],
                            parameters[1]);
                    };

                    RandomNumberGeneratorTest.SampleArray.DoubleSucceed(
                        distributionMean,
                        distributionVariance,
                        sampleSize: 10000,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            // Invalid input
            {
                void sampler(
                    int sampleSize,
                    double[] destinationArray,
                    int destinationIndex,
                    params double[] parameters)
                {
                    RandomNumberGenerator.CreateMT19937(777777)
                        .Uniform(
                            sampleSize,
                            destinationArray,
                            destinationIndex,
                            parameters[0],
                            parameters[1]);
                };

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationArrayIsNull<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationIndexIsNegative<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsNotPositive<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsTooBig<double, double>(
                        sampler,
                        distributionParameters);
            }

            #endregion
        }

        #endregion

        #region DefaultUniform

        [TestMethod()]
        public void DefaultUniformSamplePointTest()
        {
            #region Generic tests

            // Valid input
            {
                var lowerBound = 0.0;
                var upperBound = 1.0;
                double[] distributionParameters =
                    Array.Empty<double>();
                double distributionMean = (lowerBound + upperBound) / 2.0;
                double distributionVariance =
                    Math.Pow(upperBound - lowerBound, 2.0) / 12.0;

                int sampleSize = 10000;
                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    double sampler(params double[] parameters)
                    {
                        return rng.DefaultUniform();
                    };

                    RandomNumberGeneratorTest.SamplePoint.DoubleSucceed(
                        distributionMean,
                        distributionVariance,
                        sampleSize,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            #endregion
        }

        [TestMethod()]
        public void DefaultUniformArrayTest()
        {
            #region Generic tests

            var lowerBound = 0.0;
            var upperBound = 1.0;
            double[] distributionParameters =
                Array.Empty<double>();

            // Valid input
            {
                double distributionMean = (lowerBound + upperBound) / 2.0;
                double distributionVariance =
                    Math.Pow(upperBound - lowerBound, 2.0) / 12.0;

                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    void sampler(
                        int sampleSize,
                        double[] destinationArray,
                        int destinationIndex,
                        params double[] parameters)
                    {
                        rng.DefaultUniform(
                            sampleSize,
                            destinationArray,
                            destinationIndex);
                    };

                    RandomNumberGeneratorTest.SampleArray.DoubleSucceed(
                        distributionMean,
                        distributionVariance,
                        sampleSize: 10000,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            // Invalid input
            {
                void sampler(
                    int sampleSize,
                    double[] destinationArray,
                    int destinationIndex,
                    params double[] parameters)
                {
                    RandomNumberGenerator.CreateMT19937(777777)
                        .DefaultUniform(
                            sampleSize,
                            destinationArray,
                            destinationIndex);
                };

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationArrayIsNull<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationIndexIsNegative<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsNotPositive<double, double>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsTooBig<double, double>(
                        sampler,
                        distributionParameters);
            }

            #endregion
        }

        #endregion

        #region DiscreteUniform

        [TestMethod()]
        public void DiscreteUniformSamplePointTest()
        {
            #region Specific tests

            // upperBound == lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .DiscreteUniform(
                                lowerBound: 1,
                                upperBound: 1);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            // upperBound < lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .DiscreteUniform(
                                lowerBound: 2,
                                upperBound: 1);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            #endregion

            #region Generic tests

            // Valid input
            {
                var lowerBound = -10;
                var upperBound = 10;
                int[] distributionParameters =
                    new int[2] { lowerBound, upperBound };
                // In what follows, formulas must take into
                // account that parameter upperBound is exclusive.
                double distributionMean =
                    Convert.ToDouble(lowerBound + upperBound - 1) / 2.0;
                double distributionVariance =
                    (Math.Pow(
                        Convert.ToDouble(upperBound - lowerBound),
                        2.0) - 1) / 12.0;

                int sampleSize = 10000;
                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    int sampler(params int[] parameters)
                    {
                        return rng.DiscreteUniform(
                            parameters[0],
                            parameters[1]);
                    };

                    RandomNumberGeneratorTest.SamplePoint.Int32Succeed(
                        distributionMean,
                        distributionVariance,
                        sampleSize,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            #endregion
        }

        [TestMethod()]
        public void DiscreteUniformArrayTest()
        {
            #region Specific tests

            // upperBound == lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .DiscreteUniform(
                                sampleSize: 1,
                                destinationArray: new int[10],
                                destinationIndex: 0,
                                lowerBound: 1,
                                upperBound: 1);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            // upperBound < lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        RandomNumberGenerator.CreateMT19937(777777)
                            .DiscreteUniform(
                                sampleSize: 1,
                                destinationArray: new int[10],
                                destinationIndex: 0,
                                lowerBound: 2,
                                upperBound: 1);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            #endregion

            #region Generic tests

            int lowerBound = -10;
            int upperBound = 10;
            int[] distributionParameters =
                new int[2] { lowerBound, upperBound };

            // Valid input
            {
                // In what follows, formulas must take into
                // account that parameter upperBound is exclusive.
                double distributionMean =
                    Convert.ToDouble(lowerBound + upperBound - 1) / 2.0;
                double distributionVariance =
                    (Math.Pow(
                        Convert.ToDouble(upperBound - lowerBound),
                        2.0) - 1) / 12.0;

                double delta = .01;

                foreach (var rng in GetRandomNumberGenerators())
                {
                    void sampler(
                        int sampleSize,
                        int[] destinationArray,
                        int destinationIndex,
                        params int[] parameters)
                    {
                        rng.DiscreteUniform(
                            sampleSize,
                            destinationArray,
                            destinationIndex,
                            parameters[0],
                            parameters[1]);
                    };

                    RandomNumberGeneratorTest.SampleArray.Int32Succeed(
                        distributionMean,
                        distributionVariance,
                        sampleSize: 10000,
                        delta,
                        sampler,
                        distributionParameters);
                }
            }

            // Invalid input
            {
                void sampler(
                    int sampleSize,
                    int[] destinationArray,
                    int destinationIndex,
                    params int[] parameters)
                {
                    RandomNumberGenerator.CreateMT19937(777777)
                        .DiscreteUniform(
                            sampleSize,
                            destinationArray,
                            destinationIndex,
                            parameters[0],
                            parameters[1]);
                };

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationArrayIsNull<int, int>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.DestinationIndexIsNegative<int, int>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsNotPositive<int, int>(
                        sampler,
                        distributionParameters);

                RandomNumberGeneratorTest.SampleArray
                    .Fail.SampleSizeIsTooBig<int, int>(
                        sampler,
                        distributionParameters);
            }

            #endregion
        }

        #endregion
    }
}
