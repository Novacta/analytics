// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllMean<TExpected> :
        OnAllNotAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="MeanOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected OnAllMean(
                    TExpected expected,
                    TestableDoubleMatrix data) :
            base(
                expected: expected,
                data: data,
                dataWritableOps:
                    new Func<DoubleMatrix, double>[1] { Stat.Mean },
                dataReadOnlyOps:
                   new Func<ReadOnlyDoubleMatrix, double>[1] { Stat.Mean })
        {
        }
    }
}