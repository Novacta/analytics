// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (-5,-5) <para /> 
    /// (-4,-4) <para /> 
    /// (-3,-3) <para /> 
    /// </summary>
    class TestableComplexMatrix20 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix20" /> class.
        /// </summary>
        TestableComplexMatrix20() : base(
                asColumnMajorDenseArray:
                [
                    new(-5, -5),
                    new(-4, -4),
                    new(-3, -3)
                ],
                numberOfRows: 3,
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
                lowerBandwidth: 2)
        {
            // (-5,-5) 
            // (-4,-4)
            // (-3,-3)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix20"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix20"/> class.</returns>
        public static TestableComplexMatrix20 Get()
        {
            return new TestableComplexMatrix20();
        }
    }
}
