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
    /// about <see cref="PrincipalProjections"/> 
    /// instances.
    /// </summary>
    static class PrincipalProjectionsTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static PrincipalProjectionsTest()
        {
            PrincipalProjectionsTest.Accuracy = 1e-1;
        }

        #endregion

        #region Helpers 

        /// <summary>
        /// Tests the specified <see cref="Action"/> for each item in the 
        /// given list of <see cref="TestablePrincipalProjections"/> instances.
        /// </summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="testableItems">The list of 
        /// <see cref="TestablePrincipalProjections"/> instances 
        /// to test.</param>
        public static void TestAction(
            Action<TestablePrincipalProjections> test,
            List<TestablePrincipalProjections> testableItems)
        {
            for (int i = 0; i < testableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing principal projections {0}", i);
#endif
                test(testableItems[i]);
            }
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.ActiveCloud"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class ActiveCloud
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.ActiveCloud"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .ActiveCloud.Basis.GetBasisMatrix(),
                    actual: principalProjections
                        .ActiveCloud.Basis.GetBasisMatrix(),
                    delta: PrincipalProjectionsTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .ActiveCloud.Coordinates,
                    actual: principalProjections
                        .ActiveCloud.Coordinates,
                    delta: PrincipalProjectionsTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .ActiveCloud.Weights,
                    actual: principalProjections
                        .ActiveCloud.Weights,
                    delta: PrincipalProjectionsTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.Contributions"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class Contributions
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.Contributions"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Contributions,
                    actual: principalProjections
                        .Contributions,
                    delta: PrincipalProjectionsTest.Accuracy);

                Assert.AreEqual(
                    expected: Stat.Sum(testablePrincipalProjections.Contributions),
                    actual: (double)principalProjections.NumberOfDirections,
                    delta: PrincipalProjectionsTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.Coordinates"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class Coordinates
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.Coordinates"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Coordinates,
                    actual: principalProjections
                        .Coordinates,
                    delta: PrincipalProjectionsTest.Accuracy);

                Assert.AreEqual(
                    expected: testablePrincipalProjections.Coordinates.NumberOfColumns,
                    actual: principalProjections.NumberOfDirections);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.Correlations"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class Correlations
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.Correlations"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Correlations,
                    actual: principalProjections
                        .Correlations,
                    delta: PrincipalProjectionsTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.CorrelateSupplementaryVariables"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class CorrelateSupplementaryVariables
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.CorrelateSupplementaryVariables"/> 
            /// method succeeds when expected.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Correlations,
                    actual: principalProjections.CorrelateSupplementaryVariables(
                        (DoubleMatrix)principalProjections.ActiveCloud.Coordinates),
                    delta: PrincipalProjectionsTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Correlations,
                    actual: principalProjections.CorrelateSupplementaryVariables(
                        principalProjections.ActiveCloud.Coordinates),
                    delta: PrincipalProjectionsTest.Accuracy);
            }

            /// <summary>
            /// Provides methods to test that the 
            /// <see cref="PrincipalProjections.CorrelateSupplementaryVariables"/> 
            /// method fails when expected.
            /// </summary>
            public static class Fail
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="PrincipalProjections.CorrelateSupplementaryVariables"/> 
                /// method fails when supplementaryVariables is null.
                public static void SupplementaryVariablesIsNull(
                    TestablePrincipalProjections testablePrincipalProjections)
                {
                    var principalProjections =
                        testablePrincipalProjections.PrincipalProjections;

                    string parameterName = "supplementaryVariables";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.CorrelateSupplementaryVariables(
                                (DoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage:
                            ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.CorrelateSupplementaryVariables(
                                (ReadOnlyDoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage:
                            ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="PrincipalProjections.CorrelateSupplementaryVariables"/> 
                /// method fails when supplementaryVariables has not
                /// the expected number of rows.
                public static void SupplementaryVariablesHasWrongNumberOfRows(
                    TestablePrincipalProjections testablePrincipalProjections)
                {
                    var principalProjections =
                        testablePrincipalProjections.PrincipalProjections;

                    var STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS =
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS");

                    string parameterName = "supplementaryVariables";

                    DoubleMatrix supplementaryVariables = 
                        DoubleMatrix.Dense(
                            principalProjections.ActiveCloud.Coordinates.NumberOfRows + 1,
                            2);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.CorrelateSupplementaryVariables(
                                supplementaryVariables);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage:
                            STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS,
                        expectedParameterName: parameterName);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.Directions"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class Directions
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.Directions"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Directions,
                    actual: principalProjections
                        .Directions,
                    delta: PrincipalProjectionsTest.Accuracy);

                Assert.AreEqual(
                    expected: testablePrincipalProjections.Directions.NumberOfColumns,
                    actual: principalProjections.NumberOfDirections);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.LocateSupplementaryPoints(DoubleMatrix)"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class LocateSupplementaryPoints
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.LocateSupplementaryPoints"/> 
            /// method succeeds when expected.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Coordinates,
                    actual: principalProjections.LocateSupplementaryPoints(
                        (DoubleMatrix)principalProjections.ActiveCloud.Coordinates),
                    delta: PrincipalProjectionsTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Coordinates,
                    actual: principalProjections.LocateSupplementaryPoints(
                        principalProjections.ActiveCloud.Coordinates),
                    delta: PrincipalProjectionsTest.Accuracy);
            }

            /// <summary>
            /// Provides methods to test that the 
            /// <see cref="PrincipalProjections.LocateSupplementaryPoints"/> 
            /// method fails when expected.
            /// </summary>
            public static class Fail
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="PrincipalProjections.LocateSupplementaryPoints"/> 
                /// method fails when activeCoordinates is null.
                public static void ActiveCoordinatesIsNull(
                    TestablePrincipalProjections testablePrincipalProjections)
                {
                    var principalProjections =
                        testablePrincipalProjections.PrincipalProjections;

                    string parameterName = "activeCoordinates";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.LocateSupplementaryPoints(
                                (DoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage:
                            ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.LocateSupplementaryPoints(
                                (ReadOnlyDoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage:
                            ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="PrincipalProjections.LocateSupplementaryPoints"/> 
                /// method fails when activeCoordinates has not
                /// the expected number of columns.
                public static void ActiveCoordinatesHasWrongNumberOfColumns(
                    TestablePrincipalProjections testablePrincipalProjections)
                {
                    var principalProjections =
                        testablePrincipalProjections.PrincipalProjections;

                    var STR_EXCEPT_GDA_COLUMNS_NOT_EQUAL_TO_ACTIVE_VARIABLES =
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_GDA_COLUMNS_NOT_EQUAL_TO_ACTIVE_VARIABLES");

                    string parameterName = "activeCoordinates";

                    DoubleMatrix activeCoordinates =
                        DoubleMatrix.Dense(
                            2,
                            principalProjections.ActiveCloud.Coordinates.NumberOfColumns + 1);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.LocateSupplementaryPoints(
                                activeCoordinates);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage:
                            STR_EXCEPT_GDA_COLUMNS_NOT_EQUAL_TO_ACTIVE_VARIABLES,
                        expectedParameterName: parameterName);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.RegressSupplementaryVariables"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class RegressSupplementaryVariables
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.RegressSupplementaryVariables"/> 
            /// method succeeds when expected.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .RegressionCoefficients,
                    actual: principalProjections.RegressSupplementaryVariables(
                        (DoubleMatrix)principalProjections.ActiveCloud.Coordinates),
                    delta: PrincipalProjectionsTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .RegressionCoefficients,
                    actual: principalProjections.RegressSupplementaryVariables(
                        principalProjections.ActiveCloud.Coordinates),
                    delta: PrincipalProjectionsTest.Accuracy);
            }

            /// <summary>
            /// Provides methods to test that the 
            /// <see cref="PrincipalProjections.RegressSupplementaryVariables"/> 
            /// method fails when expected.
            /// </summary>
            public static class Fail
            {
                /// <summary>
                /// Tests that the 
                /// <see cref="PrincipalProjections.RegressSupplementaryVariables"/> 
                /// method fails when supplementaryVariables is null.
                public static void SupplementaryVariablesIsNull(
                    TestablePrincipalProjections testablePrincipalProjections)
                {
                    var principalProjections =
                        testablePrincipalProjections.PrincipalProjections;

                    string parameterName = "supplementaryVariables";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.RegressSupplementaryVariables(
                                (DoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage:
                            ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.RegressSupplementaryVariables(
                                (ReadOnlyDoubleMatrix)null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage:
                            ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                /// <summary>
                /// Tests that the 
                /// <see cref="PrincipalProjections.RegressSupplementaryVariables"/> 
                /// method fails when supplementaryVariables has not
                /// the expected number of rows.
                public static void SupplementaryVariablesHasWrongNumberOfRows(
                    TestablePrincipalProjections testablePrincipalProjections)
                {
                    var principalProjections =
                        testablePrincipalProjections.PrincipalProjections;

                    var STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS =
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS");

                    string parameterName = "supplementaryVariables";

                    DoubleMatrix supplementaryVariables =
                        DoubleMatrix.Dense(
                            principalProjections.ActiveCloud.Coordinates.NumberOfRows + 1,
                            2);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            principalProjections.RegressSupplementaryVariables(
                                supplementaryVariables);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage:
                            STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS,
                        expectedParameterName: parameterName);
                }
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.Variances"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class Variances
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.Variances"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .Variances,
                    actual: principalProjections
                        .Variances,
                    delta: PrincipalProjectionsTest.Accuracy);

                Assert.AreEqual(
                    expected: testablePrincipalProjections.Variances.Count,
                    actual: principalProjections.NumberOfDirections);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.RegressionCoefficients"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class RegressionCoefficients
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.RegressionCoefficients"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .RegressionCoefficients,
                    actual: principalProjections
                        .RegressionCoefficients,
                    delta: PrincipalProjectionsTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="PrincipalProjections.RepresentationQualities"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class RepresentationQualities
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="PrincipalProjections.RepresentationQualities"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestablePrincipalProjections testablePrincipalProjections)
            {
                var principalProjections =
                    testablePrincipalProjections.PrincipalProjections;

                DoubleMatrixAssert.AreEqual(
                    expected: testablePrincipalProjections
                        .RepresentationQualities,
                    actual: principalProjections
                        .RepresentationQualities,
                    delta: PrincipalProjectionsTest.Accuracy);
            }
        }

    }
}
