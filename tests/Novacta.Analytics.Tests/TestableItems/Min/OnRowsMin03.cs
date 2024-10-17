// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix46"/>.
    /// </summary>
    class OnRowsMin03 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnRowsMin03() :
                base(
                    expected: [
                        new() { index = 2, value = -2.0 },
                        new() { index = 1, value = -1.0 }
                    ],
                    data: TestableDoubleMatrix46.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMin03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMin03"/> class.</returns>
        public static OnRowsMin03 Get()
        {
            return new OnRowsMin03();
        }
    }
}