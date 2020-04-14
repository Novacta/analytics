// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnColumnsMin02 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnColumnsMin02() :
                base(
                    expected: new IndexValuePair[5] {
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 }
                    },
                    data: TestableDoubleMatrix42.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMin02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMin02"/> class.</returns>
        public static OnColumnsMin02 Get()
        {
            return new OnColumnsMin02();
        }
    }
}