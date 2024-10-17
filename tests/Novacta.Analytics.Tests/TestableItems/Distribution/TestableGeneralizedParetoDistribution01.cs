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
    /// and shape <c>-2.0</c>.
    /// </summary>
    class TestableGeneralizedParetoDistribution01 : TestableProbabilityDistribution
    {
        const double mu = -1.0;
        const double sigma = 1.4;
        const double xi = -2.0;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution01" /> class.
        /// </summary>
        TestableGeneralizedParetoDistribution01() : base(
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
                        asColumnMajorDenseArray: [
                            -2,
                            -1,
                            -0.8,
                            -0.6,
                            -0.4,
                            -0.2,
                            0,
                            0.2,
                            0.4,
                            0.6,
                            0.8,
                            1,
                            1.2,
                            1.4,
                            1.6,
                            1.8,
                            2,
                            2.2,
                            2.4,
                            2.6,
                            2.8,
                            3,
                            3.2,
                            3.4,
                            3.6,
                            3.8,
                            4],
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
                    DoubleMatrix.Dense(27, 1, [
                        0,
                        0.714285714285714,
                        0.8451542547285170000000000,
                        1.0910894511799600000000000,
                        1.8898223650461400000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.0000000000000000000000000])
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                            -2,
                            -1,
                            -0.8,
                            -0.6,
                            -0.4,
                            -0.2,
                            0,
                            0.2,
                            0.4,
                            0.6,
                            0.8,
                            1,
                            1.2,
                            1.4,
                            1.6,
                            1.8,
                            2,
                            2.2,
                            2.4,
                            2.6,
                            2.8,
                            3,
                            3.2,
                            3.4,
                            3.6,
                            3.8,
                            4],
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
                    DoubleMatrix.Dense(27, 1, [
                        0.0000000000000000000000000,
                        0.0000000000000000000000000,
                        0.1548457452714830000000000,
                        0.3453463292920230000000000,
                        0.6220355269907730000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000,
                        1.0000000000000000000000000])
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
                            0.99999,
                            1.0,
                            1.1],
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
                    DoubleMatrix.Dense(27, 1, [
                        Double.NaN,
                        Double.NaN,
                        -0.9999986000007000000000000,
                        -0.9999860000700000000000000,
                        -0.9998600070000000000000000,
                        -0.9986007000000000000000000,
                        -0.9860700000000000000000000,
                        -0.9664616437500000000000000,
                        -0.9317500000000000000000000,
                        -0.8670000000000000000000000,
                        -0.7480000000000000000000000,
                        -0.6430000000000000000000000,
                        -0.5520000000000000000000000,
                        -0.4750000000000000000000000,
                        -0.4120000000000000000000000,
                        -0.3630000000000000000000000,
                        -0.3280000000000000000000000,
                        -0.3070000000000000000000000,
                        -0.3004116437500000000000000,
                        -0.3003703000000000000000000,
                        -0.3002800000000000000000000,
                        -0.3000700000000000000000000,
                        -0.3000007000000000000000000,
                        -0.3000000070000000000000000,
                        -0.3000000000700000000000000,
                        Double.NaN,
                        Double.NaN])
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableGeneralizedParetoDistribution01"/> class.</returns>
        public static TestableGeneralizedParetoDistribution01 Get()
        {
            return new TestableGeneralizedParetoDistribution01();
        }
    }
}
