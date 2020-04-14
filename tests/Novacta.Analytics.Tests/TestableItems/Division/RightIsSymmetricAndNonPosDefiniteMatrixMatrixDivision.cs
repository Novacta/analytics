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
    /// 0    1    1    1    1  <para /> 
    /// 1    2    3    4    5  <para /> 
    /// 1    3    6   10   15  <para />
    /// 1    4   10   20   35  <para />
    /// 1    5   15   35   70  <para />
    ///
    /// l / r  =           <para />
    /// 0.2500   -0.5000    2.5000   -1.2500    0.2500 <para /> 
    ///	0         2.0000         0         0         0 <para />   
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixDivision{DoubleMatrixState}" />
    class RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision :
        TestableMatrixMatrixDivision<DoubleMatrixState>
    {
        RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[10]
                        { 0.25, 0, -0.5, 2, 2.5, 0, -1.25, 0, 0.25, 0 },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableDoubleMatrix31.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision"/> class.</returns>
        public static RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision Get()
        {
            return new RightIsSymmetricAndNonPosDefiniteMatrixMatrixDivision();
        }
    }
}
