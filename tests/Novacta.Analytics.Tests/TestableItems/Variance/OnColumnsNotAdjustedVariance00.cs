// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a testable variance which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsNotAdjustedVariance00 :
        AlongDimensionVariance<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedVariance00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5] 
                                { 32.25, 20.25, 26.5, 22.25, 1.25 },
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedVariance00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedVariance00"/> class.</returns>
        public static OnColumnsNotAdjustedVariance00 Get()
        {
            return new OnColumnsNotAdjustedVariance00();
        }
    }
}