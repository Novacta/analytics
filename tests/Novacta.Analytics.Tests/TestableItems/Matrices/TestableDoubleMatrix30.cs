// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 50   1    1  <para /> 
    /// 40   2    3  <para /> 
    /// 30   3    6  <para />
    /// 20   4   10  <para />
    /// 10   5   15  <para />
    /// </summary>
    class TestableDoubleMatrix30 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix30" /> class.
        /// </summary>
        TestableDoubleMatrix30() : base(
                asColumnMajorDenseArray:
                [
                   50, 40, 30, 20, 10,
                    1,  2,  3,  4,  5,
                    1,  3,  6, 10, 15 ],
                numberOfRows: 5,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix30"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix30"/> class.</returns>
        public static TestableDoubleMatrix30 Get()
        {
            return new TestableDoubleMatrix30();
        }
    }
}

