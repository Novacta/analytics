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
using System.Numerics;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class ComplexMatrixTests
    {
        #region Helpers

        /// <summary>
        /// Gets the list of available <see cref="TestableComplexMatrix"/> instances.
        /// </summary>
        /// <returns>The list of available <see cref="TestableComplexMatrix"/> instances.</returns>
        static List<TestableComplexMatrix> GetTestableMatrices()
        {
            List<TestableComplexMatrix> TestableItems = new()
            {
                TestableComplexMatrix00.Get(),
                TestableComplexMatrix01.Get(),
                TestableComplexMatrix02.Get(),
                TestableComplexMatrix03.Get(),
                TestableComplexMatrix04.Get(),
                TestableComplexMatrix05.Get(),
                TestableComplexMatrix06.Get(),
                TestableComplexMatrix07.Get(),
                TestableComplexMatrix08.Get(),
                TestableComplexMatrix09.Get(),
                TestableComplexMatrix10.Get(),
                TestableComplexMatrix11.Get(),
                TestableComplexMatrix12.Get(),
                TestableComplexMatrix13.Get(),
                TestableComplexMatrix14.Get(),
                TestableComplexMatrix15.Get(),
                TestableComplexMatrix16.Get(),
                TestableComplexMatrix17.Get(),
                TestableComplexMatrix18.Get(),
                TestableComplexMatrix19.Get(),
                TestableComplexMatrix20.Get(),
                TestableComplexMatrix21.Get(),
                TestableComplexMatrix22.Get(),
                TestableComplexMatrix23.Get(),
                TestableComplexMatrix24.Get(),
                TestableComplexMatrix25.Get(),
                TestableComplexMatrix26.Get(),
                TestableComplexMatrix27.Get(),
                TestableComplexMatrix28.Get(),
                TestableComplexMatrix29.Get(),
                TestableComplexMatrix30.Get(),
                TestableComplexMatrix31.Get(),
                TestableComplexMatrix32.Get(),
                TestableComplexMatrix33.Get(),
                TestableComplexMatrix34.Get(),
                TestableComplexMatrix35.Get(),
                TestableComplexMatrix36.Get(),
                TestableComplexMatrix37.Get(),
                TestableComplexMatrix38.Get(),
                TestableComplexMatrix39.Get(),
                TestableComplexMatrix40.Get(),
                TestableComplexMatrix41.Get(),
                TestableComplexMatrix42.Get(),
                TestableComplexMatrix43.Get(),
                TestableComplexMatrix44.Get(),
                TestableComplexMatrix45.Get(),
                TestableComplexMatrix46.Get(),
                TestableComplexMatrix47.Get(),
                TestableComplexMatrix48.Get(),
                TestableComplexMatrix49.Get(),
                TestableComplexMatrix50.Get(),
                TestableComplexMatrix51.Get(),
                TestableComplexMatrix52.Get(),
                TestableComplexMatrix53.Get(),
                TestableComplexMatrix54.Get(),
                TestableComplexMatrix55.Get(),
                TestableComplexMatrix56.Get(),
                TestableComplexMatrix57.Get(),
                TestableComplexMatrix58.Get(),
                TestableComplexMatrix59.Get(),
                TestableComplexMatrix60.Get(),
                TestableComplexMatrix61.Get(),
                TestableComplexMatrix62.Get(),
                TestableComplexMatrix63.Get(),
                TestableComplexMatrix64.Get(),
                TestableComplexMatrix65.Get(),
                TestableComplexMatrix66.Get(),
                TestableComplexMatrix67.Get(),
                TestableComplexMatrix68.Get(),
                TestableComplexMatrix69.Get(),
                TestableComplexMatrix70.Get(),
                TestableComplexMatrix71.Get(),
                TestableComplexMatrix72.Get()
            };

            return TestableItems;
        }

        /// <summary>
        /// Tests the specified <see cref="Action"/> for each item in the 
        /// given list of <see cref="TestableComplexMatrix"/> instances.
        /// </summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="TestableItems">The list of <see cref="TestableComplexMatrix"/> instances 
        /// to test.</param>
        static void TestAction(
            Action<TestableComplexMatrix> test,
            List<TestableComplexMatrix> TestableItems)
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

        #region IComplexMatrixPatterns

        [TestMethod()]
        public void IsHermitianTest()
        {
            TestAction(ComplexMatrixTest.IsHermitian, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSkewHermitianTest()
        {
            TestAction(ComplexMatrixTest.IsSkewHermitian, GetTestableMatrices());
        }

        #region IMatrixPatterns

        [TestMethod()]
        public void IsBidiagonalTest()
        {
            TestAction(ComplexMatrixTest.IsBidiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsColumnVectorTest()
        {
            TestAction(ComplexMatrixTest.IsColumnVector, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsDiagonalTest()
        {
            TestAction(ComplexMatrixTest.IsDiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsHessenbergTest()
        {
            TestAction(ComplexMatrixTest.IsHessenberg, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsLowerBidiagonalTest()
        {
            TestAction(ComplexMatrixTest.IsLowerBidiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsLowerHessenbergTest()
        {
            TestAction(ComplexMatrixTest.IsLowerHessenberg, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsLowerTriangularTest()
        {
            TestAction(ComplexMatrixTest.IsLowerTriangular, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsRowVectorTest()
        {
            TestAction(ComplexMatrixTest.IsRowVector, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsScalarTest()
        {
            TestAction(ComplexMatrixTest.IsScalar, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSkewSymmetricTest()
        {
            TestAction(ComplexMatrixTest.IsSkewSymmetric, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSquareTest()
        {
            TestAction(ComplexMatrixTest.IsSquare, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsSymmetricTest()
        {
            TestAction(ComplexMatrixTest.IsSymmetric, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsTriangularTest()
        {
            TestAction(ComplexMatrixTest.IsTriangular, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsTridiagonalTest()
        {
            TestAction(ComplexMatrixTest.IsTridiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsUpperBidiagonalTest()
        {
            TestAction(ComplexMatrixTest.IsUpperBidiagonal, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsUpperHessenbergTest()
        {
            TestAction(ComplexMatrixTest.IsUpperHessenberg, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsUpperTriangularTest()
        {
            TestAction(ComplexMatrixTest.IsUpperTriangular, GetTestableMatrices());
        }

        [TestMethod()]
        public void IsVectorTest()
        {
            TestAction(ComplexMatrixTest.IsVector, GetTestableMatrices());
        }

        [TestMethod()]
        public void LowerBandwidthTest()
        {
            TestAction(ComplexMatrixTest.LowerBandwidth, GetTestableMatrices());
        }

        [TestMethod()]
        public void UpperBandwidthTest()
        {
            TestAction(ComplexMatrixTest.UpperBandwidth, GetTestableMatrices());
        }

        #endregion

        #endregion

        #region ITabularCollection

        [TestMethod()]
        public void NumberOfRowsTest()
        {
            TestAction(ComplexMatrixTest.NumberOfRows, GetTestableMatrices());
        }

        [TestMethod()]
        public void NumberOfColumnsTest()
        {
            TestAction(ComplexMatrixTest.NumberOfColumns, GetTestableMatrices());
        }

        [TestMethod()]
        public void IndexerGetTest()
        {
            TestAction(ComplexMatrixTest.IndexerGet.AnyRowIndexIsOutOfRange, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerGet.AnyColumnIndexIsOutOfRange, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerGet.RowIndexesIsNull, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerGet.ColumnIndexesIsNull, GetTestableMatrices());

            #region SubMatrix - Without row or column names

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expected: new Complex(8.0, 8),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 1,
                columnIndex: 2);

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[2]
                                {
                                    new Complex(6, 6),
                                    new Complex(9, 9)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 2),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 2,
                columnIndexes: IndexCollection.Range(1, 2));

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(2,2),
                                    new Complex(5,5),
                                    new Complex(8,8)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 3),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 1,
                columnIndexes: ":");

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(4,4),
                                    new Complex(5,5),
                                    new Complex(6,6)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndex: 1);

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(4, 4),
                                    new Complex(5, 5),
                                    new Complex(6, 6),
                                    new Complex(7, 7),
                                    new Complex(8, 8),
                                    new Complex(9, 9)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: IndexCollection.Range(1, 2));

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(2, 2),
                                    new Complex(3, 3),
                                    new Complex(5, 5),
                                    new Complex(6, 6),
                                    new Complex(8, 8),
                                    new Complex(9, 9)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 3),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(1, 2),
                columnIndexes: ":");

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(4, 4),
                                    new Complex(5, 5),
                                    new Complex(6, 6)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndex: 1);

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(4, 4),
                                    new Complex(5, 5),
                                    new Complex(6, 6),
                                    new Complex(7, 7),
                                    new Complex(8, 8),
                                    new Complex(9, 9)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Range(1, 2));

            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[9]
                                {
                                    new Complex(1, 1),
                                    new Complex(2, 2),
                                    new Complex(3, 3),
                                    new Complex(4, 4),
                                    new Complex(5, 5),
                                    new Complex(6, 6),
                                    new Complex(7, 7),
                                    new Complex(8, 8),
                                    new Complex(9, 9)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 3),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion

            #region SubMatrix - With row or column names

            // 0    m[1,[0,2]]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[2]
                                {
                                    new Complex(1, 1),
                                    new Complex(5, 5)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(1) { { 0, "r1" } },
                                columnNames: new Dictionary<int, string>(2) { { 0, "c0" }, { 1, "c2" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndex: 1,
                columnIndexes: IndexCollection.Sequence(0, 2, 2));

            // 1    m[1,:]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(1, 1),
                                    new Complex(3, 3),
                                    new Complex(5, 5)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(1) { { 0, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndex: 1,
                columnIndexes: ":");

            // 2    m[[1],1]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[1]
                                {
                                    new Complex(3, 3)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 1,
                                rowNames: new Dictionary<int, string>(1) { { 0, "r1" } },
                                columnNames: new Dictionary<int, string>(1) { { 0, "c1" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndexes: IndexCollection.Range(1, 1),
                columnIndex: 1);

            // 3    m[[1,0],[0,1]]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[4]
                                {
                                    new Complex(1, 1),
                                    new Complex(0, 0),
                                    new Complex(3, 3),
                                    new Complex(2, 2)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r1" }, { 1, "r0" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndexes: IndexCollection.Sequence(1, -1, 0),
                columnIndexes: IndexCollection.Range(0, 1));

            // 4    m[[0,1],:]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(0, 0),
                                    new Complex(1, 1),
                                    new Complex(2, 2),
                                    new Complex(3, 3),
                                    new Complex(4, 4),
                                    new Complex(5, 5)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndexes: IndexCollection.Range(0, 1),
                columnIndexes: ":");

            // 5    m[:,1]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[2]
                                {
                                    new Complex(2, 2),
                                    new Complex(3, 3)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 1,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(1) { { 0, "c1" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndexes: ":",
                columnIndex: 1);

            // 6    m[:,[1,0]]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[4]
                                {
                                    new Complex(2, 2),
                                    new Complex(3, 3),
                                    new Complex(0, 0),
                                    new Complex(1, 1)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(2) { { 0, "c1" }, { 1, "c0" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Sequence(1, -1, 0));

            // 7    m[:,:]
            ComplexMatrixTest.IndexerGet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(0, 0),
                                    new Complex(1, 1),
                                    new Complex(2, 2),
                                    new Complex(3, 3),
                                    new Complex(4, 4),
                                    new Complex(5, 5)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }),
                testableMatrix: TestableComplexMatrix16.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion
        }

        [TestMethod()]
        public void IndexerSetTest()
        {
            TestAction(ComplexMatrixTest.IndexerSet.AnyRowIndexIsOutOrRange, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerSet.AnyColumnIndexIsOutOrRange, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerSet.RowIndexesIsnull, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerSet.ColumnIndexesIsnull, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerSet.CollectionValueIsNull, GetTestableMatrices());
            TestAction(ComplexMatrixTest.IndexerSet.MismatchedCollectionDimensions, GetTestableMatrices());

            #region SubMatrix

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expected: new Complex(8.0, 8),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 1,
                columnIndex: 2);

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[2]
                                {
                                    new Complex(-6, -6),
                                    new Complex(-9, -9)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 2),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 2,
                columnIndexes: IndexCollection.Range(1, 2));

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(-2, -2),
                                    new Complex(-5, -5),
                                    new Complex(-8, -8)
                                },
                                numberOfRows: 1,
                                numberOfColumns: 3),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 1,
                columnIndexes: ":");

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(-4, -4),
                                    new Complex(-5, -5),
                                    new Complex(-6, -6)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndex: 1);

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(-4, -4),
                                    new Complex(-5, -5),
                                    new Complex(-6, -6),
                                    new Complex(-7, -7),
                                    new Complex(-8, -8),
                                    new Complex(-9, -9)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: IndexCollection.Range(1, 2));

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(-2, -2),
                                    new Complex(-3, -3),
                                    new Complex(-5, -5),
                                    new Complex(-6, -6),
                                    new Complex(-8, -8),
                                    new Complex(-9, -9)
                                },
                                numberOfRows: 2,
                                numberOfColumns: 3),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(1, 2),
                columnIndexes: ":");

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[3]
                                {
                                    new Complex(-4, -4),
                                    new Complex(-5, -5),
                                    new Complex(-6, -6)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 1),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndex: 1);

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                {
                                    new Complex(-4, -4),
                                    new Complex(-5, -5),
                                    new Complex(-6, -6),
                                    new Complex(-7, -7),
                                    new Complex(-8, -8),
                                    new Complex(-9, -9)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 2),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Range(1, 2));

            ComplexMatrixTest.IndexerSet.SubMatrix(
                expectedState: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[9]
                                {
                                    new Complex(-1, -1),
                                    new Complex(-2, -2),
                                    new Complex(-3, -3),
                                    new Complex(-4, -4),
                                    new Complex(-5, -5),
                                    new Complex(-6, -6),
                                    new Complex(-7, -7),
                                    new Complex(-8, -8),
                                    new Complex(-9, -9)
                                },
                                numberOfRows: 3,
                                numberOfColumns: 3),
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion

            #region SourceIsValue

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 0,
                columnIndexes: IndexCollection.Range(0, 2));

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndex: 0,
                columnIndexes: ":");

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndex: 0);

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: IndexCollection.Range(0, 2));

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: IndexCollection.Range(0, 2),
                columnIndexes: ":");

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndex: 0);

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: IndexCollection.Range(0, 2));

            ComplexMatrixTest.IndexerSet.SourceIsValue(
                testableMatrix: TestableComplexMatrix14.Get(),
                rowIndexes: ":",
                columnIndexes: ":");

            #endregion
        }

        #endregion

        #region Names

        [TestMethod()]
        public void NameTest()
        {
            var testableMatrix = TestableComplexMatrix00.Get();

            var matrix = testableMatrix.AsDense;

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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.TryGetRowName(-1, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.TryGetRowName(matrix.NumberOfRows, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.AsReadOnly().TryGetRowName(-1, out rowName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
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

                var matrix = TestableComplexMatrix00.Get().AsDense;
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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetRowName(-1, "Name");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetRowName(0, ":");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetRowName(0, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetRowName(0, "");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetRowName(0, "  ");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                var matrix = TestableComplexMatrix00.Get().AsDense;

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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.RemoveRowName(-1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
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
                var matrix = TestableComplexMatrix00.Get().AsDense;

                bool rowHasIndex = matrix.RemoveRowName(0);
                Assert.AreEqual(expected: false, actual: rowHasIndex);
            }

            {
                // This matrix has names
                var matrix = TestableComplexMatrix16.Get().AsDense;

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
            var matrix = TestableComplexMatrix16.Get().AsDense;

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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.TryGetColumnName(-1, out columnName);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
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

                var matrix = TestableComplexMatrix00.Get().AsDense;
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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetColumnName(-1, "Name");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetColumnName(0, ":");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NAME_CANNOT_BE_RESERVED_STRING,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetColumnName(0, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetColumnName(0, "");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.SetColumnName(0, "  ");
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE,
                    expectedParameterName: parameterName);
            }

            #endregion

            #region Basic usage

            {
                var matrix = TestableComplexMatrix00.Get().AsDense;

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
                        var matrix = TestableComplexMatrix00.Get().AsDense;
                        matrix.RemoveColumnName(-1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var matrix = TestableComplexMatrix00.Get().AsDense;
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
                var matrix = TestableComplexMatrix00.Get().AsDense;

                bool columnHasIndex = matrix.RemoveColumnName(0);
                Assert.AreEqual(expected: false, actual: columnHasIndex);
            }

            {
                // This matrix has names
                var matrix = TestableComplexMatrix16.Get().AsDense;

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
            var matrix = TestableComplexMatrix16.Get().AsDense;

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
            TestAction(ComplexMatrixTest.AsColumnMajorDenseArray, GetTestableMatrices());
        }

        [TestMethod()]
        public void AsReadOnlyTest()
        {
            TestAction(ComplexMatrixTest.AsReadOnly, GetTestableMatrices());
        }

        [TestMethod()]
        public void AsRowCollectionTest()
        {
            var target = ComplexMatrix.Dense(
                3,
                2,
                new Complex[6]
                {
                    0,
                    0,
                    0,
                    new Complex(2, 2),
                    new Complex(3, 3),
                    Complex.NaN
                });

            target.SetRowName(0, "Row0");
            target.SetRowName(1, "Row1");
            target.SetRowName(2, "Row2");

            // target = 
            // [Row0] [  0   2,2   
            // [Row1]    0   3,3
            // [Row2]    0   NaN ]

            // All rows
            {
                var rowCollection = target.AsRowCollection();

                ComplexMatrix actual = rowCollection;

                var expected = ComplexMatrix.Dense(3, 2,
                    new Complex[6]
                    {
                        0,
                        0,
                        0,
                        new Complex(2, 2),
                        new Complex(3, 3),
                        Complex.NaN
                    });

                expected.SetRowName(0, "Row0");
                expected.SetRowName(1, "Row1");
                expected.SetRowName(2, "Row2");

                ComplexMatrixAssert.AreEqual(
                    expected, actual, ComplexMatrixTest.Accuracy);
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

                ComplexMatrix actual = rowCollection;

                var expected = ComplexMatrix.Dense(2, 2,
                    new Complex[4]
                    {
                        0,
                        0,
                        new Complex(2, 2),
                        Complex.NaN
                    });

                expected.SetRowName(0, "Row0");
                expected.SetRowName(1, "Row2");

                ComplexMatrixAssert.AreEqual(
                    expected, actual, ComplexMatrixTest.Accuracy);
            }
        }

        [TestMethod()]
        public void FromReadOnlyComplexMatrixTest()
        {
            string parameterName = "value";

            // value is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ComplexMatrix actual = ((ComplexMatrix)(ReadOnlyComplexMatrix)(null));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        ComplexMatrix actual = ComplexMatrix.FromReadOnlyComplexMatrix(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // value is not null
            {
                var expected = ComplexMatrix.Dense(2, 3, -1.0);
                var value = expected.AsReadOnly();
                var actual = ((ComplexMatrix)(value));

                ComplexMatrixAssert.AreEqual(value.matrix, actual, ComplexMatrixTest.Accuracy);

                actual = ComplexMatrix.FromReadOnlyComplexMatrix(value);

                ComplexMatrixAssert.AreEqual(value.matrix, actual, ComplexMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void FromComplexTest()
        {
            Complex value = new(1.2, 1.2);

            ComplexMatrix expected = ComplexMatrix.Dense(1, 1, value);

            ComplexMatrix actual = (ComplexMatrix)value;

            ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);

            actual = ComplexMatrix.FromComplex(value);

            ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
        }

        [TestMethod]
        public void ToComplexTest()
        {
            string parameterName = "value";

            // ComplexMatrix
            {
                // value is null
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ((Complex)(ComplexMatrix)(null));
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ComplexMatrix.ToComplex(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                // value is not scalar
                {
                    string STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR" });

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ((Complex)ComplexMatrix.Dense(2, 3));
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ComplexMatrix.ToComplex(ComplexMatrix.Dense(2, 3));
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);
                }

                // value is scalar
                {
                    var value = ComplexMatrix.Dense(1, 1, -1.0);
                    var actual = ((Complex)(value));

                    ComplexAssert.AreEqual(value[0], actual, ComplexMatrixTest.Accuracy);

                    actual = ComplexMatrix.ToComplex(value);

                    ComplexAssert.AreEqual(value[0], actual, ComplexMatrixTest.Accuracy);
                }
            }

            // ReadOnlyComplexMatrix
            {
                // value is null
                {
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ((Complex)(ReadOnlyComplexMatrix)(null));
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ReadOnlyComplexMatrix.ToComplex(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);
                }

                // value is not scalar
                {
                    string STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR" });

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ((Complex)ComplexMatrix.Dense(2, 3).AsReadOnly());
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            Complex actual = ReadOnlyComplexMatrix.ToComplex(
                                ComplexMatrix.Dense(2, 3).AsReadOnly());
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_MAT_CONVERTED_TO_ENTRY_TYPE_MUST_BE_SCALAR,
                        expectedParameterName: parameterName);
                }

                // value is scalar
                {
                    var value = ComplexMatrix.Dense(1, 1, -1.0).AsReadOnly();
                    var actual = ((Complex)(value));

                    ComplexAssert.AreEqual(value[0], actual, ComplexMatrixTest.Accuracy);

                    actual = ReadOnlyComplexMatrix.ToComplex(value);

                    ComplexAssert.AreEqual(value[0], actual, ComplexMatrixTest.Accuracy);
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string expected, actual;
            ComplexMatrix target;

            // matrix has no row or column names

            target = ComplexMatrix.Dense(2, 3, new Complex[6]
            {
                new Complex(.1, .1),
                new Complex(10.2, 10.2),
                new Complex(-2.3, -2.3),
                new Complex(1000.2, 1000.2),
                new Complex(.2, .2),
                new Complex(239.32, 239.32)
            });

            //         "                 " 17 char length
            //         "                                      " 38 char length

            //         first row
            expected =
                  "(              0.1,              0.1) "
                + "(             -2.3,             -2.3) "
                + "(              0.2,              0.2) "
                //         newline char
                + Environment.NewLine
                //         second row   
                + "(             10.2,             10.2) "
                + "(           1000.2,           1000.2) "
                + "(           239.32,           239.32) "
                //         newline char
                + Environment.NewLine
                //         final newline char
                + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);

            // matrix has some row or column names

            target = ComplexMatrix.Dense(3, 3, new Complex[9]
            {
                new Complex(.1, .1),
                new Complex(10.2, 10.2),
                0,
                new Complex(-2.3, -2.3),
                new Complex(1000.2, 1000.2),
                0,
                new Complex(.2, .2),
                new Complex(239.32, 239.32),
                0
            });

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

            // Too long column name "                                      " 
            target.SetColumnName(1, "Col name so much long that must be truncated");

            // No name for column 2

            //                               "                                       "
            expected =
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
                + "(              0.2,              0.2) "
                //         newline char
                + Environment.NewLine
                //         second row   
                + "                 "
                + "(             10.2,             10.2) "
                + "(           1000.2,           1000.2) "
                + "(           239.32,           239.32) "
                //         newline char
                + Environment.NewLine
                //         third row
                + "[Row name too *] "
                + "(                0,                0) "
                + "(                0,                0) "
                + "(                0,                0) "
                //         newline char
                + Environment.NewLine
                //         final newline char
                + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);

            // matrix has some row names but no col ones

            target = ComplexMatrix.Dense(3, 3, new Complex[9]
            {
                new Complex(.1, .1),
                new Complex(10.2, 10.2),
                0,
                new Complex(-2.3, -2.3),
                new Complex(1000.2, 1000.2),
                0,
                new Complex(.2, .2),
                new Complex(239.32, 239.32),
                0
            });

            //                "                 " 17 char length
            //                "                                       " 39 char length

            // ROW NAMES 

            // Row name with adequate length
            target.SetRowName(0, "Row name <  15");

            // No name for row 1

            // Too long row name
            target.SetRowName(2, "Row name too much long");

            //         first row             "                                       "
            expected =
                "[Row name <  15] "
                + "(              0.1,              0.1) "
                + "(             -2.3,             -2.3) "
                + "(              0.2,              0.2) "
                //         newline char
                + Environment.NewLine
                //         second row   
                + "                 "
                + "(             10.2,             10.2) "
                + "(           1000.2,           1000.2) "
                + "(           239.32,           239.32) "
                //         newline char
                + Environment.NewLine
                //         third row
                + "[Row name too *] "
                + "(                0,                0) "
                + "(                0,                0) "
                + "(                0,                0) "
                //         newline char
                + Environment.NewLine
                //         final newline char
                + Environment.NewLine;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            actual = target.AsReadOnly().ToString();
            Assert.AreEqual(expected, actual);

            // matrix has no row names and some column ones

            target = ComplexMatrix.Dense(3, 3, new Complex[9]
            {
                new Complex(.1, .1),
                new Complex(10.2, 10.2),
                0,
                new Complex(-2.3, -2.3),
                new Complex(1000.2, 1000.2),
                0,
                new Complex(.2, .2),
                new Complex(239.32, 239.32),
                0
            });

            //                "                 " 17 char length

            // COL NAMES

            // Column name with adequate length
            target.SetColumnName(0, "Col name <  15");

            // No name for column 1

            // Too long column name
            target.SetColumnName(2, "Col name so much long that must be truncated");

            expected =
                  "[Col name <  15]                      "
                + "                                      "
                + "[Col name so much long that must be*] "
                //         newline char
                + Environment.NewLine
                //         first row
                + "(              0.1,              0.1) "
                + "(             -2.3,             -2.3) "
                + "(              0.2,              0.2) "
                //         newline char
                + Environment.NewLine
                //         second row   
                + "(             10.2,             10.2) "
                + "(           1000.2,           1000.2) "
                + "(           239.32,           239.32) "
                //         newline char
                + Environment.NewLine
                //         third row
                + "(                0,                0) "
                + "(                0,                0) "
                + "(                0,                0) "
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
            ComplexMatrix target = ComplexMatrix.Dense(2, 3);

            ComplexMatrix view = target[":", ":"];

            target.Dispose();

            Assert.AreEqual(
                StorageScheme.Dense,
                view.implementor.StorageScheme);
        }


        [TestMethod()]
        public void CloneTest()
        {
            TestAction(ComplexMatrixTest.Clone, GetTestableMatrices());
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
                        var actual = ComplexMatrix.Dense(0, 1);
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
                        var actual = ComplexMatrix.Dense(1, 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: new Complex[6],
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                Assert.AreEqual(StorageOrder.ColumnMajor, actual.AsReadOnly().StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.AsReadOnly().StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6], actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,Complex)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense(0, 1, 1.0);
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
                        var actual = ComplexMatrix.Dense(1, 0, 1.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "numberOfColumns");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var c = new Complex(-2, -2);
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, c);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: new Complex[6] { c, c, c, c, c, c },
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6] { c, c, c, c, c, c }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,Complex[])

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense(0, 1, Array.Empty<Complex>());
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
                        var actual = ComplexMatrix.Dense(1, 0, Array.Empty<Complex>());
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
                        var actual = ComplexMatrix.Dense(1, 1, (Complex[])null);
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
                        var actual = ComplexMatrix.Dense(2, 3, new Complex[5]);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                Complex[] data = new Complex[6] { 0, 1, 2, 3, 4, 5 };
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, data);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: data,
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,Complex[],Boolean)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense(0, 1, Array.Empty<Complex>(), true);
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
                        var actual = ComplexMatrix.Dense(1, 0, Array.Empty<Complex>(), true);
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
                        var actual = ComplexMatrix.Dense(1, 1, (Complex[])null, true);
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
                        var actual = ComplexMatrix.Dense(2, 3, new Complex[5], true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input - copyData is true
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                Complex[] data = new Complex[6] { 0, 1, 2, 3, 4, 5 };
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, data, true);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: data,
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(
                    new Complex[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);

                Assert.AreNotSame(data, actual.GetStorage());
            }

            // Valid input - copyData is false
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                Complex[] data = new Complex[6] { 0, 1, 2, 3, 4, 5 };
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, data, false);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: data,
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(
                    new Complex[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);

                Assert.AreSame(data, actual.GetStorage());
            }

            #endregion

            #region Dense(int,int,Complex[],StorageOrder)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense(0, 1, Array.Empty<Complex>(), StorageOrder.RowMajor);
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
                        var actual = ComplexMatrix.Dense(1, 0, Array.Empty<Complex>(), StorageOrder.RowMajor);
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
                        var actual = ComplexMatrix.Dense(1, 1, (Complex[])null, StorageOrder.RowMajor);
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
                        var actual = ComplexMatrix.Dense(2, 3, new Complex[6] { 0, 1, 2, 3, 4, 5 }, (StorageOrder)(-1));
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
                        var actual = ComplexMatrix.Dense(2, 3, new Complex[5], StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                Complex[] data = new Complex[6] { 0, 1, 2, 3, 4, 5 };
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, data, StorageOrder.RowMajor);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: new Complex[6] { 0, 3, 1, 4, 2, 5 },
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6] { 0, 3, 1, 4, 2, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,IEnumerable<Complex>)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense(0, 1, new List<Complex>());
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
                        var actual = ComplexMatrix.Dense(1, 0, new List<Complex>());
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
                        var actual = ComplexMatrix.Dense(1, 1, (List<Complex>)null);
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
                        var actual = ComplexMatrix.Dense(2, 3, new List<Complex>() { 0, 1, 2, 3, 4 });
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var data = new List<Complex>() { 0, 1, 2, 3, 4, 5 };
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, data);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: data.ToArray(),
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(int,int,IEnumerable<Complex>,StorageOrder)

            // numberOfRows < 1
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense(0, 1, new List<Complex>(), StorageOrder.RowMajor);
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
                        var actual = ComplexMatrix.Dense(1, 0, new List<Complex>(), StorageOrder.RowMajor);
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
                        var actual = ComplexMatrix.Dense(1, 1, (List<Complex>)null, StorageOrder.RowMajor);
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
                        var actual = ComplexMatrix.Dense(2, 3, new List<Complex>() { 0, 1, 2, 3, 4, 5 }, (StorageOrder)(-1));
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
                        var actual = ComplexMatrix.Dense(2, 3, new List<Complex>() { 0, 1, 2, 3, 4 }, StorageOrder.RowMajor);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_INCONSISTENCY_DATA_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                int numberOfRows = 2;
                int numberOfColumns = 3;
                var data = new List<Complex>() { 0, 1, 2, 3, 4, 5 };
                var actual = ComplexMatrix.Dense(numberOfRows, numberOfColumns, data, StorageOrder.RowMajor);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: new Complex[6] { 0, 3, 1, 4, 2, 5 },
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6] { 0, 3, 1, 4, 2, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion

            #region Dense(Complex[,])

            // data is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Dense((Complex[,])null);
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
                        var actual = ComplexMatrix.Dense(new Complex[0, 1]);
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
                        var actual = ComplexMatrix.Dense(new Complex[1, 0]);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_MAT_UNALLOWED_NON_POSITIVE_DIMS,
                    expectedParameterName: "data");
            }

            // Valid input
            {
                var data = new Complex[2, 3] { { 0, 2, 4 }, { 1, 3, 5 } };
                var actual = ComplexMatrix.Dense(data);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: new Complex[6] { 0, 1, 2, 3, 4, 5 },
                        numberOfRows: data.GetLength(0),
                        numberOfColumns: data.GetLength(1)),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.ColumnMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.Dense, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[6] { 0, 1, 2, 3, 4, 5 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            #endregion
        }

        [TestMethod()]
        public void DiagonalTest()
        {
            // mainDiagonal is null
            {
                // ComplexMatrix
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Diagonal((ComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "mainDiagonal");

                // ReadOnlyComplexMatrix
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = ComplexMatrix.Diagonal((ReadOnlyComplexMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "mainDiagonal");
            }

            // ComplexMatrix mainDiagonal is not null
            {
                var mainDiagonal = ComplexMatrix.Dense(2, 2, new Complex[4] { 0, 2, 1, 3 });
                // diagonal = 
                // [  0   1
                //    2   3

                var actual = ComplexMatrix.Diagonal(mainDiagonal);

                var expected = ComplexMatrix.Dense(4, 4);

                for (int i = 0; i < mainDiagonal.Count; i++)
                {
                    expected[i, i] = mainDiagonal[i];
                }

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[4] { 2, 1, 3, 0 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }

            // ReadOnlyComplexMatrix mainDiagonal is not null
            {
                var mainDiagonal =
                    ComplexMatrix.Dense(2, 2, new Complex[4] { 0, 2, 1, 3 })
                    .AsReadOnly();
                // diagonal = 
                // [  0   1
                //    2   3

                var actual = ComplexMatrix.Diagonal(mainDiagonal);

                var expected = ComplexMatrix.Dense(4, 4);

                for (int i = 0; i < mainDiagonal.Count; i++)
                {
                    expected[i, i] = mainDiagonal[i];
                }

                ComplexMatrixAssert.AreEqual(expected, actual, ComplexMatrixTest.Accuracy);
                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[4] { 2, 1, 3, 0 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
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
                        var actual = ComplexMatrix.Identity(0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "dimension");

            }

            // dimension >= 1
            {
                var actual = ComplexMatrix.Identity(3);

                // actual = 
                // [  1   0   0
                //    0   1   0
                //    0   0   1  ]

                var expected = new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[9] { 1, 0, 0, 0, 1, 0, 0, 0, 1 },
                    numberOfRows: 3,
                    numberOfColumns: 3);

                ComplexMatrixAssert.IsStateAsExpected(expected, actual, ComplexMatrixTest.Accuracy);
                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                ComplexArrayAssert.AreEqual(new Complex[3] { 1, 1, 1 }, actual.GetStorage(), ComplexMatrixTest.Accuracy);
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
                        var actual = ComplexMatrix.Sparse(0, 1, 2);
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
                        var actual = ComplexMatrix.Sparse(1, 0, 2);
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
                        var actual = ComplexMatrix.Sparse(1, 1, -1);
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
                var actual = ComplexMatrix.Sparse(numberOfRows, numberOfColumns, capacity);

                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: new ComplexMatrixState(
                        asColumnMajorDenseArray: new Complex[6],
                        numberOfRows: numberOfRows,
                        numberOfColumns: numberOfColumns),
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);

                Assert.AreEqual(StorageOrder.RowMajor, actual.StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.StorageScheme);
                Assert.AreEqual(StorageOrder.RowMajor, actual.AsReadOnly().StorageOrder);
                Assert.AreEqual(StorageScheme.CompressedRow, actual.AsReadOnly().StorageScheme);
                ComplexArrayAssert.AreEqual(Array.Empty<Complex>(), actual.GetStorage(), ComplexMatrixTest.Accuracy);
            }
        }

        #endregion

        #region Find

        [TestMethod()]
        public void FindTest()
        {
            ComplexMatrixTest.Find.Value(
                testableMatrix: TestableComplexMatrix35.Get(),
                value: 0.0,
                expected: IndexCollection.FromArray(new int[] { 0, 3, 4 }));

            ComplexMatrixTest.Find.Value(
                testableMatrix: TestableComplexMatrix35.Get(),
                value: Complex.NaN,
                expected: IndexCollection.FromArray(new int[] { 5 }));

            ComplexMatrixTest.Find.Value(
                testableMatrix: TestableComplexMatrix36.Get(),
                value: new Complex(1.0, 1.0),
                expected: null);

            ComplexMatrixTest.Find.Value(
                testableMatrix: TestableComplexMatrix37.Get(),
                value: new Complex(2.0, 2.0),
                expected: IndexCollection.FromArray(new int[] { 1, 2, 4 }));
        }

        [TestMethod()]
        public void FindNonzeroTest()
        {
            ComplexMatrixTest.Find.Nonzero(
                testableMatrix: TestableComplexMatrix35.Get(),
                expected: IndexCollection.FromArray(new int[] { 1, 2, 5 }));

            ComplexMatrixTest.Find.Nonzero(
                testableMatrix: TestableComplexMatrix36.Get(),
                expected: null);
        }

        [TestMethod()]
        public void FindWhileTest()
        {
            ComplexMatrixTest.Find.While.MatchIsNull(TestableComplexMatrix35.Get());

            ComplexMatrixTest.Find.While.Succeed(
                testableMatrix: TestableComplexMatrix35.Get(),
                match: (c) => { return c.Real <= 1 && c.Imaginary <= 1; },
                expected: IndexCollection.FromArray(new int[] { 0, 3, 4 }));

            ComplexMatrixTest.Find.While.Succeed(
                testableMatrix: TestableComplexMatrix35.Get(),
                match: (c) => { return c.Real < -1 && c.Imaginary < -1.0; },
                expected: null);

            ComplexMatrixTest.Find.While.Succeed(
                testableMatrix: TestableComplexMatrix35.Get(),
                match: (c) => { return c.Real >= 0 && c.Imaginary >= 0; },
                expected: IndexCollection.FromArray(new int[] { 0, 1, 2, 3, 4 }));
        }

        #endregion

        #region Algebraic operators

        [TestMethod()]
        public void AddTest()
        {
            #region Complex, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullComplexMatrixComplexMatrixAddition.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.RightIsNull(
                RightIsNullComplexMatrixComplexMatrixAddition.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongRowsComplexMatrixComplexMatrixAddition.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongColsComplexMatrixComplexMatrixAddition.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                LeftIsScalarComplexMatrixComplexMatrixAddition.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsScalarComplexMatrixComplexMatrixAddition.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                TypicalComplexMatrixComplexMatrixAddition.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullComplexMatrixComplexScalarAddition.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                RightIsNeutralComplexMatrixComplexScalarAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                TypicalComplexMatrixComplexScalarAddition.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.RightIsNull(
                RightIsNullComplexScalarComplexMatrixAddition.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                LeftIsNeutralComplexScalarComplexMatrixAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                TypicalComplexScalarComplexMatrixAddition.Get());

            #endregion

            #region Complex, Double

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.LeftIsNull(
                LeftIsNullComplexMatrixDoubleMatrixAddition.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.RightIsNull(
                RightIsNullComplexMatrixDoubleMatrixAddition.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongRowsComplexMatrixDoubleMatrixAddition.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongColsComplexMatrixDoubleMatrixAddition.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                LeftIsScalarComplexMatrixDoubleMatrixAddition.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsScalarComplexMatrixDoubleMatrixAddition.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                TypicalComplexMatrixDoubleMatrixAddition.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.LeftIsNull(
                LeftIsNullComplexMatrixDoubleScalarAddition.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                RightIsNeutralComplexMatrixDoubleScalarAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                TypicalComplexMatrixDoubleScalarAddition.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.RightIsNull(
                RightIsNullComplexScalarDoubleMatrixAddition.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                LeftIsNeutralComplexScalarDoubleMatrixAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                TypicalComplexScalarDoubleMatrixAddition.Get());

            #endregion

            #region Double, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullDoubleMatrixComplexMatrixAddition.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.RightIsNull(
                RightIsNullDoubleMatrixComplexMatrixAddition.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongRowsDoubleMatrixComplexMatrixAddition.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongColsDoubleMatrixComplexMatrixAddition.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                LeftIsScalarDoubleMatrixComplexMatrixAddition.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsScalarDoubleMatrixComplexMatrixAddition.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                TypicalDoubleMatrixComplexMatrixAddition.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullDoubleMatrixComplexScalarAddition.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                RightIsNeutralDoubleMatrixComplexScalarAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                TypicalDoubleMatrixComplexScalarAddition.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.RightIsNull(
                RightIsNullDoubleScalarComplexMatrixAddition.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                LeftIsNeutralDoubleScalarComplexMatrixAddition.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                TypicalDoubleScalarComplexMatrixAddition.Get());

            #endregion
        }

        [TestMethod()]
        public void DivideTest()
        {
            #region Complex, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.RightIsNull(
                RightIsNullComplexMatrixComplexMatrixDivision.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongColsComplexMatrixComplexMatrixDivision.Get());

            // ----- Unsquare, rank deficient right operand
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                LeftIsScalarComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsScalarComplexMatrixComplexMatrixDivision.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsLowerTriangularComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsUpperTriangularComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsHessenbergComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsSymmetricComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsRectangularLessRowsThanColsComplexMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullComplexMatrixComplexScalarDivision.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                RightIsNeutralComplexMatrixComplexScalarDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                TypicalComplexMatrixComplexScalarDivision.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.RightIsNull(
                RightIsNullComplexScalarComplexMatrixDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                TypicalComplexScalarComplexMatrixDivision.Get());

            #endregion

            #region Complex, Double

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.LeftIsNull(
                LeftIsNullComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.RightIsNull(
                RightIsNullComplexMatrixDoubleMatrixDivision.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongColsComplexMatrixDoubleMatrixDivision.Get());

            // ----- Unsquare, rank deficient right operand
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightIsRectangularAndRankDeficientComplexMatrixDoubleMatrixDivision.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                LeftIsScalarComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsScalarComplexMatrixDoubleMatrixDivision.Get());

            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
             RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsLowerTriangularComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsUpperTriangularComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsHessenbergComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsSymmetricComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsSymmetricAndNonPosDefiniteComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.LeftIsNull(
                LeftIsNullComplexMatrixDoubleScalarDivision.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                RightIsNeutralComplexMatrixDoubleScalarDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                TypicalComplexMatrixDoubleScalarDivision.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.RightIsNull(
                RightIsNullComplexScalarDoubleMatrixDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                TypicalComplexScalarDoubleMatrixDivision.Get());

            #endregion

            #region Double, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.RightIsNull(
                RightIsNullDoubleMatrixComplexMatrixDivision.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongColsDoubleMatrixComplexMatrixDivision.Get());

            // ----- Unsquare, rank deficient right operand
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightIsRectangularAndRankDeficientDoubleMatrixComplexMatrixDivision.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                LeftIsScalarDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsScalarDoubleMatrixComplexMatrixDivision.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
             RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsLowerTriangularDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsUpperTriangularDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsHessenbergDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsSymmetricDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsSymmetricAndNonPosDefiniteDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullDoubleMatrixComplexScalarDivision.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                RightIsNeutralDoubleMatrixComplexScalarDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                TypicalDoubleMatrixComplexScalarDivision.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.RightIsNull(
                RightIsNullDoubleScalarComplexMatrixDivision.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                TypicalDoubleScalarComplexMatrixDivision.Get());

            #endregion
        }

        [TestMethod()]
        public void ElementWiseMultiplyTest()
        {
            #region Complex, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullComplexMatrixComplexMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.RightIsNull(
                RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongColsComplexMatrixComplexMatrixElementWiseMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                TypicalComplexMatrixComplexMatrixElementWiseMultiplication.Get());

            #endregion

            #region Complex, Double

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.LeftIsNull(
                LeftIsNullComplexMatrixDoubleMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.RightIsNull(
                RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongRowsComplexMatrixDoubleMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongColsComplexMatrixDoubleMatrixElementWiseMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                TypicalComplexMatrixDoubleMatrixElementWiseMultiplication.Get());

            #endregion

            #region Double, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.RightIsNull(
                RightIsNullDoubleMatrixComplexMatrixElementWiseMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongRowsDoubleMatrixComplexMatrixElementWiseMultiplication.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongColsDoubleMatrixComplexMatrixElementWiseMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                TypicalDoubleMatrixComplexMatrixElementWiseMultiplication.Get());

            #endregion
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            #region Complex, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullComplexMatrixComplexMatrixMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.RightIsNull(
                RightIsNullComplexMatrixComplexMatrixMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongRowsComplexMatrixComplexMatrixMultiplication.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                LeftIsScalarComplexMatrixComplexMatrixMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsScalarComplexMatrixComplexMatrixMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                TypicalComplexMatrixComplexMatrixMultiplication.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullComplexMatrixComplexScalarMultiplication.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                RightIsNeutralComplexMatrixComplexScalarMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                TypicalComplexMatrixComplexScalarMultiplication.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.RightIsNull(
                RightIsNullComplexScalarComplexMatrixMultiplication.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                LeftIsNeutralComplexScalarComplexMatrixMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                TypicalComplexScalarComplexMatrixMultiplication.Get());

            #endregion

            #region Complex, Double

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.LeftIsNull(
                LeftIsNullComplexMatrixDoubleMatrixMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.RightIsNull(
                RightIsNullComplexMatrixDoubleMatrixMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongRowsComplexMatrixDoubleMatrixMultiplication.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                LeftIsScalarComplexMatrixDoubleMatrixMultiplication.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsScalarComplexMatrixDoubleMatrixMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                TypicalComplexMatrixDoubleMatrixMultiplication.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.LeftIsNull(
                LeftIsNullComplexMatrixDoubleScalarMultiplication.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                RightIsNeutralComplexMatrixDoubleScalarMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                TypicalComplexMatrixDoubleScalarMultiplication.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.RightIsNull(
                RightIsNullComplexScalarDoubleMatrixMultiplication.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                LeftIsNeutralComplexScalarDoubleMatrixMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                TypicalComplexScalarDoubleMatrixMultiplication.Get());

            #endregion

            #region Double, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullDoubleMatrixComplexMatrixMultiplication.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.RightIsNull(
                RightIsNullDoubleMatrixComplexMatrixMultiplication.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongRowsDoubleMatrixComplexMatrixMultiplication.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                LeftIsScalarDoubleMatrixComplexMatrixMultiplication.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsScalarDoubleMatrixComplexMatrixMultiplication.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                TypicalDoubleMatrixComplexMatrixMultiplication.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullDoubleMatrixComplexScalarMultiplication.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                RightIsNeutralDoubleMatrixComplexScalarMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                TypicalDoubleMatrixComplexScalarMultiplication.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.RightIsNull(
                RightIsNullDoubleScalarComplexMatrixMultiplication.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                LeftIsNeutralDoubleScalarComplexMatrixMultiplication.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                TypicalDoubleScalarComplexMatrixMultiplication.Get());

            #endregion
        }

        [TestMethod()]
        public void NegateTest()
        {
            // ----- Null operand
            UnaryOperationTest.OperandIsNull(OperandIsNullComplexMatrixNegation.Get());

            // ----- Typical operand
            UnaryOperationTest.Succeed(TypicalComplexMatrixNegation.Get());
        }

        [TestMethod()]
        public void SubtractTest()
        {
            #region Complex, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullComplexMatrixComplexMatrixSubtraction.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.RightIsNull(
                RightIsNullComplexMatrixComplexMatrixSubtraction.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongRowsComplexMatrixComplexMatrixSubtraction.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Fail(
                RightWrongColsComplexMatrixComplexMatrixSubtraction.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                LeftIsScalarComplexMatrixComplexMatrixSubtraction.Get());
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                RightIsScalarComplexMatrixComplexMatrixSubtraction.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightComplexMatrix.Succeed(
                TypicalComplexMatrixComplexMatrixSubtraction.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullComplexMatrixComplexScalarSubtraction.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                RightIsNeutralComplexMatrixComplexScalarSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightComplexScalar.Succeed(
                TypicalComplexMatrixComplexScalarSubtraction.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.RightIsNull(
                RightIsNullComplexScalarComplexMatrixSubtraction.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                LeftIsNeutralComplexScalarComplexMatrixSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightComplexMatrix.Succeed(
                TypicalComplexScalarComplexMatrixSubtraction.Get());

            #endregion

            #region Complex, Double

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.LeftIsNull(
                LeftIsNullComplexMatrixDoubleMatrixSubtraction.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.RightIsNull(
                RightIsNullComplexMatrixDoubleMatrixSubtraction.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongRowsComplexMatrixDoubleMatrixSubtraction.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Fail(
                RightWrongColsComplexMatrixDoubleMatrixSubtraction.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                LeftIsScalarComplexMatrixDoubleMatrixSubtraction.Get());
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                RightIsScalarComplexMatrixDoubleMatrixSubtraction.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleMatrix.Succeed(
                TypicalComplexMatrixDoubleMatrixSubtraction.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.LeftIsNull(
                LeftIsNullComplexMatrixDoubleScalarSubtraction.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                RightIsNeutralComplexMatrixDoubleScalarSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexMatrixRightDoubleScalar.Succeed(
                TypicalComplexMatrixDoubleScalarSubtraction.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.RightIsNull(
                RightIsNullComplexScalarDoubleMatrixSubtraction.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                LeftIsNeutralComplexScalarDoubleMatrixSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftComplexScalarRightDoubleMatrix.Succeed(
                TypicalComplexScalarDoubleMatrixSubtraction.Get());

            #endregion

            #region Double, Complex

            // (Matrix, Matrix) 

            // ----- Null operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.LeftIsNull(
                LeftIsNullDoubleMatrixComplexMatrixSubtraction.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.RightIsNull(
                RightIsNullDoubleMatrixComplexMatrixSubtraction.Get());

            // ----- Mismatched operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongRowsDoubleMatrixComplexMatrixSubtraction.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Fail(
                RightWrongColsDoubleMatrixComplexMatrixSubtraction.Get());

            // ----- Scalar operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                LeftIsScalarDoubleMatrixComplexMatrixSubtraction.Get());
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                RightIsScalarDoubleMatrixComplexMatrixSubtraction.Get());

            // ----- Typical Matrix operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexMatrix.Succeed(
                TypicalDoubleMatrixComplexMatrixSubtraction.Get());

            // (Matrix, Scalar)

            // ----- Left null operand
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.LeftIsNull(
                LeftIsNullDoubleMatrixComplexScalarSubtraction.Get());

            // ----- Right operand is neutral
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                RightIsNeutralDoubleMatrixComplexScalarSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleMatrixRightComplexScalar.Succeed(
                TypicalDoubleMatrixComplexScalarSubtraction.Get());

            // (Scalar, Matrix)

            // ----- Right null operand
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.RightIsNull(
                RightIsNullDoubleScalarComplexMatrixSubtraction.Get());

            // ----- Left operand is neutral
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                LeftIsNeutralDoubleScalarComplexMatrixSubtraction.Get());

            // ----- Typical operands
            BinaryOperationTest.LeftDoubleScalarRightComplexMatrix.Succeed(
                TypicalDoubleScalarComplexMatrixSubtraction.Get());

            #endregion
        }

        #endregion

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            // ComplexMatrix
            {
                // IEnumerable.GetEnumerator
                {
                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    });
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

                // IEnumerable<Complex>.GetEnumerator
                {
                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    });
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

                // IEnumerable<Complex>.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    });
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

                // IEnumerable.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    });
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

            // ReadOnlyComplexMatrix
            {
                // IEnumerable.GetEnumerator
                {
                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    }).AsReadOnly();

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

                // IEnumerable<Complex>.GetEnumerator
                {
                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    }).AsReadOnly();

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

                // IEnumerable<Complex>.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    }).AsReadOnly();

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

                // IEnumerable.Current fails
                {
                    string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                    var target = ComplexMatrix.Dense(2, 3, new Complex[6]
                    {
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5),
                        new Complex(6, 6)
                    }).AsReadOnly();

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
            TestAction(ComplexMatrixTest.Apply.InPlace.FuncIsNull, GetTestableMatrices());

            TestAction(ComplexMatrixTest.Apply.InPlace.Succeed, GetTestableMatrices());

            TestAction(ComplexMatrixTest.Apply.OutPlace.FuncIsNull, GetTestableMatrices());

            // Add 0
            {
                Complex addZero(Complex x) { return x + 0.0; }

                ComplexMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableComplexMatrix16.Get(),
                    func: addZero,
                    expected: new ComplexMatrixState(
                                    asColumnMajorDenseArray: new Complex[6]
                                    {
                                        new Complex(0, 0),
                                        new Complex(1, 1),
                                        new Complex(2, 2),
                                        new Complex(3, 3),
                                        new Complex(4, 4),
                                        new Complex(5, 5)
                                    },
                                    numberOfRows: 2,
                                    numberOfColumns: 3));

                ComplexMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableComplexMatrix38.Get(),
                    func: addZero,
                    expected: new ComplexMatrixState(
                                    asColumnMajorDenseArray: new Complex[20]
                                        {
                                            0, 0, 0, 0, 0,
                                            0, 0, new Complex(2, 2), new Complex(3, 3), new Complex(5, 5),
                                            0, new Complex(6, 6), 0, new Complex(7, 7), 0,
                                            0, new Complex(4, 4), 0, 0, 0
                                        },
                                    numberOfRows: 4,
                                    numberOfColumns: 5));

                ComplexMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableComplexMatrix39.Get(),
                    func: addZero,
                    expected: new ComplexMatrixState(
                                    asColumnMajorDenseArray: new Complex[30]
                                        {
                                            0, 0, new Complex(3, 3),
                                            0, 0, 0,
                                            new Complex(3, 3), new Complex(5, 5), new Complex(1, 1),
                                            0, new Complex(7, 7), 0,
                                            new Complex(4, 4), 0, 0,
                                            0, 0, 0,
                                            0, new Complex(2, 2), 0,
                                            0, new Complex(1, 1), 0,
                                            new Complex(1, 1), 0, 0,
                                            0, 0, new Complex(1, 1)
                                        },
                                    numberOfRows: 3,
                                    numberOfColumns: 10));
            }

            // Add 1
            {
                Complex addOne(Complex x) { return x + new Complex(1.0, 1); }

                ComplexMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableComplexMatrix16.Get(),
                    func: addOne,
                    expected: new ComplexMatrixState(
                                    asColumnMajorDenseArray: new Complex[6]
                                        {
                                            new Complex(1, 1),
                                            new Complex(2, 2),
                                            new Complex(3, 3),
                                            new Complex(4, 4),
                                            new Complex(5, 5),
                                            new Complex(6, 6)
                                        },
                                    numberOfRows: 2,
                                    numberOfColumns: 3));

                ComplexMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableComplexMatrix38.Get(),
                    func: addOne,
                    expected: new ComplexMatrixState(
                                    asColumnMajorDenseArray: new Complex[20]
                                        {
                                            new Complex(1, 1), new Complex(1, 1), new Complex(1, 1), new Complex(1, 1), new Complex(1, 1),
                                            new Complex(1, 1), new Complex(1, 1), new Complex(3, 3), new Complex(4, 4), new Complex(6, 6),
                                            new Complex(1, 1), new Complex(7, 7), new Complex(1, 1), new Complex(8, 8), new Complex(1, 1),
                                            new Complex(1, 1), new Complex(5, 5), new Complex(1, 1), new Complex(1, 1), new Complex(1, 1)
                                        },
                                    numberOfRows: 4,
                                    numberOfColumns: 5));

                ComplexMatrixTest.Apply.OutPlace.Succeed(
                    testableMatrix: TestableComplexMatrix39.Get(),
                    func: addOne,
                    expected: new ComplexMatrixState(
                                    asColumnMajorDenseArray: new Complex[30]
                                        {
                                            new Complex(1, 1), new Complex(1, 1), new Complex(4, 4),
                                            new Complex(1, 1), new Complex(1, 1), new Complex(1, 1),
                                            new Complex(4, 4), new Complex(6, 6), new Complex(2, 2),
                                            new Complex(1, 1), new Complex(8, 8), new Complex(1, 1),
                                            new Complex(5, 5), new Complex(1, 1), new Complex(1, 1),
                                            new Complex(1, 1), new Complex(1, 1), new Complex(1, 1),
                                            new Complex(1, 1), new Complex(3, 3), new Complex(1, 1),
                                            new Complex(1, 1), new Complex(2, 2), new Complex(1, 1),
                                            new Complex(2, 2), new Complex(1, 1), new Complex(1, 1),
                                            new Complex(1, 1), new Complex(1, 1), new Complex(2, 2)
                                        },
                                    numberOfRows: 3,
                                    numberOfColumns: 10));
            }
        }

        [TestMethod()]
        public void ConjugateTest()
        {

            TestAction(ComplexMatrixTest.Conjugate.InPlace, GetTestableMatrices());

            var c00 = new Complex(0, 0);
            var c11 = new Complex(1, 1);
            var c22 = new Complex(2, 2);
            var c33 = new Complex(3, 3);
            var c44 = new Complex(4, 4);
            var c55 = new Complex(5, 5);
            var c66 = new Complex(6, 6);
            var c77 = new Complex(7, 7);

            static Complex C(Complex v) => Complex.Conjugate(v); ;

            ComplexMatrixTest.Conjugate.OutPlace(
                testableMatrix: TestableComplexMatrix38.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[20]
                                    {
                                        C(c00),
                                        C(c00),
                                        C(c00),
                                        C(c00),
                                        C(c00),
                                        C(c00),
                                        C(c00),
                                        C(c22),
                                        C(c33),
                                        C(c55),
                                        C(c00),
                                        C(c66),
                                        C(c00),
                                        C(c77),
                                        C(c00),
                                        C(c00),
                                        C(c44),
                                        C(c00),
                                        C(c00),
                                        C(c00)
                                    },
                                numberOfRows: 4,
                                numberOfColumns: 5));

            ComplexMatrixTest.Conjugate.OutPlace(
                testableMatrix: TestableComplexMatrix39.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[30]
                                    {
                                        C(c00), C(c00), C(c33),
                                        C(c00), C(c00), C(c00),
                                        C(c33), C(c55), C(c11),
                                        C(c00), C(c77), C(c00),
                                        C(c44), C(c00), C(c00),
                                        C(c00), C(c00), C(c00),
                                        C(c00), C(c22), C(c00),
                                        C(c00), C(c11), C(c00),
                                        C(c11), C(c00), C(c00),
                                        C(c00), C(c00), C(c11)
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 10));

            ComplexMatrixTest.Conjugate.OutPlace(
                testableMatrix: TestableComplexMatrix16.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                        0,
                                        C(c11),
                                        C(c22),
                                        C(c33),
                                        C(c44),
                                        C(c55)
                                    },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

            ComplexMatrixTest.Conjugate.OutPlace(
                testableMatrix: TestableComplexMatrix57.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                        0,
                                        C(c11),
                                        C(c22),
                                        C(c33),
                                        C(c44),
                                        C(c55)
                                    },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } }));

            ComplexMatrixTest.Conjugate.OutPlace(
                testableMatrix: TestableComplexMatrix58.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                        0,
                                        C(c11),
                                        C(c22),
                                        C(c33),
                                        C(c44),
                                        C(c55)
                                    },
                                numberOfRows: 2,
                                numberOfColumns: 3,
                                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

        }

        [TestMethod()]
        public void ConjugateTransposeTest()
        {

            TestAction(ComplexMatrixTest.ConjugateTranspose.InPlace, GetTestableMatrices());

            var c11 = new Complex(1, 1);
            var c22 = new Complex(2, 2);
            var c33 = new Complex(3, 3);
            var c44 = new Complex(4, 4);
            var c55 = new Complex(5, 5);
            var c66 = new Complex(6, 6);
            var c77 = new Complex(7, 7);

            static Complex C(Complex v) => Complex.Conjugate(v); ;

            ComplexMatrixTest.ConjugateTranspose.OutPlace(
                testableMatrix: TestableComplexMatrix38.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[20]
                                    {
                                      0, 0,      C(c33), 0,      C(c44),
                                      0, 0,      C(c55), C(c77), 0,
                                      0, 0,      0,      0,      0,
                                      0, C(c22), C(c66), 0,      0
                                    },
                                numberOfRows: 5,
                                numberOfColumns: 4));

            ComplexMatrixTest.ConjugateTranspose.OutPlace(
                testableMatrix: TestableComplexMatrix39.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[30]
                                    {
                                        0,      0, C(c33), 0,      C(c44), 0, 0,      0,      C(c11), 0,
                                        0,      0, C(c55), C(c77), 0,      0, C(c22), C(c11), 0,      0,
                                        C(c33), 0, C(c11), 0,      0,      0, 0,      0,      0,      C(c11)
                                    },
                                numberOfRows: 10,
                                numberOfColumns: 3));

            ComplexMatrixTest.ConjugateTranspose.OutPlace(
                testableMatrix: TestableComplexMatrix16.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                        0, 
                                        C(c22), 
                                        C(c44), 
                                        C(c11), 
                                        C(c33), 
                                        C(c55)
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                columnNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                rowNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

            ComplexMatrixTest.ConjugateTranspose.OutPlace(
                testableMatrix: TestableComplexMatrix57.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                        0, 
                                        C(c22), 
                                        C(c44), 
                                        C(c11), 
                                        C(c33), 
                                        C(c55)
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                columnNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } }));

            ComplexMatrixTest.ConjugateTranspose.OutPlace(
                testableMatrix: TestableComplexMatrix58.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                        0, 
                                        C(c22), 
                                        C(c44), 
                                        C(c11), 
                                        C(c33), 
                                        C(c55)
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                rowNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

        }

        [TestMethod()]
        public void TransposeTest()
        {

            TestAction(ComplexMatrixTest.Transpose.InPlace, GetTestableMatrices());

            var c11 = new Complex(1, 1);
            var c22 = new Complex(2, 2);
            var c33 = new Complex(3, 3);
            var c44 = new Complex(4, 4);
            var c55 = new Complex(5, 5);
            var c66 = new Complex(6, 6);
            var c77 = new Complex(7, 7);

            ComplexMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableComplexMatrix38.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[20]
                                    {
                                      0, 0,   c33, 0,   c44,
                                      0, 0,   c55, c77, 0,
                                      0, 0,   0,   0,   0,
                                      0, c22, c66, 0,   0
                                    },
                                numberOfRows: 5,
                                numberOfColumns: 4));

            ComplexMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableComplexMatrix39.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[30]
                                    {
                                        0,   0, c33, 0,    c44, 0,   0,   0,   c11, 0,
                                        0,   0, c55, c77,  0,   0,   c22, c11, 0,   0,
                                        c33, 0, c11, 0,    0,   0,   0,   0,   0,   c11
                                    },
                                numberOfRows: 10,
                                numberOfColumns: 3));

            ComplexMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableComplexMatrix16.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                      0, c22, c44, c11, c33, c55
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                columnNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                                rowNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } }));

            ComplexMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableComplexMatrix57.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                      0, c22, c44, c11, c33, c55
                                    },
                                numberOfRows: 3,
                                numberOfColumns: 2,
                                columnNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } }));

            ComplexMatrixTest.Transpose.OutPlace(
                testableMatrix: TestableComplexMatrix58.Get(),
                expected: new ComplexMatrixState(
                                asColumnMajorDenseArray: new Complex[6]
                                    {
                                      0, c22, c44, c11, c33, c55
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
                TestAction(ComplexMatrixTest.Vec.AllIndexes, GetTestableMatrices());
            }

            // Specific linear indexes
            {
                TestAction(ComplexMatrixTest.Vec.SpecificIndexes, GetTestableMatrices());
            }

            // Specific linear indexes
            {
                var c22 = new Complex(2, 2);
                var c33 = new Complex(3, 3);

                // linearIndexes is null
                {
                    var target = ComplexMatrix.Dense(
                        3,
                        2,
                        new Complex[6] { 0, 0, 0, c22, c33, Complex.NaN });

                    target.SetRowName(0, "Row0");
                    target.SetRowName(1, "Row1");
                    target.SetRowName(2, "Row2");

                    // target = 
                    // [Row0] [  0   2,2   
                    // [Row1]    0   3,3
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

                    ComplexMatrix target;

                    // Dense
                    target = TestableComplexMatrix00.Get().AsDense;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target.Vec(IndexCollection.Range(0, target.Count));
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // Sparse
                    target = TestableComplexMatrix00.Get().AsSparse;

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
                    TestAction(ComplexMatrixTest.Vec.SpecificIndexes, GetTestableMatrices());
                }
            }
        }

        #region IList<Complex>

        [TestMethod()]
        public void ContainsTest()
        {
            ComplexMatrixTest.Contains(
                testableMatrix: TestableComplexMatrix35.Get(),
                value: 0.0,
                expected: true);

            ComplexMatrixTest.Contains(
                testableMatrix: TestableComplexMatrix35.Get(),
                value: Complex.NaN,
                expected: true);

            ComplexMatrixTest.Contains(
                testableMatrix: TestableComplexMatrix36.Get(),
                value: new Complex(1.0, 1.0),
                expected: false);

            ComplexMatrixTest.Contains(
                testableMatrix: TestableComplexMatrix37.Get(),
                value: new Complex(2.0, 2.0),
                expected: true);
        }

        [TestMethod()]
        public void CopyToTest()
        {
            TestAction(ComplexMatrixTest.CopyTo.ArrayIsNull, GetTestableMatrices());

            TestAction(ComplexMatrixTest.CopyTo.ArrayIndexIsNegative, GetTestableMatrices());

            TestAction(ComplexMatrixTest.CopyTo.ArrayHasNotEnoughSpace, GetTestableMatrices());

            ComplexMatrixTest.CopyTo.Succeed(
                testableMatrix: TestableComplexMatrix09.Get(),
                array: new Complex[6] { 10, 20, 30, 40, 50, 60 },
                arrayIndex: 1,
                expected: new Complex[6] { 10, new Complex(1, 1), 0, 0, 0, 60 },
                delta: ComplexMatrixTest.Accuracy);

            ComplexMatrixTest.CopyTo.Succeed(
               testableMatrix: TestableComplexMatrix13.Get(),
               array: new Complex[6] { 10, 20, 30, 40, 50, 60 },
               arrayIndex: 2,
               expected: new Complex[6] { 10, 20, new Complex(1.1, 1.1), 0, 0, new Complex(4.4, 4.4) },
               delta: ComplexMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void IListGetTest()
        {
            TestAction(ComplexMatrixTest.GetItem.AnyLinearIndexIsOutOfRange, GetTestableMatrices());

            ComplexMatrixTest.GetItem.Succeed(
                testableMatrix: TestableComplexMatrix02.Get(),
                linearIndex: 0,
                expected: new Complex(2.0, 2.0));

            ComplexMatrixTest.GetItem.Succeed(
                testableMatrix: TestableComplexMatrix02.Get(),
                linearIndex: 2,
                expected: 0.0);

            ComplexMatrixTest.GetItem.Succeed(
                testableMatrix: TestableComplexMatrix02.Get(),
                linearIndex: 4,
                expected: new Complex(1.0, 1.0));
        }

        [TestMethod()]
        public void IListSetTest()
        {
            TestAction(ComplexMatrixTest.SetItem.AnyLinearIndexIsOutOfRange, GetTestableMatrices());

            TestAction(ComplexMatrixTest.SetItem.InstanceIsReadOnly, GetTestableMatrices());

            ComplexMatrixTest.SetItem.Succeed(
                testableMatrix: TestableComplexMatrix02.Get(),
                linearIndex: 0,
                expected: new Complex(2.0, 2.0));

            ComplexMatrixTest.SetItem.Succeed(
                testableMatrix: TestableComplexMatrix02.Get(),
                linearIndex: 2,
                expected: 0.0);

            ComplexMatrixTest.SetItem.Succeed(
                testableMatrix: TestableComplexMatrix02.Get(),
                linearIndex: 4,
                expected: new Complex(1.0, 1.0));
        }

        [TestMethod()]
        public void IListInsertTest()
        {
            // ComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<Complex>)target).Insert(0, 0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<Complex>)target).Insert(0, 0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        [TestMethod()]
        public void IListRemoveAtTest()
        {
            // ComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<Complex>)target).RemoveAt(0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((IList<Complex>)target).RemoveAt(0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            ComplexMatrixTest.IndexOf(
                testableMatrix: TestableComplexMatrix35.Get(),
                value: 0.0,
                expected: 0);

            ComplexMatrixTest.IndexOf(
                testableMatrix: TestableComplexMatrix35.Get(),
                value: Complex.NaN,
                expected: 5);

            ComplexMatrixTest.IndexOf(
                testableMatrix: TestableComplexMatrix36.Get(),
                value: new Complex(1.0, 1.0),
                expected: -1);

            ComplexMatrixTest.IndexOf(
                testableMatrix: TestableComplexMatrix37.Get(),
                value: new Complex(2.0, 2.0),
                expected: 1);
        }

        #endregion

        #region ICollection<Complex>

        [TestMethod()]
        public void ICollectionAddTest()
        {
            // ComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<Complex>)target).Add(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<Complex>)target).Add(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        [TestMethod()]
        public void ICollectionClearTest()
        {
            // ComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<Complex>)target).Clear();
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<Complex>)target).Clear();
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }


        [TestMethod()]
        public void IsReadOnlyTest()
        {
            TestAction(ComplexMatrixTest.IsReadOnly, GetTestableMatrices());
        }

        [TestMethod()]
        public void ICollectionRemoveTest()
        {
            // ComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense;
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<Complex>)target).Remove(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }

            // ReadOnlyComplexMatrix
            {
                var target = TestableComplexMatrix00.Get().AsDense.AsReadOnly();
                ExceptionAssert.Throw(
                    () =>
                    {
                        ((ICollection<Complex>)target).Remove(0.0);
                    },
                    expectedType: typeof(NotSupportedException),
                    expectedMessage: "Specified method is not supported.");
            }
        }

        #endregion
    }
}