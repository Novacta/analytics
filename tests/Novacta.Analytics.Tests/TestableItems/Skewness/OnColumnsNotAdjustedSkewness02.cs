// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnColumnsNotAdjustedSkewness02 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedSkewness02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [double.NaN, double.NaN, double.NaN, double.NaN, double.NaN],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedSkewness02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedSkewness02"/> class.</returns>
        public static OnColumnsNotAdjustedSkewness02 Get()
        {
            return new OnColumnsNotAdjustedSkewness02();
        }
    }
}