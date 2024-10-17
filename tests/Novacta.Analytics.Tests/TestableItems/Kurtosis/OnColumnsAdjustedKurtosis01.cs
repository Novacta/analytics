// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsAdjustedKurtosis01 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnColumnsAdjustedKurtosis01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [2.227146814, 0.435731789, 2.227146814, -1.289256198, 4],
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
        /// Gets an instance of the <see cref="OnColumnsAdjustedKurtosis01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsAdjustedKurtosis01"/> class.</returns>
        public static OnColumnsAdjustedKurtosis01 Get()
        {
            return new OnColumnsAdjustedKurtosis01();
        }
    }
}