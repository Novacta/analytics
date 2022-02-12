// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1.1,1.1) (3.3,3.3) (5.5,5.5) <para /> 
    /// (2.2,2.2) (4.4,4.4) (6.6,6.6) <para /> 
    /// </summary>
    class TestableComplexMatrix08 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix08" /> class.
        /// </summary>
        TestableComplexMatrix08() : base(
                asColumnMajorDenseArray: new Complex[6] 
                { 
                    new Complex(1.1, 1.1), 
                    new Complex(2.2, 2.2),
                    new Complex(3.3, 3.3),
                    new Complex(4.4, 4.4),
                    new Complex(5.5, 5.5),
                    new Complex(6.6, 6.6)
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
            // (1.1,1.1) (3.3,3.3) (5.5,5.5)
            // (2.2,2.2) (4.4,4.4) (6.6,6.6)  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix08"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix08"/> class.</returns>
        public static TestableComplexMatrix08 Get()
        {
            return new TestableComplexMatrix08();
        }
    }
}
