// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsNotAdjustedStandardDeviation00 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedStandardDeviation00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [6.11228271597445, 3.00665927567458, 3.92937654087770, 6.22896460095897],
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
        /// Gets an instance of the <see cref="OnRowsNotAdjustedStandardDeviation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedStandardDeviation00"/> class.</returns>
        public static OnRowsNotAdjustedStandardDeviation00 Get()
        {
            return new OnRowsNotAdjustedStandardDeviation00();
        }
    }
}