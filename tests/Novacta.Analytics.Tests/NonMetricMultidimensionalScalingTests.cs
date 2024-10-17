// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using Novacta.Analytics.Tests.TestableItems.MDS;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class NonMetricMultidimensionalScalingTests
    {
        [TestMethod]
        public void AnalyzeTest()
        {
            NonMetricMultidimensionalScalingTest.Analyze.Succeed(
                TestableNonMetricMultidimensionalScaling00.Get());

            NonMetricMultidimensionalScalingTest.Analyze.Succeed(
                TestableNonMetricMultidimensionalScaling01.Get());

            NonMetricMultidimensionalScalingTest.Analyze.Succeed(
                TestableNonMetricMultidimensionalScaling02.Get());

            NonMetricMultidimensionalScalingTest.Analyze.Succeed(
                TestableNonMetricMultidimensionalScaling03.Get());

            NonMetricMultidimensionalScalingTest.Analyze.Succeed(
                TestableNonMetricMultidimensionalScaling04.Get());

            NonMetricMultidimensionalScalingTest.Analyze.Succeed(
                TestableNonMetricMultidimensionalScaling05.Get());
            
            NonMetricMultidimensionalScalingTest.Analyze.DissimilaritiesIsNull();

            NonMetricMultidimensionalScalingTest.Analyze.DissimilaritiesIsNotSymmetric();

            NonMetricMultidimensionalScalingTest.Analyze.DissimilaritiesCannotBeClassicallyScaled();

            NonMetricMultidimensionalScalingTest.Analyze.ConfigurationDimensionIsNotPositive();
            
            NonMetricMultidimensionalScalingTest.Analyze.MinkowskiDistanceOrderIsNotGreaterThanOne();

            NonMetricMultidimensionalScalingTest.Analyze.MaximumNumberOfIterationsIsNotPositive();

            NonMetricMultidimensionalScalingTest.Analyze.TerminationToleranceIsNotPositive();

            NonMetricMultidimensionalScalingTest.Analyze
                .ConfigurationDimensionIsGreaterThanDissimilaritiesNumberOfRows();

            NonMetricMultidimensionalScalingTest.Analyze
                .ConfigurationDimensionIsUnallowedGivenNumberOfPositiveEigenvalues();
        }
    }
}
