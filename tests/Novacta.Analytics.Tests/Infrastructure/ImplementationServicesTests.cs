// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems;
using Novacta.Analytics.Tests.TestableItems.Addition;
using Novacta.Analytics.Tests.TestableItems.Division;
using Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.TestableItems.Multiplication;
using Novacta.Analytics.Tests.TestableItems.Negation;
using Novacta.Analytics.Tests.TestableItems.Subtraction;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Novacta.Analytics.Tests.Infrastructure
{
    public class ImplementationServicesTests
    {
        [TestMethod()]
        public void ToComplexArrayTest()
        {
            double[] doubleArray = Array.Empty<double>();

            var actual = ImplementationServices.ToComplexArray(doubleArray);

            Complex[] expected = Array.Empty<Complex>();

            ArrayAssert<Complex>.AreEqual(expected, actual);
        }
    }
}
