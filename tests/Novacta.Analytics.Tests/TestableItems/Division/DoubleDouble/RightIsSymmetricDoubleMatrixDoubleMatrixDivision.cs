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
    /// 1    3    6   10   15  <para />
    /// 1    4   10   20   35  <para />
    /// 1    5   15   35   70  <para />
    ///
    /// l / r  =           <para />
    /// -1.0000    2.0000    0   0  0 <para /> 
    ///	 0         2.0000    0   0  0 <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{DoubleMatrixState}" />
    class RightIsSymmetricDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<DoubleMatrixState>
    {
        RightIsSymmetricDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[10]
                        { -1, 0, 2, 2, 0, 0, 0, 0, 0, 0 },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix26.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsSymmetricDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsSymmetricDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsSymmetricDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsSymmetricDoubleMatrixDoubleMatrixDivision();
        }
    }
}
