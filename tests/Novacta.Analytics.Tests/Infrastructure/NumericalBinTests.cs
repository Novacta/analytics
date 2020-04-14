// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Infrastructure.Tests
{
    [TestClass]
    public class NumericalBinTests
    {
        [TestMethod]
        public void GetNumericalBinsTest()
        {
            NumericalBinTest.GetNumericalBins.Succeed.DataLengthIsOne();
            NumericalBinTest.GetNumericalBins.Succeed.DataLengthIsGreaterThanOne();
            NumericalBinTest.GetNumericalBins.Succeed.FinalCutPointExists();

            NumericalBinTest.GetNumericalBins.Fail.NumericalDataIsNull();
            NumericalBinTest.GetNumericalBins.Fail.TargetDataIsNull();
            NumericalBinTest.GetNumericalBins.Fail.NumericalAndTargetDataHaveDifferentLengths();
        }

        [TestMethod]
        public void ToStringTest()
        {
            var numericalData = DoubleMatrix.Dense(5, 1,
                new double[5] { -1, -1, -1, 1, 1 });
            var targetData = DoubleMatrix.Dense(5, 1,
                new double[5] { 10, 20, 10, 10, 10 });
            var bins = NumericalBin.GetNumericalBins(numericalData, targetData);

            // Two bins expected
            string expected, actual;

            // bins[0]

            expected = "Values: [-1, -1]. Positions: [0, 2]. " +
                "Target Distribution: [ " +
                "(10, 2) " +
                "(20, 1) ]";
            actual = bins[0].ToString();

            Assert.AreEqual(expected, actual);

            // bins[1]

            expected = "Values: [1, 1]. Positions: [3, 4]. " +
                "Target Distribution: [ " +
                "(10, 2) " +
                "(20, 0) ]";
            actual = bins[1].ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
