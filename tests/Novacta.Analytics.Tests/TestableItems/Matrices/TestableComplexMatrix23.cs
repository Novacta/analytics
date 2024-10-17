// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1)  (3,3)  (5,5)  (7,7)  ( 9, 9)  <para /> 
    /// (2,2)  (4,4)  (6,6)  (8,8)  (10,10)   <para />
    /// </summary>
    class TestableComplexMatrix23 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix23" /> class.
        /// </summary>
        TestableComplexMatrix23() : base(
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
                    new(9, 9),
                    new(10, 10)
                ],
                numberOfRows: 2,
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
                lowerBandwidth: 1)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix23"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix23"/> class.</returns>
        public static TestableComplexMatrix23 Get()
        {
            return new TestableComplexMatrix23();
        }
    }
}

