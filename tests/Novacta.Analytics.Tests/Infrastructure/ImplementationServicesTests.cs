// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.Infrastructure
{
    public class ImplementationServicesTests
    {
        [TestMethod()]
        public void ToComplexArrayTest()
        {
            double[] doubleArray = [];

            var actual = ImplementationServices.ToComplexArray(doubleArray);

            Complex[] expected = [];

            ArrayAssert<Complex>.AreEqual(expected, actual);
        }
    }
}
