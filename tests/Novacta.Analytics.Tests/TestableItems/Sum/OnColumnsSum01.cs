// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all column items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnColumnsSum01 :
        AlongDimensionSum<DoubleMatrixState>
    {
        protected OnColumnsSum01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            new double[5]
                                { 2, 7, 1, 3, 3 },
                        numberOfRows: 1,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix41.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsSum01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsSum01"/> class.</returns>
        public static OnColumnsSum01 Get()
        {
            return new OnColumnsSum01();
        }
    }
}