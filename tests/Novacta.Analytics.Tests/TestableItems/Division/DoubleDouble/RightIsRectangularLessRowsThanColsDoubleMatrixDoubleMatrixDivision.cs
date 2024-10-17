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
    /// l =                    <para />
    /// 1  3  5  7  9    <para /> 
    /// 2  4  6  8 10    <para /> 
    ///
    /// r   =                  <para /> 
    /// 50   1    1    1    1  <para /> 
    /// 40   2    3    4    5  <para /> 
    /// 30   3    6   10   15  <para />
    ///
    /// l / r  =           <para />
    /// -2.0369425996205    2.69588077166351	-0.166231815306769 <para /> 
    ///	-2.78901802656547	3.87167931688805	-0.447106261859584 <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{DoubleMatrixState}" />
    class RightIsRectangularLessRowsThanColsDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<DoubleMatrixState>
    {
        RightIsRectangularLessRowsThanColsDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray:
                    [
                         -2.0369425996205,
                         -2.78901802656547,
                          2.69588077166351,
                          3.87167931688805,
                         -0.166231815306769,
                         -0.447106261859584 ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix29.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularLessRowsThanColsDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularLessRowsThanColsDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsRectangularLessRowsThanColsDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsRectangularLessRowsThanColsDoubleMatrixDoubleMatrixDivision();
        }
    }
}
