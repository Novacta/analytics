// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to randomly permute the elements 
    /// in an <see cref="IndexCollection"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The current implementation of the <see cref="RandomIndexPermutation"/> 
    /// class is based on the Donald E. Knuth's Algorithm P 
    /// (Shuffling, p. 145)<cite>knuth-2-1997</cite>.
    /// </para>
    /// </remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle"/>
    public class RandomIndexPermutation : RandomDevice
    {
        private readonly IndexCollection indexes;

        /// <summary>
        /// Gets the indexes to permute.
        /// </summary>
        /// <value>The indexes to permute.</value>
        public IEnumerable<int> Indexes { get { return this.indexes; } }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="RandomIndexPermutation"/> class
        /// able to permute the specified <see cref="IndexCollection"/>, eventually copied.
        /// </summary>
        /// <param name="indexes">The collection of indexes to permute.</param>
        /// <param name="copyIndexes"><c>true</c> if <paramref name="indexes"/> has to be copied;
        /// otherwise <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="indexes"/> is <b>null</b>.
        /// </exception>
        public RandomIndexPermutation(IndexCollection indexes, bool copyIndexes)
        {
            ArgumentNullException.ThrowIfNull(indexes);

            this.indexes = copyIndexes ? indexes.Clone() : indexes;
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="RandomIndexPermutation"/> class
        /// able to permute the specified <see cref="IndexCollection"/>.
        /// </summary>
        /// <param name="indexes">The collection of indexes to permute.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="indexes"/> is <b>null</b>.
        /// </exception>
        public RandomIndexPermutation(IndexCollection indexes)
            : this(indexes, copyIndexes: true)
        {
        }

        /// <summary>
        /// Returns a random permutation of the <see cref="Indexes"/> of this
        /// instance.
        /// </summary>
        /// <returns>A random permutation of the <see cref="Indexes"/> of this
        /// instance.</returns>
        public IndexCollection Next()
        {
            int numberOfIndexes = this.indexes.Count;
            IndexCollection randomPermutation = (IndexCollection)this.indexes.Clone();
            int[] randomIndexes = randomPermutation.Indexes;

            for (int i = numberOfIndexes - 1; i > 0; i--) {
                // k uniformly distributed on {0, ..., i}
                int j = this.RandomNumberGenerator.DiscreteUniform(0, i + 1);

                // Swap contents at positions j and i 
                (randomIndexes[i], randomIndexes[j]) = (randomIndexes[j], randomIndexes[i]);
            }

            return randomPermutation;
        }
    }
}
