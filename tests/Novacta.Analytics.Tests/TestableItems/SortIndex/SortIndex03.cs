// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SortIndex
{
    /// <summary>
    /// Represents a testable sort index which summarizes
    /// all items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix56"/>.
    /// </summary>
    class SortIndex03 :
        SortIndexOperation<SortIndexResults>
    {
        protected SortIndex03() :
                base(
                    expected: new SortIndexResults()
                    {
                        SortedData = DoubleMatrix.Dense(2, 3, new double[6] { 2, 1, 0, 0, -1, -2 }),
                        SortedIndexes = IndexCollection.FromArray(new int[6] { 0, 2, 1, 3, 5, 4 })
                    },
                    data: TestableDoubleMatrix56.Get(),
                    sortDirection: SortDirection.Descending)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="SortIndex03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="SortIndex03"/> class.</returns>
        public static SortIndex03 Get()
        {
            return new SortIndex03();
        }
    }
}