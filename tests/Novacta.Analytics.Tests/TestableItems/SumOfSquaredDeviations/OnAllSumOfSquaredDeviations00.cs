// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllSumOfSquaredDeviations00 :
        OnAllSumOfSquaredDeviations<double>
    {
        protected OnAllSumOfSquaredDeviations00() :
                base(
                    expected: 513.2,
                    data: TestableDoubleMatrix40.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllSumOfSquaredDeviations00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllSumOfSquaredDeviations00"/> class.</returns>
        public static OnAllSumOfSquaredDeviations00 Get()
        {
            return new OnAllSumOfSquaredDeviations00();
        }
    }
}
