// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionStandardDeviation<TExpected> :
    AlongDimensionAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionStandardDeviation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected AlongDimensionStandardDeviation(
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
                    new Func<DoubleMatrix, bool, DataOperation, DoubleMatrix>[1] { Stat.StandardDeviation },
                dataReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, bool, DataOperation, DoubleMatrix>[1] { Stat.StandardDeviation })
        {
        }
    }
}