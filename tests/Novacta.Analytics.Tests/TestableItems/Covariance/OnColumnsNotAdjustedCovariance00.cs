// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Covariance
{
    /// <summary>
    /// Represents a testable covariance which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnColumnsNotAdjustedCovariance00 :
        AlongDimensionCovariance<DoubleMatrixState>
    {
        protected OnColumnsNotAdjustedCovariance00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[25] {
                                           32.2500,
                                          -17.7500,
                                          -19.0000,
                                           10.7500,
                                            5.0000,
                                          -17.7500,
                                           20.2500,
                                           23.0000,
                                          -19.2500,
                                           -4.0000,
                                          -19.0000,
                                           23.0000,
                                           26.5000,
                                          -23.0000,
                                           -4.7500,
                                           10.7500,
                                          -19.2500,
                                          -23.0000,
                                           22.2500,
                                            4.0000,
                                            5.0000,
                                           -4.0000,
                                           -4.7500,
                                            4.0000,
                                            1.2500 },
                        numberOfRows: 5,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix40.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsNotAdjustedCovariance00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsNotAdjustedCovariance00"/> class.</returns>
        public static OnColumnsNotAdjustedCovariance00 Get()
        {
            return new OnColumnsNotAdjustedCovariance00();
        }
    }
}