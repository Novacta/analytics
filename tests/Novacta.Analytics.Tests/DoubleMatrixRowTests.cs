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

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class DoubleMatrixRowTests
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
                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                        Assert.AreEqual(target[index], (double)current, DoubleMatrixTest.Accuracy);
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

            // IEnumerable<double>.GetEnumerator
            {
                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];
                    IEnumerable<double> enumerable = (IEnumerable<double>)target;

                    IEnumerator<double> enumerator = enumerable.GetEnumerator();

                    int index = 0;
                    double current;

                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        Assert.AreEqual(target[index], current, DoubleMatrixTest.Accuracy);
                        index++;
                    }

                    // reset 
                    enumerator.Reset();

                    Assert.AreEqual(-1, (int)Reflector.GetField(enumerator, "position"));

                    // dispose
                    enumerator.Dispose();
                }
            }

            // IEnumerable<double>.Current fails
            {
                string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var target = rows[i];
                    var enumerable = (IEnumerable<double>)target;

                    var enumerator = enumerable.GetEnumerator();

                    ExceptionAssert.Throw(
                        () =>
                        {
                            double current = enumerator.Current;
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

                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
            var matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });
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
                    Assert.AreEqual(matrix[i, j], row[j], DoubleMatrixTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // matrix has no row or column names
            {
                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { .1, 10.2, -2.3, 1000.2, .2, 239.32 });
                var rows = matrix.AsRowCollection();

                var actual = rows[0].ToString();

                var expected = "0.1              " + "-2.3             " + "0.2              ";

                Assert.AreEqual(expected, actual);
            }

            // matrix has some row or column names
            {
                var matrix = DoubleMatrix.Dense(3, 3,
                    new double[9] { .1, 10.2, 0, -2.3, 1000.2, 0, .2, 239.32, 0 });
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
                matrix.SetColumnName(1, "Col name too much long");

                // No name for column 2

                // Row 0

                var actual = rows[0].ToString();

                var expected =
                    "                 " + "[Col name <  15] " + "[Col name too *] " + "                 " +
                    Environment.NewLine +
                    "[Row name <  15] " + "0.1              " + "-2.3             " + "0.2              ";

                Assert.AreEqual(expected, actual);

                // Row 1 (No name)

                actual = rows[1].ToString();

                expected =
                    "[Col name <  15] " + "[Col name too *] " + "                 " +
                    Environment.NewLine +
                    "10.2             " + "1000.2           " + "239.32           ";

                Assert.AreEqual(expected, actual);

                // Row 2

                actual = rows[2].ToString();

                expected =
                    "                 " + "[Col name <  15] " + "[Col name too *] " + "                 " +
                    Environment.NewLine +
                    "[Row name too *] " + "0                " + "0                " + "0                ";

                Assert.AreEqual(expected, actual);
            }

            // matrix has some row names but no col ones
            {
                var matrix = DoubleMatrix.Dense(3, 3,
                    new double[9] { .1, 10.2, 0, -2.3, 1000.2, 0, .2, 239.32, 0 });
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
                    "[Row name <  15] " + "0.1              " + "-2.3             " + "0.2              ";

                Assert.AreEqual(expected, actual);

                // Row 1 (no name)

                actual = rows[1].ToString();

                expected =
                    "10.2             " + "1000.2           " + "239.32           ";

                Assert.AreEqual(expected, actual);

                // Row 2

                actual = rows[2].ToString();

                expected =
                    "[Row name too *] " + "0                " + "0                " + "0                ";

                Assert.AreEqual(expected, actual);
            }

            // matrix has no row names and some column ones
            {
                var matrix = DoubleMatrix.Dense(3, 3,
                    new double[9] { .1, 10.2, 0, -2.3, 1000.2, 0, .2, 239.32, 0 });
                var rows = matrix.AsRowCollection();

                // COL NAMES

                // Column name with adequate length
                matrix.SetColumnName(0, "Col name <  15");

                // Too long column name
                matrix.SetColumnName(1, "Col name too much long");

                // No name for column 2

                // Row 0

                var actual = rows[0].ToString();

                var expected =
                    "[Col name <  15] " + "[Col name too *] " + "                 " +
                    Environment.NewLine +
                    "0.1              " + "-2.3             " + "0.2              ";

                Assert.AreEqual(expected, actual);

                // Row 1

                actual = rows[1].ToString();

                expected =
                    "[Col name <  15] " + "[Col name too *] " + "                 " +
                    Environment.NewLine +
                    "10.2             " + "1000.2           " + "239.32           ";

                Assert.AreEqual(expected, actual);

                // Row 2

                actual = rows[2].ToString();

                expected =
                    "[Col name <  15] " + "[Col name too *] " + "                 " +
                    Environment.NewLine +
                    "0                " + "0                " + "0                ";

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var matrix = DoubleMatrix.Dense(3, 3,
                new double[9] { 1, 2, 3, 4, 5, 6, 1, 2, 3 },
                StorageOrder.RowMajor);
            var rows = matrix.AsRowCollection();

            var row = rows[0];
            var actual = row.GetHashCode();
            var expected = matrix.NumberOfColumns.GetHashCode();
            foreach (var item in row)
            {
                expected ^= item.GetHashCode();
            }

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DataTest()
        {
            PropertyChangedSubscriber subscriber;
            double expected;
            DoubleMatrixRow row;

            var matrix = DoubleMatrix.Dense(3, 3,
                 new double[9] { 1, 2, 3, 4, 5, 6, 1, 2, 3 },
                 StorageOrder.RowMajor);

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
                expected: new string[] { "" },
                actual: subscriber.PropertyNames.ToArray());

            expected = matrix[rowIndex, rows.YDataColumn];
            row.YData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "" },
                actual: subscriber.PropertyNames.ToArray());

            expected = matrix[rowIndex, rows.ZDataColumn];
            row.ZData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "" },
                actual: subscriber.PropertyNames.ToArray());

            Assert.AreEqual(
                matrix[rowIndex, rows.XDataColumn], 
                row.XData, 
                DoubleMatrixTest.Accuracy);
            Assert.AreEqual(
                matrix[rowIndex, rows.YDataColumn], 
                row.YData, 
                DoubleMatrixTest.Accuracy);
            Assert.AreEqual(
                matrix[rowIndex, rows.ZDataColumn], 
                row.ZData, 
                DoubleMatrixTest.Accuracy);

            // Setting by changing values

            expected = -5.0;
            row.XData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "", "XData", "[2]" },
                actual: subscriber.PropertyNames.ToArray());

            expected = -10.0;
            row.YData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "", "XData", "[2]", "YData", "[1]" },
                actual: subscriber.PropertyNames.ToArray());

            expected = -15.0;
            row.ZData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "", "XData", "[2]", "YData", "[1]", "ZData", "[0]" },
                actual: subscriber.PropertyNames.ToArray());

            Assert.AreEqual(
                matrix[rowIndex, rows.XDataColumn],
                row.XData,
                DoubleMatrixTest.Accuracy);
            Assert.AreEqual(
                matrix[rowIndex, rows.YDataColumn],
                row.YData,
                DoubleMatrixTest.Accuracy);
            Assert.AreEqual(
                matrix[rowIndex, rows.ZDataColumn],
                row.ZData,
                DoubleMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            int expected, actual;
            DoubleMatrix matrix, otherMatrix;
            DoubleMatrixRow thisRow, otherRow;

            matrix = DoubleMatrix.Dense(3, 3,
                new double[9] { 1, 2, 3, 4, 5, 6, 1, 2, 3 },
                StorageOrder.RowMajor);

            var rows = matrix.AsRowCollection();

            // m = [  1  2  3
            //        4  5  6
            //        1  2  3

            // STRONGLY TYPED COMPARISON

            // Length difference > 0

            otherMatrix = DoubleMatrix.Dense(3, 4);
            var otherRows = otherMatrix.AsRowCollection();

            thisRow = rows[0];
            otherRow = otherRows[0];

            expected = -1;
            actual = thisRow.CompareTo(otherRow);

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(thisRow < otherRow);
            Assert.IsTrue(thisRow <= otherRow);
            Assert.IsFalse(thisRow > otherRow);
            Assert.IsFalse(thisRow >= otherRow);

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // Length difference < 0

            otherMatrix = DoubleMatrix.Dense(3, 2);
            otherRows = otherMatrix.AsRowCollection();

            thisRow = rows[0];
            otherRow = otherRows[0];

            expected = 1;
            actual = thisRow.CompareTo(otherRow);

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(thisRow < otherRow);
            Assert.IsFalse(thisRow <= otherRow);
            Assert.IsTrue(thisRow > otherRow);
            Assert.IsTrue(thisRow >= otherRow);

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // Length difference equals 0

            // ----- Rows are equal

            thisRow = rows[0];
            otherRow = rows[2];

            expected = 0;
            actual = thisRow.CompareTo(otherRow);

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(thisRow < otherRow);
            Assert.IsTrue(thisRow <= otherRow);
            Assert.IsFalse(thisRow > otherRow);
            Assert.IsTrue(thisRow >= otherRow);

            Assert.IsFalse(thisRow != otherRow);
            Assert.IsTrue(thisRow == otherRow);
            Assert.IsTrue(thisRow.Equals(otherRow));

            // ----- thisRow less than otherRow

            thisRow = rows[0];
            otherRow = rows[1];

            expected = -1;
            actual = thisRow.CompareTo(otherRow);

            Assert.AreEqual(expected, actual);

            Assert.IsTrue(thisRow < otherRow);
            Assert.IsTrue(thisRow <= otherRow);
            Assert.IsFalse(thisRow > otherRow);
            Assert.IsFalse(thisRow >= otherRow);

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // ----- thisRow greater than otherRow

            thisRow = rows[1];
            otherRow = rows[2];

            expected = 1;
            actual = thisRow.CompareTo(otherRow);

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(thisRow < otherRow);
            Assert.IsFalse(thisRow <= otherRow);
            Assert.IsTrue(thisRow > otherRow);
            Assert.IsTrue(thisRow >= otherRow);

            Assert.IsTrue(thisRow != otherRow);
            Assert.IsFalse(thisRow == otherRow);
            Assert.IsFalse(thisRow.Equals(otherRow));

            // WEAKLY TYPED COMPARISON

            object weakOther;

            // null other

            thisRow = rows[1];
            weakOther = null;

            expected = 1;
            actual = thisRow.CompareTo(weakOther);

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(thisRow.Equals(weakOther));

            // DoubleMatrixRow other 

            thisRow = rows[0];
            weakOther = rows[2];

            expected = 0;
            actual = thisRow.CompareTo(weakOther);

            Assert.AreEqual(expected, actual);

            Assert.IsTrue(thisRow.Equals(weakOther));

            // other not of type DoubleMatrixRow

            thisRow = rows[1];
            weakOther = IndexCollection.Default(2);

            ArgumentExceptionAssert.Throw(
                () =>
                {
                    thisRow.CompareTo(weakOther);
                },
                expectedType: typeof(ArgumentException),
                expectedPartialMessage: String.Format(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_OBJ_HAS_WRONG_TYPE"), "DoubleMatrixRow"),
                expectedParameterName: "obj");

            Assert.IsFalse(thisRow.Equals(weakOther));

            // COMPARISONS INVOLVING NULL OBJECTS

            ComparableObjectTest.CompareToWithNulls(thisRow);

            ComparableObjectTest.EqualsWithNulls(thisRow);

            DoubleMatrixRow leftRow = null;
            DoubleMatrixRow rightRow = rows[0];
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
            DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });
            var rows = matrix.AsRowCollection();

            var actual = rows.Matrix;

            var expected = matrix;
            Assert.IsTrue(object.ReferenceEquals(expected, actual));
        }

        [TestMethod()]
        public void NotifyPropertyChangedTest()
        {
            DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });

            var rows = matrix.AsRowCollection();

            DoubleMatrixRow target = rows[0];

            target.PropertyChanged += new PropertyChangedEventHandler(
                this.PropertyChangedEventHandler);

            string propertyName = "UNKNOWN";
            target.NotifyPropertyChanged(propertyName);
        }

        [TestMethod()]
        public void ToDoubleMatrixTest()
        {
            // value is null
            {
                DoubleMatrix actual = (DoubleMatrixRow)(null);

                Assert.IsNull(actual);

                actual = DoubleMatrixRow.ToDoubleMatrix(null);

                Assert.IsNull(actual);
            }

            // value is not null
            {
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    var expected = matrix[i, ":"];
                    DoubleMatrix actual = rows[i];
                    DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                    actual = DoubleMatrixRow.ToDoubleMatrix(rows[i]);
                    DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void IndexerInt32GetTest()
        {
            // columnIndex is less than 0
            {
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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

                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;

                int rowIndex = 1;
                DoubleMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                Assert.AreEqual(matrix[rowIndex, dataColumn], row[dataColumn]);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[2]", "XData", "YData" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }

        [TestMethod()]
        public void IndexerStringGetTest()
        {
            // columnIndex not representing an integer
            {
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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

                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;

                int rowIndex = 1;
                DoubleMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                Assert.AreEqual(matrix[rowIndex, dataColumn], row["2"]);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[2]", "XData", "YData" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }

        [TestMethod()]
        public void IndexerInt32SetTest()
        {
            // columnIndex is less than 0
            {
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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

                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;
                rows.ZDataColumn = dataColumn;

                int rowIndex = 1;
                DoubleMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                Assert.AreEqual(
                    matrix[rowIndex, dataColumn], 
                    row[dataColumn], 
                    DoubleMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[2]", "XData", "YData", "ZData" },
                    actual: subscriber.PropertyNames.ToArray());

                row[0] = -1;
                Assert.AreEqual(
                    matrix[rowIndex, dataColumn], 
                    row[dataColumn],
                DoubleMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[2]", "XData", "YData", "ZData", "[0]" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }

        [TestMethod()]
        public void IndexerStringSetTest()
        {
            // columnIndex not representing an integer
            {
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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

                var matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 2;
                rows.XDataColumn = dataColumn;
                rows.YDataColumn = dataColumn;
                rows.ZDataColumn = dataColumn;

                int rowIndex = 1;
                DoubleMatrixRow row = rows[rowIndex];
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                row[dataColumn] = -1;
                Assert.AreEqual(
                    matrix[rowIndex, dataColumn],
                    row["2"],
                    DoubleMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[2]", "XData", "YData", "ZData" },
                    actual: subscriber.PropertyNames.ToArray());

                row[0] = -1;
                Assert.AreEqual(
                    matrix[rowIndex, dataColumn],
                    row["2"],
                DoubleMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[2]", "XData", "YData", "ZData", "[0]" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }

        [TestMethod()]
        public void IndexGetTest()
        {
            var matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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
                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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

                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

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

                DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                    new double[6] { 1, 2, 3, 4, 5, 6 });

                var rows = matrix.AsRowCollection();

                var target = rows[0];

                target.PropertyChanged += subscriber.PropertyChangedEventHandler;

                int expectedIndex = 1;
                target.Index = expectedIndex;

                Assert.AreEqual(expectedIndex, target.Index);

                DoubleMatrixAssert.AreEqual(
                    matrix[expectedIndex, ":"], target, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void LengthTest()
        {
            DoubleMatrix matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });
            var rows = matrix.AsRowCollection();

            DoubleMatrixRow target = rows[0];
            var actual = target.Length;

            var expected = matrix.NumberOfColumns;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void XDataTest()
        {
            var matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });
            var rows = matrix.AsRowCollection();

            int dataColumn = 1;

            rows.XDataColumn = dataColumn;

            DoubleMatrixRow row = rows[0];

            // Same value: unspecific notification 
            var subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;
            
            double expected = matrix[row.Index, dataColumn];
            row.XData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "" },
                actual: subscriber.PropertyNames.ToArray());
            var actual = row.XData;
            Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // Different value: specific notification 
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;
            
            row.XData = -1;
            Assert.AreEqual(
                matrix[row.Index, dataColumn],
                row.XData,
                DoubleMatrixTest.Accuracy);
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "", "XData", "[1]" },
                actual: subscriber.PropertyNames.ToArray());            
        }

        [TestMethod()]
        public void YDataTest()
        {
            var matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });
            var rows = matrix.AsRowCollection();

            int dataColumn = 1;

            rows.YDataColumn = dataColumn;

            DoubleMatrixRow row = rows[0];

            // Same value: unspecific notification 
            var subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;
            
            double expected = matrix[row.Index, dataColumn];
            row.YData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "" },
                actual: subscriber.PropertyNames.ToArray());
            var actual = row.YData;
            Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // Different value: specific notification 
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;
            
            row.YData = -1;
            Assert.AreEqual(
                matrix[row.Index, dataColumn],
                row.YData,
                DoubleMatrixTest.Accuracy);
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "", "YData", "[1]" },
                actual: subscriber.PropertyNames.ToArray());
        }

        [TestMethod()]
        public void ZDataTest()
        {
            var matrix = DoubleMatrix.Dense(2, 3,
                new double[6] { 1, 2, 3, 4, 5, 6 });
            var rows = matrix.AsRowCollection();

            int dataColumn = 1;

            rows.ZDataColumn = dataColumn;

            DoubleMatrixRow row = rows[0];

            // Same value: unspecific notification 
            var subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;
            
            double expected = matrix[row.Index, dataColumn];
            row.ZData = expected;
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "" },
                actual: subscriber.PropertyNames.ToArray());
            var actual = row.ZData;
            Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            // Different value: specific notification 
            subscriber = new PropertyChangedSubscriber();
            row.PropertyChanged += subscriber.PropertyChangedEventHandler;
            
            row.ZData = -1;
            Assert.AreEqual(
                matrix[row.Index, dataColumn],
                row.ZData,
                DoubleMatrixTest.Accuracy);
            ArrayAssert<string>.AreEqual(
                expected: new string[] { "", "ZData", "[1]" },
                actual: subscriber.PropertyNames.ToArray());
        }
    }
}
