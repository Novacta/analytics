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
    /// l   =      <para /> 
    /// -1         <para /> 
    /// 
    /// r =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// l - r  =         <para />
    /// (-1, 0)  (-3,-2)  (-5,-4)   <para /> 
    ///	(-2,-1)  (-4,-3)  (-6,-5)   <para />       
    ///	</summary>
    class LeftIsScalarDoubleMatrixComplexMatrixSubtraction :
        TestableDoubleMatrixComplexMatrixSubtraction<ComplexMatrixState>
    {
        LeftIsScalarDoubleMatrixComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-1,  0), 
                        new(-2, -1), 
                        new(-3, -2), 
                        new(-4, -3), 
                        new(-5, -4),
                        new(-6, -5)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix17.Get(),
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsScalarDoubleMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsScalarDoubleMatrixComplexMatrixSubtraction"/> class.</returns>
        public static LeftIsScalarDoubleMatrixComplexMatrixSubtraction Get()
        {
            return new LeftIsScalarDoubleMatrixComplexMatrixSubtraction();
        }
    }

}
