// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsSumOfSquaredDeviations00 :
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnRowsSumOfSquaredDeviations00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 186.8, 45.2, 77.2, 194 },
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsSumOfSquaredDeviations00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsSumOfSquaredDeviations00"/> class.</returns>
        public static OnRowsSumOfSquaredDeviations00 Get()
        {
            return new OnRowsSumOfSquaredDeviations00();
        }
    }
}