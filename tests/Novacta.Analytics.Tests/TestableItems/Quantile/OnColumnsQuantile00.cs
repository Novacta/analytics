// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix52"/>.
    /// </summary>
    class OnColumnsQuantile00 :
        AlongDimensionQuantile<DoubleMatrixState[]>
    {
        protected OnColumnsQuantile00() :
                base(
                    expected: [
                        new(
                            asColumnMajorDenseArray:
                                [1, 13, 19.33333333, 25, 1, 25],
                            numberOfRows: 2,
                            numberOfColumns: 3),
                        new(
                            asColumnMajorDenseArray:
                                [26, 38, 44.33333333, 50, 26, 50],
                            numberOfRows: 2,
                            numberOfColumns: 3),
                        new(
                            asColumnMajorDenseArray:
                                [51, 63, 69.33333333, 75, 51, 75],
                            numberOfRows: 2,
                            numberOfColumns: 3),
                        new(
                            asColumnMajorDenseArray:
                                [76, 88, 94.33333333, 100, 76, 100],
                            numberOfRows: 2,
                            numberOfColumns: 3)
                    ],
                    data: TestableDoubleMatrix52.Get(),
                    probabilities: DoubleMatrix.Dense(2, 3,
                         [.005, .5, .75, .999, 0, 1]),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsQuantile00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsQuantile00"/> class.</returns>
        public static OnColumnsQuantile00 Get()
        {
            return new OnColumnsQuantile00();
        }
    }
}