// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="NumericalBin"/> 
    /// instances.
    /// </summary>
    static class NumericalBinTest
    {
        /// <summary>
        /// Provides methods to test that the
        /// <see cref="NumericalBin.
        /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class GetNumericalBins
        {
            /// <summary>
            /// Provides methods to test that the
            /// <see cref="NumericalBin.
            /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
            /// method terminates successfully when expected.
            public static class Succeed
            {
                /// Tests that the
                /// <see cref="NumericalBin.
                /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data length is 
                /// greater than 1.
                /// </summary>
                public static void DataLengthIsGreaterThanOne()
                {
                    var numericalData = DoubleMatrix.Dense(5, 1, 
                        [-1, -1, -1, 1, 1]);
                    var targetData = DoubleMatrix.Dense(5, 1, 
                        [10, 20, 10, 10, 10]);
                    var bins = NumericalBin.GetNumericalBins(
                        numericalData, 
                        targetData);

                    NumericalBinAssert.IsStateAsExpected(
                        target: bins[0],
                        expectedFirstPosition: 0,
                        expectedLastPosition: 2,
                        expectedFirstValue: -1,
                        expectedLastValue: -1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(2) { { 10, 2 }, { 20, 1 } });

                    NumericalBinAssert.IsStateAsExpected(
                        target: bins[1],
                        expectedFirstPosition: 3,
                        expectedLastPosition: 4,
                        expectedFirstValue: 1,
                        expectedLastValue: 1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(2) { { 10, 2 }, { 20, 0 } });
                }

                /// Tests that the
                /// <see cref="NumericalBin.
                /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data length is 1.
                /// </summary>
                public static void DataLengthIsOne()
                {
                    var numericalData = DoubleMatrix.Dense(1, 1, -1);
                    var targetData = DoubleMatrix.Dense(1, 1, 10);
                    var bins = NumericalBin.GetNumericalBins(
                        numericalData, 
                        targetData);

                    NumericalBinAssert.IsStateAsExpected(
                        target: bins[0],
                        expectedFirstPosition: 0,
                        expectedLastPosition: 0,
                        expectedFirstValue: -1,
                        expectedLastValue: -1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(1) { { 10, 1 } });
                }

                /// Tests that the
                /// <see cref="NumericalBin.
                /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and a final cut point exists.
                /// </summary>
                public static void FinalCutPointExists()
                {
                    var numericalData = DoubleMatrix.Dense(5, 1, 
                        [-1, -1, 1, 1, 2]);
                    var targetData = DoubleMatrix.Dense(5, 1, 
                        [10, 20, 10, 10, 10]);
                    var bins = NumericalBin.GetNumericalBins(
                        numericalData, 
                        targetData);

                    NumericalBinAssert.IsStateAsExpected(
                        target: bins[0],
                        expectedFirstPosition: 0,
                        expectedLastPosition: 1,
                        expectedFirstValue: -1,
                        expectedLastValue: -1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(2) { { 10, 1 }, { 20, 1 } });

                    NumericalBinAssert.IsStateAsExpected(
                        target: bins[1],
                        expectedFirstPosition: 2,
                        expectedLastPosition: 3,
                        expectedFirstValue: 1,
                        expectedLastValue: 1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(1) { { 10, 2 }, { 20, 0 } });

                    NumericalBinAssert.IsStateAsExpected(
                        target: bins[2],
                        expectedFirstPosition: 4,
                        expectedLastPosition: 4,
                        expectedFirstValue: 2,
                        expectedLastValue: 2,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(1) { { 10, 1 }, { 20, 0 } });
                }
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="NumericalBin.
            /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
            /// method fails when expected.
            public static class Fail
            {
                /// <summary>
                /// Provides methods to test that the
                /// <see cref="NumericalBin.
                /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
                /// method fails when numerical and target data
                /// have different lengths.
                public static void NumericalAndTargetDataHaveDifferentLengths()
                {
                    var numericalData = DoubleMatrix.Dense(5, 1, 
                        [-1, -1, -1, 1, 1]);
                    var targetData = DoubleMatrix.Dense(4, 1, 
                        [20, 10, 10, 10]);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var bins = NumericalBin.GetNumericalBins(
                                numericalData, 
                                targetData);
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_PAR_MUST_HAVE_SAME_COUNT"),
                                nameof(numericalData)),
                        expectedParameterName: "targetData");
                }

                /// <summary>
                /// Provides methods to test that the
                /// <see cref="NumericalBin.
                /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
                /// method fails when the target data is <b>null</b>.
                public static void TargetDataIsNull()
                {
                    var numericalData = DoubleMatrix.Dense(5, 1, 
                        [-1, -1, -1, 1, 1]);
                    DoubleMatrix targetData = null;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var bins = NumericalBin.GetNumericalBins(
                                numericalData, 
                                targetData);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "targetData");
                }

                /// <summary>
                /// Provides methods to test that the
                /// <see cref="NumericalBin.
                /// GetNumericalBins(DoubleMatrix, DoubleMatrix)"/>
                /// method fails when the numerical data is <b>null</b>.
                public static void NumericalDataIsNull()
                {
                    DoubleMatrix numericalData = null;
                    var targetData = DoubleMatrix.Dense(5, 1, 
                        [10, 20, 10, 10, 10]);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var bins = NumericalBin.GetNumericalBins(
                                numericalData, 
                                targetData);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "numericalData");
                }
            }
        }
    }
}
