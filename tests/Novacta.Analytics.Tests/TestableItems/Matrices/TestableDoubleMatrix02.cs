// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 2 0 1 0 <para /> 
    /// 0 2 0 0 
    /// </summary>
    class TestableDoubleMatrix02 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix02" /> class.
        /// </summary>
        TestableDoubleMatrix02() : base(
                asColumnMajorDenseArray: [2, 0, 0, 2, 1, 0, 0, 0],
                numberOfRows: 2,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 0)
        {
            // 2 0 1 0 
            // 0 2 0 0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix02"/> class.</returns>
        public static TestableDoubleMatrix02 Get()
        {
            return new TestableDoubleMatrix02();
        }
    }
}
