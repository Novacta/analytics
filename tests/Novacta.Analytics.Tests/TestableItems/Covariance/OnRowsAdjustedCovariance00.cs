// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Covariance
{
    /// <summary>
    /// Represents a testable covariance which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsAdjustedCovariance00 :
        AlongDimensionCovariance<DoubleMatrixState>
    {
        protected OnRowsAdjustedCovariance00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [
                                           43.0000,
                                          -23.6666666667,
                                          -25.333333333333,
                                           14.333333333333,
                                            6.6666666667,
                                          -23.6666666667,
                                           27.0000,
                                           30.6666666667,
                                          -25.6666666667,
                                           -5.333333333333,
                                          -25.333333333333,
                                           30.6666666667,
                                           35.333333333333,
                                          -30.6666666667,
                                           -6.333333333333,
                                           14.333333333333,
                                          -25.6666666667,
                                          -30.6666666667,
                                           29.6666666667,
                                            5.333333333333,
                                            6.6666666667,
                                           -5.333333333333,
                                           -6.333333333333,
                                            5.333333333333,
                                            1.6666666667],
                        numberOfRows: 5,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix45.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedCovariance00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedCovariance00"/> class.</returns>
        public static OnRowsAdjustedCovariance00 Get()
        {
            return new OnRowsAdjustedCovariance00();
        }
    }
}