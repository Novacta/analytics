// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1.0  0.0  -2.0 <para /> 
    /// 0.0 -1.0   0.0 
    /// </summary>
    class TestableDoubleMatrix50 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix50" /> class.
        /// </summary>
        TestableDoubleMatrix50() : base(
                asColumnMajorDenseArray: [1, 0, 0, -1, -2, 0],
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 0)
        {
            // 1.0  0.0  -2.0 
            // 0.0 -1.0   0.0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix50"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix50"/> class.</returns>
        public static TestableDoubleMatrix50 Get()
        {
            return new TestableDoubleMatrix50();
        }
    }
}
