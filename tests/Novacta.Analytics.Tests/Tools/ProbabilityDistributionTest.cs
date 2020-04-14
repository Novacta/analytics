// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="ProbabilityDistribution"/> 
    /// instances.
    /// </summary>
    static class ProbabilityDistributionTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static ProbabilityDistributionTest()
        {
            ProbabilityDistributionTest.Accuracy = 1e-6;
        }

        #endregion

        #region Helpers 

        /// <summary>
        /// Tests the specified <see cref="Action"/> for each item in the 
        /// given list of <see cref="TestableProbabilityDistribution"/> instances.
        /// </summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="testableItems">The list of 
        /// <see cref="TestableProbabilityDistribution"/> instances 
        /// to test.</param>
        public static void TestAction(
            Action<TestableProbabilityDistribution> test,
            List<TestableProbabilityDistribution> testableItems)
        {
            for (int i = 0; i < testableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing probability distribution {0}", i);
#endif
                test(testableItems[i]);
            }
        }

        /// <summary>Tests the specified sample <see cref="Action"/> for each item in the
        /// given list of <see cref="TestableProbabilityDistribution"/> instances.</summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="testableItems">The list of
        /// <see cref="TestableProbabilityDistribution"/> instances
        /// to test.</param>
        /// <param name="sampleSize">The size of the sample to test.</param>
        /// <param name="delta">The required accuracy in testing conditions.</param>
        public static void TestSampleAction(
            Action<TestableProbabilityDistribution, int, double> test,
            List<TestableProbabilityDistribution> testableItems,
            int sampleSize,
            double delta)
        {
            for (int i = 0; i < testableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing probability distribution {0}", i);
#endif
                test(testableItems[i], sampleSize, delta);
            }
        }

        /// <summary>
        /// Checks that the Chebyshev inequality holds true 
        /// for the mean of a sample draw from the specified
        /// distribution.
        /// </summary>
        /// <param name="distribution">The distribution from which the 
        /// sample has been drawn.</param>
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
        /// <see cref="ProbabilityDistribution.Mean"/> and the
        /// <see cref="ProbabilityDistribution.Variance"/> of 
        /// <paramref name="distribution"/> are finite, and
        /// <paramref name="delta"/> is in the open interval
        /// having extremes <c>0</c> and <c>1</c>.
        /// Then the event
        /// </para>
        /// <para>
        /// { | sampleMean - distribution.Mean() | < epsilon },
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
        /// <paramref name="distribution"/> has infinite mean.<br/>
        /// -or-<br/>
        /// <paramref name="distribution"/> has infinite variance.<br/>
        /// -or-<br/>
        /// <paramref name="delta"/> is not in the open interval 
        /// having extremes <c>0</c> and <c>1</c>.<br/>
        /// -or-<br/>
        /// The event did not happen for the mean of the observed 
        /// sample.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Chebyshev%27s_inequality"/>
        public static void CheckChebyshevInequality(
            ProbabilityDistribution distribution,
            double sampleMean,
            int sampleSize,
            double delta)
        {
            RandomNumberGeneratorTest.CheckChebyshevInequality(
                distribution.Mean(),
                distribution.Variance(),
                sampleMean,
                sampleSize,
                delta);
        }

        /// <summary>
        /// Checks that a sequence of observed frequencies 
        /// fit a corresponding sequence of theoretical ones.
        /// </summary>
        /// <param name="expected">The expected frequencies.</param>
        /// <param name="actual">The observed frequencies.</param>
        /// <param name="criticalValue">
        /// A quantile of the chi-squared distribution with a number of
        /// degrees of freedom equal to <see cref="PopulationSize"/> 
        /// minus <c>k</c>, where <c>k</c> is the number of parameters 
        /// to be estimated plus <c>1</c>. 
        /// To serve as the critical value for the Pearson's 
        /// chi-squared test whose null hypothesis assume that the 
        /// the <paramref name="expected"/> frequencies were true
        /// in the population
        /// when the <paramref name="actual"/> ones have been observed
        /// from a corresponding sample.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// <paramref name="expected"/> and <paramref name="actual"/>
        /// have not the same dimensions.<br/>
        /// -or-<br/>
        /// The test statistic is not less than 
        /// the <paramref name="criticalValue"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Goodness_of_fit"/>
        public static void CheckGoodnessOfFit(
            DoubleMatrix expected,
            DoubleMatrix actual,
            double criticalValue)
        {
            Assert.AreEqual(expected.NumberOfRows, actual.NumberOfRows);
            Assert.AreEqual(expected.NumberOfColumns, actual.NumberOfColumns);

            double score = 0.0;

            for (int i = 0; i < expected.Count; i++)
            {
                score += Math.Pow(actual[i] - expected[i], 2.0) / expected[i];
            }

            Assert.IsTrue(score < criticalValue);
        }

        static void CheckPartialGraph(
            Func<DoubleMatrix, DoubleMatrix> matrixFunc,
            Dictionary<TestableDoubleMatrix, DoubleMatrix> partialGraph)
        {
            foreach (var pair in partialGraph)
            {
                var arguments = pair.Key;
                var values = pair.Value;

                Assert.IsNotNull(arguments);
                Assert.IsNotNull(values);
                Assert.AreEqual(
                    arguments.Expected.NumberOfRows,
                    values.NumberOfRows);
                Assert.AreEqual(
                    arguments.Expected.NumberOfColumns,
                    values.NumberOfColumns);

                DoubleMatrixAssert.AreEqual(
                    expected: values,
                    actual: matrixFunc(arguments.Dense),
                    delta: ProbabilityDistributionTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                   expected: values,
                   actual: matrixFunc(arguments.Sparse),
                   delta: ProbabilityDistributionTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: values,
                    actual: matrixFunc(arguments.View),
                    delta: ProbabilityDistributionTest.Accuracy);
            }
        }

        static void CheckPartialGraph(
            Func<ReadOnlyDoubleMatrix, DoubleMatrix> matrixFunc,
            Dictionary<TestableDoubleMatrix, DoubleMatrix> partialGraph)
        {
            foreach (var pair in partialGraph)
            {
                var arguments = pair.Key;
                var values = pair.Value;

                Assert.IsNotNull(arguments);
                Assert.IsNotNull(values);
                Assert.AreEqual(
                    arguments.Expected.NumberOfRows,
                    values.NumberOfRows);
                Assert.AreEqual(
                    arguments.Expected.NumberOfColumns,
                    values.NumberOfColumns);

                DoubleMatrixAssert.AreEqual(
                    expected: values,
                    actual: matrixFunc(arguments.Dense.AsReadOnly()),
                    delta: ProbabilityDistributionTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: values,
                    actual: matrixFunc(arguments.Sparse.AsReadOnly()),
                    delta: DoubleMatrixTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: values,
                    actual: matrixFunc(arguments.View.AsReadOnly()),
                    delta: ProbabilityDistributionTest.Accuracy);
            }
        }

        static void CheckPartialGraph(
            Func<double, double> scalarFunc,
            Dictionary<TestableDoubleMatrix, DoubleMatrix> partialGraph)
        {
            foreach (var pair in partialGraph)
            {
                var arguments = pair.Key;
                var values = pair.Value;

                Assert.IsNotNull(arguments);
                Assert.IsNotNull(values);
                Assert.AreEqual(
                    arguments.Expected.NumberOfRows,
                    values.NumberOfRows);
                Assert.AreEqual(
                    arguments.Expected.NumberOfColumns,
                    values.NumberOfColumns);

                var args = new DoubleMatrix[3] {
                    arguments.Dense,
                    arguments.Sparse,
                    arguments.View
                };
                for (int j = 0; j < args.Length; j++)
                {
                    for (int i = 0; i < args[j].Count; i++)
                    {
                        var actual = scalarFunc(args[j][i]);
                        if (Double.IsNaN(values[i]))
                        {
                            Assert.IsTrue(Double.IsNaN(actual));
                        }
                        else
                            Assert.AreEqual(
                                expected: values[i],
                                actual: actual,
                                delta: ProbabilityDistributionTest.Accuracy);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Provides methods to test that 
        /// <see cref="ProbabilityDistribution.
        /// CanInvertCdf"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class CanInvertCdf
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="ProbabilityDistribution.
            /// CanInvertCdf"/> 
            /// property terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution)
            {
                Assert.AreEqual(
                    expected: testableDistribution.CanInvertCdf,
                    actual: testableDistribution.Distribution.CanInvertCdf);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="ProbabilityDistribution.
        /// Mean"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class Mean
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="ProbabilityDistribution.
            /// Mean"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution)
            {
                Assert.AreEqual(
                    expected: testableDistribution.Mean,
                    actual: testableDistribution.Distribution.Mean(),
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="ProbabilityDistribution.
        /// Variance"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class Variance
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="ProbabilityDistribution.
            /// Variance"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution)
            {
                Assert.AreEqual(
                    expected: testableDistribution.Variance,
                    actual: testableDistribution.Distribution.Variance(),
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="ProbabilityDistribution.
        /// StandardDeviation"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class StandardDeviation
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="ProbabilityDistribution.
            /// StandardDeviation"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution)
            {
                Assert.AreEqual(
                    expected: Math.Sqrt(testableDistribution.Variance),
                    actual: testableDistribution.Distribution.StandardDeviation(),
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the overloaded
        /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
        /// Pdf"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class Pdf
        {
            /// <summary>
            /// Tests that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// Pdf"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution)
            {
                var partialGraph = testableDistribution.PdfPartialGraph;

                var distribution = testableDistribution.Distribution;

                CheckPartialGraph(
                    (Func<double, double>)distribution.Pdf,
                    partialGraph);
                CheckPartialGraph(
                    (Func<DoubleMatrix, DoubleMatrix>)distribution.Pdf,
                    partialGraph);
                CheckPartialGraph(
                    (Func<ReadOnlyDoubleMatrix, DoubleMatrix>)distribution.Pdf,
                    partialGraph);
            }

            /// Provides methods to test that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// Pdf"/> 
            /// method fails when expected.
            public static class Fail
            {
                /// <summary>
                /// Provides methods to test that the
                /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
                /// Pdf"/> 
                /// method fails when parameter arguments is <b>null</b>.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void ArgumentsIsNull(
                    TestableProbabilityDistribution testableDistribution)
                {
                    string parameterName = "arguments";

                    // DoubleMatrix
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.Pdf(
                                arguments: (DoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // ReadOnlyDoubleMatrix
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.Pdf(
                                arguments: (ReadOnlyDoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that the overloaded
        /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
        /// Cdf"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class Cdf
        {
            /// <summary>
            /// Tests that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// Cdf"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(TestableProbabilityDistribution testableDistribution)
            {
                var partialGraph = testableDistribution.CdfPartialGraph;

                var distribution = testableDistribution.Distribution;

                CheckPartialGraph(
                    (Func<double, double>)distribution.Cdf,
                    partialGraph);
                CheckPartialGraph(
                    (Func<DoubleMatrix, DoubleMatrix>)distribution.Cdf,
                    partialGraph);
                CheckPartialGraph(
                    (Func<ReadOnlyDoubleMatrix, DoubleMatrix>)distribution.Cdf,
                    partialGraph);
            }

            /// Provides methods to test that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// Cdf"/> 
            /// method fails when expected.
            public static class Fail
            {
                /// <summary>
                /// Provides methods to test that the
                /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
                /// Cdf"/> 
                /// method fails when parameter arguments is <b>null</b>.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void ArgumentsIsNull(
                    TestableProbabilityDistribution testableDistribution)
                {
                    string parameterName = "arguments";

                    // DoubleMatrix
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.Cdf(
                                arguments: (DoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // ReadOnlyDoubleMatrix
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.Cdf(
                                arguments: (ReadOnlyDoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that the overloaded
        /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
        /// InverseCdf"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class InverseCdf
        {
            /// <summary>
            /// Tests that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// InverseCdf"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution)
            {
                var partialGraph = testableDistribution.InverseCdfPartialGraph;

                var distribution = testableDistribution.Distribution;

                CheckPartialGraph(
                    (Func<double, double>)distribution.InverseCdf,
                    partialGraph);
                CheckPartialGraph(
                    (Func<DoubleMatrix, DoubleMatrix>)distribution.InverseCdf,
                    partialGraph);
                CheckPartialGraph(
                    (Func<ReadOnlyDoubleMatrix, DoubleMatrix>)distribution.InverseCdf,
                    partialGraph);
            }

            /// Provides methods to test that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// InverseCdf"/> 
            /// method fails when expected.
            public static class Fail
            {
                /// <summary>
                /// Provides methods to test that the
                /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
                /// InverseCdf"/> 
                /// method fails when the CDF cannot be inverted.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void CdfCannotBeInverted(
                    TestableProbabilityDistribution testableDistribution)
                {
                    string STR_EXCEPT_PDF_INVERSE_CDF_NOT_SUPPORTED =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_PDF_INVERSE_CDF_NOT_SUPPORTED" });

                    // DoubleMatrix
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.InverseCdf(
                                arguments: DoubleMatrix.Identity(2));
                        },
                        expectedType: typeof(NotSupportedException),
                        expectedMessage: STR_EXCEPT_PDF_INVERSE_CDF_NOT_SUPPORTED);

                    // ReadOnlyDoubleMatrix
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.InverseCdf(
                                arguments: DoubleMatrix.Identity(2).AsReadOnly());
                        },
                        expectedType: typeof(NotSupportedException),
                        expectedMessage: STR_EXCEPT_PDF_INVERSE_CDF_NOT_SUPPORTED);
                }

                /// <summary>
                /// Provides methods to test that the
                /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
                /// InverseCdf"/> 
                /// method fails when parameter arguments is <b>null</b>.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void ArgumentsIsNull(
                    TestableProbabilityDistribution testableDistribution)
                {
                    string parameterName = "arguments";

                    // DoubleMatrix
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.InverseCdf(
                                arguments: (DoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // ReadOnlyDoubleMatrix
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.InverseCdf(
                                arguments: (ReadOnlyDoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that the overloaded
        /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
        /// Sample"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class Sample
        {
            /// <summary>
            /// Tests that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// Sample"/> 
            /// method terminates successfully when expected.
            /// </summary>
            /// <param name="testableDistribution">
            /// The testable probability distribution providing the instance 
            /// on which to invoke the methods to test and their expected
            /// behaviors.
            /// </param>
            /// <param name="sampleSize">The sample size.</param>
            /// <param name="delta">The required accuracy.</param>
            public static void Succeed(
                TestableProbabilityDistribution testableDistribution,
                int sampleSize,
                double delta)
            {
                var distribution = testableDistribution.Distribution;

                bool CanCheckChebyshevInequality =
                    !Double.IsInfinity(distribution.Mean())
                    &&
                    !Double.IsPositiveInfinity(distribution.Variance());

                // distribution.Sample()
                {
                    var sample = DoubleMatrix.Dense(sampleSize, 1);

                    for (int i = 0; i < sampleSize; i++)
                        sample[i] = distribution.Sample();

                    if (CanCheckChebyshevInequality)
                    {
                        CheckChebyshevInequality(
                           distribution: distribution,
                           sampleMean: Stat.Mean(sample),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }

                // distribution.Sample(sampleSize)
                {
                    var sample = distribution.Sample(sampleSize);

                    Assert.AreEqual(expected: sampleSize, actual: sample.Count);
                    Assert.AreEqual(expected: true, sample.IsColumnVector);

                    if (CanCheckChebyshevInequality)
                    {
                        CheckChebyshevInequality(
                           distribution: distribution,
                           sampleMean: Stat.Mean(sample),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }

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
                        distribution.Sample(partialSize, destinationArray, destinationIndex);
                    }

                    if (CanCheckChebyshevInequality)
                    {
                        CheckChebyshevInequality(
                           distribution: distribution,
                           sampleMean: Stat.Mean(sample),
                           sampleSize: sampleSize,
                           delta: delta);
                    }
                }
            }

            /// Provides methods to test that the overloaded
            /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
            /// Sample"/> 
            /// method fails when expected.
            public static class Fail
            {
                /// <summary>
                /// Tests that the
                /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
                /// Sample"/> 
                /// method fails when parameter sampleSize is not positive.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void SampleSizeIsNotPositive(
                    TestableProbabilityDistribution testableDistribution)
                {
                    string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                    string parameterName = "sampleSize";

                    // distribution.Sample(sampleSize)
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var values = testableDistribution.Distribution.Sample(
                                sampleSize: 0);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                        expectedParameterName: parameterName);

                    // distribution.Sample(sampleSize, destinationArray, destinationIndex)
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableDistribution.Distribution.Sample(
                                sampleSize: 0,
                                destinationArray: new double[1],
                                destinationIndex: 0);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that the
                /// <see cref="O:Novacta.Analytics.ProbabilityDistribution.
                /// Sample"/> 
                /// method fails when parameter destinationIndex is negative.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void DestinationIndexIsNegative(
                    TestableProbabilityDistribution testableDistribution)
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
                            testableDistribution.Distribution.Sample(
                                sampleSize: 1,
                                destinationArray: new double[1],
                                destinationIndex: -1);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that the
                /// <see cref="ProbabilityDistribution.
                /// Sample(int, double[], int)"/> 
                /// method fails when parameter sampleSize is too big to 
                /// fit in the destination array.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void SampleSizeIsTooBig(
                    TestableProbabilityDistribution testableDistribution)
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
                            testableDistribution.Distribution.Sample(
                                sampleSize: 2,
                                destinationArray: new double[1],
                                destinationIndex: 0);
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_PDF_SAMPLESIZE_ARRAYLENGTH_MISMATCH,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that the
                /// <see cref="ProbabilityDistribution.
                /// Sample(int, double[], int)"/> 
                /// method fails when parameter destinationArray is <b>null</b>.
                /// </summary>
                /// <param name="testableDistribution">
                /// The testable probability distribution providing the instance 
                /// on which to invoke the methods to test and their expected
                /// behaviors.
                /// </param>
                public static void DestinationArrayIsNull(
                    TestableProbabilityDistribution testableDistribution)
                {
                    string parameterName = "destinationArray";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            testableDistribution.Distribution.Sample(
                                sampleSize: 1,
                                destinationArray: null,
                                destinationIndex: 0);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }
            }
        }
    }
}
