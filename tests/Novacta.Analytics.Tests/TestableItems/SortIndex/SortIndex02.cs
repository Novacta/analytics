// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SortIndex
{
    /// <summary>
    /// Represents a testable sort index which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix55"/>.
    /// </summary>
    class SortIndex02 :
        SortIndexOperation<SortIndexResults>
    {
        protected SortIndex02() :
                base(
                    expected: new SortIndexResults()
                    {
                        SortedData = DoubleMatrix.Dense(2, 3, [-2, -1, 0, 0, 1, 2]),
                        SortedIndexes = IndexCollection.FromArray([4, 5, 1, 3, 2, 0])
                    },
                    data: TestableDoubleMatrix55.Get(),
                    sortDirection: SortDirection.Ascending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="SortIndex02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="SortIndex02"/> class.</returns>
        public static SortIndex02 Get()
        {
            return new SortIndex02();
        }
    }
}