// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Distribution
{
    /// <summary>
    /// Provides methods to test implementations of the 
    /// Generalized Pareto
    /// distribution having location <c>-1.0</c>, scale <c>1.4</c>,
    /// and shape <c>2.0</c>.
    /// </summary>
    class TestableGeneralizedParetoDistribution02 : TestableProbabilityDistribution
    {
        const double mu = -1.0;
        const double sigma = 1.4;
        const double xi = 2.0;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution02" /> class.
        /// </summary>
        TestableGeneralizedParetoDistribution02() : base(
            probabilityDistribution:
                new GeneralizedParetoDistribution(
                    mu: mu,
                    sigma: sigma,
                    xi: xi),
            mean: Double.PositiveInfinity,
            variance: Double.PositiveInfinity,
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                           -2,
                           -1,
                            0,
                            1,
                            2,
                            3,
                            4,
                            5,
                            6,
                            7,
                            8,
                            9,
                            10,
                            11,
                            12,
                            13,
                            14,
                            15,
                            16,
                            17,
                            18,
                            19,
                            20],
                        numberOfRows: 23,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 22),
                    DoubleMatrix.Dense(23, 1, [
                        0.0,
                        0.714285714285714,
                        0.1887320435058080000000000,
                        0.0942916809661695000000000,
                        0.0587782211082216000000000,
                        0.0410555843078702000000000,
                        0.0307402036864238000000000,
                        0.0241216393714722000000000,
                        0.0195786587388158000000000,
                        0.0163019720957234000000000,
                        0.0138471827195190000000000,
                        0.0119520763240493000000000,
                        0.0104529827023553000000000,
                        0.0092430104258637100000000,
                        0.0082497003394970600000000,
                        0.0074223772189113000000000,
                        0.0067246478373365100000000,
                        0.0061297719521240400000000,
                        0.0056177075255507800000000,
                        0.0051731683820313800000000,
                        0.0047843141095176700000000,
                        0.0044418460899144200000000,
                        0.0041383710144418200000000])
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                           -2,
                           -1,
                            0,
                            1,
                            2,
                            3,
                            4,
                            5,
                            6,
                            7,
                            8,
                            9,
                            10,
                            11,
                            12,
                            13,
                            14,
                            15,
                            16,
                            17,
                            18,
                            19,
                            20],
                        numberOfRows: 23,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 22),
                    DoubleMatrix.Dense(23, 1, [
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.3583110520802520000000000,
                        0.4908249227826840000000000,
                        0.5650411637991600000000000,
                        0.6140775075060200000000000,
                        0.6495616779747690000000000,
                        0.6767700324222730000000000,
                        0.6984886554222360000000000,
                        0.7163456855344120000000000,
                        0.7313646552413320000000000,
                        0.7442255666653460000000000,
                        0.7554002047648860000000000,
                        0.7652275351830620000000000,
                        0.7739582106977810000000000,
                        0.7817821097640080000000000,
                        0.7888460579076330000000000,
                        0.7952656167990570000000000,
                        0.8011331535955020000000000,
                        0.8065235025120260000000000,
                        0.8114980240850040000000000,
                        0.8161075718775430000000000,
                        0.8203946979732250000000000])
                }
            },
            canInvertCdf: true,
            inverseCdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                            -1.0,
                            0.0,
                            0.000001,
                            0.00001,
                            0.0001,
                            0.001,
                            0.01,
                            0.02425,
                            0.05,
                            0.1,
                            0.2,
                            0.3,
                            0.4,
                            0.5,
                            0.6,
                            0.7,
                            0.8,
                            0.9,
                            0.97575,
                            0.977,
                            0.98,
                            0.99,
                            0.999,
                            0.9999,
                            1.0,
                            1.1],
                        numberOfRows: 26,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 25),
                    DoubleMatrix.Dense(26, 1, [
                        Double.NaN,
                        Double.NaN,
                        -0.9999985999979000000000000,
                        -0.9999859997899970000000000,
                        -0.9998599789972000000000000,
                        -0.9985978971964960000000000,
                        -0.9857871645750430000000000,
                        -0.9647738926986070000000000,
                        -0.9243767313019390000000000,
                        -0.8358024691358030000000000,
                        -0.6062500000000000000000000,
                        -0.2714285714285710000000000,
                        0.2444444444444440000000000,
                        1.1000000000000000000000000,
                        2.6750000000000000000000000,
                        6.0777777777777800000000000,
                        15.8000000000000000000000000,
                        68.3000000000000000000000000,
                        1188.6496652141600000000000000,
                        1321.5514177693700000000000000,
                        1748.3000000000000000000000000,
                        6998.2999999999900000000000000,
                        699998.2999999990000000000000000,
                        69999998.3000154000000000000000000,
                        Double.NaN,
                        Double.NaN])
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution02"/> class.</returns>
        public static TestableGeneralizedParetoDistribution02 Get()
        {
            return new TestableGeneralizedParetoDistribution02();
        }
    }
}
