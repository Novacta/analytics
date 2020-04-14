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
    public class BernoulliDistributionTests
    {
        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var successProbability = 0.18;
                var distribution = new BernoulliDistribution(
                    successProbability: successProbability);

                Assert.AreEqual(
                    expected: successProbability,
                    actual: distribution.SuccessProbability,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // successProbability < 0
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new BernoulliDistribution(successProbability: -2.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "successProbability");
            }

            // successProbability > 1
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new BernoulliDistribution(successProbability: 2.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "successProbability");
            }
        }

        [TestMethod()]
        public void SuccessProbabilityTest()
        {
            // Valid successProbability
            {
                var distribution = BernoulliDistribution.Balanced();
                var expected = 0.12;
                distribution.SuccessProbability = expected;
                Assert.AreEqual(
                    expected: expected,
                    actual: distribution.SuccessProbability,
                    delta: DoubleMatrixTest.Accuracy);
            }


            // successProbability < 0
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        BernoulliDistribution.Balanced().SuccessProbability = -2.0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "value");
            }

            // successProbability > 1
            {
                string STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        BernoulliDistribution.Balanced().SuccessProbability = 2.0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "value");
            }
        }

        [TestMethod()]
        public void BalancedTest()
        {
            var distribution = BernoulliDistribution.Balanced();

            Assert.AreEqual(
                expected: 0.5,
                actual: distribution.SuccessProbability,
                delta: DoubleMatrixTest.Accuracy);
        }

        #endregion

        [TestMethod()]
        public void CanInvertCdfTest()
        {
            ProbabilityDistributionTest.CanInvertCdf.Succeed(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void CdfTest()
        {
            ProbabilityDistributionTest.Cdf.Succeed(
                TestableBernoulliDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Fail.ArgumentsIsNull(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void InverseCdfTest()
        {
            ProbabilityDistributionTest.InverseCdf.Fail.CdfCannotBeInverted(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void MeanTest()
        {
            ProbabilityDistributionTest.Mean.Succeed(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void PdfTest()
        {
            ProbabilityDistributionTest.Pdf.Succeed(
                TestableBernoulliDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Fail.ArgumentsIsNull(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void SampleTest()
        {
            ProbabilityDistributionTest.Sample.Succeed(
                testableDistribution: TestableBernoulliDistribution00.Get(),
                sampleSize: 1000,
                delta: 0.01);

            ProbabilityDistributionTest.Sample.Fail.DestinationArrayIsNull(
                TestableBernoulliDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.DestinationIndexIsNegative(
                TestableBernoulliDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsNotPositive(
                TestableBernoulliDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsTooBig(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            ProbabilityDistributionTest.StandardDeviation.Succeed(
                TestableBernoulliDistribution00.Get());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            ProbabilityDistributionTest.Variance.Succeed(
                TestableBernoulliDistribution00.Get());
        }
    }
}
