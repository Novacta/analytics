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
    /// 50   1   1    1    1  <para /> 
    /// 40   2   3    4    5  <para /> 
    /// 30   3   6   10   15  <para />
    ///
    /// l / r  =           <para />
    /// (-2.036,-2.036)  (2.695,2.695)	(-0.166,-0.166) <para /> 
    ///	(-2.789,-2.789)	 (3.871,3.871)	(-0.447,-0.447) <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ComplexMatrixState}" />
    class RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[6]
            {
                -2.0369425996205,
                -2.78901802656547,
                2.69588077166351,
                3.87167931688805,
                -0.166231815306769,
                -0.447106261859584
            };
            var asColumnMajorDenseArray = new Complex[6];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix23.Get(),
                right: TestableDoubleMatrix29.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsRectangularLessRowsThanColsComplexMatrixDoubleMatrixDivision();
        }
    }
}
