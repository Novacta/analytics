// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (50,50)   (1,1)   ( 1, 1)  <para /> 
    /// (40,40)   (2,2)   ( 3, 3)  <para /> 
    /// (30,30)   (3,3)   ( 6, 6)  <para />
    /// (20,20)   (4,4)   (10,10)  <para />
    /// (10,10)   (5,5)   (15,15)  <para />
    /// </summary>
    class TestableComplexMatrix30 : TestableComplexMatrix
    {
        static readonly Complex c11 = new(1, 1);
        static readonly Complex c22 = new(2, 2);
        static readonly Complex c33 = new(3, 3);
        static readonly Complex c44 = new(4, 4);
        static readonly Complex c55 = new(5, 5);
        static readonly Complex c66 = new(6, 6);
        static readonly Complex c1010 = new(10, 10);
        static readonly Complex c1515 = new(15, 15);
        static readonly Complex c2020 = new(20, 20);
        static readonly Complex c3030 = new(30, 30);
        static readonly Complex c4040 = new(40, 40);
        static readonly Complex c5050 = new(50, 50);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix30" /> class.
        /// </summary>
        TestableComplexMatrix30() : base(
                asColumnMajorDenseArray:
                [
                    c5050, c4040, c3030, c2020, c1010,
                    c11,   c22,   c33,   c44,   c55,
                    c11,   c33,   c66,   c1010, c1515 
                ],
                numberOfRows: 5,
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
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix30"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix30"/> class.</returns>
        public static TestableComplexMatrix30 Get()
        {
            return new TestableComplexMatrix30();
        }
    }
}

