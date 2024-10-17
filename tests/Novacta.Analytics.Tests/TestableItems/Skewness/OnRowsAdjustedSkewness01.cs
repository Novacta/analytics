// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsAdjustedSkewness01 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsAdjustedSkewness01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [0.60858061945018, 1.94395898289819, -0.08099829095409, 2.03153149002105],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedSkewness01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedSkewness01"/> class.</returns>
        public static OnRowsAdjustedSkewness01 Get()
        {
            return new OnRowsAdjustedSkewness01();
        }
    }
}