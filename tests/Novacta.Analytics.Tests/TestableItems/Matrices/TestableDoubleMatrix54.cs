// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 4 3 <para /> 
    /// 1 2 <para /> 
    /// </summary>
    class TestableDoubleMatrix54 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix54" /> class.
        /// </summary>
        TestableDoubleMatrix54() : base(
                asColumnMajorDenseArray: [4, 1, 3, 2],
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix54"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix54"/> class.</returns>
        public static TestableDoubleMatrix54 Get()
        {
            return new TestableDoubleMatrix54();
        }
    }

}
