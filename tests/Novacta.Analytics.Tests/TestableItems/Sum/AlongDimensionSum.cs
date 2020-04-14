// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionSum<TExpected> :
    AlongDimensionNotAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionSum" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected AlongDimensionSum(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DataOperation dataOperation) :
            base(
                expected: expected,
                data: data,
                dataOperation: dataOperation,
                dataWritableOps:
                    new Func<DoubleMatrix, DataOperation, DoubleMatrix>[1] { Stat.Sum },
                dataReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, DataOperation, DoubleMatrix>[1] { Stat.Sum })
        {
        }
    }
}