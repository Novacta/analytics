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
    class OnRowsAdjustedVariance01 :
        AlongDimensionVariance<DoubleMatrixState>
    {
        protected OnRowsAdjustedVariance01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 1.2000, 3.8000, 3.8000, 4.7000 },
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
        /// Gets an instance of the <see cref="OnRowsAdjustedVariance01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedVariance01"/> class.</returns>
        public static OnRowsAdjustedVariance01 Get()
        {
            return new OnRowsAdjustedVariance01();
        }
    }
}