// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Distribution
{
    /// <summary>
    /// Provides methods to test implementations of the Uniform
    /// distribution having lower bound <c>-1.0</c> and upper bound
    /// <c>1.0</c>.
    /// </summary>
    class TestableUniformDistribution00 : TestableProbabilityDistribution
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableUniformDistribution00" /> class.
        /// </summary>
        TestableUniformDistribution00() : base(
            probabilityDistribution:
                new UniformDistribution(lowerBound: -1.0, upperBound: 1.0),
            mean: 0.0,
            variance: 1.0 / 3.0,
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                            -2.0,
                            -1.5,
                            -1.0,
                            -0.5,
                            0.0,
                            0.5,
                            1.0,
                            1.5,
                            2.0],
                        numberOfRows: 9,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 8),
                    DoubleMatrix.Dense(9, 1, [
                        0.0,
                        0.0,
                        0.5,
                        0.5,
                        0.5,
                        0.5,
                        0.5,
                        0.0,
                        0.0])
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                            -2.0,
                            -1.5,
                            -1.0,
                            -0.5,
                            0.0,
                            0.5,
                            1.0,
                            1.5,
                            2.0],
                        numberOfRows: 9,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 8),
                    DoubleMatrix.Dense(9, 1, [
                        0.00,
                        0.00,
                        0.00,
                        0.25,
                        0.50,
                        0.75,
                        1.00,
                        1.00,
                        1.00])
                }
            },
            canInvertCdf: true,
            inverseCdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: [
                            -0.1,
                            0.0,
                            0.1,
                            0.2,
                            0.3,
                            0.4,
                            0.5,
                            0.6,
                            0.7,
                            0.8,
                            0.9,
                            1.0,
                            1.1],
                        numberOfRows: 13,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 12),
                    DoubleMatrix.Dense(13, 1, [
                        Double.NaN,
                        -1.000000e+00,
                        -8.000000e-01,
                        -6.000000e-01,
                        -4.000000e-01,
                        -2.000000e-01,
                        2.220446e-16,
                        2.000000e-01,
                        4.000000e-01,
                        6.000000e-01,
                        8.000000e-01,
                        1.000000e+00,
                        Double.NaN])
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableUniformDistribution00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableUniformDistribution00"/> class.</returns>
        public static TestableUniformDistribution00 Get()
        {
            return new TestableUniformDistribution00();
        }
    }
}
