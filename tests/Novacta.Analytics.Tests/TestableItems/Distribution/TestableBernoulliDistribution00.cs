// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Distribution
{
    /// <summary>
    /// Provides methods to test implementations of the Bernoulli
    /// distribution having success probability equal to <c>.48</c>.
    /// </summary>
    class TestableBernoulliDistribution00 : TestableProbabilityDistribution
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableBernoulliDistribution00" /> class.
        /// </summary>
        TestableBernoulliDistribution00() : base(
            probabilityDistribution:
                new BernoulliDistribution(successProbability: 0.48),
            mean: 0.48,
            variance: (0.48)*(1.0 - .48),
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[5]{
                             -0.1,
                             0.0,
                             .2,
                             1.0,
                             1.1},
                        numberOfRows: 5,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 4),
                    DoubleMatrix.Dense(5, 1, new Double[5]{
                        0.0,
                        .52,
                        0.0,
                        .48,
                        0.0})
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[5]{
                             -0.1,
                             0.0,
                             .2,
                             1.0,
                             1.1},
                        numberOfRows: 5,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 4),
                    DoubleMatrix.Dense(5, 1, new Double[5]{
                        0.0,
                        .52,
                        .52,
                        1.0,
                        1.0})
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableBernoulliDistribution00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableBernoulliDistribution00"/> class.</returns>
        public static TestableBernoulliDistribution00 Get()
        {
            return new TestableBernoulliDistribution00();
        }
    }
}
