// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Sort
{
    /// <summary>
    /// Represents a sort operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class DataIsNullSort : 
        SortOperation<ArgumentNullException>
    {
        protected DataIsNullSort(SortDirection sortDirection) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                sortDirection: sortDirection)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="DataIsNullSort" /> class.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>An instance of the
        /// <see cref="DataIsNullSort" /> class.</returns>
        public static DataIsNullSort Get(SortDirection sortDirection)
        {
            return new DataIsNullSort(sortDirection);
        }
    }
}
