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
    /// -3 <para /> 
    /// </summary>
    class TestableDoubleMatrix20 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix20" /> class.
        /// </summary>
        TestableDoubleMatrix20() : base(
                asColumnMajorDenseArray: new double[3] { -5, -4, -3 },
                numberOfRows: 3,
                numberOfColumns: 1,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 2)
        {
            // -5.0 
            // -4.0
            // -3.0
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix20"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix20"/> class.</returns>
        public static TestableDoubleMatrix20 Get()
        {
            return new TestableDoubleMatrix20();
        }
    }
}
