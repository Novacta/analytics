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
    class OnAllAdjustedKurtosis00 :
        OnAllKurtosis<double>
    {
        protected OnAllAdjustedKurtosis00() :
                base(
                    expected: -1.268757875,
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedKurtosis00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedKurtosis00"/> class.</returns>
        public static OnAllAdjustedKurtosis00 Get()
        {
            return new OnAllAdjustedKurtosis00();
        }
    }
}
