// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 2 0 0 1 <para /> 
    /// 0 2 0 0 
    /// </summary>
    class TestableDoubleMatrix03 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix03" /> class.
        /// </summary>
        TestableDoubleMatrix03() : base(
                asColumnMajorDenseArray: [2, 0, 0, 2, 0, 0, 1, 0],
                numberOfRows: 2,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 3,
                lowerBandwidth: 0)
        {
            // 2 0 0 1 
            // 0 2 0 0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix03"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix03"/> class.</returns>
        public static TestableDoubleMatrix03 Get()
        {
            return new TestableDoubleMatrix03();
        }
    }
}
