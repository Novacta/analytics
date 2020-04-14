// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 2 1 0 0 <para /> 
    /// 0 2 0 0 
    /// </summary>
    class TestableDoubleMatrix01 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix01" /> class.
        /// </summary>
        TestableDoubleMatrix01() : base(
                asColumnMajorDenseArray: new double[8] { 2, 0, 1, 2, 0, 0, 0, 0 },
                numberOfRows: 2,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 0)
        {
            // 2 1 0 0 
            // 0 2 0 0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix01"/> class.</returns>
        public static TestableDoubleMatrix01 Get()
        {
            return new TestableDoubleMatrix01();
        }
    }
}
