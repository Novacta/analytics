// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsAdjustedKurtosis00 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnColumnsAdjustedKurtosis00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [2.912925906, -0.951989026, -0.593271627, 0.605226613, -1.2],
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
        /// Gets an instance of the <see cref="OnColumnsAdjustedKurtosis00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedKurtosis00"/> class.</returns>
        public static OnColumnsAdjustedKurtosis00 Get()
        {
            return new OnColumnsAdjustedKurtosis00();
        }
    }
}