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
    /// by <see cref="TestableDoubleMatrix54"/>.
    /// </summary>
    class Sort00 :
        SortOperation<DoubleMatrixState>
    {
        protected Sort00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray: [1, 2, 3, 4],
                        numberOfRows: 2,
                        numberOfColumns:2),
                    data: TestableDoubleMatrix54.Get(),
                    sortDirection: SortDirection.Ascending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="Sort00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="Sort00"/> class.</returns>
        public static Sort00 Get()
        {
            return new Sort00();
        }
    }
}
