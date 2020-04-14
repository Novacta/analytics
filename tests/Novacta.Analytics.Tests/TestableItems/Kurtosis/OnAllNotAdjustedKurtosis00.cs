// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllNotAdjustedKurtosis00 :
        OnAllKurtosis<double>
    {
        protected OnAllNotAdjustedKurtosis00() : 
                base(
                    expected: -1.258746641, 
                    data: TestableDoubleMatrix40.Get(), 
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedKurtosis00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedKurtosis00"/> class.</returns>
        public static OnAllNotAdjustedKurtosis00 Get()
        {
            return new OnAllNotAdjustedKurtosis00();
        }
    }
}
