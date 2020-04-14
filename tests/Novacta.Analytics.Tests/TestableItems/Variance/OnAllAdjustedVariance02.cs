// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllAdjustedVariance02 :
        OnAllVariance<double>
    {
        protected OnAllAdjustedVariance02() :
                base(
                    expected: 0.0,
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedVariance02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedVariance02"/> class.</returns>
        public static OnAllAdjustedVariance02 Get()
        {
            return new OnAllAdjustedVariance02();
        }
    }
}
