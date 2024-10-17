// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsSumOfSquaredDeviations00 :
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnColumnsSumOfSquaredDeviations00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [129, 81, 106, 89, 5],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsSumOfSquaredDeviations00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsSumOfSquaredDeviations00"/> class.</returns>
        public static OnColumnsSumOfSquaredDeviations00 Get()
        {
            return new OnColumnsSumOfSquaredDeviations00();
        }
    }
}