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
    /// about <see cref="ClassicalMultidimensionalScaling"/> 
    /// instances.
    /// </summary>
    static class ClassicalMultidimensionalScalingTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static ClassicalMultidimensionalScalingTest()
        {
            ClassicalMultidimensionalScalingTest.Accuracy = 1e-3;
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="ClassicalMultidimensionalScaling.Analyze(DoubleMatrix, int?)"/> 
        /// method, and its eventual overloads, have
        /// been properly implemented.
        /// </summary>
        internal static class Analyze
        {
            /// Tests that method
            /// <see cref="ClassicalMultidimensionalScaling.Analyze(DoubleMatrix, int?)"/>
            /// has been properly implemented.
            public static void Succeed(
                TestableClassicalMultidimensionalScaling<TestableDoubleMatrix> testableMds)
            {
                var testableProximityMatrix = testableMds.TestableProximityMatrix;

                #region Writable

                // Dense
                {
                    var actualMds = ClassicalMultidimensionalScaling.Analyze(
                        testableProximityMatrix.AsDense,
                        testableMds.ConfigurationDimension);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableMds
                            .Configuration,
                        actual: actualMds.Configuration,
                        delta: ClassicalMultidimensionalScalingTest.Accuracy);

                    if (null != testableMds.ConfigurationDimension)
                    {
                        Assert.AreEqual(
                            expected: testableMds
                                .ConfigurationDimension.Value,
                            actual: actualMds.Configuration.NumberOfColumns);
                    }

                    Assert.AreEqual(
                        expected: testableMds
                            .GoodnessOfFit,
                        actual: actualMds.GoodnessOfFit,
                        delta: ClassicalMultidimensionalScalingTest.Accuracy);
                }

                // Sparse
                {
                    var actualMds = ClassicalMultidimensionalScaling.Analyze(
                        testableProximityMatrix.AsSparse,
                        testableMds.ConfigurationDimension);

                    DoubleMatrixAssert.AreEqual(
                        expected: testableMds
                            .Configuration,
                        actual: actualMds.Configuration,
                        delta: ClassicalMultidimensionalScalingTest.Accuracy);

                    if (null != testableMds.ConfigurationDimension)
                    {
                        Assert.AreEqual(
                            expected: testableMds
                                .ConfigurationDimension.Value,
                            actual: actualMds.Configuration.NumberOfColumns);
                    }

                    Assert.AreEqual(
                        expected: testableMds
                            .GoodnessOfFit,
                        actual: actualMds.GoodnessOfFit,
                        delta: ClassicalMultidimensionalScalingTest.Accuracy);
                }

                #endregion
            }

            /// <summary>
            /// Tests the operation
            /// when its proximities parameter is set through a value represented by 
            /// a <b>null</b> instance.
            /// </summary>
            public static void ProximitiesIsNull()
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: (DoubleMatrix)null,
                            configurationDimension: 2);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "proximities");
            }

            /// <summary>
            /// Tests the operation
            /// when its proximities parameter is set through a value represented by an instance
            /// that is not a symmetric matrix.
            /// </summary>
            public static void ProximitiesIsNotSymmetric()
            {
                var STR_EXCEPT_PAR_MUST_BE_SYMMETRIC =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SYMMETRIC");

                string parameterName = "proximities";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: DoubleMatrix.Dense(2, 3),
                            configurationDimension: 2);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_SYMMETRIC,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its proximities parameter is set through a value that cannot be scaled.
            /// </summary>
            public static void ProximitiesCannotBeScaled()
            {
                var STR_EXCEPT_CMDS_PROXIMITIES_CANNOT_BE_SCALED =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CMDS_PROXIMITIES_CANNOT_BE_SCALED");

                string parameterName = "proximities";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: DoubleMatrix.Dense(3, 3),
                            configurationDimension: null);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_CMDS_PROXIMITIES_CANNOT_BE_SCALED,
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
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            /// <summary>
            /// Tests the operation
            /// when its configurationDimension parameter is greater than the number of rows
            /// of the proximities matrix.
            /// </summary>
            public static void ConfigurationDimensionIsGreaterThanProximitiesNumberOfRows()
            {
                string parameterName = "configurationDimension";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: DoubleMatrix.Dense(2, 2),
                            configurationDimension: 3);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER_ROWS"),
                            "configurationDimension",
                            "proximities"),
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
                        ClassicalMultidimensionalScaling.Analyze(
                            proximities: DoubleMatrix.Dense(5, 5,
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
                                "STR_EXCEPT_CMDS_UNALLOWED_CONFIGURATION_DIMENSION"),
                    expectedParameterName: parameterName);
            }

        }
    }
}