// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///  ( 0, 0)     ( 4, 4)     (-7,-7) <para /> 
    ///  (-4,-4)     ( 0, 0)     ( 8, 8) <para /> 
    ///  ( 7, 7)     (-8,-8)     ( 0, 0)  <para /> 
    /// </summary>
    class TestableComplexMatrix15 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix15" /> class.
        /// </summary>
        TestableComplexMatrix15() : base(
                asColumnMajorDenseArray: new Complex[9] 
                {
                    0,
                    new Complex(-4, -4),
                    new Complex(7, 7),
                    new Complex(4, 4),
                    0,
                    new Complex(-8, -8),
                    new Complex(-7, -7),
                    new Complex(8, 8),
                    0 
                },
                numberOfRows: 3,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: true,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 2,
                lowerBandwidth: 2)
        {
            //  ( 0, 0)     ( 4, 4)     (-7,-7)
            //  (-4,-4)     ( 0, 0)     ( 8, 8)
            //  ( 7, 7)     (-8,-8)     ( 0, 0)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix15"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix15"/> class.</returns>
        public static TestableComplexMatrix15 Get()
        {
            return new TestableComplexMatrix15();
        }
    }
}
