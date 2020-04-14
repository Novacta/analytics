// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsAdjustedStandardDeviation01 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnRowsAdjustedStandardDeviation01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 1.09544511501033, 1.94935886896179, 1.94935886896179, 2.16794833886788 },
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedStandardDeviation01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedStandardDeviation01"/> class.</returns>
        public static OnRowsAdjustedStandardDeviation01 Get()
        {
            return new OnRowsAdjustedStandardDeviation01();
        }
    }
}