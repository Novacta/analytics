// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a sum operation whose data operand 
    /// has one row.
    /// </summary>
    class OnColumnsDataIsRowSum : 
        AlongDimensionSum<DoubleMatrixState>
    {
        protected OnColumnsDataIsRowSum() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[3] { -5, -4, -3 },
                    numberOfRows: 1,
                    numberOfColumns: 3),
                data: TestableDoubleMatrix21.Get(),
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowSum"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowSum"/> class.</returns>
        public static OnColumnsDataIsRowSum Get()
        {
            return new OnColumnsDataIsRowSum();
        }
    }
}