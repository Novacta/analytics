// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsNotAdjustedStandardDeviation00 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedStandardDeviation00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [5.67890834580027, 4.5, 5.1478150704935, 4.71699056602830, 1.11803398874989],
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
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedStandardDeviation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedStandardDeviation00"/> class.</returns>
        public static OnColumnsNotAdjustedStandardDeviation00 Get()
        {
            return new OnColumnsNotAdjustedStandardDeviation00();
        }
    }
}