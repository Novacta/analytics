// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1)  (3,3)  (5,5)  <para /> 
    /// (2,2)  (4,4)  (6,6)  <para />
    /// </summary>
    class TestableComplexMatrix34 : TestableComplexMatrix
    {
        static readonly Complex c11 = new(1, 1);
        static readonly Complex c22 = new(2, 2);
        static readonly Complex c33 = new(3, 3);
        static readonly Complex c44 = new(4, 4);
        static readonly Complex c55 = new(5, 5);
        static readonly Complex c66 = new(6, 6);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix34" /> class.
        /// </summary>
        TestableComplexMatrix34() : base(
                asColumnMajorDenseArray: new Complex[6] { c11, c22, c33, c44, c55, c66 },
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
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix34"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix34"/> class.</returns>
        public static TestableComplexMatrix34 Get()
        {
            return new TestableComplexMatrix34();
        }
    }
}
