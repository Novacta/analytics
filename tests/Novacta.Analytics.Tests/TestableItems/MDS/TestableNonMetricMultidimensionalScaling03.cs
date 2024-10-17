// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.MDS
{
    class TestableNonMetricMultidimensionalScaling03
        : TestableNonMetricMultidimensionalScaling<TestableDoubleMatrix>
    {
        static readonly TestableDoubleMatrix dissimilarities;
        static readonly int? configurationDimension;
        static readonly int minkowskiDistanceOrder;
        static readonly int maximumNumberOfIterations;
        static readonly double terminationTolerance;
        static readonly DoubleMatrix configuration;
        static readonly double stress;
        static readonly bool hasConverged;

        static TestableNonMetricMultidimensionalScaling03()
        {
            dissimilarities = new TestableDoubleMatrix(
                asColumnMajorDenseArray: [
                      0,
                    159,
                    247,
                    131,
                    197,
                    159,
                      0,
                    230,
                     97,
                     89,
                    247,
                    230,
                      0,
                    309,
                    317,
                    131,
                     97,
                    309,
                      0,
                     68,
                    197,
                     89,
                    317,
                     68,
                      0],
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 4);

            configurationDimension = 1;

            minkowskiDistanceOrder = 2;

            maximumNumberOfIterations = 100;

            terminationTolerance = 1e-5;

            configuration = DoubleMatrix.Dense(5, 1, [
                0.499952971,
                0.500002149,
               -2.000000000,
                0.500020837,
                0.500024041]);

            stress = 7.453811870496764E-06;

            hasConverged = true;
        }

        TestableNonMetricMultidimensionalScaling03()
            : base(
                  dissimilarities,
                  configurationDimension,
                  minkowskiDistanceOrder,
                  maximumNumberOfIterations,
                  terminationTolerance,
                  configuration,
                  stress,
                  hasConverged)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableNonMetricMultidimensionalScaling03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableNonMetricMultidimensionalScaling03"/> class.</returns>
        public static TestableNonMetricMultidimensionalScaling03 Get()
        {
            return new TestableNonMetricMultidimensionalScaling03();
        }
    }
}