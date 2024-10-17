// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,0)      (0,0) <para /> 
    /// (2,2)      (4,0) <para /> 
    /// </summary>
    class TestableComplexMatrix71 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix71" /> class.
        /// </summary>
        TestableComplexMatrix71() : base(
                asColumnMajorDenseArray:
                [
                    new(1, 0),
                    new(2, 2),
                    0,
                    new(4, 0)
                ],
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: true,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 1)
        {
            // (1,0)      (0,0)
            // (2,2)      (4,0) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix71"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix71"/> class.</returns>
        public static TestableComplexMatrix71 Get()
        {
            return new TestableComplexMatrix71();
        }
    }
}
