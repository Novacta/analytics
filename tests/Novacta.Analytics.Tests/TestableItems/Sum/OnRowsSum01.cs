// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsSum01 :
        AlongDimensionSum<DoubleMatrixState>
    {
        protected OnRowsSum01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [4, 3, 3, 6],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsSum01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsSum01"/> class.</returns>
        public static OnRowsSum01 Get()
        {
            return new OnRowsSum01();
        }
    }
}