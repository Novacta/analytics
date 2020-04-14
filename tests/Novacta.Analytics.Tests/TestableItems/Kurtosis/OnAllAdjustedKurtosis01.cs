// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllAdjustedKurtosis01 :
        OnAllKurtosis<double>
    {
        protected OnAllAdjustedKurtosis01() :
                base(
                    expected: 0.92961192,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedKurtosis01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedKurtosis01"/> class.</returns>
        public static OnAllAdjustedKurtosis01 Get()
        {
            return new OnAllAdjustedKurtosis01();
        }
    }
}
