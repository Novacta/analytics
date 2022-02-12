// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.SVD
{
    class TestableSingularValueDecomposition00
        : TestableSingularValueDecomposition<TestableDoubleMatrix, DoubleMatrix>
    {
        static readonly TestableDoubleMatrix testableMatrix;
        static readonly DoubleMatrix values;
        static readonly DoubleMatrix leftVectors;
        static readonly DoubleMatrix conjugateTransposedRightVectors;

        static TestableSingularValueDecomposition00()
        {
            testableMatrix = new TestableDoubleMatrix(
                asColumnMajorDenseArray: new double[20] {
                    1,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    2,
                    0,
                    3,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    2,
                    0,
                    0,
                    0
                },
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 3);

            values = DoubleMatrix.Dense(4, 5);
            values[0, 0] = 3;
            values[1, 1] = Math.Sqrt(5);
            values[2, 2] = 2;

            leftVectors = DoubleMatrix.Dense(4, 4);
            leftVectors[0, 1] = -1;
            leftVectors[1, 0] = -1;
            leftVectors[2, 3] =  1;
            leftVectors[3, 2] = -1;

            conjugateTransposedRightVectors 
                = DoubleMatrix.Dense(5, 5);
            conjugateTransposedRightVectors[0, 2] = -1;
            conjugateTransposedRightVectors[1, 0] = -Math.Sqrt(.2);
            conjugateTransposedRightVectors[1, 4] = -Math.Sqrt(.8);
            conjugateTransposedRightVectors[2, 1] = -1;
            conjugateTransposedRightVectors[3, 3] = 1;
            conjugateTransposedRightVectors[4, 0] = -Math.Sqrt(.8);
            conjugateTransposedRightVectors[4, 4] = Math.Sqrt(.2);
        }

        TestableSingularValueDecomposition00()
            : base(
                  testableMatrix,
                  values,
                  leftVectors,
                  conjugateTransposedRightVectors)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableSingularValueDecomposition00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableSingularValueDecomposition00"/> class.</returns>
        public static TestableSingularValueDecomposition00 Get()
        {
            return new TestableSingularValueDecomposition00();
        }
    }
}
