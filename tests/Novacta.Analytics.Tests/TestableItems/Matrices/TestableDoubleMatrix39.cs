// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0   0   3   0   4   0   0   0   1   0 <para /> 
    /// 0   0   5   7   0   0   2   1   0   0 <para />
    /// 3   0   1   0   0   0   0   0   0   1 <para />
    /// </summary>
    class TestableDoubleMatrix39 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix39" /> class.
        /// </summary>
        TestableDoubleMatrix39() : base(
                asColumnMajorDenseArray: [
                    0, 0, 3,
                    0, 0, 0,
                    3, 5, 1,
                    0, 7, 0,
                    4, 0, 0,
                    0, 0, 0,
                    0, 2, 0,
                    0, 1, 0,
                    1, 0, 0,
                    0, 0, 1
                ],
                numberOfRows: 3,
                numberOfColumns: 10,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 8,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix39"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix39"/> class.</returns>
        public static TestableDoubleMatrix39 Get()
        {
            return new TestableDoubleMatrix39();
        }
    }
}
