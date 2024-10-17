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
    public class FiniteDiscreteDistributionTests
    {
        #region Specific tests

        [TestMethod()]
        public void ConstructorTest()
        {
            // Valid input
            {
                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.1, .2, .3, .2, .1, .1]);

                var distribution = new FiniteDiscreteDistribution(
                    values: values, masses: masses);

                DoubleMatrixAssert.AreEqual(
                    expected: values,
                    actual: (DoubleMatrix)distribution.Values,
                    delta: DoubleMatrixTest.Accuracy);
                DoubleMatrixAssert.AreEqual(
                    expected: masses,
                    actual: (DoubleMatrix)distribution.Masses,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // values is null
            {
                var masses = DoubleMatrix.Dense(3, 2,
                    [.1, .2, .3, .2, .1, .1]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: null, masses: masses);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "values");
            }

            // values entries are not distinct
            {
                string STR_EXCEPT_PAR_ENTRIES_MUST_BE_DISTINCT =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_DISTINCT");

                var values = DoubleMatrix.Dense(3, 2);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.1, .2, .3, .2, .1, .1]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: values, masses: masses);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_MUST_BE_DISTINCT,
                    expectedParameterName: "values");
            }

            // masses is null
            {
                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: values, masses: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "masses");
            }

            // The number of rows in masses is not equal to that of values
            {
                string STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        "values");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var masses = DoubleMatrix.Dense(1, 2,
                    [.5, .5]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: values, masses: masses);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: "masses");
            }

            // The number of columns in masses is not equal to that of values
            {
                string STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "values");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var masses = DoubleMatrix.Dense(3, 1,
                    [.2, .5, .3]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: values, masses: masses);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS,
                    expectedParameterName: "masses");
            }

            // At least an entry in masses is negative
            {
                string STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.2, .5, .3, -.1, .1, 0]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: values, masses: masses);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "masses");
            }

            // The sum of the masses is not 1
            {
                string STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1 =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.2, .5, .3, .1, .1, 0]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distribution = new FiniteDiscreteDistribution(
                            values: values, masses: masses);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: "masses");
            }
        }

        [TestMethod()]
        public void UniformTest()
        {
            var values = DoubleMatrix.Dense(3, 2,
                [1, 2, 3, 4, 5, 6]);

            var distribution = FiniteDiscreteDistribution.Uniform(values);

            DoubleMatrixAssert.AreEqual(
                expected: values,
                actual: (DoubleMatrix)distribution.Values,
                delta: DoubleMatrixTest.Accuracy);
            DoubleMatrixAssert.AreEqual(
                expected: DoubleMatrix.Dense(3, 2, 1.0 / 6.0),
                actual: (DoubleMatrix)distribution.Masses,
                delta: DoubleMatrixTest.Accuracy);
        }

        [TestMethod()]
        public void SetMassesTest()
        {
            // Valid input
            {
                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var distribution = FiniteDiscreteDistribution.Uniform(
                    values: values);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.1, .2, .3, .2, .1, .1]);

                distribution.SetMasses(masses: masses);

                DoubleMatrixAssert.AreEqual(
                    expected: masses,
                    actual: (DoubleMatrix)distribution.Masses,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // masses is null
            {
                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var distribution = FiniteDiscreteDistribution.Uniform(
                    values: values);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        distribution.SetMasses(masses: null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "masses");
            }

            // The number of rows in masses is not equal to that of values
            {
                string STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        "values");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var distribution = FiniteDiscreteDistribution.Uniform(
                    values: values);

                var masses = DoubleMatrix.Dense(1, 2,
                    [.5, .5]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        distribution.SetMasses(masses: masses);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS,
                    expectedParameterName: "masses");
            }

            // The number of columns in masses is not equal to that of values
            {
                string STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "values");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var distribution = FiniteDiscreteDistribution.Uniform(
                    values: values);

                var masses = DoubleMatrix.Dense(3, 1,
                    [.2, .5, .3]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        distribution.SetMasses(masses: masses);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS,
                    expectedParameterName: "masses");
            }

            // At least an entry in masses is negative
            {
                string STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL"),
                            "0", "1");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var distribution = FiniteDiscreteDistribution.Uniform(
                    values: values);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.2, .5, .3, -.1, .1, 0]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        distribution.SetMasses(masses: masses);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL,
                    expectedParameterName: "masses");
            }

            // The sum of the masses is not 1
            {
                string STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1 =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1");

                var values = DoubleMatrix.Dense(3, 2,
                    [1, 2, 3, 4, 5, 6]);

                var distribution = FiniteDiscreteDistribution.Uniform(
                    values: values);

                var masses = DoubleMatrix.Dense(3, 2,
                    [.2, .5, .3, .1, .1, 0]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        distribution.SetMasses(masses: masses);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1,
                    expectedParameterName: "masses");
            }
        }

        #endregion

        [TestMethod()]
        public void CanInvertCdfTest()
        {
            ProbabilityDistributionTest.CanInvertCdf.Succeed(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void CdfTest()
        {
            ProbabilityDistributionTest.Cdf.Succeed(
                TestableFiniteDiscreteDistribution00.Get());

            ProbabilityDistributionTest.Cdf.Fail.ArgumentsIsNull(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void InverseCdfTest()
        {
            ProbabilityDistributionTest.InverseCdf.Fail.CdfCannotBeInverted(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void MeanTest()
        {
            ProbabilityDistributionTest.Mean.Succeed(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void PdfTest()
        {
            ProbabilityDistributionTest.Pdf.Succeed(
                TestableFiniteDiscreteDistribution00.Get());

            ProbabilityDistributionTest.Pdf.Fail.ArgumentsIsNull(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void SampleTest()
        {
            ProbabilityDistributionTest.Sample.Succeed(
                testableDistribution: TestableFiniteDiscreteDistribution00.Get(),
                sampleSize: 1000,
                delta: 0.01);

            ProbabilityDistributionTest.Sample.Fail.DestinationArrayIsNull(
                TestableFiniteDiscreteDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.DestinationIndexIsNegative(
                TestableFiniteDiscreteDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsNotPositive(
                TestableFiniteDiscreteDistribution00.Get());

            ProbabilityDistributionTest.Sample.Fail.SampleSizeIsTooBig(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            ProbabilityDistributionTest.StandardDeviation.Succeed(
                TestableFiniteDiscreteDistribution00.Get());
        }

        [TestMethod()]
        public void VarianceTest()
        {
            ProbabilityDistributionTest.Variance.Succeed(
                TestableFiniteDiscreteDistribution00.Get());
        }
    }
}
