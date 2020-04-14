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
    class OnRowsAdjustedStandardDeviation00 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnRowsAdjustedStandardDeviation00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 6.83373982530795, 3.36154726279432, 4.39317652729776, 6.96419413859206 },
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
        /// Gets an instance of the <see cref="OnRowsAdjustedStandardDeviation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedStandardDeviation00"/> class.</returns>
        public static OnRowsAdjustedStandardDeviation00 Get()
        {
            return new OnRowsAdjustedStandardDeviation00();
        }
    }
}