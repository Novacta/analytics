// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// -5 <para /> 
    /// -4 <para /> 
    /// </summary>
    class TestableDoubleMatrix43 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix43" /> class.
        /// </summary>
        TestableDoubleMatrix43() : base(
                asColumnMajorDenseArray: [-5, -4],
                numberOfRows: 2,
                numberOfColumns: 1,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 1)
        {
            // -5.0 
            // -4.0
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix43"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix43"/> class.</returns>
        public static TestableDoubleMatrix43 Get()
        {
            return new TestableDoubleMatrix43();
        }
    }
}
