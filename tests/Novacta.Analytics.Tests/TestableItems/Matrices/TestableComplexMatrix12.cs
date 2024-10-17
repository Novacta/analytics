// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1.1,1.1)  (0.0,0.0) <para /> 
    /// (2.2,2.2)  (4.4,4.4) <para /> 
    /// </summary>
    class TestableComplexMatrix12 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix12" /> class.
        /// </summary>
        TestableComplexMatrix12() : base(
                asColumnMajorDenseArray:
                [
                    new(1.1, 1.1),
                    new(2.2, 2.2), 
                    0.0, 
                    new(4.4, 4.4) 
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
            // (1.1,1.1)  (0.0,0.0)
            // (2.2,2.2)  (4.4,4.4)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix12"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix12"/> class.</returns>
        public static TestableComplexMatrix12 Get()
        {
            return new TestableComplexMatrix12();
        }
    }

}
