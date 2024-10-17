// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1) (4,4) (7,7) <para /> 
    /// (2,2) (5,5) (8,8) <para /> 
    /// (3,3) (6,6) (9,9) <para /> 
    /// </summary>
    class TestableComplexMatrix14 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix14" /> class.
        /// </summary>
        TestableComplexMatrix14() : base(
                asColumnMajorDenseArray:
                [
                    new(1, 1),
                    new(2, 2),
                    new(3, 3),
                    new(4, 4),
                    new(5, 5),
                    new(6, 6),
                    new(7, 7),
                    new(8, 8),
                    new(9, 9)
                ],
                numberOfRows: 3,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 2,
                lowerBandwidth: 2)
        {
            // (1,1) (4,4) (7,7)
            // (2,2) (5,5) (8,8)
            // (3,3) (6,6) (9,9)   
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix14"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix14"/> class.</returns>
        public static TestableComplexMatrix14 Get()
        {
            return new TestableComplexMatrix14();
        }
    }
}
