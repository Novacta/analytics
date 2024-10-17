// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,1)      (-2,2) <para /> 
    /// (2,2)      ( 0,4) <para /> 
    /// </summary>
    class TestableComplexMatrix64 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix64" /> class.
        /// </summary>
        TestableComplexMatrix64() : base(
                asColumnMajorDenseArray:
                [
                    new( 0, 1),
                    new( 2, 2),
                    new(-2, 2),
                    new( 0, 4)
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
                isSkewHermitian: true,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
            // (0,1)      (-2,2)
            // (2,2)      ( 0,4) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix64"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix64"/> class.</returns>
        public static TestableComplexMatrix64 Get()
        {
            return new TestableComplexMatrix64();
        }
    }
}
