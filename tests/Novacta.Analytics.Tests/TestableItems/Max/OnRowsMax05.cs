// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix48"/>.
    /// </summary>
    class OnRowsMax05 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnRowsMax05() :
                base(
                    expected: new IndexValuePair[4] {
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 1, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 }
                    },
                    data: TestableDoubleMatrix48.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMax05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMax05"/> class.</returns>
        public static OnRowsMax05 Get()
        {
            return new OnRowsMax05();
        }
    }
}