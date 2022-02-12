// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics
{
    /// <summary>
    /// Specifies the scheme followed when storing matrix entries
    /// </summary>
    /// <seealso href="https://en.wikipedia.org/wiki/Sparse_matrix#Compressed_sparse_row_(CSR,_CRS_or_Yale_format)"/>
    public enum StorageScheme
    {
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

