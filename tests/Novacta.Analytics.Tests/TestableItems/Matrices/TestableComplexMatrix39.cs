// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,0)   0   (3,3)   (0,0)   (4,4)   0   (0,0)   (0,0)   (1,1)   (0,0) <para /> 
    /// (0,0)   0   (5,5)   (7,7)   (0,0)   0   (2,2)   (1,1)   (0,0)   (0,0) <para />
    /// (3,3)   0   (1,1)   (0,0)   (0,0)   0   (0,0)   (0,0)   (0,0)   (1,1) <para />
    /// </summary>
    class TestableComplexMatrix39 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix39" /> class.
        /// </summary>
        TestableComplexMatrix39() : base(
                asColumnMajorDenseArray: new Complex[30] {
                    new Complex(0, 0), new Complex(0, 0), new Complex(3, 3),
                    new Complex(0, 0), new Complex(0, 0), new Complex(0, 0),
                    new Complex(3, 3), new Complex(5, 5), new Complex(1, 1),
                    new Complex(0, 0), new Complex(7, 7), new Complex(0, 0),
                    new Complex(4, 4), new Complex(0, 0), new Complex(0, 0),
                    new Complex(0, 0), new Complex(0, 0), new Complex(0, 0),
                    new Complex(0, 0), new Complex(2, 2), new Complex(0, 0),
                    new Complex(0, 0), new Complex(1, 1), new Complex(0, 0),
                    new Complex(1, 1), new Complex(0, 0), new Complex(0, 0),
                    new Complex(0, 0), new Complex(0, 0), new Complex(1, 1)
                },
                numberOfRows: 3,
                numberOfColumns: 10,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 8,
                lowerBandwidth: 2)
        {
            // (0,0)   0   (3,3)   (0,0)   (4,4)   0   (0,0)   (0,0)   (1,1)   (0,0)
            // (0,0)   0   (5,5)   (7,7)   (0,0)   0   (2,2)   (1,1)   (0,0)   (0,0)
            // (3,3)   0   (1,1)   (0,0)   (0,0)   0   (0,0)   (0,0)   (0,0)   (1,1)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix39"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix39"/> class.</returns>
        public static TestableComplexMatrix39 Get()
        {
            return new TestableComplexMatrix39();
        }
    }
}
