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
    public class GaussianDistributionTests
    {
        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var mu = -1.1;
                var sigma = 2.2;
                var distribution = new GaussianDistribution(
                    mu: mu, 
                    sigma: sigma);

                Assert.AreEqual(
                    expected: mu,
                    actual: distribution.Mu,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: sigma,
                    actual: distribution.Sigma,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Non positive sigma
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new GaussianDistribution(mu: 1.0, sigma: 0.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "sigma");
            }
        }

        [TestMethod()]
        public void MuTest()
        {
            var distribution = GaussianDistribution.Standard();
            var expected = -1.123;
            distribution.Mu = expected;
            Assert.AreEqual(
                expected: expected,
                actual: distribution.Mu,
                delta: DoubleMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void SigmaTest()
        {
            // Valid sigma
            {
                var distribution = GaussianDistribution.Standard();
                var expected = 1.123;
                distribution.Sigma = expected;
                Assert.AreEqual(
                    expected: expected,
                    actual: distribution.Sigma,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // Non positive sigma
            {
                string STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_POSITIVE" });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        GaussianDistribution.Standard().Sigma = 0.0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "value");
            }
        }

        [TestMethod()]
        public void StandardTest()
        {
            var distribution = GaussianDistribution.Standard();

            Assert.AreEqual(
                expected: 0.0,
                actual: distribution.Mu,
                delta: DoubleMatrixTest.Accuracy);

            Assert.AreEqual(
                expected: 1.0,
                actual: distribution.Sigma,
                delta: DoubleMatrixTest.Accuracy);
        }

        #endregion

        [TestMethod()]
        public void CanInvertCdfTest()
        {
            ProbabilityDistributionTest.CanInvertCdf.Succeed(
                TestableGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void CdfTest()
        {
            ProbabilityDistributionTest.Cdf.Succeed(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Fail.ArgumentsIsNull(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Succeed(
                TestableBasicGaussianDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Fail.ArgumentsIsNull(
                TestableBasicGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void InverseCdfTest()
        {
            ProbabilityDistributionTest.InverseCdf.Succeed(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.InverseCdf.Fail.ArgumentsIsNull(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.InverseCdf.Succeed(
                TestableBasicGaussianDistribution00.Get());

            ProbabilityDistributionTest.InverseCdf.Fail.ArgumentsIsNull(
                TestableBasicGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void MeanTest()
        {
            ProbabilityDistributionTest.Mean.Succeed(
                TestableGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void PdfTest()
        {
            ProbabilityDistributionTest.Pdf.Succeed(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Fail.ArgumentsIsNull(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Succeed(
                TestableBasicGaussianDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Fail.ArgumentsIsNull(
                TestableBasicGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void SampleTest()
        {
            ProbabilityDistributionTest.Sample.Succeed(
                testableDistribution: TestableGaussianDistribution00.Get(),
                sampleSize: 1000,
                delta: 0.01);

            ProbabilityDistributionTest.Sample.Fail.DestinationArrayIsNull(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.DestinationIndexIsNegative(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsNotPositive(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsTooBig(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Succeed(
                testableDistribution: TestableBasicGaussianDistribution00.Get(),
                sampleSize: 1000,
                delta: 0.01);

            ProbabilityDistributionTest.Sample.Fail.DestinationArrayIsNull(
                TestableBasicGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.DestinationIndexIsNegative(
                TestableBasicGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsNotPositive(
                TestableBasicGaussianDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsTooBig(
                TestableBasicGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            ProbabilityDistributionTest.StandardDeviation.Succeed(
                TestableGaussianDistribution00.Get());

            ProbabilityDistributionTest.StandardDeviation.Succeed(
                TestableBasicGaussianDistribution00.Get());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            ProbabilityDistributionTest.Variance.Succeed(
                TestableGaussianDistribution00.Get());
        }
    }
}
