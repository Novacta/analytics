// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Novacta.Analytics.Infrastructure
{
    internal sealed class DenseMatrixImplementor<T>
    {
        private readonly T[] storage;
        private readonly int numberOfColumns;
        private readonly int numberOfRows;

        #region Constructors and factory methods

        internal DenseMatrixImplementor(int numberOfRows, int numberOfColumns)
        {
            Debug.Assert(numberOfRows > 0);
            Debug.Assert(numberOfColumns > 0);

            this.storage = new T[numberOfRows * numberOfColumns];
            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
        }

        #endregion

        #region Dimensions

        internal int NumberOfColumns
        {
            get
            {
                return this.numberOfColumns;
            }
        }

        internal int NumberOfRows
        {
            get
            {
                return this.numberOfRows;
            }
        }

        internal int Length
        {
            get
            {
                return this.storage.Length;
            }
        }
        
        #endregion

        #region Indexers

        internal T this[int linearIndex]
        {
            get
            {
                return this.storage[linearIndex];
            }
            set
            {
                this.storage[linearIndex] = value;
            }
        }
        
        internal T this[int rowIndex, int columnIndex]
        {
            get
            {
                int linearIndex = rowIndex + (columnIndex * this.numberOfRows);

                return this.storage[linearIndex];
            }
            set
            {
                int linearIndex = rowIndex + (columnIndex * this.numberOfRows);

                this.storage[linearIndex] = value;
            }
        }
        
        #endregion
    }
}

