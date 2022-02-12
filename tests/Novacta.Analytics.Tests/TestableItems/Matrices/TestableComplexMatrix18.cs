// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (-5,-5) (-3,-3) (-1,-1) <para /> 
    /// (-4,-4) (-2,-2) (-0,-0) <para /> 
    /// </summary>
    class TestableComplexMatrix18 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix18" /> class.
        /// </summary>
        TestableComplexMatrix18() : base(
                asColumnMajorDenseArray: new Complex[6] 
                { 
                    new Complex(-5, -5), 
                    new Complex(-4, -4), 
                    new Complex(-3, -3),
                    new Complex(-2, -2),
                    new Complex(-1, -1), 
                    0 
                },
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
                upperBandwidth: 2,
                lowerBandwidth: 1)
        {
            // -5.0 -3.0 -1.0
            // -4.0 -2.0 -0.0 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix18"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix18"/> class.</returns>
        public static TestableComplexMatrix18 Get()
        {
            return new TestableComplexMatrix18();
        }
    }
}
