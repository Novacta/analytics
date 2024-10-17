// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsAdjustedKurtosis00 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnRowsAdjustedKurtosis00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [-2.588071842, -1.913227347, -0.29074606, -3.127006058],
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
        /// Gets an instance of the <see cref="OnRowsAdjustedKurtosis00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedKurtosis00"/> class.</returns>
        public static OnRowsAdjustedKurtosis00 Get()
        {
            return new OnRowsAdjustedKurtosis00();
        }
    }
}