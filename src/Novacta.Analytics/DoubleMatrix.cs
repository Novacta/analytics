﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
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
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a collection of doubles arranged in rows and columns.
    /// Provides methods to operate algebraically on matrices.
    ///</summary>
    ///<remarks>
    /// <para><b>Instantiation</b></para>    
    /// <para>
    /// <see cref="DoubleMatrix"/> objects can be created using different storage 
    /// schemes. To allocate storage for each matrix entry, so applying 
    /// a <see cref="StorageScheme.Dense"/> scheme, one can exploit one of the 
    /// overloaded factory
    /// method <c>Dense</c>, such as <see cref="DoubleMatrix.Dense(int, int)"/>. 
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
    /// <latex depth="0" mode="inline">n-1</latex> <b> super-diagonals</b>: for 
    /// <latex mode="inline">k=1,\dots,n-1</latex>, the 
    /// <latex mode="inline">k</latex>-th super-diagonal is the collection of entries
    /// corresponding to the positions
    /// <latex mode="display">
    /// \{ (i,i+k) : i=0,\dots,\min\{m-1,n-k-1\} \}.
    /// </latex>
    /// </para>
    /// <para id='sub'>
    /// If <latex mode="inline">m>1</latex>, the matrix has 
    /// <latex depth="0" mode="inline">m-1</latex> <b> sub-diagonals</b>: for 
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
    /// If, after a call
    /// to <see cref="GetStorage()"/>, indexers are called that avoid
    /// dense allocations, such as <see cref="this[
    /// IndexCollection,IndexCollection,bool]"/> then the array returned
    /// by <see cref="GetStorage"/>
    /// must be treated as a read-only object:
    /// you shouldn't manipulate its entries; 
    /// otherwise, the 
    /// behavior of the calling <see cref="DoubleMatrix"/> instance 
    /// must be considered as undefined and  
    /// almost surely prone to errors.
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
    /// <para id='MinimizeMemoryUsage.3'>
    /// It is possible to create matrices whose entries are not directly stored. 
    /// Such behavior can be obtained by indexing into a 
    /// <see cref="DoubleMatrix"/> instance, henceforth referred to as 
    /// a <i>source</i> matrix, through
    /// <c>Item</c> getters accepting a boolean parameter named 
    /// <c>avoidDenseAllocations</c>: if such parameters evaluates to
    /// <b>true</b>, then the indexer will try to minimize memory allocations
    /// by avoiding the application 
    /// of the <see cref="StorageScheme.Dense">Dense</see> storage scheme.
    /// </para>
    /// <para id='MinimizeMemoryUsage.4'>
    /// Note that the returned matrix, referred to as the <i>sub</i> matrix, cannot 
    /// be considered as a view of the 
    /// <i>source</i> one: they are independent objects.
    /// This means that the system will try to minimize dense storage 
    /// allocations until possible, and, under some circumstances, the savings in 
    /// storage can be lost, as when
    /// setting entries of the <i>source</i> or the <i>sub</i> matrix, 
    /// when getting property <see cref="GetStorage()"/> of the <i>sub</i>
    /// matrix, or when it is involved in matrix division or
    /// multiplication operations.
    /// </para>
    /// <para><b>Serialization</b></para>
    /// <para>
    /// Matrices can be loaded from, or saved to a CSV file through the
    /// <see cref="CsvMatrixSerializer"/> class.
    /// </para>              
    ///</remarks>
    ///<seealso cref="DoubleMatrixRow">DoubleMatrixRow Class</seealso>
    ///<seealso cref="IndexCollection">IndexCollection Class</seealso>
    ///<seealso cref="ReadOnlyDoubleMatrix">ReadOnlyDoubleMatrix Class</seealso>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Naming",
        "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "Type is a data structure."), Serializable]
    public class DoubleMatrix :
        IList<double>,
        IReadOnlyList<double>,
        IMatrixPatterns,
        IReadOnlyTabularCollection<double, DoubleMatrix>,
        ITabularCollection<double, DoubleMatrix>,
        ISerializable
    {
        #region ISerializable

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DoubleMatrix"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info"/> is <b>null</b>.
        /// </exception>
        [SecurityPermission(
            SecurityAction.Demand,
            SerializationFormatter = true),
         System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1801:ReviewUnusedParameters",
        Justification = "Constructor requires parameter context.")]
        protected DoubleMatrix(
            SerializationInfo info,
            StreamingContext context)
        {
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            this.implementor =
                (MatrixImplementor<double>)info.GetValue(
                    "implementor",
                    typeof(MatrixImplementor<double>));

            this.Name =
                (string)info.GetValue(
                    "Name",
                    typeof(string));

            this.rowNames =
                (Dictionary<int, string>)info.GetValue(
                    "rowNames",
                    typeof(Dictionary<int, string>));

            this.columnNames =
                (Dictionary<int, string>)info.GetValue(
                    "columnNames",
                    typeof(Dictionary<int, string>));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info"/> is <b>null</b>.
        /// </exception>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            var matrixImplementor =
                this.implementor.StorageScheme == StorageScheme.View ?
                (DenseDoubleMatrixImplementor)(ViewDoubleMatrixImplementor)this.implementor
                :
                this.implementor;

            info.AddValue(
                "implementor",
                matrixImplementor,
                matrixImplementor.GetType());

            var name = this.Name;
            info.AddValue(
                "Name",
                name,
                typeof(string));

            var rowNames = this.rowNames;
            info.AddValue(
                "rowNames",
                rowNames,
                typeof(Dictionary<int, string>));

            var columnNames = this.columnNames;
            info.AddValue(
                "columnNames",
                columnNames,
                typeof(Dictionary<int, string>));
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleMatrix"/> class
        /// as a scalar matrix having the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public DoubleMatrix(double value)
        {
            this.implementor = new DenseDoubleMatrixImplementor(
                1, 1, new double[] { value }, StorageOrder.ColumnMajor);
        }

        internal DoubleMatrix(MatrixImplementor<Double> implementor)
        {
            this.implementor = implementor;
            this.implementor.ImplementorChanged +=
                new EventHandler<ImplementorChangedEventArgs>(
                    this.ImplementorChangedHandler);
        }

        #endregion

        #region AsReadOnly

        /// <summary>
        /// Returns a read-only representation of the <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <returns>The read-only wrapper of the <see cref="DoubleMatrix"/>.</returns>
        public ReadOnlyDoubleMatrix AsReadOnly()
        {
            return new ReadOnlyDoubleMatrix(this);
        }

        #endregion

        #region Conversion operators

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="IndexPartition{T}"/> 
        /// of <see cref="System.Double"/>
        /// to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">
        /// The object to convert.
        /// </param>
        /// <returns>
        /// The converted object.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The partition is converted to a column vector whose row 
        /// indexes are partitioned following the different parts of <paramref name="value"/>, 
        /// so that the rows whose indexes are in a given part will be filled 
        /// with the identifier of such part.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, an index partition having doubles as 
        /// part identifiers is converted to a matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample4.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static explicit operator DoubleMatrix(IndexPartition<double> value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            // Compute the matrix count
            int numberOfItems = 0;
            foreach (var identifier in value.Identifiers)
            {
                numberOfItems += value[identifier].Count;
            }

            var doubleMatrix = DoubleMatrix.Dense(numberOfItems, 1);

            foreach (var identifier in value.Identifiers)
            {
                doubleMatrix[value[identifier], 0] += identifier;
            }

            return doubleMatrix;
        }

        /// <summary>
        /// Converts  
        /// from <see cref="IndexPartition{T}"/> 
        /// of <see cref="System.Double"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">
        /// The object to convert.
        /// </param>
        /// <returns>
        /// The converted object.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The partition is converted to a column vector whose row 
        /// indexes are partitioned following the different parts of <paramref name="value"/>, 
        /// so that the rows whose indexes are in a given part will be filled 
        /// with the identifier of such part.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, an index partition having doubles as 
        /// part identifiers is converted to a matrix.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample6.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix FromIndexPartition(IndexPartition<double> value)
        {
            return (DoubleMatrix)value;
        }

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="ReadOnlyDoubleMatrix"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static explicit operator DoubleMatrix(ReadOnlyDoubleMatrix value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return value.matrix.Clone();
        }

        /// <summary>
        /// Converts 
        /// from <see cref="ReadOnlyDoubleMatrix"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix FromReadOnlyDoubleMatrix(ReadOnlyDoubleMatrix value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return value.matrix.Clone();
        }

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="System.Double"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static explicit operator DoubleMatrix(double value)
        {
            return new DoubleMatrix(value);
        }

        /// <summary>
        /// Converts 
        /// from <see cref="System.Double"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static DoubleMatrix FromDouble(double value)
        {
            return new DoubleMatrix(value);
        }

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="DoubleMatrix"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <remarks>
        /// <para>
        /// Only scalar matrices are successfully converted to a double.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not scalar.
        /// </exception>
        public static explicit operator double(DoubleMatrix value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (!value.IsScalar)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR"),
                    nameof(value));
            }

            return value[0];
        }

        /// <summary>
        /// Converts 
        /// from <see cref="DoubleMatrix"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <remarks>
        /// <para>
        /// Only scalar matrices are successfully converted to a double.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not scalar.
        /// </exception>
        public static double ToDouble(DoubleMatrix value)
        {
            return (double)value;
        }

        #endregion

        #region Implementor

        internal MatrixImplementor<Double> implementor;

        private void ImplementorChangedHandler(object sender, ImplementorChangedEventArgs e)
        {
            this.implementor.ImplementorChanged -=
                new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
            this.implementor = (MatrixImplementor<Double>)e.NewImplementor;
            this.implementor.ImplementorChanged +=
                new EventHandler<ImplementorChangedEventArgs>(this.ImplementorChangedHandler);
        }

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
                    this.rowNames = new Dictionary<int, string>();
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
            ImplementationServices.ThrowOnNullOrWhiteSpace(rowName, DoubleMatrix.paramRowName);

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
                this.rowNames = new Dictionary<int, string>();

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

            if (!(this.rowNames is null))
            {
                clonedRowNames = new Dictionary<int, string>();
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
                    this.columnNames = new Dictionary<int, string>();
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
            ImplementationServices.ThrowOnNullOrWhiteSpace(columnName, DoubleMatrix.paramColumnName);

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
                this.columnNames = new Dictionary<int, string>();

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

            if (!(this.columnNames is null))
            {
                clonedColumnNames = new Dictionary<int, string>();
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
        /// Finalizes an instance of the <see cref="DoubleMatrix"/> class.
        /// </summary>
        ~DoubleMatrix()
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
                /*
                 * The object is being explicitly disposed/closed, not finalized.
                 * It is therefore safe for category in this "if" statement to access 
                 * fields that reference other objects, because the Finalize() 
                 * method of these other objects hasn't yet been called
                 */
                this.implementor.OnChangingData();
            }
        }

        #endregion

        #region AsRowCollection

        /// <summary>
        /// Returns the collection of rows in the <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <returns>The collection of rows in this instance.</returns>
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
        /// <seealso cref="DoubleMatrixRowCollection"/>
        public DoubleMatrixRowCollection AsRowCollection()
        {
            var rows = new ObservableCollection<DoubleMatrixRow>();

            for (int i = 0; i < this.NumberOfRows; i++)
                rows.Add(new DoubleMatrixRow(i));

            return new DoubleMatrixRowCollection(rows, this);
        }

        /// <summary>
        /// Returns the collection of the rows in the <see cref="DoubleMatrix" />
        /// corresponding to the specified indexes.
        /// </summary>
        /// <param name="rowIndexes">The indexes of the rows to collect.</param>
        /// <returns>The collection of the specified rows in this instance.</returns>
        /// <example>
        ///   <para>
        /// In the following example, some rows of a matrix
        /// are enumerated.
        /// </para>
        ///   <para>
        ///     <code source="..\Novacta.Analytics.CodeExamples\RowsEnumeratorExample1.cs.txt" language="cs" />
        ///   </para>
        /// </example>
        /// <seealso cref="DoubleMatrixRowCollection" />
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the <see cref="NumberOfRows"/> of this instance.
        /// </exception>
        public DoubleMatrixRowCollection AsRowCollection(IndexCollection rowIndexes)
        {
            if (rowIndexes is null)
            {
                throw new ArgumentNullException(nameof(rowIndexes));
            }
            if (rowIndexes.maxIndex >= this.NumberOfRows)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            var rows = new ObservableCollection<DoubleMatrixRow>();

            for (int i = 0; i < rowIndexes.Count; i++)
                rows.Add(new DoubleMatrixRow(rowIndexes[i]));

            return new DoubleMatrixRowCollection(rows, this);
        }

        #endregion

        #region ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="DoubleMatrix"/> that is a copy of this instance.</returns>
        /// <remarks>
        /// <para>
        /// This method executes a deep copy of the matrix: the returned object
        /// and the cloned one will have independent states.
        /// </para>
        /// </remarks>
        internal DoubleMatrix Clone()
        {
            DoubleMatrix clone = new DoubleMatrix((MatrixImplementor<double>)this.implementor.Clone())
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
        /// Creates a <see cref="DoubleMatrix"/> instance 
        /// representing the identity matrix having the specified dimension.
        /// </summary>
        /// <param name="dimension">The dimension of the identity matrix.</param>
        /// <returns>The identity matrix having the specified dimension.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="dimension"/> is not positive.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Identity_matrix"/>
        public static DoubleMatrix Identity(
            int dimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            var identityImplementor = new SparseCsr3DoubleMatrixImplementor(
                dimension, dimension, dimension);

            for (int i = 0; i < dimension; i++)
            {
                identityImplementor.SetValue(i, i, 1.0);
            }

            return new DoubleMatrix(identityImplementor);
        }

        /// <summary>
        /// Creates a square <see cref="DoubleMatrix"/> instance having the specified 
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
        /// source="..\Novacta.Analytics.CodeExamples\DiagonalExample0.cs.txt" 
        /// language="cs" />
        /// </para>  
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mainDiagonal"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="IsSquare"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Diagonal_matrix"/>
        public static DoubleMatrix Diagonal(
            DoubleMatrix mainDiagonal)
        {
            if (mainDiagonal is null)
            {
                throw new ArgumentNullException(nameof(mainDiagonal));
            }

            int dimension = mainDiagonal.Count;
            var diagonalImplementor = new SparseCsr3DoubleMatrixImplementor(dimension, dimension, dimension);

            DoubleMatrix diagonal = new DoubleMatrix(diagonalImplementor);

            for (int i = 0; i < dimension; i++)
            {
                diagonal[i, i] = mainDiagonal[i];
            }

            return diagonal;
        }

        /// <inheritdoc cref = "Diagonal(DoubleMatrix)"/>
        public static DoubleMatrix Diagonal(
            ReadOnlyDoubleMatrix mainDiagonal)
        {
            if (mainDiagonal is null)
            {
                throw new ArgumentNullException(nameof(mainDiagonal));
            }

            return Diagonal(mainDiagonal.matrix);
        }

        /// <summary>
        /// Creates a sparse <see cref="DoubleMatrix"/> instance having the specified
        /// size and initial capacity to store entries different from zero.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="capacity">The initial capacity to store non-zero entries.</param>
        /// <returns>The matrix having the specified size and initial capacity.</returns>
        /// <remarks>
        /// <para id='Sparse.0'>
        /// <see cref="DoubleMatrix"/> sparse instances allocate storage only for a number of
        /// matrix entries equal to <paramref name="capacity"/>. If an entry is not 
        /// explicitly set, it is interpreted as zero. If needed, the capacity
        /// is automatically updated to store more non-zero entries.
        /// Dense <see cref="DoubleMatrix"/> instances, i.e. instances allocating storage for
        /// each of their entries, can be created by calling 
        /// method
        /// <see cref="DoubleMatrix.Dense(int, int)"/> or
        /// one of its overloaded versions.
        /// </para>
        /// <para id='Sparse.1'>
        /// <note type="note">In the current version of the Novacta.Analytics
        /// assembly, this method stores non-zero entries using the compressed sparse column scheme.</note>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para id='Sparse.2'>
        /// In the following example, a sparse matrix is created.
        ///</para>
        /// <para id='Sparse.3'>
        /// <code title="Creation of a sparse matrix"
        /// source="..\Novacta.Analytics.CodeExamples\SparseExample0.cs.txt" 
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
        /// <seealso href="https://en.wikipedia.org/wiki/Sparse_matrix#Compressed_sparse_column_.28CSC_or_CCS.29"/>
        public static DoubleMatrix Sparse(
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

            return new DoubleMatrix(new SparseCsr3DoubleMatrixImplementor(
                numberOfRows, numberOfColumns, capacity));
        }


        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the same
        /// size and data of the specified two-dimensional array.
        /// </summary>
        /// <param name="data">The two-dimensional array containing matrix data.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, some dense matrices are created from  
        /// two-dimensional arrays.
        /// </para>
        /// <para>
        /// <code title="Creation of dense matrices from two-dimensional arrays"
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample6.cs.txt" 
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1814:Prefer jagged arrays over multidimensional",
            Justification = "The array does not waste space.")]
        public static DoubleMatrix Dense(
            double[,] data)
        {
            #region Input validation

            if (null == data)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int numberOfRows = data.GetLength(0);
            int numberOfColumns = data.GetLength(1);

            if (numberOfRows == 0 || numberOfColumns == 0)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS"), nameof(data));
            }

            #endregion

            return new DoubleMatrix(new DenseDoubleMatrixImplementor(data));
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
        /// specified size, and assigns zero to each matrix entry.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a dense matrix is created having all its entries equal to zero.
        ///</para>
        /// <para>
        /// <code title="Creation of a dense matrix with all entries equal to zero"
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample5.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive.
        /// </exception>
        public static DoubleMatrix Dense(int numberOfRows, int numberOfColumns)
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

            return new DoubleMatrix(new DenseDoubleMatrixImplementor(
                numberOfRows, numberOfColumns));
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
        /// specified size, and assigns the same value to each matrix entry.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The value assigned to each matrix entry.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// </remarks>
        /// <example>
        /// <para id='Dense.Double.2'>
        /// In the following example, a dense matrix is created having all its entries equal to a given value.
        ///</para>
        /// <para id='Dense.Double.3'>
        /// <code title="Creation of a dense matrix with all entries equal to a given value"
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample4.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfRows"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfColumns"/> is not positive. 
        /// </exception>
        /// <seealso cref="Analytics.StorageOrder"/>
        public static DoubleMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            double data)
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

            var matrix = new DoubleMatrix(new DenseDoubleMatrixImplementor(
                numberOfRows, numberOfColumns));
            var matrixStorage = matrix.implementor.Storage;
            for (int i = 0; i < matrixStorage.Length; i++)
            {
                matrixStorage[i] = data;
            }

            return matrix;
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
        /// specified size, and assigns data to entries 
        /// assuming ColMajor ordering.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
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
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample2.cs.txt" 
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
        public static DoubleMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            double[] data)
        {
            return Dense(
                numberOfRows,
                numberOfColumns,
                data,
                StorageOrder.ColumnMajor);
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
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
        /// <see cref="DoubleMatrix"/> dense instances allocate storage for each matrix entry.
        /// Sparse <see cref="DoubleMatrix"/> instances can be created by calling method
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
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample0.cs.txt" 
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
        public static DoubleMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            double[] data,
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

            if (null == data)
            {
                throw new ArgumentNullException(nameof(data));
            }

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

            return new DoubleMatrix(new DenseDoubleMatrixImplementor(
                numberOfRows, numberOfColumns, data, storageOrder));
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
        /// specified size, and assigns data to entries, 
        /// possibly preventing copying operations before creation.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <param name="copyData">
        /// <b>true</b> if <paramref name="data"/> 
        /// must be copied before instantiation; otherwise <b>false</b>.
        /// </param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <para>
        /// <see cref="DoubleMatrix"/> dense instances allocate storage for each matrix 
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
        public static DoubleMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            double[] data,
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

            if (null == data)
            {
                throw new ArgumentNullException(nameof(data));
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

            return new DoubleMatrix(new DenseDoubleMatrixImplementor(
                numberOfRows, numberOfColumns, data, copyData));
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
        /// specified size, and assigns data to entries 
        /// assuming ColMajor ordering.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="data">The data assigned to matrix entries.</param>
        /// <returns>The matrix having the specified size and data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// <inheritdoc cref="Dense(int, int, double[])" 
        /// path="para[@id='Dense.DefaultStorageOrder.1']"/>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="Dense(int, int, double[])" 
        /// path="para[@id='Dense.DefaultStorageOrder.2']"/>
        /// <para>
        /// <code title="Creation of a dense matrix with ColMajor ordered data"
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample3.cs.txt" 
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
        public static DoubleMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            IEnumerable<double> data)
        {
            return Dense(
                numberOfRows,
                numberOfColumns,
                data,
                StorageOrder.ColumnMajor);
        }

        /// <summary>
        /// Creates a dense <see cref="DoubleMatrix"/> instance having the 
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
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.0']"/>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.1']"/>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="Dense(int, int, double[], StorageOrder)" 
        /// path="para[@id='Dense.StorageOrder.2']"/>
        /// <para id='Dense.3'>
        /// <code title="Creation of a dense matrix with RowMajor or ColMajor ordered data"
        /// source="..\Novacta.Analytics.CodeExamples\DenseExample1.cs.txt" 
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
        public static DoubleMatrix Dense(
            int numberOfRows,
            int numberOfColumns,
            IEnumerable<double> data,
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

            if (null == data)
            {
                throw new ArgumentNullException(nameof(data));
            }

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

            return new DoubleMatrix(new DenseDoubleMatrixImplementor(
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

            string numberFormatSpecifier = "{0,-17:g10}";

            bool columnsHaveNames = this.HasColumnNames;
            bool rowsHaveNames = this.HasRowNames;

            int numberOfRows = this.NumberOfRows;
            int numberOfColumns = this.NumberOfColumns;

            // The representation of names must have length 17, right aligned,
            // but a name must have no more than 14 chars (two chars needed for [ and ], 
            // the last one to 
            // needed to separate them each from the other.
            string blankNamesFormatSpecifier = "{0,-17}";
            string truncatedNamesFormatSpecifier = "[{0,-14}] ";
            int maximumNameLength = numberOfCharacters - 3;

            if (columnsHaveNames)
            {
                if (rowsHaveNames)
                {
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture,
                        blankNamesFormatSpecifier,
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
                        if (columnNameLength > maximumNameLength)
                        {
                            columnName = columnName.Insert(maximumNameLength - 1, "*")
                                .Substring(0, maximumNameLength);
                            stringBuilder.AppendFormat(
                                CultureInfo.InvariantCulture, truncatedNamesFormatSpecifier, columnName);
                        }
                        else
                        {
                            columnName =
                                string.Format(
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
                        if (rowNameLength > maximumNameLength)
                        {
                            rowName = rowName.Insert(maximumNameLength - 1, "*")
                                .Substring(0, maximumNameLength);
                            stringBuilder.AppendFormat(
                                CultureInfo.InvariantCulture, truncatedNamesFormatSpecifier, rowName);
                        }
                        else
                        {
                            rowName =
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    "[{0}]",
                                    rowName).PadRight(numberOfCharacters);
                            stringBuilder.Append(rowName);
                        }
                    }
                    else
                        stringBuilder.AppendFormat(
                            CultureInfo.InvariantCulture,
                            blankNamesFormatSpecifier,
                            " ");
                }

                for (int j = 0; j < numberOfColumns; j++)
                {
                    stringBuilder.AppendFormat(numberFormatInfo, numberFormatSpecifier,
                        this.implementor[i, j]);
                }
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append(Environment.NewLine);
            return stringBuilder.ToString();
        }

        #endregion

        #region Operations

        private const int dense = (int)StorageScheme.Dense;
        private const int view = (int)StorageScheme.View;
        private const int sparse = (int)StorageScheme.CompressedRow;
        private const int numberOfStorageSchemes = 3;

        #region Add

        #region Matrix

        private static DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> SumOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>>(numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Dense_Sum);
            operators[dense, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_View_Sum);
            operators[view, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Dense_Sum);
            operators[view, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_View_Sum);

            operators[sparse, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Dense_Sum);
            operators[dense, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Sparse_Sum);
            operators[sparse, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Sparse_Sum);
            operators[sparse, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_View_Sum);
            operators[view, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Sparse_Sum);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> sumOperators = SumOperators();

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
        /// <code source="..\Novacta.Analytics.CodeExamples\AdditionExample0.cs.txt" 
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
        public static DoubleMatrix operator +(DoubleMatrix left, DoubleMatrix right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(sumOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Addition(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Add(DoubleMatrix left, DoubleMatrix right)
        {
            return left + right;
        }

        #endregion

        #region Scalar

        private static DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> ScalarSumOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Dense_Sum);
            operators[view] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_View_Sum);
            operators[sparse] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Sparse_Sum);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> scalarSumOperators = ScalarSumOperators();

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
        /// <code source="..\Novacta.Analytics.CodeExamples\AdditionExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator +(DoubleMatrix left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            return new DoubleMatrix(scalarSumOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Addition(DoubleMatrix,double)"/>
        public static DoubleMatrix Add(DoubleMatrix left, double right)
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
        /// <code source="..\Novacta.Analytics.CodeExamples\AdditionExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator +(double left, DoubleMatrix right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(scalarSumOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Addition(double,DoubleMatrix)"/>
        public static DoubleMatrix Add(double left, DoubleMatrix right)
        {
            return left + right;
        }

        #endregion

        #endregion

        #region Apply

        #region InPlace

        private static DenseMatrixImplementor<MatrixInPlaceApplyOperator<double>> InPlaceApplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixInPlaceApplyOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] = new MatrixInPlaceApplyOperator<double>(DoubleMatrixOperators.Dense_InPlaceApply);
            operators[view] = new MatrixInPlaceApplyOperator<double>(DoubleMatrixOperators.View_InPlaceApply);
            operators[sparse] = new MatrixInPlaceApplyOperator<double>(DoubleMatrixOperators.Sparse_InPlaceApply);

            return operators;
        }
        private static readonly
            DenseMatrixImplementor<MatrixInPlaceApplyOperator<double>>
                inPlaceApplyOperators = InPlaceApplyOperators();

        /// <summary>
        /// Evaluates the specified function at each entry of this instance,
        /// and substitutes each entry with its corresponding function value.
        /// </summary>
        /// <param name="func">The function to apply to each matrix entry.</param>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Let <latex>y=f\round{x}</latex> the function represented by
        /// <paramref name="func"/>.
        /// Then method <see cref="InPlaceApply(Func{double, double})"/> transforms 
        /// <latex>A</latex> by setting
        /// <latex>A_{i,j} \leftarrow f\round{A_{i,j}},\hspace{12pt} j=0,\dots,n-1,\hspace{12pt} i=0,\dots,m-1.</latex> 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the entries in a matrix are all squared.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\InPlaceApplyExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="Apply"/>
        public void InPlaceApply(Func<double, double> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            this.implementor.OnChangingData();
            inPlaceApplyOperators[(int)this.implementor.StorageScheme](this.implementor, func);
        }

        #endregion

        #region OutPlace

        private static DenseMatrixImplementor<MatrixOutPlaceApplyOperator<double>> OutPlaceApplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixOutPlaceApplyOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] = new MatrixOutPlaceApplyOperator<double>(DoubleMatrixOperators.Dense_OutPlaceApply);
            operators[view] = new MatrixOutPlaceApplyOperator<double>(DoubleMatrixOperators.View_OutPlaceApply);
            operators[sparse] = new MatrixOutPlaceApplyOperator<double>(DoubleMatrixOperators.Sparse_OutPlaceApply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixOutPlaceApplyOperator<double>> outPlaceApplyOperators = OutPlaceApplyOperators();

        /// <summary>
        /// Evaluates the specified function at each entry of this instance,
        /// and returns a matrix whose entries are given by 
        /// the corresponding function values.
        /// </summary>
        /// <param name="func">The function to apply to each matrix entry.</param>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Let <latex>y=f\round{x}</latex> the function represented by
        /// <paramref name="func"/>.
        /// Then method <see cref="Apply(Func{double, double})"/> returns a matrix,
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
        /// <code source="..\Novacta.Analytics.CodeExamples\OutPlaceApplyExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>A matrix whose entries are given by 
        /// the values the specified function takes on at the entries of this instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="InPlaceApply"/>
        public DoubleMatrix Apply(Func<double, double> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return new DoubleMatrix(outPlaceApplyOperators[
                (int)this.implementor.StorageScheme]
                    (this.implementor, func));
        }

        #endregion

        #endregion

        #region Element-wise multiply

        private static DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> MultiplyByElementOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>>(numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Dense_ElementWiseMultiply);
            operators[dense, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_View_ElementWiseMultiply);
            operators[view, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Dense_ElementWiseMultiply);
            operators[view, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_View_ElementWiseMultiply);

            operators[sparse, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Dense_ElementWiseMultiply);
            operators[dense, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Sparse_ElementWiseMultiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Sparse_ElementWiseMultiply);
            operators[sparse, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_View_ElementWiseMultiply);
            operators[view, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Sparse_ElementWiseMultiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>>
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
        /// <inheritdoc cref="op_Addition(DoubleMatrix,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\ElementWiseMultiplyExample0.cs.txt" 
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
        public static DoubleMatrix ElementWiseMultiply(DoubleMatrix left, DoubleMatrix right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(multiplyByElementOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        #endregion

        #region Find

        #region Value

        private static DenseMatrixImplementor<FindValueOperator<double>> FindValueOperators()
        {
            var operators =
                new DenseMatrixImplementor<FindValueOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] =
                new FindValueOperator<double>(DoubleMatrixOperators.Dense_FindValue);

            operators[view] =
                new FindValueOperator<double>(DoubleMatrixOperators.View_FindValue);

            operators[sparse] =
                new FindValueOperator<double>(DoubleMatrixOperators.Sparse_FindValue);

            return operators;
        }
        private static readonly DenseMatrixImplementor<FindValueOperator<double>>
            findValueDoubleOperators = FindValueOperators();

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
        /// <code source="..\Novacta.Analytics.CodeExamples\FindExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public IndexCollection Find(double value)
        {
            var implementor = this.implementor;

            if (Double.IsNaN(value))
            {
                // Local function
                bool match(double d) { return Double.IsNaN(d); }
                return findWhileDoubleOperators[(int)implementor.StorageScheme](implementor, match);
            }

            return findValueDoubleOperators[(int)implementor.StorageScheme](implementor, value);
        }

        #endregion

        #region Nonzero

        private static DenseMatrixImplementor<FindNonzeroOperator<double>> FindNonzeroOperators()
        {
            var operators = new
                DenseMatrixImplementor<FindNonzeroOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] =
                new FindNonzeroOperator<double>(DoubleMatrixOperators.Dense_FindNonzero);

            operators[view] =
                new FindNonzeroOperator<double>(DoubleMatrixOperators.View_FindNonzero);

            operators[sparse] =
                new FindNonzeroOperator<double>(DoubleMatrixOperators.Sparse_FindNonzero);

            return operators;
        }
        private static readonly DenseMatrixImplementor<FindNonzeroOperator<double>>
            findNonzeroDoubleOperators = FindNonzeroOperators();

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
        /// <code source="..\Novacta.Analytics.CodeExamples\FindNonzeroExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public IndexCollection FindNonzero()
        {
            var implementor = this.implementor;
            return findNonzeroDoubleOperators[(int)implementor.StorageScheme](implementor);
        }

        #endregion

        #region While

        private static DenseMatrixImplementor<FindWhileOperator<double>> FindWhileOperators()
        {
            var operators =
                new DenseMatrixImplementor<FindWhileOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] =
                new FindWhileOperator<double>(DoubleMatrixOperators.Dense_FindWhile);

            operators[view] =
                new FindWhileOperator<double>(DoubleMatrixOperators.View_FindWhile);

            operators[sparse] =
                new FindWhileOperator<double>(DoubleMatrixOperators.Sparse_FindWhile);

            return operators;
        }
        private static readonly DenseMatrixImplementor<FindWhileOperator<double>>
            findWhileDoubleOperators = FindWhileOperators();

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
        /// <code source="..\Novacta.Analytics.CodeExamples\FindWhileExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="match"/> is <b>null</b>.
        /// </exception>
        public IndexCollection FindWhile(Predicate<double> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            var implementor = this.implementor;
            return findWhileDoubleOperators[(int)implementor.StorageScheme](implementor, match);
        }

        #endregion

        #endregion

        #region Transpose

        #region InPlace

        private static DenseMatrixImplementor<MatrixInPlaceOperator<double>> InPlaceTransposeOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixInPlaceOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] = new MatrixInPlaceOperator<double>(DoubleMatrixOperators.Dense_InPlaceTranspose);
            operators[view] = new MatrixInPlaceOperator<double>(DoubleMatrixOperators.View_InPlaceTranspose);
            operators[sparse] = new MatrixInPlaceOperator<double>(DoubleMatrixOperators.Sparse_InPlaceTranspose);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixInPlaceOperator<double>> inPlaceTransposeOperators = InPlaceTransposeOperators();

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\InPlaceTransposeExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso cref="Transpose"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Transpose"/>
        public void InPlaceTranspose()
        {
            this.implementor.OnChangingData();
            inPlaceTransposeOperators[(int)this.implementor.StorageScheme](this.implementor);

            if (this.HasRowNames)
            {
                if (this.HasColumnNames)
                {
                    var rowNames = this.rowNames;
                    this.rowNames = this.columnNames;
                    this.columnNames = rowNames;
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

        private static DenseMatrixImplementor<MatrixUnaryOperator<double, double>> OutPlaceTransposeOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixUnaryOperator<double, double>>(numberOfStorageSchemes, 1);

            operators[dense] = new MatrixUnaryOperator<double, double>(DoubleMatrixOperators.Dense_OutPlaceTranspose);
            operators[view] = new MatrixUnaryOperator<double, double>(DoubleMatrixOperators.View_OutPlaceTranspose);
            operators[sparse] = new MatrixUnaryOperator<double, double>(DoubleMatrixOperators.Sparse_OutPlaceTranspose);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixUnaryOperator<double, double>> outPlaceTransposeOperators = OutPlaceTransposeOperators();

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
        /// <code source="..\Novacta.Analytics.CodeExamples\OutPlaceTransposeExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The transpose of this instance.</returns>
        /// <seealso cref="InPlaceTranspose"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Transpose"/>
        public DoubleMatrix Transpose()
        {
            DoubleMatrix transposed = new DoubleMatrix(outPlaceTransposeOperators[
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

        #region Divide

        #region Matrix

        private static DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> DivideOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>>(numberOfStorageSchemes, numberOfStorageSchemes);

            operators[dense, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Dense_Divide);
            operators[dense, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_View_Divide);
            operators[view, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Dense_Divide);
            operators[view, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_View_Divide);

            operators[sparse, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Dense_Divide);
            operators[dense, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Sparse_Divide);
            operators[sparse, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Sparse_Divide);
            operators[sparse, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_View_Divide);
            operators[view, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Sparse_Divide);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> divideOperators = DivideOperators();

        /// <summary>
        /// Determines the division of a matrix by another.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(DoubleMatrix,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\DivisionExample0.cs.txt" 
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
        public static DoubleMatrix operator /(DoubleMatrix left, DoubleMatrix right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(divideOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Division(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Divide(DoubleMatrix left, DoubleMatrix right)
        {
            return left / right;
        }

        #endregion

        #region Right scalar

        private static DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> ScalarRightDivideOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Dense_RightDivide);
            operators[view] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_View_RightDivide);
            operators[sparse] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Sparse_RightDivide);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> scalarRightDivideOperators = ScalarRightDivideOperators();

        /// <summary>
        /// Determines the division of a matrix by a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(DoubleMatrix,double)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\DivisionExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator /(DoubleMatrix left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            return new DoubleMatrix(scalarRightDivideOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Division(DoubleMatrix,double)"/>
        public static DoubleMatrix Divide(DoubleMatrix left, double right)
        {
            return left / right;
        }

        #endregion

        #region Left scalar

        private static DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> ScalarLeftDivideOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Dense_LeftDivide);
            operators[view] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_View_LeftDivide);
            operators[sparse] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Sparse_LeftDivide);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> scalarLeftDivideOperators = ScalarLeftDivideOperators();


        /// <summary>
        /// Determines the division of a scalar by a matrix.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of dividing <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(double,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\DivisionExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator /(double left, DoubleMatrix right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(scalarLeftDivideOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Division(double,DoubleMatrix)"/>
        public static DoubleMatrix Divide(double left, DoubleMatrix right)
        {
            return left / right;
        }

        #endregion

        #endregion

        #region Multiply

        #region Matrix

        private static DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> MultiplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>>(numberOfStorageSchemes, numberOfStorageSchemes);

            operators[dense, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Dense_Multiply);
            operators[dense, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_View_Multiply);
            operators[view, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Dense_Multiply);
            operators[view, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_View_Multiply);

            operators[sparse, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Dense_Multiply);
            operators[dense, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Sparse_Multiply);
            operators[sparse, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Sparse_Multiply);
            operators[sparse, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_View_Multiply);
            operators[view, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Sparse_Multiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> multiplyOperators = MultiplyOperators();

        /// <summary>
        /// Determines the product of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of multiplying <paramref name="left"/> by <paramref name="right"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(DoubleMatrix,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\MultiplicationExample0.cs.txt" 
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
        public static DoubleMatrix operator *(DoubleMatrix left, DoubleMatrix right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(multiplyOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Multiply(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Multiply(DoubleMatrix left, DoubleMatrix right)
        {
            return left * right;
        }

        #endregion;

        #region scalar

        private static DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> ScalarMultiplyOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Dense_Multiply);
            operators[view] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_View_Multiply);
            operators[sparse] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Sparse_Multiply);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> scalarMultiplyOperators = ScalarMultiplyOperators();


        /// <summary>
        /// Determines the multiplication of a matrix by a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of multiplying <paramref name="right"/> by <paramref name="left"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(DoubleMatrix,double)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\MultiplicationExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator *(DoubleMatrix left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            return new DoubleMatrix(scalarMultiplyOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Multiply(DoubleMatrix,double)"/>
        public static DoubleMatrix Multiply(DoubleMatrix left, double right)
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
        /// <inheritdoc cref="op_Addition(double,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\MultiplicationExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator *(double left, DoubleMatrix right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(scalarMultiplyOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Multiply(double,DoubleMatrix)"/>
        public static DoubleMatrix Multiply(double left, DoubleMatrix right)
        {
            return left * right;
        }

        #endregion

        #endregion

        #region Subtract

        #region Matrix

        private static DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> SubtractOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>>(numberOfStorageSchemes, numberOfStorageSchemes);
            operators[dense, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Dense_Subtract);
            operators[dense, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_View_Subtract);
            operators[view, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Dense_Subtract);
            operators[view, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_View_Subtract);

            operators[sparse, dense] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Dense_Subtract);
            operators[dense, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Dense_Sparse_Subtract);
            operators[sparse, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_Sparse_Subtract);
            operators[sparse, view] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_Sparse_View_Subtract);
            operators[view, sparse] = new MatrixBinaryOperator<double, double, double>(DoubleMatrixOperators.Matrix_View_Sparse_Subtract);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixBinaryOperator<double, double, double>> subtractOperators = SubtractOperators();

        /// <summary>
        /// Determines the difference between two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(DoubleMatrix,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\SubtractionExample0.cs.txt" 
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
        public static DoubleMatrix operator -(DoubleMatrix left, DoubleMatrix right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(subtractOperators[(int)left.implementor.StorageScheme,
               (int)right.implementor.StorageScheme](left.implementor, right.implementor));
        }

        /// <inheritdoc cref = "op_Subtraction(DoubleMatrix,DoubleMatrix)"/>
        public static DoubleMatrix Subtract(DoubleMatrix left, DoubleMatrix right)
        {
            return left - right;
        }

        #endregion

        #region Right scalar

        private static DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> ScalarRightSubtractOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Dense_RightSubtract);
            operators[view] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_View_RightSubtract);
            operators[sparse] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Sparse_RightSubtract);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> scalarRightSubtractOperators = ScalarRightSubtractOperators();

        /// <summary>
        /// Determines the subtraction of a scalar from a matrix.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of subtracting <paramref name="right"/> from <paramref name="left"/>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(DoubleMatrix,double)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\SubtractionExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator -(DoubleMatrix left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            return new DoubleMatrix(scalarRightSubtractOperators[(int)left.implementor.StorageScheme]
                (left.implementor, right));
        }

        /// <inheritdoc cref = "op_Subtraction(DoubleMatrix,double)"/>
        public static DoubleMatrix Subtract(DoubleMatrix left, double right)
        {
            return left - right;
        }

        #endregion

        #region Left scalar

        private static DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> ScalarLeftSubtractOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Dense_LeftSubtract);
            operators[view] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_View_LeftSubtract);
            operators[sparse] = new MatrixScalarBinaryOperator<double, double, double>(DoubleMatrixOperators.Scalar_Sparse_LeftSubtract);

            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixScalarBinaryOperator<double, double, double>> scalarLeftSubtractOperators = ScalarLeftSubtractOperators();

        /// <summary>
        /// Determines the subtraction of a matrix from a scalar.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of subtracting <paramref name="right"/> from
        /// <paramref name="left"/>.</returns>
        /// <remarks>
        /// <inheritdoc cref="op_Addition(double,DoubleMatrix)" 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\SubtractionExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator -(double left, DoubleMatrix right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return new DoubleMatrix(scalarLeftSubtractOperators[(int)right.implementor.StorageScheme]
                (right.implementor, left));
        }

        /// <inheritdoc cref = "op_Subtraction(double,DoubleMatrix)"/>
        public static DoubleMatrix Subtract(double left, DoubleMatrix right)
        {
            return left - right;
        }

        #endregion

        #endregion

        #region Negate

        private static DenseMatrixImplementor<MatrixUnaryOperator<double, double>> NegationOperators()
        {
            var operators = new DenseMatrixImplementor<MatrixUnaryOperator<double, double>>(numberOfStorageSchemes, 1);
            operators[dense] = new MatrixUnaryOperator<double, double>(DoubleMatrixOperators.Matrix_Dense_Negation);
            operators[view] = new MatrixUnaryOperator<double, double>(DoubleMatrixOperators.Matrix_View_Negation);
            operators[sparse] = new MatrixUnaryOperator<double, double>(DoubleMatrixOperators.Matrix_Sparse_Negation);
            return operators;
        }
        private static readonly DenseMatrixImplementor<MatrixUnaryOperator<double, double>>
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
        /// <code source="..\Novacta.Analytics.CodeExamples\NegationExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix operator -(DoubleMatrix matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            return new DoubleMatrix(negationOperators[(int)matrix.implementor.StorageScheme]
                (matrix.implementor));
        }

        /// <inheritdoc cref = "op_UnaryNegation(DoubleMatrix)"/>
        public static DoubleMatrix Negate(DoubleMatrix matrix)
        {
            return -matrix;
        }

        #endregion

        #endregion

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

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Double> GetEnumerator()
        {
            return new DoubleMatrixEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DoubleMatrixEnumerator(this);
        }

        #endregion

        #region Storage

        /// <summary>
        /// Returns a column major ordered, dense representation of this instance.
        /// </summary>
        /// <returns>The column major ordered dense array representing this
        /// instance.</returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.2']"/>
        /// </remarks>
        public Double[] AsColumnMajorDenseArray()
        {
            this.implementor.OnChangingData();
            return this.implementor.AsColumnMajorDenseArray();
        }

        /// <summary>
        /// Gets the elements currently stored in this instance.
        /// </summary>
        /// <returns>The array of stored matrix elements.</returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.0']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.1']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.1.1']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.3']"/>
        /// </remarks>
        public double[] GetStorage()
        {
            this.implementor.OnChangingData();
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
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample00.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public double this[int rowIndex, int columnIndex]
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
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample01.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[int rowIndex, IndexCollection columnIndexes]
        {
            get
            {
                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                var subMatrix = new DoubleMatrix(this.implementor[rowIndex, columnIndexes]);

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
                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                this.implementor[rowIndex, columnIndexes] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[int,string]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample02.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[int rowIndex, string columnIndexes]
        {
            get
            {
                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subMatrix = new DoubleMatrix(this.implementor[rowIndex, columnIndexes]);

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
                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

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
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample10.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[IndexCollection rowIndexes, int columnIndex]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                var subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndex]);

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
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                this.implementor[rowIndexes, columnIndex] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[IndexCollection,IndexCollection]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample11.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[IndexCollection rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                DoubleMatrix subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                this.implementor[rowIndexes, columnIndexes] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[IndexCollection,string]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample12.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[IndexCollection rowIndexes, string columnIndexes]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                DoubleMatrix subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

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
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample20.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[string rowIndexes, int columnIndex]
        {
            get
            {
                if (rowIndexes is null)
                    throw new ArgumentNullException(nameof(rowIndexes));

                // Check if rowIndexes is a string reserved for sub-reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndex]);

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
                if (rowIndexes is null)
                    throw new ArgumentNullException(nameof(rowIndexes));

                // Check if rowIndexes is a string reserved for sub-reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                this.implementor[rowIndexes, columnIndex] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[string,IndexCollection]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample21.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[string rowIndexes, IndexCollection columnIndexes]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                // Check if rowIndexes is a string reserved for sub reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                DoubleMatrix subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                // Check if rowIndexes is a string reserved for sub-reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                this.implementor[rowIndexes, columnIndexes] = value.implementor;
            }
        }

        /// <inheritdoc cref="ITabularCollection{TValue,TCollection}.this[string,string]"/>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample22.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[string rowIndexes, string columnIndexes]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

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

                DoubleMatrix subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));


                if (value is null)
                    throw new ArgumentNullException(nameof(value));

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

        #region View

        /// <summary>
        /// Gets the elements of this instance corresponding to the specified row
        /// and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get.</param>
        /// <param name="avoidDenseAllocations">
        /// If set to <b>true</b> signals that the returned matrix 
        /// will not store the elements to get
        /// applying the <see cref="StorageScheme.Dense">Dense</see> storage scheme.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.3']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.4']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the number of rows of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains an index 
        /// which is greater than or equal to the number of columns of this instance.
        /// </exception>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample11.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[IndexCollection rowIndexes, IndexCollection columnIndexes, bool avoidDenseAllocations]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                DoubleMatrix subMatrix;

                if (avoidDenseAllocations
                    && this.implementor.StorageScheme == StorageScheme.Dense)
                {
                    if (rowIndexes.maxIndex >= this.implementor.NumberOfRows)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(rowIndexes),
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                    }
                    if (columnIndexes.maxIndex >= this.implementor.NumberOfColumns)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(columnIndexes),
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                    }
                    IndexCollection[] parentIndexes = new IndexCollection[2] { rowIndexes, columnIndexes };
                    var subDoubleMatrixImplementor = new ViewDoubleMatrixImplementor(
                        parentIndexes,
                        this.implementor);
                    subMatrix = new DoubleMatrix(subDoubleMatrixImplementor);
                }
                else
                    subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
        }

        /// <summary>
        /// Gets the elements of this instance corresponding to 
        /// the specified row and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get.
        /// The value must be <c>":"</c>, which means that all valid column indexes
        /// are specified.</param>
        /// <param name="avoidDenseAllocations">
        /// If set to <b>true</b> signals that the returned matrix 
        /// will not store the elements to get
        /// applying the <see cref="StorageScheme.Dense">Dense</see> storage scheme.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.3']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.4']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains an index 
        /// which is greater than or equal to the number of rows of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved for 
        /// tabular collection sub-referencing.
        /// </exception>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        ///   <para>
        ///     <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample12.cs.txt" language="cs" />
        ///   </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[IndexCollection rowIndexes, string columnIndexes, bool avoidDenseAllocations]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                // Check if columnIndexes is a string reserved for sub-reference
                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                DoubleMatrix subMatrix;

                if (avoidDenseAllocations
                    && this.implementor.StorageScheme == StorageScheme.Dense)
                {
                    if (rowIndexes.maxIndex >= this.implementor.NumberOfRows)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(rowIndexes),
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                    }
                    var columns = IndexCollection.Range(0, this.implementor.NumberOfColumns - 1);
                    IndexCollection[] parentIndexes = new IndexCollection[2] { rowIndexes, columns };
                    var subDoubleMatrixImplementor = new ViewDoubleMatrixImplementor(
                        parentIndexes,
                        this.implementor);
                    subMatrix = new DoubleMatrix(subDoubleMatrixImplementor);
                }
                else
                    subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
        }

        /// <summary>
        /// Gets the elements of this instance corresponding to 
        /// the specified row and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get.
        /// The value must be <c>":"</c>, which means that all valid row indexes
        /// are specified.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get.</param>
        /// <param name="avoidDenseAllocations">
        /// If set to <b>true</b> signals that the returned matrix 
        /// will not store the elements to get
        /// applying the <see cref="StorageScheme.Dense">Dense</see> storage scheme.</param>
        /// <value>
        /// A tabular collection formed by the entries corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.3']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.4']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> is not a string reserved 
        /// for tabular collection sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains an index 
        /// which is greater than or equal to the 
        /// number of columns of this instance.
        /// </exception>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample21.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1023:IndexersShouldNotBeMultidimensional",
            Justification = "Matrix indexers must be bi-dimensional.")]
        public DoubleMatrix this[string rowIndexes, IndexCollection columnIndexes, bool avoidDenseAllocations]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

                // Check if rowIndexes is a string reserved for sub reference
                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                DoubleMatrix subMatrix;

                if (avoidDenseAllocations
                    && this.implementor.StorageScheme == StorageScheme.Dense)
                {
                    if (columnIndexes.maxIndex >= this.implementor.NumberOfColumns)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(columnIndexes),
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                    }
                    IndexCollection rows = IndexCollection.Range(0, this.implementor.NumberOfRows - 1);
                    IndexCollection[] parentIndexes = new IndexCollection[2] { rows, columnIndexes };
                    var subDoubleMatrixImplementor = new ViewDoubleMatrixImplementor(parentIndexes, this.implementor);
                    subMatrix = new DoubleMatrix(subDoubleMatrixImplementor);
                }
                else
                    subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
        }

        /// <summary>
        /// Gets the elements of this instance corresponding to 
        /// the specified row and column indexes.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based row indexes of the elements to get.
        /// The value must be <c>":"</c>, which means that all valid row indexes
        /// are specified.</param>
        /// <param name="columnIndexes">
        /// The zero-based column indexes of the elements to get.
        /// The value must be <c>":"</c>, which means that all valid column indexes
        /// are specified.</param>
        /// <param name="avoidDenseAllocations">
        /// If set to <b>true</b> signals that the returned matrix 
        /// will not store the elements to get
        /// applying the <see cref="StorageScheme.Dense">Dense</see> storage scheme.</param>
        /// <value>
        /// A tabular collection formed by the elements corresponding to the 
        /// specified row and column indexes.</value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.3']"/>
        /// <inheritdoc cref="DoubleMatrix" path="para[@id='MinimizeMemoryUsage.4']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> is not a string reserved 
        /// for tabular collection sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved 
        /// for tabular collection sub-referencing.
        /// </exception>
        /// <example>
        /// <para>
        /// In the following example, some matrix elements are simultaneously accessed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MatrixIndexerExample22.cs.txt" 
        /// language="cs" />
        /// </para>
        /// <para>
        /// In the following example, a dense matrix is accessed while minimizing 
        /// memory usage.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MinimizeMemoryUsageExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// <para>
        /// In the following example, a dense matrix is accessed without 
        /// memory usage minimization.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MinimizeMemoryUsageExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        public DoubleMatrix this[string rowIndexes, string columnIndexes, bool avoidDenseAllocations]
        {
            get
            {
                if (null == rowIndexes)
                    throw new ArgumentNullException(nameof(rowIndexes));

                if (null == columnIndexes)
                    throw new ArgumentNullException(nameof(columnIndexes));

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

                DoubleMatrix subMatrix;

                if (avoidDenseAllocations
                    && this.implementor.StorageScheme == StorageScheme.Dense)
                {
                    var rows = IndexCollection.Range(0, this.implementor.NumberOfRows - 1);
                    var columns = IndexCollection.Range(0, this.implementor.NumberOfColumns - 1);
                    var parentIndexes = new IndexCollection[2] { rows, columns };
                    var subDoubleMatrixImplementor = new ViewDoubleMatrixImplementor(parentIndexes, this.implementor);
                    subMatrix = new DoubleMatrix(subDoubleMatrixImplementor);
                }
                else
                    subMatrix = new DoubleMatrix(this.implementor[rowIndexes, columnIndexes]);

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
        }

        #endregion

        #endregion

        #region Vectorization

        /// <summary>
        /// Returns the vectorization of this instance.
        /// </summary>
        /// <returns>A column vector obtained by stacking the columns of this instance.</returns>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='linear.indexing']"/>
        /// <para>
        /// This method, when called on a <see cref="DoubleMatrix"/> instance
        /// representing matrix <latex>A</latex>, returns a new 
        /// <see cref="DoubleMatrix"/> instance that 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\VecExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <seealso href="https://en.wikipedia.org/wiki/Vectorization_(mathematics)"/>
        public DoubleMatrix Vec()
        {
            return new DoubleMatrix(this.implementor[":"]);
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
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='linear.indexing']"/>
        /// <para>
        /// Let <latex>\round{l_0,\dots,l_{K-1}}</latex> the collection of 
        /// indexes represented by <paramref name="linearIndexes"/>.
        /// This method, when called on a <see cref="DoubleMatrix"/> instance
        /// representing matrix <latex>A</latex>, returns a new 
        /// <see cref="DoubleMatrix"/> instance that 
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
        /// <code source="..\Novacta.Analytics.CodeExamples\VecExample1.cs.txt" 
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
        public DoubleMatrix Vec(IndexCollection linearIndexes)
        {
            if (linearIndexes is null)
                throw new ArgumentNullException(nameof(linearIndexes));

            return new DoubleMatrix(this.implementor[linearIndexes]);
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
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='linear.indexing']"/>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix element is accessed using its linear
        /// index.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\LinearIndexerExample0.cs.txt" 
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
        public double this[int linearIndex]
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

        private static DenseMatrixImplementor<Func<MatrixImplementor<double>, double, int>> IndexOfOperators()
        {
            var operators = new DenseMatrixImplementor<Func<MatrixImplementor<double>, double, int>>(numberOfStorageSchemes, 1);

            operators[dense] = DoubleMatrixOperators.Dense_IndexOf;
            operators[view] = DoubleMatrixOperators.View_IndexOf;
            operators[sparse] = DoubleMatrixOperators.Sparse_IndexOf;

            return operators;
        }
        private static readonly DenseMatrixImplementor<Func<MatrixImplementor<double>, double, int>> _IndexOfOperators = IndexOfOperators();

        /// <inheritdoc/>
        public int IndexOf(double item)
        {
            return _IndexOfOperators[(int)this.implementor.StorageScheme](this.implementor, item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"></see> at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into 
        /// the <see cref="IList{T}"></see>.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<double>.Insert(int index, double item)
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
        void IList<double>.RemoveAt(int index)
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
        void ICollection<double>.Add(double item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<double>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(double item)
        {
            return -1 != this.IndexOf(item);
        }

        private static DenseMatrixImplementor<Action<MatrixImplementor<double>, double[], int>> CopyToOperators()
        {
            var operators = new DenseMatrixImplementor<Action<MatrixImplementor<double>, double[], int>>(numberOfStorageSchemes, 1);

            operators[dense] = DoubleMatrixOperators.Dense_CopyTo;
            operators[view] = DoubleMatrixOperators.View_CopyTo;
            operators[sparse] = DoubleMatrixOperators.Sparse_CopyTo;

            return operators;
        }
        private static readonly DenseMatrixImplementor<Action<MatrixImplementor<double>, double[], int>> _CopyToOperators = CopyToOperators();


        /// <inheritdoc/>
        public void CopyTo(double[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

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

            _CopyToOperators[(int)this.implementor.StorageScheme](this.implementor, array, arrayIndex);
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
        bool ICollection<double>.Remove(double item)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}

