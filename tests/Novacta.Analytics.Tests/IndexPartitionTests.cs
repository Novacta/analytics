// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using System.Collections.Generic;
using System.Linq;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Data.UCI;

namespace Novacta.Analytics.Tests
{
    [TestClass]
    public class IndexPartitionTests
    {
        [TestMethod]
        public void CreateFromDoubleMatrixTest()
        {
            // elements is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = IndexPartition.Create((DoubleMatrix)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "elements");
            }

            // elements is a vector
            {
                // Create a matrix
                var data = new double[18] {
                    0,0,1,
                    0,0,1,
                    0,1,0,
                    0,1,0,
                    1,0,0,
                    1,0,0 };
                var matrix = DoubleMatrix.Dense(6, 3, data, StorageOrder.RowMajor);

                // Partition the matrix row indexes by the contents of column 0:
                // a part is created for each distinct value in column 0
                var elements = matrix[":", 0];
                var actual = IndexPartition.Create(elements);

                // Each part is identified by its corresponding value and contains
                // the indexes of the rows in which the identifier
                // is positioned in column 0

                // Expected: 
                // 
                // Part identifier: 0
                //      indexes: 0, 1, 2, 3
                // 
                // Part identifier: 1
                //      indexes: 4, 5

                IndexPartition<double> expected = new()
                {
                    partIdentifiers =
                    [
                        0.0,
                        1.0
                    ],

                    parts = new Dictionary<double, IndexCollection>(2)
                    {
                        { 0.0, IndexCollection.Default(3) },
                        { 1.0, IndexCollection.Range(4, 5) }
                    }
                };

                IndexPartitionAssert.AreEqual(expected, actual);
            }

            // elements is a matrix of signs
            {
                // Create a matrix.
                var data = new double[8] {
                    0, 1,-2,-3,
                    0,-1, 2, 3 };
                var matrix = DoubleMatrix.Dense(2, 4, data, StorageOrder.RowMajor);

                // Check the sign of its entries
                var signs = DoubleMatrix.Dense(matrix.NumberOfRows, matrix.NumberOfColumns);
                for (int i = 0; i < matrix.Count; i++)
                {
                    signs[i] = Math.Sign(matrix[i]);
                }

                // Partition the matrix linear indexes by the sign of each entry
                var actual = IndexPartition.Create(signs);

                // The partition contains three parts, the zero part, identified by 0,
                // the negative part (identified by -1), and the positive one 
                // (identified by 1).

                // Expected:
                // 
                // Part identifier: -1
                //      indexes: 3, 4, 6
                // 
                // Part identifier: 0
                //      indexes: 0, 1
                // 
                // Part identifier: 1
                //      indexes: 2, 5, 7
                // 

                IndexPartition<double> expected = new()
                {
                    partIdentifiers =
                    [
                        -1.0,
                        0.0,
                        1.0
                    ],

                    parts = new Dictionary<double, IndexCollection>(3)
                    {
                        { -1.0, IndexCollection.FromArray([3, 4, 6]) },
                        { 0.0, IndexCollection.Default(1) },
                        { 1.0, IndexCollection.FromArray([2, 5, 7]) }
                    }
                };

                IndexPartitionAssert.AreEqual(expected, actual);
            }

            // elements is a matrix of data
            {
                // Create a matrix
                var data = new double[6] {
                    1,3,
                    0,2,
                    2,1 };
                var elements = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);

                // Partition the matrix linear indexes by the content of 
                // matrix entries: a part is created for each distinct matrix value
                var actual = IndexPartition.Create(elements);

                // Each part is identified by its corresponding value and contains
                // the linear indexes of the entries in which the identifier
                // is positioned.

                // This code example produces the following output:
                // 
                // 
                // Part identifier: 0
                //      indexes: 1
                // 
                // Part identifier: 1
                //      indexes: 0, 5
                // 
                // Part identifier: 2
                //      indexes: 2, 4
                // 
                // Part identifier: 3
                //      indexes: 3
                // 

                var expected = new IndexPartition<double>
                {
                    partIdentifiers = new List<double>(3)
                    {
                        0.0,
                        1.0,
                        2.0,
                        3.0
                    },

                    parts = new Dictionary<double, IndexCollection>(3)
                    {
                        { 0.0, IndexCollection.FromArray([1]) },
                        { 1.0, IndexCollection.FromArray([0, 5]) },
                        { 2.0, IndexCollection.FromArray([2, 4]) },
                        { 3.0, IndexCollection.FromArray([3]) }
                    }
                };

                IndexPartitionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CreateFromEnumerableTest()
        {
            // elements is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = IndexPartition.Create((string[])null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "elements");
            }

            // elements is not null
            {
                // Create an array of strings.
                var elements = new string[6] {
                    "one",
                    "two",
                    "one",
                    "one",
                    "three",
                    "three" };

                // Partition the array positions by their contents.
                var actual = IndexPartition.Create(elements);

                // The partition contains three parts, identified, respectively,
                // by the strings "one", "two", and "three".

                // Expected:
                // 
                // Part identifier: one
                //      indexes: 0, 2, 3
                // 
                // Part identifier: three
                //      indexes: 4, 5
                // 
                // Part identifier: two
                //      indexes: 1

                var expected = new IndexPartition<string>
                {
                    partIdentifiers =
                    [
                        "one",
                        "three",
                        "two"
                    ],

                    parts = new Dictionary<string, IndexCollection>(3)
                    {
                        { "one", IndexCollection.FromArray([0, 2, 3]) },
                        { "three", IndexCollection.Range(4, 5) },
                        { "two", IndexCollection.FromArray([1]) }
                    }
                };

                IndexPartitionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CreateFromIndexCollectionTest()
        {
            // elements is null
            {
                bool partitioner(int linearIndex)
                {
                    return linearIndex < 3;
                }

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = IndexPartition.Create(
                            (IndexCollection)null,
                            partitioner);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "elements");
            }

            // partitioner is null
            {
                Func<int, bool> partitioner = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = IndexPartition.Create(
                            IndexCollection.Default(3),
                            partitioner);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "partitioner");
            }

            // Valid parameters
            {
                // Create a matrix.
                var data = new double[16] {
                    -3,  3,  3, -1,
                    0,  2, -2,  2,
                    2,  1, -4, -5,
                    -8,  2,  7, -1 };
                var matrix = DoubleMatrix.Dense(4, 4, data, StorageOrder.RowMajor);

                // Create the collection of linear indexes corresponding
                // to entries on the matrix main diagonal.
                var elements =
                    IndexCollection.Sequence(0, 1 + matrix.NumberOfRows, matrix.Count);

                // Create a partitioner which returns true if
                // the absolute value in a entry having the specified linear
                // index is less than 3, otherwise false.
                bool partitioner(int linearIndex)
                {
                    return Math.Abs(matrix[linearIndex]) < 3.0;
                }

                // Partition the diagonal linear indexes through the
                // specified partitioner.
                var actual = IndexPartition.Create(elements, partitioner);

                // Two parts are created, one for diagonal
                // entries less than 3 in absolute value, the other for 
                // entries not satisfying that condition.

                // Expected:
                // 
                // Part identifier: False
                //      indexes: 0, 10
                // 
                // Part identifier: True
                //      indexes: 5, 15
                // 
                var expected = new IndexPartition<bool>
                {
                    partIdentifiers =
                    [
                        false,
                        true
                    ],

                    parts = new Dictionary<bool, IndexCollection>(2)
                    {
                        { false, IndexCollection.FromArray([0, 10]) },
                        { true, IndexCollection.FromArray([5, 15]) }
                    }
                };

                IndexPartitionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CreateFromRowCollectionTest()
        {
            // elements is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var partition = IndexPartition.Create((DoubleMatrixRowCollection)null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "elements");
            }

            // elements is not null
            {
                // Create a matrix.
                var data = new double[18] {
                    0,0,1,
                    0,0,1,
                    0,1,0,
                    0,1,0,
                    1,0,0,
                    1,0,0
                };
                var matrix = DoubleMatrix.Dense(6, 3, data, StorageOrder.RowMajor);

                // Partition the matrix row indexes by the contents of each row:
                // a part is created for each distinct row.
                var elements = matrix.AsRowCollection();
                var actual = IndexPartition.Create(elements);

                // Each part is identified by its corresponding row and contains
                // the indexes of the rows which are equal to the identifier.

                // Expected:
                // 
                // Part identifier: 0                0                1                
                // 
                //      indexes: 0, 1
                // 
                // Part identifier: 0                1                0                
                // 
                //      indexes: 2, 3
                // 
                // Part identifier: 1                0                0                
                // 
                //      indexes: 4, 5
                // 

                var expected = new IndexPartition<DoubleMatrixRow>
                {
                    partIdentifiers =
                    [
                        elements[0],
                        elements[2],
                        elements[4]
                    ],

                    parts = new Dictionary<DoubleMatrixRow, IndexCollection>(3)
                    {
                        { elements[0], IndexCollection.Default(1) },
                        { elements[2], IndexCollection.Range(2, 3) },
                        { elements[4], IndexCollection.Range(4, 5) }
                    }
                };

                IndexPartitionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IndexerGetTest()
        {
            // Create an array of strings.
            var elements = new string[6] {
                    "one",
                    "two",
                    "one",
                    "one",
                    "three",
                    "three" };

            // Partition the array positions by their contents.
            var target = IndexPartition.Create(elements);

            // The partition contains three parts, identified, respectively,
            // by the strings "one", "two", and "three".

            // Expected:
            // 
            // Part identifier: one
            //      indexes: 0, 2, 3
            // 
            // Part identifier: three
            //      indexes: 4, 5
            // 
            // Part identifier: two
            //      indexes: 1

            // partIdentifier is null   
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var part = target[(string)null];
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "partIdentifier");
            }

            // partIdentifier is not a key   
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var part = target["four"];
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_IS_NOT_A_PART_IDENTIFIER"),
                    expectedParameterName: "partIdentifier");
            }

            // Valid partIdentifier
            {
                var actual = target["one"];

                var expected = IndexCollection.FromArray([0, 2, 3]);

                IndexCollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IndexPartitionMinimumCentroidTest()
        {
            // data is null
            {
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.MinimumCentroidLinkage(null, partition);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // partition is null
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.MinimumCentroidLinkage(data, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "partition");
            }

            // The first part contains an invalid index
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                partition[1][0] = 100000;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.MinimumCentroidLinkage(data, partition);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                    expectedParameterName: "partition");
            }

            // The second part contains an invalid index
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                partition[2][0] = 100000;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.MinimumCentroidLinkage(data, partition);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                    expectedParameterName: "partition");
            }

            // Valid input
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                var actual = IndexPartition.MinimumCentroidLinkage(
                    data, partition);

                List<double> linkages = [];
                foreach (var leftId in partition.Identifiers)
                {
                    var leftPart = partition[leftId];
                    var left = data[leftPart, ":"];
                    foreach (var rightId in partition.Identifiers)
                    {
                        if (rightId != leftId)
                        {
                            var rightPart = partition[rightId];
                            var right = data[rightPart, ":"];
                            linkages.Add(Distance.CentroidLinkage(left, right));
                        }
                    }
                }

                double expected = linkages.Min();

                Assert.AreEqual(expected, actual, 1e-4);
            }
        }

        [TestMethod]
        public void IndexPartitionDunnTest()
        {
            // data is null
            {
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DunnIndex(null, partition);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // partition is null
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DunnIndex(data, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "partition");
            }

            // The first part contains an invalid index
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                partition[1][0] = 100000;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DunnIndex(data, partition);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                    expectedParameterName: "partition");
            }

            // The second part contains an invalid index
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                partition[2][0] = 100000;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DunnIndex(data, partition);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                    expectedParameterName: "partition");
            }

            // Valid input
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                var actual = IndexPartition.DunnIndex(
                    data, partition);

                // The expected below was obtained in R as follows:
                //
                // library(clv)
                // data(iris)
                // iris.data < -iris[, 1:4]
                // # cluster data
                // agnes.mod < -agnes(iris.data) # create cluster tree
                // v.pred < - as.integer(cutree(agnes.mod, 5)) # "cut" the tree
                // intraclust = c("complete", "average", "centroid")
                // interclust = c("single", "complete", "average", "centroid", "aveToCent", "hausdorff")
                // cls.scatt < -cls.scatt.data(iris.data, v.pred, dist = "euclidean")
                // dunn1 < -clv.Dunn(cls.scatt, intraclust, interclust)

                // This is dunn1[1,1], corresponding to 
                // intra = "complete",
                // inter = "single".
                double expected = 0.154041597;

                Assert.AreEqual(expected, actual, 1e-4);

                Assert.AreEqual(expected, actual, 1e-4);
            }
        }

        [TestMethod]
        public void IndexPartitionDaviesBouldinTest()
        {
            // data is null
            {
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DaviesBouldinIndex(null, partition);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "data");
            }

            // partition is null
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DaviesBouldinIndex(data, null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "partition");
            }

            // The first part contains an invalid index
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                partition[1][0] = 100000;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DaviesBouldinIndex(data, partition);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                    expectedParameterName: "partition");
            }

            // The second part contains an invalid index
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                partition[2][0] = 100000;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var actual = IndexPartition.DaviesBouldinIndex(data, partition);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                    expectedParameterName: "partition");
            }

            // Valid input
            {
                var data = IrisDataSet.GetAttributesAsDoubleMatrix();
                var target = IrisDataSet.GetClassPredictions();
                var partition = IndexPartition.Create(target);

                var actual = IndexPartition.DaviesBouldinIndex(
                    data, partition);

                // The expected below was obtained in R as follows:
                //
                // library(clv)
                // data(iris)
                // iris.data < -iris[, 1:4]
                // # cluster data
                // agnes.mod < -agnes(iris.data) # create cluster tree
                // v.pred < - as.integer(cutree(agnes.mod, 5)) # "cut" the tree
                // intraclust = c("complete", "average", "centroid")
                // interclust = c("single", "complete", "average", "centroid", "aveToCent", "hausdorff")
                // cls.scatt < -cls.scatt.data(iris.data, v.pred, dist = "euclidean")
                // davies1 <- clv.Davies.Bouldin(cls.scatt, intraclust, interclust)

                // This is davies1[4,3], corresponding to 
                // intra = "centroid",
                // inter = "centroid".
                double expected = 0.685838025870551;

                Assert.AreEqual(expected, actual, 1e-3);
            }
        }

        [TestMethod]
        public void TryGetPartTest()
        {
            // Part identifier: "false"
            //      indexes: 0, 10
            // 
            // Part identifier: "true"
            //      indexes: 5, 15
            // 
            var falsePart = IndexCollection.FromArray([0, 10]);
            var truePart = IndexCollection.FromArray([5, 15]);

            var target = new IndexPartition<string>
            {
                partIdentifiers =
                    [
                        "false",
                        "true"
                    ],

                parts = new Dictionary<string, IndexCollection>(2)
                    {
                        { "false", falsePart },
                        { "true", truePart }
                    }
            };

            bool partFound;

            partFound = target.TryGetPart("unknown", out IndexCollection part);
            Assert.AreEqual(expected: false, actual: partFound);
            Assert.IsNull(part);

            partFound = target.TryGetPart("false", out part);
            Assert.AreEqual(expected: true, actual: partFound);
            IndexCollectionAssert.AreEqual(expected: falsePart, actual: part);

            partFound = target.TryGetPart("true", out part);
            Assert.AreEqual(expected: true, actual: partFound);
            IndexCollectionAssert.AreEqual(expected: truePart, actual: part);
        }

        [TestMethod]
        public void ToDoubleMatrixTest()
        {
            // This code example produces the following output:
            // 
            // 
            // Part identifier: 0
            //      indexes: 1
            // 
            // Part identifier: 1
            //      indexes: 0, 5
            // 
            // Part identifier: 2
            //      indexes: 2, 4
            // 
            // Part identifier: 3
            //      indexes: 3
            // 

            var target = new IndexPartition<double>
            {
                partIdentifiers = new List<double>(3)
                {
                    0.0,
                    1.0,
                    2.0,
                    3.0
                },

                parts = new Dictionary<double, IndexCollection>(3)
                {
                    { 0.0, IndexCollection.FromArray([1]) },
                    { 1.0, IndexCollection.FromArray([0, 5]) },
                    { 2.0, IndexCollection.FromArray([2, 4]) },
                    { 3.0, IndexCollection.FromArray([3]) }
                }
            };

            // Convert the partition to a matrix.
            var actual = (DoubleMatrix)target;

            // Conversion of a partition to a matrix:
            // 1                
            // 0                
            // 2                
            // 3                
            // 2                
            // 1                
            // 
            var expected = DoubleMatrix.Dense(6, 1, [
                1,
                0,
                2,
                3,
                2,
                1 ]);

            DoubleMatrixAssert.AreEqual(expected, actual, 1e-2);
        }

        [TestMethod]
        public void ToStringTest()
        {
            // Create a matrix.
            var data = new double[18] {
                0,0,1,
                0,0,1,
                0,1,0,
                0,1,0,
                1,0,0,
                1,0,0
            };
            var matrix = DoubleMatrix.Dense(6, 3, data, StorageOrder.RowMajor);

            // Partition the matrix row indexes by the contents of each row:
            // a part is created for each distinct row.
            var rowCollection = matrix.AsRowCollection();
            var partition = IndexPartition.Create(rowCollection);

            // Each part is identified by its corresponding row and contains
            // the indexes of the rows which are equal to the identifier.

            // Expected:
            // 
            // Part identifier: 0                0                1                
            // 
            //      indexes: 0, 1
            // 
            // Part identifier: 0                1                0                
            // 
            //      indexes: 2, 3
            // 
            // Part identifier: 1                0                0                
            // 
            //      indexes: 4, 5
            // 

            var expected = "[(0                0                1                ),  0, 1]" +
                Environment.NewLine + "[(0                1                0                ),  2, 3]" +
                Environment.NewLine + "[(1                0                0                ),  4, 5]" +
                Environment.NewLine;

            Assert.AreEqual(expected, partition.ToString());
        }
    }
}
