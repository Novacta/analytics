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
    /// about <see cref="NumericalBlock"/> 
    /// instances.
    /// </summary>
    static class NumericalBlockTest
    {
        /// <summary>
        /// Provides methods to test that the
        /// <see cref="NumericalBlock.
        /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
        /// method has
        /// been properly implemented.
        /// </summary>
        public static class GetNumericalBlocks
        {
            /// <summary>
            /// Provides methods to test that the
            /// <see cref="NumericalBlock.
            /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
            /// method terminates successfully when expected.
            public static class Succeed
            {
                /// Tests that the
                /// <see cref="NumericalBlock.
                /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data length is 
                /// greater than 1.
                /// </summary>
                public static void DataLengthIsGreaterThanOne()
                {
                    var numericalData = DoubleMatrix.Dense(7, 1, 
                        [1, 1, 1, 2, 2, 3, 4]);
                    var targetData = DoubleMatrix.Dense(7, 1, 
                        [10, 10, 10, 10, 10, 10, 5]);
                    var blocks = NumericalBlock.GetNumericalBlocks(
                        numericalData, 
                        targetData);

                    NumericalBlockAssert.IsStateAsExpected(
                        target: blocks[0],
                        expectedFirstPosition: 0,
                        expectedLastPosition: 5,
                        expectedFirstValue: 1,
                        expectedLastValue: 3,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(2) { { 5, 0 }, { 10, 6 } });

                    NumericalBlockAssert.IsStateAsExpected(
                        target: blocks[1],
                        expectedFirstPosition: 6,
                        expectedLastPosition: 6,
                        expectedFirstValue: 4,
                        expectedLastValue: 4,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(2) { { 5, 1 }, { 10, 0 } });
                }

                /// Tests that the
                /// <see cref="NumericalBlock.
                /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data length is 1.
                /// </summary>
                public static void DataLengthIsOne()
                {
                    var numericalData = DoubleMatrix.Dense(1, 1, -1);
                    var targetData = DoubleMatrix.Dense(1, 1, 10);
                    var blocks = NumericalBlock.GetNumericalBlocks(
                        numericalData, 
                        targetData);

                    NumericalBlockAssert.IsStateAsExpected(
                        target: blocks[0],
                        expectedFirstPosition: 0,
                        expectedLastPosition: 0,
                        expectedFirstValue: -1,
                        expectedLastValue: -1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(1) { { 10, 1 } });
                }

                /// Tests that the
                /// <see cref="NumericalBlock.
                /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and a final cut point exists.
                /// </summary>
                public static void FinalCutPointExists()
                {
                    var numericalData = DoubleMatrix.Dense(5, 1, 
                        [-1, -1, 1, 1, 2]);
                    var targetData = DoubleMatrix.Dense(5, 1, 
                        [10, 20, 10, 10, 10]);
                    var blocks = NumericalBlock.GetNumericalBlocks(
                        numericalData, 
                        targetData);

                    NumericalBlockAssert.IsStateAsExpected(
                        target: blocks[0],
                        expectedFirstPosition: 0,
                        expectedLastPosition: 1,
                        expectedFirstValue: -1,
                        expectedLastValue: -1,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(2) { { 10, 1 }, { 20, 1 } });

                    NumericalBlockAssert.IsStateAsExpected(
                        target: blocks[1],
                        expectedFirstPosition: 2,
                        expectedLastPosition: 4,
                        expectedFirstValue: 1,
                        expectedLastValue: 2,
                        expectedTargetFrequencyDistribution:
                            new Dictionary<double, int>(1) { { 10, 3 }, { 20, 0 } });
                }
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="NumericalBlock.
            /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
            /// method fails when expected.
            public static class Fail
            {
                /// <summary>
                /// Provides methods to test that the
                /// <see cref="NumericalBlock.
                /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
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
                            var blocks = NumericalBlock.GetNumericalBlocks(
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
                /// <see cref="NumericalBlock.
                /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
                /// method fails when the target data is <b>null</b>.
                public static void TargetDataIsNull()
                {
                    var numericalData = DoubleMatrix.Dense(5, 1, 
                        [-1, -1, -1, 1, 1]);
                    DoubleMatrix targetData = null;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var blocks = NumericalBlock.GetNumericalBlocks(
                                numericalData, 
                                targetData);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "targetData");
                }

                /// <summary>
                /// Provides methods to test that the
                /// <see cref="NumericalBlock.
                /// GetNumericalBlocks(DoubleMatrix, DoubleMatrix)"/>
                /// method fails when the numerical data is <b>null</b>.
                public static void NumericalDataIsNull()
                {
                    DoubleMatrix numericalData = null;
                    var targetData = DoubleMatrix.Dense(5, 1, 
                        [10, 20, 10, 10, 10]);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var blocks = NumericalBlock.GetNumericalBlocks(
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
