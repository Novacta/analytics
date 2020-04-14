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
    class OnColumnsNotAdjustedVariance01 :
        AlongDimensionVariance<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedVariance01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5]
                                { 4.7500, 4.1875, 1.1875, 0.6875, 1.6875 },
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedVariance01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedVariance01"/> class.</returns>
        public static OnColumnsNotAdjustedVariance01 Get()
        {
            return new OnColumnsNotAdjustedVariance01();
        }
    }
}