// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Globalization;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="NonMetricMultidimensionalScaling"/> 
    /// instances.
    /// </summary>
    static class NonMetricMultidimensionalScalingTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static NonMetricMultidimensionalScalingTest()
        {
            NonMetricMultidimensionalScalingTest.Accuracy = 1e-3;
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="NonMetricMultidimensionalScaling
        /// .Analyze(DoubleMatrix, int?, double, int, double)"/> 
        /// method, and its eventual overloads, have
        /// been properly implemented.
        /// </summary>
        internal static class Analyze
        {
            /// Tests that method
            /// <see cref="NonMetricMultidimensionalScaling
            /// .Analyze(DoubleMatrix, int?, double, int, double)"/> 
            /// has been properly implemented.
            public static void Succeed(
                TestableNonMetricMultidimensionalScaling<TestableDoubleMatrix> testableMds)
            {
                var testableDissimilarityMatrix = testableMds.TestableDissimilarityMatrix;

                #region Writable

                // Dense
                {
                    var actualMds = NonMetricMultidimensionalScaling.Analyze(
                        testableDissimilarityMatrix.AsDense,
                        testableMds.ConfigurationDimension,
                        minkowskiDistanceOrder: testableMds.MinkowskiDistanceOrder,
                        maximumNumberOfIterations: testableMds.MaximumNumberOfIterations,
                        terminationTolerance: testableMds.TerminationTolerance);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableMds
                            .Configuration,
                        actual: actualMds.Configuration,
                        delta: NonMetricMultidimensionalScalingTest.Accuracy);

                    if (null != testableMds.ConfigurationDimension)
                    {
                        Assert.AreEqual(
                            expected: testableMds
                                .ConfigurationDimension.Value,
                            actual: actualMds.Configuration.NumberOfColumns);
                    }

                    Assert.AreEqual(
                        expected: testableMds
                            .Stress,
                        actual: actualMds.Stress,
                        delta: NonMetricMultidimensionalScalingTest.Accuracy);

                    Assert.AreEqual(
                        expected: testableMds
                            .HasConverged,
                        actual: actualMds.HasConverged);
                }

                // Sparse
                {
                    var actualMds = NonMetricMultidimensionalScaling.Analyze(
                        testableDissimilarityMatrix.AsSparse,
                        testableMds.ConfigurationDimension,
                        minkowskiDistanceOrder: testableMds.MinkowskiDistanceOrder,
                        maximumNumberOfIterations: testableMds.MaximumNumberOfIterations,
                        terminationTolerance: testableMds.TerminationTolerance);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableMds
                            .Configuration,
                        actual: actualMds.Configuration,
                        delta: NonMetricMultidimensionalScalingTest.Accuracy);

                    if (null != testableMds.ConfigurationDimension)
                    {
                        Assert.AreEqual(
                            expected: testableMds
                                .ConfigurationDimension.Value,
                            actual: actualMds.Configuration.NumberOfColumns);
                    }

                    Assert.AreEqual(
                        expected: testableMds
                            .Stress,
                        actual: actualMds.Stress,
                        delta: NonMetricMultidimensionalScalingTest.Accuracy);

                    Assert.AreEqual(
                        expected: testableMds
                            .HasConverged,
                        actual: actualMds.HasConverged);
                }

                #endregion
            }

            /// <summary>
            /// Tests the operation
            /// when its proximities parameter is set through a value represented by 
            /// a <b>null</b> instance.
            /// </summary>
            public static void DissimilaritiesIsNull()
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: (DoubleMatrix)null,
                            configurationDimension: 2);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "dissimilarities");
            }

            /// <summary>
            /// Tests the operation
            /// when its proximities parameter is set through a value represented by an instance
            /// that is not a symmetric matrix.
            /// </summary>
            public static void DissimilaritiesIsNotSymmetric()
            {
                var STR_EXCEPT_PAR_MUST_BE_SYMMETRIC =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SYMMETRIC");

                string parameterName = "dissimilarities";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 3),
                            configurationDimension: 2);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SYMMETRIC,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its dissimilarities parameter is set through a value that cannot be 
            /// classically scaled.
            /// </summary>
            public static void DissimilaritiesCannotBeClassicallyScaled()
            {
                var STR_EXCEPT_NMMDS_NO_INITIAL_CONFIGURATION =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_NMMDS_NO_INITIAL_CONFIGURATION");

                string parameterName = "dissimilarities";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(3, 3),
                            configurationDimension: null);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_NMMDS_NO_INITIAL_CONFIGURATION,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its configurationDimension parameter is not positive.
            /// </summary>
            public static void ConfigurationDimensionIsNotPositive()
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "configurationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its configurationDimension parameter is greater than the number of rows
            /// of the dissimilarities matrix.
            /// </summary>
            public static void ConfigurationDimensionIsGreaterThanDissimilaritiesNumberOfRows()
            {
                string parameterName = "configurationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 3);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER_ROWS"),
                            "configurationDimension",
                            "dissimilarities"),
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its configurationDimension parameter is greater than the number of 
            /// positive eigenvalues of the transformed dissimilarities matrix.
            /// </summary>
            public static void ConfigurationDimensionIsUnallowedGivenNumberOfPositiveEigenvalues()
            {
                string parameterName = "configurationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(5, 5, 
                                [  0,
                                 159,
                                 247,
                                 131,
                                 197,
                                 159,
                                   0,
                                 230,
                                  97,
                                  89,
                                 247,
                                 230,
                                   0,
                                 309,
                                 317,
                                 131,
                                  97,
                                 309,
                                   0,
                                  68,
                                 197,
                                  89,
                                 317,
                                  68,
                                   0]),
                            configurationDimension: 5);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_NMMDS_UNALLOWED_CONFIGURATION_DIMENSION"),
                    expectedParameterName: parameterName);
            }
            
            /// <summary>
            /// Tests the operation
            /// when its maximumNumberOfIterations parameter is not positive.
            /// </summary>
            public static void MaximumNumberOfIterationsIsNotPositive()
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 1,
                            minkowskiDistanceOrder: 1,
                            maximumNumberOfIterations: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 1,
                            minkowskiDistanceOrder: 1,
                            maximumNumberOfIterations: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its terminationTolerance parameter is not positive.
            /// </summary>
            public static void TerminationToleranceIsNotPositive()
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "terminationTolerance";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 1,
                            minkowskiDistanceOrder: 1,
                            maximumNumberOfIterations: 10,
                            terminationTolerance: 0.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 1,
                            minkowskiDistanceOrder: 1,
                            maximumNumberOfIterations: 10,
                            terminationTolerance: -1.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its minkowskiDistanceOrder parameter is not greater than one.
            /// </summary>
            public static void MinkowskiDistanceOrderIsNotGreaterThanOne()
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "minkowskiDistanceOrder";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 1,
                            minkowskiDistanceOrder: 0.0,
                            maximumNumberOfIterations: 10,
                            terminationTolerance: 1e-5);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_LESS_THAN_VALUE"),
                            1.0),
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        NonMetricMultidimensionalScaling.Analyze(
                            dissimilarities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 1,
                            minkowskiDistanceOrder: .999999,
                            maximumNumberOfIterations: 10,
                            terminationTolerance: 1e-5);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_LESS_THAN_VALUE"),
                            1.0),
                    expectedParameterName: parameterName);
            }

        }
    }
}