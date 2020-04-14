// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix49"/>.
    /// </summary>
    class OnAllMax06 :
        OnAllMax<IndexValuePair>
    {
        protected OnAllMax06() :
                base(
                    expected: new IndexValuePair() { index = 2, value = 2.0 },
                    data: TestableDoubleMatrix49.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMax06"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMax06"/> class.</returns>
        public static OnAllMax06 Get()
        {
            return new OnAllMax06();
        }
    }
}