// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsAdjustedSkewness01 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnColumnsAdjustedSkewness01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [1.12933811497125, 1.19382377370694, 1.12933811497125, 0.85456303832797, 2],
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsAdjustedSkewness01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedSkewness01"/> class.</returns>
        public static OnColumnsAdjustedSkewness01 Get()
        {
            return new OnColumnsAdjustedSkewness01();
        }
    }
}