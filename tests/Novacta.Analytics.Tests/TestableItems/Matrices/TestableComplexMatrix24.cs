// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1)    (0,0)   ( 0, 0)   ( 0, 0)   ( 0, 0)  <para /> 
    /// (1,1)    (2,2)   ( 0, 0)   ( 0, 0)   ( 0, 0)  <para /> 
    /// (1,1)    (3,3)   ( 6, 6)   ( 0, 0)   ( 0, 0)  <para />
    /// (1,1)    (4,4)   (10,10)   (20,20)   ( 0, 0)  <para />
    /// (1,1)    (5,5)   (15,15)   (35,35)   (70,70)  <para />
    /// </summary>
    class TestableComplexMatrix24 : TestableComplexMatrix
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
        static readonly Complex c3535 = new(35, 35);
        static readonly Complex c7070 = new(70, 70);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix24" /> class.
        /// </summary>
        TestableComplexMatrix24() : base(
                asColumnMajorDenseArray:
                [
                     c11,  c11,  c11,  c11,   c11,
                     0,    c22,  c33,  c44,   c55,
                     0,    0,    c66,  c1010, c1515,
                     0,    0,    0,    c2020, c3535,
                     0,    0,    0,    0,     c7070
                ],
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: true,
                isUpperTriangular: false,
                isLowerTriangular: true,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 4)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix24"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix24"/> class.</returns>
        public static TestableComplexMatrix24 Get()
        {
            return new TestableComplexMatrix24();
        }
    }
}

