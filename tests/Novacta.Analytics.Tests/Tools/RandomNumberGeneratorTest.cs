// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using System;
using System.Linq;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Encapsulates a method that draws a single sample point from 
    /// a statistical distribution having the specified parameters.
    /// </summary>
    /// <typeparam name="TSample">The type of a sample point.</typeparam>
    /// <typeparam name="TParameters">
    /// The type of the distribution parameters.
    /// </typeparam>
    /// <param name="parameters">The parameters of the distribution
    /// from which the sample point is drawn.</param>
    /// <returns>A sample point.</returns>
    delegate TSample SamplePointFunc<TSample, TParameters>(params TParameters[] parameters);

    /// <summary>
    /// Encapsulates a method that draws sample points from 
    /// a statistical distribution having the specified parameters
    /// and returns them in a destination array.
    /// </summary>
    /// <typeparam name="TSample">The type of a sample point.</typeparam>
    /// <typeparam name="TParameters">
    /// The type of the distribution parameters.
    /// </typeparam>
    /// <param name="sampleSize">The sample size.</param>
    /// <param name="destinationArray">The destination array that 
    /// receives the sampled data.</param>
    /// <param name="destinationIndex">The index in 
    /// <paramref name="destinationArray"/> at which storing 
    /// begins.</param>
    /// <param name="parameters">The parameters of the distribution
    /// from which the sample point is drawn.</param>
    delegate void SampleArrayFunc<TSample, TParameters>(
        int sampleSize,
        TSample[] destinationArray,
        int destinationIndex,
        params TParameters[] parameters);

    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="RandomNumberGenerator"/> 
    /// instances.
    /// </summary>
    static class RandomNumberGeneratorTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static RandomNumberGeneratorTest()
        {
            RandomNumberGeneratorTest.Accuracy = 1e-6;
        }

        #endregion

        #region Helpers 


        /// <summary>
        /// Checks that the Chebyshev inequality holds true 
        /// for the mean of a sample draw from a distribution
        /// having the specified mean and variance.
        /// </summary>
        /// <param name="distributionMean">The mean of the distribution 
        /// from which the sample has been drawn.</param>
        /// <param name="distributionVariance">
        /// The variance of the distribution 
        /// from which the sample has been drawn.</param>
        /// <param name="sampleMean">The observed sample mean.</param>
        /// <param name="sampleSize">The sample size.</param>
        /// <param name="delta">The delta: a positive, less than unity
        /// quantity such that the probability of observing the sample 
        /// mean close to the population mean is at least 
        /// <c>1 - delta</c>.
        /// </param>
        /// <remarks>
        /// <para>
        /// It is assumed that the 
        /// <paramref name="distributionMean"/> and the
        /// <paramref name="distributionVariance"/> are finite, and
        /// <paramref name="delta"/> is in the open interval
        /// having extremes <c>0</c> and <c>1</c>.
        /// Then the event
        /// </para>
        /// <para>
        /// { | sampleMean - distributionMean | < epsilon },
        /// </para>
        /// <para>
        /// where epsilon = sampleStdDev / Math.Sqrt( sampleSize * delta ),
        /// is expected to happen with probability 
        /// greater than or equal to 1 - delta.
        /// Hence this method asserts that the event under study 
        /// holds true for the observed sample mean.
        /// </para>
        /// </remarks>
        /// <exception cref="AssertFailedException">
        /// <paramref name="distributionMean"/> has infinite value.<br/>
        /// -or-<br/>
        /// <paramref name="distributionVariance"/> has infinite value.<br/>
        /// -or-<br/>
        /// <paramref name="delta"/> is not in the open interval 
        /// having extremes <c>0</c> and <c>1</c>.<br/>
        /// -or-<br/>
        /// The event did not happen for the mean of the observed 
        /// sample.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Chebyshev%27s_inequality"/>
        public static void CheckChebyshevInequality(
            double distributionMean,
            double distributionVariance,
            double sampleMean,
            int sampleSize,
            double delta)
        {
            Assert.IsTrue(0 < delta && delta < 1);
            double mean = distributionMean;
            Assert.IsFalse(Double.IsInfinity(mean));

            double stdDev = Math.Sqrt(distributionVariance);
            Assert.IsFalse(Double.IsInfinity(stdDev));

            double sampleStdDev = stdDev / Math.Sqrt(sampleSize);
            double epsilon = sampleStdDev / Math.Sqrt(delta);
            double score = Math.Abs(sampleMean - mean);

            Assert.IsTrue(score < epsilon);
        }

        #endregion

        /// <summary>
        /// Provides methods to test that samplers 
        /// in <see cref="RandomNumberGenerator"/>
        /// returning a sample point have
        /// been properly implemented.
        /// </summary>
        public static class SamplePoint
        {
            /// <summary>
            /// Tests that methods
            /// of <see cref="RandomNumberGenerator"/>
            /// returning an <see cref="Double"/> sample point from a 
            /// statistical distribution
            /// has been properly implemented.
            /// </summary>
            /// <typeparam name="TParameters">
            /// The type of the distribution parameters.</typeparam>
            /// <param name="distributionMean">The mean of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="distributionVariance">
            /// The variance of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="sampleSize">The sample size.</param>
            /// <param name="delta">The required accuracy.</param>
            /// <param name="samplePointFunc">
            /// A method returning a sample point from a 
            /// statistical distribution having the specified 
            /// parameters.</param>
            /// <param name="distributionParameters">The parameters of the distribution
            /// from which the sample is drawn.</param>
            public static void DoubleSucceed<TParameters>(
                    double distributionMean,
                    double distributionVariance,
                    int sampleSize,
                    double delta,
                    SamplePointFunc<double, TParameters> samplePointFunc,
                    params TParameters[] distributionParameters)
            {
                bool CanCheckChebyshevInequality =
                    !Double.IsInfinity(distributionMean)
                    &&
                    !Double.IsPositiveInfinity(distributionVariance);

                // distribution.Sample()
                {
                    var sample = DoubleMatrix.Dense(sampleSize, 1);

                    for (int i = 0; i < sampleSize; i++)
                        sample[i] = samplePointFunc(distributionParameters);

                    if (CanCheckChebyshevInequality)
                    {
                        RandomNumberGeneratorTest.CheckChebyshevInequality(
                           distributionMean: distributionMean,
                           distributionVariance: distributionVariance,
                           sampleMean: Stat.Mean(sample),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }
            }

            /// <summary>
            /// Tests that methods
            /// of <see cref="RandomNumberGenerator"/>
            /// returning an <see cref="Int32"/> sample point from a 
            /// statistical distribution
            /// has been properly implemented.
            /// </summary>
            /// <typeparam name="TParameters">
            /// The type of the distribution parameters.</typeparam>
            /// <param name="distributionMean">The mean of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="distributionVariance">
            /// The variance of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="sampleSize">The sample size.</param>
            /// <param name="delta">The required accuracy.</param>
            /// <param name="samplePointFunc">
            /// A method returning a sample point from a 
            /// statistical distribution having the specified 
            /// parameters.</param>
            /// <param name="distributionParameters">The parameters of the distribution
            /// from which the sample is drawn.</param>
            public static void Int32Succeed<TParameters>(
                    double distributionMean,
                    double distributionVariance,
                    int sampleSize,
                    double delta,
                    SamplePointFunc<int, TParameters> samplePointFunc,
                    params TParameters[] distributionParameters)
            {
                bool CanCheckChebyshevInequality =
                    !Double.IsInfinity(distributionMean)
                    &&
                    !Double.IsPositiveInfinity(distributionVariance);

                // distribution.Sample()
                {
                    var sample = DoubleMatrix.Dense(sampleSize, 1);

                    for (int i = 0; i < sampleSize; i++)
                        sample[i] = samplePointFunc(distributionParameters);

                    if (CanCheckChebyshevInequality)
                    {
                        RandomNumberGeneratorTest.CheckChebyshevInequality(
                           distributionMean: distributionMean,
                           distributionVariance: distributionVariance,
                           sampleMean: Stat.Mean(sample),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }
            }
        }

        /// <summary>
        /// Provides methods to test that samplers 
        /// in <see cref="RandomNumberGenerator"/>
        /// filling an array with a sample have
        /// been properly implemented.
        /// </summary>
        public static class SampleArray
        {
            /// <summary>
            /// Tests that methods
            /// of <see cref="RandomNumberGenerator"/>
            /// returning a sample of doubles to an array from a 
            /// statistical distribution
            /// terminates successfully when expected.
            /// </summary>
            /// <typeparam name="TParameters">
            /// The type of the distribution parameters.</typeparam>
            /// <param name="distributionMean">The mean of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="distributionVariance">
            /// The variance of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="sampleSize">The sample size.</param>
            /// <param name="delta">The required accuracy.</param>
            /// <param name="sampleArrayFunc">
            /// A method returning a sample from a 
            /// statistical distribution having the specified 
            /// parameters.</param>
            /// <param name="distributionParameters">The parameters of the distribution
            /// from which the sample is drawn.</param>
            public static void DoubleSucceed<TParameters>(
                    double distributionMean,
                    double distributionVariance,
                    int sampleSize,
                    double delta,
                    SampleArrayFunc<double, TParameters> sampleArrayFunc,
                    params TParameters[] distributionParameters)
            {
                bool canCheckChebyshevInequality =
                    !Double.IsInfinity(distributionMean)
                    &&
                    !Double.IsPositiveInfinity(distributionVariance);

                // distribution.Sample(sampleSize, destinationArray, destinationIndex)
                {
                    int destinationSize = sampleSize * 10;
                    int partialSize = sampleSize;
                    var sample = DoubleMatrix.Dense(destinationSize, 1);
                    double[] destinationArray = sample.GetStorage();

                    IndexCollection destinationIndexes =
                        IndexCollection.Sequence(0, partialSize, destinationSize - 1);

                    foreach (var destinationIndex in destinationIndexes)
                    {
                        sampleArrayFunc(
                            partialSize,
                            destinationArray,
                            destinationIndex,
                            distributionParameters);
                    }

                    if (canCheckChebyshevInequality)
                    {
                        RandomNumberGeneratorTest.CheckChebyshevInequality(
                           distributionMean: distributionMean,
                           distributionVariance: distributionVariance,
                           sampleMean: Stat.Mean(sample),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }
            }

            /// <summary>
            /// Tests that methods
            /// of <see cref="RandomNumberGenerator"/>
            /// returning a sample of integers to an array from a 
            /// statistical distribution
            /// terminates successfully when expected.
            /// </summary>
            /// <typeparam name="TParameters">
            /// The type of the distribution parameters.</typeparam>
            /// <param name="distributionMean">The mean of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="distributionVariance">
            /// The variance of the distribution 
            /// from which the sample has been drawn.</param>
            /// <param name="sampleSize">The sample size.</param>
            /// <param name="delta">The required accuracy.</param>
            /// <param name="sampleArrayFunc">
            /// A method returning a sample from a 
            /// statistical distribution having the specified 
            /// parameters.</param>
            /// <param name="distributionParameters">The parameters of the distribution
            /// from which the sample is drawn.</param>
            public static void Int32Succeed<TParameters>(
                    double distributionMean,
                    double distributionVariance,
                    int sampleSize,
                    double delta,
                    SampleArrayFunc<int, TParameters> sampleArrayFunc,
                    params TParameters[] distributionParameters)
            {
                bool CanCheckChebyshevInequality =
                    !Double.IsInfinity(distributionMean)
                    &&
                    !Double.IsPositiveInfinity(distributionVariance);

                // distribution.Sample(sampleSize, destinationArray, destinationIndex)
                {
                    int destinationSize = sampleSize * 10;
                    int partialSize = sampleSize;
                    var sample = new int[destinationSize];
                    int[] destinationArray = sample;

                    IndexCollection destinationIndexes =
                        IndexCollection.Sequence(0, partialSize, destinationSize - 1);

                    foreach (var destinationIndex in destinationIndexes)
                    {
                        sampleArrayFunc(
                            partialSize,
                            destinationArray,
                            destinationIndex,
                            distributionParameters);
                    }

                    if (CanCheckChebyshevInequality)
                    {
                        RandomNumberGeneratorTest.CheckChebyshevInequality(
                           distributionMean: distributionMean,
                           distributionVariance: distributionVariance,
                           sampleMean: sample.Average(),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }
            }

            /// <summary>
            /// Provides methods to test that samplers 
            /// in <see cref="RandomNumberGenerator"/>
            /// filling an array with a sample fail
            /// when expected.
            /// </summary>
            public static class Fail
            {
                /// <summary>
                /// Tests that samplers 
                /// in <see cref="RandomNumberGenerator"/>
                /// filling an array with a sample fail
                /// when parameter sampleSize is not positive.
                /// </summary>
                /// <typeparam name="TSample">The type of a sample point.</typeparam>
                /// <typeparam name="TParameters">
                /// The type of the distribution parameters.</typeparam>
                /// <param name="sampleArrayFunc">
                /// A method returning a sample from a 
                /// statistical distribution having the specified 
                /// parameters.</param>
                /// <param name="distributionParameters">The parameters of the distribution
                /// from which the sample is drawn.</param>
                public static void SampleSizeIsNotPositive<TSample, TParameters>(
                    SampleArrayFunc<TSample, TParameters> sampleArrayFunc,
                    TParameters[] distributionParameters)
                {
                    string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                    string parameterName = "sampleSize";

                    // distribution.Sample(sampleSize, destinationArray, destinationIndex)
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            sampleArrayFunc(
                                sampleSize: 0,
                                destinationArray: new TSample[1],
                                destinationIndex: 0,
                                parameters: distributionParameters);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that samplers 
                /// in <see cref="RandomNumberGenerator"/>
                /// filling an array with a sample fail
                /// when parameter destinationIndex is negative.
                /// </summary>
                /// <typeparam name="TSample">The type of a sample point.</typeparam>
                /// <typeparam name="TParameters">
                /// The type of the distribution parameters.</typeparam>
                /// <param name="sampleArrayFunc">
                /// A method returning a sample from a 
                /// statistical distribution having the specified 
                /// parameters.</param>
                /// <param name="distributionParameters">The parameters of the distribution
                /// from which the sample is drawn.</param>
                public static void DestinationIndexIsNegative<TSample, TParameters>(
                    SampleArrayFunc<TSample, TParameters> sampleArrayFunc,
                    TParameters[] distributionParameters)
                {
                    string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                    string parameterName = "destinationIndex";

                    // distribution.Sample(sampleSize, destinationArray, destinationIndex)
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            sampleArrayFunc(
                            sampleSize: 1,
                            destinationArray: new TSample[1],
                            destinationIndex: -1,
                            parameters: distributionParameters);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that samplers 
                /// in <see cref="RandomNumberGenerator"/>
                /// filling an array with a sample fail
                /// when parameter sampleSize is too big to 
                /// fit in the destination array.
                /// </summary>
                /// <typeparam name="TSample">The type of a sample point.</typeparam>
                /// <typeparam name="TParameters">
                /// The type of the distribution parameters.</typeparam>
                /// <param name="sampleArrayFunc">
                /// A method returning a sample from a 
                /// statistical distribution having the specified 
                /// parameters.</param>
                /// <param name="distributionParameters">The parameters of the distribution
                /// from which the sample is drawn.</param>
                public static void SampleSizeIsTooBig<TSample, TParameters>(
                    SampleArrayFunc<TSample, TParameters> sampleArrayFunc,
                    TParameters[] distributionParameters)
                {
                    string STR_EXCEPT_PDF_SAMPLESIZE_ARRAYLENGTH_MISMATCH =
                        String.Format(
                            (string)Reflector.ExecuteStaticMember(
                                typeof(ImplementationServices),
                                "GetResourceString",
                                new string[] { "STR_EXCEPT_PDF_SAMPLESIZE_ARRAYLENGTH_MISMATCH" }),
                            "sampleSize",
                            "destinationArray",
                            "destinationIndex");


                    string parameterName = "sampleSize";

                    ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        sampleArrayFunc(
                            sampleSize: 2,
                            destinationArray: new TSample[1],
                            destinationIndex: 0,
                            parameters: distributionParameters);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PDF_SAMPLESIZE_ARRAYLENGTH_MISMATCH,
                    expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that samplers 
                /// in <see cref="RandomNumberGenerator"/>
                /// filling an array with a sample fail
                /// when parameter destinationArray is <b>null</b>.
                /// </summary>
                /// <typeparam name="TSample">The type of a sample point.</typeparam>
                /// <typeparam name="TParameters">
                /// The type of the distribution parameters.</typeparam>
                /// <param name="sampleArrayFunc">
                /// A method returning a sample from a 
                /// statistical distribution having the specified 
                /// parameters.</param>
                /// <param name="distributionParameters">The parameters of the distribution
                /// from which the sample is drawn.</param>
                public static void DestinationArrayIsNull<TSample,TParameters>(
                    SampleArrayFunc<TSample, TParameters> sampleArrayFunc,
                    TParameters[] distributionParameters)
                {
                    string parameterName = "destinationArray";

                    ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        sampleArrayFunc(
                            sampleSize: 1,
                            destinationArray: null,
                            destinationIndex: 0,
                            parameters: distributionParameters);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
                }
            }
        }
    }
}

