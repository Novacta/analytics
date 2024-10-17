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
    /// 1    0    0    0    0  <para /> 
    /// 1    2    0    0    0  <para /> 
    /// 1    3    6    0    0  <para />
    /// 1    4   10   20    0  <para />
    /// 1    5   15   35   70  <para />
    ///
    /// l / r  =           <para />
    /// -0.0303571428571429	0.473214285714286	0.303571428571429	0.125	0.128571428571429 <para /> 
    ///	 0.560714285714286	0.753571428571428	0.392857142857143	0.15	0.142857142857143 <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{DoubleMatrixState}" />
    class RightIsLowerTriangularDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<DoubleMatrixState>
    {
        RightIsLowerTriangularDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray:
                    [
                        -0.0303571428571429,
                         0.560714285714286,
                         0.473214285714286,
                         0.753571428571428,
                         0.303571428571429,
                         0.392857142857143,
                         0.125,
                         0.15,
                         0.128571428571429,
                         0.142857142857143
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix24.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsLowerTriangularDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsLowerTriangularDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsLowerTriangularDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsLowerTriangularDoubleMatrixDoubleMatrixDivision();
        }
    }
}
