﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a skewness operation whose data operand 
    /// has not enough rows to enable skewness estimation.
    /// </summary>
    class OnColumnsDataIsRowUnadjustedSkewness : AlongDimensionSkewness<DoubleMatrixState>
    {
        protected OnColumnsDataIsRowUnadjustedSkewness() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [Double.NaN, Double.NaN, Double.NaN],
                    numberOfRows: 1,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix21.Get(),
                adjustForBias: false,
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowUnadjustedSkewness"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowUnadjustedSkewness"/> class.</returns>
        public static OnColumnsDataIsRowUnadjustedSkewness Get()
        {
            return new OnColumnsDataIsRowUnadjustedSkewness();
        }
    }
}