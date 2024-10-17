// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1.1 3.3 5.5 <para /> 
    /// 2.2 4.4 6.6 <para /> 
    /// </summary>
    class TestableDoubleMatrix08 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix08" /> class.
        /// </summary>
        TestableDoubleMatrix08() : base(
                asColumnMajorDenseArray: [1.1, 2.2, 3.3, 4.4, 5.5, 6.6],
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 1)
        {
            // 1.1 3.3 5.5
            // 2.2 4.4 6.6  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix08"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix08"/> class.</returns>
        public static TestableDoubleMatrix08 Get()
        {
            return new TestableDoubleMatrix08();
        }
    }
}
