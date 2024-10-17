// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.SD
{
    class TestableSpectralDecomposition00
        : TestableSpectralDecomposition<TestableDoubleMatrix, DoubleMatrix>
    {
        static readonly TestableDoubleMatrix testableMatrix;
        static readonly DoubleMatrix values;
        static readonly DoubleMatrix vectors;

        static TestableSpectralDecomposition00()
        {
            testableMatrix = new TestableDoubleMatrix(
                asColumnMajorDenseArray: [
                    2,
                    2,
                    0,
                    2,
                    0,
                    0,
                    0,
                    0,
                    5
                ],
                numberOfRows: 3,
                numberOfColumns: 3,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 1);

            values = DoubleMatrix.Dense(3, 3);
            values[0, 0] = -1.2360679774997896;
            values[1, 1] = 3.23606797749979;
            values[2, 2] = 5;

            vectors = DoubleMatrix.Dense(3, 3, [
                 0.5257311121191335,
                -0.8506508083520399,
                 0,
                -0.8506508083520399,
                -0.5257311121191335,
                 0,
                 0,
                 0,
                 1
            ]);
        }

        TestableSpectralDecomposition00()
            : base(
                  testableMatrix,
                  values,
                  vectorsIfLower: vectors,
                  vectorsIfUpper: vectors)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableSpectralDecomposition00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableSpectralDecomposition00"/> class.</returns>
        public static TestableSpectralDecomposition00 Get()
        {
            return new TestableSpectralDecomposition00();
        }
    }
}