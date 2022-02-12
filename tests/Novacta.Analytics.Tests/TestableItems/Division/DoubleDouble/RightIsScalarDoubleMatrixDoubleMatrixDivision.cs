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
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    ///
    /// r   =      <para /> 
    /// 2.0        <para /> 
    ///
    /// l / r  =         <para />
    ///  0.0  1.0  2     <para /> 
    ///	 0.5  1.5  2.5   <para />       
    ///	</summary>
    class RightIsScalarDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<DoubleMatrixState>
    {
        RightIsScalarDoubleMatrixDoubleMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] { 0, .5, 1, 1.5, 2, 2.5 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsScalarDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsScalarDoubleMatrixDoubleMatrixDivision();
        }
    }
}
