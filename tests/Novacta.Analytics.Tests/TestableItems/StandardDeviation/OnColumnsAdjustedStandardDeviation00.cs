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
    class OnColumnsAdjustedStandardDeviation00 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnColumnsAdjustedStandardDeviation00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5] 
                                { 6.55743852430200, 5.19615242270663, 5.94418483337567, 5.44671154612273, 1.29099444873581 },
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
        /// Gets an instance of the <see cref="OnColumnsAdjustedStandardDeviation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedStandardDeviation00"/> class.</returns>
        public static OnColumnsAdjustedStandardDeviation00 Get()
        {
            return new OnColumnsAdjustedStandardDeviation00();
        }
    }
}