// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///  0   2   0   2   0 <para /> 
    ///  4   0  -1   0   0 <para />
    /// -2   0   2   0   3 <para />
    ///  0   5   0   1   0 <para />
    /// </summary>
    class TestableDoubleMatrix41 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix41" /> class.
        /// </summary>
        TestableDoubleMatrix41() : base(
                asColumnMajorDenseArray: [
                    0,  4, -2, 0,
                    2,  0,  0, 5,
                    0, -1,  2, 0,
                    2,  0,  0, 1,
                    0,  0,  3, 0
                ],
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 3,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix41"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix41"/> class.</returns>
        public static TestableDoubleMatrix41 Get()
        {
            return new TestableDoubleMatrix41();
        }
    }
}
