// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.MDS
{
    class TestableNonMetricMultidimensionalScaling05
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

        static TestableNonMetricMultidimensionalScaling05()
        {
            dissimilarities = new TestableDoubleMatrix(
                asColumnMajorDenseArray: [
                    0,
                    1,
                    7,
                   20,
                    1,
                    0,
                    3,
                    8,
                    7,
                    3,
                    0,
                    5,
                   20,
                    8,
                    5,
                    0],
                numberOfRows: 4,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 3,
                lowerBandwidth: 3,
                rowNames: new System.Collections.Generic.Dictionary<int, string>()
                {
                     { 0, "A"},
                     { 1, "B"},
                     { 2, "C"},
                     { 3, "D"}
                });

            configurationDimension = null;

            minkowskiDistanceOrder = 2;

            maximumNumberOfIterations = 100;

            terminationTolerance = 1e-5;

            configuration = DoubleMatrix.Dense(4, 3, [
               -1.221135330,    0.00965935769,    6.02316527e-24,
               -0.478418519,    0.24102409800,    1.24051841e-22,
                0.256430316,   -0.27021269900,   -1.39928207e-22,
                1.443123530,    0.01952924370,    9.85320082e-24],
                StorageOrder.RowMajor);

            configuration.SetRowName(0, "A");
            configuration.SetRowName(1, "B");
            configuration.SetRowName(2, "C");
            configuration.SetRowName(3, "D");
            
            stress = 0.0;

            hasConverged = true;
        }

        TestableNonMetricMultidimensionalScaling05()
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
        /// <see cref="TestableNonMetricMultidimensionalScaling05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableNonMetricMultidimensionalScaling05"/> class.</returns>
        public static TestableNonMetricMultidimensionalScaling05 Get()
        {
            return new TestableNonMetricMultidimensionalScaling05();
        }
    }
}