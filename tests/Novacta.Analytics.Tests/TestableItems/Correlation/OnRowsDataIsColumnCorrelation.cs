﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Correlation
{
    /// <summary>
    /// Represents a covariance operation whose data operand 
    /// has not enough columns to enable adjusting for bias.
    /// </summary>
    class OnRowsDataIsColumnCorrelation : AlongDimensionCorrelation<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnCorrelation() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[9]
                    {
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN,
                        Double.NaN
                    },
                    numberOfRows: 3,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix20.Get(),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnCorrelation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnCorrelation"/> class.</returns>
        public static OnRowsDataIsColumnCorrelation Get()
        {
            return new OnRowsDataIsColumnCorrelation();
        }
    }
}