// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllMin<TExpected> :
        OnAllExtremumOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="MinOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected OnAllMin(
                    TExpected expected,
                    TestableDoubleMatrix data) :
            base(
                expected: expected,
                data: data,
                dataWritableOps:
                    [Stat.Min],
                dataReadOnlyOps:
                   [Stat.Min])
        {
        }
    }
}