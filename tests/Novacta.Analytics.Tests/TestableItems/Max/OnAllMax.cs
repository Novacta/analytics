// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllMax<TExpected> :
        OnAllExtremumOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="MaxOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected OnAllMax(
                    TExpected expected,
                    TestableDoubleMatrix data) :
            base(
                expected: expected,
                data: data,
                dataWritableOps:
                    [Stat.Max],
                dataReadOnlyOps:
                   [Stat.Max])
        {
        }
    }
}