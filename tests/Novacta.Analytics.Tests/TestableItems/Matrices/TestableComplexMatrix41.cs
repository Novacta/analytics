// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// ( 0, 0)   (2,2)   ( 0, 0)   (2,2)   (0,0) <para /> 
    /// ( 4, 4)   (0,0)   (-1,-1)   (0,0)   (0,0) <para />
    /// (-2,-2)   (0,0)   ( 2, 2)   (0,0)   (3,3) <para />
    /// ( 0, 0)   (5,5)   ( 0, 0)   (1,1)   (0,0) <para />
    /// </summary>
    class TestableComplexMatrix41 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[20]
            {
                0,  4, -2, 0,
                2,  0,  0, 5,
                0, -1,  2, 0,
                2,  0,  0, 1,
                0,  0,  3, 0
            };
            var asColumnMajorDenseArray = new Complex[20];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix41" /> class.
        /// </summary>
        TestableComplexMatrix41() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 3,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix41"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix41"/> class.</returns>
        public static TestableComplexMatrix41 Get()
        {
            return new TestableComplexMatrix41();
        }
    }
}
