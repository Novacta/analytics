// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a mean operation whose data operand 
    /// has one row.
    /// </summary>
    class OnColumnsDataIsRowMean : 
        AlongDimensionMin<DoubleMatrixState>
    {
        protected OnColumnsDataIsRowMean() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [-5, -4, -3],
                    numberOfRows: 1,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix21.Get(),
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowMean"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowMean"/> class.</returns>
        public static OnColumnsDataIsRowMean Get()
        {
            return new OnColumnsDataIsRowMean();
        }
    }
}