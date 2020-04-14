// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllMin02 :
        OnAllMin<IndexValuePair>
    {
        protected OnAllMin02() :
                base(
                    expected: new IndexValuePair() { index = 0, value = 0 },
                    data: TestableDoubleMatrix42.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMin02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMin02"/> class.</returns>
        public static OnAllMin02 Get()
        {
            return new OnAllMin02();
        }
    }
}
