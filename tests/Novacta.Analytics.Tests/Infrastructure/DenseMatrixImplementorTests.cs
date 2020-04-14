// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Infrastructure.Tests
{
    [TestClass]
    public class DenseMatrixImplementorTests
    {
        [TestMethod]
        public void LengthTest()
        {
            var implementor =
                new DenseMatrixImplementor<IndexCollection>(
                    numberOfRows: 2,
                    numberOfColumns: 3);

            Assert.AreEqual(
                expected: 6,
                actual: implementor.Length);
        }

        [TestMethod]
        public void NumberOfRowsTest()
        {
            var implementor =
                new DenseMatrixImplementor<IndexCollection>(
                    numberOfRows: 2,
                    numberOfColumns: 3);

            Assert.AreEqual(
                expected: 2,
                actual: implementor.NumberOfRows);
        }

        [TestMethod]
        public void NumberOfColumnsTest()
        {
            var implementor =
                new DenseMatrixImplementor<IndexCollection>(
                    numberOfRows: 2,
                    numberOfColumns: 3);

            Assert.AreEqual(
                expected: 3,
                actual: implementor.NumberOfColumns);
        }
    }
}
