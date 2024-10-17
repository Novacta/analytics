// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsMean00 :
        AlongDimensionMin<DoubleMatrixState>
    {
        protected OnRowsMean00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [7.2000, 7.4000, 5.6000, 7.00],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMean00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMean00"/> class.</returns>
        public static OnRowsMean00 Get()
        {
            return new OnRowsMean00();
        }
    }
}