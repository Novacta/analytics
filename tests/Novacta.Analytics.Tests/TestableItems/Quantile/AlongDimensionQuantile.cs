// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionQuantile<TExpected> :
    AlongDimensionQuantileOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="AlongDimensionQuantile" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="probabilities">The probabilities.</param>
        /// <param name="dataOperation">The data operation.</param>
        protected AlongDimensionQuantile(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DoubleMatrix probabilities,
                    DataOperation dataOperation) :
            base(
                expected: expected,
                data: data,
                probabilities: probabilities,
                dataOperation: dataOperation,
                dataWritableOps:
                    [Stat.Quantile],
                dataReadOnlyOps:
                    [Stat.Quantile])
        {
        }
    }
}