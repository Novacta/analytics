// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// ( 0, 0)   (0,0)   (0,0)   ( 0, 0)   ( 0, 0)  <para /> 
    /// (40,40)   (2,2)   (3,3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (30,30)   (3,3)   (6,6)   (10,10)   (15,15)  <para />
    /// </summary>
    class TestableComplexMatrix33 : TestableComplexMatrix
    {
        static readonly Complex c22 = new(2, 2);
        static readonly Complex c33 = new(3, 3);
        static readonly Complex c44 = new(4, 4);
        static readonly Complex c55 = new(5, 5);
        static readonly Complex c66 = new(6, 6);
        static readonly Complex c1010 = new(10, 10);
        static readonly Complex c1515 = new(15, 15);
        static readonly Complex c3030 = new(30, 30);
        static readonly Complex c4040 = new(40, 40);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix33" /> class.
        /// </summary>
        TestableComplexMatrix33() : base(
                asColumnMajorDenseArray: new Complex[15]
                {
                    0,  c4040, c3030,
                    0,  c22,   c33,
                    0,  c33,   c66,
                    0,  c44,   c1010,
                    0,  c55,   c1515
                },
                numberOfRows: 3,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 3,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix33"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix33"/> class.</returns>
        public static TestableComplexMatrix33 Get()
        {
            return new TestableComplexMatrix33();
        }
    }
}

