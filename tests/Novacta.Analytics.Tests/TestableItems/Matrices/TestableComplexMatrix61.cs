// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,2)  <para /> 
    /// </summary>
    class TestableComplexMatrix61 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix61" /> class.
        /// </summary>
        TestableComplexMatrix61() : base(
                asColumnMajorDenseArray: [new(0, 0)],
                numberOfRows: 1,
                numberOfColumns: 1,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: true,
                isHermitian: true,
                isSkewHermitian: true,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            //   (0,0) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix61"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix61"/> class.</returns>
        public static TestableComplexMatrix61 Get()
        {
            return new TestableComplexMatrix61();
        }
    }
}

