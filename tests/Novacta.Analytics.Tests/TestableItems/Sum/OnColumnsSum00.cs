// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsSum00 :
        AlongDimensionSum<DoubleMatrixState>
    {
        protected OnColumnsSum00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [26, 34, 32, 34, 10],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsSum00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsSum00"/> class.</returns>
        public static OnColumnsSum00 Get()
        {
            return new OnColumnsSum00();
        }
    }
}