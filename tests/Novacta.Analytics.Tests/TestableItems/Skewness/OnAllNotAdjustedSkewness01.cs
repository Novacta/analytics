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
    class OnAllNotAdjustedSkewness01 :
        OnAllSkewness<double>
    {
        protected OnAllNotAdjustedSkewness01() :
                base(
                    expected: 0.97355515973709,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedSkewness01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedSkewness01"/> class.</returns>
        public static OnAllNotAdjustedSkewness01 Get()
        {
            return new OnAllNotAdjustedSkewness01();
        }
    }
}
