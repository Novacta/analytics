// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// the column profiles
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
    class TestableColumnProfiles00 : TestablePrincipalProjections
    {
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

        static readonly DoubleMatrix inv_sqrt_f_r = f_r.Apply(
            x => 1.0 / Math.Sqrt(x));
        static readonly Basis a = new(
            DoubleMatrix.Diagonal(inv_sqrt_f_r));

        static readonly DoubleMatrix x_sa =
            DoubleMatrix.Dense(3, 6, 
                [
                    0.01850208, 0.1266882, 0.2729859, 0.3823338, 0.09000047, 0.10948952,
                    0.01630880, 0.1273447, 0.2295335, 0.4234903, 0.13072540, 0.07259731,
                    0.03053954, 0.2149474, 0.3001551, 0.3180348, 0.06421107, 0.07211202 ], 
                StorageOrder.RowMajor);

        static readonly DoubleMatrix f_c =
            DoubleMatrix.Dense(1, 3,
                [0.2972444, 0.5642695, 0.1384862]);

        static readonly DoubleMatrix w_s = f_c.Transpose();

        static readonly Cloud activeCloud;

        static readonly DoubleMatrix coordinates;

        static readonly DoubleMatrix contributions;

        static readonly DoubleMatrix correlations;

        static readonly DoubleMatrix directions;

        static readonly DoubleMatrix regressionCoefficients;

        static readonly DoubleMatrix representationQualities;

        static readonly DoubleMatrix variances;

        static TestableColumnProfiles00()
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
        /// <see cref="TestableColumnProfiles00" /> class.
        /// </summary>
        TestableColumnProfiles00() : base(
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
        /// <see cref="TestableColumnProfiles00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableColumnProfiles00"/> class.</returns>
        public static TestableColumnProfiles00 Get()
        {
            return new TestableColumnProfiles00();
        }
    }
}
