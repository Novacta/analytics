// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (2,2)   (1,1)   0   0 <para /> 
    /// (0,0)   (2,2)   0   0 
    /// </summary>
    class TestableComplexMatrix01 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix01" /> class.
        /// </summary>
        TestableComplexMatrix01() : base(
                asColumnMajorDenseArray: new Complex[8] 
                {
                    new Complex(2, 2),
                    0,
                    new Complex(1, 1),
                    new Complex(2, 2),
                    0,
                    0,
                    0,
                    0
                },
                numberOfRows: 2,
                numberOfColumns: 4,
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
            // (2,2)   (1,1)   0   0 
            // (0,0)   (2,2)   0   0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix01"/> class.</returns>
        public static TestableComplexMatrix01 Get()
        {
            return new TestableComplexMatrix01();
        }
    }
}
