// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix50"/>.
    /// </summary>
    class OnAllMax01 :
        OnAllMax<IndexValuePair>
    {
        protected OnAllMax01() :
                base(
                    expected: new IndexValuePair() { index = 0, value = 1.0 },
                    data: TestableDoubleMatrix50.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMax01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMax01"/> class.</returns>
        public static OnAllMax01 Get()
        {
            return new OnAllMax01();
        }
    }
}
