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
    /// (-1,-1)       <para /> 
    ///
    /// r =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    /// 
    /// l - r  =         <para />
    /// (-1,-1)  (-3,-1)  (-5,-1)   <para /> 
    ///	(-2,-1)  (-4,-1)  (-6,-1)   <para />       
    ///	</summary>
    class TypicalComplexScalarDoubleMatrixSubtraction :
        TestableComplexScalarDoubleMatrixSubtraction<ComplexMatrixState>
    {
        TypicalComplexScalarDoubleMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    {
                        new Complex(-1, -1), 
                        new Complex(-2, -1), 
                        new Complex(-3, -1), 
                        new Complex(-4, -1), 
                        new Complex(-5, -1),
                        new Complex(-6, -1)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: new Complex(-1, -1),
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexScalarDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexScalarDoubleMatrixSubtraction"/> class.</returns>
        public static TypicalComplexScalarDoubleMatrixSubtraction Get()
        {
            return new TypicalComplexScalarDoubleMatrixSubtraction();
        }
    }
}
