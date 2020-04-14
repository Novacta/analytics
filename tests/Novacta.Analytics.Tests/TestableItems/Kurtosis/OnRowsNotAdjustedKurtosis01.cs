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
    class OnRowsNotAdjustedKurtosis01 :
        AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedKurtosis01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { -1.833333333, 0.042243767, -1.204293629, 0.037799909 },
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    adjustForBias: false,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsNotAdjustedKurtosis01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedKurtosis01"/> class.</returns>
        public static OnRowsNotAdjustedKurtosis01 Get()
        {
            return new OnRowsNotAdjustedKurtosis01();
        }
    }
}