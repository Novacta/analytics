// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1)    (4,4)    (7,7)    (10,10)    (13,13) <para /> 
    /// (2,2)    (5,5)    (8,8)    (11,11)    (14,14) <para /> 
    /// (3,3)    (6,6)    (9,9)    (12,12)    (15,15) <para /> 
    /// </summary>
    class TestableComplexMatrix53 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var asColumnMajorDenseArray = new Complex[15];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(i + 1, i + 1);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="TestableComplexMatrix53" /> class.
        /// </summary>
        TestableComplexMatrix53() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 3,
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
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix53"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix53"/> class.</returns>
        public static TestableComplexMatrix53 Get()
        {
            return new TestableComplexMatrix53();
        }
    }
}
