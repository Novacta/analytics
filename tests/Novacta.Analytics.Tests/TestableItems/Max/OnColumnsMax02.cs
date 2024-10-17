// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnColumnsMax02 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsMax02() :
                base(
                    expected: [
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix42.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMax02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMax02"/> class.</returns>
        public static OnColumnsMax02 Get()
        {
            return new OnColumnsMax02();
        }
    }
}