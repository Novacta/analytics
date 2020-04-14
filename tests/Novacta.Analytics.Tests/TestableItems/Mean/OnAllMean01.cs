// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllMean01 :
        OnAllMean<double>
    {
        protected OnAllMean01() :
                base(
                    expected: .8,
                    data: TestableDoubleMatrix41.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMean01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMean01"/> class.</returns>
        public static OnAllMean01 Get()
        {
            return new OnAllMean01();
        }
    }
}
