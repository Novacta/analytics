// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,1)      (0,0) <para /> 
    /// (2,2)      (0,4) <para /> 
    /// </summary>
    class TestableComplexMatrix72 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix72" /> class.
        /// </summary>
        TestableComplexMatrix72() : base(
                asColumnMajorDenseArray: new Complex[4]
                {
                    new Complex( 0, 1),
                    new Complex( 2, 2),
                    0,
                    new Complex( 0, 4)
                },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: true,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 1)
        {
            // (0,1)      (0,0)
            // (2,2)      (0,4) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix72"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix72"/> class.</returns>
        public static TestableComplexMatrix72 Get()
        {
            return new TestableComplexMatrix72();
        }
    }
}
