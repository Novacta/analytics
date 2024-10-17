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
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =   <para /> 
    /// 0       <para /> 
    ///
    /// l - r  =        <para />
    /// (0,0)  (2,2)  (4,4)   <para /> 
    ///	(1,1)  (3,3)  (5,5)   <para />       
    ///	</summary>
    class RightIsNeutralComplexMatrixDoubleScalarSubtraction :
        TestableComplexMatrixDoubleScalarSubtraction<ComplexMatrixState>
    {
        RightIsNeutralComplexMatrixDoubleScalarSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        0,
                        new(1, 1), 
                        new(2, 2), 
                        new(3, 3), 
                        new(4, 4), 
                        new(5, 5) 
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: 0.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNeutralComplexMatrixDoubleScalarSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNeutralComplexMatrixDoubleScalarSubtraction"/> class.</returns>
        public static RightIsNeutralComplexMatrixDoubleScalarSubtraction Get()
        {
            return new RightIsNeutralComplexMatrixDoubleScalarSubtraction();
        }
    }
}
