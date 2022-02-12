// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Numerics;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class ComplexMatrixRowCollectionTests
    {
        [TestMethod()]
        public void ToComplexMatrixTest()
        {
            // value is null
            {
                ComplexMatrix actual = (ComplexMatrixRowCollection)(null);

                Assert.IsNull(actual);

                actual = ComplexMatrixRowCollection.ToComplexMatrix(null);

                Assert.IsNull(actual);
            }

            // value is not null
            {
                ComplexMatrix matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                var expected = matrix;

                var actual = (ComplexMatrix)rows;
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                actual = ComplexMatrixRowCollection.ToComplexMatrix(rows);
                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void AsComplexMatrixTest()
        {
            ComplexMatrix expected = ComplexMatrix.Dense(2, 3,
                new Complex[6] { 1, 2, 3, 4, 5, 6 });

            var doubleMatrixRowCollection = expected.AsRowCollection();

            var actual = doubleMatrixRowCollection;

            ComplexMatrixAssert.IsStateAsExpected(
                expectedState: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] { 1, 2, 3, 4, 5, 6 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                actualMatrix: actual,
                delta: ComplexMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void XDataColumnTest()
        {
            // value is less than 0
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        rows.XDataColumn = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is greater than NumberOfColumns - 1
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        rows.XDataColumn = matrix.NumberOfColumns;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is inside the bounds
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 1;

                rows.XDataColumn = dataColumn;

                ComplexMatrixRow row = rows[0];
                var subscriber = new PropertyChangedSubscriber();
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;

                // Same value: unspecific notification 
                Complex expected = matrix[row.Index, dataColumn];
                row[dataColumn] = expected;
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "" },
                    actual: subscriber.PropertyNames.ToArray());
                var actual = row[dataColumn];
                ComplexAssert.AreEqual(
                    expected,
                    actual,
                    ComplexMatrixTest.Accuracy);

                // Different value: specific notification 
                row[dataColumn] = -1;
                ComplexAssert.AreEqual(
                    matrix[row.Index, dataColumn],
                    row[dataColumn],
                    ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[1]", "XData" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }

        [TestMethod()]
        public void YDataColumnTest()
        {
            // value is less than 0
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        rows.YDataColumn = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is greater than NumberOfColumns - 1
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        rows.YDataColumn = matrix.NumberOfColumns;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is inside the bounds
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 1;

                rows.YDataColumn = dataColumn;

                ComplexMatrixRow row = rows[0];
                var subscriber = new PropertyChangedSubscriber();
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;


                // Same value: unspecific notification
                Complex expected = matrix[row.Index, dataColumn];
                row[dataColumn] = expected;
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "" },
                    actual: subscriber.PropertyNames.ToArray());
                var actual = row[dataColumn];
                ComplexAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                // Different value: specific notification 
                row[dataColumn] = -1;
                ComplexAssert.AreEqual(
                    matrix[row.Index, dataColumn],
                    row[dataColumn],
                    ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[1]", "YData" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }

        [TestMethod()]
        public void ZDataColumnTest()
        {
            // value is less than 0
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        rows.ZDataColumn = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is greater than NumberOfColumns - 1
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        rows.ZDataColumn = matrix.NumberOfColumns;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ROW_DATA_COLUMN_EXCEEDS_DIMS"),
                    expectedParameterName: "value");
            }

            // value is inside the bounds
            {
                var matrix = ComplexMatrix.Dense(2, 3,
                    new Complex[6] { 1, 2, 3, 4, 5, 6 });
                var rows = matrix.AsRowCollection();

                int dataColumn = 1;

                rows.ZDataColumn = dataColumn;

                ComplexMatrixRow row = rows[0];
                var subscriber = new PropertyChangedSubscriber();
                row.PropertyChanged += subscriber.PropertyChangedEventHandler;


                // Same value: unspecific notification 
                Complex expected = matrix[row.Index, dataColumn];
                row[dataColumn] = expected;
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "" },
                    actual: subscriber.PropertyNames.ToArray());
                var actual = row[dataColumn];
                ComplexAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

                // Different value: specific notification 
                row[dataColumn] = -1;
                ComplexAssert.AreEqual(
                    matrix[row.Index, dataColumn],
                    row[dataColumn],
                    ComplexMatrixTest.Accuracy);
                ArrayAssert<string>.AreEqual(
                    expected: new string[] { "", "[1]", "ZData" },
                    actual: subscriber.PropertyNames.ToArray());
            }
        }
    }
}