// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1 0  <para />
    /// 0 0
    /// </summary>
    class TestableDoubleMatrix09 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix09" /> class.
        /// </summary>
        TestableDoubleMatrix09() : base(
                asColumnMajorDenseArray: [1, 0, 0, 0],
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // 1 0  
            // 0 0
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix09"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix09"/> class.</returns>
        public static TestableDoubleMatrix09 Get()
        {
            return new TestableDoubleMatrix09();
        }

        public override DoubleMatrix AsSparse
        {
            get
            {
                var matrix = DoubleMatrix.Sparse(2, 2, 0);
                matrix[0, 0] = 1;
                matrix[0, 1] = 1;
                matrix[0, 1] = 0;

                return matrix;
            }
        }
    }

}
