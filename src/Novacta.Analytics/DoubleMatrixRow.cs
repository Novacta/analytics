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

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a row in a matrix of doubles. 
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// You cannot directly instantiate a <see cref="DoubleMatrixRow"/>.
    /// Instead, rows in a <see cref="DoubleMatrix"/> instance 
    /// can be collected by calling the overloaded method
    /// <see cref="DoubleMatrix.AsRowCollection()"/>.
    /// Such methods return a
    /// <see cref="DoubleMatrixRowCollection"/> object, whose items have 
    /// type <see cref="DoubleMatrixRow"/>.
    /// </para>
    /// <para id='row.in.collection'>
    /// A <see cref="DoubleMatrixRow"/> instance represents a row of a
    /// <see cref="DoubleMatrix"/>. More thoroughly, 
    /// since each <see cref="DoubleMatrixRow"/> is an item in a 
    /// <see cref="DoubleMatrixRowCollection"/>, such matrix can be
    /// inspected by getting the <see cref="DoubleMatrixRowCollection.Matrix"/>
    /// property of the given collection.
    /// </para>
    /// <para><b>Comparison</b></para>
    /// <para id='quasi.lexicographic.order'>
    /// <see cref="DoubleMatrixRow"/> instances are quasi-lexicographically ordered. This means
    /// that instances are firstly ordered by their <see cref="Length"/>, and then, within
    /// rows having the same length, by lexicographic order.
    /// </para>
    /// <para id='quasi.lexicographic.equality'>
    /// This also means that when tested for equality, <see cref="DoubleMatrixRow"/> instances 
    /// are considered equal if and only if they have the same <see cref="Length"/> and, 
    /// for each column index, entries corresponding to such index are equal, too.
    /// </para>
    /// <para><b>Data binding</b></para>
    /// <para id='row.binding.overview'>
    /// Entries of a <see cref="DoubleMatrixRow"/> instance corresponding to a specific 
    /// column index can be get or set through the 
    /// indexer <see cref="this[int]"/>.
    /// When the indexer sets the entry, the row fires 
    /// the <see cref="PropertyChanged"/> event,
    /// notifying that the name of the changed property is <c>"Item[]"</c>; 
    /// as a consequence, subscribers to the event can't know what is the column index 
    /// of the changed entry. This can be problematic if you want to 
    /// use <see cref="DoubleMatrixRow"/> instances as binding sources, for example when 
    /// binding charts or grids to matrix data.
    /// </para>
    /// <para id='row.binding.xdata'>
    /// To overcome such difficulties, the <see cref="DoubleMatrixRow"/> class defines,
    /// among others, the <see cref="XData"/> property. This property 
    /// returns a specific entry of the row, that one having as column index 
    /// the value returned by the <see cref="DoubleMatrixRowCollection.XDataColumn"/> property of 
    /// the <see cref="DoubleMatrixRowCollection"/> which
    /// the row belongs to. In this way, <see cref="XData"/> can be easily
    /// used as a path property when binding to <see cref="DoubleMatrixRow"/> sources.
    /// </para>
    /// <para id='row.binding.xdata.to.matrix'>
    /// If set, the <see cref="XData"/> property fires the 
    /// <see cref="PropertyChanged"/> event, and the new value becomes
    /// the entry of <see cref="DoubleMatrixRowCollection.Matrix"/> having row and column 
    /// indexes given
    /// by <see cref="Index"/> and <see cref="DoubleMatrixRowCollection.XDataColumn"/>,
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
    /// <code source="..\Novacta.Analytics.CodeExamples\RowsEnumeratorExample0.cs.txt" 
    /// language="cs" />
    /// </para>
    /// <para>
    /// In the following example, the first half of the rows in a matrix is collected.
    /// Then the <see cref="Index"/> of the items in the collection is modified so
    /// that, after that change, the same items represent the second half of the matrix rows.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\RowIndexDataExample1.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Naming", 
        "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "Type is a data structure.")]
    public sealed class DoubleMatrixRow :
        IEnumerable<double>,
        INotifyPropertyChanged,
        IEquatable<DoubleMatrixRow>,
        IComparable<DoubleMatrixRow>,
        IComparable
    {
        internal DoubleMatrixRowCollection collection;
        int rowIndex;

        /// <summary>
        /// Gets or sets the index of the <see cref="DoubleMatrixRow"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='arrange']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// Property <see cref="Index"/> is the zero-based index assigned 
        /// to the row in that matrix.
        /// </para>
        /// </remarks>
        /// <value>
        /// The index of the <see cref="DoubleMatrixRow"/>.
        /// </value>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="Index"/> of  
        /// a <see cref="DoubleMatrixRow"/> is modified, and 
        /// its <see cref="XData"/> property is 
        /// evaluated before and after that change.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowIndexDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/>  is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is greater 
        /// than or equal to the <see cref="DoubleMatrix.NumberOfRows"/> of the matrix 
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
        /// Performs an implicit conversion from <see cref="DoubleMatrixRow"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static implicit operator DoubleMatrix(DoubleMatrixRow value)
        {
            if (value is null)
            {
                return null;
            }

            return value.collection.matrix[value.rowIndex, ":"];
        }

        /// <summary>
        /// Converts from <see cref="DoubleMatrixRow"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static DoubleMatrix ToDoubleMatrix(DoubleMatrixRow value)
        {
            if (value is null)
            {
                return null;
            }

            return value.collection.matrix[value.rowIndex, ":"];
        }


        /// <summary>
        /// Occurs when a property of the <see cref="DoubleMatrixRow"/> changed.
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

        internal DoubleMatrixRow(int rowIndex)
        {
            this.rowIndex = rowIndex;
        }

        /// <summary>
        /// Gets or sets the entry of the <see cref="DoubleMatrixRow"/>
        /// having the specified column index.
        /// </summary>
        /// <value>The row entry corresponding to the specified
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// If setting, the <see cref="this[int]"/> indexer fires the 
        /// <see cref="PropertyChanged"/> event
        /// notifying that the name of the changed property is <c>"[" + j + ]"</c>, 
        /// where <c>j</c> is a string representation of <paramref name="columnIndex"/>,
        /// and the new value becomes
        /// the entry of <see cref="DoubleMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <paramref name="columnIndex"/>,
        /// respectively.
        /// </para>
        /// <para>
        /// If <paramref name="columnIndex"/> equals any value returned by
        /// <see cref="DoubleMatrixRowCollection.XDataColumn"/>, 
        /// <see cref="DoubleMatrixRowCollection.YDataColumn"/>, or
        /// <see cref="DoubleMatrixRowCollection.ZDataColumn"/>, then the
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
        /// <code source="..\Novacta.Analytics.CodeExamples\RowIndexerExample0.cs.txt" 
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
        public double this[int columnIndex]
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
        /// Gets or sets the entry of the <see cref="DoubleMatrixRow"/>
        /// having the specified column index.
        /// </summary>
        /// <value>The row entry corresponding to the specified
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// If setting, the <see cref="this[string]"/> indexer fires the 
        /// <see cref="PropertyChanged"/> event
        /// notifying that the name of the changed property is <c>"[" + j + ]"</c>, 
        /// where <c>j</c> is <paramref name="columnIndex"/>,
        /// and the new value becomes
        /// the entry of <see cref="DoubleMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <paramref name="columnIndex"/>,
        /// respectively.
        /// </para>
        /// <para>
        /// If <paramref name="columnIndex"/> represents any value returned by
        /// <see cref="DoubleMatrixRowCollection.XDataColumn"/>, 
        /// <see cref="DoubleMatrixRowCollection.YDataColumn"/>, or
        /// <see cref="DoubleMatrixRowCollection.ZDataColumn"/>, then the
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
        public double this[string columnIndex]
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
        /// Gets or sets the entry of the <see cref="DoubleMatrixRow"/>
        /// having the column index specified by the 
        /// <see cref="DoubleMatrixRowCollection.XDataColumn"/>
        /// property of the <see cref="DoubleMatrixRowCollection"/> of which the row
        /// is an item.
        /// </summary>
        /// <value>The row entry corresponding to the <see cref="DoubleMatrixRowCollection.XDataColumn"/>
        ///  column index.</value>
        /// <remarks>    
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.xdata']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.xdata.to.matrix']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="DoubleMatrixRowCollection.XDataColumn"/> of 
        /// a collection of matrix 
        /// rows is set, and the <see cref="XData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowXDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="DoubleMatrixRowCollection.XDataColumn"/>
        public double XData
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
        /// Gets or sets the entry of the <see cref="DoubleMatrixRow"/>
        /// having the column index specified by the 
        /// <see cref="DoubleMatrixRowCollection.YDataColumn"/>
        /// property of the <see cref="DoubleMatrixRowCollection"/> of which the row
        /// is an item.
        /// </summary>
        /// <value>The row entry corresponding to the <see cref="DoubleMatrixRowCollection.YDataColumn"/>
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="DoubleMatrixRow"/> class defines,
        /// among others, the <see cref="YData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="DoubleMatrixRowCollection.YDataColumn"/> property of 
        /// the <see cref="DoubleMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="YData"/> can be easily
        /// used as a path property when binding to <see cref="DoubleMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="YData"/> property fires the 
        /// <see cref="PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="DoubleMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <see cref="DoubleMatrixRowCollection.YDataColumn"/>,
        ///  respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="DoubleMatrixRowCollection.YDataColumn"/> of 
        /// a collection of matrix 
        /// rows is set, and the <see cref="YData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowYDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="DoubleMatrixRowCollection.YDataColumn"/>
        public double YData
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
        /// Gets or sets the entry of the <see cref="DoubleMatrixRow"/>
        /// having the column index specified by the 
        /// <see cref="DoubleMatrixRowCollection.ZDataColumn"/>
        /// property of the <see cref="DoubleMatrixRowCollection"/> of which the row
        /// is an item.
        /// </summary>
        /// <value>The row entry corresponding to the <see cref="DoubleMatrixRowCollection.ZDataColumn"/>
        ///  column index.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.binding.overview']"/>
        /// <para>
        /// To overcome such difficulties, the <see cref="DoubleMatrixRow"/> class defines,
        /// among others, the <see cref="ZData"/> property. This property 
        /// returns a specific entry of the row, that one having as column index 
        /// the value returned by the <see cref="DoubleMatrixRowCollection.ZDataColumn"/> property of 
        /// the <see cref="DoubleMatrixRowCollection"/> which
        /// the row belongs to. In this way, <see cref="ZData"/> can be easily
        /// used as a path property when binding to <see cref="DoubleMatrixRow"/> sources.
        /// </para>
        /// <para>
        /// If set, the <see cref="ZData"/> property fires the 
        /// <see cref="PropertyChanged"/> event, and the new value becomes
        /// the entry of <see cref="DoubleMatrixRowCollection.Matrix"/> having row and column 
        /// indexes given
        /// by <see cref="Index"/> and <see cref="DoubleMatrixRowCollection.ZDataColumn"/>,
        ///  respectively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the <see cref="DoubleMatrixRowCollection.ZDataColumn"/> of 
        /// a collection of matrix 
        /// rows is set, and the <see cref="ZData"/> property of its
        /// rows is evaluated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\RowZDataExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="DoubleMatrixRowCollection.ZDataColumn"/>
        public double ZData
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
        /// Gets the length of the <see cref="DoubleMatrixRow"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// Property <see cref="Length"/> returns the <see cref="DoubleMatrix.NumberOfColumns"/>
        /// of <see cref="DoubleMatrixRowCollection.Matrix"/>.
        /// </para>
        /// </remarks>
        /// <value>The number of columns of the <see cref="DoubleMatrixRow"/>.</value>
        public int Length { get { return this.collection.matrix.NumberOfColumns; } }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return new DoubleMatrixRowEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            return new DoubleMatrixRowEnumerator(this);
        }

        #region Name, ToString

        /// <summary>
        /// Gets the name of the <see cref="DoubleMatrixRow"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='row.in.collection']"/>
        /// <para>
        /// This property returns the name eventually set to the row
        /// of <see cref="DoubleMatrixRowCollection.Matrix"/> 
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
            StringBuilder stringBuilder = new StringBuilder();

            NumberFormatInfo numberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;

            // minimum number of chars in the number representation
            int numberOfCharacters = 17;

            // The precision must be <c>numberOfCharacters</c> 
            // (minimum number of chars in the number representation)
            // minus the number of positions for  additional format characters, such as
            //    the (eventual) minus sign, e.g. "-". (1 char)
            //    the scientific notation exponent, e.g. "e+101" (max 5 chars)
            // In this way, <c>numberOfCharacters</c> is also the MAXIMUM
            // number of chars in the number representation.
            //
            // ----- Current implementation details -----
            // The precision is 10 in the current implementation, so that the 
            // number representation has maximum length equal to 10+1+5 = 16 chars.
            // By setting <c>numberOfCharacters</c> to 17, we know that number representations
            // are always separated by at least one char.

            var matrix = this.collection.matrix;

            string nomberFormatSpecifier = "{0,-17:g10}";
            bool columnsHaveNames = matrix.HasColumnNames;
            string rowName = this.Name;
            bool rowHasName = !(rowName is null);

            int numberOfColumns = this.Length;

            // The representation of names must have length 17, right aligned,
            // but a name must have no more than 14 chars 
            // (two char for brackets, the last one to 
            // needed to separate them each from the other).
            string blankNamesFormatSpecifier = "{0,-17}";
            string truncatedNamesFormatSpecifier = "[{0,-14}] ";
            int maximumNameLength = numberOfCharacters - 3;

            if (columnsHaveNames)
            {
                if (rowHasName)
                {
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture,
                        blankNamesFormatSpecifier, 
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

                        if (columnNameLength > maximumNameLength)
                        {
                            columnName = columnName.Insert(maximumNameLength - 1, "*")
                                .Substring(0, maximumNameLength);
                            stringBuilder.AppendFormat(
                                CultureInfo.InvariantCulture,
                                truncatedNamesFormatSpecifier,
                                columnName);
                        }
                        else
                        {
                            columnName = string.Format(
                                CultureInfo.InvariantCulture,
                                "[{0}]", 
                                columnName).PadRight(numberOfCharacters);
                            stringBuilder.Append(columnName);
                        }
                    }
                    else
                        stringBuilder.AppendFormat(
                            CultureInfo.InvariantCulture,
                            blankNamesFormatSpecifier, 
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
                if (rowNameLength > maximumNameLength)
                {
                    rowName = rowName.Insert(maximumNameLength - 1, "*")
                        .Substring(0, maximumNameLength);
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture,
                        truncatedNamesFormatSpecifier, 
                        rowName);
                }
                else
                {
                    rowName = string.Format(
                        CultureInfo.InvariantCulture,
                        "[{0}]", 
                        rowName).PadRight(numberOfCharacters);
                    stringBuilder.Append(rowName);
                }

            }

            for (int j = 0; j < numberOfColumns; j++)
            {
                stringBuilder.AppendFormat(numberFormatInfo, nomberFormatSpecifier, this[j]);
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region IEquatable, IComparable

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing 
        /// algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            int hashCode = this.collection.matrix.NumberOfColumns.GetHashCode();

            for (int i = 0; i < this.Length; i++)
                hashCode ^= this[i].GetHashCode();

            return hashCode;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <returns>A value that indicates the relative order of the objects 
        /// being compared. The return value has the following meanings: 
        ///      <list type="table">
        ///         <listheader>
        ///            <term>Value</term>
        ///            <term>Meaning</term>
        ///         </listheader>
        ///         <item>
        ///            <term>Less than zero</term>
        ///            <term>This object is less than the <paramref name="other" /> parameter.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Zero</term>
        ///            <term>This object is equal to <paramref name="other" />.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Greater than zero</term>
        ///            <term>This object is greater than <paramref name="other" />.</term>
        ///         </item>        
        ///    </list>
        /// </returns>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public int CompareTo(DoubleMatrixRow other)
        {
            if (other is null)
                return 1;

            //  A value that indicates the relative order of the objects being compared. 
            //  The return value has the following meanings: 
            //
            //  Value                    Meaning
            //
            //  Less than zero          This object is less than the other parameter.
            //  Zero                    This object is equal to other.
            //  Greater than zero        This object is greater than other.

            // Quasi-lexicographic order

            // Order by length first
            int thisLength = this.Length;
            int lengthDifference = other.Length - thisLength;

            if (lengthDifference > 0)
                return -1;

            if (lengthDifference < 0)
                return 1;

            // Here if and only if lengthDifference == 0

            int result = 0;
            double thisValue, otherValue;
            for (int j = 0; j < thisLength; j++)
            {
                thisValue = this[j];
                otherValue = other[j];
                if (thisValue < otherValue)
                {
                    result = -1;
                    break;
                }
                else if (thisValue > otherValue)
                {
                    result = 1;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and 
        /// returns an integer that indicates whether the current instance precedes, follows, 
        /// or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <returns>A value that indicates the relative order of the objects being compared. 
        /// The return value has these meanings: 
        ///      <list type="table">
        ///         <listheader>
        ///            <term>Value</term>
        ///            <term>Meaning</term>
        ///         </listheader>
        ///         <item>
        ///            <term>Less than zero</term>
        ///            <term>This instance precedes <paramref name="obj" /> in the sort order.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Zero</term>
        ///            <term>This instance occurs in the same position in the sort order as <paramref name="obj" />.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Greater than zero</term>
        ///            <term>This instance follows <paramref name="obj" /> in the sort order.</term>
        ///         </item>        
        ///    </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="obj"/>
        /// is not the same type as this instance.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public int CompareTo(Object obj)
        {
            if (obj is null)
                return 1;

            if (this.GetType() == obj.GetType())
            {
                return this.CompareTo((DoubleMatrixRow)(obj));
            }

            throw new ArgumentException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_OBJ_HAS_WRONG_TYPE"), "DoubleMatrixRow"),
               nameof(obj));
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="DoubleMatrixRow"/> instance is 
        /// less than another <see cref="DoubleMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <i>left</i> is less than <i>right</i>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>      
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator <(DoubleMatrixRow left, DoubleMatrixRow right)
        {
            if (left is null)
            {
                return right is null ? false : true;
            }

            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="DoubleMatrixRow"/> instance is 
        /// less than or equal to another <see cref="DoubleMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <i>left</i> is less than or equal to <i>right</i>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator <=(DoubleMatrixRow left, DoubleMatrixRow right)
        {
            if (left is null)
            {
                return true;
            }

            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="DoubleMatrixRow"/> instance is 
        /// greater than another <see cref="DoubleMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <i>left</i> is greater than <i>right</i>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator >(DoubleMatrixRow left, DoubleMatrixRow right)
        {
            if (right is null)
            {
                return left is null ? false : true;
            }

            return right.CompareTo(left) < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="DoubleMatrixRow"/> instance is 
        /// greater than or equal to another <see cref="DoubleMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <i>left</i> is greater than or equal to <i>right</i>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator >=(DoubleMatrixRow left, DoubleMatrixRow right)
        {
            if (right is null)
            {
                return true;
            }

            return right.CompareTo(left) <= 0;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><b>true</b> if the current object is equal to the <paramref name="other" /> 
        /// parameter; otherwise, <b>false</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public bool Equals(DoubleMatrixRow other)
        {
            return (0 == this.CompareTo(other));
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> 
        /// is equal to the current <see cref="System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><b>true</b> if the specified object is equal to the current object; 
        /// otherwise, <b>false</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public override bool Equals(Object obj)
        {
            if (obj is null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return this.Equals((DoubleMatrixRow)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="DoubleMatrixRow"/> instance is 
        /// equal to another <see cref="DoubleMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <i>left</i> is equal to <i>right</i>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator ==(DoubleMatrixRow left, DoubleMatrixRow right)
        {
            if (left is null)
            {
                return right is null ? true : false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether a <see cref="DoubleMatrixRow"/> instance is not
        /// equal to another <see cref="DoubleMatrixRow"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <i>left</i> is not equal to <i>right</i>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="DoubleMatrixRow" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator !=(DoubleMatrixRow left, DoubleMatrixRow right)
        {
            return !(left == right);
        }

        #endregion
    }
}
