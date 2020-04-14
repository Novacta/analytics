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
    class OnAllAdjustedStandardDeviation00 :
        OnAllStandardDeviation<double>
    {
        protected OnAllAdjustedStandardDeviation00() :
                base(
                    expected: 5.19716521921225,
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedStandardDeviation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedStandardDeviation00"/> class.</returns>
        public static OnAllAdjustedStandardDeviation00 Get()
        {
            return new OnAllAdjustedStandardDeviation00();
        }
    }
}
