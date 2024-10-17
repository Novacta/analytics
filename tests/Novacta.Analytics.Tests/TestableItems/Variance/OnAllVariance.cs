// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllVariance<TExpected> :
        OnAllAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="VarianceOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="adjustForBias">A value to signal if the operation is 
        /// adjusted for bias.</param>
        protected OnAllVariance(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    bool adjustForBias) :
            base(
                expected: expected,
                data: data,
                adjustForBias: adjustForBias,
                dataWritableOps:
                    [Stat.Variance],
                dataReadOnlyOps:
                   [Stat.Variance])
        {
        }
    }
}