// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (16,16)   ( 2, 2)    ( 1, 1)    (13,13)   (4,4) <para /> 
    /// ( 5, 5)   (11,11)    (10,10)    ( 8, 8)   (3,3) <para />
    /// ( 1, 1)   ( 7, 7)    ( 6, 6)    (12,12)   (2,2) <para />
    /// ( 4, 4)   (14,14)    (15,15)    ( 1, 1)   (1,1) <para />
    /// </summary>
    class TestableComplexMatrix40 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[20]
            {
                16,  5,  1,  4,
                 2, 11,  7, 14,
                 1, 10,  6, 15,
                13,  8, 12,  1,
                 4,  3,  2,  1
            };
            var asColumnMajorDenseArray = new Complex[20];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix40" /> class.
        /// </summary>
        TestableComplexMatrix40() : base(
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
                upperBandwidth: 4,
                lowerBandwidth: 3)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix40"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix40"/> class.</returns>
        public static TestableComplexMatrix40 Get()
        {
            return new TestableComplexMatrix40();
        }
    }
}
