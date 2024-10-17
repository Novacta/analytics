// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionMax<TExpected> :
    AlongDimensionExtremumOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionMax" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected AlongDimensionMax(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DataOperation dataOperation) :
            base(
                expected: expected,
                data: data,
                dataOperation: dataOperation,
                dataWritableOps:
                    [Stat.Max],
                dataReadOnlyOps:
                    [Stat.Max])
        {
        }
    }
}