// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class ClustersTests
    {
        [TestMethod]
        public void DiscoverTest()
        {
            // data is null
            {
                string parameterName = "data";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Discover(
                            data: null,
                            maximumNumberOfParts: 2);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfParts is one
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                string parameterName = "maximumNumberOfParts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Discover(
                            data: DoubleMatrix.Dense(10, 5),
                            maximumNumberOfParts: 1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfParts is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                string parameterName = "maximumNumberOfParts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Discover(
                            data: DoubleMatrix.Dense(10, 5),
                            maximumNumberOfParts: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfParts is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1");

                string parameterName = "maximumNumberOfParts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Discover(
                            data: DoubleMatrix.Dense(10, 5),
                            maximumNumberOfParts: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfParts is equal to the number of rows in data
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS"),
                        "maximumNumberOfParts",
                        "data");

                string parameterName = "maximumNumberOfParts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Discover(
                            data: DoubleMatrix.Dense(10, 5),
                            maximumNumberOfParts: 10);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfParts is greater than the number of rows in data
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS"),
                        "maximumNumberOfParts",
                        "data");

                string parameterName = "maximumNumberOfParts";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Discover(
                            data: DoubleMatrix.Dense(10, 5),
                            maximumNumberOfParts: 11);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                const int numberOfItems = 12;
                const int numberOfFeatures = 7;

                var source = DoubleMatrix.Dense(numberOfItems, 1,
                    new double[numberOfItems]
                        { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2  });

                var expectedPartition = IndexPartition.Create(source);
                var data = DoubleMatrix.Dense(numberOfItems, numberOfFeatures);

                double mu = 1.0;

                var partIdentifiers = expectedPartition.Identifiers;

                for (int i = 0; i < partIdentifiers.Count; i++)
                {
                    var part = expectedPartition[partIdentifiers[i]];
                    int partSize = part.Count;
                    for (int j = 0; j < partSize; j++)
                    {
                        data[part[j], ":"] += mu;
                    }
                    mu += 5.0;
                }

                var actualPartition = Clusters.Discover(
                    data: data,
                    maximumNumberOfParts: 3);

                IndexPartitionAssert.HaveEqualIdentifiers(
                    expected: expectedPartition,
                    actual: actualPartition);

                IndexPartitionAssert.HaveEqualParts(
                    expected: expectedPartition,
                    actual: actualPartition);
            }
        }

        [TestMethod]
        public void ExplainTest()
        {
            // data is null
            {
                string parameterName = "data";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Explain(
                            data: null,
                            partition: IndexPartition.Create(
                                DoubleMatrix.Dense(10, 1)),
                            numberOfExplanatoryFeatures: 2); ;
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // data is null
            {
                string parameterName = "partition";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Explain(
                            data: DoubleMatrix.Dense(10, 5),
                            partition: null,
                            numberOfExplanatoryFeatures: 2); ;
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // numberOfExplanatoryFeatures is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "numberOfExplanatoryFeatures";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Explain(
                            data: DoubleMatrix.Dense(10, 5),
                            partition: IndexPartition.Create(
                                DoubleMatrix.Dense(10, 1)),
                            numberOfExplanatoryFeatures: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // numberOfExplanatoryFeatures is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "numberOfExplanatoryFeatures";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Explain(
                            data: DoubleMatrix.Dense(10, 5),
                            partition: IndexPartition.Create(
                                DoubleMatrix.Dense(10, 1)),
                            numberOfExplanatoryFeatures: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // numberOfExplanatoryFeatures is equal to the number of columns in data
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS"),
                        "numberOfExplanatoryFeatures",
                        "data");

                string parameterName = "numberOfExplanatoryFeatures";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Explain(
                            data: DoubleMatrix.Dense(10, 5),
                            partition: IndexPartition.Create(
                                DoubleMatrix.Dense(10, 1)),
                            numberOfExplanatoryFeatures: 5);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS,
                    expectedParameterName: parameterName);
            }

            // numberOfExplanatoryFeatures is greater than the number of columns in data
            {
                var STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS"),
                        "numberOfExplanatoryFeatures",
                        "data");

                string parameterName = "numberOfExplanatoryFeatures";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Clusters.Explain(
                            data: DoubleMatrix.Dense(10, 5),
                            partition: IndexPartition.Create(
                                DoubleMatrix.Dense(10, 1)),
                            numberOfExplanatoryFeatures: 6);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS,
                    expectedParameterName: parameterName);
            }

            // Valid input
            {
                const int numberOfItems = 12;

                var source = DoubleMatrix.Dense(numberOfItems, 1,
                    new double[numberOfItems]
                        { 0 ,0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 });

                var partition = IndexPartition.Create(source);
                var data = DoubleMatrix.Dense(numberOfItems, 7);

                // features 0 to 4
                var g = new GaussianDistribution(mu: 0, sigma: .1);
                for (int j = 0; j < 5; j++)
                {
                    data[":", j] = g.Sample(sampleSize: numberOfItems);
                }

                var partIdentifiers = partition.Identifiers;

                // feature 5 to 6
                double mu = 1.0;
                for (int i = 0; i < partIdentifiers.Count; i++)
                {
                    var part = partition[partIdentifiers[i]];
                    int partSize = part.Count;
                    g.Mu = mu;
                    data[part, 5] = g.Sample(sampleSize: partSize);
                    mu += 2.0;
                    g.Mu = mu;
                    data[part, 6] = g.Sample(sampleSize: partSize);
                    mu += 2.0;
                }

                IndexCollection actualFeatureIndexes = 
                    Clusters.Explain(
                        data: data,
                        partition: partition,
                        numberOfExplanatoryFeatures: 2);

                IndexCollectionAssert.AreEqual(
                    expected: IndexCollection.Range(5, 6),
                    actual: actualFeatureIndexes);
            }
        }
    }    
}