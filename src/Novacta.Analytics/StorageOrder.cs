// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics
{
    /// <summary>
    /// Specifies a linear ordering by which matrix entries are stored.
    /// </summary>
    public enum StorageOrder
    {
        /// <summary>
        /// The order is by columns. Entries are ordered by their column index first,
        /// and entries laying on a given column are in turn ordered by their row index.
        /// </summary>
        ColumnMajor = 0x66,
        /// <summary>
        /// The order is by rows. Entries are ordered by their row index first,
        /// and entries laying on a given row are in turn ordered by their column index.
        /// </summary>
        RowMajor = 0
    }
}