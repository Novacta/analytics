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
    /// (1,1)  (3,3)  (5,5)  (7,7)  ( 9, 9)    <para /> 
    /// (2,2)  (4,4)  (6,6)  (8,8)  (10,10)    <para /> 
    ///
    /// r   =                  <para /> 
    /// (50,50)   (1,1)   ( 1, 1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (40,40)   (2,2)   ( 3, 3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (30,30)   (3,3)   ( 6, 6)   (10,10)   (15,15)  <para />
    ///
    /// l / r  =>           <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{Tools.ComplexMatrixState}" />
    class RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ArgumentException>
    {
        RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ArgumentException(
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_MAT_DIVIDE_RANK_DEFICIENT_OPERATION"),
                       "right"),
                left: TestableComplexMatrix23.Get(),
                right: TestableComplexMatrix33.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsRectangularAndRankDeficientComplexMatrixComplexMatrixDivision();
        }
    }
}
