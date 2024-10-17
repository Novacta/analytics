// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1  3  5  <para /> 
    /// 2  4  6  <para />
    /// </summary>
    class TestableDoubleMatrix34 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix34" /> class.
        /// </summary>
        TestableDoubleMatrix34() : base(
                asColumnMajorDenseArray: [1, 2, 3, 4, 5, 6],
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 1)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix34"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix34"/> class.</returns>
        public static TestableDoubleMatrix34 Get()
        {
            return new TestableDoubleMatrix34();
        }
    }
}
