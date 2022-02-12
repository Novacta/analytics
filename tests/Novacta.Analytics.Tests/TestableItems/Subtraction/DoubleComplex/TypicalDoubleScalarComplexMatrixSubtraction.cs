// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =      <para /> 
    /// -1       <para /> 
    ///
    /// r =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    /// 
    /// l - r  =         <para />
    /// (-1, 0)  (-3,-2)  (-5,-4)   <para /> 
    ///	(-2,-1)  (-4,-3)  (-6,-5)   <para />       
    ///	</summary>
    class TypicalDoubleScalarComplexMatrixSubtraction :
        TestableDoubleScalarComplexMatrixSubtraction<ComplexMatrixState>
    {
        TypicalDoubleScalarComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    {
                        new Complex(-1, 0), 
                        new Complex(-2, -1), 
                        new Complex(-3, -2), 
                        new Complex(-4, -3), 
                        new Complex(-5, -4),
                        new Complex(-6, -5)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: -1,
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleScalarComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleScalarComplexMatrixSubtraction"/> class.</returns>
        public static TypicalDoubleScalarComplexMatrixSubtraction Get()
        {
            return new TypicalDoubleScalarComplexMatrixSubtraction();
        }
    }
}
