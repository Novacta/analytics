// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,1)      (2,2) <para /> 
    /// (2,2)      (0,4) <para /> 
    /// </summary>
    class TestableComplexMatrix66 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix66" /> class.
        /// </summary>
        TestableComplexMatrix66() : base(
                asColumnMajorDenseArray:
                [
                    new(0, 1),
                    new(2, 2),
                    new(2, 2),
                    new(0, 4)
                ],
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
            // (0,1)      (2,2)
            // (2,2)      (0,4) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix66"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix66"/> class.</returns>
        public static TestableComplexMatrix66 Get()
        {
            return new TestableComplexMatrix66();
        }
    }
}
