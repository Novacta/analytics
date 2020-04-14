// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsAdjustedVariance01 :
        AlongDimensionVariance<DoubleMatrixState>
    {
        protected OnColumnsAdjustedVariance01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5]
                                { 6.33333333333333, 5.58333333333333, 1.58333333333333, 0.91666666666667, 2.25 },
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsAdjustedVariance01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedVariance01"/> class.</returns>
        public static OnColumnsAdjustedVariance01 Get()
        {
            return new OnColumnsAdjustedVariance01();
        }
    }
}