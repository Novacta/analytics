// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.


namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0  0  0  <para /> 
    /// 0  0  0  <para />
    /// </summary>
    class TestableDoubleMatrix36 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix36" /> class.
        /// </summary>
        TestableDoubleMatrix36() : base(
                asColumnMajorDenseArray: [0, 0, 0, 0, 0, 0],
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix36"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix36"/> class.</returns>
        public static TestableDoubleMatrix36 Get()
        {
            return new TestableDoubleMatrix36();
        }
    }
}
