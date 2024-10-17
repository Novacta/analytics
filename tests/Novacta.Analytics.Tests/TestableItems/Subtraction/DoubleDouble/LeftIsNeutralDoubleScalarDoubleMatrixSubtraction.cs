// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =     <para /> 
    /// 0.0       <para /> 
    ///
    /// r =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    /// 
    /// l - r  =        <para />
    ///  0.0  -2.0  -4.0   <para /> 
    ///	-1.0  -3.0  -5.0   <para />       
    ///	</summary>
    class LeftIsNeutralDoubleScalarDoubleMatrixSubtraction :
        TestableDoubleScalarDoubleMatrixSubtraction<DoubleMatrixState>
    {
        LeftIsNeutralDoubleScalarDoubleMatrixSubtraction() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [0, -1, -2, -3, -4, -5],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: 0.0,
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNeutralDoubleScalarDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNeutralDoubleScalarDoubleMatrixSubtraction"/> class.</returns>
        public static LeftIsNeutralDoubleScalarDoubleMatrixSubtraction Get()
        {
            return new LeftIsNeutralDoubleScalarDoubleMatrixSubtraction();
        }
    }
}
