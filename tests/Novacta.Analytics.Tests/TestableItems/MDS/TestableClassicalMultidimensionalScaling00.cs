// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.MDS
{
    class TestableClassicalMultidimensionalScaling00
        : TestableClassicalMultidimensionalScaling<TestableDoubleMatrix>
    {
        static readonly TestableDoubleMatrix proximities;
        static readonly int? configurationDimension;
        static readonly DoubleMatrix configuration;
        static readonly double goodnessOfFit;

        static TestableClassicalMultidimensionalScaling00()
        {
            proximities = new TestableDoubleMatrix(
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
                    0
                ],
                numberOfRows: 4,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 3,
                lowerBandwidth: 3);

            configurationDimension = 1;

            configuration = DoubleMatrix.Dense(4, 1, [
                -9.8247441,
                -1.2722188,
                 0.9202902,
                10.1766727
            ]);

            goodnessOfFit = 0.743361023797779;
        }

        TestableClassicalMultidimensionalScaling00()
            : base(
                  proximities,
                  configurationDimension,
                  configuration,
                  goodnessOfFit)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableClassicalMultidimensionalScaling00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableClassicalMultidimensionalScaling00"/> class.</returns>
        public static TestableClassicalMultidimensionalScaling00 Get()
        {
            return new TestableClassicalMultidimensionalScaling00();
        }
    }
}