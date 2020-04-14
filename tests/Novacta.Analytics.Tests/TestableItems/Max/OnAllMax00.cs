// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllMax00 :
        OnAllMax<IndexValuePair>
    {
        protected OnAllMax00() :
                base(
                    expected: new IndexValuePair() { index = 0, value = 16.0 },
                    data: TestableDoubleMatrix40.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMax00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMax00"/> class.</returns>
        public static OnAllMax00 Get()
        {
            return new OnAllMax00();
        }
    }
}
