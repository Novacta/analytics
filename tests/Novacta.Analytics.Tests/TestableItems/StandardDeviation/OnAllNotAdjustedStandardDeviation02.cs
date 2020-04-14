// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllNotAdjustedStandardDeviation02 :
        OnAllStandardDeviation<double>
    {
        protected OnAllNotAdjustedStandardDeviation02() :
                base(
                    expected: 0.0,
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedStandardDeviation02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedStandardDeviation02"/> class.</returns>
        public static OnAllNotAdjustedStandardDeviation02 Get()
        {
            return new OnAllNotAdjustedStandardDeviation02();
        }
    }
}
