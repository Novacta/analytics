// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1  3  5  7  9  <para /> 
    /// 2  4  6  8 10   <para />
    /// </summary>
    class TestableDoubleMatrix23 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix23" /> class.
        /// </summary>
        TestableDoubleMatrix23() : base(
                asColumnMajorDenseArray: new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                numberOfRows: 2,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 1)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix23"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix23"/> class.</returns>
        public static TestableDoubleMatrix23 Get()
        {
            return new TestableDoubleMatrix23();
        }
    }
}

