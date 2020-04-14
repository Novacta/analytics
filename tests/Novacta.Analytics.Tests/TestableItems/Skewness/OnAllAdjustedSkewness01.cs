// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllAdjustedSkewness01 :
        OnAllSkewness<double>
    {
        protected OnAllAdjustedSkewness01() :
                base(
                    expected: 1.05433799169834,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedSkewness01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedSkewness01"/> class.</returns>
        public static OnAllAdjustedSkewness01 Get()
        {
            return new OnAllAdjustedSkewness01();
        }
    }
}
