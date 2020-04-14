// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllAdjustedStandardDeviation01 :
        OnAllStandardDeviation<double>
    {
        protected OnAllAdjustedStandardDeviation01() :
                base(
                    expected: 1.70448325245358,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllAdjustedStandardDeviation01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllAdjustedStandardDeviation01"/> class.</returns>
        public static OnAllAdjustedStandardDeviation01 Get()
        {
            return new OnAllAdjustedStandardDeviation01();
        }
    }
}
