// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.SD
{
    class TestableSpectralDecomposition01
        : TestableSpectralDecomposition<TestableComplexMatrix, ComplexMatrix>
    {
        static readonly TestableComplexMatrix testableMatrix;
        static readonly DoubleMatrix values;
        static readonly ComplexMatrix vectorsIfLower;
        static readonly ComplexMatrix vectorsIfUpper;

        static TestableSpectralDecomposition01()
        {
            double[] realArray = new double[9] {
                2,
                2,
                0,
                2,
                0,
                0,
                0,
                0,
                5
            };
            double[] imaginaryArray = new double[9] {
                0,
                1,
                0,
               -1,
                0,
                0,
                0,
                0,
                0
            };

            var complexArray = new Complex[9];
            for (int i = 0; i < 9; i++)
            {
                complexArray[i] = new Complex(realArray[i], imaginaryArray[i]);
            }

            testableMatrix = new TestableComplexMatrix(
                asColumnMajorDenseArray: complexArray,
                numberOfRows: 3,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 2);

            values = DoubleMatrix.Dense(3, 3);
            values[0, 0] = -1.449489742783178;
            values[1, 1] = 3.449489742783178;
            values[2, 2] = 5;

            vectorsIfLower = ComplexMatrix.Dense(3, 3, new Complex[9] {
                -0.543944717,
                new Complex(0.750532688, 0.375266344),
                0,
                -0.839121055171381,
                new Complex(-0.486518945, -0.243259472),
                0,
                0,
                0,
                1
            });

            vectorsIfUpper = ComplexMatrix.Dense(3, 3, new Complex[9] {
                new Complex(0.486518945, -0.243259472),
                -0.839121055171381,
                0,
                new Complex(0.750532688, -0.375266344),
                0.543944717,
                0,
                0,
                0,
                1
            });
        }

        TestableSpectralDecomposition01()
            : base(
                  testableMatrix,
                  values,
                  vectorsIfLower,
                  vectorsIfUpper)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableSpectralDecomposition01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableSpectralDecomposition01"/> class.</returns>
        public static TestableSpectralDecomposition01 Get()
        {
            return new TestableSpectralDecomposition01();
        }
    }
}
