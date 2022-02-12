// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// principal projections of a cloud having basis
    /// <para /> 
    /// 3 0  <para /> 
    /// 0 2  <para /> 
    /// <para /> 
    /// coordinates, before centering, <para /> 
    ///     k1  k2 <para /> 
    /// i1  16  15 <para /> 
    /// i2  13   5 <para /> 
    /// i3  13  12 <para /> 
    /// i4   6  11 <para /> 
    /// i5  16  10 <para /> 
    /// i6  11  12 <para /> 
    /// i7  13   9 <para /> 
    /// i8  17   7 <para /> 
    /// i9  15  15 <para /> 
    /// i10 10   4 <para /> 
    /// and elementary weights equal to <c>1/10</c>.
    /// </summary>
    class TestablePrincipalProjections00 : TestablePrincipalProjections
    {
        static readonly double[] variableCoefficients = 
            new double[2] { 9, 4 };
        static readonly Basis a = new(
            DoubleMatrix.Dense(2, 2, new double[4] 
                { Math.Sqrt(variableCoefficients[0]),
                  0,
                  0,
                  Math.Sqrt(variableCoefficients[1]) }));

        static readonly DoubleMatrix x_sa =
            DoubleMatrix.Dense(10, 2,
                new double[20] {
                    16-13,
                    13-13,
                    13-13,
                     6-13,
                    16-13,
                    11-13,
                    13-13,
                    17-13,
                    15-13,
                    10-13,
                    15-10,
                     5-10,
                    12-10,
                    11-10,
                    10-10,
                    12-10,
                     9-10,
                     7-10,
                    15-10,
                     4-10});

        static readonly DoubleMatrix w_s =
            DoubleMatrix.Dense(10, 1, 1.0 / 10.0);

        static readonly Cloud activeCloud;

        static readonly DoubleMatrix coordinates =
            DoubleMatrix.Dense(10, 2,
                new double[20] {
                    -11.4,
                    2.8,
                    -1.1,
                    19.6,
                    -8.6,
                    4.7,
                    .6,
                    -9.9,
                    -8.5,
                    12.0,
                    7.1,
                    -9.6,
                    3.8,
                    7.8,
                    -2.5,
                    5.5,
                    -1.9,
                    -9.1,
                    7.9,
                    -9.0});

        static readonly DoubleMatrix contributions =
            DoubleMatrix.Dense(10, 2);

        static readonly DoubleMatrix correlations =
            DoubleMatrix.Dense(2, 2,
                new double[4] {
                    -.979,
                    -.373,
                    -.204,
                    .923});

        static readonly DoubleMatrix directions =
            DoubleMatrix.Dense(2, 2,
                new double[4] {
                   -.3202,
                   -.1390,
                   -.0927,
                    .4803});

        static readonly DoubleMatrix regressionCoefficients =
            DoubleMatrix.Dense(2, 2,
                new double[4] {
                   -3.0957,
                   -1.3437,
                    -.6454,
                    3.3459});

        static readonly DoubleMatrix representationQualities =
            DoubleMatrix.Dense(10, 2,
                new double[20] {
                    .72,
                    .08,
                    .08,
                    .87,
                    .92,
                    .42,
                    .08,
                    .54,
                    .54,
                    .64,
                    .28,
                    .92,
                    .92,
                    .13,
                    .08,
                    .58,
                    .92,
                    .46,
                    .46,
                    .36});

        static readonly DoubleMatrix variances =
            DoubleMatrix.Dense(2, 1,
                new double[2] { 93.4722, 48.5278 });


        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1810:Initialize reference type static fields inline",
            Justification = "Performance is not a concern.")]
        static TestablePrincipalProjections00()
        {
            var m_sp = w_s.Transpose() * coordinates;

            for (int i = 0; i < contributions.NumberOfRows; i++)
            {
                for (int j = 0; j < contributions.NumberOfColumns; j++)
                {
                    contributions[i, j] = w_s[i] * Math.Pow(coordinates[i, j] - m_sp[j], 2.0);
                }
            }

            contributions *= DoubleMatrix.Diagonal(
                variances.Apply(x => 1.0 / x));

            for (int i = 0; i < x_sa.NumberOfRows; i++)
            {
                x_sa.SetRowName(i, "i" + i);
                coordinates.SetRowName(i, "i" + i);
                representationQualities.SetRowName(i, "i" + i);
                contributions.SetRowName(i, "i" + i);
            }

            string format = "P_{0}";
            for (int j = 0; j < x_sa.NumberOfColumns; j++)
            {
                x_sa.SetColumnName(j, "k" + j);
                regressionCoefficients.SetRowName(j, "k" + j);
                correlations.SetRowName(j, "k" + j);
            }

            for (int j = 0; j < coordinates.NumberOfColumns; j++)
            {
                coordinates.SetColumnName(j, String.Format(format, j + 1));
                contributions.SetColumnName(j, String.Format(format, j + 1));
                representationQualities.SetColumnName(j, String.Format(format, j + 1));
                regressionCoefficients.SetColumnName(j, String.Format(format, j + 1));
                correlations.SetColumnName(j, String.Format(format, j + 1));
            }

            activeCloud = new Cloud(
                coordinates: x_sa,
                weights: w_s,
                basis: a);
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestablePrincipalProjections00" /> class.
        /// </summary>
        /// <param name="asPrincipalComponents">
        /// <c>true</c> if <see cref="TestablePrincipalProjections.PrincipalProjections"/>
        /// must be created as a <see cref="PrincipalComponents"/> instance;
        /// otherwise <c>false</c>.
        /// </param>
        TestablePrincipalProjections00(bool asPrincipalComponents) : base(
            principalProjections:
                asPrincipalComponents ?
                      PrincipalComponents.Analyze(
                          data: x_sa,
                          individualWeights: w_s,
                          variableCoefficients:
                             DoubleMatrix.Dense(1, 2, new double[2]
                                {
                                    variableCoefficients[0],
                                    variableCoefficients[1] })
                      )
                    : new PrincipalProjections(activeCloud),
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
        /// <see cref="TestablePrincipalProjections00"/> class.
        /// </summary>
        /// <param name="asPrincipalComponents">
        /// <c>true</c> if <see cref="TestablePrincipalProjections.PrincipalProjections"/>
        /// must be created as a <see cref="PrincipalComponents"/> instance;
        /// otherwise <c>false</c>.
        /// </param>
        /// <returns>An instance of the 
        /// <see cref="TestablePrincipalProjections00"/> class.</returns>
        public static TestablePrincipalProjections00 Get(bool asPrincipalComponents)
        {
            return new TestablePrincipalProjections00(asPrincipalComponents);
        }
    }
}
