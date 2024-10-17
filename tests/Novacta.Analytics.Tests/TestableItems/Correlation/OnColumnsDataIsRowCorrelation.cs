// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Correlation
{
    /// <summary>
    /// Represents a covariance operation whose data operand 
    /// has not enough rows to enable covariance estimation.
    /// </summary>
    class OnColumnsDataIsRowCorrelation : AlongDimensionCorrelation<DoubleMatrixState>
    {
        protected OnColumnsDataIsRowCorrelation() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray:
                    [
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN
                    ],
                    numberOfRows: 3,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix21.Get(),
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowCorrelation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowCorrelation"/> class.</returns>
        public static OnColumnsDataIsRowCorrelation Get()
        {
            return new OnColumnsDataIsRowCorrelation();
        }
    }
}