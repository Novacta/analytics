// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllSumOfSquaredDeviations<TExpected> :
        OnAllNotAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="SumOfSquaredDeviationsOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected OnAllSumOfSquaredDeviations(
                    TExpected expected,
                    TestableDoubleMatrix data) :
            base(
                expected: expected,
                data: data,
                dataWritableOps:
                    [Stat.SumOfSquaredDeviations],
                dataReadOnlyOps:
                   [Stat.SumOfSquaredDeviations])
        {
        }
    }
}