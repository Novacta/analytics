﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a testable skewness which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnRowsAdjustedSkewness02 :
        AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnRowsAdjustedSkewness02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[4] { double.NaN, double.NaN, double.NaN, double.NaN },
                        numberOfRows: 4,
                        numberOfColumns: 1
                    ),
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsAdjustedSkewness02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsAdjustedSkewness02"/> class.</returns>
        public static OnRowsAdjustedSkewness02 Get()
        {
            return new OnRowsAdjustedSkewness02();
        }
    }
}