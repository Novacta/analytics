// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Covariance
{
    /// <summary>
    /// Represents a covariance operation whose data operand 
    /// has not enough columns to enable covariance estimation.
    /// </summary>
    class OnRowsDataIsColumnUnadjustedCovariance : AlongDimensionCovariance<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnUnadjustedCovariance() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[9],
                    numberOfRows: 3,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix20.Get(),
                adjustForBias: false,
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnUnadjustedCovariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnUnadjustedCovariance"/> class.</returns>
        public static OnRowsDataIsColumnUnadjustedCovariance Get()
        {
            return new OnRowsDataIsColumnUnadjustedCovariance();
        }
    }
}