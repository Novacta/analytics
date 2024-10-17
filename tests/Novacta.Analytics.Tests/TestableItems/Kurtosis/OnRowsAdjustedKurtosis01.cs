// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsAdjustedKurtosis01 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnRowsAdjustedKurtosis01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [-3.333333333, 4.168975069, -0.817174515, 4.151199638],
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedKurtosis01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedKurtosis01"/> class.</returns>
        public static OnRowsAdjustedKurtosis01 Get()
        {
            return new OnRowsAdjustedKurtosis01();
        }
    }
}