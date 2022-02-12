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
    /// r   =     <para /> 
    /// 1.0       <para /> 
    ///
    /// l / r  =        <para />
    /// 0.0  2.0  4.0   <para /> 
    ///	1.0  3.0  5.0   <para />       
    ///	</summary>
    class RightIsNeutralDoubleMatrixDoubleScalarDivision :
        TestableDoubleMatrixDoubleScalarDivision<DoubleMatrixState>
    {
        RightIsNeutralDoubleMatrixDoubleScalarDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] { 0, 1, 2, 3, 4, 5 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: 1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNeutralDoubleMatrixDoubleScalarDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNeutralDoubleMatrixDoubleScalarDivision"/> class.</returns>
        public static RightIsNeutralDoubleMatrixDoubleScalarDivision Get()
        {
            return new RightIsNeutralDoubleMatrixDoubleScalarDivision();
        }
    }
}
