// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix48"/>.
    /// </summary>
    class OnColumnsMax05 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsMax05() :
                base(
                    expected: new IndexValuePair[5] {
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 }
                    },
                    data: TestableDoubleMatrix48.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMax05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMax05"/> class.</returns>
        public static OnColumnsMax05 Get()
        {
            return new OnColumnsMax05();
        }
    }
}