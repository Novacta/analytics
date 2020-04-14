// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.ObjectModel;

using Novacta.Analytics.Infrastructure;
using System.ComponentModel;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a collection of rows from a <see cref="DoubleMatrix"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// You cannot directly instantiate a <see cref="DoubleMatrixRowCollection"/>.
    /// Instead, the collection of all rows in a <see cref="DoubleMatrix"/> instance can 
    /// be obtained by calling <see cref="DoubleMatrix.AsRowCollection()"/>, or 
    /// you can collect rows having specific indexes by 
    /// calling <see cref="DoubleMatrix.AsRowCollection(IndexCollection)"/>. Such methods return 
    /// a <see cref="DoubleMatrixRowCollection"/> object, whose items have 
    /// type <see cref="DoubleMatrixRow"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, the rows of a matrix 
    /// are enumerated.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\RowsEnumeratorExample0.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    public sealed class DoubleMatrixRowCollection
        : ReadOnlyObservableCollection<DoubleMatrixRow>
    {
        /*
        Rows are strongly coupled with a collection: e.g., rows must subscribe to 
        PropertyChanged events fired by the collection. This implies that enabling
        the addition of a row in a new collection should not be allowed, otherwise
        a race among the collections in which a given row is added rises about the
        handling of the corresponding PropertyChanged events related to 
        [X|Y|Z]DataColumn changes.

        As a consequence, the collection type 
        INHERITS FROM ReadOnlyObservableCollection.
        */

        #region Matrix 

        internal DoubleMatrix matrix;

        /// <summary>
        /// Gets the <see cref="Matrix"/> whose rows this instance collects.
        /// </summary>
        /// <value>
        /// The <see cref="Matrix"/> whose rows are collected by this instance.
        /// </value>
        public DoubleMatrix Matrix { get { return this.matrix; } }

        #endregion

        internal DoubleMatrixRowCollection(ObservableCollection<DoubleMatrixRow> list,
            DoubleMatrix matrix)
            : base(list)
        {
            this.matrix = matrix;
            INotifyPropertyChanged notifier = this as INotifyPropertyChanged;
            foreach (var row in this)
            {
                row.collection = this;
                notifier.PropertyChanged += row.PropertyChangedHandler;
            }
        }

        #region XDataColumn

        internal int xDataColumn;

        /// <summary>
        /// Gets or sets the column index of the entries which are to be returned by the 
        /// <see cref="DoubleMatrixRow.XData"/> property of the rows in the collection.
        /// </summary>
        /// <value>The column index used when evaluating 
        /// the <see cref="DoubleMatrixRow.XData"/> property of the collected rows.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="DoubleMatrixRow"/> class defines,
        /// among others, the <see cref="DoubleMatrixRow.XData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="XDataColumn"/> property of 
        /// the <see cref="DoubleMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="DoubleMatrixRow.XData"/> can be easily
        /// used as a path property when binding to <see cref="DoubleMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="DoubleMatrixRow.XData"/> property fires the 
        /// <see cref="DoubleMatrixRow.PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="Matrix"/> having row and column indexes given by
        /// <see cref="DoubleMatrixRow.Index"/> and <see cref="XDataColumn"/>, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="XDataColumn"/> of a collection of matrix 
        /// rows is set, and the <see cref="DoubleMatrixRow.XData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowXDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/>  is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="DoubleMatrix.NumberOfColumns"/> of the matrix 
        /// whose rows this instance is collecting.  
        /// </exception>
        /// <seealso cref="DoubleMatrixRow.XData"/>
        public int XDataColumn
        {
            get { return this.xDataColumn; }
            set
            {
                if (value < 0 || this.matrix.NumberOfColumns <= value)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"));
                }
                if (value != this.xDataColumn)
                {
                    this.xDataColumn = value;
                    this.OnPropertyChanged(
                        new PropertyChangedEventArgs(nameof(this.XDataColumn)));
                }
            }
        }

        #endregion

        #region YDataColumn

        internal int yDataColumn;

        /// <summary>
        /// Gets or sets the column index of the entries which are to be returned by the 
        /// <see cref="DoubleMatrixRow.YData"/> property of the rows in the collection.
        /// </summary>
        /// <value>The column index used when evaluating 
        /// the <see cref="DoubleMatrixRow.YData"/> property of the collected rows.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="DoubleMatrixRow"/> class defines,
        /// among others, the <see cref="DoubleMatrixRow.YData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="YDataColumn"/> property of 
        /// the <see cref="DoubleMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="DoubleMatrixRow.YData"/> can be easily
        /// used as a path property when binding to <see cref="DoubleMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="DoubleMatrixRow.YData"/> property fires the 
        /// <see cref="DoubleMatrixRow.PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="Matrix"/> having row and column indexes given by
        /// <see cref="DoubleMatrixRow.Index"/> and <see cref="YDataColumn"/>, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="YDataColumn"/> of a collection of matrix 
        /// rows is set, and the <see cref="DoubleMatrixRow.YData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowYDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="DoubleMatrix.NumberOfColumns"/> of the matrix 
        /// whose rows this instance is collecting.  
        /// </exception>
        /// <seealso cref="DoubleMatrixRow.YData"/>
        public int YDataColumn
        {
            get { return this.yDataColumn; }
            set
            {
                if (value < 0 || this.matrix.NumberOfColumns <= value)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"));
                }
                if (value != this.yDataColumn)
                {
                    this.yDataColumn = value;
                    this.OnPropertyChanged(
                        new PropertyChangedEventArgs(nameof(this.YDataColumn)));
                }
            }
        }

        #endregion

        #region ZDataColumn

        internal int zDataColumn;

        /// <summary>
        /// Gets or sets the column index of the entries which are to be returned by the 
        /// <see cref="DoubleMatrixRow.ZData"/> property of the rows in the collection.
        /// </summary>
        /// <value>The column index used when evaluating 
        /// the <see cref="DoubleMatrixRow.ZData"/> property of the collected rows.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="DoubleMatrixRow"/> class defines,
        /// among others, the <see cref="DoubleMatrixRow.ZData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="ZDataColumn"/> property of 
        /// the <see cref="DoubleMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="DoubleMatrixRow.ZData"/> can be easily
        /// used as a path property when binding to <see cref="DoubleMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="DoubleMatrixRow.ZData"/> property fires the 
        /// <see cref="DoubleMatrixRow.PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="Matrix"/> having row and column indexes given by
        /// <see cref="DoubleMatrixRow.Index"/> and <see cref="ZDataColumn"/>, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="ZDataColumn"/> of a collection of matrix 
        /// rows is set, and the <see cref="DoubleMatrixRow.ZData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowZDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="DoubleMatrix.NumberOfColumns"/> of the matrix 
        /// whose rows this instance is collecting.  
        /// </exception>
        /// <seealso cref="DoubleMatrixRow.ZData"/>
        public int ZDataColumn
        {
            get { return this.zDataColumn; }
            set
            {
                if (value < 0 || this.matrix.NumberOfColumns <= value)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"));
                }
                if (value != this.zDataColumn)
                {
                    this.zDataColumn = value;
                    this.OnPropertyChanged(
                        new PropertyChangedEventArgs(nameof(this.ZDataColumn)));
                }
            }
        }

        #endregion

        /// <summary>
        /// Performs an implicit conversion 
        /// from <see cref="DoubleMatrixRowCollection"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static implicit operator DoubleMatrix(DoubleMatrixRowCollection value)
        {
            if (value is null)
            {
                return null;
            }

            if (value.Count == value.matrix.NumberOfRows)
            {
                return value.matrix;
            }

            int numberOfCollectedRows = value.Count;
            int[] rowIndexes = new int[numberOfCollectedRows];
            for (int i = 0; i < numberOfCollectedRows; i++)
            {
                rowIndexes[i] = value[i].Index;
            }
            return value.matrix[IndexCollection.FromArray(rowIndexes), ":"];
        }

        /// <summary>
        /// Converts 
        /// from <see cref="DoubleMatrixRowCollection"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static DoubleMatrix ToDoubleMatrix(DoubleMatrixRowCollection value)
        {
            return value;
        }
    }
}