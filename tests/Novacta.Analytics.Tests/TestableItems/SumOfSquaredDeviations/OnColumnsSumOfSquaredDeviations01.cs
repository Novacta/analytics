// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsSumOfSquaredDeviations01 :
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnColumnsSumOfSquaredDeviations01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [19, 16.75, 4.75, 2.75, 6.75],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsSumOfSquaredDeviations01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsSumOfSquaredDeviations01"/> class.</returns>
        public static OnColumnsSumOfSquaredDeviations01 Get()
        {
            return new OnColumnsSumOfSquaredDeviations01();
        }
    }
}