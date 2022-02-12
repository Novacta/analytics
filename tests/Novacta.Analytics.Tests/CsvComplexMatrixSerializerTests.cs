// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Novacta.Analytics.Tests
{
    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class CsvComplexMatrixSerializerTests
    {
        private readonly string EntryType = "complex";

        #region Helpers

        /// <summary>
        /// Gets a matrix testable for CSV serialization.
        /// </summary>
        private TestableComplexMatrix GetTestableMatrix(
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

            var testableMatrix = new TestableComplexMatrix(
                asColumnMajorDenseArray: new Complex[20] {
                                        0,  0, -2, 0,
                                        0,  0,  0, 0,
                                        0, -1,  0, 0,
                                        0,  0,  0, 0,
                                        0,  0,  0, 0
                },
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 2,
                name: matrixName,
                rowNames: rowNames,
                columnNames: columnNames);

            return testableMatrix;
        }

        private static void TestSerializationToPath(
            TestableComplexMatrix testableMatrix,
            string partialPath)
        {
            // dense
            {
                var expected = testableMatrix.AsDense;

                var path = "dense-" + partialPath;

                CsvComplexMatrixSerializer.Serialize(path, expected);

                var actual = CsvComplexMatrixSerializer.Deserialize(path);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            // sparse
            {
                var expected = testableMatrix.AsSparse;

                var path = "sparse-" + partialPath;

                CsvComplexMatrixSerializer.Serialize(path, expected);

                var actual = CsvComplexMatrixSerializer.Deserialize(path);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            // read-only dense
            {
                var expected = testableMatrix.AsDense;

                var path = "read-only-dense-" + partialPath;

                CsvComplexMatrixSerializer.Serialize(path, expected.AsReadOnly());

                var actual = CsvComplexMatrixSerializer.Deserialize(path);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            // read-only sparse
            {
                var expected = testableMatrix.AsSparse;

                var path = "read-only-sparse-" + partialPath;

                CsvComplexMatrixSerializer.Serialize(path, expected.AsReadOnly());

                var actual = CsvComplexMatrixSerializer.Deserialize(path);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }
        }

        private static void TestSerializationToStream(
            TestableComplexMatrix testableMatrix)
        {
            // dense
            {
                var expected = testableMatrix.AsDense;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvComplexMatrixSerializer.Serialize(textWriter, expected);

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvComplexMatrixSerializer.Deserialize(textReader);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            // sparse
            {
                var expected = testableMatrix.AsSparse;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvComplexMatrixSerializer.Serialize(textWriter, expected);

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvComplexMatrixSerializer.Deserialize(textReader);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            // read-only dense
            {
                var expected = testableMatrix.AsDense;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvComplexMatrixSerializer.Serialize(textWriter, expected.AsReadOnly());

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvComplexMatrixSerializer.Deserialize(textReader);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }

            // read-only sparse
            {
                var expected = testableMatrix.AsSparse;

                MemoryStream stream = new();

                var textWriter = new StreamWriter(stream);

                CsvComplexMatrixSerializer.Serialize(textWriter, expected.AsReadOnly());

                stream.Position = 0;

                var textReader = new StreamReader(stream);

                var actual = CsvComplexMatrixSerializer.Deserialize(textReader);

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }
        }

        #endregion

        [TestMethod]
        public void DeserializationUnexpectedEntryTypeTest()
        {
            ExceptionAssert.Throw(
                () =>
                {
                    CsvComplexMatrixSerializer.Deserialize(
                        Path.Combine("Data", "deserialize-valid-expected-complex-declared-double.csv"));
                },
                expectedType: typeof(InvalidOperationException),
                expectedMessage: ImplementationServices.GetResourceString(
                    "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"),
                expectedInnerType: typeof(InvalidOperationException),
                expectedInnerMessage: String.Format(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_DESERIALIZE_UNEXPECTED_ENTRY_TYPE"),
                    "Double",
                    "Complex"));
        }

        [TestMethod]
        public void SerializationToPathTest()
        {
            // path is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Serialize((string)null, ComplexMatrix.Dense(2, 2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "path");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Serialize((string)null, ComplexMatrix.Dense(2, 2).AsReadOnly());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "path");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Deserialize((string)null);
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
                        CsvComplexMatrixSerializer.Serialize("n/:.csv", ComplexMatrix.Dense(2, 2));
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_SERIALIZE"));

                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Serialize("n/:.csv", ComplexMatrix.Dense(2, 2).AsReadOnly());
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
                        CsvComplexMatrixSerializer.Deserialize(
                            Path.Combine("Data", $"deserialize-invalid-sparse-{EntryType}.csv"));
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"));

                ExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Deserialize(
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
                        CsvComplexMatrixSerializer.Serialize("file.csv", (ComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Serialize("file.csv", (ReadOnlyComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }

            // valid input
            {
                TestableComplexMatrix testableMatrix;

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
                        CsvComplexMatrixSerializer.Serialize((TextWriter)null, ComplexMatrix.Dense(2, 2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "writer");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Serialize((TextWriter)null, ComplexMatrix.Dense(2, 2).AsReadOnly());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "writer");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Deserialize((TextReader)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "reader");
            }

            // stream does not contain a valid dense matrix representation
            {
                string[] data = new string[4] {
                "Dense,2, 3, MatrixName",
                ",column0,,column2",
                "row0,1.0,4.0,5.0",
                "row1,2.0,6.0"};

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
                        CsvComplexMatrixSerializer.Deserialize(reader);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_REP_UNABLE_TO_DESERIALIZE"));
            }

            // stream does not contain a valid sparse matrix representation
            {
                string[] data = new string[4] {
                "Sparse,2, 3, MatrixName",
                "2,3,5.0",
                "0,Column0",
                "0,Row0"};

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
                        CsvComplexMatrixSerializer.Deserialize(reader);
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
                        CsvComplexMatrixSerializer.Serialize(writer, (ComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        CsvComplexMatrixSerializer.Serialize(writer, (ReadOnlyComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "matrix");
            }

            // valid input
            {
                TestableComplexMatrix testableMatrix;

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