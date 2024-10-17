// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsMin00 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnRowsMin00() :
                base(
                    expected: [
                        new() { index = 2, value = 1.0 },
                        new() { index = 4, value = 3.0 },
                        new() { index = 0, value = 1.0 },
                        new() { index = 3, value = 1.0 }
                    ],
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMin00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMin00"/> class.</returns>
        public static OnRowsMin00 Get()
        {
            return new OnRowsMin00();
        }
    }
}