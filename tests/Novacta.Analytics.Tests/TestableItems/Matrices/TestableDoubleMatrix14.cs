// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1 4 7 <para /> 
    /// 2 5 8 <para /> 
    /// 3 6 9 <para /> 
    /// </summary>
    class TestableDoubleMatrix14 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix14" /> class.
        /// </summary>
        TestableDoubleMatrix14() : base(
                asColumnMajorDenseArray: new double[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                numberOfRows: 3,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 2)
        {
            // 1 4 7  
            // 2 5 8  
            // 3 6 9  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix14"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix14"/> class.</returns>
        public static TestableDoubleMatrix14 Get()
        {
            return new TestableDoubleMatrix14();
        }
    }
}
