// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix47"/>.
    /// </summary>
    class OnAllMin04 :
        OnAllMin<IndexValuePair>
    {
        protected OnAllMin04() :
                base(
                    expected: new IndexValuePair() { index = 4, value = 0.0 },
                    data: TestableDoubleMatrix47.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMin04"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMin04"/> class.</returns>
        public static OnAllMin04 Get()
        {
            return new OnAllMin04();
        }
    }
}
