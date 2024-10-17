// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using System.Collections.ObjectModel;

using Novacta.Analytics.Infrastructure;
using System.Linq;
using System.Numerics;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a collection of complex values arranged in rows and columns.
    /// Provides methods to operate algebraically on matrices.
    ///</summary>
    ///<remarks>
    /// <para><b>Instantiation</b></para>    
    /// <para>
    /// <see cref="ComplexMatrix"/> objects can be created using different storage 
    /// schemes. To allocate storage for each matrix entry, so applying 
    /// a <see cref="StorageScheme.Dense"/> scheme, one can exploit one of the 
    /// overloaded factory
    /// method <c>Dense</c>, such as <see cref="ComplexMatrix.Dense(int, int)"/>. 
    /// On the contrary, method 
    /// <see cref="Sparse">Sparse</see> returns
    /// instances whose scheme 
    /// is <see cref="StorageScheme.CompressedRow">StorageScheme.CompressedRow</see>,
    /// which means that storage is allocated for non-zero entries only, using
    /// a compressed sparse row scheme.
    /// </para>
    /// <para><b>Indexing</b></para>
    /// <para id='arrange'>                  
    /// In a matrix, entries are arranged in rows and columns.
    /// Zero-based indexes are assigned to rows and columns, so that
    /// each entry can be identified by the indexes of the specific row 
    /// and column on which it lies.
    /// </para>
    /// <para id='matrix'>
    /// Let <latex mode="inline">A</latex> be a matrix, and consider its generic entry
    /// <latex mode="display">
    /// A_{i,j},\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1,
    /// </latex>
    /// where <latex mode="inline">m</latex> and  
    /// <latex mode="inline">n</latex> are the 
    /// number of rows and columns of <latex mode="inline">A</latex>, respectively.
    /// </para>
    /// <para>
    /// Entry <latex mode="inline">A_{i,j}</latex> can be set or get through the
    /// indexer <see cref="this[int,int]"/>. Further overloads
    /// of the indexer enable the access to sub-matrices, as well.
    /// </para>
    /// <para id='linear.indexing'>
    /// A <b><i>linear index</i></b> completely
    /// identifies an entry, 
    /// assuming that entries are linearly ordered following the 
    /// <see cref="StorageOrder.ColumnMajor"/>
    /// data order. This means that entry <latex mode="inline">A_{i,j}</latex> has linear
    /// index equal to <latex mode="inline">i + (m)j</latex>, and matrix entries can be enumerated as
    /// follows:
    /// <latex mode="display">
    /// A_l,\hspace{12pt} l=0,\dots,L-1,
    /// </latex>    
    /// where <latex mode="inline">L = (m)n</latex> is the <see cref="Count"/> of the matrix.
    /// </para>
    /// <para>
    /// Given a linear index, the corresponding entry can be accessed
    /// via the indexer <see cref="this[int]"/>.
    /// In order to retrieve entries corresponding to
    /// multiple linear indexes simultaneously, linear indexers are also overloaded to accept 
    /// <see cref="IndexCollection"/> instances,
    /// or strings as arguments.
    /// </para>
    /// <para id='dimensions.0'><b>Dimensions</b></para>
    /// <para id='dimensions.1'>
    /// The dimensions of a matrix can be inspected using the properties
    /// <see cref="NumberOfRows"/> and
    /// <see cref="NumberOfColumns"/>,
    /// Use the property
    /// <see cref="Count"/>
    /// to know how many entries are arranged in a matrix.
    /// </para>
    /// <para id='dimensions.2'>
    /// The following table reports some particular dimensions 
    /// for which a property exists
    /// which can be evaluated to inspect if the dimensions hold true for a given 
    /// matrix.
    /// </para>
    /// <para id='dimensions.3'>
    ///   <list type="table">
    ///     <listheader>
    ///        <term>Matrix Property</term>
    ///        <term>Number of rows</term>
    ///        <term>Number of columns</term>
    ///     </listheader>
    ///     <item>
    ///        <term><see cref="IsScalar"/></term>
    ///        <term>1</term>
    ///        <term>1</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsRowVector"/></term>
    ///        <term>1</term>
    ///        <term>Any</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsColumnVector"/></term>
    ///        <term>Any</term>
    ///        <term>1</term>
    ///     </item>
    ///  </list>
    /// </para>
    /// <para id='dimensions.4'>
    /// In addition, property <see cref="IsSquare"/> returns <c>true</c>
    /// for matrices having the same number of rows and columns.
    /// You can also inspect property <see cref="IsVector"/> to verify
    /// if a matrix instance has only one row or column.
    /// </para>
    /// 
    /// <para><b>Diagonals</b></para>
    /// <para>
    /// There are <latex mode="inline">m+n-1</latex> diagonals in <latex mode="inline">A</latex>.
    /// </para>
    /// <para id='main'>The <b>main diagonal</b> of <latex mode="inline">A</latex> is also said the diagonal
    /// of order <c>0</c>, and is the collection of 
    /// entries <latex mode="inline">A_{i,j}</latex> such 
    /// that <latex mode="inline">i=j</latex>. 
    /// </para>
    /// <para id='super'>
    /// If <latex mode="inline">n>1</latex>, then the matrix has 
    /// <latex mode="inline">n-1</latex> <b> super-diagonals</b>: for 
    /// <latex mode="inline">k=1,\dots,n-1</latex>, the 
    /// <latex mode="inline">k</latex>-th super-diagonal is the collection of entries
    /// corresponding to the positions
    /// <latex mode="display">
    /// \{ (i,i+k) : i=0,\dots,\min\{m-1,n-k-1\} \}.
    /// </latex>
    /// </para>
    /// <para id='sub'>
    /// If <latex mode="inline">m>1</latex>, the matrix has 
    /// <latex mode="inline">m-1</latex> <b> sub-diagonals</b>: for 
    /// <latex mode="inline">k=1,\dots,m-1</latex>, the 
    /// <latex mode="inline">k</latex>-th sub-diagonal is the collection of entries
    /// corresponding to the positions
    /// <latex mode="display">
    /// \{ (j-k,j) : j=0,\dots,\min\{n-1,m+k-1\} \}.
    /// </latex>
    /// </para>
    /// <para><b>Bandwidths</b></para>
    /// <para id='lower'>
    /// The <b>lower bandwidth</b> of <latex mode="inline">A</latex> is the smallest integer, 
    /// say <latex mode="inline">l</latex>, such that 
    /// <latex mode="inline">A_{i,j}=0</latex> if <latex mode="inline">i>j+l</latex>.
    /// </para>
    /// <para id='upper'>
    /// The <b>upper bandwidth</b> of a matrix <latex mode="inline">A</latex> is the smallest integer, 
    /// say <latex mode="inline">u</latex>, such that 
    /// <latex mode="inline">A_{i,j}=0</latex> if <latex mode="inline">j>i+u</latex>.
    /// </para>
    /// <para>
    /// These definitions imply that, if <latex mode="inline">k>l</latex>, then the sub-diagonal
    /// of order <latex mode="inline">k</latex> contains all zero entries, and
    /// if <latex mode="inline">k>u</latex>, then the super-diagonal
    /// of order <latex mode="inline">k</latex> contains all zero entries.
    /// </para>
    /// <para>
    /// Properties <see cref="LowerBandwidth"/> and <see cref="UpperBandwidth"/> can be 
    /// inspected to access the bandwidths of a given instance.
    /// </para>
    /// <para><b>Patterns</b></para>
    /// <para>                  
    /// The arrangement of zero entries in a matrix often follows a particular pattern.
    /// Some properties are defined which can be tested to verify if
    /// any of these patterns hold for a given matrix.
    /// </para>
    /// <para>
    /// Relevant patterns can be described by means of lower and upper matrix 
    /// bandwidths.
    /// For instance, an upper triangular matrix can be defined as a square matrix 
    /// having sub-diagonals whose entries are all zero, or, equivalently, as a
    /// square matrix having a lower bandwidth equal to 0.
    /// </para>
    /// <para>
    /// The following table reports some bandwidth-dependent patterns 
    /// for which a property exists
    /// which can be evaluated to inspect if the pattern holds true for a given square 
    /// matrix.
    /// </para>
    /// <para>
    ///   <list type="table">
    ///     <listheader>
    ///        <term>Matrix Pattern Property</term>
    ///        <term>Lower Bandwidth</term>
    ///        <term>Upper Bandwidth</term>
    ///     </listheader>
    ///     <item>
    ///        <term><see cref="IsLowerTriangular"/></term>
    ///        <term>Any</term>
    ///        <term>0</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsUpperTriangular"/></term>
    ///        <term>0</term>
    ///        <term>Any</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsLowerHessenberg"/></term>
    ///        <term>Any</term>
    ///        <term>0 or 1</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsUpperHessenberg"/></term>
    ///        <term>0 or 1</term>
    ///        <term>Any</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsDiagonal"/></term>
    ///        <term>0</term>
    ///        <term>0</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsLowerBidiagonal"/></term>
    ///        <term>0 or 1</term>
    ///        <term>0</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsUpperBidiagonal"/></term>
    ///        <term>0</term>
    ///        <term>0 or 1</term>
    ///     </item>
    ///     <item>
    ///        <term><see cref="IsTridiagonal"/></term>
    ///        <term>0 or 1</term>
    ///        <term>0 or 1</term>
    ///     </item>                                          
    ///   </list>
    /// </para>
    /// <para>
    /// Finally, property <see cref="IsTriangular"/>
    /// can be inspected to verify if a matrix instance is lower or upper triangular, 
    /// <see cref="IsBidiagonal"/> to verify if
    /// the instance is lower or upper bidiagonal,
    /// while property <see cref="IsHessenberg"/> 
    /// returns a value indicating if a matrix instance is lower or upper Hessenberg.
    /// </para>
    /// <para><b>Enumeration of entries and rows</b></para>    
    /// <para>
    /// Matrix values can be enumerated, and queried, using the "Linq to Objects" paradigm 
    /// (see, e.g., the 
    /// <see cref="System.Linq"/>
    /// documentation). Matrices can also be enumerated by rows using the iterator
    /// <see cref="AsRowCollection()"/>.
    /// This means that you can apply the LINQ approach to retrieve data from a matrix:
    /// you write declarative code that describes what you want to
    /// retrieve and at what conditions, which can be specified to 
    /// filter, order, and aggregate data as needed.
    /// </para>
    /// <para><b>Memory usage</b></para>
    /// <para id='MinimizeMemoryUsage.0'>
    /// Matrix entries can be stored using different data schemes, as enumerated
    /// in <see cref="Analytics.StorageScheme"/>.
    /// </para>
    /// <para id='MinimizeMemoryUsage.1'>
    /// The data currently stored can be accessed as an array
    /// through the
    /// <see cref="GetStorage()"/> method.
    /// </para>
    /// <para id='MinimizeMemoryUsage.1.1'>
    /// <note type="caution">
    /// This method is intended for advanced users and must always be used 
    /// carefully. 
    /// For performance reasons, the returned reference points directly to 
    /// the matrix internal data. Do not call <see cref="GetStorage()"/> if 
    /// you do not have complete control of the instance you used to invoke 
    /// the method.
    /// </note>
    /// </para>
    /// <para id='MinimizeMemoryUsage.2'>
    /// Method <see cref="AsColumnMajorDenseArray"/> always
    /// returns a <see cref="StorageOrder.ColumnMajor"/> ordered 
    /// array which is a dense representation of the matrix entries,
    /// irrespective of the specific data scheme used to implement the matrix.
    /// If the underlying scheme is
    /// <see cref="StorageScheme.Dense"/>, such method 
    /// returns a copy of the matrix data.
    /// </para>
    /// <para><b>Serialization</b></para>
    /// <para>
    /// Matrices can be loaded from, or saved to a CSV file through the
    /// <see cref="CsvComplexMatrixSerializer"/> class.
    /// </para>       
    /// <para>
    /// Matrices can also be represented as JSON strings, see <see cref="JsonSerialization"/>.
    /// </para>
    ///</remarks>
    ///<seealso cref="ComplexMatrixRow">ComplexMatrixRow Class</seealso>
    ///<seealso cref="IndexCollection">IndexCollection Class</seealso>
    ///<seealso cref="ReadOnlyComplexMatrix">ReadOnlyComplexMatrix Class</seealso>
    public class ComplexMatrix :
        IList<Complex>,
        IReadOnlyList<Complex>,
        IComplexMatrixPatterns,
        IReadOnlyTabularCollection<Complex, ComplexMatrix>,
        ITabularCollection<Complex, ComplexMatrix>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexMatrix"/> class
        /// as a scalar matrix having the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public ComplexMatrix(Complex value)
        {
            this.implementor = new DenseComplexMatrixImplementor(
                1, 1, [value], StorageOrder.ColumnMajor);
        }

        internal ComplexMatrix(MatrixImplementor<Complex> implementor)
        {
            this.implementor = implementor;
        }

        #endregion

        #region AsReadOnly

        /// <summary>
        /// Returns a read-only representation of the <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <returns>The read-only wrapper of the <see cref="ComplexMatrix"/>.</returns>
        public ReadOnlyComplexMatrix AsReadOnly()
        {
            return new ReadOnlyComplexMatrix(this);
        }

        #endregion

        #region Conversion operators

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="ReadOnlyComplexMatrix"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static explicit operator ComplexMatrix(ReadOnlyComplexMatrix value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return value.matrix.Clone();
        }

        /// <summary>
        /// Converts 
        /// from <see cref="ReadOnlyComplexMatrix"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix FromReadOnlyComplexMatrix(ReadOnlyComplexMatrix value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return value.matrix.Clone();
        }

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="Complex"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static explicit operator ComplexMatrix(Complex value)
        {
            return new ComplexMatrix(value);
        }

        /// <summary>
        /// Converts 
        /// from <see cref="Complex"/> to <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static ComplexMatrix FromComplex(Complex value)
        {
            return new ComplexMatrix(value);
        }

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="ComplexMatrix"/> to <see cref="Complex"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <remarks>
        /// <para>
        /// Only scalar matrices are successfully converted to a <see cref="Complex"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not scalar.
        /// </exception>
        public static explicit operator Complex(ComplexMatrix value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (!value.IsScalar)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR"),
                    nameof(value));
            }

            return value[0];
        }

        /// <summary>
        /// Converts 
        /// from <see cref="ComplexMatrix"/> to <see cref="Complex"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <remarks>
        /// <para>
        /// Only scalar matrices are successfully converted to a <see cref="Complex"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not scalar.
        /// </exception>
        public static Complex ToComplex(ComplexMatrix value)
        {
            return (Complex)value;
        }

        #endregion

        #region Implementor

        internal MatrixImplementor<Complex> implementor;

        #endregion

        #region Names

        /// <summary>
        /// Gets or sets the name of this instance.
        /// </summary>
        /// <value>The name of the matrix.</value>
        public string Name { get; set; }

        #region Rows

        /// <summary>
        /// Gets a value indicating whether this instance has at least a named row.
        /// </summary>
        /// <value><c>true</c> if this instance has row names; otherwise, <c>false</c>.</value>
        public bool HasRowNames
        {
            get
            {
                if (this.rowNames is null)
                    return false;
                else
                {
                    return this.rowNames.Count > 0;
                }
            }
        }

        /// <summary>
        /// Tries to get the name of the specified row.
        /// </summary>
        /// <param name="rowIndex">Index of the row whose name has to be retrieved.</param>
        /// <param name="rowName">The name of the specified row.</param>
        /// <returns>
        /// <c>true</c> if a name is successfully found for the specified row; 
        /// otherwise, <c>false</c>. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is equal to or greater than 
        /// <see cref="NumberOfRows"/>.
        /// </exception>
        public bool TryGetRowName(int rowIndex, out string rowName)
        {
            if (rowIndex < 0 || this.NumberOfRows <= rowIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (!this.HasRowNames)
            {
                rowName = null;
                return false;
            }

            return this.rowNames.TryGetValue(rowIndex, out rowName);
        }

        internal Dictionary<int, string> rowNames;

        /// <summary>
        /// Represents the name of parameter rowName.
        /// </summary>
        private const string paramRowName = "rowName";

        /// <summary>
        /// Exposes the dictionary of row names, keyed by row indexes.
        /// </summary>
        /// <value>The read only collection of row index/name pairs.</value>
        /// <remarks>
        /// If no row has a name, the collection is empty.
        /// </remarks>
        public ReadOnlyDictionary<int, string> RowNames
        {
            get
            {
                if (null == this.rowNames)
                {
                    this.rowNames = [];
                }

                return new ReadOnlyDictionary<int, string>(this.rowNames);
            }
        }

        /// <summary>
        /// Sets the name of the specified row.
        /// </summary>
        /// <param name="rowIndex">the index of the row whose name is to be set.</param>
        /// <param name="rowName">The name of the row.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowName"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowName"/> is empty, or consists only of 
        /// white-space characters, or is a string reserved for matrix sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is equal to or greater than 
        /// <see cref="NumberOfRows"/>.
        /// </exception>
        public void SetRowName(int rowIndex, string rowName)
        {
            ImplementationServices.ThrowOnNullOrWhiteSpace(rowName, ComplexMatrix.paramRowName);

            if (0 == string.Compare(rowName, ":", StringComparison.Ordinal))
            {
                throw new ArgumentOutOfRangeException(nameof(rowName),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING"));
            }

            if (rowIndex < 0 || this.NumberOfRows <= rowIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (null == this.rowNames)
                this.rowNames = [];

            this.rowNames[rowIndex] = rowName;
        }

        /// <summary>
        /// Removes the name of the specified row.
        /// </summary>
        /// <param name="rowIndex">
        /// The index of the row whose name has to be removed.
        /// </param>
        /// <returns>
        /// <c>true</c> if the name is successfully found and removed; otherwise,
        /// <c>false</c>. This method returns <c>false</c> if there is no name in the 
        /// current instance for the row whose index is equal to <paramref name="rowIndex"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="rowIndex"/> is equal to or greater than 
        /// <see cref="NumberOfRows"/>.
        /// </exception>
        public bool RemoveRowName(int rowIndex)
        {
            if (rowIndex < 0 || this.NumberOfRows <= rowIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (!this.HasRowNames)
                return false;

            return this.rowNames.Remove(rowIndex);
        }

        /// <summary>
        /// Removes all row names.
        /// </summary>
        public void RemoveAllRowNames()
        {
            this.rowNames = null;
        }

        private Dictionary<int, string> CloneRowIndexesByName()
        {
            Dictionary<int, string> clonedRowNames = null;

            if (this.rowNames is not null)
            {
                clonedRowNames = [];
                foreach (var i in this.rowNames.Keys)
                {
                    clonedRowNames[i] = this.rowNames[i];
                }
            }

            return clonedRowNames;
        }

        #endregion

        #region Columns

        /// <summary>
        /// Gets a value indicating whether this instance has at least a named column.
        /// </summary>
        /// <value><c>true</c> if this instance has column names; otherwise, <c>false</c>.</value>
        public bool HasColumnNames
        {
            get
            {
                if (this.columnNames is null)
                    return false;
                else
                {
                    return this.columnNames.Count > 0;
                }
            }
        }

        /// <summary>
        /// Tries to get the name of the specified column.
        /// </summary>
        /// <param name="columnIndex">Index of the column whose name has to be retrieved.</param>
        /// <param name="columnName">The name of the specified column.</param>
        /// <returns>
        /// <c>true</c> if a name is successfully found for the specified column; 
        /// otherwise, <c>false</c>. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="columnIndex"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is equal to or greater than 
        /// <see cref="NumberOfColumns"/>.
        /// </exception>
        public bool TryGetColumnName(int columnIndex, out string columnName)
        {
            if (columnIndex < 0 || this.NumberOfColumns <= columnIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (!this.HasColumnNames)
            {
                columnName = null;
                return false;
            }

            return this.columnNames.TryGetValue(columnIndex, out columnName);
        }

        internal Dictionary<int, string> columnNames;

        /// <summary>
        /// Represents the name of parameter columnName.
        /// </summary>
        private const string paramColumnName = "columnName";

        /// <summary>
        /// Exposes the dictionary of column names, keyed by column indexes.
        /// </summary>
        /// <value>The read only collection of column index/name pairs.</value>
        /// <remarks>
        /// If no column has a name, the collection is empty.
        /// </remarks>
        public ReadOnlyDictionary<int, string> ColumnNames
        {
            get
            {
                if (null == this.columnNames)
                {
                    this.columnNames = [];
                }
                return new ReadOnlyDictionary<int, string>(this.columnNames);
            }
        }

        /// <summary>
        /// Sets the name of the specified column.
        /// </summary>
        /// <param name="columnIndex">the index of the column whose name is to be set.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="columnName"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="columnName"/> is empty, or consists only of white-space characters, 
        /// or is a string reserved for matrix sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is less than zero. <br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is equal to or greater than 
        /// <see cref="NumberOfColumns"/>.
        /// </exception>
        public void SetColumnName(int columnIndex, string columnName)
        {
            ImplementationServices.ThrowOnNullOrWhiteSpace(columnName, ComplexMatrix.paramColumnName);

            if (0 == string.Compare(columnName, ":", StringComparison.Ordinal))
            {
                throw new ArgumentOutOfRangeException(nameof(columnName),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING"));
            }

            if (columnIndex < 0 || this.NumberOfColumns <= columnIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (null == this.columnNames)
                this.columnNames = [];

            this.columnNames[columnIndex] = columnName;
        }

        /// <summary>
        /// Removes the name of the specified column.
        /// </summary>
        /// <param name="columnIndex">
        /// The index of the column whose name has to be removed.
        /// </param>
        /// <returns>
        /// <c>true</c> if the name is successfully found and removed; otherwise,
        /// <c>false</c>. This method returns <c>false</c> if there is no name in the 
        /// current instance for the column whose index is equal to <paramref name="columnIndex"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="columnIndex"/> is less than zero. <br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> is equal to or greater than 
        /// <see cref="NumberOfColumns"/>.
        /// </exception>
        public bool RemoveColumnName(int columnIndex)
        {
            if (columnIndex < 0 || this.NumberOfColumns <= columnIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex),
                    ImplementationServices.GetResourceString("STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (!this.HasColumnNames)
                return false;

            return this.columnNames.Remove(columnIndex);
        }

        /// <summary>
        /// Removes all column names.
        /// </summary>
        public void RemoveAllColumnNames()
        {
            this.columnNames = null;
        }

        private Dictionary<int, string> CloneColumnIndexesByName()
        {
            Dictionary<int, string> clonedColumnNames = null;

            if (this.columnNames is not null)
            {
                clonedColumnNames = [];
                foreach (var i in this.columnNames.Keys)
                {
                    clonedColumnNames[i] = this.columnNames[i];
                }
            }

            return clonedColumnNames;
        }

        #endregion

        #endregion

        #region IDisposable

        /// <summary>
        /// Finalizes an instance of the <see cref="ComplexMatrix"/> class.
        /// </summary>
        ~ComplexMatrix()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged 
        /// resources; 
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        #endregion

        #region AsRowCollection

        /// <summary>
        /// Returns the collection of rows in the <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <returns>The collection of rows in this instance.</returns>
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
        /// <seealso cref="ComplexMatrixRowCollection"/>
        public ComplexMatrixRowCollection AsRowCollection()
        {
            var rows = new ObservableCollection<ComplexMatrixRow>();

            for (int i = 0; i < this.NumberOfRows; i++)
                rows.Add(new ComplexMatrixRow(i));

            return new ComplexMatrixRowCollection(rows, this);
        }

        /// <summary>
        /// Returns the collection of the rows in the <see cref="ComplexMatrix" />
        /// corresponding to the specified indexes.
        /// </summary>
        /// <param name="rowIndexes">The indexes of the rows to collect.</param>
        /// <returns>The collection of the specified rows in this instance.</returns>
        /// <example>
        ///   <para>
        /// In the following example, some rows of a matrix
        /// are enumerated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexRowsEnumeratorExample1.cs.txt" language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="ComplexMatrixRowCollection" />
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the <see cref="NumberOfRows"/> of this instance.
        /// </exception>
        public ComplexMatrixRowCollection AsRowCollection(IndexCollection rowIndexes)
        {
            ArgumentNullException.ThrowIfNull(rowIndexes);
            if (rowIndexes.maxIndex >= this.NumberOfRows)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            var rows = new ObservableCollection<ComplexMatrixRow>();

            for (int i = 0; i < rowIndexes.Count; i++)
                rows.Add(new ComplexMatrixRow(rowIndexes[i]));

            return new ComplexMatrixRowCollection(rows, this);
        }

        #endregion

        #region ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="ComplexMatrix"/> that is a copy of this instance.</returns>
        /// <remarks>
        /// <para>
        /// This method executes a deep copy of the matrix: the returned object
        /// and the cloned one will have independent states.
        /// </para>
        /// </remarks>
        internal ComplexMatrix Clone()
        {
            ComplexMatrix clone = new((MatrixImplementor<Complex>)this.implementor.Clone())
            {
                columnNames = this.CloneColumnIndexesByName(),
                rowNames = this.CloneRowIndexesByName(),
                Name = this.Name
            };

            return clone;
        }

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates a <see cref="ComplexMatrix"/> instance 
        /// representing the identity matrix having the specified dimension.
        /// </summary>
        /// <param name="dimension">The dimension of the identity matrix.</param>
        /// <returns>The identity matrix having the specified dimension.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="dimension"/> is not positive.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Identity_matrix"/>
        public static ComplexMatrix Identity(
            int dimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            var identityImplementor = new SparseCsr3ComplexMatrixImplementor(
                dimension, dimension, dimension);

            for (int i = 0; i < dimension; i++)
            {
                identityImplementor.SetValue(i, i, 1.0);
            }

            return new ComplexMatrix(identityImplementor);
        }

        /// <summary>
        /// Creates a square <see cref="ComplexMatrix"/> instance having the specified 
        /// data on its main diagonal and zero otherwise.
        /// </summary>
        /// <param name="mainDiagonal">The data to be inserted in the main diagonal
        /// of the matrix.</param>
        /// <returns>The diagonal matrix having the specified main diagonal data.</returns>
        /// <remarks>
        /// <para>
        /// Parameter <paramref name="mainDiagonal"/> is a matrix storing the 
        /// main diagonal data.
        /// Both <see cref="NumberOfRows"/> and
        /// <see cref="NumberOfColumns"/> of the created diagonal matrix will be
        /// equal to the <see cref="Count"/> of <paramref name="mainDiagonal"/>.
        /// Note that <paramref name="mainDiagonal"/> can have any size: if it is not
        /// a vector, its entries will be inserted in the main
        /// diagonal of the created diagonal matrix 
        /// assuming <see cref="StorageOrder.ColumnMajor"/> ordering.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para id='Diagonal.2'>
        /// In the following example, a diagonal matrix is created.
        ///</para>
        /// <para id='Diagonal.3'>
        /// <code title="Creation of a diagonal matrix"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDiagonalExample0.cs.txt" 
        /// language="cs" />
        /// </para>  
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mainDiagonal"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Diagonal_matrix"/>
        public static ComplexMatrix Diagonal(
            ComplexMatrix mainDiagonal)
        {
            ArgumentNullException.ThrowIfNull(mainDiagonal);

            int dimension = mainDiagonal.Count;
            var diagonalImplementor = new SparseCsr3ComplexMatrixImplementor(dimension, dimension, dimension);

            ComplexMatrix diagonal = new(diagonalImplementor);

            for (int i = 0; i < dimension; i++)
            {
                diagonal[i, i] = mainDiagonal[i];
            }

            return diagonal;
        }

        /// <inheritdoc cref = "Diagonal(ComplexMatrix)"/>
        public static ComplexMatrix Diagonal(
            ReadOnlyComplexMatrix mainDiagonal)
        {
            ArgumentNullException.ThrowIfNull(mainDiagonal);

            return Diagonal(mainDiagonal.matrix);
        }

        /// <summary>
        /// Creates a sparse <see cref="ComplexMatrix"/> instance having the specified
        /// size and initial capacity to store entries different from zero.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="capacity">The initial capacity to store non-zero entries.</param>
        /// <returns>The matrix having the specified size and initial capacity.</returns>
        /// <remarks>
        /// <para id='Sparse.0'>
        /// <see cref="ComplexMatrix"/> sparse instances allocate storage only for a number of
        /// matrix entries equal to <paramref name="capacity"/>. If an entry is not 
        /// explicitly set, it is interpreted as zero. If needed, the capacity
        /// is automatically updated to store more non-zero entries.
        /// Dense <see cref="ComplexMatrix"/> instances, i.e. instances allocating storage for
        /// each of their entries, can be created by calling 
        /// method
        /// <see cref="ComplexMatrix.Dense(int, int)"/> or
        /// one of its overloaded versions.
        /// </para>
        /// <para id='Sparse.1'>
        /// <note type="note">In the current version of the Novacta.Analytics
        /// assembly, this method stores non-zero entries using the compressed sparse row scheme.</note>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para id='Sparse.2'>
        /// In the following example, a sparse matrix is created.
        ///</para>
        /// <para id='Sparse.3'>
        /// <code title="Creation of a sparse matrix"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexSparseExample0.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="capacity"/> is negative.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Sparse_matrix#Compressed_sparse_row_(CSR,_CRS_or_Yale_format)"/>
        public static ComplexMatrix Sparse(
            int numberOfRows,
            int numberOfColumns,
            int capacity)
        {
            #region Input validation

            if (numberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRows),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfColumns),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            #endregion

            return new ComplexMatrix(new SparseCsr3ComplexMatrixImplementor(
                numberOfRows, numberOfColumns, capacity));
        }


        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the same
        /// size and data of the specified two-dimensional array.
        /// </summary>
        /// <param name="data">The two-dimensional array containing matrix data.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some dense matrices are created from  
        /// two-dimensional arrays.
        /// </para>
        /// <para>
        /// <code title="Creation of dense matrices from two-dimensional arrays"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample6.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="data"/> has at least a dimension along which the
        /// number of elements is zero.
        /// </exception>
        public static ComplexMatrix Dense(
            Complex[,] data)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(data);

            int numberOfRows = data.GetLength(0);
            int numberOfColumns = data.GetLength(1);

            if (numberOfRows == 0 || numberOfColumns == 0)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS"), nameof(data));
            }

            #endregion

            return new ComplexMatrix(new DenseComplexMatrixImplementor(data));
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns zero to each matrix entry.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <returns>The matrix having the specified size.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a dense matrix is created having all its entries equal to zero.
        ///</para>
        /// <para>
        /// <code title="Creation of a dense matrix with all entries equal to zero"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample5.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive.
        /// </exception>
        public static ComplexMatrix Dense(int numberOfRows, int numberOfColumns)
        {
            #region Input validation

            if (numberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRows),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfColumns),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            return new ComplexMatrix(new DenseComplexMatrixImplementor(
                numberOfRows, numberOfColumns));
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns the same value to each matrix entry.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The value assigned to each matrix entry.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// </remarks>
        /// <example>
        /// <para id='Dense.Complex.2'>
        /// In the following example, a dense matrix is created having all its entries equal to a given value.
        ///</para>
        /// <para id='Dense.Complex.3'>
        /// <code title="Creation of a dense matrix with all entries equal to a given value"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample4.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <seealso cref="Analytics.StorageOrder"/>
        public static ComplexMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            Complex data)
        {
            #region Input validation

            if (numberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRows),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfColumns),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            var matrix = new ComplexMatrix(new DenseComplexMatrixImplementor(
                numberOfRows, numberOfColumns));
            var matrixStorage = matrix.implementor.Storage;
            for (int i = 0; i < matrixStorage.Length; i++)
            {
                matrixStorage[i] = data;
            }

            return matrix;
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns data to entries 
        /// assuming ColMajor ordering.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// <para id='Dense.DefaultStorageOrder.1'>
        /// Parameter <paramref name="data"/> is unidimensional, while matrix entries
        /// are bi-dimensional, since are defined by their row and column indexes. As a
        /// consequence, entries must be linearly ordered to define a correspondence between data and
        /// matrix entries. This method assumes that the order is by columns: matrix entries 
        /// are ordered by their column index 
        /// first, and entries laying on a given column are in turn ordered by their row index.
        /// This implies that the first <paramref name="numberOfRows"/> entries in <paramref name="data"/>
        /// will be assumed to contain the first column of the returned matrix, and so on.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para id='Dense.DefaultStorageOrder.2'>
        /// In the following example, a dense matrix is created by passing data
        /// assumed to be ColMajor ordered.
        ///</para>
        /// <para id='Dense.DefaultStorageOrder.3'>
        /// <code title="Creation of a dense matrix with ColMajor ordered data"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample2.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="data"/> has <see cref="Array.Length"/> not equal to the multiplication of 
        /// <paramref name="numberOfRows"/> by <paramref name="numberOfColumns"/>.
        /// </exception>
        /// <seealso cref="Analytics.StorageOrder"/>
        public static ComplexMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            Complex[] data)
        {
            return Dense(
                numberOfRows,
                numberOfColumns,
                data,
                StorageOrder.ColumnMajor);
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns data to entries 
        /// applying the given storage order.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <param name="storageOrder">The linear ordering of matrix entries assumed
        /// when assigning data.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <para id='Dense.StorageOrder.0'>
        /// <see cref="ComplexMatrix"/> dense instances allocate storage for each matrix entry.
        /// Sparse <see cref="ComplexMatrix"/> instances can be created by calling method
        /// <see cref="Sparse(int, int, int)"/>.
        /// </para>
        /// <para id='Dense.StorageOrder.1'>
        /// Parameter <paramref name="data"/> is unidimensional, while matrix entries
        /// are bi-dimensional, since are defined by their row and column indexes. As a
        /// consequence, entries must be linearly ordered to define a correspondence between data and
        /// matrix entries. Supported linear orderings can be specified through parameter
        /// <paramref name="storageOrder"/>. If constant <see cref="StorageOrder.ColumnMajor"/> is
        /// passed, then the order is assumed by columns: matrix entries are ordered by their column index 
        /// first, and entries laying on a given column are in turn ordered by their row index.
        /// This implies that the first <paramref name="numberOfRows"/> entries in <paramref name="data"/>
        /// will be assumed to contain the first column of the returned matrix, and so on.
        /// If constant <see cref="StorageOrder.RowMajor"/> is
        /// passed, then the order is assumed by rows: matrix entries are ordered by their row index first,
        /// and entries laying on a given row are in turn ordered by their column index.
        /// This implies that the first <paramref name="numberOfColumns"/> entries in <paramref name="data"/>
        /// will be assumed to contain the first row of the returned matrix, and so on.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para id='Dense.StorageOrder.2'>
        /// In the following example, a dense matrix is created by passing
        /// both RowMajor and ColMajor ordered data.
        ///</para>
        /// <para id='Dense.StorageOrder.3'>
        /// <code title="Creation of a dense matrix with RowMajor or ColMajor ordered data"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample0.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentException">
        /// <paramref name="storageOrder"/> is not a field of 
        /// <see cref="Analytics.StorageOrder"/>.<br/>
        /// -or-<br/>
        /// <paramref name="data"/> has <see cref="Array.Length"/> not equal to the 
        /// multiplication of 
        /// <paramref name="numberOfRows"/> by <paramref name="numberOfColumns"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="Analytics.StorageOrder"/>
        public static ComplexMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            Complex[] data,
            StorageOrder storageOrder)
        {
            #region Input validation

            if (numberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRows),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfColumns),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            ArgumentNullException.ThrowIfNull(data);

            if ((StorageOrder.ColumnMajor != storageOrder)
                && (StorageOrder.RowMajor != storageOrder))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_NOT_FIELD_OF_STORAGE_ORDER"),
                    nameof(storageOrder));
            }

            int matrixCount = numberOfRows * numberOfColumns;
            if (data.Length != matrixCount)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS"),
                    nameof(data));
            }

            #endregion

            return new ComplexMatrix(new DenseComplexMatrixImplementor(
                numberOfRows, numberOfColumns, data, storageOrder));
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns data to entries, 
        /// possibly preventing copying operations before creation.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <param name="copyData">
        /// <c>true</c> if <paramref name="data"/> 
        /// must be copied before instantiation; otherwise <c>false</c>.
        /// </param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <para>
        /// <see cref="ComplexMatrix"/> dense instances allocate storage for each matrix 
        /// entry.
        /// </para>
        /// <para id='Dense.StorageOrder.1'>
        /// Parameter <paramref name="data"/> is unidimensional, while matrix entries
        /// are bi-dimensional, since are defined by their row and column indexes. As a
        /// consequence, entries must be linearly ordered to define a correspondence 
        /// between data and matrix entries. The order of <paramref name="data"/> is 
        /// assumed by columns (see <see cref="StorageOrder.ColumnMajor"/>): 
        /// matrix entries are ordered by their column index 
        /// first, and entries laying on a given column are in turn ordered by their 
        /// row index. This implies that the first <paramref name="numberOfRows"/> entries 
        /// in <paramref name="data"/> will be assumed to contain the first column of the 
        /// returned matrix, and so on.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="data"/> has <see cref="Array.Length"/> not equal to 
        /// the multiplication of 
        /// <paramref name="numberOfRows"/> by <paramref name="numberOfColumns"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            Complex[] data,
            bool copyData)
        {
            #region Input validation

            if (numberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRows),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfColumns),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            ArgumentNullException.ThrowIfNull(data);

            int matrixCount = numberOfRows * numberOfColumns;
            if (data.Length != matrixCount)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS"),
                    nameof(data));
            }

            #endregion

            return new ComplexMatrix(new DenseComplexMatrixImplementor(
                numberOfRows, numberOfColumns, data, copyData));
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns data to entries 
        /// assuming ColMajor ordering.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// <inheritdoc cref="Dense(int, int, Complex[])" 
        /// path="para[@id='Dense.DefaultStorageOrder.1']"/>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="Dense(int, int, Complex[])" 
        /// path="para[@id='Dense.DefaultStorageOrder.2']"/>
        /// <para>
        /// <code title="Creation of a dense matrix with ColMajor ordered data"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample3.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of items in <paramref name="data"/> is not equal to the multiplication of 
        /// <paramref name="numberOfRows"/> by <paramref name="numberOfColumns"/>.
        /// </exception>
        /// <seealso cref="Analytics.StorageOrder"/>
        public static ComplexMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            IEnumerable<Complex> data)
        {
            return Dense(
                numberOfRows,
                numberOfColumns,
                data,
                StorageOrder.ColumnMajor);
        }

        /// <summary>
        /// Creates a dense <see cref="ComplexMatrix"/> instance having the 
        /// specified size, and assigns data to entries 
        /// applying the given storage order.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <param name="storageOrder">The linear ordering of matrix entries assumed
        /// when assigning data.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.1']"/>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="Dense(int, int, Complex[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.2']"/>
        /// <para id='Dense.3'>
        /// <code title="Creation of a dense matrix with RowMajor or ColMajor ordered data"
        /// source="..\Novacta.Analytics.CodeExamples\ComplexDenseExample1.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentException">
        /// <paramref name="storageOrder"/> is not a field of 
        /// <see cref="Analytics.StorageOrder"/>.<br/>
        /// -or-<br/>
        /// The number of items in <paramref name="data"/> is not equal to the multiplication of 
        /// <paramref name="numberOfRows"/> by <paramref name="numberOfColumns"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="Analytics.StorageOrder"/>
        public static ComplexMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            IEnumerable<Complex> data,
            StorageOrder storageOrder)
        {
            #region Input validation

            if (numberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRows),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfColumns),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            ArgumentNullException.ThrowIfNull(data);

            if ((StorageOrder.ColumnMajor != storageOrder)
                && (StorageOrder.RowMajor != storageOrder))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_NOT_FIELD_OF_STORAGE_ORDER"),
                    nameof(storageOrder));
            }

            int matrixCount = numberOfRows * numberOfColumns;
            if (data.Count() != matrixCount)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS"),
                    nameof(data));
            }

            #endregion

            return new ComplexMatrix(new DenseComplexMatrixImplementor(
                numberOfRows, numberOfColumns, data, storageOrder));
        }

        #endregion

        #region Object

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

            bool columnsHaveNames = this.HasColumnNames;
            bool rowsHaveNames = this.HasRowNames;

            int numberOfRows = this.NumberOfRows;
            int numberOfColumns = this.NumberOfColumns;

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
                if (rowsHaveNames)
                {
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture,
                        blankRowNamesFormatSpecifier,
                        " ");
                }
                //stringBuilder.Append("   ");
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (this.columnNames.TryGetValue(j, out string columnName))
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
                            columnName =
                                string.Format(
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

            for (int i = 0; i < numberOfRows; i++)
            {
                if (rowsHaveNames)
                {
                    if (this.rowNames.TryGetValue(i, out string rowName))
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
                            rowName =
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    "[{0}]",
                                    rowName).PadRight(rowNameSize);
                            stringBuilder.Append(rowName);
                        }
                    }
                    else
                        stringBuilder.AppendFormat(
                            CultureInfo.InvariantCulture,
                            blankRowNamesFormatSpecifier,
                            " ");
                }

                for (int j = 0; j < numberOfColumns; j++)
                {
                    var value = this.implementor[i, j];
                    var asString = string.Format(
                        numberFormatInfo,
                        numberFormatSpecifier,
                        value.Real,
                        value.Imaginary);
                    stringBuilder.Append(asString);
                }
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append(Environment.NewLine);
            return stringBuilder.ToString();
        }

        #endregion

        #region Operations

        private const int dense = (int)StorageScheme.Dense;
        private const int sparse = (int)StorageScheme.CompressedRow;
        private const int numberOfStorageSchemes = 2;

        #region Add

        #region Matrix

        #region Complex, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>> ComplexComplexSumOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Dense_Sum);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Dense_Sum);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Sparse_Sum);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Sparse_Sum);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>
            complexComplexSumOperators = ComplexComplexSumOperators();

        /// <summary>
        /// Determines the sum of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of adding <paramref name="left"/> to <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <para id='operands'>
        /// Let <latex mode="inline">m_L</latex> and  
        /// <latex mode="inline">n_L</latex> be the <paramref name="left"/>
        /// number of rows and columns, respectively, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{left}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.
        /// </latex>
        /// Analogously, Let <latex mode="inline">m_R</latex> and  
        /// <latex mode="inline">n_R</latex> be the <paramref name="right"/>
        /// number of rows and columns, respectively, and let its generic entry given by
        /// <latex mode="display">
        /// \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are added.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexAdditionExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and they have not the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix operator +(ComplexMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(complexComplexSumOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Addition(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(ComplexMatrix left, ComplexMatrix right)
        {
            return left + right;
        }

        #endregion

        #region Double, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>> DoubleComplexSumOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Dense_Sum);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Dense_Sum);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Sparse_Sum);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Sparse_Sum);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>
            doubleComplexSumOperators = DoubleComplexSumOperators();

        /// <summary>
        /// Determines the sum of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of adding <paramref name="left"/> to <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <para id='operands'>
        /// Let <latex mode="inline">m_L</latex> and  
        /// <latex mode="inline">n_L</latex> be the <paramref name="left"/>
        /// number of rows and columns, respectively, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{left}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.
        /// </latex>
        /// Analogously, Let <latex mode="inline">m_R</latex> and  
        /// <latex mode="inline">n_R</latex> be the <paramref name="right"/>
        /// number of rows and columns, respectively, and let its generic entry given by
        /// <latex mode="display">
        /// \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and they have not the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix operator +(DoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(doubleComplexSumOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(DoubleMatrix left, ComplexMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator +(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix + right;
        }

        /// <inheritdoc cref = "op_Addition(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Add(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            return left + right;
        }

        #endregion

        #region Complex, Double

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>> ComplexDoubleSumOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Dense_Sum);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Dense_Sum);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Sparse_Sum);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Sparse_Sum);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>
            complexDoubleSumOperators = ComplexDoubleSumOperators();

        /// <summary>
        /// Determines the sum of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of adding <paramref name="left"/> to <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <para id='operands'>
        /// Let <latex mode="inline">m_L</latex> and  
        /// <latex mode="inline">n_L</latex> be the <paramref name="left"/>
        /// number of rows and columns, respectively, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{left}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.
        /// </latex>
        /// Analogously, Let <latex mode="inline">m_R</latex> and  
        /// <latex mode="inline">n_R</latex> be the <paramref name="right"/>
        /// number of rows and columns, respectively, and let its generic entry given by
        /// <latex mode="display">
        /// \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and they have not the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix operator +(ComplexMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(complexDoubleSumOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Add(ComplexMatrix left, DoubleMatrix right)
        {
            return left + right;
        }

        /// <inheritdoc cref = "op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator +(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left + right.matrix;
        }

        /// <inheritdoc cref = "op_Addition(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Add(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left + right;
        }

        #endregion

        #endregion

        #region Scalar

        #region Complex

        private static MatrixScalarBinaryOperator<Complex, Complex, Complex>[] ScalarSumOperators()
        {
            var operators = new MatrixScalarBinaryOperator<Complex, Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Dense_Sum);
            operators[sparse] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Sparse_Sum);

            return operators;
        }
        private static readonly MatrixScalarBinaryOperator<Complex, Complex, Complex>[]
            scalarSumOperators = ScalarSumOperators();

        /// <summary>
        /// Determines the addition of a matrix to a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of adding <paramref name="left"/> to <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <para id='left.operand'>
        /// Let <latex mode="inline">m_L</latex> and  
        /// <latex mode="inline">n_L</latex> be the <paramref name="left"/>
        /// number of rows and columns, respectively, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{left}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] + \mathit{right},\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is added to a scalar.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexAdditionExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator +(ComplexMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return new ComplexMatrix(scalarSumOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Addition(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Add(ComplexMatrix left, Complex right)
        {
            return left + right;
        }

        /// <summary>
        /// Determines the addition of a scalar to a matrix.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of adding <paramref name="left"/> to <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <para id='right.operand'>
        /// Let <latex mode="inline">m_R</latex> and  
        /// <latex mode="inline">n_R</latex> be the <paramref name="right"/>
        /// number of rows and columns, respectively, and let its generic entry given by
        /// <latex mode="display">
        /// \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method returns a matrix having
        /// the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left} + \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a scalar is added to a matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexAdditionExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator +(Complex left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(scalarSumOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Addition(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Add(Complex left, ComplexMatrix right)
        {
            return left + right;
        }

        #endregion

        #endregion

        #endregion

        #region Apply

        #region InPlace

        private static MatrixInPlaceApplyOperator<Complex>[] InPlaceApplyOperators()
        {
            var operators = new MatrixInPlaceApplyOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixInPlaceApplyOperator<Complex>(
                ComplexMatrixOperators.Dense_InPlaceApply);
            operators[sparse] = new MatrixInPlaceApplyOperator<Complex>(
                ComplexMatrixOperators.Sparse_InPlaceApply);

            return operators;
        }
        private static readonly
            MatrixInPlaceApplyOperator<Complex>[]
                inPlaceApplyOperators = InPlaceApplyOperators();

        /// <summary>
        /// Evaluates the specified function at each entry of this instance,
        /// and substitutes each entry with its corresponding function value.
        /// </summary>
        /// <param name="func">The function to apply to each matrix entry.</param>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Let <latex>y=f\round{x}</latex> the function represented by
        /// <paramref name="func"/>.
        /// Then method <see cref="InPlaceApply(Func{Complex, Complex})"/> transforms 
        /// <latex>A</latex> by setting
        /// <latex>A_{i,j} \leftarrow f\round{A_{i,j}},\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the entries in a matrix are all squared.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexInPlaceApplyExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="Apply"/>
        public void InPlaceApply(Func<Complex, Complex> func)
        {
            ArgumentNullException.ThrowIfNull(func);
            inPlaceApplyOperators[(int)this.implementor.StorageScheme](this.implementor, func);
        }

        #endregion

        #region OutPlace

        private static MatrixOutPlaceApplyOperator<Complex>[] OutPlaceApplyOperators()
        {
            var operators = new MatrixOutPlaceApplyOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixOutPlaceApplyOperator<Complex>(
                ComplexMatrixOperators.Dense_OutPlaceApply);
            operators[sparse] = new MatrixOutPlaceApplyOperator<Complex>(
                ComplexMatrixOperators.Sparse_OutPlaceApply);

            return operators;
        }
        private static readonly MatrixOutPlaceApplyOperator<Complex>[]
            outPlaceApplyOperators = OutPlaceApplyOperators();

        /// <summary>
        /// Evaluates the specified function at each entry of this instance,
        /// and returns a matrix whose entries are given by 
        /// the corresponding function values.
        /// </summary>
        /// <param name="func">The function to apply to each matrix entry.</param>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Let <latex>y=f\round{x}</latex> the function represented by
        /// <paramref name="func"/>.
        /// Then method <see cref="Apply(Func{Complex, Complex})"/> returns a matrix,
        /// say <latex>R</latex>, having the same dimensions of 
        /// <latex>A</latex> by setting
        /// <latex>R_{i,j} \leftarrow f\round{A_{i,j}},\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a new matrix is built by squaring all the entries in a 
        /// given matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexOutPlaceApplyExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>A matrix whose entries are given by 
        /// the values the specified function takes on at the entries of this instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="InPlaceApply"/>
        public ComplexMatrix Apply(Func<Complex, Complex> func)
        {
            ArgumentNullException.ThrowIfNull(func);

            return new ComplexMatrix(outPlaceApplyOperators[
                (int)this.implementor.StorageScheme]
                    (this.implementor, func));
        }

        #endregion

        #endregion

        #region Element-wise multiply

        #region Complex, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>> MultiplyByElementOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Dense_ElementWiseMultiply);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Dense_ElementWiseMultiply);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Sparse_ElementWiseMultiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Sparse_ElementWiseMultiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>
            multiplyByElementOperators = MultiplyByElementOperators();

        /// <summary>
        /// Determines the element wise product of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of element wise multiplying <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>        
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] * \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are element wise multiplied.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexElementWiseMultiplyExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="left"/> and <paramref name="right"/> have
        /// not the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix ElementWiseMultiply(ComplexMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(multiplyByElementOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        #endregion

        #region Double, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>> DoubleComplexMultiplyByElementOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Dense_ElementWiseMultiply);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Dense_ElementWiseMultiply);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Sparse_ElementWiseMultiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Sparse_ElementWiseMultiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>
            doubleComplexMultiplyByElementOperators = DoubleComplexMultiplyByElementOperators();

        /// <summary>
        /// Determines the element wise product of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of element wise multiplying <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>        
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] * \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="left"/> and <paramref name="right"/> have
        /// not the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix ElementWiseMultiply(DoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(doubleComplexMultiplyByElementOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "ElementWiseMultiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return ElementWiseMultiply(left.matrix, right);
        }

        #endregion

        #region Complex, Double

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>> ComplexDoubleMultiplyByElementOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Dense_ElementWiseMultiply);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Dense_ElementWiseMultiply);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Sparse_ElementWiseMultiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Sparse_ElementWiseMultiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>
            complexDoubleMultiplyByElementOperators = ComplexDoubleMultiplyByElementOperators();

        /// <inheritdoc cref = "ElementWiseMultiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ComplexMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(complexDoubleMultiplyByElementOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "ElementWiseMultiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix ElementWiseMultiply(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return ElementWiseMultiply(left, right.matrix);
        }

        #endregion

        #endregion

        #region Find

        #region Value

        private static FindValueOperator<Complex>[] FindValueOperators()
        {
            var operators = new FindValueOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new FindValueOperator<Complex>(
                ComplexMatrixOperators.Dense_FindValue);

            operators[sparse] = new FindValueOperator<Complex>(
                ComplexMatrixOperators.Sparse_FindValue);

            return operators;
        }
        private static readonly FindValueOperator<Complex>[]
            findValueComplexOperators = FindValueOperators();

        /// <summary>
        /// Searches for entries in this instance that equal the specified value, 
        /// and returns their zero-based linear indexes.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The collection of zero-based linear indexes of the entries that 
        /// matches the <paramref name="value"/>, if found; 
        /// otherwise <b>null</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="SortIndexResults" 
        /// path="para[@id='linearPositions']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the negative entries of a data matrix 
        /// are found.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexFindExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public IndexCollection Find(Complex value)
        {
            var implementor = this.implementor;

            if (Complex.IsNaN(value))
            {
                // Local function
                static bool match(Complex c) { return Complex.IsNaN(c); }
                return findWhileComplexOperators[(int)implementor.StorageScheme](implementor, match);
            }

            return findValueComplexOperators[(int)implementor.StorageScheme](implementor, value);
        }

        #endregion

        #region Nonzero

        private static FindNonzeroOperator<Complex>[] FindNonzeroOperators()
        {
            var operators = new FindNonzeroOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new FindNonzeroOperator<Complex>(
                ComplexMatrixOperators.Dense_FindNonzero);

            operators[sparse] = new FindNonzeroOperator<Complex>(
                ComplexMatrixOperators.Sparse_FindNonzero);

            return operators;
        }
        private static readonly FindNonzeroOperator<Complex>[]
            findNonzeroComplexOperators = FindNonzeroOperators();

        /// <summary>
        /// Searches for nonzero entries in this instance, 
        /// and returns their zero-based linear indexes.
        /// </summary>
        /// <returns>The collection of zero-based linear indexes of the nonzero entries 
        /// of this instance, if found; 
        /// otherwise <b>null</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="SortIndexResults" 
        /// path="para[@id='linearPositions']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the nonzero entries of a data matrix 
        /// are found.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexFindNonzeroExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public IndexCollection FindNonzero()
        {
            var implementor = this.implementor;
            return findNonzeroComplexOperators[(int)implementor.StorageScheme](implementor);
        }

        #endregion

        #region While

        private static FindWhileOperator<Complex>[] FindWhileOperators()
        {
            var operators = new FindWhileOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new FindWhileOperator<Complex>(
                ComplexMatrixOperators.Dense_FindWhile);

            operators[sparse] = new FindWhileOperator<Complex>(
                ComplexMatrixOperators.Sparse_FindWhile);

            return operators;
        }
        private static readonly FindWhileOperator<Complex>[]
            findWhileComplexOperators = FindWhileOperators();

        /// <summary>
        /// Searches for entries in this instance that matches the conditions defined by the specified predicate, 
        /// and returns their zero-based linear indexes.
        /// </summary>
        /// <param name="match">The predicate that defines the conditions 
        /// of the entries to search for.</param>
        /// <returns>The collection of zero-based linear indexes of the entries that 
        /// matches the conditions defined by <paramref name="match"/>, if found; 
        /// otherwise <b>null</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="SortIndexResults" 
        /// path="para[@id='linearPositions']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the negative entries of a data matrix 
        /// are found.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexFindWhileExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="match"/> is <b>null</b>.
        /// </exception>
        public IndexCollection FindWhile(Predicate<Complex> match)
        {
            ArgumentNullException.ThrowIfNull(match);

            var implementor = this.implementor;
            return findWhileComplexOperators[(int)implementor.StorageScheme](implementor, match);
        }

        #endregion

        #endregion

        #region Transpose

        #region InPlace

        private static MatrixInPlaceOperator<Complex>[] InPlaceTransposeOperators()
        {
            var operators = new MatrixInPlaceOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixInPlaceOperator<Complex>(
                ComplexMatrixOperators.Dense_InPlaceTranspose);
            operators[sparse] = new MatrixInPlaceOperator<Complex>(
                ComplexMatrixOperators.Sparse_InPlaceTranspose);

            return operators;
        }
        private static readonly MatrixInPlaceOperator<Complex>[]
            inPlaceTransposeOperators = InPlaceTransposeOperators();

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// The transpose of <latex mode="inline">A</latex> is the matrix <latex mode="inline">T</latex>
        /// having <latex mode="inline">n</latex> rows and 
        /// <latex mode="inline">m</latex> columns, whose generic
        /// entry is:
        /// <latex mode="display">T[j,i] = \mathit{A}[i,j],\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1.</latex> 
        /// </para>
        /// <para>
        /// The method transforms this instance in its transpose.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is
        /// transposed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexInPlaceTransposeExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="Transpose"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Transpose"/>
        public void InPlaceTranspose()
        {
            inPlaceTransposeOperators[(int)this.implementor.StorageScheme](this.implementor);

            if (this.HasRowNames)
            {
                if (this.HasColumnNames)
                {
                    (this.columnNames, this.rowNames) = (this.rowNames, this.columnNames);
                }
                else
                {
                    this.columnNames = this.rowNames;
                    this.rowNames = null;
                }
            }
            else
            {
                if (this.HasColumnNames)
                {
                    this.rowNames = this.columnNames;
                    this.columnNames = null;
                }
            }
        }

        #endregion

        #region OutPlace

        private static MatrixUnaryOperator<Complex, Complex>[] OutPlaceTransposeOperators()
        {
            var operators = new MatrixUnaryOperator<Complex, Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Dense_OutPlaceTranspose);
            operators[sparse] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Sparse_OutPlaceTranspose);

            return operators;
        }
        private static readonly MatrixUnaryOperator<Complex, Complex>[]
            outPlaceTransposeOperators = OutPlaceTransposeOperators();

        /// <summary>
        /// Returns the transpose of this instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the 
        /// number of rows and columns, respectively, of this instance, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{this}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method returns the transpose of this instance, i.e. a matrix, say <latex mode="inline">T</latex>,
        /// having <latex mode="inline">n</latex> rows and 
        /// <latex mode="inline">m</latex> columns, whose generic
        /// entry is:
        /// <latex mode="display">T[j,i] = \mathit{this}[i,j],\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the transpose of a matrix is
        /// computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexOutPlaceTransposeExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The transpose of this instance.</returns>
        /// <seealso cref="InPlaceTranspose"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Transpose"/>
        public ComplexMatrix Transpose()
        {
            ComplexMatrix transposed = new(outPlaceTransposeOperators[
                (int)this.implementor.StorageScheme]
                (this.implementor));

            if (this.HasRowNames)
            {
                ImplementationServices.SetMatrixColumnNames(
                    transposed,
                    this.rowNames);
            }

            if (this.HasColumnNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    transposed,
                    this.columnNames);
            }

            return transposed;
        }

        #endregion

        #endregion

        #region ConjugateTranspose

        #region InPlace

        private static MatrixInPlaceOperator<Complex>[] InPlaceConjugateTransposeOperators()
        {
            var operators = new MatrixInPlaceOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixInPlaceOperator<Complex>(
                ComplexMatrixOperators.Dense_InPlaceConjugateTranspose);
            operators[sparse] = new MatrixInPlaceOperator<Complex>(
                ComplexMatrixOperators.Sparse_InPlaceConjugateTranspose);

            return operators;
        }
        private static readonly MatrixInPlaceOperator<Complex>[]
            inPlaceConjugateTransposeOperators = InPlaceConjugateTransposeOperators();

        /// <summary>
        /// Conjugate transposes this instance.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// The conjugate transpose of <latex mode="inline">A</latex> is the matrix <latex mode="inline">H</latex>
        /// having <latex mode="inline">n</latex> rows and 
        /// <latex mode="inline">m</latex> columns, whose generic
        /// entry is:
        /// <latex mode="display">H[j,i] = \left(\mathit{A}[i,j]\right)^{*},\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1,</latex> 
        /// where <latex>z^*</latex> is the conjugate of complex <latex>z</latex>.
        /// </para>
        /// <para>
        /// The method transforms this instance in its conjugate transpose.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is conjugate
        /// transposed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexInPlaceConjugateTransposeExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="ComplexMatrix.ConjugateTranspose"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Conjugate_transpose"/>
        public void InPlaceConjugateTranspose()
        {
            inPlaceConjugateTransposeOperators[(int)this.implementor.StorageScheme](this.implementor);

            if (this.HasRowNames)
            {
                if (this.HasColumnNames)
                {
                    (this.columnNames, this.rowNames) = (this.rowNames, this.columnNames);
                }
                else
                {
                    this.columnNames = this.rowNames;
                    this.rowNames = null;
                }
            }
            else
            {
                if (this.HasColumnNames)
                {
                    this.rowNames = this.columnNames;
                    this.columnNames = null;
                }
            }
        }

        #endregion

        #region OutPlace

        private static MatrixUnaryOperator<Complex, Complex>[] OutPlaceConjugateTransposeOperators()
        {
            var operators = new MatrixUnaryOperator<Complex, Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Dense_OutPlaceConjugateTranspose);
            operators[sparse] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Sparse_OutPlaceConjugateTranspose);

            return operators;
        }
        private static readonly MatrixUnaryOperator<Complex, Complex>[]
            outPlaceConjugateTransposeOperators = OutPlaceConjugateTransposeOperators();

        /// <summary>
        /// Returns the conjugate transpose of this instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the 
        /// number of rows and columns, respectively, of this instance, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{this}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method returns the conjugate transpose of this instance, i.e. a matrix, 
        /// say <latex mode="inline">H</latex>,
        /// having <latex mode="inline">n</latex> rows and 
        /// <latex mode="inline">m</latex> columns, whose generic
        /// entry is:
        /// <latex mode="display">H[j,i] = \left(\mathit{this}[i,j]\right)^{*},\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1,</latex> 
        /// where <latex>z^*\left</latex> is the conjugate of complex <latex>z</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the conjugate transpose of a matrix is
        /// computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexOutPlaceConjugateTransposeExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The conjugate transpose of this instance.</returns>
        /// <seealso cref="InPlaceConjugateTranspose"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Conjugate_transpose"/>
        public ComplexMatrix ConjugateTranspose()
        {
            ComplexMatrix transposed = new(outPlaceConjugateTransposeOperators[
                (int)this.implementor.StorageScheme]
                (this.implementor));

            if (this.HasRowNames)
            {
                ImplementationServices.SetMatrixColumnNames(
                    transposed,
                    this.rowNames);
            }

            if (this.HasColumnNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    transposed,
                    this.columnNames);
            }

            return transposed;
        }

        #endregion

        #endregion

        #region Conjugate

        #region InPlace

        private static MatrixInPlaceOperator<Complex>[] InPlaceConjugateOperators()
        {
            var operators = new MatrixInPlaceOperator<Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixInPlaceOperator<Complex>(
                ComplexMatrixOperators.Dense_InPlaceConjugate);
            operators[sparse] = new MatrixInPlaceOperator<Complex>(
                ComplexMatrixOperators.Sparse_InPlaceConjugate);

            return operators;
        }
        private static readonly MatrixInPlaceOperator<Complex>[]
            inPlaceConjugateOperators = InPlaceConjugateOperators();

        /// <summary>
        /// Transforms the entries of this instance in their complex conjugates.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// The conjugate of <latex mode="inline">A</latex> is the matrix <latex mode="inline">C</latex>
        /// having <latex mode="inline">m</latex> rows and 
        /// <latex mode="inline">n</latex> columns, whose generic
        /// entry is:
        /// <latex mode="display">C[i,j] = \left(\mathit{A}[i,j]\right)^{*},\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1,</latex> 
        /// where <latex>z^*</latex> is the conjugate of complex <latex>z</latex>.
        /// </para>
        /// <para>
        /// The method transforms this instance in its conjugate.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, entries of a matrix are transformed in their corresponding complex conjugates.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexInPlaceConjugateExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="ComplexMatrix.Conjugate"/>
        /// <seealso href="https://mathworld.wolfram.com/ConjugateMatrix.html"/>
        public void InPlaceConjugate()
        {
            inPlaceConjugateOperators[(int)this.implementor.StorageScheme](this.implementor);
        }

        #endregion

        #region OutPlace

        private static MatrixUnaryOperator<Complex, Complex>[] OutPlaceConjugateOperators()
        {
            var operators = new MatrixUnaryOperator<Complex, Complex>[numberOfStorageSchemes];

            operators[dense] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Dense_OutPlaceConjugate);
            operators[sparse] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Sparse_OutPlaceConjugate);

            return operators;
        }
        private static readonly MatrixUnaryOperator<Complex, Complex>[]
            outPlaceConjugateOperators = OutPlaceConjugateOperators();

        /// <summary>
        /// Returns the conjugate of this instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the 
        /// number of rows and columns, respectively, of this instance, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{this}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method returns the conjugate of this instance, i.e. a matrix, 
        /// say <latex mode="inline">C</latex>,
        /// having <latex mode="inline">m</latex> rows and 
        /// <latex mode="inline">n</latex> columns, whose generic
        /// entry is:
        /// <latex mode="display">C[i,j] = \left(\mathit{this}[i,j]\right)^{*},\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1,</latex> 
        /// where <latex>z^*\left</latex> is the conjugate of complex <latex>z</latex>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the conjugate of a matrix is
        /// computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexOutPlaceConjugateExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The conjugate of this instance.</returns>
        /// <seealso cref="InPlaceConjugate"/>
        /// <seealso href="https://mathworld.wolfram.com/ConjugateMatrix.html"/>
        public ComplexMatrix Conjugate()
        {
            ComplexMatrix conjugate = new(outPlaceConjugateOperators[
                (int)this.implementor.StorageScheme]
                (this.implementor));

            if (this.HasRowNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    conjugate,
                    this.rowNames);
            }

            if (this.HasColumnNames)
            {
                ImplementationServices.SetMatrixColumnNames(
                    conjugate,
                    this.columnNames);
            }

            return conjugate;
        }

        #endregion

        #endregion

        #region Divide

        #region Matrix

        #region Complex, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>> DivideOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);

            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Dense_Divide);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Dense_Divide);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Sparse_Divide);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Sparse_Divide);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>
            divideOperators = DivideOperators();

        /// <summary>
        /// Determines the division of a matrix by another.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] / \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] / \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method interprets the division of <paramref name="left"/> by <paramref name="right"/> as 
        /// the solution <latex mode="inline">X</latex> of the following system of simultaneous equations:
        /// <latex mode="display">\mathit{left} = X \mathit{right},</latex> 
        /// provided that both <paramref name="left"/> and <paramref name="right"/> have
        /// the same number of columns; otherwise, an exception
        /// is thrown. 
        /// <para></para>
        /// <para>
        /// If <paramref name="right"/> is square, then the solution is computed differently for specific
        /// patterns. More thoroughly, if <paramref name="right"/> is upper or lower triangular, then a back or forward
        /// substitution algorithm is executed, respectively, or, if <paramref name="right"/> is symmetric, a Cholesky
        /// decomposition is tentatively applied; in every other case, the solution is obtained by LU decomposition of 
        /// matrix <paramref name="right"/>.
        /// </para>
        /// <para>
        /// If <paramref name="right"/> is not square, then the solution is computed by
        /// QR or LQ factorization of <paramref name="right"/>, provided that it has full rank; otherwise,
        /// an exception is thrown.
        /// </para>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the division of a matrix by another is
        /// computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexDivisionExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and any of the following conditions holds true:
        /// <list type="bullet">
        /// <item>
        /// <paramref name="left"/> and <paramref name="right"/> have not the same number of columns;
        /// </item>
        /// <item>
        /// <paramref name="right"/> is not square and has not full rank.
        /// </item>
        /// </list>
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Triangular_matrix#Forward_and_back_substitution"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Cholesky_decomposition"/>
        /// <seealso href="https://en.wikipedia.org/wiki/LU_decomposition"/>
        /// <seealso href="https://en.wikipedia.org/wiki/QR_decomposition"/>
        public static ComplexMatrix operator /(ComplexMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(divideOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Division(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(ComplexMatrix left, ComplexMatrix right)
        {
            return left / right;
        }

        #endregion

        #region Double, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>> DoubleComplexDivideOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Dense_Divide);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Dense_Divide);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Sparse_Divide);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Sparse_Divide);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>
            doubleComplexDivideOperators = DoubleComplexDivideOperators();

        /// <summary>
        /// Determines the division of a matrix by another.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] / \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] / \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method interprets the division of <paramref name="left"/> by <paramref name="right"/> as 
        /// the solution <latex mode="inline">X</latex> of the following system of simultaneous equations:
        /// <latex mode="display">\mathit{left} = X \mathit{right},</latex> 
        /// provided that both <paramref name="left"/> and <paramref name="right"/> have
        /// the same number of columns; otherwise, an exception
        /// is thrown. 
        /// <para></para>
        /// <para>
        /// If <paramref name="right"/> is square, then the solution is computed differently for specific
        /// patterns. More thoroughly, if <paramref name="right"/> is upper or lower triangular, then a back or forward
        /// substitution algorithm is executed, respectively, or, if <paramref name="right"/> is symmetric, a Cholesky
        /// decomposition is tentatively applied; in every other case, the solution is obtained by LU decomposition of 
        /// matrix <paramref name="right"/>.
        /// </para>
        /// <para>
        /// If <paramref name="right"/> is not square, then the solution is computed by
        /// QR or LQ factorization of <paramref name="right"/>, provided that it has full rank; otherwise,
        /// an exception is thrown.
        /// </para>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and any of the following conditions holds true:
        /// <list type="bullet">
        /// <item>
        /// <paramref name="left"/> and <paramref name="right"/> have not the same number of columns;
        /// </item>
        /// <item>
        /// <paramref name="right"/> is not square and has not full rank.
        /// </item>
        /// </list>
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Triangular_matrix#Forward_and_back_substitution"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Cholesky_decomposition"/>
        /// <seealso href="https://en.wikipedia.org/wiki/LU_decomposition"/>
        /// <seealso href="https://en.wikipedia.org/wiki/QR_decomposition"/>
        public static ComplexMatrix operator /(DoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(doubleComplexDivideOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(DoubleMatrix left, ComplexMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator /(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix / right;
        }

        /// <inheritdoc cref = "op_Division(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Divide(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            return left / right;
        }

        #endregion

        #region Complex, Double

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>> ComplexDoubleDivideOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Dense_Divide);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Dense_Divide);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Sparse_Divide);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Sparse_Divide);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>
            complexDoubleDivideOperators = ComplexDoubleDivideOperators();

        /// <summary>
        /// Determines the division of a matrix by another.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] / \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] / \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method interprets the division of <paramref name="left"/> by <paramref name="right"/> as 
        /// the solution <latex mode="inline">X</latex> of the following system of simultaneous equations:
        /// <latex mode="display">\mathit{left} = X \mathit{right},</latex> 
        /// provided that both <paramref name="left"/> and <paramref name="right"/> have
        /// the same number of columns; otherwise, an exception
        /// is thrown. 
        /// <para></para>
        /// <para>
        /// If <paramref name="right"/> is square, then the solution is computed differently for specific
        /// patterns. More thoroughly, if <paramref name="right"/> is upper or lower triangular, then a back or forward
        /// substitution algorithm is executed, respectively, or, if <paramref name="right"/> is symmetric, a Cholesky
        /// decomposition is tentatively applied; in every other case, the solution is obtained by LU decomposition of 
        /// matrix <paramref name="right"/>.
        /// </para>
        /// <para>
        /// If <paramref name="right"/> is not square, then the solution is computed by
        /// QR or LQ factorization of <paramref name="right"/>, provided that it has full rank; otherwise,
        /// an exception is thrown.
        /// </para>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and any of the following conditions holds true:
        /// <list type="bullet">
        /// <item>
        /// <paramref name="left"/> and <paramref name="right"/> have not the same number of columns;
        /// </item>
        /// <item>
        /// <paramref name="right"/> is not square and has not full rank.
        /// </item>
        /// </list>
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Triangular_matrix#Forward_and_back_substitution"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Cholesky_decomposition"/>
        /// <seealso href="https://en.wikipedia.org/wiki/LU_decomposition"/>
        /// <seealso href="https://en.wikipedia.org/wiki/QR_decomposition"/>
        public static ComplexMatrix operator /(ComplexMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(complexDoubleDivideOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Divide(ComplexMatrix left, DoubleMatrix right)
        {
            return left / right;
        }

        /// <inheritdoc cref = "op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator /(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left / right.matrix;
        }

        /// <inheritdoc cref = "op_Division(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Divide(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left / right;
        }

        #endregion

        #endregion

        #region Right scalar

        private static MatrixScalarBinaryOperator<Complex, Complex, Complex>[] ScalarRightDivideOperators()
        {
            var operators = new MatrixScalarBinaryOperator<Complex, Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Dense_RightDivide);
            operators[sparse] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Sparse_RightDivide);

            return operators;
        }
        private static readonly MatrixScalarBinaryOperator<Complex, Complex, Complex>[]
            scalarRightDivideOperators = ScalarRightDivideOperators();

        /// <summary>
        /// Determines the division of a matrix by a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,Complex)" 
        /// path="para[@id='left.operand']"/>
        /// <para>
        /// The method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] / \mathit{right},\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is divided by a scalar.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexDivisionExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator /(ComplexMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return new ComplexMatrix(scalarRightDivideOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Division(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Divide(ComplexMatrix left, Complex right)
        {
            return left / right;
        }

        #endregion

        #region Left scalar

        private static MatrixScalarBinaryOperator<Complex, Complex, Complex>[] ScalarLeftDivideOperators()
        {
            var operators = new MatrixScalarBinaryOperator<Complex, Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Dense_LeftDivide);
            operators[sparse] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Sparse_LeftDivide);

            return operators;
        }
        private static readonly MatrixScalarBinaryOperator<Complex, Complex, Complex>[]
            scalarLeftDivideOperators = ScalarLeftDivideOperators();


        /// <summary>
        /// Determines the division of a scalar by a matrix.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(Complex,ComplexMatrix)" 
        /// path="para[@id='right.operand']"/>
        /// <para>
        /// The method returns a matrix having
        /// the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left} / \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a scalar is divided by a matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexDivisionExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator /(Complex left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(scalarLeftDivideOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Division(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Divide(Complex left, ComplexMatrix right)
        {
            return left / right;
        }

        #endregion

        #endregion

        #region Multiply

        #region Matrix

        #region Complex, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>> MultiplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);

            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Dense_Multiply);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Dense_Multiply);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Sparse_Multiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Sparse_Multiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>
            multiplyOperators = MultiplyOperators();

        /// <summary>
        /// Determines the product of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of multiplying <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] * \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] * \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\sum_{k=0}^{n_{L}-1} \mathit{left}[i,l] * \mathit{right}[l,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_R-1,</latex> 
        /// provided that the number of columns of <paramref name="left"/> agrees 
        /// with the number of rows of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are multiplied.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMultiplicationExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and the number of columns of <paramref name="left"/> is not equal
        /// to the number of rows of <paramref name="right"/>.
        /// </exception>
        public static ComplexMatrix operator *(ComplexMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(multiplyOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Multiply(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(ComplexMatrix left, ComplexMatrix right)
        {
            return left * right;
        }

        #endregion

        #region Double, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>> DoubleComplexMultiplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Dense_Multiply);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Dense_Multiply);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Sparse_Multiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Sparse_Multiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>
            doubleComplexMultiplyOperators = DoubleComplexMultiplyOperators();

        /// <summary>
        /// Determines the product of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of multiplying <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] * \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] * \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\sum_{k=0}^{n_{L}-1} \mathit{left}[i,l] * \mathit{right}[l,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_R-1,</latex> 
        /// provided that the number of columns of <paramref name="left"/> agrees 
        /// with the number of rows of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and the number of columns of <paramref name="left"/> is not equal
        /// to the number of rows of <paramref name="right"/>.
        /// </exception>
        public static ComplexMatrix operator *(DoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(doubleComplexMultiplyOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(DoubleMatrix left, ComplexMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator *(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix * right;
        }

        /// <inheritdoc cref = "op_Multiply(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            return left * right;
        }

        #endregion

        #region Complex, Double

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>> ComplexDoubleMultiplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Dense_Multiply);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Dense_Multiply);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Sparse_Multiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Sparse_Multiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>
            complexDoubleMultiplyOperators = ComplexDoubleMultiplyOperators();

        /// <summary>
        /// Determines the product of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of multiplying <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] * \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] * \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\sum_{k=0}^{n_{L}-1} \mathit{left}[i,l] * \mathit{right}[l,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_R-1,</latex> 
        /// provided that the number of columns of <paramref name="left"/> agrees 
        /// with the number of rows of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and the number of columns of <paramref name="left"/> is not equal
        /// to the number of rows of <paramref name="right"/>.
        /// </exception>
        public static ComplexMatrix operator *(ComplexMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(complexDoubleMultiplyOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Multiply(ComplexMatrix left, DoubleMatrix right)
        {
            return left * right;
        }

        /// <inheritdoc cref = "op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator *(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left * right.matrix;
        }

        /// <inheritdoc cref = "op_Multiply(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Multiply(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left * right;
        }

        #endregion

        #endregion

        #region scalar

        private static MatrixScalarBinaryOperator<Complex, Complex, Complex>[] ScalarMultiplyOperators()
        {
            var operators = new MatrixScalarBinaryOperator<Complex, Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Dense_Multiply);
            operators[sparse] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Sparse_Multiply);

            return operators;
        }
        private static readonly MatrixScalarBinaryOperator<Complex, Complex, Complex>[]
            scalarMultiplyOperators = ScalarMultiplyOperators();


        /// <summary>
        /// Determines the multiplication of a matrix by a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of multiplying <paramref name="right"/> by <paramref name="left"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,Complex)" 
        /// path="para[@id='left.operand']"/>
        /// <para>
        /// The method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] * \mathit{right},\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is multiplied by a scalar.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMultiplicationExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator *(ComplexMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return new ComplexMatrix(scalarMultiplyOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Multiply(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Multiply(ComplexMatrix left, Complex right)
        {
            return left * right;
        }

        /// <summary>
        /// Determines the multiplication of a scalar by a matrix.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of multiplying <paramref name="right"/> by
        /// <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(Complex,ComplexMatrix)" 
        /// path="para[@id='right.operand']"/>
        /// <para>
        /// The method returns a matrix having
        /// the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left} * \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a scalar is multiplied by a matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMultiplicationExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator *(Complex left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(scalarMultiplyOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Multiply(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Multiply(Complex left, ComplexMatrix right)
        {
            return left * right;
        }

        #endregion

        #endregion

        #region Subtract

        #region Matrix

        #region Complex, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>> SubtractOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Dense_Subtract);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Dense_Subtract);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Sparse_Subtract);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Sparse_Subtract);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, Complex>>
            subtractOperators = SubtractOperators();

        /// <summary>
        /// Determines the difference between two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the difference between two matrices is
        /// computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexSubtractionExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and they have not 
        /// the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix operator -(ComplexMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(subtractOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Subtraction(ComplexMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(ComplexMatrix left, ComplexMatrix right)
        {
            return left - right;
        }

        #endregion

        #region Double, Complex

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>> DoubleComplexSubtractOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Dense_Subtract);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Dense_Subtract);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Dense_Sparse_Subtract);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, double, Complex>(
                MixedDoubleComplexBinaryOperators.Matrix_Sparse_Sparse_Subtract);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, double, Complex>>
            doubleComplexSubtractOperators = DoubleComplexSubtractOperators();

        /// <summary>
        /// Determines the difference between two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and they have not 
        /// the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix operator -(DoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(doubleComplexSubtractOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(DoubleMatrix left, ComplexMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix operator -(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);

            return left.matrix - right;
        }

        /// <inheritdoc cref = "op_Subtraction(DoubleMatrix,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(ReadOnlyDoubleMatrix left, ComplexMatrix right)
        {
            return left - right;
        }

        #endregion

        #region Complex, Double

        private static DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>> ComplexDoubleSubtractOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>(
                numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Dense_Subtract);
            operators[sparse, dense] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Dense_Subtract);
            operators[dense, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Dense_Sparse_Subtract);
            operators[sparse, sparse] = new MatrixBinaryOperator<Complex, Complex, double>(
                MixedComplexDoubleBinaryOperators.Matrix_Sparse_Sparse_Subtract);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<Complex, Complex, double>>
            complexDoubleSubtractOperators = ComplexDoubleSubtractOperators();

        /// <summary>
        /// Determines the difference between two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,ComplexMatrix)" 
        /// path="para[@id='operands']"/>
        /// <para>
        /// The method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="left"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[0,0] - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </item>
        /// <item>
        /// If <paramref name="right"/> is scalar, then the method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right}[0,0],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </item>
        /// <item>
        /// If neither <paramref name="left"/> nor <paramref name="right"/> is
        /// scalar, then the method returns a matrix 
        /// whose generic entry is 
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1,</latex> 
        /// provided that the dimensions of <paramref name="left"/> agree 
        /// with those of <paramref name="right"/>; otherwise, an exception
        /// is thrown.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Both <paramref name="left"/> and <paramref name="right"/> are not scalar 
        /// matrices, and they have not 
        /// the same number of rows and columns.
        /// </exception>
        public static ComplexMatrix operator -(ComplexMatrix left, DoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(complexDoubleSubtractOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Subtract(ComplexMatrix left, DoubleMatrix right)
        {
            return left - right;
        }

        /// <inheritdoc cref = "op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix operator -(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return left - right.matrix;
        }

        /// <inheritdoc cref = "op_Subtraction(ComplexMatrix,DoubleMatrix)"/>
        public static ComplexMatrix Subtract(ComplexMatrix left, ReadOnlyDoubleMatrix right)
        {
            return left - right;
        }

        #endregion

        #endregion

        #region Right scalar

        private static MatrixScalarBinaryOperator<Complex, Complex, Complex>[] ScalarRightSubtractOperators()
        {
            var operators = new MatrixScalarBinaryOperator<Complex, Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Dense_RightSubtract);
            operators[sparse] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Sparse_RightSubtract);

            return operators;
        }
        private static readonly MatrixScalarBinaryOperator<Complex, Complex, Complex>[]
            scalarRightSubtractOperators = ScalarRightSubtractOperators();

        /// <summary>
        /// Determines the subtraction of a scalar from a matrix.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of subtracting <paramref name="right"/> from <paramref name="left"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(ComplexMatrix,Complex)" 
        /// path="para[@id='left.operand']"/>
        /// <para>
        /// The method returns a matrix
        /// having the same dimensions of <paramref name="left"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left}[i,j] - \mathit{right},\hspace{12pt} i=0,\dots,m_L-1,\hspace{12pt} j=0,\dots,n_L-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a scalar is subtracted from a matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexSubtractionExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator -(ComplexMatrix left, Complex right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return new ComplexMatrix(scalarRightSubtractOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Subtraction(ComplexMatrix,Complex)"/>
        public static ComplexMatrix Subtract(ComplexMatrix left, Complex right)
        {
            return left - right;
        }

        #endregion

        #region Left scalar

        private static MatrixScalarBinaryOperator<Complex, Complex, Complex>[] ScalarLeftSubtractOperators()
        {
            var operators = new MatrixScalarBinaryOperator<Complex, Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Dense_LeftSubtract);
            operators[sparse] = new MatrixScalarBinaryOperator<Complex, Complex, Complex>(
                ComplexMatrixOperators.Scalar_Sparse_LeftSubtract);

            return operators;
        }
        private static readonly MatrixScalarBinaryOperator<Complex, Complex, Complex>[]
            scalarLeftSubtractOperators = ScalarLeftSubtractOperators();

        /// <summary>
        /// Determines the subtraction of a matrix from a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of subtracting <paramref name="right"/> from
        /// <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(Complex,ComplexMatrix)" 
        /// path="para[@id='right.operand']"/>
        /// <para>
        /// The method returns a matrix having
        /// the same dimensions of <paramref name="right"/>, whose generic
        /// entry is:
        /// <latex mode="display">\mathit{left} - \mathit{right}[i,j],\hspace{12pt} i=0,\dots,m_R-1,\hspace{12pt} j=0,\dots,n_R-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is subtracted from a scalar.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexSubtractionExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator -(Complex left, ComplexMatrix right)
        {
            ArgumentNullException.ThrowIfNull(right);
            return new ComplexMatrix(scalarLeftSubtractOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Subtraction(Complex,ComplexMatrix)"/>
        public static ComplexMatrix Subtract(Complex left, ComplexMatrix right)
        {
            return left - right;
        }

        #endregion

        #endregion

        #region Negate

        private static MatrixUnaryOperator<Complex, Complex>[] NegationOperators()
        {
            var operators = new MatrixUnaryOperator<Complex, Complex>[numberOfStorageSchemes];
            operators[dense] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Matrix_Dense_Negation);
            operators[sparse] = new MatrixUnaryOperator<Complex, Complex>(
                ComplexMatrixOperators.Matrix_Sparse_Negation);
            return operators;
        }
        private static readonly MatrixUnaryOperator<Complex, Complex>[]
            negationOperators = NegationOperators();

        /// <summary>
        /// Determines the negation of a matrix.
        /// </summary>
        /// <param name="matrix">The operand.</param>
        /// <returns>The result of negating <paramref name="matrix"/>.
        /// </returns>
        /// <remarks>
        /// <para id='left.operand'>
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="matrix"/>
        /// number of rows and columns, respectively, and consider its generic entry
        /// <latex mode="display">
        /// \mathit{operand}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para>
        /// The method returns a matrix having
        /// the same dimensions of <paramref name="matrix"/>, whose generic
        /// entry is:
        /// <latex mode="display">- \mathit{operand}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrices are negated.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexNegationExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        public static ComplexMatrix operator -(ComplexMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);
            return new ComplexMatrix(negationOperators[(int)matrix.implementor.StorageScheme]
                (matrix.implementor));
        }

        /// <inheritdoc cref = "op_UnaryNegation(ComplexMatrix)"/>
        public static ComplexMatrix Negate(ComplexMatrix matrix)
        {
            return -matrix;
        }

        #endregion

        #endregion

        #region ICompleMatrixPatterns

        /// <inheritdoc/>
        public bool IsHermitian
        {
            get
            {
                return this.implementor.IsHermitian;
            }
        }

        /// <inheritdoc/>
        public bool IsSkewHermitian
        {
            get
            {
                return this.implementor.IsSkewHermitian;
            }
        }

        #region IMatrixPatterns

        /// <inheritdoc/>
        public bool IsVector
        {
            get
            {
                return (this.IsRowVector || this.IsColumnVector);
            }
        }

        /// <inheritdoc/>
        public bool IsRowVector
        {
            get
            {
                return 1 == this.implementor.NumberOfRows;
            }
        }

        /// <inheritdoc/>
        public bool IsColumnVector
        {
            get
            {
                return 1 == this.implementor.NumberOfColumns;
            }
        }

        /// <inheritdoc/>
        public bool IsScalar
        {
            get
            {
                return ((1 == this.implementor.NumberOfRows)
                        &&
                        (1 == this.implementor.NumberOfColumns));
            }
        }

        /// <inheritdoc/>
        public bool IsSquare
        {
            get
            {
                return (this.implementor.NumberOfRows
                        ==
                        this.implementor.NumberOfColumns);
            }
        }

        /// <inheritdoc/>
        public bool IsDiagonal
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.LowerBandwidth == 0
                        &&
                        this.implementor.UpperBandwidth == 0);
            }
        }

        /// <inheritdoc/>
        public bool IsHessenberg
        {
            get
            {
                return (this.IsSquare
                        &&
                        (this.implementor.LowerBandwidth <= 1
                         ||
                         this.implementor.UpperBandwidth <= 1));
            }
        }

        /// <inheritdoc/>
        public bool IsLowerHessenberg
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.UpperBandwidth <= 1);
            }
        }

        /// <inheritdoc/>
        public bool IsSymmetric
        {
            get
            {
                return this.implementor.IsSymmetric;
            }
        }

        /// <inheritdoc/>
        public bool IsSkewSymmetric
        {
            get
            {
                return this.implementor.IsSkewSymmetric;
            }
        }

        /// <inheritdoc/>
        public bool IsTriangular
        {
            get
            {
                return (this.IsSquare
                        &&
                        (this.implementor.UpperBandwidth == 0 // IsLowerTriangular
                         ||
                         this.implementor.LowerBandwidth == 0));  // IsUpperTriangular
            }
        }

        /// <inheritdoc/>
        public bool IsLowerTriangular
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.UpperBandwidth == 0);
            }
        }

        /// <inheritdoc/>
        public bool IsUpperTriangular
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.LowerBandwidth == 0);
            }
        }

        /// <inheritdoc/>
        public bool IsTridiagonal
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.LowerBandwidth <= 1
                        &&
                        this.implementor.UpperBandwidth <= 1);
            }
        }

        /// <inheritdoc/>
        public bool IsLowerBidiagonal
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.LowerBandwidth <= 1
                        &&
                        this.implementor.UpperBandwidth == 0);
            }
        }

        /// <inheritdoc/>
        public bool IsUpperBidiagonal
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.LowerBandwidth == 0
                        &&
                        this.implementor.UpperBandwidth <= 1);
            }
        }

        /// <inheritdoc/>
        public bool IsBidiagonal
        {
            get
            {
                return (this.IsLowerBidiagonal || this.IsUpperBidiagonal);
            }
        }

        /// <inheritdoc/>

        public bool IsUpperHessenberg
        {
            get
            {
                return (this.IsSquare
                        &&
                        this.implementor.LowerBandwidth <= 1);
            }
        }

        /// <inheritdoc/>
        public int LowerBandwidth
        {
            get
            {
                return this.implementor.LowerBandwidth;
            }
        }

        /// <inheritdoc/>
        public int UpperBandwidth
        {
            get
            {
                return this.implementor.UpperBandwidth;
            }
        }

        #endregion

        #endregion

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Complex> GetEnumerator()
        {
            return new ComplexMatrixEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ComplexMatrixEnumerator(this);
        }

        #endregion

        #region Storage

        /// <summary>
        /// Returns a column major ordered, dense representation of this instance.
        /// </summary>
        /// <returns>The column major ordered dense array representing this
        /// instance.</returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" path="para[@id='MinimizeMemoryUsage.2']"/>
        /// </remarks>
        public Complex[] AsColumnMajorDenseArray()
        {
            return this.implementor.AsColumnMajorDenseArray();
        }

        /// <summary>
        /// Gets the elements currently stored in this instance.
        /// </summary>
        /// <returns>The array of stored matrix elements.</returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" path="para[@id='MinimizeMemoryUsage.0']"/>
        /// <inheritdoc cref="ComplexMatrix" path="para[@id='MinimizeMemoryUsage.1']"/>
        /// <inheritdoc cref="ComplexMatrix" path="para[@id='MinimizeMemoryUsage.1.1']"/>
        /// </remarks>
        public Complex[] GetStorage()
        {
            return this.implementor.Storage;
        }

        /// <summary>
        /// Gets the storage order of this instance.
        /// </summary>
        /// <value>The storage order.</value>
        public StorageOrder StorageOrder
        {
            get
            {
                return this.implementor.StorageOrder;
            }
        }

        /// <summary>
        /// Gets the storage scheme of this instance.
        /// </summary>
        /// <value>The storage scheme.</value>
        public StorageScheme StorageScheme
        {
            get
            {
                return this.implementor.StorageScheme;
            }
        }

        #endregion

        #region ITabularCollection

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.NumberOfColumns"/>
        public int NumberOfColumns
        {
            get
            {
                return this.implementor.NumberOfColumns;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.NumberOfRows"/> 
        public int NumberOfRows
        {
            get
            {
                return this.implementor.NumberOfRows;
            }
        }

        #region this[int, *]

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,int]"/>
        /// <example>
        /// <para>
        /// In the following example, a matrix element is accessed using its row and
        /// column indexes.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample00.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public Complex this[int rowIndex, int columnIndex]
        {
            get
            {
                return this.implementor[rowIndex, columnIndex];
            }
            set
            {
                this.implementor[rowIndex, columnIndex] = value;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,IndexCollection]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample01.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[int rowIndex, IndexCollection columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(columnIndexes);

                var subMatrix = new ComplexMatrix(this.implementor[rowIndex, columnIndexes]);

                if (this.HasRowNames)
                    ImplementationServices.TrySetMatrixRowName(
                        subMatrix,
                        rowIndex,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.TrySetMatrixColumnNames(
                        subMatrix,
                        columnIndexes,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(columnIndexes);

                ArgumentNullException.ThrowIfNull(value);

                this.implementor[rowIndex, columnIndexes] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,string]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample02.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[int rowIndex, string columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(columnIndexes);

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subMatrix = new ComplexMatrix(this.implementor[rowIndex, columnIndexes]);

                if (this.HasRowNames)
                    ImplementationServices.TrySetMatrixRowName(
                        subMatrix,
                        rowIndex,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.SetMatrixColumnNames(
                        subMatrix,
                        this.columnNames);

                return subMatrix;
            }

            set
            {
                ArgumentNullException.ThrowIfNull(columnIndexes);

                ArgumentNullException.ThrowIfNull(value);

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                this.implementor[rowIndex, columnIndexes] = value.implementor;
            }
        }

        #endregion

        #region this[IndexCollection, *]

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[IndexCollection,int]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample10.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[IndexCollection rowIndexes, int columnIndex]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                var subMatrix = new ComplexMatrix(this.implementor[rowIndexes, columnIndex]);

                if (this.HasRowNames)
                    ImplementationServices.TrySetMatrixRowNames(
                        subMatrix,
                        rowIndexes,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.TrySetMatrixColumnName(
                        subMatrix,
                        columnIndex,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(value);

                this.implementor[rowIndexes, columnIndex] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[IndexCollection,IndexCollection]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample11.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                ComplexMatrix subMatrix = new(this.implementor[rowIndexes, columnIndexes]);

                if (this.HasRowNames)
                    ImplementationServices.TrySetMatrixRowNames(
                        subMatrix,
                        rowIndexes,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.TrySetMatrixColumnNames(
                        subMatrix,
                        columnIndexes,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                ArgumentNullException.ThrowIfNull(value);

                this.implementor[rowIndexes, columnIndexes] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[IndexCollection,string]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample12.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[IndexCollection rowIndexes, string columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                ComplexMatrix subMatrix = new(this.implementor[rowIndexes, columnIndexes]);

                if (this.HasRowNames)
                    ImplementationServices.TrySetMatrixRowNames(
                        subMatrix,
                        rowIndexes,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.SetMatrixColumnNames(
                        subMatrix,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                ArgumentNullException.ThrowIfNull(value);

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                this.implementor[rowIndexes, columnIndexes] = value.implementor;
            }
        }

        #endregion

        #region this[string, *]

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[string,int]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample20.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[string rowIndexes, int columnIndex]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                // Check if rowIndexes is a string reserved for sub-reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subMatrix = new ComplexMatrix(this.implementor[rowIndexes, columnIndex]);

                if (this.HasRowNames)
                    ImplementationServices.SetMatrixRowNames(
                        subMatrix,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.TrySetMatrixColumnName(
                        subMatrix,
                        columnIndex,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                // Check if rowIndexes is a string reserved for sub-reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                ArgumentNullException.ThrowIfNull(value);

                this.implementor[rowIndexes, columnIndex] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[string,IndexCollection]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample21.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[string rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                // Check if rowIndexes is a string reserved for sub reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                ComplexMatrix subMatrix = new(this.implementor[rowIndexes, columnIndexes]);

                if (this.HasRowNames)
                    ImplementationServices.SetMatrixRowNames(
                        subMatrix,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.TrySetMatrixColumnNames(
                        subMatrix,
                        columnIndexes,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                // Check if rowIndexes is a string reserved for sub-reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                ArgumentNullException.ThrowIfNull(value);

                this.implementor[rowIndexes, columnIndexes] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[string,string]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexMatrixIndexerExample22.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public ComplexMatrix this[string rowIndexes, string columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                ComplexMatrix subMatrix = new(this.implementor[rowIndexes, columnIndexes]);

                if (this.HasRowNames)
                    ImplementationServices.SetMatrixRowNames(
                        subMatrix,
                        this.rowNames);

                if (this.HasColumnNames)
                    ImplementationServices.SetMatrixColumnNames(
                        subMatrix,
                        this.columnNames);

                return subMatrix;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);


                ArgumentNullException.ThrowIfNull(value);

                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                this.implementor[rowIndexes, columnIndexes] = value.implementor;
            }
        }

        #endregion

        #endregion

        #region Vectorization

        /// <summary>
        /// Returns the vectorization of this instance.
        /// </summary>
        /// <returns>A column vector obtained by stacking the columns of this instance.</returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='linear.indexing']"/>
        /// <para>
        /// This method, when called on a <see cref="ComplexMatrix"/> instance
        /// representing matrix <latex>A</latex>, returns a new 
        /// <see cref="ComplexMatrix"/> instance that 
        /// represents a column vector, say <latex>v</latex>,
        /// such that:
        /// <latex mode="display">
        /// v_l = A_l,\hspace{12pt} l=0,\dots,L-1.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix is vectorized.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexVecExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso href="https://en.wikipedia.org/wiki/Vectorization_(mathematics)"/>
        public ComplexMatrix Vec()
        {
            return new ComplexMatrix(this.implementor[":"]);
        }

        /// <summary>
        /// Returns a column vector obtained by stacking the specified elements of this instance.
        /// </summary>
        /// <param name="linearIndexes">The linear indexes of the elements to stack.</param>
        /// <returns>
        /// A column vector obtained by stacking the specified elements 
        /// of this instance.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='linear.indexing']"/>
        /// <para>
        /// Let <latex>\round{l_0,\dots,l_{K-1}}</latex> the collection of 
        /// indexes represented by <paramref name="linearIndexes"/>.
        /// This method, when called on a <see cref="ComplexMatrix"/> instance
        /// representing matrix <latex>A</latex>, returns a new 
        /// <see cref="ComplexMatrix"/> instance that 
        /// represents a column vector, say <latex>v</latex>,
        /// such that:
        /// <latex mode="display">
        /// v_{l_k} = A_{l_k},\hspace{12pt} k=0,\dots,K-1.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some matrix entries are vectorized.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexVecExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="linearIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="linearIndexes"/> contains an index 
        /// which is greater than or equal to the <see cref="Count"/> of this instance.
        /// </exception>
        public ComplexMatrix Vec(IndexCollection linearIndexes)
        {
            ArgumentNullException.ThrowIfNull(linearIndexes);

            return new ComplexMatrix(this.implementor[linearIndexes]);
        }

        #endregion

        #region IList

        /// <summary>
        /// Gets the number of elements in this instance.
        /// </summary>
        /// <value>The number of elements in this instance.</value>
        public int Count
        {
            get
            {
                return this.implementor.Count;
            }
        }

        /// <summary>
        /// Gets or sets the element of this instance corresponding to the specified linear
        /// index.
        /// </summary>
        /// <param name="linearIndex">The zero-based linear index of the element to get or set.</param>
        /// <value>The element corresponding to the specified linear index.</value>
        /// <remarks>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="ComplexMatrix" 
        /// path="para[@id='linear.indexing']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix element is accessed using its linear
        /// index.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\ComplexLinearIndexerExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="linearIndex"/> is less than zero.<br/>
        /// -or-<br/>
        /// <paramref name="linearIndex"/> is equal to or greater than 
        /// the <see cref="Count"/> of this instance.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order"/>
        public Complex this[int linearIndex]
        {
            get
            {
                return this.implementor[linearIndex];
            }
            set
            {
                this.implementor[linearIndex] = value;
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get => false; }

        private static Func<MatrixImplementor<Complex>, Complex, int>[] IndexOfOperators()
        {
            var operators = new Func<MatrixImplementor<Complex>, Complex, int>[numberOfStorageSchemes];

            operators[dense] = ComplexMatrixOperators.Dense_IndexOf;
            operators[sparse] = ComplexMatrixOperators.Sparse_IndexOf;

            return operators;
        }
        private static readonly Func<MatrixImplementor<Complex>, Complex, int>[]
            indexOfOperators = IndexOfOperators();

        /// <inheritdoc/>
        public int IndexOf(Complex item)
        {
            return indexOfOperators[(int)this.implementor.StorageScheme](this.implementor, item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"></see> at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into 
        /// the <see cref="IList{T}"></see>.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<Complex>.Insert(int index, Complex item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes the <see cref="IList{T}"></see> item 
        /// at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<Complex>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region ICollection

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item">The object to add to 
        /// the <see cref="ICollection{T}"></see>.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<Complex>.Add(Complex item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<Complex>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(Complex item)
        {
            return -1 != this.IndexOf(item);
        }

        private static Action<MatrixImplementor<Complex>, Complex[], int>[] CopyToOperators()
        {
            var operators = new Action<MatrixImplementor<Complex>, Complex[], int>[numberOfStorageSchemes];

            operators[dense] = ComplexMatrixOperators.Dense_CopyTo;
            operators[sparse] = ComplexMatrixOperators.Sparse_CopyTo;

            return operators;
        }
        private static readonly Action<MatrixImplementor<Complex>, Complex[], int>[]
            copyToOperators = CopyToOperators();


        /// <inheritdoc/>
        public void CopyTo(Complex[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(arrayIndex),
                    ImplementationServices.GetResourceString("STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (array.Length - arrayIndex < this.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString("STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY"));
            }

            copyToOperators[(int)this.implementor.StorageScheme](this.implementor, array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from 
        /// the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item">The object to remove from 
        /// the <see cref="ICollection{T}"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> was successfully removed 
        /// from the <see cref="ICollection{T}"></see>; otherwise, 
        /// false. This method also returns false if <paramref name="item">item</paramref> is 
        /// not found in the original <see cref="ICollection{T}"></see>.
        /// </returns>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        bool ICollection<Complex>.Remove(Complex item)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}