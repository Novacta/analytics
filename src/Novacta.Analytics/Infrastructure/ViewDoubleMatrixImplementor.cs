// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Infrastructure
{
    internal sealed class ViewDoubleMatrixImplementor : 
        MatrixImplementor<double>
    {
        internal IndexCollection[] parentIndexes;
        internal MatrixImplementor<double> parentImplementor;
        internal int numberOfColumns;
        internal int numberOfRows;

        #region Constructors

        public sealed override object Clone()
        {
            IndexCollection[] clonedIndexes = new IndexCollection[2];
            this.parentIndexes.CopyTo(clonedIndexes, 0);

            var clone = new ViewDoubleMatrixImplementor(clonedIndexes, this.parentImplementor);

            return clone;
        }

        internal ViewDoubleMatrixImplementor(
            IndexCollection[] parentIndexes, 
            MatrixImplementor<double> parentImplementor)
        {
            this.numberOfRows = parentIndexes[0].Count;
            this.numberOfColumns = parentIndexes[1].Count;
            this.parentIndexes = parentIndexes;
            this.parentImplementor = parentImplementor;
            this.parentImplementor.ChangingData += 
                new EventHandler<EventArgs>(this.ChangingDataHandler);
            this.parentImplementor.ImplementorChanged += 
                new EventHandler<ImplementorChangedEventArgs>(
                    this.ImplementorChangedHandler);
        }

        #endregion

        #region Events

        internal void ChangingDataHandler(object sender, EventArgs e)
        {
            ImplementorChangedEventArgs implementorChangedEventArgs = null;
            DenseDoubleMatrixImplementor newFullImplementor = null;
            MatrixImplementor<double> senderAsImplementor = (MatrixImplementor<double>)sender;
            senderAsImplementor.ChangingData -=
                new EventHandler<EventArgs>(this.ChangingDataHandler);
            senderAsImplementor.ImplementorChanged -= new EventHandler<ImplementorChangedEventArgs>(
                this.ImplementorChangedHandler);
            newFullImplementor = (DenseDoubleMatrixImplementor)this;
            implementorChangedEventArgs = new ImplementorChangedEventArgs(newFullImplementor);
            base.OnImplementorChanged(implementorChangedEventArgs);
        }

        internal void ImplementorChangedHandler(
            object sender, 
            ImplementorChangedEventArgs e)
        {
            MatrixImplementor<double> senderAsImplementor = (MatrixImplementor<double>)sender;
            senderAsImplementor.ImplementorChanged -=
                new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
            senderAsImplementor.ChangingData -=
                new EventHandler<EventArgs>(this.ChangingDataHandler);
            this.parentImplementor = (MatrixImplementor<double>)e.NewImplementor;
            this.parentImplementor.ImplementorChanged +=
                new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
            this.parentImplementor.ChangingData +=
                new EventHandler<EventArgs>(this.ChangingDataHandler);
        }

        #endregion

        #region Indexers

        #region Linear indexers

        internal sealed override double this[int linearIndex]
        {
            get
            {
                // Check if the linear index is outside the range defined by the matrix dimensions
                if (linearIndex < 0 || this.Count <= linearIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(linearIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                int quotient, remainder;
                quotient = Convert.ToInt32(Math.Floor(Convert.ToDouble(linearIndex / this.numberOfRows)));
                remainder = linearIndex - (this.numberOfRows * quotient);
                return this.parentImplementor[this.parentIndexes[0][remainder], this.parentIndexes[1][quotient]];
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[linearIndex] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "0")]
        internal sealed override MatrixImplementor<double> this[IndexCollection linearIndexes]
        {
            get
            {
                // Check if any linearIndex is outside the range defined by the matrix dimensions
                if (linearIndexes.maxIndex >= this.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(linearIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection[] refererredIndexes = this.GetReferredImplementor(
                    out MatrixImplementor<double> referredImplementor);
                int[] referredRows = refererredIndexes[0].indexes;
                int[] referredColumns = refererredIndexes[1].indexes;

                int[] linears = linearIndexes.indexes;
                int subColumnsLength = 1;
                int subRowsLength = linears.Length;

                var subMatrix = new DenseDoubleMatrixImplementor(subRowsLength, subColumnsLength);
                double[] subStorage = subMatrix.storage;

                int sourceOffset;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] sourceStorage = source.storage;
                            int sourceLeadingDimension = source.numberOfRows;
                            for (int j = 0; j < subRowsLength; j++)
                            {
                                IndexCollection.ConvertToTabularIndexes(
                                    linears[j], 
                                    this.numberOfRows,
                                    out int rowIndex,
                                    out int columnIndex);
                                sourceOffset = sourceLeadingDimension * referredColumns[columnIndex];
                                subStorage[j] = sourceStorage[referredRows[rowIndex] + sourceOffset];
                            }
                        }
                        break;
                }

                return subMatrix;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "0")]
        internal sealed override MatrixImplementor<double> this[string linearIndexes]
        {
            get
            {
                IndexCollection[] refererredIndexes = this.GetReferredImplementor(
                    out MatrixImplementor<double> referredImplementor);
                int[] referredRows = refererredIndexes[0].indexes;
                int[] referredColumns = refererredIndexes[1].indexes;

                int thisNumberOfRows = this.numberOfRows;
                int thisNumberOfColumns = this.numberOfColumns;

                int subColumnsLength = 1;
                int subRowsLength = this.Count;

                var subMatrix = new DenseDoubleMatrixImplementor(subRowsLength, subColumnsLength);
                double[] subStorage = subMatrix.storage;

                int sourceOffset, index = 0;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] sourceStorage = source.storage;
                            int sourceLeadingDimension = source.numberOfRows;
                            for (int j = 0; j < thisNumberOfColumns; j++)
                            {
                                sourceOffset = sourceLeadingDimension * referredColumns[j];
                                for (int i = 0; i < thisNumberOfRows; i++, index++)
                                    subStorage[index] = sourceStorage[referredRows[i] + sourceOffset];
                            }
                        }
                        break;
                }

                return subMatrix;
            }
        }

        #endregion

        #region Tabular indexers

        // int, *

        internal sealed override double this[int rowIndex, int columnIndex]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                return this.parentImplementor[this.parentIndexes[0][rowIndex], this.parentIndexes[1][columnIndex]];
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndex, columnIndex] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "0")]
        internal sealed override MatrixImplementor<double> this[int rowIndex, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection[] refererredIndexes = this.GetReferredImplementor(
                    out MatrixImplementor<double> referredImplementor);
                int[] referredRows = refererredIndexes[0].indexes;
                int[] referredColumns = refererredIndexes[1].indexes;

                int[] columns = columnIndexes.indexes;
                int subColumnsLength = columns.Length;
                int subRowsLength = 1;

                var subMatrix = new DenseDoubleMatrixImplementor(subRowsLength, subColumnsLength);
                double[] subStorage = subMatrix.storage;

                int sourceOffset;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] sourceStorage = source.storage;
                            int sourceLeadingDimension = source.numberOfRows;
                            for (int j = 0; j < subColumnsLength; j++)
                            {
                                sourceOffset = sourceLeadingDimension * referredColumns[columns[j]];
                                subStorage[j] = sourceStorage[referredRows[rowIndex] + sourceOffset];
                            }
                        }
                        break;
                }

                return subMatrix;
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndex, columnIndexes] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        internal sealed override MatrixImplementor<double> this[int rowIndex, string columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndex < 0 || this.numberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection[] refererredIndexes = this.GetReferredImplementor(
                    out MatrixImplementor<double> referredImplementor);
                int[] referredRows = refererredIndexes[0].indexes;
                int[] referredColumns = refererredIndexes[1].indexes;

                int subColumnsLength = this.numberOfColumns;
                int subRowsLength = 1;

                var subMatrix = new DenseDoubleMatrixImplementor(subRowsLength, subColumnsLength);
                double[] subStorage = subMatrix.storage;

                int sourceOffset;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] sourceStorage = source.storage;
                            int sourceLeadingDimension = source.numberOfRows;
                            for (int j = 0; j < subColumnsLength; j++)
                            {
                                sourceOffset = sourceLeadingDimension * referredColumns[j];
                                subStorage[j] = sourceStorage[referredRows[rowIndex] + sourceOffset];
                            }
                        }
                        break;
                }

                return subMatrix;
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndex, columnIndexes] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        // IndexCollection, *

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "0")]
        internal sealed override MatrixImplementor<double> this[IndexCollection rowIndexes, int columnIndex]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection[] refererredIndexes = this.GetReferredImplementor(
                    out MatrixImplementor<double> referredImplementor);
                int[] referredRows = refererredIndexes[0].indexes;
                int[] referredColumns = refererredIndexes[1].indexes;

                int[] rows = rowIndexes.indexes;
                int subColumnsLength = 1;
                int subRowsLength = rows.Length;

                var subMatrix = new DenseDoubleMatrixImplementor(subRowsLength, subColumnsLength);
                double[] subStorage = subMatrix.storage;

                int sourceOffset;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] sourceStorage = source.storage;
                            int sourceLeadingDimension = source.numberOfRows;
                            sourceOffset = sourceLeadingDimension * referredColumns[columnIndex];
                            for (int i = 0; i < subRowsLength; i++)
                                subStorage[i] = sourceStorage[referredRows[rows[i]] + sourceOffset];
                        }
                        break;
                }

                return subMatrix;
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndexes, columnIndex] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "0")]
        internal sealed override MatrixImplementor<double> this[
            IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                // Check if any row index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection[] subIndexes = new IndexCollection[2] { rowIndexes, columnIndexes };

                return new ViewDoubleMatrixImplementor(subIndexes, this);
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndexes, columnIndexes] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "0")]
        internal sealed override MatrixImplementor<double> this[
            IndexCollection rowIndexes, string columnIndexes]
        {
            get
            {
                // Check if any row index is outside the range defined by matrix dimensions
                if (rowIndexes.maxIndex >= this.numberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection columns = IndexCollection.Range(0, this.numberOfColumns - 1);
                IndexCollection[] subIndexes = new IndexCollection[2] { rowIndexes, columns };

                return new ViewDoubleMatrixImplementor(subIndexes, this);
            }
            set
            {
                this.parentImplementor.ChangingData -=
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndexes, columnIndexes] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        // string, *

        internal sealed override MatrixImplementor<double> this[
            string rowIndexes, int columnIndex]
        {
            get
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndex < 0 || this.numberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection[] refererredIndexes = this.GetReferredImplementor(
                    out MatrixImplementor<double> referredImplementor);
                int[] referredRows = refererredIndexes[0].indexes;
                int[] referredColumns = refererredIndexes[1].indexes;

                int subColumnsLength = 1;
                int subRowsLength = this.numberOfRows;

                var subMatrix = new DenseDoubleMatrixImplementor(subRowsLength, subColumnsLength);
                double[] subStorage = subMatrix.storage;

                int sourceOffset;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            var source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] sourceStorage = source.storage;
                            int sourceLeadingDimension = source.numberOfRows;
                            sourceOffset = sourceLeadingDimension * referredColumns[columnIndex];
                            for (int i = 0; i < subRowsLength; i++)
                                subStorage[i] = sourceStorage[referredRows[i] + sourceOffset];
                        }
                        break;
                }

                return subMatrix;
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndexes, columnIndex] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", 
            "CA1062:Validate parameters of public methods",
            Justification = "Validation is delegated to DoubleMatrix indexers.", MessageId = "1")]
        internal sealed override MatrixImplementor<double> this[
            string rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                // Check if any column index is outside the range defined by matrix dimensions
                if (columnIndexes.maxIndex >= this.numberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                IndexCollection rows = IndexCollection.Range(0, this.numberOfRows - 1);
                IndexCollection[] subIndexes = new IndexCollection[2] { rows, columnIndexes };

                return new ViewDoubleMatrixImplementor(subIndexes, this);
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndexes, columnIndexes] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        internal sealed override MatrixImplementor<double> this[
            string rowIndexes, string columnIndexes]
        {
            get
            {
                IndexCollection rows = IndexCollection.Range(0, this.numberOfRows - 1);
                IndexCollection columns = IndexCollection.Range(0, this.numberOfColumns - 1);
                IndexCollection[] subIndexes = new IndexCollection[2] { rows, columns };

                return new ViewDoubleMatrixImplementor(subIndexes, this);
            }
            set
            {
                this.parentImplementor.ChangingData -= 
                    new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= 
                    new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                var newDenseImplementor = (DenseDoubleMatrixImplementor)this;
                newDenseImplementor[rowIndexes, columnIndexes] = value;
                var implementorChangedEventArgs = new ImplementorChangedEventArgs(newDenseImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
            }
        }

        #endregion

        #endregion

        #region Dimensions

        internal sealed override int NumberOfColumns
        {
            get
            {
                return this.numberOfColumns;
            }
        }

        internal sealed override int NumberOfRows
        {
            get
            {
                return this.numberOfRows;
            }
        }

        internal sealed override int Count
        {
            get
            {
                return (this.numberOfRows * this.numberOfColumns);
            }
        }

        #endregion

        #region Patterns

        internal sealed override bool IsSymmetric
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                if (numberOfRows != numberOfColumns)
                    return false;

                int offset;

                for (int i = 1; i < numberOfRows; i++)
                {
                    offset = i * numberOfRows;
                    for (int j = 0; j < i; j++)
                    {
                        if (this[i + numberOfRows * j] != this[j + offset])
                            return false;
                    }
                }

                return true;
            }
        }

        internal sealed override bool IsSkewSymmetric
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                if (numberOfRows != numberOfColumns)
                    return false;

                int offset;

                for (int i = 1; i < numberOfRows; i++)
                {
                    offset = i * numberOfRows;
                    for (int j = 0; j < i; j++)
                    {
                        if (this[i + numberOfRows * j] != -this[j + offset])
                            return false;
                    }
                }

                // Diagonal entries
                for (int i = 0; i < numberOfRows; i++)
                {
                    offset = i * numberOfRows;
                    if (this[i + offset] != 0.0)
                        return false;
                }

                return true;
            }
        }

        internal sealed override int UpperBandwidth
        {
            get
            {
                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                IndexCollection[] subIndexes = this.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);
                int[] rows = subIndexes[0].indexes;
                int[] columns = subIndexes[1].indexes;

                int offset, column;
                int referredLeadingDimension;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            DenseDoubleMatrixImplementor source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] referredStorage = source.storage;
                            referredLeadingDimension = source.numberOfRows;
                            for (int j = numberOfColumns - 1; j > 0; j--)
                            {
                                column = j;
                                offset = columns[j] * referredLeadingDimension;
                                for (int i = 0; i <= (numberOfColumns - numberOfRows < j ? numberOfColumns - 1 - j : numberOfRows - 1); i++)
                                {
                                    if (0.0 != referredStorage[rows[i] + offset])
                                        return column - i;
                                    offset += referredLeadingDimension;
                                    column++;
                                }
                            }
                        }
                        break;
                }

                return 0;
            }
        }

        internal sealed override int LowerBandwidth
        {
            get
            {
                // The lower bandwidth of an MxN matrix A is the smallest number p, lambdaArray.t. 0<=p<M 
                // such that the entry aij vanishes whenever i > j + p. 

                int numberOfRows = this.numberOfRows;
                int numberOfColumns = this.numberOfColumns;

                IndexCollection[] subIndexes = this.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);
                int[] rows = subIndexes[0].indexes;
                int[] columns = subIndexes[1].indexes;

                int offset;
                int referredLeadingDimension;

                switch (referredImplementor.StorageScheme)
                {
                    case StorageScheme.Dense:
                        {
                            DenseDoubleMatrixImplementor source = (DenseDoubleMatrixImplementor)referredImplementor;
                            double[] referredStorage = source.storage;
                            referredLeadingDimension = source.numberOfRows;
                            for (int j = 0; j <= numberOfColumns - 1; j++)
                            {
                                offset = columns[j] * referredLeadingDimension;
                                for (int i = numberOfRows - 1; i > j; i--)
                                    if (0.0 != referredStorage[rows[i] + offset])
                                        return i - j;
                            }
                        }
                        break;
                }

                return 0;
            }
        }

        #endregion

        #region Storage

        internal IndexCollection[] GetReferredImplementor(out MatrixImplementor<double> referredImplementor)
        {
            IndexCollection[] indexes = this.parentIndexes;
            referredImplementor = this.parentImplementor;
            while (StorageScheme.View == referredImplementor.StorageScheme)
            {
                ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)referredImplementor;
                indexes[0] = subImplementor.parentIndexes[0][indexes[0]];
                indexes[1] = subImplementor.parentIndexes[1][indexes[1]];
                referredImplementor = subImplementor.parentImplementor;
            }
            return indexes;
        }

        internal MatrixImplementor<double> ParentImplementor
        {
            get { return this.parentImplementor; }
        }

        internal sealed override double[] AsColumnMajorDenseArray()
        {
            int numberOfRows = this.numberOfRows;
            int numberOfColumns = this.numberOfColumns;

            double[] fullStorage = new double[this.Count];

            IndexCollection[] subIndexes = this.GetReferredImplementor(out MatrixImplementor<double> referredImplementor);
            int[] rows = subIndexes[0].indexes;
            int[] columns = subIndexes[1].indexes;

            int offset, index = 0;
            int referredLeadingDimension;

            switch (referredImplementor.StorageScheme)
            {
                case StorageScheme.Dense:
                    {
                        DenseDoubleMatrixImplementor source = (DenseDoubleMatrixImplementor)referredImplementor;
                        double[] referredStorage = source.storage;
                        referredLeadingDimension = source.numberOfRows;
                        for (int j = 0; j < numberOfColumns; j++)
                        {
                            offset = columns[j] * referredLeadingDimension;
                            for (int i = 0; i < numberOfRows; i++, index++)
                                fullStorage[index] = referredStorage[rows[i] + offset];
                        }
                    }
                    break;
            }

            return fullStorage;
        }

        internal sealed override StorageOrder StorageOrder
        {
            get
            {
                return this.parentImplementor.StorageOrder;
            }
        }

        internal sealed override double[] Storage
        {
            get
            {
                this.parentImplementor.ChangingData -= new EventHandler<EventArgs>(this.ChangingDataHandler);
                this.parentImplementor.ImplementorChanged -= new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
                DenseDoubleMatrixImplementor newFullImplementor = (DenseDoubleMatrixImplementor)this;
                ImplementorChangedEventArgs implementorChangedEventArgs = new ImplementorChangedEventArgs(newFullImplementor);
                base.OnImplementorChanged(implementorChangedEventArgs);
                return newFullImplementor.Storage;
            }
        }

        internal sealed override StorageScheme StorageScheme
        {
            get
            {
                return StorageScheme.View;
            }
        }

        #endregion
    }
}


