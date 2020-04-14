// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="Advanced.Cloud"/> 
    /// instances.
    /// </summary>
    static class CloudTest
    {
        #region State

        /// <summary>
        /// Gets or sets the required test accuracy. 
        /// Assertions will fail only if an expected value will be
        /// different from an actual one by more than <see cref="Accuracy"/>.
        /// </summary>
        /// <value>The required test accuracy.</value>
        public static double Accuracy { get; set; }

        static CloudTest()
        {
            CloudTest.Accuracy = 1e-1;
        }

        #endregion

        #region Helpers 

        /// <summary>
        /// Tests the specified <see cref="Action"/> for each item in the 
        /// given list of <see cref="TestableCloud"/> instances.
        /// </summary>
        /// <param name="test">The test to be executed.</param>
        /// <param name="testableItems">The list of 
        /// <see cref="TestableCloud"/> instances 
        /// to test.</param>
        public static void TestAction(
            Action<TestableCloud> test,
            List<TestableCloud> testableItems)
        {
            for (int i = 0; i < testableItems.Count; i++)
            {
#if DEBUG
                Console.WriteLine("Testing cloud {0}", i);
#endif
                test(testableItems[i]);
            }
        }

        #endregion

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Coordinates"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        internal static class Coordinates
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Coordinates"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Coordinates,
                    actual: cloud
                        .Coordinates,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Weights"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        internal static class Weights
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Weights"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Weights,
                    actual: cloud
                        .Weights,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Basis"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        internal static class Basis
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Basis"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Basis.basisMatrixT,
                    actual: cloud
                        .Basis.basisMatrixT,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Mean"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        internal static class Mean
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Mean"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Mean,
                    actual: cloud
                        .Mean,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Variance"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        internal static class Variance
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Variance"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                Assert.AreEqual(
                    expected: testableCloud
                        .Variance,
                    actual: cloud
                        .Variance,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Covariance"/> 
        /// property has
        /// been properly implemented.
        /// </summary>
        internal static class Covariance
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Covariance"/> 
            /// property has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Covariance,
                    actual: cloud
                        .Covariance,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Center()"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        internal static class Center
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Centred"/> 
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                var actual = cloud.Center();

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Centred,
                    actual: actual
                        .Coordinates,
                    delta: CloudTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Weights,
                    actual: actual
                        .Weights,
                    delta: CloudTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Basis.basisMatrixT,
                    actual: actual
                        .Basis.basisMatrixT,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Standardize()"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        internal static class Standardize
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Standardize"/> 
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                var actual = cloud.Standardize();

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Standardized,
                    actual: actual
                        .Coordinates,
                    delta: CloudTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Weights,
                    actual: actual
                        .Weights,
                    delta: CloudTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Basis.basisMatrixT,
                    actual: actual
                        .Basis.basisMatrixT,
                    delta: CloudTest.Accuracy);
            }
        }

        /// <summary>
        /// Provides methods to test that the 
        /// <see cref="Advanced.Cloud.Rebase(Advanced.Basis)"/> 
        /// method has
        /// been properly implemented.
        /// </summary>
        internal static class Rebase
        {
            /// <summary>
            /// Tests that the 
            /// <see cref="Advanced.Cloud.Rebase(Advanced.Basis)"/> 
            /// method has
            /// been properly implemented.
            public static void Succeed(
                TestableCloud testableCloud)
            {
                var cloud =
                    testableCloud.Cloud;

                var rebased = testableCloud.Rebased;
                var newBasis = rebased.Keys.First();
                var newCoordinates = rebased.Values.First();

                var actual = cloud.Rebase(newBasis);

                DoubleMatrixAssert.AreEqual(
                    expected: newCoordinates,
                    actual: actual
                        .Coordinates,
                    delta: CloudTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: testableCloud
                        .Weights,
                    actual: actual
                        .Weights,
                    delta: CloudTest.Accuracy);

                DoubleMatrixAssert.AreEqual(
                    expected: newBasis
                        .basisMatrixT,
                    actual: actual
                        .Basis.basisMatrixT,
                    delta: CloudTest.Accuracy);
            }
        }
    }
}
