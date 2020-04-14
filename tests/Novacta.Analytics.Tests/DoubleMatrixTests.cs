// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using Novacta.Analytics.Tests.TestableItems.Addition;
using Novacta.Analytics.Tests.TestableItems.Division;
using Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.TestableItems.Multiplication;
using Novacta.Analytics.Tests.TestableItems.Negation;
using Novacta.Analytics.Tests.TestableItems.Subtraction;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class DoubleMatrixTests
    {
        #region Helpers

        /// <summary>
        /// Gets the list of available <see cref="TestableDoubleMatrix"/> instances.
        /// </summary>
        /// <returns>The list of available <see cref="TestableDoubleMatrix"/> instances.</returns>
        static List<TestableDoubleMatrix> GetTestableMatrices()
        {
            List<TestableDoubleMatrix> TestableItems = new List<TestableDoubleMatrix>
            {
                TestableDoubleMatrix00.Get(),
                TestableDoubleMatrix01.Get(),
                TestableDoubleMatrix02.Get(),
                TestableDoubleMatrix03.Get(),
                TestableDoubleMatrix04.Get(),
                TestableDoubleMatrix05.Get(),
                TestableDoubleMatrix06.Get(),
                TestableDoubleMatrix07.Get(),
                TestableDoubleMatrix08.Get(),
                TestableDoubleMatrix09.Get(),
                TestableDoubleMatrix10.Get(),
                TestableDoubleMatrix11.Get(),
                TestableDoubleMatrix12.Get(),
                TestableDoubleMatrix13.Get(),
                TestableDoubleMatrix14.Get(),
                TestableDoubleMatrix15.Get(),
                TestableDoubleMatrix16.Get(),
                TestableDoubleMatrix17.Get(),
                TestableDoubleMatrix18.Get(),
                TestableDoubleMatrix19.Get(),
                TestableDoubleMatrix20.Get(),
                TestableDoubleMatrix21.Get(),
                TestableDoubleMatrix22.Get(),
                TestableDoubleMatrix23.Get(),
                TestableDoubleMatrix24.Get(),
                TestableDoubleMatrix25.Get(),
                TestableDoubleMatrix26.Get(),
                TestableDoubleMatrix27.Get(),
                TestableDoubleMatrix28.Get(),
                TestableDoubleMatrix29.Get(),
                TestableDoubleMatrix30.Get(),
                TestableDoubleMatrix31.Get(),
                TestableDoubleMatrix32.Get(),
                TestableDoubleMatrix33.Get(),
                TestableDoubleMatrix34.Get(),
                TestableDoubleMatrix35.Get(),
                TestableDoubleMatrix36.Get(),
                TestableDoubleMatrix37.Get(),
                TestableDoubleMatrix38.Get(),
                TestableDoubleMatrix39.Get(),
                TestableDoubleMatrix40.Get(),
                TestableDoubleMatrix41.Get(),
                TestableDoubleMatrix42.Get(),
                TestableDoubleMatrix43.Get(),
                TestableDoubleMatrix44.Get(),
                TestableDoubleMatrix45.Get(),
                TestableDoubleMatrix46.Get(),
                TestableDoubleMatrix47.Get(),
                TestableDoubleMatrix48.Get(),
                TestableDoubleMatrix49.Get(),
                TestableDoubleMatrix50.Get(),
                TestableDoubleMatrix51.Get(),
                TestableDoubleMatrix52.Get(),
                TestableDoubleMatrix53.Get(),
                TestableDoubleMatrix54.Get(),
                TestableDoubleMatrix55.Get(),
                TestableDoubleMatrix56.Get(),
                TestableDoubleMatrix57.Get(),
                TestableDoubleMatrix58.Get()
            };

            return TestableItems;
        }

        /// <summary>
        /// Tests the specified <see cref="Action"/> for each item in the 
        /// given list of <see cref="TestableDoubleMatrix"/> instances.
        /// </summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="TestableItems">The list of <see cref="TestableDoubleMatrix"/> instances 
        /// to test.</param>
        static void TestAction(
            Action<TestableDoubleMatrix> test,
            List<TestableDoubleMatrix> TestableItems)
        {
            for (int i = 0; i < TestableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing matrix {0}", i);
#endif
                test(TestableItems[i]);
            }
        }

        #endregion

        #region IMatrixPatterns

        [TestMethod()]
        public void IsBidiagonalTest()
        {
            TestAction(DoubleMatrixTest.IsBidiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsColumnVectorTest()
        {
            TestAction(DoubleMatrixTest.IsColumnVector, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsDiagonalTest()
        {
            TestAction(DoubleMatrixTest.IsDiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsHessenbergTest()
        {
            TestAction(DoubleMatrixTest.IsHessenberg, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsLowerBidiagonalTest()
        {
            TestAction(DoubleMatrixTest.IsLowerBidiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsLowerHessenbergTest()
        {
            TestAction(DoubleMatrixTest.IsLowerHessenberg, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsLowerTriangularTest()
        {
            TestAction(DoubleMatrixTest.IsLowerTriangular, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsRowVectorTest()
        {
            TestAction(DoubleMatrixTest.IsRowVector, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsScalarTest()
        {
            TestAction(DoubleMatrixTest.IsScalar, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSkewSymmetricTest()
        {
            TestAction(DoubleMatrixTest.IsSkewSymmetric, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSquareTest()
        {
            TestAction(DoubleMatrixTest.IsSquare, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSymmetricTest()
        {
            TestAction(DoubleMatrixTest.IsSymmetric, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsTriangularTest()
        {
            TestAction(DoubleMatrixTest.IsTriangular, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsTridiagonalTest()
        {
            TestAction(DoubleMatrixTest.IsTridiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsUpperBidiagonalTest()
        {
            TestAction(DoubleMatrixTest.IsUpperBidiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsUpperHessenbergTest()
        {
            TestAction(DoubleMatrixTest.IsUpperHessenberg, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsUpperTriangularTest()
        {
            TestAction(DoubleMatrixTest.IsUpperTriangular, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsVectorTest()
        {
            TestAction(DoubleMatrixTest.IsVector, GetTestableMatrices());
        }

        [TestMethod()]
        public void LowerBandwidthTest()
        {
            TestAction(DoubleMatrixTest.LowerBandwidth, GetTestableMatrices());
        }

        [TestMethod()]
        public void UpperBandwidthTest()
        {
            TestAction(DoubleMatrixTest.UpperBandwidth, GetTestableMatrices());
        }

        #endregion

        #region ITabularCollection

        [TestMethod()]
        public void NumberOfRowsTest()
        {
            TestAction(DoubleMatrixTest.NumberOfRows, GetTestableMatrices());
        }

        [TestMethod()]
        public void NumberOfColumnsTest()
        {
            TestAction(DoubleMatrixTest.NumberOfColumns, GetTestableMatrices());
        }

        [TestMethod()]
        public void IndexerGetTest()
        {
            TestAction(DoubleMatrixTest.IndexerGet.AnyRowIndexIsOutOfRange, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerGet.AnyColumnIndexIsOutOfRange, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerGet.RowIndexesIsNull, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerGet.ColumnIndexesIsNull, GetTestableMatrices());

            #region SubMatrix - Without row or column names

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expected: 8.0,
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 1,
                columnIndex: 2);

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[2] { 6, 9 },
                                numberOfRows: 1,
                                numberOfColumns: 2),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 2,
                columnIndexes: IndexCollection.Range(1, 2));

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { 2, 5, 8 },
                                numberOfRows: 1,
                                numberOfColumns: 3),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 1,
                columnIndexes: ":");

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { 4, 5, 6 },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndex: 1);

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { 4, 5, 6, 7, 8, 9 },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: IndexCollection.Range(1, 2));

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { 2, 3, 5, 6, 8, 9 },
                                numberOfRows: 2,
                                numberOfColumns: 3),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(1, 2),
                columnIndexes: ":");

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { 4, 5, 6 },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndex: 1);

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { 4, 5, 6, 7, 8, 9 },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Range(1, 2));

            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                                numberOfRows: 3,
                                numberOfColumns: 3),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion

            #region SubMatrix - With row or column names

            // 0    m[1,[0,2]]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[2] { 1, 5 },
                                numberOfRows: 1,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(1) { { 0, "r1" } },
                                columnNames: new Dictionary<int, string>(2) { { 0, "c0" }, { 1, "c2" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndex: 1,
                columnIndexes: IndexCollection.Sequence(0, 2, 2));

            // 1    m[1,:]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { 1, 3, 5 },
                                numberOfRows: 1,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(1) { { 0, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndex: 1,
                columnIndexes: ":");

            // 2    m[[1],1]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[1] { 3 },
                                numberOfRows: 1,
                                numberOfColumns: 1,
                                rowNames: new Dictionary<int, string>(1) { { 0, "r1" } },
                                columnNames: new Dictionary<int, string>(1) { { 0, "c1" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndexes: IndexCollection.Range(1, 1),
                columnIndex: 1);

            // 3    m[[1,0],[0,1]]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[4] { 1, 0, 3, 2 },
                                numberOfRows: 2,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r1" }, { 1, "r0" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndexes: IndexCollection.Sequence(1, -1, 0),
                columnIndexes: IndexCollection.Range(0, 1));

            // 4    m[[0,1],:]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { 0, 1, 2, 3, 4, 5 },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndexes: IndexCollection.Range(0, 1),
                columnIndexes: ":");

            // 5    m[:,1]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[2] { 2, 3 },
                                numberOfRows: 2,
                                numberOfColumns: 1,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(1) { { 0, "c1" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndexes: ":",
                columnIndex: 1);

            // 6    m[:,[1,0]]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[4] { 2, 3, 0, 1 },
                                numberOfRows: 2,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(2) { { 0, "c1" }, { 1, "c0" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Sequence(1, -1, 0));

            // 7    m[:,:]
            DoubleMatrixTest.IndexerGet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { 0, 1, 2, 3, 4, 5 },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }),
                testableMatrix: TestableDoubleMatrix16.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion
        }

        [TestMethod()]
        public void IndexerSetTest()
        {
            TestAction(DoubleMatrixTest.IndexerSet.AnyRowIndexIsOutOrRange, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerSet.AnyColumnIndexIsOutOrRange, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerSet.RowIndexesIsnull, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerSet.ColumnIndexesIsnull, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerSet.CollectionValueIsNull, GetTestableMatrices());
            TestAction(DoubleMatrixTest.IndexerSet.MismatchedCollectionDimensions, GetTestableMatrices());

            #region SubMatrix

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expected: 8.0,
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 1,
                columnIndex: 2);

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[2] { -6, -9 },
                                numberOfRows: 1,
                                numberOfColumns: 2),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 2,
                columnIndexes: IndexCollection.Range(1, 2));

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { -2, -5, -8 },
                                numberOfRows: 1,
                                numberOfColumns: 3),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 1,
                columnIndexes: ":");

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { -4, -5, -6 },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndex: 1);

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { -4, -5, -6, -7, -8, -9 },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: IndexCollection.Range(1, 2));

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { -2, -3, -5, -6, -8, -9 },
                                numberOfRows: 2,
                                numberOfColumns: 3),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(1, 2),
                columnIndexes: ":");

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[3] { -4, -5, -6 },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndex: 1);

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6] { -4, -5, -6, -7, -8, -9 },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Range(1, 2));

            DoubleMatrixTest.IndexerSet.SubMatrix(
                expectedState: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[9] { -1, -2, -3, -4, -5, -6, -7, -8, -9 },
                                numberOfRows: 3,
                                numberOfColumns: 3),
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion

            #region SourceIsValue

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 0,
                columnIndexes: IndexCollection.Range(0, 2));

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndex: 0,
                columnIndexes: ":");

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndex: 0);

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: IndexCollection.Range(0, 2));

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: ":");

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndex: 0);

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Range(0, 2));

            DoubleMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableDoubleMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion
        }

        #endregion

        #region Names

        [TestMethod()]
        public void NameTest()
        {
            var testableMatrix = TestableDoubleMatrix00.Get();

            var matrix = testableMatrix.Dense;

            matrix.Name = null;

            Assert.IsNull(matrix.Name);

            var expected = "Name";

            matrix.Name = expected;

            Assert.AreEqual(
                expected: expected,
                actual: matrix.Name);

            Assert.AreEqual(
                expected: expected,
                actual: matrix.AsReadOnly().Name);
        }

        #region Row Names

        [TestMethod()]
        public void TryGetRowNameTest()
        {
            #region  Input validation: rowIndex

            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "rowIndex";
                string rowName;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.TryGetRowName(-1, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.TryGetRowName(matrix.NumberOfRows, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.AsReadOnly().TryGetRowName(-1, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.AsReadOnly().TryGetRowName(matrix.NumberOfRows, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                bool indexFound;

                var matrix = TestableDoubleMatrix00.Get().Dense;
                matrix.RemoveAllRowNames();

                indexFound = matrix.TryGetRowName(0, out string rowName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: rowName);

                indexFound = matrix.AsReadOnly().TryGetRowName(0, out rowName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: rowName);

                var expectedName = "Name";
                matrix.SetRowName(1, expectedName);

                indexFound = matrix.TryGetRowName(0, out rowName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: rowName);

                indexFound = matrix.TryGetRowName(1, out rowName);
                Assert.AreEqual(true, indexFound);
                Assert.AreEqual(expected: expectedName, actual: rowName);

                indexFound = matrix.AsReadOnly().TryGetRowName(0, out rowName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: rowName);

                indexFound = matrix.AsReadOnly().TryGetRowName(1, out rowName);
                Assert.AreEqual(true, indexFound);
                Assert.AreEqual(expected: expectedName, actual: rowName);
            }

            #endregion
        }

        [TestMethod()]
        public void SetRowNameTest()
        {
            #region  Input validation: rowIndex

            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "rowIndex";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetRowName(-1, "Name");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetRowName(matrix.NumberOfRows, "Name");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Input validation: rowName

            {
                string STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING");

                string STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE");

                string parameterName = "rowName";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetRowName(0, ":");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetRowName(0, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetRowName(0, "");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetRowName(0, "  ");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                var matrix = TestableDoubleMatrix00.Get().Dense;

                var rowIndex = matrix.NumberOfRows - 1;
                var expectedName = "Name";
                matrix.SetRowName(rowIndex, expectedName);

                Assert.AreEqual(expected: true, actual: matrix.HasRowNames);
                Assert.AreEqual(expected: expectedName, actual: matrix.rowNames[rowIndex]);

                Assert.AreEqual(expected: true, actual: matrix.AsReadOnly().HasRowNames);
                Assert.AreEqual(expected: expectedName, actual: matrix.AsReadOnly().RowNames[rowIndex]);
            }

            #endregion
        }

        [TestMethod()]
        public void RemoveRowNameTest()
        {
            #region  Input validation: rowIndex

            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "rowIndex";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.RemoveRowName(-1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.RemoveRowName(matrix.NumberOfRows);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                // No names in this matrix
                var matrix = TestableDoubleMatrix00.Get().Dense;

                bool rowHasIndex = matrix.RemoveRowName(0);
                Assert.AreEqual(expected: false, actual: rowHasIndex);
            }

            {
                // This matrix has names
                var matrix = TestableDoubleMatrix16.Get().Dense;

                var rowIndex = matrix.NumberOfRows - 1;

                bool rowHasIndex = matrix.RemoveRowName(rowIndex);
                Assert.AreEqual(expected: true, actual: rowHasIndex);

                rowHasIndex = matrix.RemoveRowName(rowIndex);
                Assert.AreEqual(expected: false, actual: rowHasIndex);
            }

            #endregion
        }

        [TestMethod()]
        public void RemoveAllRowNamesTest()
        {
            var matrix = TestableDoubleMatrix16.Get().Dense;

            Assert.AreEqual(expected: true, actual: matrix.HasRowNames);

            matrix.RemoveAllRowNames();

            Assert.AreEqual(expected: false, actual: matrix.HasRowNames);
            Assert.AreEqual(expected: 0, actual: matrix.RowNames.Count);

            Assert.AreEqual(expected: false, actual: matrix.AsReadOnly().HasRowNames);
            Assert.AreEqual(expected: 0, actual: matrix.AsReadOnly().RowNames.Count);
        }

        #endregion

        #region Column Names

        [TestMethod()]
        public void TryGetColumnNameTest()
        {
            #region  Input validation: columnIndex

            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "columnIndex";
                string columnName;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.TryGetColumnName(-1, out columnName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.TryGetColumnName(matrix.NumberOfColumns, out columnName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                bool indexFound;

                var matrix = TestableDoubleMatrix00.Get().Dense;
                matrix.RemoveAllColumnNames();

                indexFound = matrix.TryGetColumnName(0, out string columnName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: columnName);

                indexFound = matrix.AsReadOnly().TryGetColumnName(0, out columnName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: columnName);

                var expectedName = "Name";
                matrix.SetColumnName(1, expectedName);

                indexFound = matrix.TryGetColumnName(0, out columnName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: columnName);

                indexFound = matrix.TryGetColumnName(1, out columnName);
                Assert.AreEqual(true, indexFound);
                Assert.AreEqual(expected: expectedName, actual: columnName);

                indexFound = matrix.AsReadOnly().TryGetColumnName(0, out columnName);
                Assert.AreEqual(expected: false, actual: indexFound);
                Assert.AreSame(expected: null, actual: columnName);

                indexFound = matrix.AsReadOnly().TryGetColumnName(1, out columnName);
                Assert.AreEqual(true, indexFound);
                Assert.AreEqual(expected: expectedName, actual: columnName);
            }

            #endregion
        }

        [TestMethod()]
        public void SetColumnNameTest()
        {
            #region  Input validation: columnIndex

            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "columnIndex";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetColumnName(-1, "Name");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetColumnName(matrix.NumberOfColumns, "Name");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Input validation: columnName

            {
                string STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING");

                string STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE");

                string parameterName = "columnName";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetColumnName(0, ":");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetColumnName(0, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetColumnName(0, "");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.SetColumnName(0, "  ");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                var matrix = TestableDoubleMatrix00.Get().Dense;

                var columnIndex = matrix.NumberOfColumns - 1;
                var expectedName = "Name";
                matrix.SetColumnName(columnIndex, expectedName);

                Assert.AreEqual(expected: true, actual: matrix.HasColumnNames);
                Assert.AreEqual(expected: expectedName, actual: matrix.ColumnNames[columnIndex]);

                Assert.AreEqual(expected: true, actual: matrix.AsReadOnly().HasColumnNames);
                Assert.AreEqual(expected: expectedName, actual: matrix.AsReadOnly().ColumnNames[columnIndex]);
            }

            #endregion
        }

        [TestMethod()]
        public void RemoveColumnNameTest()
        {
            #region  Input validation: columnIndex

            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS");

                string parameterName = "columnIndex";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.RemoveColumnName(-1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableDoubleMatrix00.Get().Dense;
                        matrix.RemoveColumnName(matrix.NumberOfColumns);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                // No names in this matrix
                var matrix = TestableDoubleMatrix00.Get().Dense;

                bool columnHasIndex = matrix.RemoveColumnName(0);
                Assert.AreEqual(expected: false, actual: columnHasIndex);
            }

            {
                // This matrix has names
                var matrix = TestableDoubleMatrix16.Get().Dense;

                var columnIndex = matrix.NumberOfColumns - 1;

                bool columnHasIndex = matrix.RemoveColumnName(columnIndex);
                Assert.AreEqual(expected: true, actual: columnHasIndex);

                columnHasIndex = matrix.RemoveColumnName(columnIndex);
                Assert.AreEqual(expected: false, actual: columnHasIndex);
            }

            #endregion
        }

        [TestMethod()]
        public void RemoveAllColumnNamesTest()
        {
            var matrix = TestableDoubleMatrix16.Get().Dense;

            Assert.AreEqual(expected: true, actual: matrix.HasColumnNames);

            matrix.RemoveAllColumnNames();

            Assert.AreEqual(expected: false, actual: matrix.HasColumnNames);
            Assert.AreEqual(expected: 0, actual: matrix.ColumnNames.Count);


            Assert.AreEqual(expected: false, actual: matrix.AsReadOnly().HasColumnNames);
            Assert.AreEqual(expected: 0, actual: matrix.AsReadOnly().ColumnNames.Count);
        }

        #endregion

        #endregion

        #region Conversion methods and operators

        [TestMethod()]
        public void AsColumnMajorDenseArrayTest()
        {
            TestAction(DoubleMatrixTest.AsColumnMajorDenseArray, GetTestableMatrices());
        }

        [TestMethod()]
        public void AsReadOnlyTest()
        {
            TestAction(DoubleMatrixTest.AsReadOnly, GetTestableMatrices());
        }

        [TestMethod()]
        public void AsRowCollectionTest()
        {
            var target = DoubleMatrix.Dense(
                3,
                2,
                new double[6] { 0, 2, 0, 3, 0, Double.NaN },
                StorageOrder.RowMajor);

            target.SetRowName(0, "Row0");
            target.SetRowName(1, "Row1");
            target.SetRowName(2, "Row2");

            // target = 
            // [Row0] [  0   2   
            // [Row1]    0   3
            // [Row2]    0   NaN ]

            // All rows
            {
                var rowCollection = target.AsRowCollection();

                DoubleMatrix actual = rowCollection;

                var expected = DoubleMatrix.Dense(3, 2,
                    new double[6] { 0, 2, 0, 3, 0, Double.NaN },
                    StorageOrder.RowMajor);

                expected.SetRowName(0, "Row0");
                expected.SetRowName(1, "Row1");
                expected.SetRowName(2, "Row2");

                DoubleMatrixAssert.AreEqual(
                    expected, actual, DoubleMatrixTest.Accuracy);
            }

            string parameterName = "rowIndexes";

            // rowIndexes is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target.AsRowCollection(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // rowIndexes exceeds dimensions
            {
                string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = target.AsRowCollection(IndexCollection.Range(0, target.NumberOfRows));
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);
            }

            // rowIndexes is not null
            {
                var rowIndexes = IndexCollection.Sequence(0, 2, 2);

                var rowCollection = target.AsRowCollection(rowIndexes);

                DoubleMatrix actual = rowCollection;

                var expected = DoubleMatrix.Dense(2, 2,
                    new double[4] { 0, 2, 0, Double.NaN },
                    StorageOrder.RowMajor);

                expected.SetRowName(0, "Row0");
                expected.SetRowName(1, "Row2");

                DoubleMatrixAssert.AreEqual(
                    expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void FromIndexPartitionTest()
        {
            string parameterName = "value";

            // value is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        DoubleMatrix actual = ((DoubleMatrix)(IndexPartition<double>)(null));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        DoubleMatrix actual = DoubleMatrix.FromIndexPartition(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // value is not null
            {
                var expected = DoubleMatrix.Dense(6, 1, new double[6] { 0, 0, 1, 1, 0, 1 });
                var value = IndexPartition.Create(expected);
                var actual = ((DoubleMatrix)(value));

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

                actual = DoubleMatrix.FromIndexPartition(value);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }

        }

        [TestMethod()]
        public void FromReadOnlyDoubleMatrixTest()
        {
            string parameterName = "value";

            // value is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        DoubleMatrix actual = ((DoubleMatrix)(ReadOnlyDoubleMatrix)(null));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        DoubleMatrix actual = DoubleMatrix.FromReadOnlyDoubleMatrix(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // value is not null
            {
                var expected = DoubleMatrix.Dense(2, 3, -1.0);
                var value = expected.AsReadOnly();
                var actual = ((DoubleMatrix)(value));

                DoubleMatrixAssert.AreEqual(value.matrix, actual, DoubleMatrixTest.Accuracy);

                actual = DoubleMatrix.FromReadOnlyDoubleMatrix(value);

                DoubleMatrixAssert.AreEqual(value.matrix, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void FromDoubleTest()
        {
            double value = 1.2;

            DoubleMatrix expected = DoubleMatrix.Dense(1, 1, value);

            DoubleMatrix actual = (DoubleMatrix)value;

            DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);

            actual = DoubleMatrix.FromDouble(value);

            DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
        }

        [TestMethod]
        public void ToDoubleTest()
        {
            string parameterName = "value";

            // DoubleMatrix
            {
                // value is null
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = ((double)(DoubleMatrix)(null));
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = DoubleMatrix.ToDouble(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                // value is not scalar
                {
                    string STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR" });

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = ((double)DoubleMatrix.Dense(2, 3));
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = DoubleMatrix.ToDouble(DoubleMatrix.Dense(2, 3));
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);
                }

                // value is scalar
                {
                    var value = DoubleMatrix.Dense(1, 1, -1.0);
                    var actual = ((double)(value));

                    Assert.AreEqual(value[0], actual, DoubleMatrixTest.Accuracy);

                    actual = DoubleMatrix.ToDouble(value);

                    Assert.AreEqual(value[0], actual, DoubleMatrixTest.Accuracy);
                }
            }

            // ReadOnlyDoubleMatrix
            {
                // value is null
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = ((double)(ReadOnlyDoubleMatrix)(null));
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = ReadOnlyDoubleMatrix.ToDouble(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                // value is not scalar
                {
                    string STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR" });

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = ((double)DoubleMatrix.Dense(2, 3).AsReadOnly());
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            double actual = ReadOnlyDoubleMatrix.ToDouble(
                                DoubleMatrix.Dense(2, 3).AsReadOnly());
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_DOUBLE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);
                }

                // value is scalar
                {
                    var value = DoubleMatrix.Dense(1, 1, -1.0).AsReadOnly();
                    var actual = ((double)(value));

                    Assert.AreEqual(value[0], actual, DoubleMatrixTest.Accuracy);

                    actual = ReadOnlyDoubleMatrix.ToDouble(value);

                    Assert.AreEqual(value[0], actual, DoubleMatrixTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string expected, actual;
            DoubleMatrix target;

            // matrix has no row or column names

            target = DoubleMatrix.Dense(2, 3, new double[6] { .1, 10.2, -2.3, 1000.2, .2, 239.32 });

            //         "                 " 17 char length

            //         first row
            expected = "0.1              " + "-2.3             " + "0.2              "
                     //         newline char
                     + Environment.NewLine
                     //         second row
                     + "10.2             " + "1000.2           " + "239.32           "
                     //         newline char
                     + Environment.NewLine
                     //         final newline char
                     + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);

            // matrix has some row or column names

            target = DoubleMatrix.Dense(3, 3, new double[9] { .1, 10.2, 0, -2.3, 1000.2, 0, .2, 239.32, 0 });

            //                "                 " 17 char length

            // ROW NAMES 

            // Row name with adequate length
            target.SetRowName(0, "Row name <  15");

            // No name for row 1

            // Too long row name
            target.SetRowName(2, "Row name too much long");

            // COL NAMES

            // Column name with adequate length
            target.SetColumnName(0, "Col name <  15");

            // Too long column name
            target.SetColumnName(1, "Col name too much long");

            // No name for column 2


            expected = "                 " + "[Col name <  15] " + "[Col name too *] " + "                 "
                     //         newline char
                     + Environment.NewLine
                     //         first row
                     + "[Row name <  15] " + "0.1              " + "-2.3             " + "0.2              "
                     //         newline char
                     + Environment.NewLine
                     //         second row
                     + "                 " + "10.2             " + "1000.2           " + "239.32           "
                     //         newline char
                     + Environment.NewLine
                     //         third row
                     + "[Row name too *] " + "0                " + "0                " + "0                "
                     //         newline char
                     + Environment.NewLine
                     //         final newline char
                     + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);

            // matrix has some row names but no col ones

            target = DoubleMatrix.Dense(3, 3, new double[9] { .1, 10.2, 0, -2.3, 1000.2, 0, .2, 239.32, 0 });

            //                "                 " 17 char length

            // ROW NAMES 

            // Row name with adequate length
            target.SetRowName(0, "Row name <  15");

            // No name for row 1

            // Too long row name
            target.SetRowName(2, "Row name too much long");

            //         first row
            expected = "[Row name <  15] " + "0.1              " + "-2.3             " + "0.2              "
                     //         newline char
                     + Environment.NewLine
                     //         second row
                     + "                 " + "10.2             " + "1000.2           " + "239.32           "
                     //         newline char
                     + Environment.NewLine
                     //         third row
                     + "[Row name too *] " + "0                " + "0                " + "0                "
                     //         newline char
                     + Environment.NewLine
                     //         final newline char
                     + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);

            // matrix has no row names and some column ones

            target = DoubleMatrix.Dense(3, 3, new double[9] { .1, 10.2, 0, -2.3, 1000.2, 0, .2, 239.32, 0 });

            //                "                 " 17 char length

            // COL NAMES

            // Column name with adequate length
            target.SetColumnName(0, "Col name <  15");

            // Too long column name
            target.SetColumnName(1, "Col name too much long");

            // No name for column 2


            expected = "[Col name <  15] " + "[Col name too *] " + "                 "
                     //         newline char
                     + Environment.NewLine
                     //         first row
                     + "0.1              " + "-2.3             " + "0.2              "
                     //         newline char
                     + Environment.NewLine
                     //         second row
                     + "10.2             " + "1000.2           " + "239.32           "
                     //         newline char
                     + Environment.NewLine
                     //         third row
                     + "0                " + "0                " + "0                "
                     //         newline char
                     + Environment.NewLine
                     //         final newline char
                     + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Construction

        [TestMethod()]
        public void DisposeTest()
        {
            DoubleMatrix target = DoubleMatrix.Dense(2, 3);

            DoubleMatrix view = target[":", ":", true];

            // Here view.matrixImplementor is of type ViewDoubleMatrixImplementor.

            target.Dispose();

            // Here view.matrixImplementor is of type DenseDoubleMatrixImplementor.

            Assert.AreEqual(
                StorageScheme.Dense,
                view.implementor.StorageScheme);
        }


        [TestMethod()]
        public void CloneTest()
        {
            TestAction(DoubleMatrixTest.Clone, GetTestableMatrices());
        }

        [TestMethod()]
        public void DenseTest()
        {
            string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                (string)Reflector.ExecuteStaticMember(
                    typeof(ImplementationServices),
                    "GetResourceString",
                    new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

            string STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS =
                (string)Reflector.ExecuteStaticMember(
                    typeof(ImplementationServices),
                    "GetResourceString",
                    new string[] { "STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS" });

            string STR_EXCEPT_NOT_FIELD_OF_STORAGE_ORDER =
                (string)Reflector.ExecuteStaticMember(
                    typeof(ImplementationServices),
                    "GetResourceString",
                    new string[] { "STR_EXCEPT_NOT_FIELD_OF_STORAGE_ORDER" });

            string STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS =
                (string)Reflector.ExecuteStaticMember(
                    typeof(ImplementationServices),
                    "GetResourceString",
                    new string[] { "STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS" });

            #region Dense(int,int)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6],
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                Assert.AreEqual(StorageOrder.ColumnMajor, actual.AsReadOnly().StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.AsReadOnly().StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6], actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,double)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1, 1.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0, 1.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, -2.0);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6] { -2, -2, -2, -2, -2, -2 },
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6] { -2, -2, -2, -2, -2, -2 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,double[])

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1, Array.Empty<double>());
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0, Array.Empty<double>());
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 1, (double[])null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // Inconsistency between data and dimensions
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new double[5]);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                double[] data = new double[6] { 0, 1, 2, 3, 4, 5 };
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, data);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: data,
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,double[],bool)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1, Array.Empty<double>(), true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0, Array.Empty<double>(), true);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 1, (double[])null, true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // Inconsistency between data and dimensions
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new double[5], true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input - copyData is true
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                double[] data = new double[6] { 0, 1, 2, 3, 4, 5 };
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, data, true);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: data,
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(
                    new double[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);

                Assert.AreNotSame(data, actual.GetStorage());
            }

            // Valid input - copyData is false
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                double[] data = new double[6] { 0, 1, 2, 3, 4, 5 };
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, data, false);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: data,
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(
                    new double[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);

                Assert.AreSame(data, actual.GetStorage());
            }

            #endregion

            #region Dense(int,int,double[],StorageOrder)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1, Array.Empty<double>(), StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0, Array.Empty<double>(), StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 1, (double[])null, StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // Invalid StorageOrder
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new double[6] { 0, 1, 2, 3, 4, 5 }, (StorageOrder)(-1));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_NOT_FIELD_OF_STORAGE_ORDER,
                    expectedParameterName: "storageOrder");
            }

            // Inconsistency between data and dimensions
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new double[5], StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                double[] data = new double[6] { 0, 1, 2, 3, 4, 5 };
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, data, StorageOrder.RowMajor);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6] { 0, 3, 1, 4, 2, 5 },
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6] { 0, 3, 1, 4, 2, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,IEnumerable<double>)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1, new List<double>());
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0, new List<double>());
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 1, (List<double>)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // Inconsistency between data and dimensions
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new List<double>() { 0, 1, 2, 3, 4 });
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var data = new List<double>() { 0, 1, 2, 3, 4, 5 };
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, data);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: data.ToArray(),
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,IEnumerable<double>,StorageOrder)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(0, 1, new List<double>(), StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 0, new List<double>(), StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(1, 1, (List<double>)null, StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // Invalid StorageOrder
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new List<double>() { 0, 1, 2, 3, 4, 5 }, (StorageOrder)(-1));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_NOT_FIELD_OF_STORAGE_ORDER,
                    expectedParameterName: "storageOrder");
            }

            // Inconsistency between data and dimensions
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(2, 3, new List<double>() { 0, 1, 2, 3, 4 }, StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var data = new List<double>() { 0, 1, 2, 3, 4, 5 };
                var actual = DoubleMatrix.Dense(numberOfRows, numberOfColumns, data, StorageOrder.RowMajor);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6] { 0, 3, 1, 4, 2, 5 },
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6] { 0, 3, 1, 4, 2, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(double[,])

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense((double[,])null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(new double[0, 1]);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS,
                    expectedParameterName: "data");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Dense(new double[1, 0]);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                var data = new double[2, 3] { { 0, 2, 4 }, { 1, 3, 5 } };
                var actual = DoubleMatrix.Dense(data);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6] { 0, 1, 2, 3, 4, 5 },
                        numberOfRows: data.GetLength(0),
                        numberOfColumns: data.GetLength(1)),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            #endregion
        }

        [TestMethod()]
        public void DiagonalTest()
        {
            // mainDiagonal is null
            {
                // DoubleMatrix
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Diagonal((DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "mainDiagonal");

                // ReadOnlyDoubleMatrix
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Diagonal((ReadOnlyDoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "mainDiagonal");
            }

            // DoubleMatrix mainDiagonal is not null
            {
                var mainDiagonal = DoubleMatrix.Dense(2, 2, new double[4] { 0, 2, 1, 3 });
                // diagonal = 
                // [  0   1
                //    2   3

                var actual = DoubleMatrix.Diagonal(mainDiagonal);

                var expected = DoubleMatrix.Dense(4, 4);

                for (int i = 0; i < mainDiagonal.Count; i++)
                {
                    expected[i, i] = mainDiagonal[i];
                }

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[4] { 2, 1, 3, 0 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }

            // ReadOnlyDoubleMatrix mainDiagonal is not null
            {
                var mainDiagonal =
                    DoubleMatrix.Dense(2, 2, new double[4] { 0, 2, 1, 3 })
                    .AsReadOnly();
                // diagonal = 
                // [  0   1
                //    2   3

                var actual = DoubleMatrix.Diagonal(mainDiagonal);

                var expected = DoubleMatrix.Dense(4, 4);

                for (int i = 0; i < mainDiagonal.Count; i++)
                {
                    expected[i, i] = mainDiagonal[i];
                }

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[4] { 2, 1, 3, 0 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void IdentityTest()
        {
            // dimension < 1
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Identity(0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "dimension");

            }

            // dimension >= 1
            {
                var actual = DoubleMatrix.Identity(3);

                // actual = 
                // [  1   0   0
                //    0   1   0
                //    0   0   1  ]

                var expected = new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[9] { 1, 0, 0, 0, 1, 0, 0, 0, 1 },
                    numberOfRows: 3,
                    numberOfColumns: 3);

                DoubleMatrixAssert.IsStateAsExpected(expected, actual, DoubleMatrixTest.Accuracy);
                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                DoubleArrayAssert.AreEqual(new double[3] { 1, 1, 1 }, actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void SparseTest()
        {
            string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                (string)Reflector.ExecuteStaticMember(
                    typeof(ImplementationServices),
                    "GetResourceString",
                    new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Sparse(0, 1, 2);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfRows");
            }

            // numberOfColumns < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Sparse(1, 0, 2);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // capacity < 0
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = DoubleMatrix.Sparse(1, 1, -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: "capacity");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                int capacity = 0;
                var actual = DoubleMatrix.Sparse(numberOfRows, numberOfColumns, capacity);

                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[6],
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                Assert.AreEqual(StorageOrder.RowMajor, actual.AsReadOnly().StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.AsReadOnly().StorageScheme);
                DoubleArrayAssert.AreEqual(Array.Empty<double>(), actual.GetStorage(), DoubleMatrixTest.Accuracy);
            }
        }

        #endregion

        #region Find

        [TestMethod()]
        public void FindTest()
        {
            DoubleMatrixTest.Find.Value(
                testableMatrix: TestableDoubleMatrix35.Get(),
                value: 0.0,
                expected: IndexCollection.FromArray(new int[] { 0, 3, 4 }));

            DoubleMatrixTest.Find.Value(
                testableMatrix: TestableDoubleMatrix35.Get(),
                value: Double.NaN,
                expected: IndexCollection.FromArray(new int[] { 5 }));

            DoubleMatrixTest.Find.Value(
                testableMatrix: TestableDoubleMatrix36.Get(),
                value: 1.0,
                expected: null);

            DoubleMatrixTest.Find.Value(
                testableMatrix: TestableDoubleMatrix37.Get(),
                value: 2.0,
                expected: IndexCollection.FromArray(new int[] { 1, 2, 4 }));
        }

        [TestMethod()]
        public void FindNonzeroTest()
        {
            DoubleMatrixTest.Find.Nonzero(
                testableMatrix: TestableDoubleMatrix35.Get(),
                expected: IndexCollection.FromArray(new int[] { 1, 2, 5 }));

            DoubleMatrixTest.Find.Nonzero(
                testableMatrix: TestableDoubleMatrix36.Get(),
                expected: null);
        }

        [TestMethod()]
        public void FindWhileTest()
        {
            DoubleMatrixTest.Find.While.MatchIsNull(TestableDoubleMatrix35.Get());

            DoubleMatrixTest.Find.While.Succeed(
                testableMatrix: TestableDoubleMatrix35.Get(),
                match: (d) => { return d <= 1; },
                expected: IndexCollection.FromArray(new int[] { 0, 3, 4 }));

            DoubleMatrixTest.Find.While.Succeed(
                testableMatrix: TestableDoubleMatrix35.Get(),
                match: (d) => { return d < -1.0; },
                expected: null);

            DoubleMatrixTest.Find.While.Succeed(
                testableMatrix: TestableDoubleMatrix35.Get(),
                match: (d) => { return d >= 0; },
                expected: IndexCollection.FromArray(new int[] { 0, 1, 2, 3, 4 }));
        }

        #endregion

        #region Algebraic operators

        [TestMethod()]
        public void AddTest()
        {
            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftMatrixRightMatrix.LeftIsNull(LeftIsNullMatrixMatrixAddition.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.RightIsNull(RightIsNullMatrixMatrixAddition.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongRowsMatrixMatrixAddition.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongColsMatrixMatrixAddition.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(LeftIsScalarMatrixMatrixAddition.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsScalarMatrixMatrixAddition.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(TypicalMatrixMatrixAddition.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftMatrixRightScalar.LeftIsNull(LeftIsNullMatrixScalarAddition.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(RightIsNeutralMatrixScalarAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(TypicalMatrixScalarAddition.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftScalarRightMatrix.RightIsNull(RightIsNullScalarMatrixAddition.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(LeftIsNeutralScalarMatrixAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(TypicalScalarMatrixAddition.Get());
        }

        [TestMethod()]
        public void DivideTest()
        {
            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftMatrixRightMatrix.LeftIsNull(LeftIsNullMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.RightIsNull(RightIsNullMatrixMatrixDivision.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongColsMatrixMatrixDivision.Get());

            // ----- Unsquare, rank deficient right operand
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightIsRectangularAndRankDeficientMatrixMatrixDivision.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(LeftIsScalarMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsScalarMatrixMatrixDivision.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsNoPatternedSquareMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsLowerTriangularMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsUpperTriangularMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsHessenbergMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsSymmetricMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsRectangularLessRowsThanColsMatrixMatrixDivision.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsRectangularNotLessRowsThanColsMatrixMatrixDivision.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftMatrixRightScalar.LeftIsNull(LeftIsNullMatrixScalarDivision.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(RightIsNeutralMatrixScalarDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(TypicalMatrixScalarDivision.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftScalarRightMatrix.RightIsNull(RightIsNullScalarMatrixDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(TypicalScalarMatrixDivision.Get());
        }

        [TestMethod()]
        public void ElementWiseMultiplyTest()
        {
            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftMatrixRightMatrix.LeftIsNull(LeftIsNullMatrixMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.RightIsNull(RightIsNullMatrixMatrixElementWiseMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongRowsMatrixMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongColsMatrixMatrixElementWiseMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(TypicalMatrixMatrixElementWiseMultiplication.Get());
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftMatrixRightMatrix.LeftIsNull(LeftIsNullMatrixMatrixMultiplication.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.RightIsNull(RightIsNullMatrixMatrixMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongRowsMatrixMatrixMultiplication.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(LeftIsScalarMatrixMatrixMultiplication.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsScalarMatrixMatrixMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(TypicalMatrixMatrixMultiplication.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftMatrixRightScalar.LeftIsNull(LeftIsNullMatrixScalarMultiplication.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(RightIsNeutralMatrixScalarMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(TypicalMatrixScalarMultiplication.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftScalarRightMatrix.RightIsNull(RightIsNullScalarMatrixMultiplication.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(LeftIsNeutralScalarMatrixMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(TypicalScalarMatrixMultiplication.Get());
        }

        [TestMethod()]
        public void NegateTest()
        {
            // ----- Null operand
            UnaryOperationTest.OperandIsNull(OperandIsNullMatrixNegation.Get());

            // ----- Typical operand
            UnaryOperationTest.Succeed(TypicalMatrixNegation.Get());
        }

        [TestMethod()]
        public void SubtractTest()
        {
            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftMatrixRightMatrix.LeftIsNull(LeftIsNullMatrixMatrixSubtraction.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.RightIsNull(RightIsNullMatrixMatrixSubtraction.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongRowsMatrixMatrixSubtraction.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Fail(RightWrongColsMatrixMatrixSubtraction.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(LeftIsScalarMatrixMatrixSubtraction.Get());
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(RightIsScalarMatrixMatrixSubtraction.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftMatrixRightMatrix.Succeed(TypicalMatrixMatrixSubtraction.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftMatrixRightScalar.LeftIsNull(LeftIsNullMatrixScalarSubtraction.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(RightIsNeutralMatrixScalarSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftMatrixRightScalar.Succeed(TypicalMatrixScalarSubtraction.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftScalarRightMatrix.RightIsNull(RightIsNullScalarMatrixSubtraction.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(LeftIsNeutralScalarMatrixSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftScalarRightMatrix.Succeed(TypicalScalarMatrixSubtraction.Get());
        }

        #endregion

        [TestMethod]
        public void GetObjectDataTest()
        {
            // info is null
            {
                var testableMatrix = TestableDoubleMatrix00.Get();
                var matrix = testableMatrix.Dense;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        matrix.GetObjectData(
                            info: null,
                            context: new System.Runtime.Serialization.StreamingContext());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "info");
            }
        }

        [TestMethod]
        public void SerializableTest()
        {
            #region DoubleMatrix 

            // info is null
            {
                string parameterName = "info";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                ConstructorInfo serializationCtor = null;
                TypeInfo t = typeof(DoubleMatrix).GetTypeInfo();
                IEnumerable<ConstructorInfo> ctors = t.DeclaredConstructors;
                foreach (var ctor in ctors)
                {
                    var parameters = ctor.GetParameters();
                    if (parameters.Length == 2)
                    {
                        if ((parameters[0].ParameterType == typeof(SerializationInfo))
                            &&
                            (parameters[1].ParameterType == typeof(StreamingContext)))
                        {
                            serializationCtor = ctor;
                            break;
                        }
                    }
                }

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        serializationCtor.Invoke(
                            new object[] { null, new StreamingContext() });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }

            #region Without names

            // View
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.View;

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (DoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Dense
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.Dense;

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (DoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.Sparse;

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (DoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region With names

            // View
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.View;

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (DoubleMatrix)formatter.Deserialize(ms);

                serializedMatrix[0] = -1001.2;
                deserializedMatrix[0] = -1001.2;

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Dense
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.Dense;

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (DoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.Sparse;

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (DoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #endregion

            #region ReadOnlyDoubleMatrix 

            #region Without names

            // View
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.View.AsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (ReadOnlyDoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Dense
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.Dense.AsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (ReadOnlyDoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix11.Get();
                var serializedMatrix = testableMatrix.Sparse.AsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (ReadOnlyDoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #region With names

            // View
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.View.AsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (ReadOnlyDoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Dense
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.Dense.AsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (ReadOnlyDoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Sparse
            {
                MemoryStream ms = new MemoryStream();

                var testableMatrix = TestableDoubleMatrix16.Get();
                var serializedMatrix = testableMatrix.Sparse.AsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedMatrix);

                // Reset stream position
                ms.Position = 0;

                var deserializedMatrix = (ReadOnlyDoubleMatrix)formatter.Deserialize(ms);

                DoubleMatrixAssert.AreEqual(
                    expected: serializedMatrix,
                    actual: deserializedMatrix,
                    delta: DoubleMatrixTest.Accuracy);
            }

            #endregion

            #endregion
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            // DoubleMatrix
            {
                // IEnumerable.GetEnumerator
                {
                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 });
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

                // IEnumerable<double>.GetEnumerator
                {
                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 });
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

                // IEnumerable<double>.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 });
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

                // IEnumerable.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 });
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

            // ReadOnlyDoubleMatrix
            {
                // IEnumerable.GetEnumerator
                {
                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 }).AsReadOnly();
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

                // IEnumerable<double>.GetEnumerator
                {
                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 }).AsReadOnly();
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

                // IEnumerable<double>.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 }).AsReadOnly();
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

                // IEnumerable.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = DoubleMatrix.Dense(2, 3, new double[6] { 1, 2, 3, 4, 5, 6 }).AsReadOnly();
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
        public void ApplyTest()
        {
            TestAction(DoubleMatrixTest.Apply.InPlace.FuncIsNull, GetTestableMatrices());

            TestAction(DoubleMatrixTest.Apply.InPlace.Succeed, GetTestableMatrices());

            TestAction(DoubleMatrixTest.Apply.OutPlace.FuncIsNull, GetTestableMatrices());

            // Add 0
            {
                double addZero(double x) { return x + 0.0; }

                DoubleMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableDoubleMatrix16.Get(),
                    func: addZero,
                    expected: new DoubleMatrixState(
                                    asColumnMajorDenseArray: new double[6]
                                        { 0, 1, 2, 3, 4, 5 },
                                    numberOfRows: 2,
                                    numberOfColumns: 3));

                DoubleMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableDoubleMatrix38.Get(),
                    func: addZero,
                    expected: new DoubleMatrixState(
                                    asColumnMajorDenseArray: new double[20]
                                        {
                                        0, 0, 0, 0, 0,
                                        0, 0, 2, 3, 5,
                                        0, 6, 0, 7, 0,
                                        0, 4, 0, 0, 0
                                        },
                                    numberOfRows: 4,
                                    numberOfColumns: 5));

                DoubleMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableDoubleMatrix39.Get(),
                    func: addZero,
                    expected: new DoubleMatrixState(
                                    asColumnMajorDenseArray: new double[30]
                                        {
                                        0, 0, 3,
                                        0, 0, 0,
                                        3, 5, 1,
                                        0, 7, 0,
                                        4, 0, 0,
                                        0, 0, 0,
                                        0, 2, 0,
                                        0, 1, 0,
                                        1, 0, 0,
                                        0, 0, 1
                                        },
                                    numberOfRows: 3,
                                    numberOfColumns: 10));
            }

            // Add 1
            {
                double addOne(double x) { return x + 1.0; }

                DoubleMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableDoubleMatrix16.Get(),
                    func: addOne,
                    expected: new DoubleMatrixState(
                                    asColumnMajorDenseArray: new double[6]
                                        { 1, 2, 3, 4, 5, 6 },
                                    numberOfRows: 2,
                                    numberOfColumns: 3));

                DoubleMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableDoubleMatrix38.Get(),
                    func: addOne,
                    expected: new DoubleMatrixState(
                                    asColumnMajorDenseArray: new double[20]
                                        {
                                        1, 1, 1, 1, 1,
                                        1, 1, 3, 4, 6,
                                        1, 7, 1, 8, 1,
                                        1, 5, 1, 1, 1
                                        },
                                    numberOfRows: 4,
                                    numberOfColumns: 5));

                DoubleMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableDoubleMatrix39.Get(),
                    func: addOne,
                    expected: new DoubleMatrixState(
                                    asColumnMajorDenseArray: new double[30]
                                        {
                                        1, 1, 4,
                                        1, 1, 1,
                                        4, 6, 2,
                                        1, 8, 1,
                                        5, 1, 1,
                                        1, 1, 1,
                                        1, 3, 1,
                                        1, 2, 1,
                                        2, 1, 1,
                                        1, 1, 2
                                        },
                                    numberOfRows: 3,
                                    numberOfColumns: 10));
            }
        }


        [TestMethod()]
        public void TransposeTest()
        {

            TestAction(DoubleMatrixTest.Transpose.InPlace, GetTestableMatrices());


            DoubleMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableDoubleMatrix38.Get(),
                expected: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[20]
                                    {
                                      0, 0, 3, 0, 4,
                                      0, 0, 5, 7, 0,
                                      0, 0, 0, 0, 0,
                                      0, 2, 6, 0, 0
                                    },
                                numberOfRows: 5,
                                numberOfColumns: 4));

            DoubleMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableDoubleMatrix39.Get(),
                expected: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[30]
                                    {
                                      0, 0, 3, 0, 4, 0, 0, 0, 1, 0,
                                      0, 0, 5, 7, 0, 0, 2, 1, 0, 0,
                                      3, 0, 1, 0, 0, 0, 0, 0, 0, 1
                                    },
                                numberOfRows: 10,
                                numberOfColumns: 3));

            DoubleMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableDoubleMatrix16.Get(),
                expected: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6]
                                    {
                                      0, 2, 4, 1, 3, 5
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                columnNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                rowNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

            DoubleMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableDoubleMatrix57.Get(),
                expected: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6]
                                    {
                                      0, 2, 4, 1, 3, 5
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                columnNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } }));

            DoubleMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableDoubleMatrix58.Get(),
                expected: new DoubleMatrixState(
                                asColumnMajorDenseArray: new double[6]
                                    {
                                      0, 2, 4, 1, 3, 5
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

        }

        [TestMethod()]
        public void VecTest()
        {
            // All linear indexes
            {
                TestAction(DoubleMatrixTest.Vec.AllIndexes, GetTestableMatrices());
            }

            // Specific linear indexes
            {
                TestAction(DoubleMatrixTest.Vec.SpecificIndexes, GetTestableMatrices());
            }

            // Specific linear indexes
            {
                // linearIndexes is null
                {
                    var target = DoubleMatrix.Dense(
                        3,
                        2,
                        new double[6] { 0, 2, 0, 3, 0, Double.NaN },
                        StorageOrder.RowMajor);

                    target.SetRowName(0, "Row0");
                    target.SetRowName(1, "Row1");
                    target.SetRowName(2, "Row2");

                    // target = 
                    // [Row0] [  0   2   
                    // [Row1]    0   3
                    // [Row2]    0   NaN ]

                    string parameterName = "linearIndexes";

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target.Vec(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                // linearIndexes exceeds dimensions
                {
                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    string parameterName = "linearIndexes";

                    DoubleMatrix target;

                    // Dense
                    target = TestableDoubleMatrix00.Get().Dense;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target.Vec(IndexCollection.Range(0, target.Count));
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // Sparse
                    target = TestableDoubleMatrix00.Get().Sparse;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target.Vec(IndexCollection.Range(0, target.Count));
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // View
                    target = TestableDoubleMatrix00.Get().View;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target.Vec(IndexCollection.Range(0, target.Count));
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);
                }

                // Valid linearIndexes
                {
                    TestAction(DoubleMatrixTest.Vec.SpecificIndexes, GetTestableMatrices());
                }
            }
        }

        #region IList<double>

        [TestMethod()]
        public void ContainsTest()
        {
            DoubleMatrixTest.Contains(
                testableMatrix: TestableDoubleMatrix35.Get(),
                value: 0.0,
                expected: true);

            DoubleMatrixTest.Contains(
                testableMatrix: TestableDoubleMatrix35.Get(),
                value: Double.NaN,
                expected: true);

            DoubleMatrixTest.Contains(
                testableMatrix: TestableDoubleMatrix36.Get(),
                value: 1.0,
                expected: false);

            DoubleMatrixTest.Contains(
                testableMatrix: TestableDoubleMatrix37.Get(),
                value: 2.0,
                expected: true);
        }

        [TestMethod()]
        public void CopyToTest()
        {
            TestAction(DoubleMatrixTest.CopyTo.ArrayIsNull, GetTestableMatrices());

            TestAction(DoubleMatrixTest.CopyTo.ArrayIndexIsNegative, GetTestableMatrices());

            TestAction(DoubleMatrixTest.CopyTo.ArrayHasNotEnoughSpace, GetTestableMatrices());

            DoubleMatrixTest.CopyTo.Succeed(
                testableMatrix: TestableDoubleMatrix09.Get(),
                array: new double[6] { 10, 20, 30, 40, 50, 60 },
                arrayIndex: 1,
                expected: new double[6] { 10, 1, 0, 0, 0, 60 },
                delta: DoubleMatrixTest.Accuracy);

            DoubleMatrixTest.CopyTo.Succeed(
               testableMatrix: TestableDoubleMatrix13.Get(),
               array: new double[6] { 10, 20, 30, 40, 50, 60 },
               arrayIndex: 2,
               expected: new double[6] { 10, 20, 1.1, 0, 0, 4.4 },
               delta: DoubleMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void IListGetTest()
        {
            TestAction(DoubleMatrixTest.GetItem.AnyLinearIndexIsOutOfRange, GetTestableMatrices());

            DoubleMatrixTest.GetItem.Succeed(
                testableMatrix: TestableDoubleMatrix02.Get(),
                linearIndex: 0,
                expected: 2.0);

            DoubleMatrixTest.GetItem.Succeed(
                testableMatrix: TestableDoubleMatrix02.Get(),
                linearIndex: 2,
                expected: 0.0);

            DoubleMatrixTest.GetItem.Succeed(
                testableMatrix: TestableDoubleMatrix02.Get(),
                linearIndex: 4,
                expected: 1.0);
        }

        [TestMethod()]
        public void IListSetTest()
        {
            TestAction(DoubleMatrixTest.SetItem.AnyLinearIndexIsOutOfRange, GetTestableMatrices());

            TestAction(DoubleMatrixTest.SetItem.InstanceIsReadOnly, GetTestableMatrices());

            DoubleMatrixTest.SetItem.Succeed(
                testableMatrix: TestableDoubleMatrix02.Get(),
                linearIndex: 0,
                expected: 2.0);

            DoubleMatrixTest.SetItem.Succeed(
                testableMatrix: TestableDoubleMatrix02.Get(),
                linearIndex: 2,
                expected: 0.0);

            DoubleMatrixTest.SetItem.Succeed(
                testableMatrix: TestableDoubleMatrix02.Get(),
                linearIndex: 4,
                expected: 1.0);
        }

        [TestMethod()]
        public void IListInsertTest()
        {
            // DoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<double>)target).Insert(0, 0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyDoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<double>)target).Insert(0, 0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        [TestMethod()]
        public void IListRemoveAtTest()
        {
            // DoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<double>)target).RemoveAt(0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyDoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<double>)target).RemoveAt(0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            DoubleMatrixTest.IndexOf(
                testableMatrix: TestableDoubleMatrix35.Get(),
                value: 0.0,
                expected: 0);

            DoubleMatrixTest.IndexOf(
                testableMatrix: TestableDoubleMatrix35.Get(),
                value: Double.NaN,
                expected: 5);

            DoubleMatrixTest.IndexOf(
                testableMatrix: TestableDoubleMatrix36.Get(),
                value: 1.0,
                expected: -1);

            DoubleMatrixTest.IndexOf(
                testableMatrix: TestableDoubleMatrix37.Get(),
                value: 2.0,
                expected: 1);
        }

        #endregion

        #region ICollection<double>

        [TestMethod()]
        public void ICollectionAddTest()
        {
            // DoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<double>)target).Add(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyDoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<double>)target).Add(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        [TestMethod()]
        public void ICollectionClearTest()
        {
            // DoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<double>)target).Clear();
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyDoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<double>)target).Clear();
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }


        [TestMethod()]
        public void IsReadOnlyTest()
        {
            TestAction(DoubleMatrixTest.IsReadOnly, GetTestableMatrices());
        }

        [TestMethod()]
        public void ICollectionRemoveTest()
        {
            // DoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<double>)target).Remove(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyDoubleMatrix
            {
                var target = TestableDoubleMatrix00.Get().Dense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<double>)target).Remove(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        #endregion
    }
}