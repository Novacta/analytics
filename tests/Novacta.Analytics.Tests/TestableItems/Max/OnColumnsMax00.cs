// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsMax00 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsMax00() :
                base(
                    expected: new IndexValuePair[5] {
                        new IndexValuePair() { index = 0, value = 16.0 },
                        new IndexValuePair() { index = 3, value = 14.0 },
                        new IndexValuePair() { index = 3, value = 15.0 },
                        new IndexValuePair() { index = 0, value = 13.0 },
                        new IndexValuePair() { index = 0, value = 4.0 }
                    },
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMax00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMax00"/> class.</returns>
        public static OnColumnsMax00 Get()
        {
            return new OnColumnsMax00();
        }
    }
}