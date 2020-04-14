// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a standard deviation operation whose data operand 
    /// has not enough columns to enable standard deviation estimation.
    /// </summary>
    class OnRowsDataIsColumnUnadjustedStandardDeviation : AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnUnadjustedStandardDeviation() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3],
                    numberOfRows: 3,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix20.Get(),
                adjustForBias: false,
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnUnadjustedStandardDeviation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnUnadjustedStandardDeviation"/> class.</returns>
        public static OnRowsDataIsColumnUnadjustedStandardDeviation Get()
        {
            return new OnRowsDataIsColumnUnadjustedStandardDeviation();
        }
    }
}