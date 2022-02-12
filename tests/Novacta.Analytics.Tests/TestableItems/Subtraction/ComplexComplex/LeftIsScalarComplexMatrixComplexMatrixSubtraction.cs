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
    /// l   =      <para /> 
    /// (-1,-1)       <para /> 
    /// 
    /// r =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// l - r  =         <para />
    /// (-1,-1)  (-3,-3)  (-5,-5)   <para /> 
    ///	(-2,-2)  (-4,-4)  (-6,-6)   <para />       
    ///	</summary>
    class LeftIsScalarComplexMatrixComplexMatrixSubtraction :
        TestableComplexMatrixComplexMatrixSubtraction<ComplexMatrixState>
    {
        LeftIsScalarComplexMatrixComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        new Complex(-1, -1), 
                        new Complex(-2, -2), 
                        new Complex(-3, -3), 
                        new Complex(-4, -4), 
                        new Complex(-5, -5),
                        new Complex(-6, -6)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix17.Get(),
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsScalarComplexMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsScalarComplexMatrixComplexMatrixSubtraction"/> class.</returns>
        public static LeftIsScalarComplexMatrixComplexMatrixSubtraction Get()
        {
            return new LeftIsScalarComplexMatrixComplexMatrixSubtraction();
        }
    }

}
