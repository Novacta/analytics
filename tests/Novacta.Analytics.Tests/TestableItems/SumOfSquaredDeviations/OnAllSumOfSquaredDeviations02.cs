// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllSumOfSquaredDeviations02 :
        OnAllSumOfSquaredDeviations<double>
    {
        protected OnAllSumOfSquaredDeviations02() :
                base(
                    expected: 0.0,
                    data: TestableDoubleMatrix42.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllSumOfSquaredDeviations02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllSumOfSquaredDeviations02"/> class.</returns>
        public static OnAllSumOfSquaredDeviations02 Get()
        {
            return new OnAllSumOfSquaredDeviations02();
        }
    }
}
