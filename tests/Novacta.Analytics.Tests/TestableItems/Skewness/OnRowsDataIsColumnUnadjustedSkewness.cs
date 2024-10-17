// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a skewness operation whose data operand 
    /// has not enough columns to enable skewness estimation.
    /// </summary>
    class OnRowsDataIsColumnUnadjustedSkewness : AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnUnadjustedSkewness() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [Double.NaN, Double.NaN, Double.NaN],
                    numberOfRows: 3,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix20.Get(),
                adjustForBias: false,
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnUnadjustedSkewness"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnUnadjustedSkewness"/> class.</returns>
        public static OnRowsDataIsColumnUnadjustedSkewness Get()
        {
            return new OnRowsDataIsColumnUnadjustedSkewness();
        }
    }
}