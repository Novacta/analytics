// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllNotAdjustedSkewness02 :
        OnAllSkewness<double>
    {
        protected OnAllNotAdjustedSkewness02() :
                base(
                    expected: Double.NaN,
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedSkewness02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedSkewness02"/> class.</returns>
        public static OnAllNotAdjustedSkewness02 Get()
        {
            return new OnAllNotAdjustedSkewness02();
        }
    }
}
