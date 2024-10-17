// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsMin01 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnColumnsMin01() :
                base(
                    expected: [
                        new() { index = 2, value = -2.0 },
                        new() { index = 1, value = 0.0 },
                        new() { index = 1, value = -1.0 },
                        new() { index = 1, value = 0.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMin01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMin01"/> class.</returns>
        public static OnColumnsMin01 Get()
        {
            return new OnColumnsMin01();
        }
    }
}