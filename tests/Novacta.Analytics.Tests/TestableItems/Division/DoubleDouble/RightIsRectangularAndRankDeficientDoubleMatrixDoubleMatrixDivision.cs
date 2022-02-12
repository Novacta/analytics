// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

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
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{Tools.DoubleMatrixState}" />
    class RightIsRectangularAndRankDeficientDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<ArgumentException>
    {
        RightIsRectangularAndRankDeficientDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new ArgumentException(
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_MAT_DIVIDE_RANK_DEFICIENT_OPERATION"),
                       "right"),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix33.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularAndRankDeficientDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularAndRankDeficientDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsRectangularAndRankDeficientDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsRectangularAndRankDeficientDoubleMatrixDoubleMatrixDivision();
        }
    }
}
