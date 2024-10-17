// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

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
                asColumnMajorDenseArray: [
                    0, 
                    0, 
                    0, 
                    0, 
                    0, 
                    0, 
                    0, 
                    new(2, 2), 
                    new(3, 3),
                    new(5, 5), 
                    0,
                    new(6, 6), 
                    0, 
                    new(7, 7), 
                    0, 
                    0,
                    new(4, 4), 
                    0, 
                    0, 
                    0 
                ],
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
