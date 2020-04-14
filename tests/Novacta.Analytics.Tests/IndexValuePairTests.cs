// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Novacta.Analytics.Tests
{
    [TestClass]
    public class IndexValuePairTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            // obj is not an IndexValuePair instance
            {
                IndexValuePair target;
                target.index = 1;
                target.value = -1.1;

                Assert.IsFalse(target.Equals(new object()));
            }

            // obj is an equal IndexValuePair instance
            {
                IndexValuePair target;
                target.index = 1;
                target.value = -1.1;

                IndexValuePair obj;
                obj.index = target.Index;
                obj.value = target.Value;
                Assert.IsTrue(target.Equals(obj));
                Assert.IsTrue(target.Equals((object)obj));
            }

            // obj/other is an unequal IndexValuePair instance - index 
            {
                IndexValuePair target;
                target.index = 1;
                target.value = -1.1;

                IndexValuePair obj;
                obj.index = target.Index + 2;
                obj.value = target.Value;
                Assert.IsFalse(target.Equals(obj));
                Assert.IsFalse(target.Equals((object)obj));
            }

            // obj/other is an unequal IndexValuePair instance - value 
            {
                IndexValuePair target;
                target.index = 1;
                target.value = -1.1;

                IndexValuePair obj;
                obj.index = target.Index;
                obj.value = target.Value + 2.2;
                Assert.IsFalse(target.Equals(obj));
                Assert.IsFalse(target.Equals((object)obj));
            }

            // left and right are equal IndexValuePair instances 
            {
                IndexValuePair left;
                left.index = 1;
                left.value = -1.1;

                IndexValuePair right;
                right.index = left.Index;
                right.value = left.Value;
                Assert.IsTrue(left == right);
                Assert.IsFalse(left != right);
            }

            // left and right are unequal IndexValuePair instances - index 
            {
                IndexValuePair left;
                left.index = 1;
                left.value = -1.1;

                IndexValuePair right;
                right.index = left.Index + 2;
                right.value = left.Value;
                Assert.IsFalse(left == right);
                Assert.IsTrue(left != right);
            }

            // left and right are unequal IndexValuePair instances - value 
            {
                IndexValuePair left;
                left.index = 1;
                left.value = -1.1;

                IndexValuePair right;
                right.index = left.Index;
                right.value = left.Value + 2.2;
                Assert.IsFalse(left == right);
                Assert.IsTrue(left != right);
            }

        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            int index = 3;
            double value = -4.3;
            IndexValuePair target;
            target.index = index;
            target.value = value;

            Assert.AreEqual(
                expected: target.GetHashCode(),
                actual: value.GetHashCode() ^ index);
        }
    }
}
