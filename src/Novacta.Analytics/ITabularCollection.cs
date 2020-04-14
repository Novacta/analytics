// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Defines methods to manipulate collections whose elements are
    /// arranged in rows and columns.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the values arranged in tabular form.</typeparam>
    /// <typeparam name="TCollection">
    /// The type used to assign or refer to multiple positions in the
    /// tabular collection.
    /// </typeparam>
    /// <remarks>
    /// <para id='dimensions.0'><b>Dimensions</b></para>
    /// <para id='dimensions.1'>
    /// The dimensions of a 
    /// <see cref="ITabularCollection{TValue, TCollection}"/> can be 
    /// inspected using the properties
    /// <see cref="NumberOfRows"/> and
    /// <see cref="NumberOfColumns"/>.
    /// </para>
    /// <para><b>Indexing</b></para>
    /// <para id='arrange'>                  
    /// In a tabular collection, elements are arranged in rows and columns. 
    /// Zero-based indexes are assigned to rows and columns, so that
    /// each element can be identified by the indexes of the specific row 
    /// and column on which it lies.
    /// </para>
    /// <para id='matrix'>
    /// Let <latex mode="inline">A</latex> be an instance whose type 
    /// implements the <see cref="ITabularCollection{TValue, TCollection}"/> 
    /// interface, and consider its generic element
    /// <latex mode="display">
    /// A[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1,
    /// </latex>
    /// where <latex mode="inline">m</latex> and  
    /// <latex mode="inline">n</latex> are the number of rows and columns 
    /// of <latex mode="inline">A</latex>, respectively.
    /// </para>
    /// <para>
    /// Element <latex mode="inline">A_{i,j}</latex> can be set or get 
    /// through the
    /// indexer 
    /// <see cref="ITabularCollection{TValue, TCollection}.this[System.Int32,System.Int32]"/>. 
    /// Further overloads of the indexer enable the access to tabular 
    /// sub-collections, as well.
    /// </para>
    /// </remarks>
    /// <seealso cref="DoubleMatrix"/>
    /// <seealso cref="ReadOnlyDoubleMatrix"/>
    /// <seealso cref="CategoricalDataSet"/>
    public interface ITabularCollection<TValue, TCollection>
        where TCollection : ITabularCollection<TValue, TCollection>
    {
        /// <summary>
        /// Gets the number of rows of this instance.
        /// </summary>
        /// <value>The number of rows of this instance.</value>
        int NumberOfRows { get; }

        /// <summary>
        /// Gets the number of columns of this instance.
        /// </summary>
        /// <value>The number of columns of this instance.</value>
        int NumberOfColumns { get; }

        /// <summary>
        /// Gets or sets the element of this instance corresponding to the 
        /// specified row and column indexes.
        /// </summary>
        /// <param name="rowIndex">
        /// The zero-based row index of the element to get or set.
        /// </param>
        /// <param name="columnIndex">
        /// The zero-based column index of the element to get or set.
        /// </param>
        /// <value>
        /// The element corresponding to the specified row and column indexes.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is equal to or greater than 
        /// <see cref="NumberOfRows"/>.<br/>
        /// -or- <br/>
        /// <paramref name="columnIndex"/> is less than zero. <br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is equal to or greater than 
        /// <see cref="NumberOfColumns"/>.
        /// </exception>
        TValue this[int rowIndex, int columnIndex] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to the specified row
        /// and column indexes.
        /// </summary>
        /// <param name="rowIndex">
        /// The zero-based row index of the elements to get or set.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get or set.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the specified row 
        /// and column indexes.</value>
        /// <remarks>
        /// <para id='set'>
        /// When setting elements simultaneously, the <paramref name="value"/> must be a 
        /// tabular collection having a number of rows equal to the number of specified row indexes, 
        /// and a number of columns equal to the number of specified column indexes.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="columnIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> is 
        /// less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is greater than or equal to the 
        /// number of rows of this instance.
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains an index 
        /// which is greater than or equal to the 
        /// number of columns of this instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of rows in <paramref name="value"/> is greater than 1.<br/>
        /// -or-<br/>
        /// The number of columns in <paramref name="value"/> is not equal to 
        /// the <see cref="IndexCollection.Count"/> of <paramref name="columnIndexes"/>.
        /// </exception>
        TCollection this[int rowIndex, IndexCollection columnIndexes] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to the 
        /// specified row and column indexes.
        /// </summary>
        /// <param name="rowIndex">
        /// The zero-based row index of the elements to get or set.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get or set.
        /// The value must be <c>":"</c>, which means that all valid column indexes
        /// are specified.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="columnIndexes"/> is <b>null</b>.<br/>    
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> is 
        /// less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is greater than or equal to the 
        /// number of rows of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved for 
        /// tabular collection sub-referencing.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of rows in <paramref name="value"/> is greater than 1.<br/>
        /// -or-<br/>  
        /// The number of columns in <paramref name="value"/> 
        /// is not equal to the number of columns of this instance.
        /// </exception>
        TCollection this[int rowIndex, string columnIndexes] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to the specified row
        /// and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get or set.</param>
        /// <param name="columnIndex">
        /// The zero-based column index of the elements to get or set.</param>
        /// <value>A tabular collection formed by the elements corresponding to 
        /// the specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the number of rows of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is 
        /// less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is greater than or equal to the 
        /// number of columns of this instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of columns in <paramref name="value"/> is greater than 1.<br/>
        /// -or-<br/> 
        /// The number of rows in <paramref name="value"/> is not equal to 
        /// the <see cref="IndexCollection.Count"/> of <paramref name="rowIndexes"/>.
        /// </exception>
        TCollection this[IndexCollection rowIndexes, int columnIndex] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to the specified row
        /// and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get or set.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get or set.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the number of rows of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains an index 
        /// which is greater than or equal to the number of columns of this instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of rows in <paramref name="value"/> is not equal to 
        /// the <see cref="IndexCollection.Count"/> of <paramref name="rowIndexes"/>.<br/>
        /// -or- <br/>
        /// The number of columns in <paramref name="value"/> is not equal to 
        /// the <see cref="IndexCollection.Count"/> of <paramref name="columnIndexes"/>.
        /// </exception>
        TCollection this[IndexCollection rowIndexes, IndexCollection columnIndexes] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to 
        /// the specified row and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get or set.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get or set.
        /// The value must be <c>":"</c>, which means that all valid column indexes
        /// are specified.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the number of rows of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved for 
        /// tabular collection sub-referencing.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of rows in <paramref name="value"/> is not equal to 
        /// the <see cref="IndexCollection.Count"/> of <paramref name="rowIndexes"/>.<br/> 
        /// -or- <br/>
        /// The number of columns in <paramref name="value"/> is not equal to the number 
        /// of columns of this instance.
        /// </exception>
        TCollection this[IndexCollection rowIndexes, string columnIndexes] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to the specified row
        /// and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get or set.
        /// The value must be <c>":"</c>, which means that all valid row indexes
        /// are specified.</param>
        /// <param name="columnIndex">
        /// The zero-based column index of the elements to get or set.</param>
        /// <value>
        /// A tabular collection formed by the entries corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> is not a string reserved for 
        /// collection sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is
        /// less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is greater than or equal to the 
        /// number of columns of this instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of rows in <paramref name="value"/> is not equal to 
        /// the to the number of rows of this instance.<br/> 
        /// -or- <br/>
        /// The number of columns in <paramref name="value"/> is greater than 1.
        /// </exception>
        TCollection this[string rowIndexes, int columnIndex] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to 
        /// the specified row and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get or set.
        /// The value must be <c>":"</c>, which means that all valid row indexes
        /// are specified.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get or set.</param>
        /// <value>
        /// A tabular collection formed by the entries corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> is not a string reserved 
        /// for tabular collection sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains an index 
        /// which is greater than or equal to the 
        /// number of columns of this instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of rows in <paramref name="value"/> is not equal to 
        /// the number of rows of this instance. <br/>
        /// -or- <br/>
        /// The number of columns in <paramref name="value"/> is not 
        /// equal to the <see cref="IndexCollection.Count"/> of 
        /// <paramref name="columnIndexes"/>.
        /// </exception>
        TCollection this[string rowIndexes, IndexCollection columnIndexes] { get; set; }

        /// <summary>
        /// Gets or sets the elements of this instance corresponding to 
        /// the specified row and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get or set.
        /// The value must be <c>":"</c>, which means that all valid row indexes
        /// are specified.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get or set.
        /// The value must be <c>":"</c>, which means that all valid column indexes
        /// are specified.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]" 
        /// path="para[@id='set']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> is not a string reserved 
        /// for tabular collection sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved 
        /// for tabular collection sub-referencing.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> has not the same dimensions of this instance.
        /// </exception>
        TCollection this[string rowIndexes, string columnIndexes] { get; set; }
    }
}
