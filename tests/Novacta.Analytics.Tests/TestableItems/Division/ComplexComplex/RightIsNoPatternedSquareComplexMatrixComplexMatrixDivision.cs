// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (1,1)  (3,3)  (5,5)  (7,7)  ( 9, 9)    <para /> 
    /// (2,2)  (4,4)  (6,6)  (8,8)  (10,10)    <para /> 
    ///
    /// r   =                  <para /> 
    /// (50,50)   (1,1)   ( 1, 1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (40,40)   (2,2)   ( 3, 3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (30,30)   (3,3)   ( 6, 6)   (10,10)   (15,15)  <para />
    /// (20,20)   (4,4)   (10,10)   (20,20)   (35,35)  <para />
    /// (10,10)   (5,5)   (15,15)   (35,35)   (70,70)  <para />
    ///
    /// l / r  =           <para />
    /// -3.4167    6.8333   -4.8333    2.4167   -0.4833 <para /> 
    ///	-6.5000   15.0000  -13.0000    6.5000   -1.3000 <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -3.4167,
                -6.5000,
                    6.8333,
                15,
                -4.8333,
                -13,
                    2.4167,
                    6.5,
                -0.4833,
                -1.3
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], 0);
            }
            return asColumnMajorDenseArray;
        }

        RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableComplexMatrix28.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsNoPatternedSquareComplexMatrixComplexMatrixDivision();
        }
    }
}
