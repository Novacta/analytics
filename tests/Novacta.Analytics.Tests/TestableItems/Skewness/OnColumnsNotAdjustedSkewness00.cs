// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsNotAdjustedSkewness00 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedSkewness00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [0.91730699077646, -0.26337448559671, 0.0, -0.68602109986928, 0.0],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedSkewness00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedSkewness00"/> class.</returns>
        public static OnColumnsNotAdjustedSkewness00 Get()
        {
            return new OnColumnsNotAdjustedSkewness00();
        }
    }
}