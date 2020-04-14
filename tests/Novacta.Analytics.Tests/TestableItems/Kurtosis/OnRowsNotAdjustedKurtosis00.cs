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
    class OnRowsNotAdjustedKurtosis00 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedKurtosis00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { -1.647017961, -1.478306837, -1.072686515, -1.781751515 },
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
        /// Gets an instance of the <see cref="OnRowsNotAdjustedKurtosis00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedKurtosis00"/> class.</returns>
        public static OnRowsNotAdjustedKurtosis00 Get()
        {
            return new OnRowsNotAdjustedKurtosis00();
        }
    }
}