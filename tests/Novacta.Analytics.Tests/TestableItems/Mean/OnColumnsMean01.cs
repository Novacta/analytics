// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsMean01 :
        AlongDimensionMin<DoubleMatrixState>
    {
        protected OnColumnsMean01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [.5, 1.75, .25, .75, .75],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMean01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMean01"/> class.</returns>
        public static OnColumnsMean01 Get()
        {
            return new OnColumnsMean01();
        }
    }
}