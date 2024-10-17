// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    [DeploymentItem("Data", "Data")]
    public class CategoricalDataSetTests
    {
        [TestMethod()]
        public void CategorizeTest()
        {
            CategoricalDataSetTest.Categorize.Succeed.FromPathWithVariableNames();
            CategoricalDataSetTest.Categorize.Succeed.FromPathWithoutVariableNames();
            CategoricalDataSetTest.Categorize.Succeed.FromTextReaderWithVariableNames();
            CategoricalDataSetTest.Categorize.Succeed.FromTextReaderWithoutVariableNames();
            CategoricalDataSetTest.Categorize.Fail.CategoryLabelIsMissing();
            CategoricalDataSetTest.Categorize.Fail.ExtractedColumnIsMissingInDataRow();
            CategoricalDataSetTest.Categorize.Fail.NoDataRows();
            CategoricalDataSetTest.Categorize.Fail.NumericalColumnsIsNull();
            CategoricalDataSetTest.Categorize.Fail.ProviderIsNull();
            CategoricalDataSetTest.Categorize.Fail.ReaderIsNull();
            CategoricalDataSetTest.Categorize.Fail.TargetColumnIsNegative();
        }

        [TestMethod()]
        public void EncodeTest()
        {
            CategoricalDataSetTest.Encode.Basic.Succeed.FromPathWithVariableNames();
            CategoricalDataSetTest.Encode.Basic.Succeed.FromTextReaderWithVariableNames();
            CategoricalDataSetTest.Encode.Basic.Succeed.FromTextReaderWithoutVariableNames();
            CategoricalDataSetTest.Encode.Basic.Fail.ExtractedColumnIsMissingInHeaderRow();
            CategoricalDataSetTest.Encode.Basic.Fail.ExtractedColumnIsMissingInDataRow();
            CategoricalDataSetTest.Encode.Basic.Fail.NoDataRows();
            CategoricalDataSetTest.Encode.Basic.Fail.CategoryLabelIsMissing();
            CategoricalDataSetTest.Encode.Basic.Fail.VariableNameIsMissing();
            CategoricalDataSetTest.Encode.Basic.Fail.ReaderIsNull();
            CategoricalDataSetTest.Encode.Basic.Fail.ExtractedColumnsIsNull();

            CategoricalDataSetTest.Encode.Advanced.Succeed.FromPathWithVariableNames();
            CategoricalDataSetTest.Encode.Advanced.Succeed.FromTextReaderWithVariableNames();
            CategoricalDataSetTest.Encode.Advanced.Succeed.FromTextReaderWithoutVariableNames();
            CategoricalDataSetTest.Encode.Advanced.Fail.ExtractedColumnIsMissingInHeaderRow();
            CategoricalDataSetTest.Encode.Advanced.Fail.ExtractedColumnIsMissingInDataRow();
            CategoricalDataSetTest.Encode.Advanced.Fail.NoDataRows();
            CategoricalDataSetTest.Encode.Advanced.Fail.CategoryLabelIsMissing();
            CategoricalDataSetTest.Encode.Advanced.Fail.VariableNameIsMissing();
            CategoricalDataSetTest.Encode.Advanced.Fail.ReaderIsNull();
            CategoricalDataSetTest.Encode.Advanced.Fail.ExtractedColumnsIsNull();
            CategoricalDataSetTest.Encode.Advanced.Fail.SpecialCategorizersIsNull();
            CategoricalDataSetTest.Encode.Advanced.Fail.SpecialCategorizersContainsIrrelevantKey();
            CategoricalDataSetTest.Encode.Advanced.Fail.SpecialCategorizersContainsNullValue();
            CategoricalDataSetTest.Encode.Advanced.Fail.ProviderIsNull();
        }

        [TestMethod()]
        public void FromEncodedDataTest()
        {

            // variables is null
            {
                List<CategoricalVariable> variables = null;
                DoubleMatrix data = DoubleMatrix.Identity(3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = CategoricalDataSet.FromEncodedData(
                            variables: variables,
                            data: data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "variables");
            }

            // data is null
            {
                List<CategoricalVariable> variables =
                    [];
                DoubleMatrix data = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = CategoricalDataSet.FromEncodedData(
                            variables: variables,
                            data: data);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // variables count unequal to data number of columns
            {
                List<CategoricalVariable> variables =
                    [
                        new CategoricalVariable("var0"),
                        new CategoricalVariable("var1")
                    ];

                DoubleMatrix data = DoubleMatrix.Dense(6, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = CategoricalDataSet.FromEncodedData(
                            variables: variables,
                            data: data);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: String.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CAT_COLUMNS_NOT_EQUAL_TO_VARIABLES_COUNT"),
                            "variables"),
                    expectedParameterName: "data");
            }

            // category not included in variable
            {
                List<CategoricalVariable> variables =
                    [
                        new CategoricalVariable("var0"),
                        new CategoricalVariable("var1")
                    ];

                variables[0].Add(0.0);
                variables[1].Add(1.0);

                DoubleMatrix data = DoubleMatrix.Dense(1, 2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = CategoricalDataSet.FromEncodedData(
                            variables: variables,
                            data: data);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CAT_MATRIX_ENTRY_NOT_IN_VARIABLE_LIST"),
                    expectedParameterName: "data");
            }

            // Valid input
            {
                // Create a data stream 
                string[] data = [
                    "COLOR,NUMBER",
                    "Red,  -2.2",
                    "Green, 0.0",
                    "Red,  -3.3",
                    "Black,-1.1",
                    "Black, 4.4" ];

                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Define a special categorizer for variable NUMBER
                string numberCategorizer(string token, IFormatProvider provider)
                {
                    double datum = Convert.ToDouble(token, provider);
                    if (datum == 0)
                    {
                        return "Zero";
                    }
                    else if (datum < 0)
                    {
                        return "Negative";
                    }
                    else
                    {
                        return "Positive";
                    }
                }

                // Attach the special categorizer to variable NUMBER
                int numberColumnIndex = 1;
                var specialCategorizers = new Dictionary<int, Categorizer>
                {
                    { numberColumnIndex, numberCategorizer }
                };

                // Encode the categorical data set
                StreamReader streamReader = new(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                bool firstLineContainsColumnHeaders = true;
                CategoricalDataSet actual = CategoricalDataSet.Encode(
                    streamReader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders,
                    specialCategorizers,
                    CultureInfo.InvariantCulture);

                CategoricalVariable color = new("COLOR")
                {
                    { 0, "Red" },
                    { 1, "Green" },
                    { 2, "Black" }
                };
                color.SetAsReadOnly();

                CategoricalVariable number = new("NUMBER")
                {
                    { 0, "Negative" },
                    { 1, "Zero" },
                    { 2, "Positive" }
                };
                number.SetAsReadOnly();

                List<CategoricalVariable> expectedVariables =
                    [color, number];

                DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);
                expectedData[0, 0] = 0;
                expectedData[1, 0] = 1;
                expectedData[2, 0] = 0;
                expectedData[3, 0] = 2;
                expectedData[4, 0] = 2;

                expectedData[0, 1] = 0;
                expectedData[1, 1] = 1;
                expectedData[2, 1] = 0;
                expectedData[3, 1] = 0;
                expectedData[4, 1] = 2;

                CategoricalDataSet expected = CategoricalDataSet.FromEncodedData(
                    expectedVariables,
                    expectedData);

                CategoricalDataSetAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetContingencyTableTest()
        {
            // rowVariableIndex is out of scope 
            {
                var STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "rowVariableIndex";

                // Create a data stream.
                string[] data = [
                        "COLOR,NUMBER",
                        "Red,Negative",
                        "Green,Zero",
                        "White,Positive",
                        "Red,Negative",
                        "Blue,Negative",
                        "Blue,Positive" ];

                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Encode the categorical data set
                StreamReader streamReader = new(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                bool firstLineContainsColumnHeaders = true;
                var dataSet = CategoricalDataSet.Encode(
                    streamReader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        dataSet.GetContingencyTable(
                            rowVariableIndex: -1,
                            columnVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        dataSet.GetContingencyTable(
                            rowVariableIndex: dataSet.NumberOfColumns,
                            columnVariableIndex: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            // columnVariableIndex is out of scope 
            {
                var STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "columnVariableIndex";

                // Create a data stream.
                string[] data = [
                        "COLOR,NUMBER",
                        "Red,Negative",
                        "Green,Zero",
                        "White,Positive",
                        "Red,Negative",
                        "Blue,Negative",
                        "Blue,Positive" ];

                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Encode the categorical data set
                StreamReader streamReader = new(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                bool firstLineContainsColumnHeaders = true;
                var dataSet = CategoricalDataSet.Encode(
                    streamReader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        dataSet.GetContingencyTable(
                            rowVariableIndex: 0,
                            columnVariableIndex: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        dataSet.GetContingencyTable(
                            rowVariableIndex: 0,
                            columnVariableIndex: dataSet.NumberOfColumns);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                // Create a data stream.
                string[] data = [
                        "COLOR,NUMBER",
                        "Red,Negative",
                        "Green,Zero",
                        "White,Positive",
                        "Red,Negative",
                        "Blue,Negative",
                        "Blue,Positive" ];

                MemoryStream stream = new();
                StreamWriter writer = new(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Encode the categorical data set
                StreamReader streamReader = new(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                bool firstLineContainsColumnHeaders = true;
                var dataSet = CategoricalDataSet.Encode(
                    streamReader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders);

                var actual = dataSet.GetContingencyTable(1, 0);

                var expected = DoubleMatrix.Dense(3, 4);
                expected[0, 0] = 2;
                expected[0, 3] = 1;
                expected[1, 1] = 1;
                expected[2, 2] = 1;
                expected[2, 3] = 1;

                var rowNames =
                    new string[3] { "Negative", "Zero", "Positive" };

                for (int i = 0; i < expected.NumberOfRows; i++)
                {
                    expected.SetRowName(i, rowNames[i]);
                }

                var columnNames =
                    new string[4] { "Red", "Green", "White", "Blue" };

                for (int j = 0; j < expected.NumberOfColumns; j++)
                {
                    expected.SetColumnName(j, columnNames[j]);
                }

                expected.Name = "NUMBER-by-COLOR";

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void DecodeTest()
        {
            // Create a data stream 
            string[] data = [
                "COLOR,HAPPINESS,NUMBER",
                "Red,TRUE,  -2.2",
                "Green,TRUE, 0.0",
                "Red,FALSE,  -3.3",
                "Black,TRUE,-1.1",
                "Black,FALSE, 4.4" ];

            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Define a special categorizer for variable NUMBER
            string numberCategorizer(string token, IFormatProvider provider)
            {
                double datum = Convert.ToDouble(token, provider);
                if (datum == 0)
                {
                    return "Zero";
                }
                else if (datum < 0)
                {
                    return "Negative";
                }
                else
                {
                    return "Positive";
                }
            }

            // Attach the special categorizer to variable NUMBER
            int numberColumnIndex = 2;
            var specialCategorizers = new Dictionary<int, Categorizer>
            {
                { numberColumnIndex, numberCategorizer }
            };

            // Encode the categorical data set
            StreamReader reader = new(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
            bool firstLineContainsColumnHeaders = true;
            CategoricalDataSet actual = CategoricalDataSet.Encode(
                reader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders,
                specialCategorizers,
                CultureInfo.InvariantCulture);

            string[][] expectedLabels =
            [
                ["Red", "Negative"],
                ["Green", "Zero"],
                ["Red", "Negative"],
                ["Black", "Negative"],
                ["Black", "Positive"]
            ];

            var actualLabels = actual.Decode();

            Assert.AreEqual(expectedLabels.GetLength(0), actualLabels.GetLength(0));
            Assert.AreEqual(expectedLabels.GetLowerBound(0), actualLabels.GetLowerBound(0));
            Assert.AreEqual(expectedLabels.GetUpperBound(0), actualLabels.GetUpperBound(0));

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(expectedLabels[i][j], actualLabels[i][j]);
                }
            }
        }

        [TestMethod()]
        public void DisjoinTest()
        {
            CategoricalDataSetTest.Disjoin.Succeed();

            CategoricalDataSetTest.Disjoin.SupplementaryData.IsValid();
            CategoricalDataSetTest.Disjoin.SupplementaryData.IsNull();
            CategoricalDataSetTest.Disjoin.SupplementaryData.HasWrongNumberOfColumns();
            CategoricalDataSetTest.Disjoin.SupplementaryData.ContainsUnexpectedCodes();
        }

        #region ITabularCollection

        [TestMethod()]
        public void NumberOfRowsTest()
        {
            CategoricalVariable color = new("COLOR")
            {
                { 0, "Red" },
                { 1, "Green" },
                { 2, "Black" }
            };
            color.SetAsReadOnly();

            CategoricalVariable number = new("NUMBER")
            {
                { 0, "Negative" },
                { 1, "Zero" },
                { 2, "Positive" }
            };
            number.SetAsReadOnly();

            List<CategoricalVariable> sourceVariables =
                [color, number];

            DoubleMatrix sourceData = DoubleMatrix.Dense(5, 2);
            sourceData[0, 0] = 0;
            sourceData[1, 0] = 1;
            sourceData[2, 0] = 0;
            sourceData[3, 0] = 2;
            sourceData[4, 0] = 2;

            sourceData[0, 1] = 0;
            sourceData[1, 1] = 1;
            sourceData[2, 1] = 0;
            sourceData[3, 1] = 0;
            sourceData[4, 1] = 2;

            CategoricalDataSet source = CategoricalDataSet.FromEncodedData(
                sourceVariables,
                sourceData);

            ReadOnlyTabularCollectionTest.NumberOfRows.Get(
                expected: 5,
                source: source);
        }

        [TestMethod()]
        public void NumberOfColumnsTest()
        {
            CategoricalVariable color = new("COLOR")
            {
                { 0, "Red" },
                { 1, "Green" },
                { 2, "Black" }
            };
            color.SetAsReadOnly();

            CategoricalVariable number = new("NUMBER")
            {
                { 0, "Negative" },
                { 1, "Zero" },
                { 2, "Positive" }
            };
            number.SetAsReadOnly();

            List<CategoricalVariable> sourceVariables =
                [color, number];

            DoubleMatrix sourceData = DoubleMatrix.Dense(5, 2);
            sourceData[0, 0] = 0;
            sourceData[1, 0] = 1;
            sourceData[2, 0] = 0;
            sourceData[3, 0] = 2;
            sourceData[4, 0] = 2;

            sourceData[0, 1] = 0;
            sourceData[1, 1] = 1;
            sourceData[2, 1] = 0;
            sourceData[3, 1] = 0;
            sourceData[4, 1] = 2;

            CategoricalDataSet source = CategoricalDataSet.FromEncodedData(
                sourceVariables,
                sourceData);

            ReadOnlyTabularCollectionTest.NumberOfColumns.Get(
                expected: 2,
                source: source);
        }

        [TestMethod()]
        public void IndexerGetTest()
        {
            CategoricalVariable color = new("COLOR")
            {
                { 0, "Red" },
                { 1, "Green" },
                { 2, "Black" }
            };
            color.SetAsReadOnly();

            CategoricalVariable number = new("NUMBER")
            {
                { 0, "Negative" },
                { 1, "Zero" },
                { 2, "Positive" }
            };
            number.SetAsReadOnly();

            List<CategoricalVariable> sourceVariables =
                [color, number];

            DoubleMatrix sourceData = DoubleMatrix.Dense(5, 2);
            sourceData[0, 0] = 0;
            sourceData[1, 0] = 1;
            sourceData[2, 0] = 0;
            sourceData[3, 0] = 2;
            sourceData[4, 0] = 2;

            sourceData[0, 1] = 0;
            sourceData[1, 1] = 1;
            sourceData[2, 1] = 0;
            sourceData[3, 1] = 0;
            sourceData[4, 1] = 2;

            CategoricalDataSet source = CategoricalDataSet.FromEncodedData(
                sourceVariables,
                sourceData);

            ReadOnlyTabularCollectionTest.Indexer.Get.AnyRowIndexIsOutOrRange(source);
            ReadOnlyTabularCollectionTest.Indexer.Get.AnyColumnIndexIsOutOrRange(source);
            ReadOnlyTabularCollectionTest.Indexer.Get.ColumnIndexesIsNull(source);
            ReadOnlyTabularCollectionTest.Indexer.Get.RowIndexesIsNull(source);

            #region SubCollection

            {
                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoryAssert.AreEqual,
                    expected: new Category(0.0, "Negative"),
                    source: source,
                    rowIndex: 2,
                    columnIndex: 1);
            }

            {
                var rowIndex = 3;
                var columnIndexes = IndexCollection.FromArray(
                    [0, 1, 0, 0, 1]);

                var expectedVariables =
                    new List<CategoricalVariable>() {
                        color,
                        number,
                        color,
                        color,
                        number
                    };

                var expectedData = sourceData[rowIndex, columnIndexes];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
            }

            {
                var rowIndex = 1;
                var columnIndexes = ":";

                var expectedVariables =
                    new List<CategoricalVariable>() {
                        color,
                        number
                    };

                var expectedData = sourceData[rowIndex, columnIndexes];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndex: rowIndex,
                    columnIndexes: columnIndexes);
            }

            {
                var rowIndexes = IndexCollection.Range(0, 2);
                var columnIndex = 1;

                var expectedVariables =
                    new List<CategoricalVariable>() {
                        number
                    };

                var expectedData = sourceData[rowIndexes, columnIndex];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
            }

            {
                var rowIndexes = IndexCollection.Range(1, 3);
                var columnIndexes = IndexCollection.Sequence(1, -1, 0);

                var expectedVariables =
                    new List<CategoricalVariable>() {
                        number,
                        color
                    };

                var expectedData = sourceData[rowIndexes, columnIndexes];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            {
                var rowIndexes = IndexCollection.Range(2, 2);
                var columnIndexes = ":";

                var expectedVariables =
                    new List<CategoricalVariable>() {
                        color,
                        number
                    };

                var expectedData = sourceData[rowIndexes, columnIndexes];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            {
                var rowIndexes = ":";
                var columnIndex = 1;

                var expectedVariables =
                    new List<CategoricalVariable>() {
                        number
                    };

                var expectedData = sourceData[rowIndexes, columnIndex];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndexes: rowIndexes,
                    columnIndex: columnIndex);
            }

            {
                var rowIndexes = ":";
                var columnIndexes = IndexCollection.FromArray(
                    [1, 0, 1]);

                var expectedVariables =
                   new List<CategoricalVariable>() {
                        number,
                        color,
                        number
                   };

                var expectedData = sourceData[rowIndexes, columnIndexes];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            {
                var rowIndexes = ":";
                var columnIndexes = ":";

                var expectedVariables =
                   new List<CategoricalVariable>() {
                        color,
                        number
                   };

                var expectedData = sourceData[rowIndexes, columnIndexes];

                CategoricalDataSet expected =
                    CategoricalDataSet.FromEncodedData(
                        expectedVariables,
                        expectedData);

                ReadOnlyTabularCollectionTest.Indexer.Get.SubCollection(
                    areEqual: CategoricalDataSetAssert.AreEqual,
                    expected: expected,
                    source: source,
                    rowIndexes: rowIndexes,
                    columnIndexes: columnIndexes);
            }

            #endregion
        }

        #endregion
    }
}