// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    ///
    /// r   =                  <para /> 
    /// (-5,-5)  (-3,-3)  (-1,-1)       <para /> 
    /// (-4,-4)  (-2,-2)  ( 0, 0)       <para /> 
    ///
    /// l + r  =           <para />
    /// (-5,-5)  (-1,-3)  (3,-1)    <para /> 
    ///	(-3,-4)  ( 1,-2)  (5, 0)    <para />   
    /// </summary>
    class TypicalDoubleMatrixComplexMatrixAddition :
        TestableDoubleMatrixComplexMatrixAddition<ComplexMatrixState>
    {
        TypicalDoubleMatrixComplexMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        new Complex(-5, -5), 
                        new Complex(-3, -4),
                        new Complex(-1, -3), 
                        new Complex(1, -2), 
                        new Complex(3, -1),
                        new Complex(5, 0)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableComplexMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixComplexMatrixAddition"/> class.</returns>
        public static TypicalDoubleMatrixComplexMatrixAddition Get()
        {
            return new TypicalDoubleMatrixComplexMatrixAddition();
        }
    }
}
