// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (4,4) (3,3) <para /> 
    /// (1,1) (2,2) <para /> 
    /// </summary>
    class TestableComplexMatrix54 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix54" /> class.
        /// </summary>
        TestableComplexMatrix54() : base(
                asColumnMajorDenseArray:
                [
                    new(4, 4), 
                    new(1, 1), 
                    new(3, 3),
                    new(2, 2)
                ],
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix54"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix54"/> class.</returns>
        public static TestableComplexMatrix54 Get()
        {
            return new TestableComplexMatrix54();
        }
    }

}
