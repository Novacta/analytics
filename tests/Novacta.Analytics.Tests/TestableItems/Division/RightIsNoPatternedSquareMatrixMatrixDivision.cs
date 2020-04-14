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
    /// 20   4   10   20   35  <para />
    /// 10   5   15   35   70  <para />
    ///
    /// l / r  =           <para />
    /// -3.4167    6.8333   -4.8333    2.4167   -0.4833 <para /> 
    ///	-6.5000   15.0000  -13.0000    6.5000   -1.3000 <para />   
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixDivision{DoubleMatrixState}" />
    class RightIsNoPatternedSquareMatrixMatrixDivision :
        TestableMatrixMatrixDivision<DoubleMatrixState>
    {
        RightIsNoPatternedSquareMatrixMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[10]
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
                        -1.3 },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix28.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNoPatternedSquareMatrixMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNoPatternedSquareMatrixMatrixDivision"/> class.</returns>
        public static RightIsNoPatternedSquareMatrixMatrixDivision Get()
        {
            return new RightIsNoPatternedSquareMatrixMatrixDivision();
        }
    }
}
