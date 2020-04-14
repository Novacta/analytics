// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.SortIndex
{
    /// <summary>
    /// Represents a sort index operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class DataIsNullSortIndex : 
        SortIndexOperation<ArgumentNullException>
    {
        protected DataIsNullSortIndex(SortDirection sortDirection) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                sortDirection: sortDirection)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="DataIsNullSortIndex" /> class.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>An instance of the
        /// <see cref="DataIsNullSortIndex" /> class.</returns>
        public static DataIsNullSortIndex Get(SortDirection sortDirection)
        {
            return new DataIsNullSortIndex(sortDirection);
        }
    }
}
