// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Distribution
{
    /// <summary>
    /// Provides methods to test implementations of a finite, discrete
    /// distribution having values
    /// <para /> 
    ///  2  <para /> 
    /// -1  <para /> 
    ///  3  <para /> 
    /// -8  <para /> 
    /// -4  <para /> 
    /// and corresponding probabilities
    /// <para /> 
    /// 1 / 10  <para /> 
    /// 3 / 10  <para /> 
    /// 2 / 10  <para /> 
    /// 1 / 10  <para /> 
    /// 3 / 10  <para /> 
    /// </summary>
    class TestableFiniteDiscreteDistribution00 : TestableProbabilityDistribution
    {
        static readonly DoubleMatrix values = DoubleMatrix.Dense(5, 1,
            new double[5] { 2, -1, 3, -8, -4 });

        static readonly DoubleMatrix masses = DoubleMatrix.Dense(5, 1,
            new double[5] { .1, .3, .2, .1, .3 });

        const double mean = -1.5;
        const double variance = 11.45;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableFiniteDiscreteDistribution00" /> class.
        /// </summary>
        TestableFiniteDiscreteDistribution00() : base(
            probabilityDistribution:
                new FiniteDiscreteDistribution(values: values, masses: masses),
            mean: mean,
            variance: variance,
            pdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[2]{
                             values[2],
                             -1.1},
                        numberOfRows: 2,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 1),
                    DoubleMatrix.Dense(2, 1, new Double[2]{
                        masses[2],
                        0.0})
                }
            },
            cdfPartialGraph: new Dictionary<TestableDoubleMatrix, DoubleMatrix>()
            {
                {
                    new TestableDoubleMatrix(
                        asColumnMajorDenseArray: new double[3]{
                            -9.0,
                            values[4],
                            2.1},
                        numberOfRows: 3,
                        numberOfColumns: 1,
                        isUpperHessenberg: false,
                        isLowerHessenberg: false,
                        isUpperTriangular: false,
                        isLowerTriangular: false,
                        isSymmetric: false,
                        isSkewSymmetric: false,
                        upperBandwidth: 0,
                        lowerBandwidth: 2),
                    DoubleMatrix.Dense(3, 1, new Double[3]{
                        0.0,
                        masses[3] + masses[4],
                        masses[3] + masses[4] + masses[1] + masses[0]})
                }
            })
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableFiniteDiscreteDistribution00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableFiniteDiscreteDistribution00"/> class.</returns>
        public static TestableFiniteDiscreteDistribution00 Get()
        {
            return new TestableFiniteDiscreteDistribution00();
        }
    }
}
