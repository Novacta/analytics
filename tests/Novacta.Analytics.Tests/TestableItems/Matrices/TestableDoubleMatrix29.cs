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
    /// </summary>
    class TestableDoubleMatrix29 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix29" /> class.
        /// </summary>
        TestableDoubleMatrix29() : base(
                asColumnMajorDenseArray: new double[15]
                {
                   50, 40, 30,
                    1,  2,  3,
                    1,  3,  6,
                    1,  4, 10,
                    1,  5, 15 },
                numberOfRows: 3,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix29"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix29"/> class.</returns>
        public static TestableDoubleMatrix29 Get()
        {
            return new TestableDoubleMatrix29();
        }
    }
}

