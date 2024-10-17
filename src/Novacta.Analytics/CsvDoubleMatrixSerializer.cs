// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text;
using System.Globalization;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Serializes and deserializes matrix objects into and from CSV documents. 
    /// </summary>
    public static class CsvDoubleMatrixSerializer
    {
        private const char delimiter = ',';
        private static readonly NumberFormatInfo numberFormatInfo = new();

        #region Serialize

        private static void SerializeSparseCsr3Matrix(
            TextWriter writer,
            DoubleMatrix matrix)
        {
            StringBuilder stringBuilder = new();

            var implementor = (SparseCsr3DoubleMatrixImplementor)
                matrix.implementor;

            #region Matrix instantiation

            stringBuilder.Append("Sparse|Double");
            stringBuilder.Append(delimiter);

            stringBuilder.Append(matrix.NumberOfRows.ToString(numberFormatInfo));
            stringBuilder.Append(delimiter);
            stringBuilder.Append(matrix.NumberOfColumns.ToString(numberFormatInfo));
            stringBuilder.Append(delimiter);

            if (null != matrix.Name)
                stringBuilder.Append(matrix.Name);

            stringBuilder.Append(delimiter);

            stringBuilder.Append(implementor.rowIndex[implementor.NumberOfRows].ToString(numberFormatInfo));

            writer.WriteLine(stringBuilder.ToString());
            writer.Flush();
            stringBuilder.Clear();

            #endregion

            #region Data

            int numberOfStoredPositions = implementor.rowIndex[implementor.numberOfRows];
            if (numberOfStoredPositions != 0)
            {
                int[] rowIndex = implementor.rowIndex;
                int[] columns = implementor.columns;
                double[] values = implementor.values;

                for (int i = 0; i < implementor.numberOfRows; i++)
                {
                    for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++)
                    {
                        stringBuilder.Append(i.ToString(numberFormatInfo));
                        stringBuilder.Append(delimiter);
                        stringBuilder.Append(columns[p].ToString(numberFormatInfo));
                        stringBuilder.Append(delimiter);
                        stringBuilder.Append(values[p].ToString(numberFormatInfo));
                        writer.WriteLine(stringBuilder.ToString());
                        writer.Flush();
                        stringBuilder.Clear();
                    }
                }
            }

            #endregion

            #region Column names

            if (matrix.HasColumnNames)
            {
                foreach (var item in matrix.ColumnNames)
                {
                    stringBuilder.Append(item.Key);
                    stringBuilder.Append(delimiter);

                    stringBuilder.Append(item.Value);
                    stringBuilder.Append(delimiter);
                }
                // The following lines are due to the behavior of string.Split, 
                // as explained below.
                // "Each element of separator defines a separate delimiter character. 
                // If two delimiters are adjacent, or a delimiter is found at the 
                // beginning or end of this instance, 
                // the corresponding array element contains Empty." 
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

                writer.WriteLine(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            else
            {
                writer.WriteLine();
            }

            writer.Flush();

            #endregion

            #region Row names

            if (matrix.HasRowNames)
            {
                foreach (var item in matrix.RowNames)
                {
                    stringBuilder.Append(item.Key);
                    stringBuilder.Append(delimiter);

                    stringBuilder.Append(item.Value);
                    stringBuilder.Append(delimiter);
                }
                // The following lines are due to the behavior of string.Split, 
                // as explained below.
                // "Each element of separator defines a separate delimiter character. 
                // If two delimiters are adjacent, or a delimiter is found at the 
                // beginning or end of this instance, 
                // the corresponding array element contains Empty." 
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

                writer.WriteLine(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            else
            {
                writer.WriteLine();
            }

            writer.Flush();

            #endregion
        }

        private static void SerializeNonSparseMatrix(
            TextWriter writer,
            DoubleMatrix matrix)
        {
            StringBuilder stringBuilder = new();

            #region First line

            // StorageScheme, NumberOfRows, NumberOfColumns, MatrixName

            stringBuilder.Append("Dense|Double");
            stringBuilder.Append(delimiter);

            stringBuilder.Append(matrix.NumberOfRows.ToString(numberFormatInfo));
            stringBuilder.Append(delimiter);
            stringBuilder.Append(matrix.NumberOfColumns.ToString(numberFormatInfo));
            stringBuilder.Append(delimiter);

            if (null != matrix.Name)
                stringBuilder.Append(matrix.Name);

            stringBuilder.Append(delimiter);

            writer.WriteLine(stringBuilder.ToString());
            writer.Flush();
            stringBuilder.Clear();

            #endregion

            #region Column names

            // The first cell is always empty
            stringBuilder.Append(delimiter);

            string name;
            if (matrix.columnNames != null)
            {
                for (int j = 0; j < matrix.NumberOfColumns; j++)
                {

                    if (matrix.TryGetColumnName(j, out name))
                        stringBuilder.Append(name);

                    stringBuilder.Append(delimiter);
                }
            }
            else
            {
                for (int j = 0; j < matrix.NumberOfColumns; j++)
                {
                    stringBuilder.Append(delimiter);
                }
            }

            // The following two lines are due to the behavior of string.Split, 
            // as explained below.
            // "Each element of separator defines a separate delimiter character. 
            // If two delimiters are adjacent, or a delimiter is found at the 
            // beginning or end of this instance, 
            // the corresponding array element contains Empty." 
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            writer.WriteLine(stringBuilder.ToString());
            writer.Flush();
            stringBuilder.Clear();

            #endregion

            #region RowNames and data

            bool rowNamesExist = matrix.HasRowNames;

            for (int i = 0; i < matrix.NumberOfRows; i++)
            {
                if (rowNamesExist)
                {
                    if (matrix.TryGetRowName(i, out name))
                        stringBuilder.Append(name);
                }

                stringBuilder.Append(delimiter);

                for (int j = 0; j < matrix.NumberOfColumns; j++)
                {
                    stringBuilder.Append(matrix[i, j].ToString(numberFormatInfo));
                    stringBuilder.Append(delimiter);
                }
                writer.WriteLine(stringBuilder.ToString());
                writer.Flush();
                stringBuilder.Clear();
            }

            #endregion
        }

        /// <summary>
        /// Serializes the specified <see cref="DoubleMatrix"/> writing a CSV document
        /// to a file using the specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> used to 
        /// write the CSV document.</param>
        /// <param name="matrix">The <see cref="DoubleMatrix"/> being serialized.</param>
        /// <remarks>
        /// <para>
        /// This method writes a CSV document containing the information
        /// required to represent the state of <paramref name="matrix"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix instance is serialized by writing 
        /// a CSV document to a stream. Hence the matrix is deserialized by reading 
        /// such stream.
        /// </para>
        /// <para>
        /// <code title="Serialization and deserialization of a matrix using a CSV document"
        /// source="..\Novacta.Analytics.CodeExamples\CsvSerializeExample1.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer"/> is <b>null</b>.<br/>
        /// -or- <br/>
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        public static void Serialize(
            TextWriter writer,
            DoubleMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(writer);

            ArgumentNullException.ThrowIfNull(matrix);

            switch (matrix.StorageScheme)
            {
                case StorageScheme.CompressedRow:
                    SerializeSparseCsr3Matrix(writer, matrix);
                    break;
                default:
                    SerializeNonSparseMatrix(writer, matrix);
                    break;
            }
        }

        /// <summary>
        /// Serializes the specified <see cref="DoubleMatrix"/> writing a CSV document
        /// to the specified file.
        /// </summary>
        /// <param name="path">The CSV file to be opened for
        /// serializing.</param>
        /// <param name="matrix">The <see cref="DoubleMatrix"/> being serialized.</param>
        /// <remarks>
        /// <para>
        /// This method writes a CSV document containing the information
        /// required to represent the state of <paramref name="matrix"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <b>null</b>.<br/>
        /// -or- <br/>
        /// <paramref name="matrix"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during serialization. The original exception is available 
        /// using the <see cref="Exception.InnerException"/> property.
        ///</exception>
        public static void Serialize(string path, DoubleMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(path);

            ArgumentNullException.ThrowIfNull(matrix);

            try
            {
                using StreamWriter writer =
                    new(path, false,
                    new UTF8Encoding(false));
                Serialize(
                     writer,
                     matrix);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_SERIALIZE"), e);
            }
        }

        /// <summary>
        /// Serializes the specified <see cref="ReadOnlyDoubleMatrix"/> writing a CSV document
        /// to the specified file.
        /// </summary>
        /// <param name="path">The CSV file to be opened for
        /// serializing.</param>
        /// <param name="matrix">The <see cref="ReadOnlyDoubleMatrix"/> being serialized.</param>
        /// <inheritdoc cref="Serialize(string, DoubleMatrix)"/>
        public static void Serialize(string path, ReadOnlyDoubleMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            Serialize(path, matrix.matrix);
        }

        /// <summary>
        /// Serializes the specified <see cref="ReadOnlyDoubleMatrix"/> writing a CSV document
        /// to a file using the specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> used to 
        /// write the CSV document.</param>
        /// <param name="matrix">The <see cref="ReadOnlyDoubleMatrix"/> being serialized.</param>
        /// <inheritdoc cref="Serialize(TextWriter, DoubleMatrix)"/>
        public static void Serialize(TextWriter writer, ReadOnlyDoubleMatrix matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            Serialize(writer, matrix.matrix);
        }

        #endregion

        #region Deserialize

        private static DoubleMatrix InstantiateMatrix(TextReader reader)
        {
            DoubleMatrix matrix;

            var line = reader.ReadLine();

            // Split line contents by delimiter.
            // In line 0, the first string represent the storage scheme
            // and the entry type.
            // Additional two strings must be convertible to integers 
            // representing the number of rows and columns, respectively. 
            // A subsequent string is used to possibly store the matrix name.
            // The last one is optional, and can be used to represent the 
            // number of nonzero entries in case of a sparse matrix.

            var tokens = line.Split(delimiter);

            int numberOfRows = Int32.Parse(tokens[1], numberFormatInfo);
            int numberOfColumns = Int32.Parse(tokens[2], numberFormatInfo);

            var subtokens = tokens[0].Split('|');

            string storageScheme = subtokens[0];
            
            if (subtokens.Length > 1)
            {
                string entryType = subtokens[1];
                string expectedEntryType = "Double";

                if (entryType != expectedEntryType)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_REP_DESERIALIZE_UNEXPECTED_ENTRY_TYPE"),
                            entryType,
                            expectedEntryType));
                }
            }

            switch (storageScheme)
            {
                case "Sparse":
                    {
                        int numberOfNonzeros = Int32.Parse(tokens[4], numberFormatInfo);
                        matrix = DoubleMatrix.Sparse(
                            numberOfRows,
                            numberOfColumns,
                            numberOfNonzeros);
                    }
                    break;
                default:
                    {
                        matrix = DoubleMatrix.Dense(
                            numberOfRows,
                            numberOfColumns);
                    }
                    break;
            }

            var matrixNameToken = tokens[3];
            matrix.Name = string.IsNullOrEmpty(matrixNameToken) ? null : matrixNameToken;

            return matrix;
        }

        private static void DeserializeMatrixFromSparseCsv(
            DoubleMatrix matrix,
            TextReader reader)
        {
            string line;

            #region Data

            var implementor = (SparseCsr3DoubleMatrixImplementor)matrix.implementor;

            int numberOfStoredPositions = implementor.values.Length;
            string[] tokens;
            if (numberOfStoredPositions != 0)
            {
                for (int l = 0; l < numberOfStoredPositions; l++)
                {
                    line = reader.ReadLine();
                    tokens = line.Split(delimiter);
                    int i = Convert.ToInt32(tokens[0], numberFormatInfo);
                    int j = Convert.ToInt32(tokens[1], numberFormatInfo);
                    double value = Convert.ToDouble(tokens[2], numberFormatInfo);
                    implementor.SetValue(i, j, value);
                }
            }

            #endregion

            #region Column names              

            line = reader.ReadLine();

            bool hasColumnNames = line is not null;

            if (hasColumnNames)
            {
                tokens = line.Split(delimiter);

                int numberOfColumnNames = tokens.Length / 2;

                int p = 0;
                for (int j = 0; j < numberOfColumnNames; j++, p += 2)
                {
                    int index = Convert.ToInt32(tokens[p], numberFormatInfo);
                    matrix.SetColumnName(index, tokens[p + 1]);
                }
            }

            #endregion

            #region Row names

            line = reader.ReadLine();

            bool hasRowNames = line is not null;

            if (hasRowNames)
            {
                tokens = line.Split(delimiter);

                int numberOfRowNames = tokens.Length / 2;

                int p = 0;
                for (int i = 0; i < numberOfRowNames; i++, p += 2)
                {
                    int index = Convert.ToInt32(tokens[p], numberFormatInfo);
                    matrix.SetRowName(index, tokens[p + 1]);
                }
            }

            #endregion
        }

        private static void DeserializeMatrixFromDenseCsv(
            DoubleMatrix matrix,
            TextReader reader)
        {
            string line;

            //
            // read line 1
            //
            line = reader.ReadLine();
            string[] tokens = line.Split(delimiter);

            // the first string is always empty in this line, 
            // so skip it by starting j from 1 
            // in the following loop
            for (int j = 1; j < tokens.Length; j++)
            {

                if (!string.IsNullOrWhiteSpace(tokens[j]))
                    matrix.SetColumnName(j - 1, tokens[j]);
            }

            // Read additional lines until the end of
            // the file is reached
            int i = 0;
            int numberOfColumns = matrix.NumberOfColumns;
            while ((line = reader.ReadLine()) != null)
            {
                tokens = line.Split(delimiter);

                // read the (eventual) row name
                if (!string.IsNullOrWhiteSpace(tokens[0]))
                    matrix.SetRowName(i, tokens[0]);

                for (int j = 0; j < numberOfColumns; j++)
                {
                    matrix[i, j] = Convert.ToDouble(tokens[j + 1], numberFormatInfo);
                }
                i++;
            }
        }

        private static void InternalDeserialize(
            DoubleMatrix matrix,
            TextReader reader)
        {
            switch (matrix.StorageScheme)
            {
                case StorageScheme.CompressedRow:
                    DeserializeMatrixFromSparseCsv(matrix, reader);
                    break;
                default:
                    DeserializeMatrixFromDenseCsv(matrix, reader);
                    break;
            }
        }

        /// <summary>
        /// Deserializes as <see cref="DoubleMatrix"/> the CSV document contained 
        /// by the specified <see cref="TextReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> that contains 
        /// the CSV document to deserialize.</param>
        /// <returns>The <see cref="DoubleMatrix"/> being deserialized.</returns>
        /// <remarks>
        /// <para>
        /// This method reads CSV files created by method
        /// <see cref="Serialize(TextWriter, DoubleMatrix)"/> or
        /// one of its overloaded versions.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a matrix instance is serialized by writing 
        /// a CSV document to a stream. Hence the matrix is deserialized by reading 
        /// such stream.
        /// </para>
        /// <para>
        /// <code title="Serialization and deserialization of a matrix using a CSV document"
        /// source="..\Novacta.Analytics.CodeExamples\CsvSerializeExample1.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during deserialization. The original exception is available 
        /// using the <see cref="Exception.InnerException"/> property.
        ///</exception>
        public static DoubleMatrix Deserialize(TextReader reader)
        {
            ArgumentNullException.ThrowIfNull(reader);

            DoubleMatrix matrix;
            try
            {
                matrix = InstantiateMatrix(reader);

                InternalDeserialize(matrix, reader);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"), e);
            }

            return matrix;
        }

        /// <summary>
        /// Deserializes as <see cref="DoubleMatrix"/> the specified CSV file.
        /// </summary>
        /// <param name="path">The CSV file to be opened for
        /// deserializing.</param>
        /// <returns>The <see cref="DoubleMatrix"/> being deserialized.</returns>
        /// <remarks>
        /// <para>
        /// This method reads CSV files created by  method
        /// <see cref="Serialize(string, DoubleMatrix)"/> or
        /// one of its overloaded versions.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during deserialization. The original exception is available 
        /// using the <see cref="Exception.InnerException"/> property.
        ///</exception>
        public static DoubleMatrix Deserialize(string path)
        {
            ArgumentNullException.ThrowIfNull(path);
            try
            {
                using StreamReader reader = new(path);

                var matrix = InstantiateMatrix(reader);

                InternalDeserialize(matrix, reader);

                return matrix;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"), e);
            }
        }

        #endregion
    }
}
