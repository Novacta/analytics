// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =          <para />
    /// 1  3  5      <para /> 
    /// 2  4  6      <para /> 
    ///
    /// r   =       <para /> 
    /// 50   1    1 <para /> 
    /// 40   2    3 <para /> 
    /// 30   3    6 <para />
    /// 20   4   10 <para />
    /// 10   5   15 <para />
    /// 
    /// l / r  =           <para />
    /// -1.24142857142857	0.485714285714283	1.15809523809524	0.775714285714289	-0.661428571428575 <para /> 
    ///	-1.82571428571429	0.742857142857138	1.72571428571429	1.12285714285715	-1.06571428571429  <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{DoubleMatrixState}" />
    class RightIsRectangularNotLessRowsThanColsDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<DoubleMatrixState>
    {
        RightIsRectangularNotLessRowsThanColsDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[10]
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
                         -1.06571428571429 },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix34.Get(),
                right: TestableDoubleMatrix30.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularNotLessRowsThanColsDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularNotLessRowsThanColsDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsRectangularNotLessRowsThanColsDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsRectangularNotLessRowsThanColsDoubleMatrixDoubleMatrixDivision();
        }
    }
}
