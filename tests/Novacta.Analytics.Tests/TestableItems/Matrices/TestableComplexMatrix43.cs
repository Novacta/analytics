// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (-5,-5) <para /> 
    /// (-4,-4) <para /> 
    /// </summary>
    class TestableComplexMatrix43 : TestableComplexMatrix
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
        /// Initializes a new instance of the <see cref="TestableComplexMatrix43" /> class.
        /// </summary>
        TestableComplexMatrix43() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 2,
                numberOfColumns: 1,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 1)
        {
            // -5.0 
            // -4.0
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix43"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix43"/> class.</returns>
        public static TestableComplexMatrix43 Get()
        {
            return new TestableComplexMatrix43();
        }
    }
}
