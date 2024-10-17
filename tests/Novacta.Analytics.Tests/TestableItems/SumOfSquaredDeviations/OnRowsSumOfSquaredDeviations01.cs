// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsSumOfSquaredDeviations01 :
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnRowsSumOfSquaredDeviations01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [4.8, 15.2, 15.2, 18.8],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsSumOfSquaredDeviations01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsSumOfSquaredDeviations01"/> class.</returns>
        public static OnRowsSumOfSquaredDeviations01 Get()
        {
            return new OnRowsSumOfSquaredDeviations01();
        }
    }
}