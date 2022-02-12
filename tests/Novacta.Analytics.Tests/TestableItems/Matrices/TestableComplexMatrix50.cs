// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1) ( 0, 0)  (-2,-2) <para /> 
    /// (0,0) (-1,-1)  ( 0, 0) 
    /// </summary>
    class TestableComplexMatrix50 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[6]
            {
                1, 0, 0, -1, -2, 0
            };
            var asColumnMajorDenseArray = new Complex[6];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix50" /> class.
        /// </summary>
        TestableComplexMatrix50() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 2,
                lowerBandwidth: 0)
        {
            // 1.0  0.0  -2.0 
            // 0.0 -1.0   0.0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix50"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix50"/> class.</returns>
        public static TestableComplexMatrix50 Get()
        {
            return new TestableComplexMatrix50();
        }
    }
}
