// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix48"/>.
    /// </summary>
    class OnAllMin05 :
        OnAllMin<IndexValuePair>
    {
        protected OnAllMin05() :
                base(
                    expected: new IndexValuePair() { index = 2, value = -2.0 },
                    data: TestableDoubleMatrix48.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMin05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMin05"/> class.</returns>
        public static OnAllMin05 Get()
        {
            return new OnAllMin05();
        }
    }
}
