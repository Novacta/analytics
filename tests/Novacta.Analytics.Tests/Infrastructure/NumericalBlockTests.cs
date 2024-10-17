// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Infrastructure.Tests
{
    [TestClass]
    public class NumericalBlockTests
    {
        [TestMethod]
        public void GetNumericalBlocksTest()
        {
            NumericalBlockTest.GetNumericalBlocks.Succeed.DataLengthIsOne();
            NumericalBlockTest.GetNumericalBlocks.Succeed.DataLengthIsGreaterThanOne();
            NumericalBlockTest.GetNumericalBlocks.Succeed.FinalCutPointExists();

            NumericalBlockTest.GetNumericalBlocks.Fail.NumericalDataIsNull();
            NumericalBlockTest.GetNumericalBlocks.Fail.TargetDataIsNull();
            NumericalBlockTest.GetNumericalBlocks.Fail.NumericalAndTargetDataHaveDifferentLengths();
        }

        [TestMethod]
        public void ToStringTest()
        {
            var numericalData = DoubleMatrix.Dense(7, 1, 
                [1, 1, 1, 2, 2, 3, 4]);
            var targetData = DoubleMatrix.Dense(7, 1, 
                [10, 10, 10, 10, 10, 10, 5]);
            var blocks = NumericalBlock.GetNumericalBlocks(
                numericalData, 
                targetData);

            // Two blocks expected
            string expected, actual;

            // blocks[0]

            expected = "Values: [1, 3]. Positions: [0, 5]. " +
                "Target Distribution: [ " +
                "(5, 0) " +
                "(10, 6) ]";
            actual = blocks[0].ToString();

            Assert.AreEqual(expected, actual);

            // blocks[1]

            expected = "Values: [4, 4]. Positions: [6, 6]. " +
                "Target Distribution: [ " +
                "(5, 1) " +
                "(10, 0) ]";
            actual = blocks[1].ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
