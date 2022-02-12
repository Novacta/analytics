// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,0)      (2,-2) <para /> 
    /// (2,2)      (4, 0) <para /> 
    /// </summary>
    class TestableComplexMatrix63 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix63" /> class.
        /// </summary>
        TestableComplexMatrix63() : base(
                asColumnMajorDenseArray: new Complex[4]
                {
                    new Complex(1, 0),
                    new Complex(2, 2),
                    new Complex(2,-2),
                    new Complex(4, 0)
                },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: true,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
            // (1,0)      (2,-2)
            // (2,2)      (4, 0) 
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix63"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix63"/> class.</returns>
        public static TestableComplexMatrix63 Get()
        {
            return new TestableComplexMatrix63();
        }
    }
}
