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
    /// and shape <c>0.0</c>.
    /// </summary>
    class TestableGeneralizedParetoDistribution00 : TestableProbabilityDistribution
    {
        const double mu = -1.0;
        const double sigma = 1.4;
        const double xi = 0.0;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution00" /> class.
        /// </summary>
        TestableGeneralizedParetoDistribution00() : base(
            probabilityDistribution:
                new GeneralizedParetoDistribution(
                    mu: mu,
                    sigma: sigma,
                    xi: xi),
            mean: mu + sigma / (1.0 - xi),
            variance: sigma * sigma / ((1.0 - xi) * (1.0 - xi) * (1.0 - 2.0 * xi)),
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[23]{
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
                            20},
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
                    DoubleMatrix.Dense(23, 1, new Double[23]{
                        0,
                        0.714285714285714,
                        0.3496726139692520000000000,
                        0.1711793117441260000000000,
                        0.0837994043530363000000000,
                        0.0410232994768695000000000,
                        0.0200826141064086000000000,
                        0.0098312762378931400000000,
                        0.0048128192850610500000000,
                        0.0023560755399564900000000,
                        0.0011533971298718500000000,
                        0.0005646359450856900000000,
                        0.0002764128176027580000000,
                        0.0001353155894520670000000,
                        0.0000662426182242924000000,
                        0.0000324285212589178000000,
                        0.0000158751121140685000000,
                        0.0000077715287299738000000,
                        0.0000038044870717659100000,
                        0.0000018624549148752600000,
                        0.0000009117492698780370000,
                        0.0000004463392506759350000,
                        0.0000002185016575013040000})
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[23]{
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
                            20},
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
                    DoubleMatrix.Dense(23, 1, new Double[23]{
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.5104583404430470000000000,
                        0.7603489635582240000000000,
                        0.8826808339057490000000000,
                        0.9425673807323830000000000,
                        0.9718843402510280000000000,
                        0.9862362132669500000000000,
                        0.9932620530009150000000000,
                        0.9967014942440610000000000,
                        0.9983852440181790000000000,
                        0.9992095096768800000000000,
                        0.9996130220553560000000000,
                        0.9998105581747670000000000,
                        0.9999072603344860000000000,
                        0.9999546000702380000000000,
                        0.9999777748430400000000000,
                        0.9999891198597780000000000,
                        0.9999946737181000000000000,
                        0.9999973925631190000000000,
                        0.9999987235510220000000000,
                        0.9999993751250490000000000,
                        0.9999996940976800000000000})
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
                        Double.NaN,
                        -0.9999985999993000000000000,
                        -0.9999859999300000000000000,
                        -0.9998599929995330000000000,
                        -0.9985992995329830000000000,
                        -0.9859295298050980000000000,
                        -0.9656315779100220000000000,
                        -0.9281893878574290000000000,
                        -0.8524952780790430000000000,
                        -0.6875990281601060000000000,
                        -0.5006550784857750000000000,
                        -0.2848441267276130000000000,
                        -0.0295939472160767000000000,
                        0.2828070246238170000000000,
                        0.6855619260563100000000000,
                        1.2532130774077400000000000,
                        2.2236191301916600000000000,
                        4.2070741262381000000000000,
                        4.2811654882741800000000000,
                        4.4768322075994000000000000,
                        5.4472382603833300000000000,
                        8.6708573905749900000000000,
                        11.8944765207668000000000000,
                        15.1180956509647000000000000,
                        Double.NaN,
                        Double.NaN})
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution00"/> class.</returns>
        public static TestableGeneralizedParetoDistribution00 Get()
        {
            return new TestableGeneralizedParetoDistribution00();
        }
    }
}
