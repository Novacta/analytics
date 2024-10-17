// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix49"/>.
    /// </summary>
    class OnColumnsMax06 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsMax06() :
                base(
                    expected: [
                        new() { index = 2, value = 2.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 1, value = 1.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix49.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMax06"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMax06"/> class.</returns>
        public static OnColumnsMax06 Get()
        {
            return new OnColumnsMax06();
        }
    }
}