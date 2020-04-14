// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllMin01 :
        OnAllMin<IndexValuePair>
    {
        protected OnAllMin01() :
                base(
                    expected: new IndexValuePair() { index = 2, value = -2.0 },
                    data: TestableDoubleMatrix41.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMin01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMin01"/> class.</returns>
        public static OnAllMin01 Get()
        {
            return new OnAllMin01();
        }
    }
}
