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
    /// 0    2    3    4    5  <para /> 
    /// 0    0    6   10   15  <para />
    /// 0    0    0   20   35  <para />
    /// 0    0    0    0   70  <para />
    ///
    /// l / r  =           <para />
    /// 1.0000    1.0000    0.1667    0.0167   -0.0012 <para /> 
    ///	2.0000    1.0000    0.1667    0.0167   -0.0012 <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{DoubleMatrixState}" />
    class RightIsUpperTriangularDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<DoubleMatrixState>
    {
        RightIsUpperTriangularDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[10]
                    { 1, 2, 1, 1, 0.1667, 0.1667, 0.0167, 0.0167, -0.0012, -0.0012 },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix25.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsUpperTriangularDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsUpperTriangularDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsUpperTriangularDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsUpperTriangularDoubleMatrixDoubleMatrixDivision();
        }
    }
}
