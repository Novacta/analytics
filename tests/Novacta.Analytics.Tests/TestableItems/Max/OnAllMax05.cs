// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix48"/>.
    /// </summary>
    class OnAllMax05 :
        OnAllMax<IndexValuePair>
    {
        protected OnAllMax05() :
                base(
                    expected: new IndexValuePair() { index = 0, value = 0.0 },
                    data: TestableDoubleMatrix48.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMax05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMax05"/> class.</returns>
        public static OnAllMax05 Get()
        {
            return new OnAllMax05();
        }
    }
}
