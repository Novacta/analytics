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
    class OnColumnsNotAdjustedKurtosis00 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedKurtosis00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5] 
                                { -0.811609879, -1.32693187, -1.279102884, -1.119303118, -1.36 },
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
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedKurtosis00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedKurtosis00"/> class.</returns>
        public static OnColumnsNotAdjustedKurtosis00 Get()
        {
            return new OnColumnsNotAdjustedKurtosis00();
        }
    }
}