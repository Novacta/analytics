// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllQuantile<TExpected> :
        OnAllQuantileOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="QuantileOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="probabilities">The probabilities.</param>
        protected OnAllQuantile(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DoubleMatrix probabilities) :
            base(
                expected: expected,
                data: data,
                probabilities: probabilities,
                dataWritableOps:
                    [Stat.Quantile],
                dataReadOnlyOps:
                   [Stat.Quantile])
        {
        }
    }
}