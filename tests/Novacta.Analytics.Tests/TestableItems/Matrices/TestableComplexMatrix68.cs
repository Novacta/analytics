// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,1)      (-2,2) <para /> 
    /// (2,2)      ( 4,4) <para /> 
    /// </summary>
    class TestableComplexMatrix68 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix68" /> class.
        /// </summary>
        TestableComplexMatrix68() : base(
                asColumnMajorDenseArray: new Complex[4]
                {
                    new Complex( 0, 1),
                    new Complex( 2, 2),
                    new Complex(-2, 2),
                    new Complex( 4, 4)
                },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
            // (0,1)      (-2,2)
            // (2,2)      ( 4,4) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix68"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix68"/> class.</returns>
        public static TestableComplexMatrix68 Get()
        {
            return new TestableComplexMatrix68();
        }
    }
}
