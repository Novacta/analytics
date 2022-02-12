// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using System.Globalization;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class CategoryTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            double expectedCode = 1.2;
            string expectedLabel = "The label";

            var target = new Category(expectedCode, expectedLabel);

            CategoryAssert.IsStateAsExpected(
                target,
                expectedCode,
                expectedLabel);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            double code = 1.2;
            string label = "The label";

            var target = new Category(code, label);

            string expected, actual;

            expected = target.Code.ToString(CultureInfo.InvariantCulture) +
                " - " + target.Label;
            actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}