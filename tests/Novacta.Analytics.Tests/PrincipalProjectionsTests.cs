// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.TestableItems.GDA;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class PrincipalProjectionsTests
    {
        [TestMethod()]
        public void ActiveCloudTest()
        {
            PrincipalProjectionsTest.ActiveCloud.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void ContributionsTest()
        {
            PrincipalProjectionsTest.Contributions.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void CoordinatesTest()
        {
            PrincipalProjectionsTest.Coordinates.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void CorrelationsTest()
        {
            PrincipalProjectionsTest.Correlations.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void CorrelateSupplementaryVariablesTest()
        {
            PrincipalProjectionsTest.CorrelateSupplementaryVariables.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));

            PrincipalProjectionsTest.CorrelateSupplementaryVariables.Fail
                .SupplementaryVariablesIsNull(
                    TestablePrincipalProjections00.Get(
                        asPrincipalComponents: false));

            PrincipalProjectionsTest.CorrelateSupplementaryVariables.Fail
                .SupplementaryVariablesHasWrongNumberOfRows(
                    TestablePrincipalProjections00.Get(
                        asPrincipalComponents: false));
        }

        [TestMethod()]
        public void DirectionsTest()
        {
            PrincipalProjectionsTest.Directions.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void LocateSupplementaryPointsTest()
        {
            PrincipalProjectionsTest.LocateSupplementaryPoints.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));

            PrincipalProjectionsTest.LocateSupplementaryPoints.Fail
                .ActiveCoordinatesIsNull(
                    TestablePrincipalProjections00.Get(
                        asPrincipalComponents: false));

            PrincipalProjectionsTest.LocateSupplementaryPoints.Fail
                .ActiveCoordinatesHasWrongNumberOfColumns(
                    TestablePrincipalProjections00.Get(
                        asPrincipalComponents: false));
        }

        [TestMethod()]
        public void RegressionCoefficientsTest()
        {
            PrincipalProjectionsTest.RegressionCoefficients.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void RegressSupplementaryVariablesTest()
        {
            PrincipalProjectionsTest.RegressSupplementaryVariables.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));

            PrincipalProjectionsTest.RegressSupplementaryVariables.Fail
                .SupplementaryVariablesIsNull(
                    TestablePrincipalProjections00.Get(
                        asPrincipalComponents: false));

            PrincipalProjectionsTest.RegressSupplementaryVariables.Fail
                .SupplementaryVariablesHasWrongNumberOfRows(
                    TestablePrincipalProjections00.Get(
                        asPrincipalComponents: false));
        }

        [TestMethod()]
        public void RepresentationQualitiesTest()
        {
            PrincipalProjectionsTest.RepresentationQualities.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }

        [TestMethod()]
        public void VariancesTest()
        {
            PrincipalProjectionsTest.Variances.Succeed(
                TestablePrincipalProjections00.Get(asPrincipalComponents: false));
        }
    }
}
