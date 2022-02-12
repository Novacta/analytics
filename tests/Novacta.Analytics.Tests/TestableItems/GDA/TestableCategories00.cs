// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// categories of the
    /// multiple correspondence regarding the 
    /// categorical data set
    /// <para /> 
    /// COLOR  NUMBER    <para />
    /// Red    Negative  <para />
    /// Green  Zero      <para />
    /// Red    Negative  <para />
    /// Blue   Negative  <para />
    /// Blue   Positive  <para />
    /// </summary>
    class TestableCategories00 : TestablePrincipalProjections
    {
        static readonly DoubleMatrix f_r =
            DoubleMatrix.Dense(5, 1, .2);

        static readonly DoubleMatrix inv_sqrt_f_r = f_r.Apply(
            x => 1.0 / Math.Sqrt(x));
        static readonly Basis a = new(
            DoubleMatrix.Diagonal(inv_sqrt_f_r));

        static readonly DoubleMatrix x_sa =
            DoubleMatrix.Dense(6, 5,
                new double[30] {
                    0.5000000,  0, 0.5000000, 0.0000000, 0.0,
                    0.0000000,  1, 0.0000000, 0.0000000, 0.0,
                    0.0000000,  0, 0.0000000, 0.5000000, 0.5,
                    0.3333333,  0, 0.3333333, 0.3333333, 0.0,
                    0.0000000,  1, 0.0000000, 0.0000000, 0.0,
                    0.0000000,  0, 0.0000000, 0.0000000, 1.0
                }, StorageOrder.RowMajor);

        static readonly DoubleMatrix f_c =
            DoubleMatrix.Dense(1, 6,
                new double[6] { 0.2, 0.1, 0.2, 0.3, 0.1, 0.1 });

        static readonly DoubleMatrix w_s = f_c.Transpose();

        static readonly Cloud activeCloud;

        static readonly DoubleMatrix coordinates;

        static readonly DoubleMatrix contributions;

        static readonly DoubleMatrix correlations;

        static readonly DoubleMatrix directions;

        static readonly DoubleMatrix regressionCoefficients;

        static readonly DoubleMatrix representationQualities;

        static readonly DoubleMatrix variances;


        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1810:Initialize reference type static fields inline",
            Justification = "Performance is not a concern.")]
        static TestableCategories00()
        {
            string[] columnNames = new string[]
                {
                    "Red", "Green", "Blue", "Negative", "Zero", "Positive"
                };

            for (int j = 0; j < x_sa.NumberOfColumns; j++)
            {
                x_sa.SetColumnName(j, columnNames[j]);
            }

            for (int i = 0; i < x_sa.NumberOfRows; i++)
            {
                x_sa.SetRowName(i, String.Format("i{0}", i + 1));
            }

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
        /// <see cref="TestableCategories00" /> class.
        /// </summary>
        TestableCategories00() : base(
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
        /// <see cref="TestableCategories00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableCategories00"/> class.</returns>
        public static TestableCategories00 Get()
        {
            return new TestableCategories00();
        }
    }
}
