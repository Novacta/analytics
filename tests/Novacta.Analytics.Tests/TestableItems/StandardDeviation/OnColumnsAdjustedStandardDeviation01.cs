// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a testable standard deviation which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsAdjustedStandardDeviation01 :
        AlongDimensionStandardDeviation<DoubleMatrixState>
    {
        protected OnColumnsAdjustedStandardDeviation01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5]
                                { 2.51661147842358, 2.36290781312630, 1.25830573921179, 0.95742710775634, 1.5 },
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
        /// Gets an instance of the <see cref="OnColumnsAdjustedStandardDeviation01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedStandardDeviation01"/> class.</returns>
        public static OnColumnsAdjustedStandardDeviation01 Get()
        {
            return new OnColumnsAdjustedStandardDeviation01();
        }
    }
}