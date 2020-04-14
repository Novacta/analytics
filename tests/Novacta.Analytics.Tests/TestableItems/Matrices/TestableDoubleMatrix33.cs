// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///  0   0    0    0    0  <para /> 
    /// 40   2    3    4    5  <para /> 
    /// 30   3    6   10   15  <para />
    /// </summary>
    class TestableDoubleMatrix33 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix33" /> class.
        /// </summary>
        TestableDoubleMatrix33() : base(
                asColumnMajorDenseArray: new double[15]
                {
                    0, 40, 30,
                    0,  2,  3,
                    0,  3,  6,
                    0,  4, 10,
                    0,  5, 15 },
                numberOfRows: 3,
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
        /// Gets an instance of the <see cref="TestableDoubleMatrix33"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix33"/> class.</returns>
        public static TestableDoubleMatrix33 Get()
        {
            return new TestableDoubleMatrix33();
        }
    }
}

