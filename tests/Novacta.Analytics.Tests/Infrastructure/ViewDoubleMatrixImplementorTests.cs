// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Novacta.Analytics.Infrastructure.Tests
{
    [TestClass]
    public class ViewDoubleMatrixImplementorTests
    {
        [TestMethod]
        public void StorageOrderTest()
        {
            var testableMatrix = TestableDoubleMatrix00.Get();
            var matrix = testableMatrix.View;

            Assert.AreEqual(
                expected: StorageOrder.ColumnMajor,
                actual: matrix.StorageOrder);
        }

        [TestMethod]
        public void StorageTest()
        {
            var testableMatrix = TestableDoubleMatrix00.Get();
            var matrix = testableMatrix.View;

            var storage = matrix.GetStorage();

            DoubleArrayAssert.AreEqual(
                expected: testableMatrix.Expected.AsColumnMajorDenseArray,
                actual: storage,
                delta: DoubleMatrixTest.Accuracy);

            Assert.AreEqual(
                expected: StorageScheme.Dense,
                actual: matrix.StorageScheme);
        }

        [TestMethod]
        public void ImplementorChangedHandlerTest()
        {
            var testableMatrix = TestableDoubleMatrix16.Get();

            var dense0 = testableMatrix.Dense;
            // dense0 =  
            //      [c0]  [c1]  [c2]  
            // [r0]   0     2     4   
            // [r1]   1     3     5   

            var sub1 = dense0[":", ":", true];
            // sub1 =  
            //      [c0]  [c1]  [c2]  
            // [r0]   0     2     4   
            // [r1]   1     3     5   

            var sub2 = sub1[":", IndexCollection.Range(0, 1)];
            // sub2 =  
            //      [c0]  [c1]  
            // [r0]   0     2   
            // [r1]   1     3   

            /*
            Due to the following line, dense0.matrixImplementor raises event ChangingData.
            Since sub1.matrixImplementor is listening for such event from the 
            dense0.matrixImplementor source (remember that dense0.matrixImplementor == 
            sub1.matrixImplementor.ParentImplementor is true),
            sub1.matrixImplementor.ChangingDataHandler is executed, so that sub1.matrixImplementor 
            raises the StorerChanged event. Both sub1 and sub2.matrixImplementor are listening 
            for it, hence the following happens:
            1. sub1 is no longer implemented by a ViewDoubleMatrixImplementor, since its implementor
            is changed in one having a Dense storage scheme, say sub1DenseParentImplementor.
            2. sub2 continues to be implemented by a ViewDoubleMatrixImplementor, but 
            sub2.matrixImplementor.ParentImplementor is set to sub1DenseParentImplementor.
            */
            dense0[1, 0] = -10.0;

            Assert.AreEqual(
                expected: 1.0,
                actual: sub1[1, 0]);

            Assert.AreEqual(
                expected: 1.0,
                actual: sub2[1, 0]);

            Assert.AreEqual(
                expected: StorageScheme.Dense,
                actual: sub1.StorageScheme);

            Assert.AreEqual(
                expected: StorageScheme.View,
                actual: sub2.StorageScheme);

            var actualSub2Implmentor = (ViewDoubleMatrixImplementor)
                sub2.implementor;

            Assert.AreEqual(
                expected: sub1.implementor,
                actual: actualSub2Implmentor.parentImplementor);
        }
    }
}