// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =     <para /> 
    /// (0,0)       <para /> 
    ///
    /// r =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    /// 
    /// l + r  =        <para />
    /// (0,0)  (2,0)  (4,0)   <para /> 
    ///	(1,0)  (3,0)  (5,0)   <para />       
    ///	</summary>
    class LeftIsNeutralComplexScalarDoubleMatrixAddition :
        TestableComplexScalarDoubleMatrixAddition<ComplexMatrixState>
    {
        LeftIsNeutralComplexScalarDoubleMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        0,
                        new(1, 0),
                        new(2, 0),
                        new(3, 0),
                        new(4, 0),
                        new(5, 0)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: 0.0,
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNeutralComplexScalarDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNeutralComplexScalarDoubleMatrixAddition"/> class.</returns>
        public static LeftIsNeutralComplexScalarDoubleMatrixAddition Get()
        {
            return new LeftIsNeutralComplexScalarDoubleMatrixAddition();
        }
    }
}
