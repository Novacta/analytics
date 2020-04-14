// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///  0   0   0   0   0 <para /> 
    ///  0   0   1   0   0 <para />
    ///  2   0   0   0   0 <para />
    ///  0   0   0   0   0 <para />
    /// </summary>
    class TestableDoubleMatrix49 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix49" /> class.
        /// </summary>
        TestableDoubleMatrix49() : base(
                asColumnMajorDenseArray: new double[20] {
                    0,  0,  2, 0,
                    0,  0,  0, 0,
                    0,  1,  0, 0,
                    0,  0,  0, 0,
                    0,  0,  0, 0
                },
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 1,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix49"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix49"/> class.</returns>
        public static TestableDoubleMatrix49 Get()
        {
            return new TestableDoubleMatrix49();
        }
    }
}
