// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SortIndex
{
    /// <summary>
    /// Represents a testable sort index which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix36"/>.
    /// </summary>
    class SortIndex01 :
        SortIndexOperation<SortIndexResults>
    {
        protected SortIndex01() :
                base(
                    expected: new SortIndexResults()
                    {
                        SortedData = DoubleMatrix.Dense(2, 3),
                        SortedIndexes = IndexCollection.Range(0, 5)
                    },
                    data: TestableDoubleMatrix36.Get(),
                    sortDirection: SortDirection.Descending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="SortIndex01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="SortIndex01"/> class.</returns>
        public static SortIndex01 Get()
        {
            return new SortIndex01();
        }
    }
}
