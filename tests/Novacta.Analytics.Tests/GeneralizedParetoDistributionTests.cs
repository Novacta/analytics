// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using Novacta.Analytics.Tests.TestableItems.Distribution;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using static Novacta.Analytics.Tests.Tools.ProbabilityDistributionTest;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class GeneralizedParetoDistributionTests
    {
        #region Helpers

        /// <summary>
        /// Gets the list of available 
        /// <see cref="TestableProbabilityDistribution"/> instances.
        /// </summary>
        /// <returns>The list of available <see cref="TestableDoubleMatrix"/> instances.</returns>
        static List<TestableProbabilityDistribution> GetTestableParetoDistributions()
        {
            var TestableItems = new List<TestableProbabilityDistribution>
            {
                TestableGeneralizedParetoDistribution00.Get(),
                TestableGeneralizedParetoDistribution01.Get(),
                TestableGeneralizedParetoDistribution02.Get()
            };

            return TestableItems;
        }

        #endregion

        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var mu = -1.1;
                var sigma = 2.2;
                var xi = -3.3;
                var distribution = new GeneralizedParetoDistribution(
                    mu: mu,
                    sigma: sigma,
                    xi: xi);

                Assert.AreEqual(
                    expected: mu,
                    actual: distribution.Mu,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: sigma,
                    actual: distribution.Sigma,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: xi,
                    actual: distribution.Xi,
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
                        new GeneralizedParetoDistribution(
                            mu: 1.0, sigma: 0.0, xi: -1.0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "sigma");
            }
        }

        [TestMethod()]
        public void MuTest()
        {
            var distribution = new GeneralizedParetoDistribution(
                mu: 1.0, sigma: 1.0, xi: -1.0);

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
                var distribution = new GeneralizedParetoDistribution(
                    mu: 1.0, sigma: 1.0, xi: -1.0);

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

                var distribution = new GeneralizedParetoDistribution(
                    mu: 1.0, sigma: 1.0, xi: -1.0);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        distribution.Sigma = 0.0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: "value");
            }
        }

        [TestMethod()]
        public void XiTest()
        {
            var distribution = new GeneralizedParetoDistribution(
                mu: 1.0, sigma: 1.0, xi: -1.0);

            var expected = -1.123;
            distribution.Xi = expected;
            Assert.AreEqual(
                expected: expected,
                actual: distribution.Xi,
                delta: DoubleMatrixTest.Accuracy);
        }

        #endregion

        [TestMethod()]
        public void CanInvertCdfTest()
        {
            TestAction(CanInvertCdf.Succeed, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void CdfTest()
        {
            TestAction(Cdf.Succeed, GetTestableParetoDistributions());

            TestAction(Cdf.Fail.ArgumentsIsNull, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void InverseCdfTest()
        {
            TestAction(InverseCdf.Succeed, GetTestableParetoDistributions());

            TestAction(InverseCdf.Fail.ArgumentsIsNull, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void MeanTest()
        {
            TestAction(Mean.Succeed, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void PdfTest()
        {
            TestAction(Pdf.Succeed, GetTestableParetoDistributions());

            TestAction(Pdf.Fail.ArgumentsIsNull, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void SampleTest()
        {
            TestSampleAction(Sample.Succeed,
                testableItems: GetTestableParetoDistributions(),
                sampleSize: 1000,
                delta: 0.01);

            TestAction(Sample.Fail.DestinationArrayIsNull, GetTestableParetoDistributions());

            TestAction(Sample.Fail.DestinationIndexIsNegative, GetTestableParetoDistributions());

            TestAction(Sample.Fail.SampleSizeIsNotPositive, GetTestableParetoDistributions());

            TestAction(Sample.Fail.SampleSizeIsTooBig, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            TestAction(StandardDeviation.Succeed, GetTestableParetoDistributions());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            TestAction(Variance.Succeed, GetTestableParetoDistributions());
        }
    }
}
