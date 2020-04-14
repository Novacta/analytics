// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a standard deviation operation whose data operand 
    /// has not enough rows to enable standard deviation estimation.
    /// </summary>
    class OnColumnsDataIsRowUnadjustedStandardDeviation : AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnColumnsDataIsRowUnadjustedStandardDeviation() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3],
                    numberOfRows: 1,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix21.Get(),
                adjustForBias: false,
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowUnadjustedStandardDeviation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowUnadjustedStandardDeviation"/> class.</returns>
        public static OnColumnsDataIsRowUnadjustedStandardDeviation Get()
        {
            return new OnColumnsDataIsRowUnadjustedStandardDeviation();
        }
    }
}