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
    /// 1    1    1    1    1  <para /> 
    /// 1    2    3    4    5  <para /> 
    /// 0    3    6   10   15  <para />
    /// 0    0   10   20   35  <para />
    /// 0    0    0   35   70  <para />
    ///
    /// l / r  =           <para />
    /// -0.999	    1.999	4.246E-15	-2.151E-16	-1.517E-16 <para /> 
    ///	 6.260E-15  1.999	5.398E-15	-6.203E-16	-1.674E-16 <para />   
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixDivision{DoubleMatrixState}" />
    class RightIsHessenbergMatrixMatrixDivision :
        TestableMatrixMatrixDivision<DoubleMatrixState>
    {
        RightIsHessenbergMatrixMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[10]
                    {
                        -0.999,
                         6.260E-15,
                         1.999,
                         1.999,
                         4.246E-15,
                         5.398E-15,
                        -2.151E-16,
                        -6.203E-16,
                        -1.517E-16,
                        -1.674E-16 },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix27.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsHessenbergMatrixMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsHessenbergMatrixMatrixDivision"/> class.</returns>
        public static RightIsHessenbergMatrixMatrixDivision Get()
        {
            return new RightIsHessenbergMatrixMatrixDivision();
        }
    }
}
