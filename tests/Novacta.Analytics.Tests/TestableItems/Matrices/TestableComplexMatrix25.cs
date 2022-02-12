// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1)    (1,1)    (1,1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (0,0)    (2,2)    (3,3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (0,0)    (0,0)    (6,6)   (10,10)   (15,15)  <para />
    /// (0,0)    (0,0)    (0,0)   (20,20)   (35,35)  <para />
    /// (0,0)    (0,0)    (0,0)   ( 0, 0)   (70,70)  <para />
    /// </summary>
    class TestableComplexMatrix25 : TestableComplexMatrix
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
        /// Initializes a new instance of the <see cref="TestableComplexMatrix25" /> class.
        /// </summary>
        TestableComplexMatrix25() : base(
                asColumnMajorDenseArray: new Complex[25]
                {
                     c11,  0,    0,      0,      0,
                     c11,  c22,  0,      0,      0,
                     c11,  c33,  c66,    0,      0,
                     c11,  c44,  c1010,  c2020,  0,
                     c11,  c55,  c1515,  c3535,  c7070 },
                numberOfRows: 5,
                numberOfColumns: 5,
                isUpperHessenberg: true,
                isLowerHessenberg: false,
                isUpperTriangular: true,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 4,
                lowerBandwidth: 0)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix25"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix25"/> class.</returns>
        public static TestableComplexMatrix25 Get()
        {
            return new TestableComplexMatrix25();
        }
    }
}

