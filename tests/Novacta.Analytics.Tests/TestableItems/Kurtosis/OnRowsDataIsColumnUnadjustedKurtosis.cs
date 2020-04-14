// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a kurtosis operation whose data operand 
    /// has not enough columns to enable kurtosis estimation.
    /// </summary>
    class OnRowsDataIsColumnUnadjustedKurtosis : AlongDimensionKurtosis<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnUnadjustedKurtosis() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3] { Double.NaN, Double.NaN, Double.NaN },
                    numberOfRows: 3,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix20.Get(),
                adjustForBias: false,
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnUnadjustedKurtosis"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnUnadjustedKurtosis"/> class.</returns>
        public static OnRowsDataIsColumnUnadjustedKurtosis Get()
        {
            return new OnRowsDataIsColumnUnadjustedKurtosis();
        }
    }
}