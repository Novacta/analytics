// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix47"/>.
    /// </summary>
    class OnColumnsMin04 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnColumnsMin04() :
                base(
                    expected: new IndexValuePair[5] {
                        new IndexValuePair() { index = 2, value = 1.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 1.0 },
                        new IndexValuePair() { index = 3, value = 1.0 },
                        new IndexValuePair() { index = 3, value = 0.0 }
                    },
                    data: TestableDoubleMatrix47.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMin04"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMin04"/> class.</returns>
        public static OnColumnsMin04 Get()
        {
            return new OnColumnsMin04();
        }
    }
}