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
    /// Represents a collection of rows from a <see cref="ComplexMatrix"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// You cannot directly instantiate a <see cref="ComplexMatrixRowCollection"/>.
    /// Instead, the collection of all rows in a <see cref="ComplexMatrix"/> instance can 
    /// be obtained by calling <see cref="ComplexMatrix.AsRowCollection()"/>, or 
    /// you can collect rows having specific indexes by 
    /// calling <see cref="ComplexMatrix.AsRowCollection(IndexCollection)"/>. Such methods return 
    /// a <see cref="ComplexMatrixRowCollection"/> object, whose items have 
    /// type <see cref="ComplexMatrixRow"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, the rows of a matrix 
    /// are enumerated.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowsEnumeratorExample0.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    public sealed class ComplexMatrixRowCollection
        : ReadOnlyObservableCollection<ComplexMatrixRow>
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

        internal ComplexMatrix matrix;

        /// <summary>
        /// Gets the <see cref="Matrix"/> whose rows this instance collects.
        /// </summary>
        /// <value>
        /// The <see cref="Matrix"/> whose rows are collected by this instance.
        /// </value>
        public ComplexMatrix Matrix { get { return this.matrix; } }

#endregion

        internal ComplexMatrixRowCollection(ObservableCollection<ComplexMatrixRow> list,
            ComplexMatrix matrix)
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
        /// <see cref="ComplexMatrixRow.XData"/> property of the rows in the collection.
        /// </summary>
        /// <value>The column index used when evaluating 
        /// the <see cref="ComplexMatrixRow.XData"/> property of the collected rows.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="ComplexMatrixRow"/> class defines,
        /// among others, the <see cref="ComplexMatrixRow.XData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="XDataColumn"/> property of 
        /// the <see cref="ComplexMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="ComplexMatrixRow.XData"/> can be easily
        /// used as a path property when binding to <see cref="ComplexMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="ComplexMatrixRow.XData"/> property fires the 
        /// <see cref="ComplexMatrixRow.PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="Matrix"/> having row and column indexes given by
        /// <see cref="ComplexMatrixRow.Index"/> and <see cref="XDataColumn"/>, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="XDataColumn"/> of a collection of matrix 
        /// rows is set, and the <see cref="ComplexMatrixRow.XData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowXDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/>  is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="ComplexMatrix.NumberOfColumns"/> of the matrix 
        /// whose rows this instance is collecting.  
        /// </exception>
        /// <seealso cref="ComplexMatrixRow.XData"/>
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
        /// <see cref="ComplexMatrixRow.YData"/> property of the rows in the collection.
        /// </summary>
        /// <value>The column index used when evaluating 
        /// the <see cref="ComplexMatrixRow.YData"/> property of the collected rows.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="ComplexMatrixRow"/> class defines,
        /// among others, the <see cref="ComplexMatrixRow.YData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="YDataColumn"/> property of 
        /// the <see cref="ComplexMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="ComplexMatrixRow.YData"/> can be easily
        /// used as a path property when binding to <see cref="ComplexMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="ComplexMatrixRow.YData"/> property fires the 
        /// <see cref="ComplexMatrixRow.PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="Matrix"/> having row and column indexes given by
        /// <see cref="ComplexMatrixRow.Index"/> and <see cref="YDataColumn"/>, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="YDataColumn"/> of a collection of matrix 
        /// rows is set, and the <see cref="ComplexMatrixRow.YData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowYDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="ComplexMatrix.NumberOfColumns"/> of the matrix 
        /// whose rows this instance is collecting.  
        /// </exception>
        /// <seealso cref="ComplexMatrixRow.YData"/>
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
        /// <see cref="ComplexMatrixRow.ZData"/> property of the rows in the collection.
        /// </summary>
        /// <value>The column index used when evaluating 
        /// the <see cref="ComplexMatrixRow.ZData"/> property of the collected rows.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="ComplexMatrixRow"/> class defines,
        /// among others, the <see cref="ComplexMatrixRow.ZData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="ZDataColumn"/> property of 
        /// the <see cref="ComplexMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="ComplexMatrixRow.ZData"/> can be easily
        /// used as a path property when binding to <see cref="ComplexMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="ComplexMatrixRow.ZData"/> property fires the 
        /// <see cref="ComplexMatrixRow.PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="Matrix"/> having row and column indexes given by
        /// <see cref="ComplexMatrixRow.Index"/> and <see cref="ZDataColumn"/>, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="ZDataColumn"/> of a collection of matrix 
        /// rows is set, and the <see cref="ComplexMatrixRow.ZData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowZDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="ComplexMatrix.NumberOfColumns"/> of the matrix 
        /// whose rows this instance is collecting.  
        /// </exception>
        /// <seealso cref="ComplexMatrixRow.ZData"/>
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
        /// from <see cref="ComplexMatrixRowCollection"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static implicit operator ComplexMatrix(ComplexMatrixRowCollection value)
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
        /// from <see cref="ComplexMatrixRowCollection"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static ComplexMatrix ToComplexMatrix(ComplexMatrixRowCollection value)
        {
            return value;
        }
    }
}