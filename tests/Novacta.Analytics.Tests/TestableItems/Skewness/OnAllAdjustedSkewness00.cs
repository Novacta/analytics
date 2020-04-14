// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllAdjustedSkewness00 :
        OnAllSkewness<double>
    {
        protected OnAllAdjustedSkewness00() :
                base(
                    expected: 0.45161218561942,
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedSkewness00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedSkewness00"/> class.</returns>
        public static OnAllAdjustedSkewness00 Get()
        {
            return new OnAllAdjustedSkewness00();
        }
    }
}
