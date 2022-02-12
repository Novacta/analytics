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
    /// 1  3  5  7   9    <para /> 
    /// 2  4  6  8  10    <para /> 
    ///
    /// r   =                  <para /> 
    /// (50,50)   (1,1)   ( 1, 1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (40,40)   (2,2)   ( 3, 3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (30,30)   (3,3)   ( 6, 6)   (10,10)   (15,15)  <para />
    /// (20,20)   (4,4)   (10,10)   (20,20)   (35,35)  <para />
    /// (10,10)   (5,5)   (15,15)   (35,35)   (70,70)  <para />
    ///
    /// l / r  =           <para />
    /// (-1.7083,1.7083)  (3.4167,-3.4167)  (-2.4167,2.4167)  (1.2083,-1.2083)  (-0.2417,0.2417) <para /> 
    ///	(-3.2500,3.2500)  (7.5000,-7.5000)  (-6.5000,6.5000)  (3.2500,-3.2500)  (-0.6500,0.6500) <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision :
        TestableDoubleMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -1.708333333333313,
                -3.249999999999936,
                 3.416666666666595,
                 7.499999999999771,
                -2.416666666666573,
                -6.499999999999697,
                 1.208333333333278,
                 3.249999999999821,
                -0.241666666666654,
                -0.649999999999960
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableComplexMatrix28.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision Get()
        {
            return new RightIsNoPatternedSquareDoubleMatrixComplexMatrixDivision();
        }
    }
}
