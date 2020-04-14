// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 2 0  <para /> 
    /// 0 2  <para /> 
    /// 0 0  <para /> 
    /// 0 0  <para /> 
    /// </summary>
    class TestableDoubleMatrix04 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix04" /> class.
        /// </summary>
        TestableDoubleMatrix04() : base(
                asColumnMajorDenseArray: new double[8] { 2, 0, 0, 0, 0, 2, 0, 0 },
                numberOfRows: 4,
                numberOfColumns: 2,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // 2 0  
            // 0 2 
            // 0 0   
            // 0 0  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix04"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix04"/> class.</returns>
        public static TestableDoubleMatrix04 Get()
        {
            return new TestableDoubleMatrix04();
        }
    }
}
