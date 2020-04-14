// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllMean00 :
        OnAllMean<double>
    {
        protected OnAllMean00() :
                base(
                    expected: 6.80,
                    data: TestableDoubleMatrix40.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMean00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMean00"/> class.</returns>
        public static OnAllMean00 Get()
        {
            return new OnAllMean00();
        }
    }
}
