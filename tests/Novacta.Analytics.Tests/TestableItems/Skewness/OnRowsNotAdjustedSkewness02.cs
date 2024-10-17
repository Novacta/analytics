// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnRowsNotAdjustedSkewness02 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedSkewness02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [double.NaN, double.NaN, double.NaN, double.NaN],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsNotAdjustedSkewness02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedSkewness02"/> class.</returns>
        public static OnRowsNotAdjustedSkewness02 Get()
        {
            return new OnRowsNotAdjustedSkewness02();
        }
    }
}