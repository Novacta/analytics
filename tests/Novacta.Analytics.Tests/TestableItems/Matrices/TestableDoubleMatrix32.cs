// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0    1    1    1    1  <para /> 
    /// 0    2    3    4    5  <para /> 
    /// 0    3    6   10   15  <para />
    /// 0    4   10   20   35  <para />
    /// 0    5   15   35   70  <para />
    /// </summary>
    class TestableDoubleMatrix32 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix32" /> class.
        /// </summary>
        TestableDoubleMatrix32() : base(
                asColumnMajorDenseArray: new double[25]
                {
                    0, 0,  0,  0,  0,
                    1, 2,  3,  4,  5,
                    1, 3,  6, 10, 15,
                    1, 4, 10, 20, 35,
                    1, 5, 15, 35, 70 },
                numberOfRows: 5,
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
        /// Gets an instance of the <see cref="TestableDoubleMatrix32"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix32"/> class.</returns>
        public static TestableDoubleMatrix32 Get()
        {
            return new TestableDoubleMatrix32();
        }
    }
}
