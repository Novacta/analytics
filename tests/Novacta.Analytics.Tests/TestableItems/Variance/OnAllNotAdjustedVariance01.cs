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
    class OnAllNotAdjustedVariance01 :
        OnAllVariance<double>
    {
        protected OnAllNotAdjustedVariance01() :
                base(
                    expected: 2.76,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedVariance01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedVariance01"/> class.</returns>
        public static OnAllNotAdjustedVariance01 Get()
        {
            return new OnAllNotAdjustedVariance01();
        }
    }
}
