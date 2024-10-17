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
    /// l =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    ///
    /// r   =     <para /> 
    /// (0,0)       <para /> 
    ///
    /// l - r  =        <para />
    /// 0  2  4   <para /> 
    ///	1  3  5   <para />       
    ///	</summary>
    class RightIsNeutralDoubleMatrixComplexScalarSubtraction :
        TestableDoubleMatrixComplexScalarSubtraction<ComplexMatrixState>
    {
        RightIsNeutralDoubleMatrixComplexScalarSubtraction() :
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
                left: TestableDoubleMatrix16.Get(),
                right: 0.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNeutralDoubleMatrixComplexScalarSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNeutralDoubleMatrixComplexScalarSubtraction"/> class.</returns>
        public static RightIsNeutralDoubleMatrixComplexScalarSubtraction Get()
        {
            return new RightIsNeutralDoubleMatrixComplexScalarSubtraction();
        }
    }
}
