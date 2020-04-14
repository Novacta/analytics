// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics
{
    /// <summary>
    /// Specifies the scheme followed when storing matrix entries
    /// </summary>
    public enum StorageScheme
    {
        /// <summary>
        /// Entries are not directly stored. Another matrix is referred to 
        /// as the data source, and row and column indexes are stored to 
        /// identify the data which in the source correspond to the target entries.
        /// </summary>
        View,
        /// <summary>
        /// All entries are stored in a one-dimensional array.
        /// </summary>
        Dense,
        /// <summary>
        /// Entries are stored using the compressed sparse row scheme.
        /// </summary>
        CompressedRow
    }
}

