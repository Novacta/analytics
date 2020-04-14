// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.GDA;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using System.IO;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class MultipleCorrespondenceTests
    {
        [TestMethod()]
        public void AnalyzeTest()
        {
            // dataSet is null
            {
                string parameterName = "dataSet";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        MultipleCorrespondence.Analyze(
                            null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // dataSet must have positive marginal column sums
            {
                var STR_EXCEPT_GDA_MCA_NON_POSITIVE_MARGINAL_SUMS =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_MCA_NON_POSITIVE_MARGINAL_SUMS");

                string parameterName = "dataSet";

                CategoricalVariable color = new CategoricalVariable("COLOR")
                {
                    { 0, "Red" },
                    { 1, "Green" },
                    { 2, "Black" }
                };
                color.SetAsReadOnly();

                CategoricalVariable number = new CategoricalVariable("NUMBER")
                {
                    { 0, "Negative" },
                    { 1, "Zero" },
                    { 2, "Positive" }
                };
                number.SetAsReadOnly();

                List<CategoricalVariable> variables =
                    new List<CategoricalVariable>() { color, number };

                DoubleMatrix data = DoubleMatrix.Dense(5, 2);
                data[0, 0] = 0;
                data[1, 0] = 1;
                data[2, 0] = 0;
                data[3, 0] = 2;
                data[4, 0] = 2;

                data[0, 1] = 0;
                data[1, 1] = 1;
                data[2, 1] = 0;
                data[3, 1] = 0;
                data[4, 1] = 1;

                var dataSet = CategoricalDataSet.FromEncodedData(
                    variables,
                    data);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        MultipleCorrespondence.Analyze(
                            dataSet);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_GDA_MCA_NON_POSITIVE_MARGINAL_SUMS,
                    expectedParameterName: parameterName);
            }

            // No positive principal variances
            {
                var STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES");

                // Create a data stream.
                string[] data = new string[6] {
                        "COLOR,NUMBER",
                        "Red,Negative",
                        "Red,Negative",
                        "Red,Negative",
                        "Red,Negative",
                        "Red,Negative" };

                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Encode the categorical data set
                StreamReader streamReader = new StreamReader(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                bool firstLineContainsColumnHeaders = true;
                var dataSet = CategoricalDataSet.Encode(
                    streamReader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders);

                ExceptionAssert.Throw(
                    () =>
                    {
                        MultipleCorrespondence.Analyze(
                            dataSet);
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage:
                        STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES);
            }

            // Valid input 
            {
                {
                    // Create a data stream.
                    string[] data = new string[6] {
                        "COLOR,NUMBER",
                        "Red,Negative",
                        "Green,Zero",
                        "Red,Negative",
                        "Blue,Negative",
                        "Blue,Positive" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Encode the categorical data set
                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                    bool firstLineContainsColumnHeaders = true;
                    var dataSet = CategoricalDataSet.Encode(
                        streamReader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders);

                    var multipleCorrespondence =
                        MultipleCorrespondence.Analyze(
                            dataSet);

                    var rows = multipleCorrespondence.Individuals.ActiveCloud;

                    Assert.IsNotNull(rows);

                    var columns = multipleCorrespondence.Categories.ActiveCloud;

                    Assert.IsNotNull(columns);
                }
            }
        }

        [TestMethod()]
        public void ActiveCloudTest()
        {
            PrincipalProjectionsTest.ActiveCloud.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.ActiveCloud.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void ContributionsTest()
        {
            PrincipalProjectionsTest.Contributions.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.Contributions.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void CoordinatesTest()
        {
            PrincipalProjectionsTest.Coordinates.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.Coordinates.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void CorrelationsTest()
        {
            PrincipalProjectionsTest.Correlations.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.Correlations.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void DirectionsTest()
        {
            PrincipalProjectionsTest.Directions.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.Directions.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void RegressionCoefficientsTest()
        {
            PrincipalProjectionsTest.RegressionCoefficients.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.RegressionCoefficients.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void RepresentationQualitiesTest()
        {
            PrincipalProjectionsTest.RepresentationQualities.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.RepresentationQualities.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }

        [TestMethod()]
        public void VariancesTest()
        {
            PrincipalProjectionsTest.Variances.Succeed(
                TestableMultipleCorrespondence00.Get().Individuals);

            PrincipalProjectionsTest.Variances.Succeed(
                TestableMultipleCorrespondence00.Get().Categories);
        }
    }
}
