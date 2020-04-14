// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllAdjustedVariance01 :
        OnAllVariance<double>
    {
        protected OnAllAdjustedVariance01() :
                base(
                    expected: 2.90526315789474,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedVariance01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedVariance01"/> class.</returns>
        public static OnAllAdjustedVariance01 Get()
        {
            return new OnAllAdjustedVariance01();
        }
    }
}
