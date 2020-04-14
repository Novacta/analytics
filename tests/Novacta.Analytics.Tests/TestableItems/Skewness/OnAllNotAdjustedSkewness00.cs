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
    class OnAllNotAdjustedSkewness00 :
        OnAllSkewness<double>
    {
        protected OnAllNotAdjustedSkewness00() : 
                base(
                    expected: 0.41700989338505, 
                    data: TestableDoubleMatrix40.Get(), 
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedSkewness00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedSkewness00"/> class.</returns>
        public static OnAllNotAdjustedSkewness00 Get()
        {
            return new OnAllNotAdjustedSkewness00();
        }
    }
}
