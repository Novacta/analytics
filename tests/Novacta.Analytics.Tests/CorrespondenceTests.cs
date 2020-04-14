// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.GDA;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class CorrespondenceTests
    {
        [TestMethod()]
        public void AnalyzeTest()
        {
            // data is null
            {
                string parameterName = "data";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Correspondence.Analyze(
                            (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // data must have positive marginal row sums
            {
                var STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS");

                string parameterName = "data";

                var data = DoubleMatrix.Dense(
                    new double[6, 3]
                    {
                    {   7836,   13112,   6026 },
                    {  53655,  102383,  42413 },
                    { 115615,  184541,  59226 },
                    { 161926,  340479,  62754 },
                    {      0,       0,      0 },
                    {  46371,   58367,  14229 }
                    });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Correspondence.Analyze(
                            data);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS,
                    expectedParameterName: parameterName);
            }

            // data must have positive marginal column sums
            {
                var STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS");

                string parameterName = "data";

                var data = DoubleMatrix.Dense(
                    new double[6, 3]
                    {
                    {   7836,   0,   6026 },
                    {  53655,   0,  42413 },
                    { 115615,   0,  59226 },
                    { 161926,   0,  62754 },
                    {  38117,   0,  12670 },
                    {  46371,   0,  14229 }
                    });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Correspondence.Analyze(
                            data);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS,
                    expectedParameterName: parameterName);
            }

            // SVD cannot be executed or does not converge
            {
                var STR_EXCEPT_SVD_ERRORS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_SVD_ERRORS");

                DoubleMatrix data = DoubleMatrix.Dense(2, 2,
                    new double[4] { Double.NaN, 1, 1, 1 });

                ExceptionAssert.Throw(
                    () =>
                    {
                        Correspondence.Analyze(
                            data);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_SVD_ERRORS);
            }

            // No positive principal variances
            {
                var STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES");

                DoubleMatrix data = DoubleMatrix.Dense(4, 2, 1.0);

                ExceptionAssert.Throw(
                    () =>
                    {
                        Correspondence.Analyze(
                            data);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES);

            }

            // Valid input 
            {
                {
                    var data = DoubleMatrix.Dense(
                        new double[6, 3]
                        {
                    {   7836,   13112,   6026 },
                    {  53655,  102383,  42413 },
                    { 115615,  184541,  59226 },
                    { 161926,  340479,  62754 },
                    {  38117,  105101,  12670 },
                    {  46371,   58367,  14229 }
                        });

                    var rowNames = new string[6]
                        {
                            "Rousseau",
                            "Chateaubriand",
                            "Hugo",
                            "Zola",
                            "Proust",
                            "Giraudoux"
                        };

                    for (int i = 0; i < data.NumberOfRows; i++)
                    {
                        data.SetRowName(i, rowNames[i]);
                    }

                    var columnNames = new string[3]
                        {
                            "Period",
                            "Comma",
                            "OtherMarks"
                        };

                    for (int j = 0; j < data.NumberOfColumns; j++)
                    {
                        data.SetColumnName(j, columnNames[j]);
                    }

                    var correspondence =
                        Correspondence.Analyze(
                            data);

                    var rows = correspondence.RowProfiles.ActiveCloud;

                    Assert.IsNotNull(rows);

                    var columns = correspondence.ColumnProfiles.ActiveCloud;

                    Assert.IsNotNull(columns);
                }
            }
        }

        [TestMethod()]
        public void ActiveCloudTest()
        {
            PrincipalProjectionsTest.ActiveCloud.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.ActiveCloud.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void ContributionsTest()
        {
            PrincipalProjectionsTest.Contributions.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.Contributions.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void CoordinatesTest()
        {
            PrincipalProjectionsTest.Coordinates.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.Coordinates.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void CorrelationsTest()
        {
            PrincipalProjectionsTest.Correlations.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.Correlations.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void DirectionsTest()
        {
            PrincipalProjectionsTest.Directions.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.Directions.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void RegressionCoefficientsTest()
        {
            PrincipalProjectionsTest.RegressionCoefficients.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.RegressionCoefficients.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void RepresentationQualitiesTest()
        {
            PrincipalProjectionsTest.RepresentationQualities.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.RepresentationQualities.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }

        [TestMethod()]
        public void VariancesTest()
        {
            PrincipalProjectionsTest.Variances.Succeed(
                TestableCorrespondence00.Get().RowProfiles);

            PrincipalProjectionsTest.Variances.Succeed(
                TestableCorrespondence00.Get().ColumnProfiles);
        }
    }
}
