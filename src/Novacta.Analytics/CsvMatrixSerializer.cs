// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text;

using System.Globalization;
using Novacta.Analytics.Infrastructure;
using System.Numerics;

namespace Novacta.Analytics
{
    /// <summary>
    /// Serializes and deserializes matrix objects into and from CSV documents. 
    /// </summary>
    /// <remarks>
    /// <note type="caution">
    /// This class has been deprecated and its use is not recommended.
    /// Please use instead class
    /// <see cref="CsvDoubleMatrixSerializer"/>.
    /// </note>
    /// </remarks>
    /// <seealso cref="CsvComplexMatrixSerializer"/>
    [Obsolete(message: "Use instead class CsvDoubleMatrixSerializer.")]
    public static class CsvMatrixSerializer
    {
        #region Serialize

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
            CsvDoubleMatrixSerializer.Serialize(writer, matrix);
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
            CsvDoubleMatrixSerializer.Serialize(path, matrix);
        }

        /// <summary>
        /// Serializes the specified <see cref="ReadOnlyDoubleMatrix"/> writing a CSV document
        /// to the specified file.
        /// </summary>
        /// <param name="path">The CSV file to be opened for
        /// serializing.</param>
        /// <param name="matrix">The <see cref="ReadOnlyDoubleMatrix"/> being serialized.</param>
        /// <inheritdoc cref="CsvMatrixSerializer.Serialize(string, DoubleMatrix)"/>
        public static void Serialize(string path, ReadOnlyDoubleMatrix matrix)
        {
            CsvDoubleMatrixSerializer.Serialize(path, matrix);
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
            CsvDoubleMatrixSerializer.Serialize(writer, matrix);
        }

        #endregion

        #region Deserialize

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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during deserialization. The original exception is available 
        /// using the <see cref="Exception.InnerException"/> property.
        ///</exception>
        public static DoubleMatrix Deserialize(TextReader reader)
        {
            return CsvDoubleMatrixSerializer.Deserialize(reader);
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
            return CsvDoubleMatrixSerializer.Deserialize(path);
        }

        #endregion
    }
}
