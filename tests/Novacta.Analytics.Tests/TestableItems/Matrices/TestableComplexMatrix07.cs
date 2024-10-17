// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (2,2)  (0,0)  <para /> 
    /// (0,0)  (2,2)  <para /> 
    /// (0,0)  (0,0)  <para /> 
    /// (1,1)  (0,0)  <para /> 
    /// </summary>
    class TestableComplexMatrix07 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix07" /> class.
        /// </summary>
        TestableComplexMatrix07() : base(
                asColumnMajorDenseArray:
                [
                    new(2, 2),
                    0,
                    0,
                    new(1, 1),
                    0,
                    new(2, 2),
                    0,
                    0
                ],
                numberOfRows: 4,
                numberOfColumns: 2,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 3)
        {
            // (2,2)  (0,0)  
            // (0,0)  (2,2) 
            // (0,0)  (0,0)   
            // (1,1)  (0,0)  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix07"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix07"/> class.</returns>
        public static TestableComplexMatrix07 Get()
        {
            return new TestableComplexMatrix07();
        }
    }
}
