// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnRowsSumOfSquaredDeviations02 :
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnRowsSumOfSquaredDeviations02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsSumOfSquaredDeviations02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsSumOfSquaredDeviations02"/> class.</returns>
        public static OnRowsSumOfSquaredDeviations02 Get()
        {
            return new OnRowsSumOfSquaredDeviations02();
        }
    }
}