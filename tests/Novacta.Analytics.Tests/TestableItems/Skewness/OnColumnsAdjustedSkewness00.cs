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
    class OnColumnsAdjustedSkewness00 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnColumnsAdjustedSkewness00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [1.58882231416295, -0.45617799047082, 0.0, -1.18822340003788, 0.0],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsAdjustedSkewness00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedSkewness00"/> class.</returns>
        public static OnColumnsAdjustedSkewness00 Get()
        {
            return new OnColumnsAdjustedSkewness00();
        }
    }
}