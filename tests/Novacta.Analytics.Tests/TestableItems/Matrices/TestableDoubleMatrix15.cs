// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///  0  4 -7 <para /> 
    /// -4  0  8 <para /> 
    ///  7 -8  0 <para /> 
    /// </summary>
    class TestableDoubleMatrix15 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix15" /> class.
        /// </summary>
        TestableDoubleMatrix15() : base(
                asColumnMajorDenseArray: new double[9] { 0, -4, 7, 4, 0, -8, -7, 8, 0 },
                numberOfRows: 3,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: true,
                upperBandwidth: 2,
                lowerBandwidth: 2)
        {
            //  0  4 -7 
            // -4  0  8 
            //  7 -8  0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix15"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix15"/> class.</returns>
        public static TestableDoubleMatrix15 Get()
        {
            return new TestableDoubleMatrix15();
        }
    }
}
