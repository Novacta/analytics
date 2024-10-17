// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsNotAdjustedVariance01 :
        AlongDimensionVariance<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedVariance01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [0.9600, 3.0400, 3.0400, 3.7600],
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
        /// Gets an instance of the <see cref="OnRowsNotAdjustedVariance01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedVariance01"/> class.</returns>
        public static OnRowsNotAdjustedVariance01 Get()
        {
            return new OnRowsNotAdjustedVariance01();
        }
    }
}