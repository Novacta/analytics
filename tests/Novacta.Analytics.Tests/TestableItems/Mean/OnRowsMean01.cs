// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a testable mean which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsMean01 :
        AlongDimensionMin<DoubleMatrixState>
    {
        protected OnRowsMean01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [.8, .6, .6, 1.2],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMean01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMean01"/> class.</returns>
        public static OnRowsMean01 Get()
        {
            return new OnRowsMean01();
        }
    }
}