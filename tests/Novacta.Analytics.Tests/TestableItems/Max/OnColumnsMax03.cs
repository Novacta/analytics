// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix46"/>.
    /// </summary>
    class OnColumnsMax03 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsMax03() :
                base(
                    expected: new IndexValuePair[3] {
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 1, value = 0.0 }
                    },
                    data: TestableDoubleMatrix46.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMax03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMax03"/> class.</returns>
        public static OnColumnsMax03 Get()
        {
            return new OnColumnsMax03();
        }
    }
}