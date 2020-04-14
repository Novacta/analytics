// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a mean operation whose data operand 
    /// has one column.
    /// </summary>
    class OnRowsDataIsColumnMean : 
        AlongDimensionMin<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnMean() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3] { -5, -4, -3 },
                    numberOfRows: 3,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix20.Get(),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnMean"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnMean"/> class.</returns>
        public static OnRowsDataIsColumnMean Get()
        {
            return new OnRowsDataIsColumnMean();
        }
    }
}