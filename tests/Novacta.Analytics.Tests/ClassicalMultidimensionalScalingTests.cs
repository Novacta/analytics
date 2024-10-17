// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using Novacta.Analytics.Tests.TestableItems.MDS;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class ClassicalMultidimensionalScalingTests
    {
        [TestMethod]
        public void AnalyzeTest()
        {
            ClassicalMultidimensionalScalingTest.Analyze.Succeed(
                TestableClassicalMultidimensionalScaling00.Get());

            ClassicalMultidimensionalScalingTest.Analyze.Succeed(
                TestableClassicalMultidimensionalScaling01.Get());

            ClassicalMultidimensionalScalingTest.Analyze.Succeed(
                TestableClassicalMultidimensionalScaling02.Get());

            ClassicalMultidimensionalScalingTest.Analyze.Succeed(
                TestableClassicalMultidimensionalScaling03.Get());

            ClassicalMultidimensionalScalingTest.Analyze.ProximitiesIsNull();

            ClassicalMultidimensionalScalingTest.Analyze.ProximitiesIsNotSymmetric();

            ClassicalMultidimensionalScalingTest.Analyze.ProximitiesCannotBeScaled();

            ClassicalMultidimensionalScalingTest.Analyze.ConfigurationDimensionIsNotPositive();

            ClassicalMultidimensionalScalingTest.Analyze
                .ConfigurationDimensionIsGreaterThanProximitiesNumberOfRows();

            ClassicalMultidimensionalScalingTest.Analyze
                .ConfigurationDimensionIsUnallowedGivenNumberOfPositiveEigenvalues();
        }
    }
}
