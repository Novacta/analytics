// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Distribution
{
    /// <summary>
    /// Provides methods to test basic implementations of the Gaussian
    /// distribution (no overrides of virtual functions) having mean 
    /// <c>-1.246312</c> and standard deviation
    /// <c>2.876598</c>.
    /// </summary>
    class TestableBasicGaussianDistribution00 : TestableProbabilityDistribution
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableBasicGaussianDistribution00" /> class.
        /// </summary>
        TestableBasicGaussianDistribution00() : base(
            probabilityDistribution:
                new BasicGaussianDistribution(mu: -1.246312, sigma: 2.876598),
            mean: -1.246312,
            variance: Math.Pow(2.876598, 2.0),
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[33]{
                            -0.1,
                            -1.2,
                            -2.3,
                            -3.4,
                            -4.5,
                            -5.6,
                            -6.7,
                            -7.8,
                            -8.9,
                            -10.0,
                            -11.1,
                            -12.2,
                            -13.3,
                            -14.4,
                            -15.5,
                            -16.6,
                            0.0,
                            0.1,
                            1.2,
                            2.3,
                            3.4,
                            4.5,
                            5.6,
                            6.7,
                            7.8,
                            8.9,
                            10.0,
                            11.1,
                            12.2,
                            13.3,
                            14.4,
                            15.5,
                            16.6},
                        numberOfRows: 33,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 32),
                    DoubleMatrix.Dense(33, 1, new Double[33]{
                        0.12809971437561015750183913,
                        0.13866747511425936423279381,
                        0.12968673463485569374853412,
                        0.10478786785160919270420976,
                        0.07315111548040882649690531,
                        0.04411897390810021191143164,
                        0.02298922625569894614971922,
                        0.01034946277234577785564262,
                        0.00402536957546407309083136,
                        0.00135265864915232723354743,
                        0.00039270385882622584495377,
                        0.00009850008941082278939375,
                        0.00002134531565656625385231,
                        0.00000399634560774924396682,
                        0.00000064642478270711313288,
                        0.00000009033737312946198472,
                        0.12626106592815339313418121,
                        0.12429850426538630037143207,
                        0.09660230802010652539912883,
                        0.06486398158220241239035886,
                        0.03762826601597399345244810,
                        0.01885902813982586259666796,
                        0.00816617932100211288415359,
                        0.00305501267986595537867744,
                        0.00098741932545048038093183,
                        0.00027573044992650568409467,
                        0.00006652154648365866821194,
                        0.00001386546521441909313958,
                        0.00000249689945807121950038,
                        0.00000038847418614337073032,
                        0.00000005221770457556176268,
                        0.00000000606412142999072224,
                        0.00000000060843263751472045})
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[33]{
                            -0.1,
                            -1.2,
                            -2.3,
                            -3.4,
                            -4.5,
                            -5.6,
                            -6.7,
                            -7.8,
                            -8.9,
                            -10.0,
                            -11.1,
                            -12.2,
                            -13.3,
                            -14.4,
                            -15.5,
                            -16.6,
                            0.0,
                            0.1,
                            1.2,
                            2.3,
                            3.4,
                            4.5,
                            5.6,
                            6.7,
                            7.8,
                            8.9,
                            10.0,
                            11.1,
                            12.2,
                            13.3,
                            14.4,
                            15.5,
                            16.6},
                        numberOfRows: 33,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 32),
                    DoubleMatrix.Dense(33, 1, new Double[33]{
                        0.654867594842958977707781,
                        0.506422522986286538504430,
                        0.357071891874265689992285,
                        0.227021214264884391154098,
                        0.129008842815357321898162,
                        0.065078250891844352521431,
                        0.028987879279436631202449,
                        0.011355029844501901559184,
                        0.003899227155279522214620,
                        0.001170890718447873844396,
                        0.000306873126087952295162,
                        0.000070086748969448505885,
                        0.000013931867820387709128,
                        0.000002407922536482515990,
                        0.000000361557760994169125,
                        0.000000047132345606196455,
                        0.667586693192083302506035,
                        0.680115677197046131219338,
                        0.802453757763913810840961,
                        0.891177527700809490873723,
                        0.946867467830534437212009,
                        0.977120352446058659801054,
                        0.991343752510473574801608,
                        0.997131086157401091973895,
                        0.999168984733565967992774,
                        0.999790003556823569930145,
                        0.999953773096488029992202,
                        0.999991145806228387549197,
                        0.999998525784755787348956,
                        0.999999786798311585123145,
                        0.999999973235657257930598,
                        0.999999997085103009553109,
                        0.999999999724708987791644})
                }
            },
            canInvertCdf: true,
            inverseCdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[27]{
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
                            0.99999,
                            1.0,
                            1.1},
                        numberOfRows: 27,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 26),
                DoubleMatrix.Dense(27, 1, new Double[27]{
                    Double.NaN,
                    Double.NegativeInfinity,
                   -14.92000285991133,
                   -13.51468832801681,
                   -11.94442738402884,
                   -10.13566807145772,
                    -7.93827964177014,
                    -6.92172781428166,
                    -5.97789465358135,
                    -4.93282067034247,
                    -3.66731795725338,
                    -2.75480146605493,
                    -1.97508977018624,
                    -1.24631200000000,
                    -0.51753422981377,
                     0.26217746605492,
                     1.17469395725338,
                     2.44019667034247,
                     4.42910381428166,
                     4.49363240524215,
                     4.66149800882568,
                     5.44565564177014,
                     7.64304407145775,
                     9.45180338402883,
                    11.02206432801931,
                    Double.PositiveInfinity,
                    Double.NaN})
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableBasicGaussianDistribution00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableBasicGaussianDistribution00"/> class.</returns>
        public static TestableBasicGaussianDistribution00 Get()
        {
            return new TestableBasicGaussianDistribution00();
        }
    }
}
