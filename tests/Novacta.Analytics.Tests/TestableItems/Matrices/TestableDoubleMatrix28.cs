// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 50   1    1    1    1  <para /> 
    /// 40   2    3    4    5  <para /> 
    /// 30   3    6   10   15  <para />
    /// 20   4   10   20   35  <para />
    /// 10   5   15   35   70  <para />
    /// </summary>
    class TestableDoubleMatrix28 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix28" /> class.
        /// </summary>
        TestableDoubleMatrix28() : base(
                asColumnMajorDenseArray: new double[25]
                {
                   50, 40, 30, 20, 10,
                    1,  2,  3,  4,  5,
                    1,  3,  6, 10, 15,
                    1,  4, 10, 20, 35,
                    1,  5, 15, 35, 70 },
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix28"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix28"/> class.</returns>
        public static TestableDoubleMatrix28 Get()
        {
            return new TestableDoubleMatrix28();
        }
    }
}

