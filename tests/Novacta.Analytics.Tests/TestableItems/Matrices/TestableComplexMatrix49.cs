// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///  (0,0)   0   (0,0)   0   0 <para /> 
    ///  (0,0)   0   (1,1)   0   0 <para />
    ///  (2,2)   0   (0,0)   0   0 <para />
    ///  (0,0)   0   (0,0)   0   0 <para />
    /// </summary>
    class TestableComplexMatrix49 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[20] 
            {
                0,  0,  2,  0,
                0,  0,  0,  0,
                0,  1,  0,  0,
                0,  0,  0,  0,
                0,  0,  0,  0
            };
            var asColumnMajorDenseArray = new Complex[20];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix49" /> class.
        /// </summary>
        TestableComplexMatrix49() : base(
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
                upperBandwidth: 1,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix49"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix49"/> class.</returns>
        public static TestableComplexMatrix49 Get()
        {
            return new TestableComplexMatrix49();
        }
    }
}
