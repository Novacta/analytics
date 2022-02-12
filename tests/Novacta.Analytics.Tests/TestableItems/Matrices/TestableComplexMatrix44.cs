// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (-5,-5)  (-4,-4)  (-3,-3) <para /> 
    /// </summary>
    class TestableComplexMatrix44 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[2]
            {
                -5, -4
            };
            var asColumnMajorDenseArray = new Complex[2];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix44" /> class.
        /// </summary>
        TestableComplexMatrix44() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 1,
                numberOfColumns: 2,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 0)
        {
            // -5  -4  -3
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix44"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix44"/> class.</returns>
        public static TestableComplexMatrix44 Get()
        {
            return new TestableComplexMatrix44();
        }
    }
}
