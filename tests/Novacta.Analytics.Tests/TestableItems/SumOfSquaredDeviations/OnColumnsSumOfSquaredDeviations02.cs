// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnColumnsSumOfSquaredDeviations02 :
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnColumnsSumOfSquaredDeviations02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsSumOfSquaredDeviations02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsSumOfSquaredDeviations02"/> class.</returns>
        public static OnColumnsSumOfSquaredDeviations02 Get()
        {
            return new OnColumnsSumOfSquaredDeviations02();
        }
    }
}