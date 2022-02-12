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
    /// 50   1    1    1    1  <para /> 
    /// 40   2    3    4    5  <para /> 
    /// 30   3    6   10   15  <para />
    /// 20   4   10   20   35  <para />
    /// 10   5   15   35   70  <para />
    ///
    /// l / r  =           <para />
    /// (-3.41,-3.41) ( 6.83, 6.83) ( -4.83, -4.83) (2.41,2.41) (-0.48,-0.48) <para /> 
    ///	(-6.50,-6.50) (15.00,15.00) (-13.00,-13.00) (6.50,6.50) (-1.30,-1.30) <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ComplexMatrixState}" />
    class RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                  -3.416666666666639,
                  -6.499999999999926,
                   6.833333333333233,
                  14.999999999999734,
                  -4.833333333333199,
                 -12.999999999999645,
                   2.416666666666587,
                   6.499999999999791,
                  -0.483333333333315,
                  -1.299999999999954
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableDoubleMatrix28.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsNoPatternedSquareComplexMatrixDoubleMatrixDivision();
        }
    }
}
