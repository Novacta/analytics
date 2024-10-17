// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1    0    0    0    0  <para /> 
    /// 1    2    0    0    0  <para /> 
    /// 1    3    6    0    0  <para />
    /// 1    4   10   20    0  <para />
    /// 1    5   15   35   70  <para />
    /// </summary>
    class TestableDoubleMatrix24 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix24" /> class.
        /// </summary>
        TestableDoubleMatrix24() : base(
                asColumnMajorDenseArray:
                [
                     1,  1,  1,  1,  1, 
                     0,  2,  3,  4,  5,
                     0,  0,  6, 10, 15,
                     0,  0,  0, 20, 35,
                     0,  0,  0,  0, 70 ],
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: true,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix24"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix24"/> class.</returns>
        public static TestableDoubleMatrix24 Get()
        {
            return new TestableDoubleMatrix24();
        }
    }
}

