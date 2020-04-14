// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Distribution
{
    /// <summary>
    /// Provides methods to test implementations of the Exponential
    /// distribution having rate equal to <c>12.38465</c>.
    /// </summary>
    class TestableExponentialDistribution00 : TestableProbabilityDistribution
    {
        const double rate = 12.38465;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableExponentialDistribution00" /> class.
        /// </summary>
        TestableExponentialDistribution00() : base(
            probabilityDistribution:
                new ExponentialDistribution(rate: rate),
            mean: 1.0 / rate,
            variance: 1.0 / (rate * rate),
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[17]{
                            -1.0,
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
                        numberOfRows: 17,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 16),
                    DoubleMatrix.Dense(17, 1, new Double[17]{
                        0.0,
                        3.589427799784654e+000,
                        4.350924540285889e-006,
                        5.273972736378129e-012,
                        6.392845512837189e-018,
                        7.749087034353631e-024,
                        9.393055055907102e-030,
                        1.138579072504372e-035,
                        1.380128506251731e-041,
                        1.672922627656406e-047,
                        2.027832991962262e-053,
                        2.458037553745876e-059,
                        2.979509969298932e-065,
                        3.611612704461358e-071,
                        4.377815970220162e-077,
                        5.306569180421715e-083,
                        6.432357289149691e-089})
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[17]{
                            -1.0,
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
                        numberOfRows: 17,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 16),
                    DoubleMatrix.Dense(17, 1, new Double[17]{
                        0.0,
                        7.101712361847405e-001,
                        9.999996486840936e-001,
                        9.999999999995741e-001,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000,
                        1.000000000000000e+000})
                }
            },
            canInvertCdf: true,
            inverseCdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[8]{
                            -0.01,
                             0.0,
                             0.000001,
                             .25,
                             .75,
                             .9999999,
                             1.0,
                             1.01},
                        numberOfRows: 8,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 7),
                    DoubleMatrix.Dense(8, 1, new Double[8]{
                         Double.NaN,
                         0.0,
                         8.07451563023851E-08,
                         0.0232289222910442,
                         0.111936498901454,
                         1.30145750194674,
                         Double.PositiveInfinity,
                         Double.NaN})
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableExponentialDistribution00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableExponentialDistribution00"/> class.</returns>
        public static TestableExponentialDistribution00 Get()
        {
            return new TestableExponentialDistribution00();
        }
    }
}
