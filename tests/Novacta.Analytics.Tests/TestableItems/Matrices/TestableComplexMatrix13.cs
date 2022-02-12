// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1.1,1.1)   (0.0,0.0) <para /> 
    /// (0.0,0.0)   (4.4,4.4) <para /> 
    /// </summary>
    class TestableComplexMatrix13 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix13" /> class.
        /// </summary>
        TestableComplexMatrix13() : base(
                asColumnMajorDenseArray: new Complex[4] 
                { 
                    new Complex(1.1, 1.1), 
                    0.0, 
                    0.0, 
                    new Complex(4.4, 4.4)
                },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // (1.1,1.1)   (0.0,0.0)
            // (0.0,0.0)   (4.4,4.4)  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix13"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix13"/> class.</returns>
        public static TestableComplexMatrix13 Get()
        {
            return new TestableComplexMatrix13();
        }
    }

}
