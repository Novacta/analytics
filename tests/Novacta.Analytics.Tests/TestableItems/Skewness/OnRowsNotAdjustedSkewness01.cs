// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnRowsNotAdjustedSkewness01 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsNotAdjustedSkewness01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { 0.40824829046386, 1.30404732936951, -0.05433530539040, 1.36279275303555 },
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
        /// Gets an instance of the <see cref="OnRowsNotAdjustedSkewness01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsNotAdjustedSkewness01"/> class.</returns>
        public static OnRowsNotAdjustedSkewness01 Get()
        {
            return new OnRowsNotAdjustedSkewness01();
        }
    }
}