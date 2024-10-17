// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about complex matrices in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class ComplexMatrixAssert
    {
        /// <summary>
        /// Determines if a row in the given matrix has the
        /// specified name.
        /// </summary>
        /// <param name="target">The matrix to test.</param>
        /// <param name="columnName">The name of the row.</param>
        /// <returns><c>true</c> if a row in the matrix has the
        /// specified name, <c>false</c> otherwise.</returns>
        public static bool RowNameExists(ComplexMatrix obj, string rowName)
        {
            var rowNames = (Dictionary<int, string>)Reflector.GetField(
                obj,
                "rowNames");

            return rowNames.ContainsValue(rowName);
        }

        /// <summary>
        /// Determines if a column in the given matrix has the
        /// specified name.
        /// </summary>
        /// <param name="target">The matrix to test.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns><c>true</c> if a column in the matrix has the
        /// specified name, <c>false</c> otherwise.</returns>
        public static bool ColumnNameExists(ComplexMatrix target, string columnName)
        {
            var columnNames = (Dictionary<int, string>)Reflector.GetField(
                target,
                "columnNames");

            return columnNames.ContainsValue(columnName);
        }

        /// <summary>
        /// Determines whether the specified matrix has the expected state.
        /// </summary>
        /// <param name="expectedState">The expected state.</param>
        /// <param name="actualMatrix">The actual matrix.</param>
        /// <param name="delta">The required precision.</param>
        public static void IsStateAsExpected(
            ComplexMatrixState expectedState,
            ComplexMatrix actualMatrix,
            double delta)
        {
            // Null reference ?

            Assert.IsNotNull(actualMatrix);

            // Matrix Fields

            object implementor = Reflector.GetField(
                actualMatrix,
                "implementor");
            Assert.IsNotNull(implementor);

            Complex[] actualColumnMajorOrderedEntries;

            actualColumnMajorOrderedEntries =
                (Complex[])Reflector.ExecuteMember(
                    implementor,
                    "AsColumnMajorDenseArray",
                    null);

            Assert.IsNotNull(actualColumnMajorOrderedEntries);

            var expectedColumnMajorOrderedEntries = expectedState.AsColumnMajorDenseArray;
            Assert.AreEqual(
                expectedColumnMajorOrderedEntries.Length,
                actualColumnMajorOrderedEntries.Length);

            for (int Id = 0; Id < actualColumnMajorOrderedEntries.Length; Id++)
            {
                ComplexAssert.AreEqual(
                    expectedColumnMajorOrderedEntries[Id],
                    actualColumnMajorOrderedEntries[Id],
                    delta);
            }

            int actualNumberOfRows = (int)Reflector.GetProperty(
                implementor,
                "NumberOfRows");

            int expectedNumberOfRows = expectedState.NumberOfRows;

            Assert.AreEqual(expectedNumberOfRows, actualNumberOfRows);

            int actualNumberOfColumns = (int)Reflector.GetProperty(
                implementor,
                "NumberOfColumns");

            int expectedNumberOfColumns = expectedState.NumberOfColumns;
            Assert.AreEqual(expectedNumberOfColumns, actualNumberOfColumns);

            // Matrix Names

            // Matrix name

            Assert.AreEqual(expectedState.Name, actualMatrix.Name);

            // Row names

            // Both have row names
            bool expectedHasRowNames = expectedState.RowNames is not null;

            Assert.AreEqual(expectedHasRowNames, actualMatrix.HasRowNames);

            if (expectedHasRowNames)
            {
                CompareDimensionNames("row", expectedState.RowNames, actualMatrix.RowNames);
            }

            // Column names

            // Both have column names
            bool expectedHasColumnNames = expectedState.ColumnNames is not null;

            Assert.AreEqual(expectedHasColumnNames, actualMatrix.HasColumnNames);

            if (expectedHasColumnNames)
            {
                CompareDimensionNames("column", expectedState.ColumnNames, actualMatrix.ColumnNames);
            }
        }

        private static void MatrixNamesAreEqual(ComplexMatrix expected, ComplexMatrix actual)
        {
            // Matrix name

            Assert.AreEqual(expected.Name, actual.Name);

            // Row names

            // Both have row names
            Assert.AreEqual(expected.HasRowNames, actual.HasRowNames);

            if (expected.HasRowNames)
            {
                CompareDimensionNames("row", expected.RowNames, actual.RowNames);
            }

            // Column names

            // Both have column names
            Assert.AreEqual(expected.HasColumnNames, actual.HasColumnNames);

            if (expected.HasColumnNames)
            {
                CompareDimensionNames("column", expected.ColumnNames, actual.ColumnNames);
            }
        }

        private static void CompareDimensionNames(string dimensionName,
            IReadOnlyDictionary<int, string> expectedNames,
            IReadOnlyDictionary<int, string> actualNames)
        {
            // Both have the same number of names
            Assert.AreEqual(expectedNames.Count, actualNames.Count);
            foreach (var pair in expectedNames)
            {
                var position = pair.Key;
                if (actualNames.TryGetValue(position, out string actualName))
                {
                    string expectedName = pair.Value;
                    Assert.AreEqual(expectedName, actualName,
                        "Expected and actual names for {0} {1} are not the same.",
                        dimensionName, position);
                }
                else
                {
                    Assert.Fail("Expected name not found in actual {0} {1}.",
                       dimensionName, position);
                }
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="ComplexMatrix"/> 
        /// instances are equal.
        /// </summary>
        /// <param name="expected">The expected matrix.</param>
        /// <param name="actual">The actual matrix.</param>
        /// <param name="delta">The required accuracy.</param>
        public static void AreEqual(
            ComplexMatrix expected,
            ComplexMatrix actual,
            double delta)
        {
            // Null references ?

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            // Matrix Fields

            object expectedImplementor = Reflector.GetField(
                expected,
                "implementor");
            Assert.IsNotNull(expectedImplementor);

            var expectedColumnMajorOrderedEntries =
                (Complex[])Reflector.ExecuteMember(
                    expectedImplementor,
                    "AsColumnMajorDenseArray",
                    null);

            Assert.IsNotNull(expectedColumnMajorOrderedEntries);

            object actualImplementor = Reflector.GetField(
                actual,
                "implementor");
            Assert.IsNotNull(actualImplementor);

            var actualColumnMajorOrderedEntries =
                (Complex[])Reflector.ExecuteMember(
                    actualImplementor,
                    "AsColumnMajorDenseArray",
                    null);

            Assert.IsNotNull(actualColumnMajorOrderedEntries);

            Assert.AreEqual(
                expectedColumnMajorOrderedEntries.Length,
                actualColumnMajorOrderedEntries.Length);

            for (int l = 0; l < actualColumnMajorOrderedEntries.Length; l++)
            {
                ComplexAssert.AreEqual(
                    expectedColumnMajorOrderedEntries[l],
                    actualColumnMajorOrderedEntries[l],
                    delta);
            }

            int expectedNumberOfRows = (int)Reflector.GetProperty(
                expectedImplementor,
                "NumberOfRows");

            int actualNumberOfRows = (int)Reflector.GetProperty(
                actualImplementor,
                "NumberOfRows");

            Assert.AreEqual(expectedNumberOfRows, actualNumberOfRows);

            int expectedNumberOfColumns = (int)Reflector.GetProperty(
               expectedImplementor,
               "NumberOfColumns");

            int actualNumberOfColumns = (int)Reflector.GetProperty(
                actualImplementor,
                "NumberOfColumns");

            Assert.AreEqual(expectedNumberOfColumns, actualNumberOfColumns);

            // Matrix Names

            MatrixNamesAreEqual(expected, actual);
        }

        /// <summary>
        /// Checks that the specified <see cref="ReadOnlyComplexMatrix"/> 
        /// instances are equal.
        /// </summary>
        /// <param name="expected">The expected matrix.</param>
        /// <param name="actual">The actual matrix.</param>
        /// <param name="accuracy">The required accuracy.</param>
        public static void AreEqual(
            ReadOnlyComplexMatrix expected,
            ReadOnlyComplexMatrix actual,
            double delta)
        {
            AreEqual((ComplexMatrix)expected, (ComplexMatrix)actual, delta);
        }

        /// <summary>
        /// Checks that the specified <see cref="ReadOnlyComplexMatrix"/> 
        /// and <see cref="ComplexMatrix"/> instances are equal.
        /// </summary>
        /// <param name="expected">The expected matrix.</param>
        /// <param name="actual">The actual matrix.</param>
        /// <param name="delta">The required accuracy.</param>
        public static void AreEqual(
            ReadOnlyComplexMatrix expected,
            ComplexMatrix actual,
            double delta)
        {
            AreEqual((ComplexMatrix)expected, actual, delta);
        }

        /// <summary>
        /// Checks that the specified <see cref="ComplexMatrix"/> 
        /// and <see cref="ReadOnlyComplexMatrix"/> instances are equal.
        /// </summary>
        /// <param name="expected">The expected matrix.</param>
        /// <param name="actual">The actual matrix.</param>
        /// <param name="delta">The required accuracy.</param>
        public static void AreEqual(
            ComplexMatrix expected,
            ReadOnlyComplexMatrix actual,
            double delta)
        {
            AreEqual(expected, (ComplexMatrix)actual, delta);
        }
    }
}
