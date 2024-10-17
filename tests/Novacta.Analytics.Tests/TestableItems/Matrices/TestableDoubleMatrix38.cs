// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0  0  3  0  4 <para /> 
    /// 0  0  5  7  0 <para />
    /// 0  0  0  0  0 <para />
    /// 0  2  6  0  0 <para />
    /// </summary>
    class TestableDoubleMatrix38 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix38" /> class.
        /// </summary>
        TestableDoubleMatrix38() : base(
                asColumnMajorDenseArray: [
                    0, 0, 0, 0, 0, 0, 0, 2, 3, 5, 0, 6, 0, 7, 0, 0, 4, 0, 0, 0 ],
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix38"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix38"/> class.</returns>
        public static TestableDoubleMatrix38 Get()
        {
            return new TestableDoubleMatrix38();
        }
    }
}
