// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsAdjustedSkewness00 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsAdjustedSkewness00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [0.60695193437640, -0.37909218094008, 0.59442158709175, 0.48850761308011],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedSkewness00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedSkewness00"/> class.</returns>
        public static OnRowsAdjustedSkewness00 Get()
        {
            return new OnRowsAdjustedSkewness00();
        }
    }
}