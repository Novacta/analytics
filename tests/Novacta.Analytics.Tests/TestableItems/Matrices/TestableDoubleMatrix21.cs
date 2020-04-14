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
    class TestableDoubleMatrix21 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix21" /> class.
        /// </summary>
        TestableDoubleMatrix21() : base(
                asColumnMajorDenseArray: new double[3] { -5, -4, -3 },
                numberOfRows: 1,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 0)
        {
            // -5  -4  -3
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix21"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix21"/> class.</returns>
        public static TestableDoubleMatrix21 Get()
        {
            return new TestableDoubleMatrix21();
        }
    }
}
