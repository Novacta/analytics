// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (2,0)  <para /> 
    /// </summary>
    class TestableComplexMatrix59 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix59" /> class.
        /// </summary>
        TestableComplexMatrix59() : base(
                asColumnMajorDenseArray: [new(2, 0)],
                numberOfRows: 1,
                numberOfColumns: 1,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                isHermitian: true,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            //   (2,0) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix59"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix59"/> class.</returns>
        public static TestableComplexMatrix59 Get()
        {
            return new TestableComplexMatrix59();
        }
    }
}

