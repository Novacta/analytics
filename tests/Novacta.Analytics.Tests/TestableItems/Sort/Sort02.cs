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
    /// by <see cref="TestableDoubleMatrix55"/>.
    /// </summary>
    class Sort02 :
        SortOperation<DoubleMatrixState>
    {
        protected Sort02() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray: [-2, -1, 0, 0, 1, 2],
                        numberOfRows: 2,
                        numberOfColumns: 3),
                    data: TestableDoubleMatrix55.Get(),
                    sortDirection: SortDirection.Ascending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="Sort02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="Sort02"/> class.</returns>
        public static Sort02 Get()
        {
            return new Sort02();
        }
    }
}