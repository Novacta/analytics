// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllSumOfSquaredDeviations01 :
        OnAllSumOfSquaredDeviations<double>
    {
        protected OnAllSumOfSquaredDeviations01() :
                base(
                    expected: 55.2,
                    data: TestableDoubleMatrix41.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllSumOfSquaredDeviations01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllSumOfSquaredDeviations01"/> class.</returns>
        public static OnAllSumOfSquaredDeviations01 Get()
        {
            return new OnAllSumOfSquaredDeviations01();
        }
    }
}
