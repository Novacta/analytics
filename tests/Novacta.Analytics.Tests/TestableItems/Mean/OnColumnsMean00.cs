// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsMean00 :
        AlongDimensionMin<DoubleMatrixState>
    {
        protected OnColumnsMean00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [6.5000, 8.5000, 8.0000, 8.5000, 2.5],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMean00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMean00"/> class.</returns>
        public static OnColumnsMean00 Get()
        {
            return new OnColumnsMean00();
        }
    }
}