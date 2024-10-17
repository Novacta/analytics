// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnColumnsAdjustedKurtosis02 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnColumnsAdjustedKurtosis02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [double.NaN, double.NaN, double.NaN, double.NaN, double.NaN],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsAdjustedKurtosis02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedKurtosis02"/> class.</returns>
        public static OnColumnsAdjustedKurtosis02 Get()
        {
            return new OnColumnsAdjustedKurtosis02();
        }
    }
}