// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// -1  <para /> 
    /// </summary>
    class TestableDoubleMatrix17 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix17" /> class.
        /// </summary>
        TestableDoubleMatrix17() : base(
                asColumnMajorDenseArray: [-1],
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
            //   -1 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix17"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix17"/> class.</returns>
        public static TestableDoubleMatrix17 Get()
        {
            return new TestableDoubleMatrix17();
        }
    }
}

