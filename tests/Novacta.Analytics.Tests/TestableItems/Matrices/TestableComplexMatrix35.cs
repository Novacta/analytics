// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,0)  (2,2)  (0,0)    <para /> 
    /// (3,3)  (0,0)  NaN      <para />
    /// </summary>
    class TestableComplexMatrix35 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix35" /> class.
        /// </summary>
        TestableComplexMatrix35() : base(
                asColumnMajorDenseArray: new Complex[6] 
                { 
                    0,
                    new Complex(3, 3),
                    new Complex(2, 2),
                    0,
                    0,
                    Complex.NaN 
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
                upperBandwidth: 1,
                lowerBandwidth: 1)
        {
            // (0,0)  (2,2)  (0,0)
            // (3,3)  (0,0)  NaN  
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix35"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix35"/> class.</returns>
        public static TestableComplexMatrix35 Get()
        {
            return new TestableComplexMatrix35();
        }
    }
}
