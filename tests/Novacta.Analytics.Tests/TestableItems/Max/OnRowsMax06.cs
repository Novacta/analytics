// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix49"/>.
    /// </summary>
    class OnRowsMax06 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnRowsMax06() :
                base(
                    expected: new IndexValuePair[4] {
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 2, value = 1.0 },
                        new IndexValuePair() { index = 0, value = 2.0 },
                        new IndexValuePair() { index = 0, value = 0.0 }
                    },
                    data: TestableDoubleMatrix49.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMax06"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMax06"/> class.</returns>
        public static OnRowsMax06 Get()
        {
            return new OnRowsMax06();
        }
    }
}