// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix51"/>.
    /// </summary>
    class OnAllMax04 :
        OnAllMax<IndexValuePair>
    {
        protected OnAllMax04() :
                base(
                    expected: new IndexValuePair() { index = 4, value = 0.0 },
                    data: TestableDoubleMatrix51.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMax04"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMax04"/> class.</returns>
        public static OnAllMax04 Get()
        {
            return new OnAllMax04();
        }
    }
}
