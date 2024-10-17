// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// a cloud having basis
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
    class TestableCloud00 : TestableCloud
    {
        static readonly Basis a = new(
            DoubleMatrix.Dense(2, 2, [3, 0, 0, 2]));

        static readonly DoubleMatrix x_sa =
            DoubleMatrix.Dense(10, 2,
                [
                    16,
                    13,
                    13,
                     6,
                    16,
                    11,
                    13,
                    17,
                    15,
                    10,
                    15,
                     5,
                    12,
                    11,
                    10,
                    12,
                     9,
                     7,
                    15,
                     4]);

        static readonly DoubleMatrix w_s =
            DoubleMatrix.Dense(10, 1, 1.0 / 10.0);

        static readonly DoubleMatrix m_sa =
            DoubleMatrix.Dense(1, 2, [13, 10]);

        static readonly double var_s;

        static readonly DoubleMatrix cov_sa =
            DoubleMatrix.Dense(2, 2, [10, 2, 2, 13]);

        static readonly DoubleMatrix centred_x_sa =
            DoubleMatrix.Dense(10, 2,
                [
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
                             4-10]);

        static readonly DoubleMatrix standardized_x_sa =
            centred_x_sa *
                DoubleMatrix.Dense(2, 2, [
                    1.0 / Math.Sqrt(cov_sa[0,0]), 0, 0, 1.0 / Math.Sqrt(cov_sa[1,1]) ]
                );

        static readonly Dictionary<Basis, DoubleMatrix> rebased;

        static TestableCloud00()
        {
            var q_a = a.basisScalarProducts;
            var cov_sa_by_q_a = cov_sa * q_a;
            var_s = cov_sa_by_q_a[0] + cov_sa_by_q_a[3];

            Basis newBasis = Basis.Standard(2);

            rebased = new Dictionary<Basis, DoubleMatrix>()
            {
                {
                    newBasis,
                    Basis.ChangeCoordinates(newBasis, x_sa, a)
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableCloud00" /> class.
        /// </summary>
        TestableCloud00() : base(
            cloud: new Cloud(
                coordinates: x_sa,
                weights: w_s,
                basis: a, copyData: true),
            coordinates: x_sa,
            weights: w_s,
            basis: a,
            mean: m_sa,
            variance: var_s,
            covariance: cov_sa,
            centred: centred_x_sa,
            standardized: standardized_x_sa,
            rebased: rebased
            )
        {

        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableCloud00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableCloud00"/> class.</returns>
        public static TestableCloud00 Get()
        {
            return new TestableCloud00();
        }
    }
}
