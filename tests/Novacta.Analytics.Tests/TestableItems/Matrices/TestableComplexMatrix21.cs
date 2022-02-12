// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (-5,-5)  (-4,-4)  (-3,-3) <para /> 
    /// </summary>
    class TestableComplexMatrix21 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix21" /> class.
        /// </summary>
        TestableComplexMatrix21() : base(
                asColumnMajorDenseArray: new Complex[3] 
                {
                    new Complex(-5, -5),
                    new Complex(-4, -4),
                    new Complex(-3, -3)
                },
                numberOfRows: 1,
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
                lowerBandwidth: 0)
        {
            // (-5, -5) (-4, -4) (-3, -3)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix21"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix21"/> class.</returns>
        public static TestableComplexMatrix21 Get()
        {
            return new TestableComplexMatrix21();
        }
    }
}
