// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix46"/>.
    /// </summary>
    class OnAllMin03 :
        OnAllMin<IndexValuePair>
    {
        protected OnAllMin03() :
                base(
                    expected: new IndexValuePair() { index = 4, value = -2.0 },
                    data: TestableDoubleMatrix46.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMin03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMin03"/> class.</returns>
        public static OnAllMin03 Get()
        {
            return new OnAllMin03();
        }
    }
}
