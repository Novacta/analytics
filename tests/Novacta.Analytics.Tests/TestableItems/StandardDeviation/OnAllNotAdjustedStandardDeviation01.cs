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
    class OnAllNotAdjustedStandardDeviation01 :
        OnAllStandardDeviation<double>
    {
        protected OnAllNotAdjustedStandardDeviation01() :
                base(
                    expected: 1.66132477258362,
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedStandardDeviation01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedStandardDeviation01"/> class.</returns>
        public static OnAllNotAdjustedStandardDeviation01 Get()
        {
            return new OnAllNotAdjustedStandardDeviation01();
        }
    }
}
