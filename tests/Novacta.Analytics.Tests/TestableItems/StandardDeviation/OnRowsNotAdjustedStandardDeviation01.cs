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
    class OnRowsNotAdjustedStandardDeviation01 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedStandardDeviation01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 0.97979589711327, 1.74355957741627, 1.74355957741627, 1.93907194296653 },
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsNotAdjustedStandardDeviation01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedStandardDeviation01"/> class.</returns>
        public static OnRowsNotAdjustedStandardDeviation01 Get()
        {
            return new OnRowsNotAdjustedStandardDeviation01();
        }
    }
}