// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix46"/>.
    /// </summary>
    class OnRowsMax03 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnRowsMax03() :
                base(
                    expected: [
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix46.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMax03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMax03"/> class.</returns>
        public static OnRowsMax03 Get()
        {
            return new OnRowsMax03();
        }
    }
}