// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix49"/>.
    /// </summary>
    class OnAllMin06 :
        OnAllMin<IndexValuePair>
    {
        protected OnAllMin06() :
                base(
                    expected: new IndexValuePair() { index = 0, value = 0.0 },
                    data: TestableDoubleMatrix49.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllMin06"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllMin06"/> class.</returns>
        public static OnAllMin06 Get()
        {
            return new OnAllMin06();
        }
    }
}