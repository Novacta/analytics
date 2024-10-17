// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsMin01 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnRowsMin01() :
                base(
                    expected: [
                        new() { index = 0, value = 0.0 },
                        new() { index = 2, value = -1.0 },
                        new() { index = 0, value = -2.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMin01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMin01"/> class.</returns>
        public static OnRowsMin01 Get()
        {
            return new OnRowsMin01();
        }
    }
}