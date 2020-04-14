// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Distribution;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class UniformDistributionTests
    {
        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var lowerBound = -1.1;
                var upperBound = 2.2;
                var distribution = new UniformDistribution(
                    lowerBound: lowerBound,
                    upperBound: upperBound);

                Assert.AreEqual(
                    expected: lowerBound,
                    actual: distribution.LowerBound,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: upperBound,
                    actual: distribution.UpperBound,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // upperBound == lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new UniformDistribution(lowerBound: 1.0, upperBound: 1.0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }

            // upperBound < lowerBound
            {
                string STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "upperBound",
                        "lowerBound");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new UniformDistribution(lowerBound: 2.0, upperBound: 1.0);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: "upperBound");
            }
        }

        [TestMethod()]
        public void DefaultTest()
        {
            var distribution = UniformDistribution.Default();

            Assert.AreEqual(
                expected: 0.0,
                actual: distribution.LowerBound,
                delta: DoubleMatrixTest.Accuracy);

            Assert.AreEqual(
                expected: 1.0,
                actual: distribution.UpperBound,
                delta: DoubleMatrixTest.Accuracy);
        }

        #endregion

        [TestMethod()]
        public void CanInvertCdfTest()
        {
            ProbabilityDistributionTest.CanInvertCdf.Succeed(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void CdfTest()
        {
            ProbabilityDistributionTest.Cdf.Succeed(
                TestableUniformDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Fail.ArgumentsIsNull(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void InverseCdfTest()
        {
            ProbabilityDistributionTest.InverseCdf.Succeed(
                TestableUniformDistribution00.Get());

            ProbabilityDistributionTest.InverseCdf.Fail.ArgumentsIsNull(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void MeanTest()
        {
            ProbabilityDistributionTest.Mean.Succeed(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void PdfTest()
        {
            ProbabilityDistributionTest.Pdf.Succeed(
                TestableUniformDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Fail.ArgumentsIsNull(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void SampleTest()
        {
            ProbabilityDistributionTest.Sample.Succeed(
                testableDistribution: TestableUniformDistribution00.Get(),
                sampleSize: 1000,
                delta: 0.01);

            ProbabilityDistributionTest.Sample.Fail.DestinationArrayIsNull(
                TestableUniformDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.DestinationIndexIsNegative(
                TestableUniformDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsNotPositive(
                TestableUniformDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsTooBig(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            ProbabilityDistributionTest.StandardDeviation.Succeed(
                TestableUniformDistribution00.Get());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            ProbabilityDistributionTest.Variance.Succeed(
                TestableUniformDistribution00.Get());
        }
    }
}
