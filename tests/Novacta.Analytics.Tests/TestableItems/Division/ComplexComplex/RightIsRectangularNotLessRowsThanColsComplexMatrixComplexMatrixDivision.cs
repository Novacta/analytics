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
    /// l =          <para />
    /// (1,1)  (3,3)  (5,5)      <para /> 
    /// (2,2)  (4,4)  (6,6)      <para /> 
    ///
    /// r   =       <para /> 
    /// (50,50)   (1,1)   ( 1, 1)    <para /> 
    /// (40,40)   (2,2)   ( 3, 3)    <para /> 
    /// (30,30)   (3,3)   ( 6, 6)    <para />
    /// (20,20)   (4,4)   (10,10)    <para />
    /// (10,10)   (5,5)   (15,15)    <para />
    /// 
    /// l / r  =           <para />
    /// -1.24142857142857	0.485714285714283	1.15809523809524	0.775714285714289	-0.661428571428575 <para /> 
    ///	-1.82571428571429	0.742857142857138	1.72571428571429	1.12285714285715	-1.06571428571429  <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -1.24142857142857,
                -1.82571428571429,
                0.485714285714283,
                0.742857142857138,
                1.15809523809524,
                1.72571428571429,
                0.775714285714289,
                1.12285714285715,
                -0.661428571428575,
                -1.06571428571429
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], 0);
            }
            return asColumnMajorDenseArray;
        }

        RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix34.Get(),
                right: TestableComplexMatrix30.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsRectangularNotLessRowsThanColsComplexMatrixComplexMatrixDivision();
        }
    }
}
