// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class ComplexMatrixRowTests
    {
        private void PropertyChangedEventHandler(
            Object sender,
            PropertyChangedEventArgs e)
        {
            Assert.AreEqual("UNKNOWN", e.PropertyName);
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            // IEnumerable.GetEnumerator
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];
                    IEnumerable enumerable = (IEnumerable)target;

                    IEnumerator enumerator = enumerable.GetEnumerator();
                    object current;
                    int index = 0;

                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        ComplexAssert.AreEqual(target[index], (Complex)current, ComplexMatrixTest.Accuracy);
                        index++;
                    }

                    // reset 
                    enumerator.Reset();

                    Assert.AreEqual(-1, (int)Reflector.GetField(enumerator, "position"));

                    // dispose
                    enumerator = null;
                    GC.Collect(10, GCCollectionMode.Forced);
                }
            }

            // IEnumerable<Complex>.GetEnumerator
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];
                    IEnumerable<Complex> enumerable = (IEnumerable<Complex>)target;

                    IEnumerator<Complex> enumerator = enumerable.GetEnumerator();

                    int index = 0;
                    Complex current;

                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        ComplexAssert.AreEqual(target[index], current, ComplexMatrixTest.Accuracy);
                        index++;
                    }

                    // reset 
                    enumerator.Reset();

                    Assert.AreEqual(-1, (int)Reflector.GetField(enumerator, "position"));

                    // dispose
                    enumerator.Dispose();
                }
            }

            // IEnumerable<Complex>.Current fails
            {
                string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];
                    var enumerable = (IEnumerable<Complex>)target;

                    var enumerator = enumerable.GetEnumerator();

                    ExceptionAssert.Throw(
                        () =>
                        {
                            Complex current = enumerator.Current;
                        },
                        expectedType: typeof(InvalidOperationException),
                        expectedMessage: STR_EXCEPT_ENU_OUT_OF_BOUNDS);
                }
            }

            // IEnumerable.Current fails
            {
                string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];
                    var enumerable = (IEnumerable)target;

                    var enumerator = enumerable.GetEnumerator();

                    ExceptionAssert.Throw(
                        () =>
                        {
                            object current = enumerator.Current;
                        },
                        expectedType: typeof(InvalidOperationException),
                        expectedMessage: STR_EXCEPT_ENU_OUT_OF_BOUNDS);
                }
            }
        }

        [TestMethod()]
        public void ConstructorTest()
        {
            var matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);
            var rows = matrix.AsRowCollection();

            for (int i = 0; i < matrix.NumberOfRows; i++)
            {
                var row = rows[i];
                Assert.AreEqual(i, row.Index);
                Assert.AreEqual(matrix.NumberOfColumns, row.Length);
                matrix.TryGetRowName(i, out string expectedRowName);

                Assert.AreEqual(expectedRowName, row.Name);

                for (int j = 0; j < matrix.NumberOfColumns; j++)
                {
                    ComplexAssert.AreEqual(matrix[i, j], row[j], ComplexMatrixTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // matrix has no row or column names
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    [
                        new(.1, .1),
                        new(10.2, 10.2),
                        new(-2.3, -2.3),
                        new(1000.2, 1000.2),
                        new(.2, .2),
                        new(239.32, 239.32)
                    ]);
                var rows = matrix.AsRowCollection();

                var actual = rows[0].ToString();

                var expected =
                  "(              0.1,              0.1) "
                + "(             -2.3,             -2.3) "
                + "(              0.2,              0.2) ";

                Assert.AreEqual(expected, actual);
            }

            // matrix has some row or column names
            {
                var matrix = ComplexMatrix.Dense(3, 3,
                    [
                        new(.1, .1),
                        new(10.2, 10.2),
                        0,
                        new(-2.3, -2.3),
                        new(1000.2, 1000.2),
                        0,
                        new(.2, .2),
                        new(239.32, 239.32),
                        0
                    ]);
                var rows = matrix.AsRowCollection();

                // ROW NAMES 

                // Row name with adequate length
                matrix.SetRowName(0, "Row name <  15");

                // No name for row 1

                // Too long row name
                matrix.SetRowName(2, "Row name too much long");

                // COL NAMES

                // Column name with adequate length
                matrix.SetColumnName(0, "Col name <  15");

                // Too long column name
                matrix.SetColumnName(1, "Col name so much long that must be truncated");

                // No name for column 2

                // Row 0

                var actual = rows[0].ToString();

                var expected =
                    "                 "
                    + "[Col name <  15]                      "
                    + "[Col name so much long that must be*] "
                    + "                                      "
                    //         newline char
                    + Environment.NewLine
                    //         first row
                    + "[Row name <  15] "
                    + "(              0.1,              0.1) "
                    + "(             -2.3,             -2.3) "
                    + "(              0.2,              0.2) ";

                Assert.AreEqual(expected, actual);

                // Row 1 (No name)

                actual = rows[1].ToString();

                expected =
                    "[Col name <  15]                      "
                    + "[Col name so much long that must be*] "
                    + "                                      "
                    //         newline char
                    + Environment.NewLine
                    + "(             10.2,             10.2) "
                    + "(           1000.2,           1000.2) "
                    + "(           239.32,           239.32) ";

                Assert.AreEqual(expected, actual);

                // Row 2

                actual = rows[2].ToString();

                expected =
                    "                 "
                    + "[Col name <  15]                      "
                    + "[Col name so much long that must be*] "
                    + "                                      "
                    //         newline char
                    + Environment.NewLine
                    + "[Row name too *] "
                    + "(                0,                0) "
                    + "(                0,                0) "
                    + "(                0,                0) ";

                Assert.AreEqual(expected, actual);
            }

            // matrix has some row names but no col ones
            {
                var matrix = ComplexMatrix.Dense(3, 3,
                    [
                        new(.1, .1),
                        new(10.2, 10.2),
                        0,
                        new(-2.3, -2.3),
                        new(1000.2, 1000.2),
                        0,
                        new(.2, .2),
                        new(239.32, 239.32),
                        0
                    ]);
                var rows = matrix.AsRowCollection();

                // ROW NAMES 

                // Row name with adequate length
                matrix.SetRowName(0, "Row name <  15");

                // No name for row 1

                // Too long row name
                matrix.SetRowName(2, "Row name too much long");

                // Row 0

                var actual = rows[0].ToString();

                var expected =
                    "[Row name <  15] "
                    + "(              0.1,              0.1) "
                    + "(             -2.3,             -2.3) "
                    + "(              0.2,              0.2) ";

                Assert.AreEqual(expected, actual);

                // Row 1 (no name)

                actual = rows[1].ToString();

                expected =
                    "(             10.2,             10.2) "
                    + "(           1000.2,           1000.2) "
                    + "(           239.32,           239.32) ";

                Assert.AreEqual(expected, actual);

                // Row 2

                actual = rows[2].ToString();

                expected =
                    "[Row name too *] "
                    + "(                0,                0) "
                    + "(                0,                0) "
                    + "(                0,                0) ";

                Assert.AreEqual(expected, actual);
            }

            // matrix has no row names and some column ones
            {
                var matrix = ComplexMatrix.Dense(3, 3,
                    [
                        new(.1, .1),
                        new(10.2, 10.2),
                        0,
                        new(-2.3, -2.3),
                        new(1000.2, 1000.2),
                        0,
                        new(.2, .2),
                        new(239.32, 239.32),
                        0
                    ]);
                var rows = matrix.AsRowCollection();

                // COL NAMES

                // Column name with adequate length
                matrix.SetColumnName(0, "Col name <  15");

                // No name for column 1

                // Too long column name
                matrix.SetColumnName(2, "Col name so much long that must be truncated");

                // Row 0

                var actual = rows[0].ToString();

                var expected =
                    "[Col name <  15]                      "
                    + "                                      "
                    + "[Col name so much long that must be*] "
                    + Environment.NewLine
                    + "(              0.1,              0.1) "
                    + "(             -2.3,             -2.3) "
                    + "(              0.2,              0.2) ";

                Assert.AreEqual(expected, actual);

                // Row 1

                actual = rows[1].ToString();

                expected =
                    "[Col name <  15]                      "
                    + "                                      "
                    + "[Col name so much long that must be*] "
                    + Environment.NewLine
                    + "(             10.2,             10.2) "
                    + "(           1000.2,           1000.2) "
                    + "(           239.32,           239.32) ";

                Assert.AreEqual(expected, actual);

                // Row 2

                actual = rows[2].ToString();

                expected =
                    "[Col name <  15]                      "
                    + "                                      "
                    + "[Col name so much long that must be*] "
                    + Environment.NewLine
                    + "(                0,                0) "
                    + "(                0,                0) "
                    + "(                0,                0) ";

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var matrix = ComplexMatrix.Dense(3, 3,
                [1, 2, 3, 4, 5, 6, 1, 2, 3]);
            var rows = matrix.AsRowCollection();

            var row = rows[0];
            var actual = row.GetHashCode();

            var hash = new HashCode();
            hash.Add(matrix.NumberOfColumns);

            foreach (var item in row)
            {
                hash.Add(item);
            }

            var expected = hash.ToHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DataTest()
        {
            PropertyChangedSubscriber subscriber;
            Complex expected;
            ComplexMatrixRow row;

            var matrix = ComplexMatrix.Dense(3, 3,
                 [1, 4, 1, 2, 5, 2, 3, 6, 3],
                 StorageOrder.ColumnMajor);

            var rows = matrix.AsRowCollection();

            // m = [  1  2  3
            //        4  5  6
            //        1  2  3  ]

            rows.XDataColumn = 2;
            rows.YDataColumn = 1;
            rows.ZDataColumn = 0;

            int rowIndex = 1;
            row = rows[rowIndex];
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            // Setting without changing values

            expected = matrix[rowIndex, rows.XDataColumn];
            row.XData = expected;
            ArrayAssert<string>.AreEqual(
                expected: [""],
                actual: [.. subscriber.PropertyNames]);

            expected = matrix[rowIndex, rows.YDataColumn];
            row.YData = expected;
            ArrayAssert<string>.AreEqual(
                expected: [""],
                actual: [.. subscriber.PropertyNames]);

            expected = matrix[rowIndex, rows.ZDataColumn];
            row.ZData = expected;
            ArrayAssert<string>.AreEqual(
                expected: [""],
                actual: [.. subscriber.PropertyNames]);

            ComplexAssert.AreEqual(
                matrix[rowIndex, rows.XDataColumn],
                row.XData,
                ComplexMatrixTest.Accuracy);
            ComplexAssert.AreEqual(
                matrix[rowIndex, rows.YDataColumn],
                row.YData,
                ComplexMatrixTest.Accuracy);
            ComplexAssert.AreEqual(
                matrix[rowIndex, rows.ZDataColumn],
                row.ZData,
                ComplexMatrixTest.Accuracy);

            // Setting by changing values

            expected = -5.0;
            row.XData = expected;
            ArrayAssert<string>.AreEqual(
                expected: ["", "XData", "[2]"],
                actual: [.. subscriber.PropertyNames]);

            expected = -10.0;
            row.YData = expected;
            ArrayAssert<string>.AreEqual(
                expected: ["", "XData", "[2]", "YData", "[1]"],
                actual: [.. subscriber.PropertyNames]);

            expected = -15.0;
            row.ZData = expected;
            ArrayAssert<string>.AreEqual(
                expected: ["", "XData", "[2]", "YData", "[1]", "ZData", "[0]"],
                actual: [.. subscriber.PropertyNames]);

            ComplexAssert.AreEqual(
                matrix[rowIndex, rows.XDataColumn],
                row.XData,
                ComplexMatrixTest.Accuracy);
            ComplexAssert.AreEqual(
                matrix[rowIndex, rows.YDataColumn],
                row.YData,
                ComplexMatrixTest.Accuracy);
            ComplexAssert.AreEqual(
                matrix[rowIndex, rows.ZDataColumn],
                row.ZData,
                ComplexMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void EqualityTest()
        {
            ComplexMatrix matrix, otherMatrix;
            ComplexMatrixRow thisRow, otherRow;

            matrix = ComplexMatrix.Dense(3, 3,
                [1, 2, 3, 4, 5, 6, 1, 2, 3],
                StorageOrder.RowMajor);

            var rows = matrix.AsRowCollection();

            // m = [  1  2  3
            //        4  5  6
            //        1  2  3

            // STRONGLY TYPED COMPARISON

            // Length difference > 0

            otherMatrix = ComplexMatrix.Dense(3, 4);
            var otherRows = otherMatrix.AsRowCollection();

            thisRow = rows[0];
            otherRow = otherRows[0];

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // Length difference < 0

            otherMatrix = ComplexMatrix.Dense(3, 2);
            otherRows = otherMatrix.AsRowCollection();

            thisRow = rows[0];
            otherRow = otherRows[0];

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // Length difference equals 0

            // ----- Rows are equal

            thisRow = rows[0];
            otherRow = rows[2];

            Assert.IsFalse(thisRow != otherRow);
            Assert.IsTrue(thisRow == otherRow);
            Assert.IsTrue(thisRow.Equals(otherRow));

            // ----- thisRow less than otherRow

            thisRow = rows[0];
            otherRow = rows[1];

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // ----- thisRow greater than otherRow

            thisRow = rows[1];
            otherRow = rows[2];

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // WEAKLY TYPED COMPARISON

            object weakOther;

            // null other

            thisRow = rows[1];
            weakOther = null;

            Assert.IsFalse(thisRow.Equals(weakOther));

            // ComplexMatrixRow other 

            thisRow = rows[0];
            weakOther = rows[2];

            Assert.IsTrue(thisRow.Equals(weakOther));

            // other not of type ComplexMatrixRow

            thisRow = rows[1];
            weakOther = IndexCollection.Default(2);

            Assert.IsFalse(thisRow.Equals(weakOther));

            // COMPARISONS INVOLVING NULL OBJECTS

            ComplexMatrixRow leftRow = null;
            ComplexMatrixRow rightRow = rows[0];
            Assert.IsFalse(leftRow == rightRow);
            leftRow = rows[0];
            rightRow = null;
            Assert.IsFalse(leftRow == rightRow);

            leftRow = null;
            rightRow = null;
            Assert.IsTrue(leftRow == rightRow);
        }

        [TestMethod()]
        public void MatrixTest()
        {
            ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);
            var rows = matrix.AsRowCollection();

            var actual = rows.Matrix;

            var expected = matrix;
            Assert.IsTrue(object.ReferenceEquals(expected, actual));
        }

        [TestMethod()]
        public void NotifyPropertyChangedTest()
        {
            ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);

            var rows = matrix.AsRowCollection();

            ComplexMatrixRow target = rows[0];

            target.PropertyChanged += new PropertyChangedEventHandler(
                this.PropertyChangedEventHandler);

            string propertyName = "UNKNOWN";
            target.NotifyPropertyChanged(propertyName);
        }

        [TestMethod()]
        public void ToComplexMatrixTest()
        {
            // value is null
            {
                ComplexMatrix actual = (ComplexMatrixRow)(null);

                Assert.IsNull(actual);

                actual = ComplexMatrixRow.ToComplexMatrix(null);

                Assert.IsNull(actual);
            }

            // value is not null
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);
                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var expected = matrix[i, ":"];
                    ComplexMatrix actual = rows[i];
                    ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                    actual = ComplexMatrixRow.ToComplexMatrix(rows[i]);
                    ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void IndexerInt32GetTest()
        {
            // columnIndex is less than 0
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target[-1];
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is greater than NumberOfColumns - 1
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target[matrix.NumberOfColumns];
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is inside the bounds
            {
                var subscriber = new PropertyChangedSubscriber();

                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;

                int rowIndex = 1;
                ComplexMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                Assert.AreEqual(matrix[rowIndex, dataColumn], row[dataColumn]);
                ArrayAssert<string>.AreEqual(
                    expected: ["", "[2]", "XData", "YData"],
                    actual: [.. subscriber.PropertyNames]);
            }
        }

        [TestMethod()]
        public void IndexerStringGetTest()
        {
            // columnIndex not representing an integer
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target["A"];
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_CANNOT_PARSE_AS_INT32"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is less than 0
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target["-1"];
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is greater than NumberOfColumns - 1
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target["3"];
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is inside the bounds
            {
                var subscriber = new PropertyChangedSubscriber();

                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;

                int rowIndex = 1;
                ComplexMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                Assert.AreEqual(matrix[rowIndex, dataColumn], row["2"]);
                ArrayAssert<string>.AreEqual(
                    expected: ["", "[2]", "XData", "YData"],
                    actual: [.. subscriber.PropertyNames]);
            }
        }

        [TestMethod()]
        public void IndexerInt32SetTest()
        {
            // columnIndex is less than 0
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target[-1] = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is greater than NumberOfColumns - 1
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target[matrix.NumberOfColumns] = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is inside the bounds
            {
                var subscriber = new PropertyChangedSubscriber();

                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;
                rows.ZDataColumn = dataColumn;

                int rowIndex = 1;
                ComplexMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                ComplexAssert.AreEqual(
                    matrix[rowIndex, dataColumn],
                    row[dataColumn],
                    ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: ["", "[2]", "XData", "YData", "ZData"],
                    actual: [.. subscriber.PropertyNames]);

                row[0] = -1;
                ComplexAssert.AreEqual(
                    matrix[rowIndex, dataColumn],
                    row[dataColumn],
                ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: ["", "[2]", "XData", "YData", "ZData", "[0]"],
                    actual: [.. subscriber.PropertyNames]);
            }
        }

        [TestMethod()]
        public void IndexerStringSetTest()
        {
            // columnIndex not representing an integer
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target["A"] = -1;
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_CANNOT_PARSE_AS_INT32"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is less than 0
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target["-1"] = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is greater than NumberOfColumns - 1
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target["3"] = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "columnIndex");
            }

            // columnIndex is inside the bounds
            {
                var subscriber = new PropertyChangedSubscriber();

                var matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;
                rows.ZDataColumn = dataColumn;

                int rowIndex = 1;
                ComplexMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                ComplexAssert.AreEqual(
                    matrix[rowIndex, dataColumn],
                    row["2"],
                    ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: ["", "[2]", "XData", "YData", "ZData"],
                    actual: [.. subscriber.PropertyNames]);

                row[0] = -1;
                ComplexAssert.AreEqual(
                    matrix[rowIndex, dataColumn],
                    row["2"],
                ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: ["", "[2]", "XData", "YData", "ZData", "[0]"],
                    actual: [.. subscriber.PropertyNames]);
            }
        }

        [TestMethod()]
        public void IndexGetTest()
        {
            var matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);

            var rows = matrix.AsRowCollection();

            for (int i = 0; i < matrix.NumberOfRows; i++)
            {
                var target = rows[i];

                var actual = target.Index;

                Assert.AreEqual(expected: i, actual: actual);
            }
        }

        [TestMethod()]
        public void IndexSetTest()
        {
            // value is less than 0
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.Index = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is greater than NumberOfColumns - 1
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.Index = matrix.NumberOfColumns;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // Setting Index without changing its value
            {
                var subscriber = new PropertyChangedSubscriber();

                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];

                    target.PropertyChanged += subscriber.PropertyChangedEventHandler;

                    target.Index = i;

                    Assert.AreEqual(expected: i, actual: target.Index);
                }
            }

            // Setting Index changing its value
            {
                var subscriber = new PropertyChangedSubscriber();

                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    [1, 2, 3, 4, 5, 6]);

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                target.PropertyChanged += subscriber.PropertyChangedEventHandler;

                int expectedIndex = 1;
                target.Index = expectedIndex;

                Assert.AreEqual(expectedIndex, target.Index);

                ComplexMatrixAssert.AreEqual(
                    matrix[expectedIndex, ":"], target, ComplexMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void LengthTest()
        {
            ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);
            var rows = matrix.AsRowCollection();

            ComplexMatrixRow target = rows[0];
            var actual = target.Length;

            var expected = matrix.NumberOfColumns;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void XDataTest()
        {
            var matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);
            var rows = matrix.AsRowCollection();

            int dataColumn = 1;

            rows.XDataColumn = dataColumn;

            ComplexMatrixRow row = rows[0];

            // Same value: unspecific notification 
            var subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            Complex expected = matrix[row.Index, dataColumn];
            row.XData = expected;
            ArrayAssert<string>.AreEqual(
                expected: [""],
                actual: [.. subscriber.PropertyNames]);
            var actual = row.XData;
            ComplexAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            // Different value: specific notification 
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            row.XData = -1;
            ComplexAssert.AreEqual(
                matrix[row.Index, dataColumn],
                row.XData,
                ComplexMatrixTest.Accuracy);
            ArrayAssert<string>.AreEqual(
                expected: ["", "XData", "[1]"],
                actual: [.. subscriber.PropertyNames]);
        }

        [TestMethod()]
        public void YDataTest()
        {
            var matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);
            var rows = matrix.AsRowCollection();

            int dataColumn = 1;

            rows.YDataColumn = dataColumn;

            ComplexMatrixRow row = rows[0];

            // Same value: unspecific notification 
            var subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            Complex expected = matrix[row.Index, dataColumn];
            row.YData = expected;
            ArrayAssert<string>.AreEqual(
                expected: [""],
                actual: [.. subscriber.PropertyNames]);
            var actual = row.YData;
            ComplexAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            // Different value: specific notification 
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            row.YData = -1;
            ComplexAssert.AreEqual(
                matrix[row.Index, dataColumn],
                row.YData,
                ComplexMatrixTest.Accuracy);
            ArrayAssert<string>.AreEqual(
                expected: ["", "YData", "[1]"],
                actual: [.. subscriber.PropertyNames]);
        }

        [TestMethod()]
        public void ZDataTest()
        {
            var matrix = ComplexMatrix.Dense(2, 3,
                [1, 2, 3, 4, 5, 6]);
            var rows = matrix.AsRowCollection();

            int dataColumn = 1;

            rows.ZDataColumn = dataColumn;

            ComplexMatrixRow row = rows[0];

            // Same value: unspecific notification 
            var subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            Complex expected = matrix[row.Index, dataColumn];
            row.ZData = expected;
            ArrayAssert<string>.AreEqual(
                expected: [""],
                actual: [.. subscriber.PropertyNames]);
            var actual = row.ZData;
            ComplexAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            // Different value: specific notification 
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;

            row.ZData = -1;
            ComplexAssert.AreEqual(
                matrix[row.Index, dataColumn],
                row.ZData,
                ComplexMatrixTest.Accuracy);
            ArrayAssert<string>.AreEqual(
                expected: ["", "ZData", "[1]"],
                actual: [.. subscriber.PropertyNames]);
        }
    }
}
