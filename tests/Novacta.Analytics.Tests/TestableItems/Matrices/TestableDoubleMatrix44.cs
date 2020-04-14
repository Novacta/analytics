// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// -5  -4  -3 <para /> 
    /// </summary>
    class TestableDoubleMatrix44 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix44" /> class.
        /// </summary>
        TestableDoubleMatrix44() : base(
                asColumnMajorDenseArray: new double[2] { -5, -4 },
                numberOfRows: 1,
                numberOfColumns: 2,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 0)
        {
            // -5  -4  -3
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix44"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix44"/> class.</returns>
        public static TestableDoubleMatrix44 Get()
        {
            return new TestableDoubleMatrix44();
        }
    }
}
