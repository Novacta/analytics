// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix46"/>.
    /// </summary>
    class OnAllMax03 :
        OnAllMax<IndexValuePair>
    {
        protected OnAllMax03() :
                base(
                    expected: new IndexValuePair() { index = 0, value = 0.0 },
                    data: TestableDoubleMatrix46.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMax03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMax03"/> class.</returns>
        public static OnAllMax03 Get()
        {
            return new OnAllMax03();
        }
    }
}
