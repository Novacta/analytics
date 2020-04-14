// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a sum of squared deviations operation whose data operand 
    /// has not enough columns to enable sum of squared deviations estimation.
    /// </summary>
    class OnRowsDataIsColumnSumOfSquaredDeviations : 
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnSumOfSquaredDeviations() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3],
                    numberOfRows: 3,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix20.Get(),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnSumOfSquaredDeviations"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnSumOfSquaredDeviations"/> class.</returns>
        public static OnRowsDataIsColumnSumOfSquaredDeviations Get()
        {
            return new OnRowsDataIsColumnSumOfSquaredDeviations();
        }
    }
}