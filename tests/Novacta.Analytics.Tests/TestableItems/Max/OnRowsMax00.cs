// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsMax00 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnRowsMax00() :
                base(
                    expected: new IndexValuePair[4] {
                        new IndexValuePair() { index = 0, value = 16.0 },
                        new IndexValuePair() { index = 1, value = 11.0 },
                        new IndexValuePair() { index = 3, value = 12.0 },
                        new IndexValuePair() { index = 2, value = 15.0 }
                    },
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMax00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMax00"/> class.</returns>
        public static OnRowsMax00 Get()
        {
            return new OnRowsMax00();
        }
    }
}