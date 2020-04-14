// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Data.UCI;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass]
    public class DistanceTests
    {
        #region Expected values

        /*
         Expected values evaluated in R as follows.
         
         # Data
         data("iris")
         
         number_of_items <- 4
         iris_attributes <- iris[1:number_of_items,c(1:4)]
         setosa <- iris3[,,1][1:number_of_items,]
         versicolor <- iris3[,,2][1:number_of_items,]
         virginica <- iris3[,,3][1:number_of_items,]
         
         # Euclidean
         euclidean_expected <- dist(
           iris_attributes, 
           method = "euclidean", 
           diag = TRUE, 
           upper = TRUE, 
           p = 2
         )
         euclidean_expected
                   1         2         3         4
         1 0.0000000 0.5385165 0.5099020 0.6480741
         2 0.5385165 0.0000000 0.3000000 0.3316625
         3 0.5099020 0.3000000 0.0000000 0.2449490
         4 0.6480741 0.3316625 0.2449490 0.0000000
         
         # CompleteDiameter
         complete_diameter_expected <- max(euclidean_expected)
         complete_diameter_expected
         [1] 0.6480741
         
         # CEntroidLinkage
         left <- virginica
         right <- setosa
         
         left_centroid <- apply(left, 2, mean)
         right_centroid <- apply(right, 2, mean)
         
         centroid_linkage_expected <- dist(
           rbind(left_centroid, right_centroid), 
           method = "euclidean", 
           diag = TRUE, 
           upper = TRUE, 
           p = 2
         )[1]
         centroid_linkage_expected
         [1] 4.902168
         
         # Average, Single, Complete Linkages
         library(SpatialTools)
         
         left <- virginica
         right <- setosa
         
         distances <- dist2(left, right)
         
         average_linkage_expected <- mean(distances)
         average_linkage_expected
         [1] 4.932325
         
         single_linkage_expected <- min(distances)
         single_linkage_expected
         [1] 4.17732
         
         complete_linkage_expected <- max(distances)
         complete_linkage_expected
         [1] 5.529014

         */

        #endregion

        [TestMethod]
        public void AverageLinkageTest()
        {
            // left is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.AverageLinkage(
                            left: (DoubleMatrix)null,
                            right: DoubleMatrix.Identity(2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "left");
            }

            // right is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.AverageLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "right");
            }

            // right and left have not the same number of columns 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.AverageLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: DoubleMatrix.Identity(3));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "left"),
                    expectedParameterName: "right");
            }

            // input is valid
            {
                var items = IndexCollection.Range(0, 3);
                int numberOfItems = items.Count;
                var attributes = IrisDataSet.GetAttributesAsDoubleMatrix();
                var classes = IrisDataSet.GetClasses();
                var partition = IndexPartition.Create(classes);
                var left = attributes[partition["virginica"], ":"][items, ":"];
                var right = attributes[partition["setosa"], ":"][items, ":"];

                var actual = Distance.AverageLinkage(left, right);

                var expected = 4.932325;

                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void CentroidLinkageTest()
        {
            // left is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.CentroidLinkage(
                            left: (DoubleMatrix)null,
                            right: DoubleMatrix.Identity(2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "left");
            }

            // right is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.CentroidLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "right");
            }

            // right and left have not the same number of columns 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.CentroidLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: DoubleMatrix.Identity(3));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "left"),
                    expectedParameterName: "right");
            }

            // input is valid
            {
                var items = IndexCollection.Range(0, 3);
                int numberOfItems = items.Count;
                var attributes = IrisDataSet.GetAttributesAsDoubleMatrix();
                var classes = IrisDataSet.GetClasses();
                var partition = IndexPartition.Create(classes);
                var left = attributes[partition["virginica"], ":"][items, ":"];
                var right = attributes[partition["setosa"], ":"][items, ":"];

                var actual = Distance.CentroidLinkage(left, right);

                var expected = 4.902168;

                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void CompleteDiameterTest()
        {
            // cluster is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = Distance.CompleteDiameter((DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "cluster");
            }

            // cluster is valid
            {
                var items = IndexCollection.Range(0, 3);
                int numberOfItems = items.Count;
                var attributes = IrisDataSet.GetAttributesAsDoubleMatrix()[items, ":"];

                var actual = Distance.CompleteDiameter(attributes);

                var expected = 0.6480741;

                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void CompleteLinkageTest()
        {
            // left is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.CompleteLinkage(
                            left: (DoubleMatrix)null,
                            right: DoubleMatrix.Identity(2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "left");
            }

            // right is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.CompleteLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "right");
            }

            // right and left have not the same number of columns 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.CompleteLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: DoubleMatrix.Identity(3));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "left"),
                    expectedParameterName: "right");
            }

            // input is valid
            {
                var items = IndexCollection.Range(0, 3);
                int numberOfItems = items.Count;
                var attributes = IrisDataSet.GetAttributesAsDoubleMatrix();
                var classes = IrisDataSet.GetClasses();
                var partition = IndexPartition.Create(classes);
                var left = attributes[partition["virginica"], ":"][items, ":"];
                var right = attributes[partition["setosa"], ":"][items, ":"];

                var actual = Distance.CompleteLinkage(left, right);

                var expected = 5.529014;

                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void EuclideanTest()
        {
            // cluster is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = Distance.Euclidean((DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "cluster");
            }

            // cluster is valid
            {
                var items = IndexCollection.Range(0, 3);
                int numberOfItems = items.Count;
                var attributes = IrisDataSet.GetAttributesAsDoubleMatrix()[items, ":"];

                var actual = Distance.Euclidean(attributes);

                var expected = DoubleMatrix.Dense(
                    numberOfItems,
                    numberOfItems,
                    new double[] {
                        0.0000000, 0.5385165, 0.5099020, 0.6480741,
                        0.5385165, 0.0000000, 0.3000000, 0.3316625,
                        0.5099020, 0.3000000, 0.0000000, 0.2449490,
                        0.6480741, 0.3316625, 0.2449490, 0.0000000
                    },
                    StorageOrder.RowMajor);

                DoubleMatrixAssert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void SingleLinkageTest()
        {
            // left is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.SingleLinkage(
                            left: (DoubleMatrix)null,
                            right: DoubleMatrix.Identity(2));
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "left");
            }

            // right is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.SingleLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: (DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "right");
            }

            // right and left have not the same number of columns 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        Distance.SingleLinkage(
                            left: DoubleMatrix.Identity(2),
                            right: DoubleMatrix.Identity(3));
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        "left"),
                    expectedParameterName: "right");
            }

            // input is valid
            {
                var items = IndexCollection.Range(0, 3);
                int numberOfItems = items.Count;
                var attributes = IrisDataSet.GetAttributesAsDoubleMatrix();
                var classes = IrisDataSet.GetClasses();
                var partition = IndexPartition.Create(classes);
                var left = attributes[partition["virginica"], ":"][items, ":"];
                var right = attributes[partition["setosa"], ":"][items, ":"];

                var actual = Distance.SingleLinkage(left, right);

                var expected = 4.17732;

                Assert.AreEqual(expected, actual, DoubleMatrixTest.Accuracy);
            }
        }
    }
}
