// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a sum of squared deviations operation whose data operand 
    /// has not enough rows to enable sum of squared deviations estimation.
    /// </summary>
    class OnColumnsDataIsRowSumOfSquaredDeviations : 
        AlongDimensionSumOfSquaredDeviations<DoubleMatrixState>
    {
        protected OnColumnsDataIsRowSumOfSquaredDeviations() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3],
                    numberOfRows: 1,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix21.Get(),
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowSumOfSquaredDeviations"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowSumOfSquaredDeviations"/> class.</returns>
        public static OnColumnsDataIsRowSumOfSquaredDeviations Get()
        {
            return new OnColumnsDataIsRowSumOfSquaredDeviations();
        }
    }
}