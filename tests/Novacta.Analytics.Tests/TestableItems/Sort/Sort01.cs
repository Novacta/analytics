// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Sort
{
    /// <summary>
    /// Represents a testable sort which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix36"/>.
    /// </summary>
    class Sort01 :
        SortOperation<DoubleMatrixState>
    {
        protected Sort01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6],
                        numberOfRows: 2,
                        numberOfColumns: 3),
                    data: TestableDoubleMatrix36.Get(),
                    sortDirection: SortDirection.Descending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="Sort01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="Sort01"/> class.</returns>
        public static Sort01 Get()
        {
            return new Sort01();
        }
    }
}
