// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix53"/>.
    /// </summary>
    class OnRowsQuantile00 :
        AlongDimensionQuantile<DoubleMatrixState[]>
    {
        protected OnRowsQuantile00() :
                base(
                    expected: new DoubleMatrixState[3] {
                        new DoubleMatrixState(
                            asColumnMajorDenseArray: 
                                new double[4] { 4.28, 9.56, 1, 13 },
                            numberOfRows: 2,
                            numberOfColumns: 2),
                        new DoubleMatrixState(
                            asColumnMajorDenseArray: 
                                new double[4] { 5.28, 10.56, 2, 14 },
                            numberOfRows: 2,
                            numberOfColumns: 2),
                        new DoubleMatrixState(
                            asColumnMajorDenseArray: 
                                new double[4] { 6.28, 11.56, 3, 15 },
                            numberOfRows: 2,
                            numberOfColumns: 2)
                    },
                    data: TestableDoubleMatrix53.Get(),
                    probabilities: DoubleMatrix.Dense(2, 2, 
                         new double[4] { .33, .66, 0, 1 }),
                    dataOperation: DataOperation.OnRows)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsQuantile00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsQuantile00"/> class.</returns>
        public static OnRowsQuantile00 Get()
        {
            return new OnRowsQuantile00();
        }
    }
}