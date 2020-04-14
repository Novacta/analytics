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
    class OnRowsNotAdjustedSkewness00 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedSkewness00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 0.40715573530219, -0.25430276589620, 0.39875012280914, 0.32770086911199 },
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsNotAdjustedSkewness00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedSkewness00"/> class.</returns>
        public static OnRowsNotAdjustedSkewness00 Get()
        {
            return new OnRowsNotAdjustedSkewness00();
        }
    }
}