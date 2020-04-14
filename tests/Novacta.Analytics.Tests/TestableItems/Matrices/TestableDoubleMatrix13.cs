// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1.1 0.0 <para /> 
    /// 0.0 4.4 <para /> 
    /// </summary>
    class TestableDoubleMatrix13 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix13" /> class.
        /// </summary>
        TestableDoubleMatrix13() : base(
                asColumnMajorDenseArray: new double[4] { 1.1, 0.0, 0.0, 4.4 },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // 1.1 0.0
            // 0.0 4.4  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix13"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix13"/> class.</returns>
        public static TestableDoubleMatrix13 Get()
        {
            return new TestableDoubleMatrix13();
        }
    }

}
