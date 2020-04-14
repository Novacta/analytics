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
    class OnAllAdjustedVariance00 :
        OnAllVariance<double>
    {
        protected OnAllAdjustedVariance00() :
                base(
                    expected: 27.01052631578947,
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedVariance00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedVariance00"/> class.</returns>
        public static OnAllAdjustedVariance00 Get()
        {
            return new OnAllAdjustedVariance00();
        }
    }
}
