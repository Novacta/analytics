// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
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
    /// (-4,-4)  (-2,-2)  (-0,-0)       <para /> 
    ///
    /// ComplexMatrix.ElementWiseMultiply(l, r)  => <para />
    /// ( 0, 0)  (-6,-6)  (-4,-4)    <para /> 
    ///	(-4,-4)  (-6,-6)  ( 0, 0)    <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixElementWiseMultiplication{ComplexMatrixState}" />
    class TypicalDoubleMatrixComplexMatrixElementWiseMultiplication :
        TestableDoubleMatrixComplexMatrixElementWiseMultiplication<ComplexMatrixState>
    {
        TypicalDoubleMatrixComplexMatrixElementWiseMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        0,
                        new(-4, -4),
                        new(-6, -6),
                        new(-6, -6),
                        new(-4, -4),
                        0
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableComplexMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixComplexMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixComplexMatrixElementWiseMultiplication"/> class.</returns>
        public static TypicalDoubleMatrixComplexMatrixElementWiseMultiplication Get()
        {
            return new TypicalDoubleMatrixComplexMatrixElementWiseMultiplication();
        }
    }
}
