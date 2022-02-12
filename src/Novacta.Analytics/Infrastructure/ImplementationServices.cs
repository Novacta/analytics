// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Provides helper methods for the implementation of types.
    /// </summary>
    internal static class ImplementationServices
    {
        #region Arrays 

        /// <summary>
        /// Converts an array of doubles into
        /// an equivalent one having <see cref="Complex"/> entries.
        /// </summary>
        /// <param name="doubleArray">
        /// The array of double to convert.
        /// </param>
        /// <returns>
        /// The array of <see cref="Complex"/> values equivalent
        /// to <paramref name="doubleArray"/>.
        /// </returns>
        internal static Complex[] ToComplexArray(
            double[] doubleArray)
        {
            Debug.Assert(doubleArray is not null);

            if (doubleArray.Length == 0)
            {
                return Array.Empty<Complex>();
            }

            Complex[] complexArray = new Complex[doubleArray.Length];

            unsafe
            {
                fixed (void* complexArrayPtr = complexArray)
                {
                    var unfixedComplexArrayPtr = (double*)complexArrayPtr;
                    for (int i = 0; i < doubleArray.Length; i++, unfixedComplexArrayPtr += 2)
                    {
                        *unfixedComplexArrayPtr = doubleArray[i];
                    }
                }
            }

            return complexArray;
        }

        /// <summary>
        /// Converts the RowMajor ordered storage of a matrix to its 
        /// ColMajor ordered equivalent.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="storage">
        /// On input, the RowMajor ordered matrix storage. On output, its
        /// ColMajor equivalent.
        /// </param>
        internal static void ConvertStorageToColMajorOrdered(
            int numberOfRows,
            int numberOfColumns,
            double[] storage)
        {
            /* We have a RowMajor representation of a given matrix, say A,
               having size m x n. 
               Let us define such matrix as the triple (m,n,row(A)).

               Such information is passed by setting the parameters as follows:
               numberOfRows = m
               numberOfColumns = n
               storage = row(A)

               We want a colMajor representation of A.
               We know that, given the matrix representations (m,n,row(A)) and 
               (n,m,col(A')), then - as vectors - , row(A) is equivalent to col(A').
               For example, let us consider matrix (2,3,row(1,2,3,4,5,6)), i.e.

               A = [ 1 2 3
                     4 5 6 ]

               Then A' is [ 1 4
                            2 5
                            3 6 ]

               which is ColMajor represented as (3,2,col(1,2,3,4,5,6)).

               This implies that, in order to obtain a ColMajor representation of A,
               we can start with (n,m,col(A')) and hence compute its inverse with
               Array_InPlaceTranspose
            */
            Debug.Assert(storage is not null);
            Debug.Assert(storage.Length == (numberOfRows * numberOfColumns));

            DoubleMatrixOperators.ArrayInPlaceTranspose(
                numberOfColumns,
                numberOfRows,
                storage);
        }

        /// <summary>
        /// Converts the RowMajor ordered storage of a matrix to its 
        /// ColMajor ordered equivalent.
        /// </summary>
        /// <param name="numberOfRows">The number of matrix rows.</param>
        /// <param name="numberOfColumns">The number of matrix columns.</param>
        /// <param name="storage">
        /// On input, the RowMajor ordered matrix storage. On output, its
        /// ColMajor equivalent.
        /// </param>
        internal static void ConvertStorageToColMajorOrdered(
            int numberOfRows,
            int numberOfColumns,
            Complex[] storage)
        {
            /* We have a RowMajor representation of a given matrix, say A,
               having size m x n. 
               Let us define such matrix as the triple (m,n,row(A)).

               Such information is passed by setting the parameters as follows:
               numberOfRows = m
               numberOfColumns = n
               storage = row(A)

               We want a colMajor representation of A.
               We know that, given the matrix representations (m,n,row(A)) and 
               (n,m,col(A')), then - as vectors - , row(A) is equivalent to col(A').
               For example, let us consider matrix (2,3,row(1,2,3,4,5,6)), i.e.

               A = [ 1 2 3
                     4 5 6 ]

               Then A' is [ 1 4
                            2 5
                            3 6 ]

               which is ColMajor represented as (3,2,col(1,2,3,4,5,6)).

               This implies that, in order to obtain a ColMajor representation of A,
               we can start with (n,m,col(A')) and hence compute its inverse with
               Array_InPlaceTranspose
            */
            Debug.Assert(storage is not null);
            Debug.Assert(storage.Length == (numberOfRows * numberOfColumns));

            ComplexMatrixOperators.ArrayInPlaceTranspose(
                numberOfColumns,
                numberOfRows,
                storage);
        }

        #endregion

        #region Names

        #region DoubleMatrix

        /// <summary>
        /// Tries to set the row names of a matrix, 
        /// getting the name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs, given the specified 
        /// paired indexes.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndexes">
        /// The indexes eventually paired to the required names.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are values paired with 
        /// keys represented as indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th row of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndexes[i]]</c>
        /// if <c>pairedIndexes[i]</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixRowNames(
            DoubleMatrix matrix,
            IndexCollection pairedIndexes,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.rowNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(pairedIndexes is not null);
            Debug.Assert(pairedIndexes.Count == matrix.NumberOfRows);

            Dictionary<int, string> matrixRowNames = null;
            bool isAddingFirstMatrixRowName = true;
            for (int i = 0; i < pairedIndexes.Count; i++)
            {
                if (indexNamePairs.TryGetValue(pairedIndexes[i], out string name))
                {
                    if (isAddingFirstMatrixRowName)
                    {
                        matrixRowNames = new Dictionary<int, string>();
                        isAddingFirstMatrixRowName = false;
                    }
                    matrixRowNames[i] = name;
                }
            }

            matrix.rowNames = matrixRowNames;
        }

        /// <summary>
        /// Sets the row names of a matrix,
        /// getting each name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th row of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[k]</c>
        /// where <c>k</c> is the <i>i</i>-th enumerated key in
        /// <see cref="Dictionary{TKey, TValue}.Keys"/> of 
        /// <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void SetMatrixRowNames(
            DoubleMatrix matrix,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.rowNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(indexNamePairs.Keys.Count() == matrix.NumberOfRows);

            int numberOfNames = indexNamePairs.Count;
            var matrixRowNames = new Dictionary<int, string>(numberOfNames);

            foreach (var key in indexNamePairs.Keys)
            {
                matrixRowNames[key] = indexNamePairs[key];
            }

            matrix.rowNames = matrixRowNames;
        }

        /// <summary>
        /// Tries to set the name of the first row of a matrix, 
        /// getting the name corresponding to
        /// a paired index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndex">
        /// The index eventually paired to the required name.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the first row of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndex]</c>
        /// if <c>pairedIndex</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixRowName(
            DoubleMatrix matrix,
            int pairedIndex,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.rowNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(matrix.NumberOfRows == 1);

            Dictionary<int, string> matrixRowNames;

            if (indexNamePairs.TryGetValue(pairedIndex, out string name))
            {
                matrixRowNames = new Dictionary<int, string>
                {
                    [0] = name
                };
                matrix.rowNames = matrixRowNames;
            }
        }

        /// <summary>
        /// Tries to set the column names of a matrix, 
        /// getting the name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs, given the specified 
        /// paired indexes.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndexes">
        /// The indexes eventually paired to the required names.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are values paired with 
        /// keys represented as indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th column of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndexes[i]]</c>
        /// if <c>pairedIndexes[i]</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixColumnNames(
            DoubleMatrix matrix,
            IndexCollection pairedIndexes,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.columnNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(pairedIndexes is not null);
            Debug.Assert(pairedIndexes.Count == matrix.NumberOfColumns);

            Dictionary<int, string> matrixColumnNames = null;
            bool isAddingFirstMatrixColumnName = true;
            for (int i = 0; i < pairedIndexes.Count; i++)
            {
                if (indexNamePairs.TryGetValue(pairedIndexes[i], out string name))
                {
                    if (isAddingFirstMatrixColumnName)
                    {
                        matrixColumnNames = new Dictionary<int, string>();
                        isAddingFirstMatrixColumnName = false;
                    }
                    matrixColumnNames[i] = name;
                }
            }

            matrix.columnNames = matrixColumnNames;
        }

        /// <summary>
        /// Sets the column names of a matrix,
        /// getting each name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th column of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[k]</c>
        /// where <c>k</c> is the <i>i</i>-th enumerated key in
        /// <see cref="Dictionary{TKey, TValue}.Keys"/> of 
        /// <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void SetMatrixColumnNames(
            DoubleMatrix matrix,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.columnNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(indexNamePairs.Keys.Count() == matrix.NumberOfColumns);

            int numberOfNames = indexNamePairs.Count;
            var matrixColumnNames = new Dictionary<int, string>(numberOfNames);

            foreach (var key in indexNamePairs.Keys)
            {
                matrixColumnNames[key] = indexNamePairs[key];
            }

            matrix.columnNames = matrixColumnNames;
        }

        /// <summary>
        /// Tries to set the name of the first column of a matrix, 
        /// getting the name corresponding to
        /// a paired index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndex">
        /// The index eventually paired to the required name.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the first column of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndex]</c>
        /// if <c>pairedIndex</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixColumnName(
            DoubleMatrix matrix,
            int pairedIndex,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.columnNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(matrix.NumberOfColumns == 1);

            Dictionary<int, string> matrixColumnNames;

            if (indexNamePairs.TryGetValue(pairedIndex, out string name))
            {
                matrixColumnNames = new Dictionary<int, string>
                {
                    [0] = name
                };
                matrix.columnNames = matrixColumnNames;
            }
        }

        #endregion

        #region ComplexMatrix

        /// <summary>
        /// Tries to set the row names of a matrix, 
        /// getting the name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs, given the specified 
        /// paired indexes.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndexes">
        /// The indexes eventually paired to the required names.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are values paired with 
        /// keys represented as indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th row of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndexes[i]]</c>
        /// if <c>pairedIndexes[i]</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixRowNames(
            ComplexMatrix matrix,
            IndexCollection pairedIndexes,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.rowNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(pairedIndexes is not null);
            Debug.Assert(pairedIndexes.Count == matrix.NumberOfRows);

            Dictionary<int, string> matrixRowNames = null;
            bool isAddingFirstMatrixRowName = true;
            for (int i = 0; i < pairedIndexes.Count; i++)
            {
                if (indexNamePairs.TryGetValue(pairedIndexes[i], out string name))
                {
                    if (isAddingFirstMatrixRowName)
                    {
                        matrixRowNames = new Dictionary<int, string>();
                        isAddingFirstMatrixRowName = false;
                    }
                    matrixRowNames[i] = name;
                }
            }

            matrix.rowNames = matrixRowNames;
        }

        /// <summary>
        /// Sets the row names of a matrix,
        /// getting each name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th row of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[k]</c>
        /// where <c>k</c> is the <i>i</i>-th enumerated key in
        /// <see cref="Dictionary{TKey, TValue}.Keys"/> of 
        /// <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void SetMatrixRowNames(
            ComplexMatrix matrix,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.rowNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(indexNamePairs.Keys.Count() == matrix.NumberOfRows);

            int numberOfNames = indexNamePairs.Count;
            var matrixRowNames = new Dictionary<int, string>(numberOfNames);

            foreach (var key in indexNamePairs.Keys)
            {
                matrixRowNames[key] = indexNamePairs[key];
            }

            matrix.rowNames = matrixRowNames;
        }

        /// <summary>
        /// Tries to set the name of the first row of a matrix, 
        /// getting the name corresponding to
        /// a paired index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndex">
        /// The index eventually paired to the required name.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the first row of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndex]</c>
        /// if <c>pairedIndex</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixRowName(
            ComplexMatrix matrix,
            int pairedIndex,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.rowNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(matrix.NumberOfRows == 1);

            Dictionary<int, string> matrixRowNames;

            if (indexNamePairs.TryGetValue(pairedIndex, out string name))
            {
                matrixRowNames = new Dictionary<int, string>
                {
                    [0] = name
                };
                matrix.rowNames = matrixRowNames;
            }
        }

        /// <summary>
        /// Tries to set the column names of a matrix, 
        /// getting the name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs, given the specified 
        /// paired indexes.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndexes">
        /// The indexes eventually paired to the required names.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are values paired with 
        /// keys represented as indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th column of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndexes[i]]</c>
        /// if <c>pairedIndexes[i]</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixColumnNames(
            ComplexMatrix matrix,
            IndexCollection pairedIndexes,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.columnNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(pairedIndexes is not null);
            Debug.Assert(pairedIndexes.Count == matrix.NumberOfColumns);

            Dictionary<int, string> matrixColumnNames = null;
            bool isAddingFirstMatrixColumnName = true;
            for (int i = 0; i < pairedIndexes.Count; i++)
            {
                if (indexNamePairs.TryGetValue(pairedIndexes[i], out string name))
                {
                    if (isAddingFirstMatrixColumnName)
                    {
                        matrixColumnNames = new Dictionary<int, string>();
                        isAddingFirstMatrixColumnName = false;
                    }
                    matrixColumnNames[i] = name;
                }
            }

            matrix.columnNames = matrixColumnNames;
        }

        /// <summary>
        /// Sets the column names of a matrix,
        /// getting each name corresponding to
        /// an index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the <i>i</i>-th column of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[k]</c>
        /// where <c>k</c> is the <i>i</i>-th enumerated key in
        /// <see cref="Dictionary{TKey, TValue}.Keys"/> of 
        /// <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void SetMatrixColumnNames(
            ComplexMatrix matrix,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.columnNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(indexNamePairs.Keys.Count() == matrix.NumberOfColumns);

            int numberOfNames = indexNamePairs.Count;
            var matrixColumnNames = new Dictionary<int, string>(numberOfNames);

            foreach (var key in indexNamePairs.Keys)
            {
                matrixColumnNames[key] = indexNamePairs[key];
            }

            matrix.columnNames = matrixColumnNames;
        }

        /// <summary>
        /// Tries to set the name of the first column of a matrix, 
        /// getting the name corresponding to
        /// a paired index from the specified 
        /// collection
        /// of <see cref="Int32"/> and <see cref="String"/>
        /// pairs.
        /// </summary>
        /// <param name="matrix">The matrix to set.</param>
        /// <param name="pairedIndex">
        /// The index eventually paired to the required name.
        /// </param>
        /// <param name="indexNamePairs">
        /// The collection in which names are paired with 
        /// indexes.
        /// </param>
        /// <remarks>
        /// <para>
        /// The name of the first column of <paramref name="matrix"/>
        /// is set to <paramref name="indexNamePairs"/><c>[pairedIndex]</c>
        /// if <c>pairedIndex</c> is a key in <paramref name="indexNamePairs"/>.
        /// </para>
        /// </remarks>
        internal static void TrySetMatrixColumnName(
            ComplexMatrix matrix,
            int pairedIndex,
            IReadOnlyDictionary<int, string> indexNamePairs)
        {
            Debug.Assert(matrix is not null);
            Debug.Assert(matrix.columnNames is null);
            Debug.Assert(indexNamePairs is not null);
            Debug.Assert(matrix.NumberOfColumns == 1);

            Dictionary<int, string> matrixColumnNames;

            if (indexNamePairs.TryGetValue(pairedIndex, out string name))
            {
                matrixColumnNames = new Dictionary<int, string>
                {
                    [0] = name
                };
                matrix.columnNames = matrixColumnNames;
            }
        }

        #endregion

        #endregion

        #region Exceptions

        private static readonly ResourceManager resourceManager
            = new(
                "Novacta.Analytics.Properties.Resources",
                    typeof(DoubleMatrix).GetTypeInfo().Assembly);

        /// <summary>
        /// Gets the value of the string resource localized for 
        /// the current user interface culture.
        /// </summary>
        /// <param name="name">The name of the resource to retrieve.</param>
        /// <returns>
        /// The value of the string resource localized for 
        /// the current user interface culture.
        /// </returns>
        internal static string GetResourceString(string name)
        {
            return resourceManager.GetString(name, CultureInfo.CurrentUICulture);
        }

        internal static void ThrowOnMismatchedMatrixDimensions<T>(
            int expectedNumberOfRows,
            int expectedNumberOfColumns,
            MatrixImplementor<T> value)
        {
            if ((expectedNumberOfRows != value.NumberOfRows)
                || (expectedNumberOfColumns != value.NumberOfColumns))
            {
                throw new ArgumentException(GetResourceString(
                    "STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT"), nameof(value));
            }
        }

        internal static void ThrowOnNullOrWhiteSpace(
            string parameterValue,
            string parameterName)
        {
            if (null == parameterValue)
            {
                throw new ArgumentNullException(parameterName);
            }

            bool isEmptyOrWhiteSpace = true;
            for (int i = 0; i < parameterValue.Length; i++)
            {
                if (!Char.IsWhiteSpace(parameterValue[i]))
                {
                    isEmptyOrWhiteSpace = false;
                    break;
                }
            }

            if (isEmptyOrWhiteSpace)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"));
            }
        }

        #endregion
    }
}
