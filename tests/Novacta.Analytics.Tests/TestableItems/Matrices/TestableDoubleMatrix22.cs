// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 10  <para /> 
    /// </summary>
    class TestableDoubleMatrix22 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix22" /> class.
        /// </summary>
        TestableDoubleMatrix22() : base(
                asColumnMajorDenseArray: [10],
                numberOfRows: 1,
                numberOfColumns: 1,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            //   10 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix22"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix22"/> class.</returns>
        public static TestableDoubleMatrix22 Get()
        {
            return new TestableDoubleMatrix22();
        }
    }
}

