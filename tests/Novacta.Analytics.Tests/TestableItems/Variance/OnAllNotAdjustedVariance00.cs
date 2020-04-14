// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllNotAdjustedVariance00 :
        OnAllVariance<double>
    {
        protected OnAllNotAdjustedVariance00() : 
                base(
                    expected: 25.66, 
                    data: TestableDoubleMatrix40.Get(), 
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedVariance00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedVariance00"/> class.</returns>
        public static OnAllNotAdjustedVariance00 Get()
        {
            return new OnAllNotAdjustedVariance00();
        }
    }
}
