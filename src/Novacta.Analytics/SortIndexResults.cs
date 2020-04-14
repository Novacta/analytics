// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents the results of sorting operations applied to
    /// matrix entries and to their corresponding linear indexes.
    /// </summary>
    /// <remarks>
    /// <para id='linearPositions'>
    /// Matrix entries are interpreted as well ordered following a 
    /// column major ordering. The position of an entry in such well
    /// ordering is referred to as the <i>linear</i> position of that entry.
    /// </para>    
    /// <para id='data'>
    /// Property <see cref="SortedData"/> returns the matrix whose entries 
    /// are those of the original matrix but ordered through a sorting operation.
    /// </para>
    /// <para id='indexes'>
    /// Property <see cref="SortedIndexes"/> gets the original linear positions
    /// of the sorted entries, arranged in the same order applied to sort
    /// matrix entries. 
    /// </para>
    /// </remarks>
    /// <see cref="Stat.SortIndex(DoubleMatrix, SortDirection)"/>
    /// <seealso href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order"/>
    public class SortIndexResults
    {
        /// <summary>
        /// Gets the sorted data matrix.
        /// </summary>
        /// <remarks>
        /// Property <see cref="SortedData"/> returns a matrix having the same 
        /// dimensions of the original matrix to which the sorting operation has been applied.
        /// The entry of <see cref="SortedData"/> occupying the <i>l</i>-th linear position
        /// is the entry of the original matrix occupying the <i>l</i>-th position in
        /// the resulted ordering.
        /// </remarks>
        /// <value>The sorted data matrix.</value>
        public DoubleMatrix SortedData { get; internal set; }

        /// <summary>
        /// Gets the original linear indexes of sorted entries arranged in
        /// the same ordering.
        /// </summary>
        /// <remarks>
        /// The <i>l</i>-th index in <see cref="SortedIndexes"/> is the 
        /// linear position
        /// of the entry in the original matrix occupying the <i>l</i>-th 
        /// linear position in
        /// the <see cref="SortedData"/>.
        /// </remarks>
        /// <value>The sorted linear indexes.</value>
        public IndexCollection SortedIndexes { get; internal set; }
    }
}
