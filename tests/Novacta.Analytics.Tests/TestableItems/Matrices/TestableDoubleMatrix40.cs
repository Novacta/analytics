// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 16     2     1    13   4 <para /> 
    ///  5    11    10     8   3 <para />
    ///  1     7     6    12   2 <para />
    ///  4    14    15     1   1 <para />
    /// </summary>
    class TestableDoubleMatrix40 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix40" /> class.
        /// </summary>
        TestableDoubleMatrix40() : base(
                asColumnMajorDenseArray: [
                   16,  5,  1,  4,
                    2, 11,  7, 14,
                    1, 10,  6, 15,
                   13,  8, 12,  1,
                    4,  3,  2,  1
                ],
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 3)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix40"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix40"/> class.</returns>
        public static TestableDoubleMatrix40 Get()
        {
            return new TestableDoubleMatrix40();
        }
    }
}
