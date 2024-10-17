// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a sum operation whose data operand 
    /// has one column.
    /// </summary>
    class OnRowsDataIsColumnSum : 
        AlongDimensionSum<DoubleMatrixState>
    {
        protected OnRowsDataIsColumnSum() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [-5, -4, -3],
                    numberOfRows: 3,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix20.Get(),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnSum"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnSum"/> class.</returns>
        public static OnRowsDataIsColumnSum Get()
        {
            return new OnRowsDataIsColumnSum();
        }
    }
}