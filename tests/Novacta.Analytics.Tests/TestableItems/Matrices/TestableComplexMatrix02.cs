// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (2,2)  (0,0)   (1,1)   0 <para /> 
    /// (0,0)  (2,2)   (0,0)   0 
    /// </summary>
    class TestableComplexMatrix02 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix02" /> class.
        /// </summary>
        TestableComplexMatrix02() : base(
                asColumnMajorDenseArray:
                [
                    new(2, 2),
                    0,
                    0,
                    new(2, 2),
                    new(1, 1),
                    0,
                    0,
                    0
                ],
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
                upperBandwidth: 2,
                lowerBandwidth: 0)
        {
            // (2,2)  (0,0)   (1,1)   0 
            // (0,0)  (2,2)   (0,0)   0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix02"/> class.</returns>
        public static TestableComplexMatrix02 Get()
        {
            return new TestableComplexMatrix02();
        }
    }
}
