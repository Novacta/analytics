// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.


using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0  0  0  <para /> 
    /// 0  0  0  <para />
    /// </summary>
    class TestableComplexMatrix36 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix36" /> class.
        /// </summary>
        TestableComplexMatrix36() : base(
                asColumnMajorDenseArray: new Complex[6] { 0, 0, 0, 0, 0, 0 },
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // 0  0  0
            // 0  0  0
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix36"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix36"/> class.</returns>
        public static TestableComplexMatrix36 Get()
        {
            return new TestableComplexMatrix36();
        }
    }
}
