// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix51"/>.
    /// </summary>
    class OnRowsMax04 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnRowsMax04() :
                base(
                    expected: new IndexValuePair[4] {
                        new IndexValuePair() { index = 1, value = 0.0 },
                        new IndexValuePair() { index = 4, value = -3.0 },
                        new IndexValuePair() { index = 0, value = -1.0 },
                        new IndexValuePair() { index = 4, value = 0.0 }
                    },
                    data: TestableDoubleMatrix51.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMax04"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMax04"/> class.</returns>
        public static OnRowsMax04 Get()
        {
            return new OnRowsMax04();
        }
    }
}