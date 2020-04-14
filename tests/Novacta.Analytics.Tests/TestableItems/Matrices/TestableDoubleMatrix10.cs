// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1.1 2.2 <para /> 
    /// 2.2 4.4 <para /> 
    /// </summary>
    class TestableDoubleMatrix10 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix10" /> class.
        /// </summary>
        TestableDoubleMatrix10() : base(
                asColumnMajorDenseArray: new double[4] { 1.1, 2.2, 2.2, 4.4 },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
            // 1.1 2.2
            // 2.2 4.4  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix10"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix10"/> class.</returns>
        public static TestableDoubleMatrix10 Get()
        {
            return new TestableDoubleMatrix10();
        }
    }
}
