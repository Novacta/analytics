// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnRowsAdjustedVariance02 :
        AlongDimensionVariance<DoubleMatrixState>
    {
        protected OnRowsAdjustedVariance02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedVariance02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedVariance02"/> class.</returns>
        public static OnRowsAdjustedVariance02 Get()
        {
            return new OnRowsAdjustedVariance02();
        }
    }
}