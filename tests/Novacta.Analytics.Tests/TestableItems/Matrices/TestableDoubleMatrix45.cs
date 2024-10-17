// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 16   5   1   4  <para /> 
    ///  2  11   7  14  <para /> 
    ///  1  10   6  15  <para /> 
    /// 13   8  12   1  <para /> 
    ///  4   3   2   1  <para /> 
    /// </summary>
    class TestableDoubleMatrix45 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix45" /> class.
        /// </summary>
        TestableDoubleMatrix45() : base(
                asColumnMajorDenseArray: [
                   16,  2,  1, 13,  4,
                    5, 11, 10,  8,  3,
                    1,  7,  6, 12,  2,
                    4, 14, 15,  1,  1
                ],
                numberOfRows: 5,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 3,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix45"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix45"/> class.</returns>
        public static TestableDoubleMatrix45 Get()
        {
            return new TestableDoubleMatrix45();
        }
    }
}
