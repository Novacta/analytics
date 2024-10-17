// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.MDS
{
    class TestableClassicalMultidimensionalScaling01
        : TestableClassicalMultidimensionalScaling<TestableDoubleMatrix>
    {
        static readonly TestableDoubleMatrix proximities;
        static readonly int? configurationDimension;
        static readonly DoubleMatrix configuration;
        static readonly double goodnessOfFit;

        static TestableClassicalMultidimensionalScaling01()
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
                lowerBandwidth: 3,
                rowNames: new System.Collections.Generic.Dictionary<int, string>()
                { 
                     { 0, "A"},
                     { 1, "B"},
                     { 2, "C"},
                     { 3, "D"}
                });

            configurationDimension = 2;

            configuration = DoubleMatrix.Dense(4, 2, [
                -9.8247441,
                -1.2722188,
                 0.9202902,
                10.1766727,
               -0.08835289,
                1.01441620,
               -1.06378036,
                0.13771705
            ]);

            configuration.SetRowName(0, "A");
            configuration.SetRowName(1, "B");
            configuration.SetRowName(2, "C");
            configuration.SetRowName(3, "D");

            goodnessOfFit = 0.751388732453942;
        }

        TestableClassicalMultidimensionalScaling01()
            : base(
                  proximities,
                  configurationDimension,
                  configuration,
                  goodnessOfFit)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableClassicalMultidimensionalScaling01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableClassicalMultidimensionalScaling01"/> class.</returns>
        public static TestableClassicalMultidimensionalScaling01 Get()
        {
            return new TestableClassicalMultidimensionalScaling01();
        }
    }
}