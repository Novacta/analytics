// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1    1    1    1    1  <para /> 
    /// 0    2    3    4    5  <para /> 
    /// 0    0    6   10   15  <para />
    /// 0    0    0   20   35  <para />
    /// 0    0    0    0   70  <para />
    /// </summary>
    class TestableDoubleMatrix25 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix25" /> class.
        /// </summary>
        TestableDoubleMatrix25() : base(
                asColumnMajorDenseArray: new double[25]
                {
                     1,  0,  0,  0,  0,
                     1,  2,  0,  0,  0,
                     1,  3,  6,  0,  0,
                     1,  4, 10, 20,  0,
                     1,  5, 15, 35, 70 },
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: true,
                isLowerHessenberg: false,
                isUpperTriangular: true,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 0)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix25"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix25"/> class.</returns>
        public static TestableDoubleMatrix25 Get()
        {
            return new TestableDoubleMatrix25();
        }
    }
}

