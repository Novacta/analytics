// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// the row profiles
    /// of the contingency table
    /// <para /> 
    ///               Period Comma   OtherMarks <para />
    /// Rousseau      7836   13112   6026       <para />
    /// Chateaubriand 53655  102383  42413      <para />
    /// Hugo          115615 184541  59226      <para />
    /// Zola          161926 340479  62754      <para />
    /// Proust        38117  105101  12670      <para />
    /// Giraudoux     46371  58367   14229      <para />
    /// </summary>
    class TestableRowProfiles00 : TestablePrincipalProjections
    {
        static readonly DoubleMatrix f_c =
            DoubleMatrix.Dense(1, 3,
                [0.2972444,  0.5642695,  0.1384862]);
        static readonly DoubleMatrix inv_sqrt_f_c = f_c.Apply(
            x=>1.0 / Math.Sqrt(x));
        static readonly Basis a = new(
            DoubleMatrix.Diagonal(inv_sqrt_f_c));

        static readonly DoubleMatrix x_sa =
            DoubleMatrix.Dense(6, 3, 
                [
                    0.2905020, 0.4860977,  0.2234003,
                    0.2703690, 0.5159107,  0.2137203,
                    0.3217050, 0.5134954,  0.1647996,
                    0.2865141, 0.6024482,  0.1110378,
                    0.2445153, 0.6742084,  0.0812763,
                    0.3897804, 0.4906150,  0.1196046
                ], StorageOrder.RowMajor);

        static readonly DoubleMatrix f_r =
            DoubleMatrix.Dense(6, 1,
                [
                    0.0189315,
                    0.1392814,
                    0.2522296,
                    0.3966526,
                    0.1094088,
                    0.0834961
                ]);

        static readonly DoubleMatrix w_s = f_r;

        static readonly Cloud activeCloud;

        static readonly DoubleMatrix coordinates;

        static readonly DoubleMatrix contributions;

        static readonly DoubleMatrix correlations;

        static readonly DoubleMatrix directions;

        static readonly DoubleMatrix regressionCoefficients;

        static readonly DoubleMatrix representationQualities;

        static readonly DoubleMatrix variances;

        static TestableRowProfiles00()
        {
            activeCloud = new Cloud(
                coordinates: x_sa,
                weights: w_s,
                basis: a);

            var principalProjections = activeCloud.GetPrincipalProjections();

            contributions =
                (DoubleMatrix)principalProjections.Contributions;

            coordinates =
                (DoubleMatrix)principalProjections.Coordinates;

            directions =
                (DoubleMatrix)principalProjections.Directions;

            representationQualities =
                (DoubleMatrix)principalProjections.RepresentationQualities;

            regressionCoefficients =
                (DoubleMatrix)principalProjections.RegressionCoefficients;

            correlations =
                (DoubleMatrix)principalProjections.Correlations;

            variances =
                (DoubleMatrix)principalProjections.Variances;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableRowProfiles00" /> class.
        /// </summary>
        TestableRowProfiles00() : base(
            principalProjections:
                     new PrincipalProjections(activeCloud),
            activeCloud,
            coordinates,
            variances,
            directions,
            contributions,
            representationQualities,
            regressionCoefficients,
            correlations)
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableRowProfiles00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableRowProfiles00"/> class.</returns>
        public static TestableRowProfiles00 Get()
        {
            return new TestableRowProfiles00();
        }
    }
}
