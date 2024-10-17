// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.SVD
{
    class TestableSingularValueDecomposition01
        : TestableSingularValueDecomposition<TestableComplexMatrix, ComplexMatrix>
    {
        static readonly TestableComplexMatrix testableMatrix;
        static readonly DoubleMatrix values;
        static readonly ComplexMatrix leftVectors;
        static readonly ComplexMatrix conjugateTransposedRightVectors;

        static TestableSingularValueDecomposition01()
        {
            testableMatrix = new TestableComplexMatrix(
                asColumnMajorDenseArray: [
                    new( 1, -2),
                    0,
                    new(-4, -1),
                    0,
                    new(-2,  3),
                    new( 5,  4),
                ],
                numberOfRows: 3,
                numberOfColumns: 2,
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

            values = DoubleMatrix.Dense(3, 2);
            values[0, 0] = 8.298837152328408;
            values[1, 1] = 2.670075264694562;

            var real_u = new double[9] {
                 0.059137125746635,
                 0.059651327824406,
                -0.888736353866709,
                 0.326316462593674,
                -0.104430852095699,
                -0.163488534128387,
                 0.478154269689203,
                 0.302925389042980,
                 0.305397976503591
            };
            var imag_u = new double[9] {
                -0.118274251493269,
                -0.373814987699610,
                -0.222184088466677,
                -0.652632925187348,
                 0.654433339799714,
                -0.040872133532097,
                -0.470606340131404,
                -0.570746426727529,
                 0.197775043937648
            };

            var u = new Complex[9];
            for (int i = 0; i < 9; i++)
            { 
                u[i] = new Complex(real_u[i], imag_u[i]);
            }
            leftVectors = ComplexMatrix.Dense(3, 3, u);

            var real_vt = new double[4] {
                 0.490769376228087,
                 0.871289515233998,
                -0.792058648855841,
                 0.446141176082724
            };
            var imag_vt = new double[4] {
                                 0,
                                 0,
                -0.363026880725594,
                 0.204481372371248
            };

            var vt = new Complex[4];
            for (int i = 0; i < 4; i++)
            {
                vt[i] = new Complex(real_vt[i], imag_vt[i]);
            }

            conjugateTransposedRightVectors = 
                ComplexMatrix.Dense(2, 2, vt);
        }

        TestableSingularValueDecomposition01()
            : base(
                  testableMatrix,
                  values,
                  leftVectors,
                  conjugateTransposedRightVectors)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableSingularValueDecomposition01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableSingularValueDecomposition01"/> class.</returns>
        public static TestableSingularValueDecomposition01 Get()
        {
            return new TestableSingularValueDecomposition01();
        }
    }
}
