// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;

using System.Text;
using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;

using Novacta.Analytics.Infrastructure;
using System.Numerics;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a row in a matrix of complex values. 
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// You cannot directly instantiate a <see cref="ComplexMatrixRow"/>.
    /// Instead, rows in a <see cref="ComplexMatrix"/> instance 
    /// can be collected by calling the overloaded method
    /// <see cref="ComplexMatrix.AsRowCollection()"/>.
    /// Such methods return a
    /// <see cref="ComplexMatrixRowCollection"/> object, whose items have 
    /// type <see cref="ComplexMatrixRow"/>.
    /// </para>
    /// <para id='row.in.collection'>
    /// A <see cref="ComplexMatrixRow"/> instance represents a row of a
    /// <see cref="ComplexMatrix"/>. More thoroughly, 
    /// since each <see cref="ComplexMatrixRow"/> is an item in a 
    /// <see cref="ComplexMatrixRowCollection"/>, such matrix can be
    /// inspected by getting the <see cref="ComplexMatrixRowCollection.Matrix"/>
    /// property of the given collection.
    /// </para>
    /// <para><b>Equality</b></para>
    /// <para id='quasi.lexicographic.equality'>
    /// When tested for equality, <see cref="ComplexMatrixRow"/> instances 
    /// are considered equal if and only if they have the same <see cref="Length"/> and, 
    /// for each column index, entries corresponding to such index are equal, too.
    /// </para>
    /// <para><b>Data binding</b></para>
    /// <para id='row.binding.overview'>
    /// Entries of a <see cref="ComplexMatrixRow"/> instance corresponding to a specific 
    /// column index can be get or set through the 
    /// indexer <see cref="this[int]"/>.
    /// When the indexer sets the entry, the row fires 
    /// the <see cref="PropertyChanged"/> event,
    /// notifying that the name of the changed property is <c>"Item[]"</c>; 
    /// as a consequence, subscribers to the event can't know what is the column index 
    /// of the changed entry. This can be problematic if you want to 
    /// use <see cref="ComplexMatrixRow"/> instances as binding sources, for example when 
    /// binding charts or grids to matrix data.
    /// </para>
    /// <para id='row.binding.xdata'>
    /// To overcome such difficulties, the <see cref="ComplexMatrixRow"/> class defines,
    /// among others, the <see cref="XData"/> property. This property 
    /// returns a specific entry of the row, that one having as column index 
    /// the value returned by the <see cref="ComplexMatrixRowCollection.XDataColumn"/> property of 
    /// the <see cref="ComplexMatrixRowCollection"/> which
    /// the row belongs to. In this way, <see cref="XData"/> can be easily
    /// used as a path property when binding to <see cref="ComplexMatrixRow"/> sources.
    /// </para>
    /// <para id='row.binding.xdata.to.matrix'>
    /// If set, the <see cref="XData"/> property fires the 
    /// <see cref="PropertyChanged"/> event, and the new value becomes
    /// the entry of <see cref="ComplexMatrixRowCollection.Matrix"/> having row and column 
    /// indexes given
    /// by <see cref="Index"/> and <see cref="ComplexMatrixRowCollection.XDataColumn"/>,
    ///  respectively.
    /// </para>
    /// <para>
    /// Additional not indexed properties are <see cref="YData"/> 
    /// and <see cref="ZData"/>.
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
    /// <para>
    /// In the following example, the first half of the rows in a matrix is collected.
    /// Then the <see cref="Index"/> of the items in the collection is modified so
    /// that, after that change, the same items represent the second half of the matrix rows.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowIndexDataExample1.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    public sealed class ComplexMatrixRow :
        IEnumerable<Complex>,
        INotifyPropertyChanged,
        IEquatable<ComplexMatrixRow>
    {
        internal ComplexMatrixRowCollection collection;
        int rowIndex;

        /// <summary>
        /// Gets or sets the index of the <see cref="ComplexMatrixRow"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='arrange']"/>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// Property <see cref="Index"/> is the zero-based index assigned 
        /// to the row in that matrix.
        /// </para>
        /// </remarks>
        /// <value>
        /// The index of the <see cref="ComplexMatrixRow"/>.
        /// </value>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="Index"/> of  
        /// a <see cref="ComplexMatrixRow"/> is modified, and 
        /// its <see cref="XData"/> property is 
        /// evaluated before and after that change.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowIndexDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/>  is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="ComplexMatrix.NumberOfRows"/> of the matrix 
        /// of which this instance represents a row.  
        /// </exception>
        public int Index
        {
            get { return this.rowIndex; }
            set
            {
                var matrix = this.collection.matrix;
                if (value < 0 || matrix.NumberOfRows <= value)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }
                if (value != this.rowIndex)
                {
                    int oldIndex = this.rowIndex;
                    this.rowIndex = value;
                    this.NotifyPropertyChanged(nameof(this.Index));

                    int columnIndex = this.collection.xDataColumn;
                    if (matrix[oldIndex, columnIndex] != matrix[this.rowIndex, columnIndex])
                    {
                        this.NotifyPropertyChanged(nameof(this.XData));
                    }

                    columnIndex = this.collection.yDataColumn;
                    if (matrix[oldIndex, columnIndex] != matrix[this.rowIndex, columnIndex])
                    {
                        this.NotifyPropertyChanged(nameof(this.YData));
                    }

                    columnIndex = this.collection.zDataColumn;
                    if (matrix[oldIndex, columnIndex] != matrix[this.rowIndex, columnIndex])
                    {
                        this.NotifyPropertyChanged(nameof(this.ZData));
                    }
                }
            }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ComplexMatrixRow"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static implicit operator ComplexMatrix(ComplexMatrixRow value)
        {
            if (value is null)
            {
                return null;
            }

            return value.collection.matrix[value.rowIndex, ":"];
        }

        /// <summary>
        /// Converts from <see cref="ComplexMatrixRow"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static ComplexMatrix ToComplexMatrix(ComplexMatrixRow value)
        {
            if (value is null)
            {
                return null;
            }

            return value.collection.matrix[value.rowIndex, ":"];
        }


        /// <summary>
        /// Occurs when a property of the <see cref="ComplexMatrixRow"/> changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event  
        /// passing the source property that is being updated.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        internal void NotifyPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal ComplexMatrixRow(int rowIndex)
        {
            this.rowIndex = rowIndex;
        }

        /// <summary>
        /// Gets or sets the entry of the <see cref="ComplexMatrixRow"/>
        /// having the specified column index.
        /// </summary>
        /// <value>The row entry corresponding to the specified
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// If setting, the <see cref="this[int]"/> indexer fires the 
        /// <see cref="PropertyChanged"/> event
        /// notifying that the name of the changed property is <c>"[" + j + ]"</c>, 
        /// where <c>j</c> is a string representation of <paramref name="columnIndex"/>,
        /// and the new value becomes
        /// the entry of <see cref="ComplexMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <paramref name="columnIndex"/>,
        /// respectively.
        /// </para>
        /// <para>
        /// If <paramref name="columnIndex"/> equals any value returned by
        /// <see cref="ComplexMatrixRowCollection.XDataColumn"/>, 
        /// <see cref="ComplexMatrixRowCollection.YDataColumn"/>, or
        /// <see cref="ComplexMatrixRowCollection.ZDataColumn"/>, then the
        /// <see cref="PropertyChanged"/> event is also fired to
        /// notify that properties <see cref="XData"/>, <see cref="YData"/>, or 
        /// <see cref="ZData"/> have changed values, respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, 
        /// all rows in a matrix are collected, and the indexer is applied to set specific
        /// row entries.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowIndexerExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="columnIndex"/> is not a valid column index 
        /// for the matrix from
        /// which this row has been collected.  
        /// </exception>
        /// <seealso cref="XData"/>
        /// <seealso cref="YData"/>
        /// <seealso cref="ZData"/>
        public Complex this[int columnIndex]
        {
            get
            {
                if (columnIndex < 0 || columnIndex >= this.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                return this.collection.matrix[this.rowIndex, columnIndex];
            }
            set
            {
                if (columnIndex < 0 || columnIndex >= this.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var matrix = this.collection.matrix;
                if (value != matrix[this.rowIndex, columnIndex])
                {
                    matrix[this.rowIndex, columnIndex] = value;
                    this.NotifyPropertyChanged("[" + columnIndex + "]");
                    if (this.collection.xDataColumn == columnIndex)
                    {
                        this.NotifyPropertyChanged(nameof(this.XData));
                    }
                    if (this.collection.yDataColumn == columnIndex)
                    {
                        this.NotifyPropertyChanged(nameof(this.YData));
                    }
                    if (this.collection.zDataColumn == columnIndex)
                    {
                        this.NotifyPropertyChanged(nameof(this.ZData));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the entry of the <see cref="ComplexMatrixRow"/>
        /// having the specified column index.
        /// </summary>
        /// <value>The row entry corresponding to the specified
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// If setting, the <see cref="this[string]"/> indexer fires the 
        /// <see cref="PropertyChanged"/> event
        /// notifying that the name of the changed property is <c>"[" + j + ]"</c>, 
        /// where <c>j</c> is <paramref name="columnIndex"/>,
        /// and the new value becomes
        /// the entry of <see cref="ComplexMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <paramref name="columnIndex"/>,
        /// respectively.
        /// </para>
        /// <para>
        /// If <paramref name="columnIndex"/> represents any value returned by
        /// <see cref="ComplexMatrixRowCollection.XDataColumn"/>, 
        /// <see cref="ComplexMatrixRowCollection.YDataColumn"/>, or
        /// <see cref="ComplexMatrixRowCollection.ZDataColumn"/>, then the
        /// <see cref="PropertyChanged"/> event is also fired to
        /// notify that properties <see cref="XData"/>, <see cref="YData"/>, or 
        /// <see cref="ZData"/> have changed values, respectively.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="columnIndex"/> does not represent a valid integer value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="columnIndex"/> does not represent a valid column index 
        /// for the matrix from
        /// which this row has been collected.  
        /// </exception>
        /// <seealso cref="XData"/>
        /// <seealso cref="YData"/>
        /// <seealso cref="ZData"/>
        public Complex this[string columnIndex]
        {
            get
            {
                if (!Int32.TryParse(columnIndex, out int index))
                {
                    throw new ArgumentException(
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PAR_CANNOT_PARSE_AS_INT32"),
                       nameof(columnIndex));
                }
                return this[index];
            }
            set
            {
                if (!Int32.TryParse(columnIndex, out int index))
                {
                    throw new ArgumentException(
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PAR_CANNOT_PARSE_AS_INT32"),
                       nameof(columnIndex));
                }
                this[index] = value;
            }
        }

        /// <summary>
        /// Gets or sets the entry of the <see cref="ComplexMatrixRow"/>
        /// having the column index specified by the 
        /// <see cref="ComplexMatrixRowCollection.XDataColumn"/>
        /// property of the <see cref="ComplexMatrixRowCollection"/> of which the row
        /// is an item.
        /// </summary>
        /// <value>The row entry corresponding to the <see cref="ComplexMatrixRowCollection.XDataColumn"/>
        ///  column index.</value>
        /// <remarks>    
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.xdata']"/>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.xdata.to.matrix']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="ComplexMatrixRowCollection.XDataColumn"/> of 
        /// a collection of matrix 
        /// rows is set, and the <see cref="XData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowXDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="ComplexMatrixRowCollection.XDataColumn"/>
        public Complex XData
        {
            get
            {
                return this.collection.matrix[
                    this.rowIndex,
                    this.collection.xDataColumn];
            }
            set
            {
                var matrix = this.collection.matrix;
                int columnIndex = this.collection.xDataColumn;
                if (value != matrix[this.rowIndex, columnIndex])
                {
                    matrix[this.rowIndex, columnIndex] = value;
                    this.NotifyPropertyChanged(nameof(this.XData));
                    this.NotifyPropertyChanged("[" + columnIndex + "]");
                }
            }
        }

        internal void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "XDataColumn":
                    this.NotifyPropertyChanged(nameof(this.XData));
                    break;
                case "YDataColumn":
                    this.NotifyPropertyChanged(nameof(this.YData));
                    break;
                case "ZDataColumn":
                    this.NotifyPropertyChanged(nameof(this.ZData));
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the entry of the <see cref="ComplexMatrixRow"/>
        /// having the column index specified by the 
        /// <see cref="ComplexMatrixRowCollection.YDataColumn"/>
        /// property of the <see cref="ComplexMatrixRowCollection"/> of which the row
        /// is an item.
        /// </summary>
        /// <value>The row entry corresponding to the <see cref="ComplexMatrixRowCollection.YDataColumn"/>
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="ComplexMatrixRow"/> class defines,
        /// among others, the <see cref="YData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="ComplexMatrixRowCollection.YDataColumn"/> property of 
        /// the <see cref="ComplexMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="YData"/> can be easily
        /// used as a path property when binding to <see cref="ComplexMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="YData"/> property fires the 
        /// <see cref="PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="ComplexMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <see cref="ComplexMatrixRowCollection.YDataColumn"/>,
        ///  respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="ComplexMatrixRowCollection.YDataColumn"/> of 
        /// a collection of matrix 
        /// rows is set, and the <see cref="YData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowYDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="ComplexMatrixRowCollection.YDataColumn"/>
        public Complex YData
        {
            get { return this.collection.matrix[this.rowIndex, this.collection.yDataColumn]; }
            set
            {
                var matrix = this.collection.matrix;
                int columnIndex = this.collection.yDataColumn;
                if (value != matrix[this.rowIndex, columnIndex])
                {
                    matrix[this.rowIndex, columnIndex] = value;
                    this.NotifyPropertyChanged(nameof(this.YData));
                    this.NotifyPropertyChanged("[" + columnIndex + "]");
                }
            }
        }

        /// <summary>
        /// Gets or sets the entry of the <see cref="ComplexMatrixRow"/>
        /// having the column index specified by the 
        /// <see cref="ComplexMatrixRowCollection.ZDataColumn"/>
        /// property of the <see cref="ComplexMatrixRowCollection"/> of which the row
        /// is an item.
        /// </summary>
        /// <value>The row entry corresponding to the <see cref="ComplexMatrixRowCollection.ZDataColumn"/>
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="ComplexMatrixRow"/> class defines,
        /// among others, the <see cref="ZData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="ComplexMatrixRowCollection.ZDataColumn"/> property of 
        /// the <see cref="ComplexMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="ZData"/> can be easily
        /// used as a path property when binding to <see cref="ComplexMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="ZData"/> property fires the 
        /// <see cref="PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="ComplexMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <see cref="ComplexMatrixRowCollection.ZDataColumn"/>,
        ///  respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="ComplexMatrixRowCollection.ZDataColumn"/> of 
        /// a collection of matrix 
        /// rows is set, and the <see cref="ZData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowZDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="ComplexMatrixRowCollection.ZDataColumn"/>
        public Complex ZData
        {
            get { return this.collection.matrix[this.rowIndex, this.collection.zDataColumn]; }
            set
            {
                var matrix = this.collection.matrix;
                int columnIndex = this.collection.zDataColumn;
                if (value != matrix[this.rowIndex, columnIndex])
                {
                    matrix[this.rowIndex, columnIndex] = value;
                    this.NotifyPropertyChanged(nameof(this.ZData));
                    this.NotifyPropertyChanged("[" + columnIndex + "]");
                }
            }
        }

        /// <summary>
        /// Gets the length of the <see cref="ComplexMatrixRow"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// Property <see cref="Length"/> returns the <see cref="ComplexMatrix.NumberOfColumns"/>
        /// of <see cref="ComplexMatrixRowCollection.Matrix"/>.
        /// </para>
        /// </remarks>
        /// <value>The number of columns of the <see cref="ComplexMatrixRow"/>.</value>
        public int Length { get { return this.collection.matrix.NumberOfColumns; } }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return new ComplexMatrixRowEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<Complex> IEnumerable<Complex>.GetEnumerator()
        {
            return new ComplexMatrixRowEnumerator(this);
        }

#region Name, ToString

        /// <summary>
        /// Gets the name of the <see cref="ComplexMatrixRow"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// This property returns the name eventually set to the row
        /// of <see cref="ComplexMatrixRowCollection.Matrix"/> 
        /// having the corresponding <see cref="Index"/>; otherwise <b>null</b>.
        /// </para>
        /// </remarks>
        /// <value>The row name.</value>
        public string Name
        {
            get
            {
                this.collection.matrix.TryGetRowName(this.rowIndex, out string rowName);
                return rowName;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            NumberFormatInfo numberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;

            // The number of chars in a column (included a blank for separation).
            int columnSize = 38;
            // The number of chars in a row name (included a blank for separation).
            int rowNameSize = 17;

            // Real and imaginary complex parts are represented with a precision set
            // to 9. In this way, each part requires a representation having length
            // at most equal to 9 + 1 + 1 + 5 = 16, where additional chars are
            //    the (eventual) minus sign, e.g., "-". (1 char),
            //    the (eventual) decimal point, e.g. ".", (1 char)
            //    the scientific notation exponent, e.g., "e+101" (max 5 chars).
            //
            // By setting the format specifier
            // "({0,17:g9},{1,17:g9}) "
            // we thus have a complex value represented via a string having length
            // 2 * 17 + 4 = 38. 

            string numberFormatSpecifier = "({0,17:g9},{1,17:g9}) ";

            var matrix = this.collection.matrix;

            bool columnsHaveNames = matrix.HasColumnNames;
            string rowName = this.Name;
            bool rowHasName = rowName is not null;

            int numberOfColumns = this.Length;

            // The representation of row names must have length 17, right aligned,
            // but a row name must have no more than 14 chars (two chars needed for '[' and ']', 
            // the last one blank needed for separation.
            string blankRowNamesFormatSpecifier = "{0,-17}";
            string truncatedRowNamesFormatSpecifier = "[{0,-14}] ";
            int maximumRowNameLength = rowNameSize - 3;

            // A column size must have length 38, right aligned,
            // but a column name must have no more than 35 chars (two chars needed for '[' and ']', 
            // the last one blank needed for separation.
            string blankColumnNamesFormatSpecifier = "{0,-38}";
            int maximumColumnNameLength = columnSize - 3;
            string truncatedColumnNamesFormatSpecifier = "[{0,-35}] ";

            if (columnsHaveNames)
            {
                if (rowHasName)
                {
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture,
                        blankRowNamesFormatSpecifier,
                        " ");
                }

                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (matrix.columnNames.TryGetValue(j, out string columnName))
                    {
                        int columnNameLength = columnName.Length;

                        // Names having length greater than <c>maximumNameLength</c> 
                        // are truncated, and the truncation is signaled
                        // by inserting the char '*' at the last available position

                        if (columnNameLength > maximumColumnNameLength)
                        {
                            columnName = columnName.Insert(maximumColumnNameLength - 1, "*")
                                [..maximumColumnNameLength];
                            stringBuilder.AppendFormat(
                                CultureInfo.InvariantCulture,
                                truncatedColumnNamesFormatSpecifier,
                                columnName);
                        }
                        else
                        {
                            columnName = string.Format(
                                CultureInfo.InvariantCulture,
                                "[{0}]",
                                columnName).PadRight(columnSize);
                            stringBuilder.Append(columnName);
                        }
                    }
                    else
                        stringBuilder.AppendFormat(
                            CultureInfo.InvariantCulture,
                            blankColumnNamesFormatSpecifier,
                            " ");
                }
                stringBuilder.Append(Environment.NewLine);
            }

            if (rowHasName)
            {
                int rowNameLength = rowName.Length;
                // Names having length greater than <c>maximumNameLength</c> 
                // are truncated, and the truncation is signaled
                // by inserting the char '*' at the last available position
                if (rowNameLength > maximumRowNameLength)
                {
                    rowName = rowName.Insert(maximumRowNameLength - 1, "*")
                        [..maximumRowNameLength];
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture,
                        truncatedRowNamesFormatSpecifier,
                        rowName);
                }
                else
                {
                    rowName = string.Format(
                        CultureInfo.InvariantCulture,
                        "[{0}]",
                        rowName).PadRight(rowNameSize);
                    stringBuilder.Append(rowName);
                }
            }

            for (int j = 0; j < numberOfColumns; j++)
            {
                var value = this[j];
                var asString = string.Format(
                    numberFormatInfo,
                    numberFormatSpecifier,
                    value.Real,
                    value.Imaginary);
                stringBuilder.Append(asString);
            }

            return stringBuilder.ToString();
        }

#endregion

#region IEquatable

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing 
        /// algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.collection.matrix.NumberOfColumns);

            for (int i = 0; i < this.Length; i++)
                hash.Add(this[i].GetHashCode());

            return hash.ToHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="other" /> 
        /// parameter; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        public bool Equals(ComplexMatrixRow other)
        {
            if (other is null)
                return false;

            int thisLength = this.Length;
            int lengthDifference = other.Length - thisLength;

            if (lengthDifference != 0)
                return false;

            // Here if and only if lengthDifference == 0

            bool result = true;
            Complex thisValue, otherValue;
            for (int j = 0; j < thisLength; j++)
            {
                thisValue = this[j];
                otherValue = other[j];
                if (!thisValue.Equals(otherValue))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> 
        /// is equal to the current <see cref="System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; 
        /// otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        public override bool Equals(Object obj)
        {
            if (obj is null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return this.Equals((ComplexMatrixRow)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="ComplexMatrixRow"/> instance is 
        /// equal to another <see cref="ComplexMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><c>true</c> if <i>left</i> is equal to <i>right</i>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        public static bool operator ==(ComplexMatrixRow left, ComplexMatrixRow right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="ComplexMatrixRow"/> instance is not
        /// equal to another <see cref="ComplexMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><c>true</c> if <i>left</i> is not equal to <i>right</i>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        public static bool operator !=(ComplexMatrixRow left, ComplexMatrixRow right)
        {
            return !(left == right);
        }

#endregion
    }
}