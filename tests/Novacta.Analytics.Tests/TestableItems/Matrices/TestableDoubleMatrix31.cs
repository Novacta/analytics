// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0    1    1    1    1  <para /> 
    /// 1    2    3    4    5  <para /> 
    /// 1    3    6   10   15  <para />
    /// 1    4   10   20   35  <para />
    /// 1    5   15   35   70  <para />
    /// </summary>
    class TestableDoubleMatrix31 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix31" /> class.
        /// </summary>
        TestableDoubleMatrix31() : base(
                asColumnMajorDenseArray:
                [
                    0, 1,  1,  1,  1,
                    1, 2,  3,  4,  5,
                    1, 3,  6, 10, 15,
                    1, 4, 10, 20, 35,
                    1, 5, 15, 35, 70 ],
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix31"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix31"/> class.</returns>
        public static TestableDoubleMatrix31 Get()
        {
            return new TestableDoubleMatrix31();
        }
    }
}

