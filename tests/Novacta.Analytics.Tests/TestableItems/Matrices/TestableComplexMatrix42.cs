// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0  0  0  0  0 <para /> 
    /// 0  0  0  0  0 <para />
    /// 0  0  0  0  0 <para />
    /// 0  0  0  0  0 <para />
    /// </summary>
    class TestableComplexMatrix42 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix42" /> class.
        /// </summary>
        TestableComplexMatrix42() : base(
                asColumnMajorDenseArray: new Complex[20],
                numberOfRows: 4,
                numberOfColumns: 5,
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
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix42"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix42"/> class.</returns>
        public static TestableComplexMatrix42 Get()
        {
            return new TestableComplexMatrix42();
        }
    }
}
