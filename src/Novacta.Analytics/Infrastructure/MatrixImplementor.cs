// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Infrastructure
{
    [Serializable]
    internal abstract class MatrixImplementor<T> : ICloneable
    {
        #region Constructors

        public abstract object Clone();

        #endregion

        #region Dimensions

        internal abstract int NumberOfColumns { get; }

        internal abstract int NumberOfRows { get; }

        internal abstract int Count { get; }

        #endregion

        #region Events

        [field:NonSerialized()]
        internal event EventHandler<EventArgs> ChangingData;

        [field: NonSerialized()]
        internal event EventHandler<ImplementorChangedEventArgs> ImplementorChanged;

        internal void OnChangingData()
        {
            ChangingData?.Invoke(this, EventArgs.Empty);
        }

        internal void OnImplementorChanged(ImplementorChangedEventArgs e)
        {
            ImplementorChanged?.Invoke(this, e);
        }

        #endregion

        #region Indexers

        #region Linear indexers

        internal abstract T this[int linearIndex]
        { get; set; }

        internal abstract MatrixImplementor<T> this[IndexCollection linearIndexes]
        { get; }

        internal abstract MatrixImplementor<T> this[string linearIndexes]
        { get; }

        #endregion

        #region Tabular indexers

        // int, *

        internal abstract T this[int rowIndex, int columnIndex]
        { get; set; }

        internal abstract MatrixImplementor<T> this[int rowIndex, IndexCollection columnIndexes]
        { get; set; }

        internal abstract MatrixImplementor<T> this[int rowIndex, string columnIndexes]
        { get; set; }

        // IndexCollection, *

        internal abstract MatrixImplementor<T> this[IndexCollection rowIndexes, int columnIndex]
        { get; set; }

        internal abstract MatrixImplementor<T> this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        { get; set; }

        internal abstract MatrixImplementor<T> this[IndexCollection rowIndexes, string columnIndexes]
        { get; set; }

        // string, *

        internal abstract MatrixImplementor<T> this[string rowIndexes, int columnIndex]
        { get; set; }

        internal abstract MatrixImplementor<T> this[string rowIndexes, IndexCollection columnIndexes]
        { get; set; }

        internal abstract MatrixImplementor<T> this[string rowIndexes, string columnIndexes]
        { get; set; }

        #endregion

        #endregion

        #region Patterns

        internal abstract bool IsSymmetric
        { get; }

        internal abstract bool IsSkewSymmetric
        { get; }

        internal abstract int UpperBandwidth
        { get; }

        internal abstract int LowerBandwidth
        { get; }

        #endregion

        #region Storage

        internal abstract T[] AsColumnMajorDenseArray();

        internal abstract StorageOrder StorageOrder { get; }

        internal abstract T[] Storage { get; }

        internal abstract StorageScheme StorageScheme { get; }

        #endregion
    }
}

