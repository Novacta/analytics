// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// -5.0 -3.0 -1.0 <para /> 
    /// -4.0 -2.0 -0.0 <para /> 
    /// </summary>
    class TestableDoubleMatrix18 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix18" /> class.
        /// </summary>
        TestableDoubleMatrix18() : base(
                asColumnMajorDenseArray: [-5, -4, -3, -2, -1, 0],
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
            // -5.0 -3.0 -1.0
            // -4.0 -2.0 -0.0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix18"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix18"/> class.</returns>
        public static TestableDoubleMatrix18 Get()
        {
            return new TestableDoubleMatrix18();
        }
    }
}
