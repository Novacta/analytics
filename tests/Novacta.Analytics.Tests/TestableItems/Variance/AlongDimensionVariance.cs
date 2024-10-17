// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionVariance<TExpected> :
    AlongDimensionAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionVariance" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected AlongDimensionVariance(
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
                    [Stat.Variance],
                dataReadOnlyOps:
                    [Stat.Variance])
        {
        }
    }
}