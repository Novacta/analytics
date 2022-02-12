// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (2,2)    (0,0)  <para /> 
    /// (0,0)    (2,2)  <para /> 
    /// (1,1)    (0,0)  <para /> 
    /// (0,0)    (0,0)  <para /> 
    /// </summary>
    class TestableComplexMatrix06 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix06" /> class.
        /// </summary>
        TestableComplexMatrix06() : base(
                asColumnMajorDenseArray: new Complex[8] 
                {
                    new Complex(2, 2),
                    0, 
                    new Complex(1, 1), 
                    0, 
                    0, 
                    new Complex(2, 2), 
                    0, 
                    0
                },
                numberOfRows: 4,
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
                lowerBandwidth: 2)
        {
            // (2,2)    (0,0) 
            // (0,0)    (2,2)
            // (1,1)    (0,0) 
            // (0,0)    (0,0)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix06"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix06"/> class.</returns>
        public static TestableComplexMatrix06 Get()
        {
            return new TestableComplexMatrix06();
        }
    }
}
