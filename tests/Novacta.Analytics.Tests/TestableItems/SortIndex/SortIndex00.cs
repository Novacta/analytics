// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SortIndex
{
    /// <summary>
    /// Represents a testable sort index which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix54"/>.
    /// </summary>
    class SortIndex00 :
        SortIndexOperation<SortIndexResults>
    {
        protected SortIndex00() :
                base(
                    expected: new SortIndexResults() {
                       SortedData = DoubleMatrix.Dense(2, 2, new double[4] { 1, 2, 3, 4 }),
                       SortedIndexes = IndexCollection.FromArray(new int[4] { 1, 3, 2, 0 })
                    },
                    data: TestableDoubleMatrix54.Get(),
                    sortDirection: SortDirection.Ascending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="SortIndex00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="SortIndex00"/> class.</returns>
        public static SortIndex00 Get()
        {
            return new SortIndex00();
        }
    }
}