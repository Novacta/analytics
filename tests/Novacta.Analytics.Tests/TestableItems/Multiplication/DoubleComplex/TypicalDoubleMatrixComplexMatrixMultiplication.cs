// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    ///
    /// r   =       <para /> 
    /// (-5,-5)        <para /> 
    /// (-4,-4)        <para /> 
    /// (-3,-3)        <para /> 
    ///
    /// l * r  =           <para />
    /// (-20,-20)              <para /> 
    ///	(-32,-32)              <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixMultiplication{ComplexMatrixState}" />
    class TypicalDoubleMatrixComplexMatrixMultiplication :
        TestableDoubleMatrixComplexMatrixMultiplication<ComplexMatrixState>
    {
        TypicalDoubleMatrixComplexMatrixMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-20, -20), 
                        new(-32, -32) 
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 1),
                left: TestableDoubleMatrix16.Get(),
                right: TestableComplexMatrix20.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixComplexMatrixMultiplication"/> class.</returns>
        public static TypicalDoubleMatrixComplexMatrixMultiplication Get()
        {
            return new TypicalDoubleMatrixComplexMatrixMultiplication();
        }
    }
}
