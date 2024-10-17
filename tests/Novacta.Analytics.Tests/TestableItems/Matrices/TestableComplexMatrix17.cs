// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (-1,-1)  <para /> 
    /// </summary>
    class TestableComplexMatrix17 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix17" /> class.
        /// </summary>
        TestableComplexMatrix17() : base(
                asColumnMajorDenseArray: [new(-1, -1)],
                numberOfRows: 1,
                numberOfColumns: 1,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            //   (-1,-1) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix17"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix17"/> class.</returns>
        public static TestableComplexMatrix17 Get()
        {
            return new TestableComplexMatrix17();
        }
    }
}

