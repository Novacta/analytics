// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0  (0,0)  (3,3)  (0,0)   (4,4) <para /> 
    /// 0  (0,0)  (5,5)  (7,7)   (0,0)   <para />
    /// 0  (0,0)  (0,0)  (0,0)   (0,0)   <para />
    /// 0  (2,2)  (6,6)  (0,0)   (0,0)   <para />
    /// </summary>
    class TestableComplexMatrix38 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix38" /> class.
        /// </summary>
        TestableComplexMatrix38() : base(
                asColumnMajorDenseArray: new Complex[20] {
                    0, 
                    0, 
                    0, 
                    0, 
                    0, 
                    0, 
                    0, 
                    new Complex(2, 2), 
                    new Complex(3, 3),
                    new Complex(5, 5), 
                    0,
                    new Complex(6, 6), 
                    0, 
                    new Complex(7, 7), 
                    0, 
                    0,
                    new Complex(4, 4), 
                    0, 
                    0, 
                    0 
                },
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
                lowerBandwidth: 2)
        {
            // 0  (0,0)  (3,3)  (0,0)   (4,4)
            // 0  (0,0)  (5,5)  (7,7)   (0,0)
            // 0  (0,0)  (0,0)  (0,0)   (0,0)
            // 0  (2,2)  (6,6)  (0,0)   (0,0)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix38"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix38"/> class.</returns>
        public static TestableComplexMatrix38 Get()
        {
            return new TestableComplexMatrix38();
        }
    }
}
