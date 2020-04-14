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
    public class ExponentialDistributionTests
    {
        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var rate = 1.1;
                var distribution = new ExponentialDistribution(
                    rate: rate);

                Assert.AreEqual(
                    expected: rate,
                    actual: distribution.Rate,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // rate == 0
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ExponentialDistribution(rate: 0.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "rate");
            }

            // rate < 0
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ExponentialDistribution(rate: -0.1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "rate");
            }
        }

        [TestMethod()]
        public void RateTest()
        {
            // Valid input
            {
                var distribution = new ExponentialDistribution(
                    rate: 1.0);

                var rate = 2.0;
                distribution.Rate = rate;

                Assert.AreEqual(
                    expected: rate,
                    actual: distribution.Rate,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // value == 0
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ExponentialDistribution(rate: 1.0).Rate = 0.0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "value");
            }

            // value < 0
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ExponentialDistribution(rate: 1.0).Rate = -1.0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "value");
            }
        }

        #endregion

        [TestMethod()]
        public void CanInvertCdfTest()
        {
            ProbabilityDistributionTest.CanInvertCdf.Succeed(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void CdfTest()
        {
            ProbabilityDistributionTest.Cdf.Succeed(
                TestableExponentialDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Fail.ArgumentsIsNull(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void InverseCdfTest()
        {
            ProbabilityDistributionTest.InverseCdf.Succeed(
                TestableExponentialDistribution00.Get());

            ProbabilityDistributionTest.InverseCdf.Fail.ArgumentsIsNull(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void MeanTest()
        {
            ProbabilityDistributionTest.Mean.Succeed(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void PdfTest()
        {
            ProbabilityDistributionTest.Pdf.Succeed(
                TestableExponentialDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Fail.ArgumentsIsNull(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void SampleTest()
        {
            ProbabilityDistributionTest.Sample.Succeed(
                testableDistribution: TestableExponentialDistribution00.Get(),
                sampleSize: 1000,
                delta: 0.01);

            ProbabilityDistributionTest.Sample.Fail.DestinationArrayIsNull(
                TestableExponentialDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.DestinationIndexIsNegative(
                TestableExponentialDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsNotPositive(
                TestableExponentialDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsTooBig(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            ProbabilityDistributionTest.StandardDeviation.Succeed(
                TestableExponentialDistribution00.Get());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            ProbabilityDistributionTest.Variance.Succeed(
                TestableExponentialDistribution00.Get());
        }
    }
}
