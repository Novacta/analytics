// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a testable max which summarizes
    /// all column items in the matrix represented
    /// by <see cref="TestableDoubleMatrix50"/>.
    /// </summary>
    class OnColumnsMax01 :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsMax01() :
                base(
                    expected: [
                        new() { index = 0, value = 1.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 1, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix50.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMax01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMax01"/> class.</returns>
        public static OnColumnsMax01 Get()
        {
            return new OnColumnsMax01();
        }
    }
}