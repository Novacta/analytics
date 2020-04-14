// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionKurtosis<TExpected> :
    AlongDimensionAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionKurtosis" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected AlongDimensionKurtosis(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    bool adjustForBias,
                    DataOperation dataOperation) :
            base(
                expected: expected,
                data: data,
                adjustForBias: adjustForBias,
                dataOperation: dataOperation,
                dataWritableOps:
                    new Func<DoubleMatrix, bool, DataOperation, DoubleMatrix>[1] { Stat.Kurtosis },
                dataReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, bool, DataOperation, DoubleMatrix>[1] { Stat.Kurtosis })
        {
        }
    }
}