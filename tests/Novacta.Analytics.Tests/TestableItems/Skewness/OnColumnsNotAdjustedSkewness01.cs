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
    class OnColumnsNotAdjustedSkewness01 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedSkewness01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5]
                                { 0.65202366468475, 0.68925447711468, 0.65202366468475, 0.49338220021816, 1.15470053837925 },
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedSkewness01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedSkewness01"/> class.</returns>
        public static OnColumnsNotAdjustedSkewness01 Get()
        {
            return new OnColumnsNotAdjustedSkewness01();
        }
    }
}