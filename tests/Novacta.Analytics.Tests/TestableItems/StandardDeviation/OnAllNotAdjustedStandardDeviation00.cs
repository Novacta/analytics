// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllNotAdjustedStandardDeviation00 :
        OnAllStandardDeviation<double>
    {
        protected OnAllNotAdjustedStandardDeviation00() : 
                base(
                    expected: 5.06557005676558, 
                    data: TestableDoubleMatrix40.Get(), 
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedStandardDeviation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedStandardDeviation00"/> class.</returns>
        public static OnAllNotAdjustedStandardDeviation00 Get()
        {
            return new OnAllNotAdjustedStandardDeviation00();
        }
    }
}
