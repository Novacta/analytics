﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Novacta.Analytics.Tests
{
    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class CsvDoubleMatrixSerializerTests
    {
        private readonly string EntryType = "double";

        #region Helpers

        /// <summary>
        /// Gets a matrix testable for CSV serialization.
        /// </summary>
        private TestableDoubleMatrix GetTestableMatrix(
            bool hasName,
            bool hasRowNames,
            bool hasColumnNames,
            out string partialPath)
        {
            StringBuilder stringBuilder = new();

            string matrixName = null;
            if (hasName)
            {
                matrixName = "MatrixName";
                stringBuilder.Append($"{EntryType}-named-");
            }
            else
            {
                stringBuilder.Append($"{EntryType}-unnamed-");
            }

            Dictionary<int, string> rowNames = null;
            if (hasRowNames)
            {
                rowNames = new Dictionary<int, string>() {
                    { 0, "Row0" },
                    { 1, "Row1" },
                    { 2, "Row2" },
                    { 3, "Row3" },
                };
                stringBuilder.Append("with-row-names-");
            }
            else
            {
                stringBuilder.Append("without-row-names-");
            }

            Dictionary<int, string> columnNames = null;
            if (hasColumnNames)
            {
                columnNames = new Dictionary<int, string>() {
                    { 0, "Column0" },
                    { 1, "Column1" },
                    { 2, "Column2" },
                    { 3, "Column3" },
                    { 4, "Column4" },
                };
                stringBuilder.Append("with-col-names");
            }
            else
            {
                stringBuilder.Append("without-col-names");
            }

            stringBuilder.Append(".csv");

            partialPath = stringBuilder.ToString();

            var testableMatrix = new TestableDoubleMatrix(
                asColumnMajorDenseArray: [
                                        0,  0, -2, 0,
                                        0,  0,  0, 0,
                                        0, -1,  0, 0,
                                        0,  0,  0, 0,
                                        0,  0,  0, 0
                ],
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 2,
                name: matrixName,
                rowNames: rowNames,
                columnNames: columnNames);

            return testableMatrix;
        }

        private static void TestSerializationToPath(
            TestableDoubleMatrix testableMatrix,
            string partialPath)
        {
            // dense
            {
                var expected = testableMatrix.AsDense;

                var path = "dense-" + partialPath;

                CsvDoubleMatrixSerializer.Serialize(path, expected);

                var actual = CsvDoubleMatrixSerializer.Deserialize(path);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            // sparse
            {
                var expected = testableMatrix.AsSparse;

                var path = "sparse-" + partialPath;

                CsvDoubleMatrixSerializer.Serialize(path, expected);

                var actual = CsvDoubleMatrixSerializer.Deserialize(path);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            // read-only dense
            {
                var expected = testableMatrix.AsDense;

                var path = "read-only-dense-" + partialPath;

                CsvDoubleMatrixSerializer.Serialize(path, expected.AsReadOnly());

                var actual = CsvDoubleMatrixSerializer.Deserialize(path);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            // read-only sparse
            {
                var expected = testableMatrix.AsSparse;

                var path = "read-only-sparse-" + partialPath;

                CsvDoubleMatrixSerializer.Serialize(path, expected.AsReadOnly());

                var actual = CsvDoubleMatrixSerializer.Deserialize(path);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        private static void TestSerializationToStream(
            TestableDoubleMatrix testableMatrix)
        {
            // dense
            {
                var expected = testableMatrix.AsDense;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvDoubleMatrixSerializer.Serialize(textWriter, expected);

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvDoubleMatrixSerializer.Deserialize(textReader);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            // sparse
            {
                var expected = testableMatrix.AsSparse;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvDoubleMatrixSerializer.Serialize(textWriter, expected);

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvDoubleMatrixSerializer.Deserialize(textReader);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            // read-only dense
            {
                var expected = testableMatrix.AsDense;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvDoubleMatrixSerializer.Serialize(textWriter, expected.AsReadOnly());

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvDoubleMatrixSerializer.Deserialize(textReader);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

            // read-only sparse
            {
                var expected = testableMatrix.AsSparse;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvDoubleMatrixSerializer.Serialize(textWriter, expected.AsReadOnly());

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvDoubleMatrixSerializer.Deserialize(textReader);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        #endregion

        [TestMethod]
        public void DeserializationUnexpectedEntryTypeTest()
        {
            ExceptionAssert.Throw(
                () =>
                {
                    CsvDoubleMatrixSerializer.Deserialize(
                        Path.Combine("Data", "deserialize-valid-expected-double-declared-complex.csv"));
                },
                expectedType: typeof(InvalidOperationException),
                expectedMessage: ImplementationServices.GetResourceString(
                    "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"),
                expectedInnerType: typeof(InvalidOperationException),
                expectedInnerMessage: String.Format(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_DESERIALIZE_UNEXPECTED_ENTRY_TYPE"),
                    "Complex",
                    "Double"));
        }

        [TestMethod]
        public void SerializationToPathTest()
        {
            // path is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize((string)null, DoubleMatrix.Dense(2, 2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "path");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize((string)null, DoubleMatrix.Dense(2, 2).AsReadOnly());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "path");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Deserialize((string)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "path");
            }

            // path is not valid
            {
                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize("n/:.csv", DoubleMatrix.Dense(2, 2));
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_SERIALIZE"));

                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize("n/:.csv", DoubleMatrix.Dense(2, 2).AsReadOnly());
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_SERIALIZE"));
            }

            // path of invalid representation
            {
                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Deserialize(
                            Path.Combine("Data", $"deserialize-invalid-sparse-{EntryType}.csv"));
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"));

                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Deserialize(
                            Path.Combine("Data", $"deserialize-invalid-dense-{EntryType}.csv"));
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"));
            }

            // matrix is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize("file.csv", (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize("file.csv", (ReadOnlyDoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }

            // valid input
            {
                TestableDoubleMatrix testableMatrix;

                testableMatrix = GetTestableMatrix(true, true, true, out string partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(true, true, false, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(true, false, true, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(true, false, false, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(false, true, true, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(false, true, false, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(false, false, true, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);

                testableMatrix = GetTestableMatrix(false, false, false, out partialPath);
                TestSerializationToPath(testableMatrix, partialPath);
            }
        }

        [TestMethod]
        public void SerializationToStreamTest()
        {
            // reader or writer is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize((TextWriter)null, DoubleMatrix.Dense(2, 2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "writer");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize((TextWriter)null, DoubleMatrix.Dense(2, 2).AsReadOnly());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "writer");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Deserialize((TextReader)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "reader");
            }

            // stream does not contain a valid dense matrix representation
            {
                string[] data = [
                "Dense,2, 3, MatrixName",
                ",column0,,column2",
                "row0,1.0,4.0,5.0",
                "row1,2.0,6.0"];

                // Create a stream containing the CSV content.
                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Create a reader for the stream.
                StreamReader reader = new(stream);

                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Deserialize(reader);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"));
            }

            // stream does not contain a valid sparse matrix representation
            {
                string[] data = [
                "Sparse,2, 3, MatrixName",
                "2,3,5.0",
                "0,Column0",
                "0,Row0"];

                // Create a stream containing the CSV content.
                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Create a reader for the stream.
                StreamReader reader = new(stream);

                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Deserialize(reader);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"));
            }

            // matrix is null
            {
                MemoryStream stream = new();
                StreamWriter writer = new(stream);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize(writer, (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvDoubleMatrixSerializer.Serialize(writer, (ReadOnlyDoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }

            // valid input
            {
                TestableDoubleMatrix testableMatrix;

                testableMatrix = GetTestableMatrix(true, true, true, out string partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(true, true, false, out partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(true, false, true, out partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(true, false, false, out partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(false, true, true, out partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(false, true, false, out partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(false, false, true, out partialPath);
                TestSerializationToStream(testableMatrix);

                testableMatrix = GetTestableMatrix(false, false, false, out partialPath);
                TestSerializationToStream(testableMatrix);
            }
        }
    }
}