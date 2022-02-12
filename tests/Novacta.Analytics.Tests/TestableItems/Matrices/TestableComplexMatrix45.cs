// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (16,16)  ( 5, 5)  ( 1, 1)  ( 4, 4)  <para /> 
    /// ( 2, 2)  (11,11)  ( 7, 7)  (14,14)  <para /> 
    /// ( 1, 1)  (10,10)  ( 6, 6)  (15,15)  <para /> 
    /// (13,13)  ( 8, 8)  (12,12)  ( 1, 1)  <para /> 
    /// ( 4, 4)  ( 3, 3)  ( 2, 2)  ( 1, 1)  <para /> 
    /// </summary>
    class TestableComplexMatrix45 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[20]
            {
                16,  2,  1, 13,  4,
                5,  11, 10,  8,  3,
                1,   7,  6, 12,  2,
                4,  14, 15,  1,  1
            };
            var asColumnMajorDenseArray = new Complex[20];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix45" /> class.
        /// </summary>
        TestableComplexMatrix45() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 5,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 3,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix45"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix45"/> class.</returns>
        public static TestableComplexMatrix45 Get()
        {
            return new TestableComplexMatrix45();
        }
    }
}
